using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LightHouse))]
public class AI : MonoBehaviour
{
	private LightHouse lh;
	public float durationAdd = 4;
	private void Awake()
	{
		lh = GetComponent<LightHouse>();
	}

	private void Update()
	{
		if (lh.isInsideWave)
		{
			lh.lastWave.Bounce(lh,lh.lastWaveArc.duration + durationAdd);
		}
	}
}
