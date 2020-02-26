using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouse : MonoBehaviour
{

    public void CreateWave(int waveLevel)
	{
		Wave.SpawnWave(this, waveLevel);
	}
}
