using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour
{
	
	private LightHouse emitter;
	private float duration = 0;
	private float scale = 0;

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
		if(duration <= 0)
		{
			Destroy(gameObject);
		}
	}

	private void DebugDisplay()
	{
		float angle = 0;
		float thick = WaveManager.instance.waveThickness;
		for(int i = 0; i< 100; i++)
		{
			Vector3 v0 = new Vector3(Mathf.Cos(angle) * scale, 0, Mathf.Sin(angle) * scale);
			Vector3 v1 = new Vector3(Mathf.Cos(angle) * (scale-thick) , 0, Mathf.Sin(angle) * (scale - thick));
			angle += Mathf.PI * 2 / 100;
			Vector3 v2 = new Vector3(Mathf.Cos(angle) * scale, 0, Mathf.Sin(angle) * scale);
			Vector3 v3 = new Vector3(Mathf.Cos(angle) * (scale - thick), 0, Mathf.Sin(angle) * (scale - thick));

			Debug.DrawLine(v0, v2);
			Debug.DrawLine(v1, v3);
		}
	}


	public static Wave SpawnWave(LightHouse l, int waveDuration)
	{
		GameObject g = new GameObject("new wave");
		Wave w = g.AddComponent<Wave>();
		g.transform.position = l.transform.position;
		w.emitter = l;
		w.duration = waveDuration;
		return w;
	}
}
