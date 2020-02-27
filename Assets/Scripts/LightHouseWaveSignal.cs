using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LightHouse))]
public class LightHouseWaveSignal : MonoBehaviour
{
	private LightHouse lh;
	public MeshRenderer lamp;
	public Color color = Color.green;
	public float intensity = 10;

	private bool active;

	public static int activeCount = 0;
	public delegate void BasicEvent();
	public static BasicEvent OnLightHouseActive;

	private void Awake()
	{
		lh = GetComponent<LightHouse>();
	}

	private void OnEnable()
	{
		lh.OnReceiveWave += OnReceiveWave;
	}

	private void OnDisable()
	{
		lh.OnReceiveWave -= OnReceiveWave;
	}

	private void OnReceiveWave()
	{
		if (GetComponent<AI>())
		{
			if (!active)
			{
				activeCount++;
				OnLightHouseActive?.Invoke();
			}
			active = true;
			
		}
		
	}

	private void Update()
	{
		if (lh.isInsideWave || active)
		{
			lamp.material.SetColor("_EmissionColor",color * intensity);
		}else
		{
			lamp.material.SetColor("_EmissionColor", Color.black);
		}
	}
}
