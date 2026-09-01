using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
	public Transform target;

	private TextMeshPro text;

	private void Awake()
	{
		text = GetComponentInChildren<TextMeshPro>();
	}

	private void LateUpdate()
	{
		if ((bool)target)
		{
			base.transform.position = target.position + Vector3.up * 0.2f;
		}
	}

	public void SetText(string txt, Transform targ)
	{
		if (!text)
		{
			text = GetComponentInChildren<TextMeshPro>();
		}
		text.text = txt;
		target = targ;
	}
}
