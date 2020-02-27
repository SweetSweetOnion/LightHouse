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

	private bool isDisplay = false;
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
		FindObjectOfType<Player>().OnWaveBounce  += OnDisplayText;
	}

	private void OnDisable()
	{
		FindObjectOfType<Player>().OnWaveBounce -= OnDisplayText;
	}

	private void OnDisplayText()
	{
		if(!isDisplay)
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
		isDisplay = true;
		text.text = Pick(lines2);
		float t = 0;
		Color c = text.color;
		while (t < 1)
		{
			t += Time.deltaTime;
			text.color = Color.Lerp(new Color(c.r, c.g, c.b, 0), new Color(c.r, c.g, c.b, 1), t);
			yield return null;
		}
		yield return new WaitForSeconds(textDisplayDuration);
		t = 0;
		while (t < 1)
		{
			t += Time.deltaTime;
			text.color = Color.Lerp(new Color(c.r, c.g, c.b, 1), new Color(c.r, c.g, c.b, 0), t);
			yield return null;
		}
		text.text = "";
		isDisplay = false;
	}

}
