using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LightHouse))]
public class LightHouseFX : MonoBehaviour
{
	private LightHouse lh;
	public ParticleSystem partSystem;

	private void Awake()
	{
		lh = GetComponent<LightHouse>();
	}

	private void OnEnable()
	{
		lh.OnCreateWave += OnCreateWave;
	}

	private void OnDisable()
	{
		lh.OnCreateWave -= OnCreateWave;
	}

	private void OnCreateWave()
	{
		partSystem.Play();
	}

}
