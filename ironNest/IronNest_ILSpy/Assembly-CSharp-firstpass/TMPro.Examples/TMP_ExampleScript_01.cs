using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class TMP_ExampleScript_01 : MonoBehaviour
{
	public enum objectType
	{
		TextMeshPro,
		TextMeshProUGUI
	}

	public objectType ObjectType;

	public bool isStatic;

	private TMP_Text m_text;

	private const string k_label = "The count is <#0080ff>{0}</color>";

	private int count;

	private void Awake()
	{
		TextMeshPro textMeshPro = default(TextMeshPro);
		TextMeshPro text;
		GameObject gameObject;
		if (ObjectType != objectType.TextMeshPro)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			bool flag = (object)textMeshPro != null;
			text = textMeshPro;
			if (flag)
			{
				goto IL_0093;
			}
			gameObject = base.gameObject;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			bool flag2 = (object)textMeshPro != null;
			text = textMeshPro;
			if (flag2)
			{
				goto IL_0093;
			}
			gameObject = base.gameObject;
		}
		text = gameObject.AddComponent<TextMeshPro>();
		goto IL_0093;
		IL_0093:
		m_text = text;
		TMP_FontAsset font = Resources.Load<TMP_FontAsset>("Fonts & Materials/Anton SDF");
		m_text.font = font;
		Material fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/Anton SDF - Drop Shadow");
		m_text.fontSharedMaterial = fontSharedMaterial;
		m_text.fontSize = 120f;
		m_text.text = "A <#0080ff>simple</color> line of text.";
		Vector2 preferredValues = m_text.GetPreferredValues(1f / 0f, 1f / 0f);
		RectTransform rectTransform = m_text.rectTransform;
		Vector2 sizeDelta = default(Vector2);
		rectTransform.sizeDelta = sizeDelta;
	}

	private void Update()
	{
		//IL_004b: Expected O, but got I
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39DD0]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (!isStatic)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
			nint num = default(nint);
			object obj = num >> 6;
			object obj2 = obj >> 31;
			object obj3 = obj + obj2;
			object obj4 = obj3 * 1000;
			float arg = (float)count - (float)obj4;
			m_text.SetText("The count is <#0080ff>{0}</color>", arg);
			int num2 = count + 1;
			count = num2;
		}
	}
}
