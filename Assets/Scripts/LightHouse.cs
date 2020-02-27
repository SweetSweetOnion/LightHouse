using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouse : MonoBehaviour
{
	private bool _isInsideWave = false;
	private Wave _lastWaveReceive = null;
	private WaveArc _lastWaveArc = null;
	private float _lastWaveTime = 0;
	public float waveAmplitude = 10;

	//accessors
	public bool isInsideWave => _isInsideWave;
	public Wave lastWaveReceive => _lastWaveReceive;
	public WaveArc lastWaveArc => _lastWaveArc;
	public float lastWaveTime => _lastWaveTime;

	public delegate void BasicEvent();
	public event BasicEvent OnCreateWave;
	public event BasicEvent OnReceiveWave;

	public List<Wave> waves = new List<Wave>();

	public void CreateWave(float waveDuration, Vector3 direction, float rangeAngle)
	{
		Wave.SpawnWave(this, waveDuration, direction, rangeAngle, WaveManager.instance.GetWaveColor(waveDuration));
		OnCreateWave?.Invoke();
	}

	private void Update()
	{
		CheckInside();
		if(Time.time > lastWaveTime + 0.5f)
		{
			_isInsideWave = false;
		}
	}

	private void CheckInside()
	{
		//sInsideWave = false;
		WaveArc arc = null;
		for (int i = 0; i < WaveManager.instance.allWaves.Count; i++)
		{
			Wave w = WaveManager.instance.allWaves[i];
			if (w.IsInsideWave(transform.position, out arc))
			{
				if (w.CanBounce(this))
				{
					if(_lastWaveReceive != w)
					{
						OnReceiveWave?.Invoke();
					}
					_lastWaveReceive = w;
					_isInsideWave = true;
					_lastWaveArc = arc;
					_lastWaveTime = Time.time;
				}
			}
		}
	}

}
