using System;
using UnityEngine;

public class UploadingAnimationTimer : MonoBehaviour
{
	public float interval = 0.3f;

	public int currentText;

	public string[] dotText;

	public TextMesh MainText;

	public TextMesh AnimationText;

	[NonSerialized]
	private float BaseSize;

	private float lastUpdate;

	private void Start()
	{
		BaseSize = MainText.GetComponent<Renderer>().bounds.size.x;
		dotText = new string[4];
		dotText[0] = string.Empty;
		dotText[1] = ".";
		dotText[2] = "..";
		dotText[3] = "...";
	}

	private void Update()
	{
		if (Time.time > lastUpdate + interval)
		{
			if (MainText.GetComponent<Renderer>().bounds.size.x != BaseSize)
			{
				AnimationText.transform.position = new Vector3(MainText.GetComponent<Renderer>().bounds.max.x, AnimationText.transform.position.y, AnimationText.transform.position.z);
				BaseSize = MainText.GetComponent<Renderer>().bounds.size.x;
			}
			lastUpdate = Time.time;
			AnimationText.text = dotText[currentText++ % 4];
		}
	}
}
