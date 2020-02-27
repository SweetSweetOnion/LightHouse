using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LightHouse))]
public class AI : MonoBehaviour
{
	private LightHouse lh;
	[Range(0,1)]
	public float rangeAngle = 1;
	public Vector3 direction = Vector3.forward;

	private Wave lastWaveBounce = null;
	private void Awake()
	{
		lh = GetComponent<LightHouse>();
	}

	private void Update()
	{
		if (lh.isInsideWave)
		{
			if(lastWaveBounce != lh.lastWaveReceive)
			{
				lastWaveBounce = lh.lastWaveReceive;
				lh.lastWaveReceive.Bounce(lh, lh.lastWaveReceive.maxDuration, direction, rangeAngle);
			}		
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawRay(transform.position, direction.normalized*10);
	}
}
