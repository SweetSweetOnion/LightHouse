using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource),typeof(LightHouse))]
public class LightHouseAudio : MonoBehaviour
{
	private AudioSource src;
	private LightHouse lh;
	[Range(0, 1)]
	public float waveVolume = 1;
	[Range(0, 1)]
	public float boomVolume = 1;

	private void Awake()
	{
		src = GetComponent<AudioSource>();
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
		src.PlayOneShot(AudioManager.instance.GetRandom(AudioManager.instance.boomAudio),boomVolume);
		src.PlayOneShot(AudioManager.instance.GetRandom(AudioManager.instance.waveAudio),waveVolume);
		
		/*if (!src.isPlaying)
		{
			
			src.clip = AudioManager.instance.waveAudio[Random.Range(0, AudioManager.instance.waveAudio.Length)];
			src.Play();
		}*/
	}
}
