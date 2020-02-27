using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-50)]
public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	public AudioClip[] boomAudio;
	public AudioClip[] waveAudio;
	public float startDelay = 0;
	public float transitionDuration = 3;
	private AudioSource[] sources;
	private int current = 0;
	private bool fading = false;

	public int lightHouseCap1 = 5;
	public int lightHouseCap2 = 8;

	void Awake()
	{
		if (!instance)
		{
			instance = this;
		}else
		{
			Destroy(this);
		}

		sources = GetComponentsInChildren<AudioSource>();
	}

	private void Start()
	{
		StartCoroutine(DelayStart());
	}

	private void Update()
	{
		if(LightHouseWaveSignal.activeCount > lightHouseCap2)
		{
			CrossFade(2);
		}
		else if(LightHouseWaveSignal.activeCount > lightHouseCap1)
		{
			CrossFade(1);
		}
	}

	public void CrossFade(int next)
	{
		if (!fading && next != current && next < sources.Length)
		{
			StartCoroutine(CrossFade(current, next, transitionDuration));
		}
	}

	private IEnumerator DelayStart()
	{
		yield return new WaitForSeconds(startDelay);
		Debug.Log("hey");
		sources[0].Play();
		current = 0;
	}

	private IEnumerator CrossFade(int from, int to, float duration)
	{
		fading = true;
		sources[to].volume = 0;
		sources[to].Play();
		sources[to].time = sources[from].time;

		float t = 0;
		while(t< duration)
		{
			t += Time.deltaTime;
			sources[to].volume = Mathf.Lerp(0, 1, t / duration);
			sources[from].volume = Mathf.Lerp(1, 0, t / duration);
			yield return null;
		}
		sources[from].Stop();
		current = to;
		fading = false;
	}

	public AudioClip GetRandom(AudioClip[] clips)
	{
		return clips[Random.Range(0, clips.Length)];
	}
}
