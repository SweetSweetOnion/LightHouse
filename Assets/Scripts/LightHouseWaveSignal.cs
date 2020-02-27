using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LightHouse))]
public class LightHouseWaveSignal : MonoBehaviour
{
	private LightHouse lh;
	public MeshRenderer lamp;
	public Color color = Color.green;
	public float intensity = 10;

	private void Awake()
	{
		lh = GetComponent<LightHouse>();
	}

	private void Update()
	{
		if (lh.isInsideWave)
		{
			lamp.material.SetColor("_EmissionColor",color * intensity);
		}else
		{
			lamp.material.SetColor("_EmissionColor", Color.black);
		}
	}
}
