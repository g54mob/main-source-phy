using System;
using UnityEngine;

public class TagCheckbox : ClickBehaviour
{
	public Transform tagIcon;

	public TagCheckbox[] other;

	private bool enabledState;

	private int checkboxNumber;

	private TagCheckboxManager tagManager;

	private Renderer r;

	private Renderer iconRenderer;

	private TextMesh textMesh;

	public void Initialize(TagCheckboxManager m)
	{
		tagManager = m;
		checkboxNumber = Convert.ToInt32(base.transform.name.Substring(10, 2));
		iconRenderer = tagIcon.GetComponent<Renderer>();
		r = base.transform.GetComponent<Renderer>();
		textMesh = GetComponentInChildren<TextMesh>();
		Toggle(false);
	}

	public void Initialize(TagCheckboxManager manager, string tagText)
	{
		Initialize(manager);
		if (textMesh != null)
		{
			textMesh.text = tagText;
		}
	}

	public override void OnClicked()
	{
		if (other.Length > 0)
		{
			Toggle(true);
		}
		else
		{
			Toggle(!enabledState);
		}
	}

	public void Toggle(bool toggle)
	{
		if (toggle)
		{
			for (int i = 0; i < other.Length; i++)
			{
				TagCheckbox tagCheckbox = other[i];
				if (tagCheckbox != null)
				{
					tagCheckbox.Toggle(false);
				}
			}
		}
		enabledState = toggle;
		tagManager.SetTagState(checkboxNumber, enabledState);
		r.enabled = enabledState;
		iconRenderer.enabled = enabledState;
	}
}
