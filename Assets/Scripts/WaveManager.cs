using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DefaultExecutionOrder(-1000)]
public class WaveManager : MonoBehaviour
{
	public static WaveManager instance;

	public float waveSpeed = 10;
	public float waveThickness = 1;
	public Material waveMaterial;

	private List<Wave> _allWaves = new List<Wave>();
	public IReadOnlyList<Wave> allWaves => _allWaves;

	private void Awake()
	{
		if (!instance)
		{
			instance = this;
		}else
		{
			Destroy(this);
		}
	}

	public void AddWave(Wave w)
	{
		_allWaves.Add(w);
	}

	public void RemoveWave(Wave w)
	{
		_allWaves.Remove(w);
	}




}
