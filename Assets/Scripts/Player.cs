using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LightHouse))]
public class Player : MonoBehaviour
{
	private LightHouse lh;
	private Animator animator;

	public float baseDuration = 5;
	public float addDuration = 2;
	public float bufferDuration = 1;
	public Transform lamp;
	

	public float minCharDuration = 1.5f;
	private float chargeDuration = 0;
	private bool chargeComplete = false;
	

	public delegate void BasicEvent();
	public event BasicEvent OnChargeComplete;
	public event BasicEvent OnNewWave;
	public event BasicEvent OnWaveBounce;
	public event BasicEvent OnCharging;
	public event BasicEvent OnChargeCanceled;

	private Vector3 direction = Vector3.forward;
	
	[Range(0,1)]
	public float angleRange = 0.5f;

	private void Awake()
	{
		lh = GetComponent<LightHouse>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		direction = lamp.forward;
		lamp.transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
		if(Input.GetMouseButton(0) ||  Input.GetAxis("RightTrigger") >= 0.5f)
		{
			if(chargeDuration == 0)
			{
				animator.SetBool("Impacting", false);
				OnCharging?.Invoke();
			}
			if (chargeDuration > minCharDuration && chargeComplete == false)
			{
				chargeComplete = true;
				OnChargeComplete?.Invoke();
			}
			chargeDuration += Time.deltaTime;
			animator.SetBool("Charging",true);
			
		}else
		{
			if(chargeDuration > minCharDuration)
			{
				animator.SetBool("Charging", false);
				animator.SetBool("Impacting", true);
				StartCoroutine(WaitAndWave(0.2f));
			

			}else
			{
				if(chargeDuration > 0)
				{
					OnChargeCanceled?.Invoke();
				}
			}
			chargeComplete = false;
			animator.SetBool("Charging", false);
			chargeDuration = 0;
		}
	}

	private IEnumerator WaitAndWave(float delay)
	{
		yield return new WaitForSeconds(delay);
		if (lh.isInsideWave || lh.lastWaveTime > Time.time - bufferDuration)
		{
			lh.CreateWave(lh.lastWaveArc.duration + addDuration, direction, angleRange, Color.red);
			OnWaveBounce?.Invoke();
		}
		else
		{
			lh.CreateWave(baseDuration, direction, angleRange, Color.blue);
			OnNewWave?.Invoke();
		}

	}

	public float GetNormalizedCharge()
	{
		return Mathf.Clamp01( chargeDuration / minCharDuration);
	
	}
}
