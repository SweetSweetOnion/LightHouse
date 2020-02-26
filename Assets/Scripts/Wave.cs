using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour
{

	private LightHouse emitter;
	private float duration = 0;
	private float scale = 0;

	private List<LightHouse> reboundList = new List<LightHouse>();


	private void OnEnable()
	{
		WaveManager.instance.AddWave(this);
	}

	private void OnDisable()
	{
		WaveManager.instance.RemoveWave(this);
	}

	private void Update()
	{
		duration -= Time.deltaTime;
		scale += WaveManager.instance.waveSpeed * Time.deltaTime;
		DebugDisplay();
		if (duration <= 0)
		{
			Destroy(gameObject);
		}
	}

	private void DebugDisplay()
	{
		float angle = 0;
		float thick = WaveManager.instance.waveThickness;
		Color c = new Color(1,1,1, duration/10);
		for (int i = 0; i < 100; i++)
		{
			Vector3 v0 = new Vector3(Mathf.Cos(angle) * scale, 0, Mathf.Sin(angle) * scale);
			Vector3 v1 = new Vector3(Mathf.Cos(angle) * (scale - thick), 0, Mathf.Sin(angle) * (scale - thick));
			angle += Mathf.PI * 2 / 100;
			Vector3 v2 = new Vector3(Mathf.Cos(angle) * scale, 0, Mathf.Sin(angle) * scale);
			Vector3 v3 = new Vector3(Mathf.Cos(angle) * (scale - thick), 0, Mathf.Sin(angle) * (scale - thick));

			Debug.DrawLine(transform.position + v0, transform.position + v2,c);
			Debug.DrawLine(transform.position + v1, transform.position + v3,c);
		}
	}

	public bool IsInsideWave(Vector3 pos)
	{
		float dist = (new Vector3(emitter.transform.position.x, 0, emitter.transform.position.z) - new Vector3(pos.x, 0, pos.z)).magnitude;
		return (dist < scale && dist > scale - WaveManager.instance.waveThickness);
	}

	public bool CanBounce(LightHouse l)
	{
		return !reboundList.Contains(l) && l != emitter;
	}

	public void Bounce(LightHouse l)
	{
		SpawnWave(l, duration + 1);
		reboundList.Add(l);
	}

	public static Wave SpawnWave(LightHouse l, float waveDuration)
	{
		GameObject g = new GameObject("new wave");
		Wave w = g.AddComponent<Wave>();
		g.transform.position = l.transform.position;
		w.emitter = l;
		w.duration = waveDuration;
		return w;
	}
}
