using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextSystem : MonoBehaviour
{
	[TextArea(10,50)]
	public string fullText;

	private List<string> lines = new List<string>();

	private TextMeshPro[] texts;

    void Awake()
    {
		texts = GetComponentsInChildren<TextMeshPro>();
		
    }

	private void Start()
	{
		LoadText();
		foreach(TextMeshPro tmp in texts)
		{
			tmp.text = Pick();
		}
	}

	private void LoadText()
	{
		lines = new List<string>();
		lines.AddRange(fullText.Split('\n'));
	}

	private string Pick()
	{
		int i = Random.Range(0, lines.Count);
		string str = lines[i];
		lines.RemoveAt(i);
		return str;
	}


}
