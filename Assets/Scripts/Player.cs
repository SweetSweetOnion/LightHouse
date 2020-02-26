using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LightHouse))]
public class Player : MonoBehaviour
{
	private LightHouse lh;
	private Animator animator;

	public float baseDuration = 5;
	public float addDuration = 2;
	/*public AnimationCurve chargeCurve;
	public Transform lightHouse;
	public Transform targetPos;
	private Vector3 startPos, startScale;
	private Quaternion startRot;*/

	public float minCharDuration = 1.5f;
	private float chargeDuration = 0;

	private void Awake()
	{
		lh = GetComponent<LightHouse>();
		animator = GetComponent<Animator>();
	}

	/*private void Start()
	{
		startPos = lightHouse.transform.position;
		startScale = lightHouse.transform.localScale;
		startRot = lightHouse.transform.rotation;
	}*/

	private void Update()
	{
		if(Input.GetMouseButton(0) ||  Input.GetAxis("RightTrigger") >= 0.5f)
		{
			chargeDuration += Time.deltaTime;
			animator.SetBool("Charging",true);
		}else
		{
			if(chargeDuration > minCharDuration)
			{
				animator.SetTrigger("Impact");
				if (lh.isInsideWave)
				{
					lh.CreateWave(lh.lastWaveArc.duration + addDuration);
				}
				else
				{
					lh.CreateWave(baseDuration);
				}	
			}
			animator.SetBool("Charging", false);
			chargeDuration = 0;
		}

		

		/*lightHouse.transform.position = Vector3.Lerp(startPos, targetPos.position, GetNormalizedCharge());
		lightHouse.transform.localScale = Vector3.Lerp(startScale, targetPos.localScale, GetNormalizedCharge());
		lightHouse.transform.rotation = Quaternion.Lerp(startRot, targetPos.rotation, GetNormalizedCharge());*/
	}

	public float GetNormalizedCharge()
	{
		return Mathf.Clamp01( chargeDuration / minCharDuration);
	}
}
