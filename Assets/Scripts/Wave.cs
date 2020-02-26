using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour
{
	const int RESOLUTION = 50;

	private LightHouse emitter;

	private List<LightHouse> reboundList = new List<LightHouse>();

	private WaveArc[] arcs;

	private MeshFilter mf;
	private MeshRenderer rend;
	private Mesh mesh;

	private void OnEnable()
	{
		WaveManager.instance.AddWave(this);
	}

	private void OnDisable()
	{
		WaveManager.instance.RemoveWave(this);
	}

	private void InitWave(float duration, Vector3 position, Vector3 direction, float rangeAngle)
	{
		arcs = new WaveArc[RESOLUTION];
		float angle = 0;
		float a = Vector3.SignedAngle(direction.normalized, Vector3.forward,Vector3.up) * Mathf.Deg2Rad;
		for (int i = 0; i < arcs.Length; i++)
		{
			Vector3 v = new Vector3(Mathf.Cos(angle),0, Mathf.Sin(angle));
			float dot = Vector3.Dot(direction.normalized, v.normalized);
			dot = (dot + 1) / 2;
			//float d = 1.0f - (Mathf.Abs(angle - a)) / Mathf.PI*2;	
			if(dot>= rangeAngle)
			{
				dot = duration;
			}else
			{
				dot = 0;
			}
			arcs[i] = new WaveArc(dot,WaveManager.instance.waveSpeed,WaveManager.instance.waveThickness,position,angle,angle + Mathf.PI * 2 / RESOLUTION);
			angle += Mathf.PI * 2 / RESOLUTION;
		}
	}

	private void Update()
	{
		bool isAlive = false;
		float maxDuration = 0;
		for (int i = 0; i < arcs.Length; i++)
		{
			arcs[i].Update();
			arcs[i].DebugDisplay();
			if(arcs[i].duration > maxDuration)
			{
				maxDuration = arcs[i].duration;
			}
			if (arcs[i].IsArcAlive()) isAlive = true;
		}
		rend.material.SetFloat("_Fade", maxDuration/10);
		UpdateMesh();


		if (!isAlive)
		{
			Destroy(gameObject);
		}
		
	}


	public bool IsInsideWave(Vector3 pos, out WaveArc arc)
	{
		bool b = false;
		arc = null;
		foreach(WaveArc a in arcs)
		{
			if (a.IsInside(pos))
			{
				arc = a;
				b = true;
			}
		}
		return b;
	}

	public bool CanBounce(LightHouse l)
	{
		return !reboundList.Contains(l) && l != emitter;
	}

	public void Bounce(LightHouse l, float duration, Vector3 direction, float rangeAngle)
	{
		SpawnWave(l, duration,direction,rangeAngle);
		reboundList.Add(l);
	}

	public static Wave SpawnWave(LightHouse l, float waveDuration, Vector3 direction, float rangeAngle)
	{
		GameObject g = new GameObject("new wave");
		Wave w = g.AddComponent<Wave>();
		w.mf = g.AddComponent<MeshFilter>();
		w.rend = g.AddComponent<MeshRenderer>();
		w.mesh = new Mesh();
		w.mesh.MarkDynamic();
		w.mf.mesh = w.mesh;
		w.rend.material = WaveManager.instance.waveMaterial;
		g.transform.position = Vector3.zero;
		w.emitter = l;
		w.InitWave(waveDuration,l.transform.position, direction, rangeAngle);
		return w;
	}

	private void UpdateMesh()
	{
		//Vector3[] vertices = new Vector3[arcs.Length * 4];
		List<Vector3> vertices = new List<Vector3>();
		List<int> tris = new List<int>();
		mesh.Clear();
		for(int i = 0; i < arcs.Length; i++)
		{
			vertices.Add(arcs[i].p1);
			vertices.Add(arcs[i].p2);
			vertices.Add(arcs[i].p3);
			vertices.Add(arcs[i].p4);

			tris.Add(i * 4 + 0);
			tris.Add(i * 4 + 2);
			tris.Add(i * 4 + 1);

			tris.Add(i * 4 + 1);
			tris.Add(i * 4 + 2);
			tris.Add(i * 4 + 3);
		}

		mesh.vertices = vertices.ToArray();
		mesh.triangles = tris.ToArray();

		mesh.RecalculateNormals();
		mesh.MarkDynamic();
		mesh.RecalculateBounds();
		mf.mesh = mesh;

	}
}
