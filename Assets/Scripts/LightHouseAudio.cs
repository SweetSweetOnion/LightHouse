using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource),typeof(LightHouse))]
public class LightHouseAudio : MonoBehaviour
{
	private AudioSource src;
	private LightHouse lh;

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
		src.PlayOneShot(AudioManager.instance.GetRandom(AudioManager.instance.boomAudio));
		src.PlayOneShot(AudioManager.instance.GetRandom(AudioManager.instance.waveAudio),0.5f);
		/*if (!src.isPlaying)
		{
			
			src.clip = AudioManager.instance.waveAudio[Random.Range(0, AudioManager.instance.waveAudio.Length)];
			src.Play();
		}*/
	}
}
