using System;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class SimpleScript : MonoBehaviour
{
	private TextMeshPro m_textMeshPro;

	private const string label = "The <#0050FF>count is: </color>{0:2}";

	private float m_frame;

	private void Start()
	{
		GameObject gameObject = base.gameObject;
		TextMeshPro textMeshPro = gameObject.AddComponent<TextMeshPro>();
		m_textMeshPro = textMeshPro;
		m_textMeshPro.autoSizeTextContainer = true;
		m_textMeshPro.fontSize = 48f;
		m_textMeshPro.alignment = TextAlignmentOptions.Center;
		m_textMeshPro.textWrappingMode = TextWrappingModes.NoWrap;
	}

	private void Update()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39DB0]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		float arg = MathF.FMod(m_frame, 1000f);
		m_textMeshPro.SetText("The <#0050FF>count is: </color>{0:2}", arg);
		float deltaTime = Time.deltaTime;
		float frame = deltaTime + m_frame;
		m_frame = frame;
	}
}
