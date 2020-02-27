using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainTitleScreen : MonoBehaviour
{
	public float minTime = 2;
	private float time = 0;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		time += Time.deltaTime;
		if(time > minTime && Input.anyKeyDown)
		{
			SceneManager.LoadScene(1);
		}
	}
}
