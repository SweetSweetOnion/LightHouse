﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouse : MonoBehaviour
{
	private bool _isInsideWave = false;
	private Wave _lastWave = null;
	private WaveArc _lastWaveArc = null;

	//accessors
	public bool isInsideWave => _isInsideWave;
	public Wave lastWave => _lastWave;
	public WaveArc lastWaveArc => _lastWaveArc;


    public void CreateWave(float waveLevel)
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
		WaveArc arc = null;
		for (int i = 0; i < WaveManager.instance.allWaves.Count; i++)
		{
			Wave w = WaveManager.instance.allWaves[i];
			if (w.IsInsideWave(transform.position, out arc))
			{
				if (w.CanBounce(this))
				{
					_lastWave = w;
					_isInsideWave = true;
					_lastWaveArc = arc;
				}
			}
		}
	}
	
}
