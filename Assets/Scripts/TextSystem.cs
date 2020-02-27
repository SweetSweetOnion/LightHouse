using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextSystem : MonoBehaviour
{
	[TextArea(10,50)]
	public string fullText;
	[TextArea(10, 50)]
	public string fullText2;

	private List<string> lines = new List<string>();
	private List<string> lines2 = new List<string>();

	private TextMeshPro[] texts;

	public TextMeshProUGUI text;
	public float textDisplayDuration = 10;

    void Awake()
    {
		texts = GetComponentsInChildren<TextMeshPro>();
		
    }

	private void Start()
	{
		LoadText();
		foreach(TextMeshPro tmp in texts)
		{
			tmp.text = Pick(lines);
		}
	}

	private void OnEnable()
	{
		LightHouseWaveSignal.OnLightHouseActive += OnHouseLightActive;
	}

	private void OnDisable()
	{
		LightHouseWaveSignal.OnLightHouseActive -= OnHouseLightActive;
	}

	private void OnHouseLightActive()
	{
		StartCoroutine(DisplayText());
	}

	private void LoadText()
	{
		lines = new List<string>();
		lines.AddRange(fullText.Split('\n'));
		lines2 = new List<string>();
		lines2.AddRange(fullText2.Split('\n'));
	}

	private string Pick(List<string> l)
	{
		int i = Random.Range(0, l.Count);
		string str = l[i];
		l.RemoveAt(i);
		return str;
	}

	IEnumerator DisplayText()
	{
		text.text = Pick(lines2);
		yield return new WaitForSeconds(textDisplayDuration);
		text.text = "";
	}

}
