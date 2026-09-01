using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class AlignTextMesh : AlignGUIObject
{
	protected TextMesh textMesh;

	public int frameOffset;

	public bool automatic = true;

	private bool inited;

	protected override void Start()
	{
		inited = true;
		textMesh = GetComponent<TextMesh>();
		if (automatic)
		{
			if (frameOffset == 0)
			{
				base.Start();
			}
			else
			{
				StartCoroutine(IEStart());
			}
		}
	}

	protected IEnumerator IEStart()
	{
		for (int i = 0; i < frameOffset; i++)
		{
			yield return null;
		}
		base.Start();
	}

	public void Align()
	{
		if (!inited)
		{
			textMesh = GetComponent<TextMesh>();
			inited = true;
		}
		base.Start();
	}

	private void SetTextAnchor()
	{
		if (!(textMesh == null))
		{
			textMesh.anchor = ((alignment != ObjectAlignment.Left) ? TextAnchor.MiddleLeft : TextAnchor.MiddleRight);
		}
	}

	protected override void UpdateAlignment()
	{
		base.UpdateAlignment();
		SetTextAnchor();
	}
}
