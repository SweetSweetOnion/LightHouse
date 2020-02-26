using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouse : MonoBehaviour
{
	private bool _isInsideWave = false;
	private Wave _lastWave = null;

	//accessors
	public bool isInsideWave => _isInsideWave;
	public Wave lastWave => _lastWave;


    public void CreateWave(int waveLevel)
	{
		Wave.SpawnWave(this, waveLevel);
	}

	private void Update()
	{
		CheckInside();
	}

	private void CheckInside()
	{
		_isInsideWave = false;
		for (int i = 0; i < WaveManager.instance.allWaves.Count; i++)
		{
			Wave w = WaveManager.instance.allWaves[i];
			if (w.IsInsideWave(transform.position))
			{
				if (w.CanBounce(this))
				{
					_lastWave = w;
					_isInsideWave = true;
				}
			}
		}
	}
	
}
