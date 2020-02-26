using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LightHouse))]
public class Player : MonoBehaviour
{
	private LightHouse lh;

	private void Awake()
	{
		lh = GetComponent<LightHouse>();
	}

	private void Update()
	{
		if(Input.anyKeyDown)
		{
			lh.CreateWave(10);
		}
	}
}
