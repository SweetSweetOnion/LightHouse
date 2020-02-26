using UnityEngine;
using System.Collections;

public class Reef : MonoBehaviour
{

	private void Update()
	{
		WaveArc arc = null;
		for (int i = 0; i < WaveManager.instance.allWaves.Count; i++)
		{
			Wave w = WaveManager.instance.allWaves[i];
			if (w.IsInsideWave(transform.position, out arc))
			{
				arc.StopArc();
			}
		}
	}
}
