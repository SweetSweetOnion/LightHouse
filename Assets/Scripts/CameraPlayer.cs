using UnityEngine;
using System.Collections;


public class CameraPlayer : MonoBehaviour
{
	public LightHouse lh;
	private GameObject camParent;
	public float smooth = 3;
	public float rotSpeed = 50;

	private Vector3 startPos;
	private Quaternion startRot;
	private Vector3 target;

	float maxScale = 0;

	private void Awake()
	{
		camParent = transform.parent.gameObject;
	} 

	void Start()
	{
		startPos = transform.localPosition;
		startRot = transform.localRotation;
	}

	void Update()
	{

		float max = GetBiggestWave();
		if (max > maxScale)
		{
			maxScale = max;
		}
		if (lh.waves.Count == 0) maxScale = 0;
		target = startPos + startRot * -Vector3.forward * maxScale/2;
		transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * smooth);

		camParent.transform.Rotate(0, Time.deltaTime * Input.GetAxis("Horizontal") * rotSpeed, 0);
	}

	private float GetBiggestWave()
	{
		float f = 0;
		foreach(Wave w in lh.waves)
		{
			if( w.GetScale() > f)
			{
				f = w.GetScale();
			}
		}
		return f;
	}
}
