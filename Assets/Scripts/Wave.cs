using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour
{
	const int RESOLUTION = 50;

	private LightHouse emitter;

	private List<LightHouse> reboundList = new List<LightHouse>();

	private WaveArc[] arcs;


	private void OnEnable()
	{
		WaveManager.instance.AddWave(this);
	}

	private void OnDisable()
	{
		WaveManager.instance.RemoveWave(this);
	}

	private void InitWave(float duration, Vector3 position)
	{
		arcs = new WaveArc[RESOLUTION];
		float angle = 0;
		for (int i = 0; i < arcs.Length; i++)
		{
			arcs[i] = new WaveArc(duration,WaveManager.instance.waveSpeed,WaveManager.instance.waveThickness,position,angle,angle + Mathf.PI * 2 / RESOLUTION);
			angle += Mathf.PI * 2 / RESOLUTION;
		}
	}

	private void Update()
	{
		bool isAlive = false;
		for (int i = 0; i < arcs.Length; i++)
		{
			arcs[i].Update();
			arcs[i].DebugDisplay();
			if (arcs[i].IsArcAlive()) isAlive = true;
		}

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

	public void Bounce(LightHouse l, float duration)
	{
		SpawnWave(l, duration);
		reboundList.Add(l);
	}

	public static Wave SpawnWave(LightHouse l, float waveDuration)
	{
		GameObject g = new GameObject("new wave");
		Wave w = g.AddComponent<Wave>();
		g.transform.position = l.transform.position;
		w.emitter = l;
		w.InitWave(waveDuration,l.transform.position);
		return w;
	}
}
