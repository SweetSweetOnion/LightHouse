using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouse : MonoBehaviour
{
	private bool _isInsideWave = false;
	private Wave _lastWaveReceive = null;
	private WaveArc _lastWaveArc = null;
	private float _lastWaveTime = 0;

	//accessors
	public bool isInsideWave => _isInsideWave;
	public Wave lastWaveReceive => _lastWaveReceive;
	public WaveArc lastWaveArc => _lastWaveArc;
	public float lastWaveTime => _lastWaveTime;



	public delegate void BasicEvent();
	public event BasicEvent OnCreateWave;


	public void CreateWave(float waveLevel, Vector3 direction, float rangeAngle,Color c)
	{
		Wave.SpawnWave(this, waveLevel, direction, rangeAngle, c);
		OnCreateWave?.Invoke();
	}

	private void Update()
	{
		CheckInside();
	}

	private void CheckInside()
	{
		_isInsideWave = false;
		WaveArc arc = null;
		for (int i = 0; i < WaveManager.instance.allWaves.Count; i++)
		{
			Wave w = WaveManager.instance.allWaves[i];
			if (w.IsInsideWave(transform.position, out arc))
			{
				if (w.CanBounce(this))
				{
					_lastWaveReceive = w;
					_isInsideWave = true;
					_lastWaveArc = arc;
					_lastWaveTime = Time.time;
				}
			}
		}
	}

}
