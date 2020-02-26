using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LightHouse))]
public class AI : MonoBehaviour
{
	private LightHouse lh;
	private void Awake()
	{
		lh = GetComponent<LightHouse>();
	}

	private void Update()
	{
		for(int i =0; i< WaveManager.instance.allWaves.Count; i++)
		{
			Wave w = WaveManager.instance.allWaves[i];
			if (w.IsInsideWave(transform.position))
			{
				Debug.DrawRay(transform.position, Vector3.up * 30,Color.red,1);
				if (w.CanBounce(lh))
				{
					w.Bounce(lh);
				}
			}
		}
	}
}
