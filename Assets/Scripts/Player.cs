using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LightHouse))]
public class Player : MonoBehaviour
{
	private LightHouse lh;
	public float baseDuration = 5;
	public float addDuration = 2;
	public AnimationCurve chargeCurve;
	public Transform lightHouse;
	public Transform targetPos;
	private Vector3 startPos, startScale;
	private Quaternion startRot;

	public float minCharDuration = 1.5f;
	private float chargeDuration = 0;

	private void Awake()
	{
		lh = GetComponent<LightHouse>();
	}

	private void Start()
	{
		startPos = lightHouse.transform.position;
		startScale = lightHouse.transform.localScale;
		startRot = lightHouse.transform.rotation;
	}

	private void Update()
	{
		if(Input.GetAxis("RightTrigger") >= 0.5f)
		{
			chargeDuration += Time.deltaTime;
		}else
		{
			if(chargeDuration > minCharDuration)
			{
				if (lh.isInsideWave)
				{
					lh.CreateWave(lh.lastWaveArc.duration + addDuration);
				}
				else
				{
					lh.CreateWave(baseDuration);
				}	
			}
			chargeDuration = 0;
		}

		lightHouse.transform.position = Vector3.Lerp(startPos, targetPos.position, GetNormalizedCharge());
		lightHouse.transform.localScale = Vector3.Lerp(startScale, targetPos.localScale, GetNormalizedCharge());
		lightHouse.transform.rotation = Quaternion.Lerp(startRot, targetPos.rotation, GetNormalizedCharge());
	}

	public float GetNormalizedCharge()
	{
		return Mathf.Clamp01( chargeDuration / minCharDuration);
	}
}
