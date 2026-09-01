using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TMPro;

public class TMP_TextEventHandler : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	[Serializable]
	public class CharacterSelectionEvent : UnityEvent<char, int>
	{
	}

	[Serializable]
	public class SpriteSelectionEvent : UnityEvent<char, int>
	{
	}

	[Serializable]
	public class WordSelectionEvent : UnityEvent<string, int, int>
	{
	}

	[Serializable]
	public class LineSelectionEvent : UnityEvent<string, int, int>
	{
	}

	[Serializable]
	public class LinkSelectionEvent : UnityEvent<string, string, int>
	{
	}

	private CharacterSelectionEvent m_OnCharacterSelection = new CharacterSelectionEvent();

	private SpriteSelectionEvent m_OnSpriteSelection = new SpriteSelectionEvent();

	private WordSelectionEvent m_OnWordSelection = new WordSelectionEvent();

	private LineSelectionEvent m_OnLineSelection = new LineSelectionEvent();

	private LinkSelectionEvent m_OnLinkSelection = new LinkSelectionEvent();

	private TMP_Text m_TextComponent;

	private Camera m_Camera;

	private Canvas m_Canvas;

	private int m_selectedLink = -1;

	private int m_lastCharIndex;

	private int m_lastWordIndex = -1;

	private int m_lastLineIndex;

	public CharacterSelectionEvent onCharacterSelection
	{
		get
		{
			return m_OnCharacterSelection;
		}
		set
		{
			m_OnCharacterSelection = value;
		}
	}

	public SpriteSelectionEvent onSpriteSelection
	{
		get
		{
			return m_OnSpriteSelection;
		}
		set
		{
			m_OnSpriteSelection = value;
		}
	}

	public WordSelectionEvent onWordSelection
	{
		get
		{
			return m_OnWordSelection;
		}
		set
		{
			m_OnWordSelection = value;
		}
	}

	public LineSelectionEvent onLineSelection
	{
		get
		{
			return m_OnLineSelection;
		}
		set
		{
			m_OnLineSelection = value;
		}
	}

	public LinkSelectionEvent onLinkSelection
	{
		get
		{
			return m_OnLinkSelection;
		}
		set
		{
			m_OnLinkSelection = value;
		}
	}

	private void Awake()
	{
		GameObject gameObject = base.gameObject;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		TMP_Text textComponent = default(TMP_Text);
		m_TextComponent = textComponent;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(TextMeshProUGUI));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABB0");
		object obj = default(object);
		Camera camera;
		if (obj == null)
		{
			camera = Camera.main;
		}
		else
		{
			GameObject gameObject2 = base.gameObject;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9300");
			Canvas canvas = default(Canvas);
			m_Canvas = canvas;
			if (!(m_Canvas != null))
			{
				return;
			}
			if (m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				m_Camera = null;
				return;
			}
			camera = m_Canvas.worldCamera;
		}
		m_Camera = camera;
	}

	private unsafe void LateUpdate()
	{
		//IL_0008: Expected O, but got Ref
		//IL_05cf: Expected O, but got Ref
		//IL_05fb: Expected O, but got Ref
		//IL_06ab: Expected O, but got Ref
		//IL_0700: Expected O, but got Ref
		//IL_0755: Expected O, but got Ref
		//IL_00cd: Expected O, but got I4
		//IL_0243: Expected O, but got I4
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Expected O, but got Unknown
		//IL_01a6: Expected O, but got I4
		//IL_01b6: Expected O, but got I
		//IL_0319: Expected O, but got I4
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Expected O, but got Unknown
		//IL_034a: Expected O, but got I
		//IL_037a: Expected O, but got I
		//IL_04d2: Expected O, but got I
		//IL_0271: Expected O, but got Ref
		//IL_027f: Expected O, but got Ref
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Expected O, but got Unknown
		//IL_03c8: Expected O, but got I
		//IL_03d1: Expected O, but got I4
		//IL_014e: Expected O, but got I4
		//IL_015e: Expected O, but got I
		//IL_04e5: Expected O, but got Ref
		//IL_04f3: Expected O, but got Ref
		//IL_05a8: Expected O, but got Ref
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Expected O, but got Unknown
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Expected O, but got Unknown
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Expected O, but got Unknown
		//IL_0484: Expected O, but got I
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		RectTransform rectTransform = m_TextComponent.rectTransform;
		_ = Input.mousePosition.x;
		float num = default(float);
		if (!TMP_TextUtilities.IsIntersectingRectTransform(rectTransform, (Vector3)(&num), m_Camera))
		{
			m_selectedLink = -1;
			m_lastWordIndex = -1;
			return;
		}
		_ = Input.mousePosition.x;
		int num2 = TMP_TextUtilities.FindIntersectingCharacter(m_TextComponent, (Vector3)(&num), m_Camera, visibleOnly: true);
		UnityEvent<char, int> unityEvent;
		if (num2 != -1 && num2 != m_lastCharIndex)
		{
			m_lastCharIndex = num2;
			TMP_TextInfo textInfo = m_TextComponent.textInfo;
			TMP_CharacterInfo[] characterInfo = textInfo.characterInfo;
			object obj3 = num2 * 376;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v790 @ rcx_v55+20+v138 @ rdx_v39 (TMPro.TMP_CharacterInfo[])]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v790 @ rcx_v55+20+v138 @ rdx_v39 (TMPro.TMP_CharacterInfo[])]");
				if ((nint)0 == 1)
				{
					TMP_TextInfo textInfo2 = m_TextComponent.textInfo;
					TMP_CharacterInfo[] characterInfo2 = textInfo2.characterInfo;
					object obj4 = num2 * 376;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1077 @ rcx_v63+24+v140 @ rdx_v45 (TMPro.TMP_CharacterInfo[])]");
					object obj5 = 0;
					if (m_OnSpriteSelection != null)
					{
						unityEvent = m_OnSpriteSelection;
						goto IL_063b;
					}
				}
			}
			else
			{
				TMP_TextInfo textInfo3 = m_TextComponent.textInfo;
				TMP_CharacterInfo[] characterInfo3 = textInfo3.characterInfo;
				object obj6 = num2 * 376;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1064 @ rcx_v58+24+v142 @ rdx_v43 (TMPro.TMP_CharacterInfo[])]");
				object obj5 = 0;
				if (m_OnCharacterSelection != null)
				{
					unityEvent = m_OnCharacterSelection;
					goto IL_063b;
				}
			}
		}
		goto IL_01ca;
		IL_01ca:
		_ = Input.mousePosition.x;
		int num3 = TMP_TextUtilities.FindIntersectingWord(m_TextComponent, (Vector3)(&num), m_Camera);
		if (num3 != -1 && num3 != m_lastWordIndex)
		{
			m_lastWordIndex = num3;
			TMP_TextInfo textInfo4 = m_TextComponent.textInfo;
			TMP_WordInfo[] wordInfo = textInfo4.wordInfo;
			object obj7 = num3 * 2;
			object obj8 = num3 + obj7;
			TMP_WordInfo tMP_WordInfo = default(TMP_WordInfo);
			string word = tMP_WordInfo.GetWord();
			if (m_OnWordSelection != null)
			{
				object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
				object obj10 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 64));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v35 (TMPro.TMP_WordInfo[])+30+v1047 @ rcx_v49*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180941A60");
			}
		}
		_ = Input.mousePosition.x;
		int num4 = TMP_TextUtilities.FindIntersectingLine(m_TextComponent, (Vector3)(&num), m_Camera);
		if (num4 != -1 && num4 != m_lastLineIndex)
		{
			m_lastLineIndex = num4;
			TMP_TextInfo textInfo5 = m_TextComponent.textInfo;
			TMP_LineInfo[] lineInfo = textInfo5.lineInfo;
			object obj11 = num4 * 2;
			object obj12 = num4 + obj11;
			object obj13 = obj12 << 5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1096 @ rcx_v34+20+v148 @ rdx_v24 (TMPro.TMP_LineInfo[])]");
			object obj14 = (nint)0 >> 32;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1096 @ rcx_v34+30+v148 @ rdx_v24 (TMPro.TMP_LineInfo[])]");
			_ = 0;
			char[] array = new char[obj14];
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1096 @ rcx_v34+20+v148 @ rdx_v24 (TMPro.TMP_LineInfo[])]");
			object obj15 = (nint)0 >> 32;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
			if ((nint)obj15 > 0)
			{
				object obj16 = array + 32;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1096 @ rcx_v34+20+v148 @ rdx_v24 (TMPro.TMP_LineInfo[])]");
				object obj17 = (nint)0 >> 32;
				object obj18 = 0;
				do
				{
					TMP_TextInfo textInfo6 = m_TextComponent.textInfo;
					TMP_CharacterInfo[] characterInfo4 = textInfo6.characterInfo;
					if ((nint)obj18 >= characterInfo4.Length)
					{
						break;
					}
					TMP_TextInfo textInfo7 = m_TextComponent.textInfo;
					TMP_CharacterInfo[] characterInfo5 = textInfo7.characterInfo;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-80]");
					object obj19 = 0 + obj18;
					obj18++;
					object obj20 = obj19 * 376;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1127 @ rax_v48+24+v152 @ rdx_v33 (TMPro.TMP_CharacterInfo[])]");
					obj16 = 0;
					obj16 += 2;
				}
				while (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj18) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj17));
			}
			string text = ((string)null).CreateString(array);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1096 @ rcx_v34+20+v148 @ rdx_v24 (TMPro.TMP_LineInfo[])]");
			object obj21 = (nint)0 >> 32;
			if (m_OnLineSelection != null)
			{
				object obj22 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
				object obj23 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 64));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-80]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180941A60");
			}
		}
		_ = Input.mousePosition.x;
		int num5 = TMP_TextUtilities.FindIntersectingLink(m_TextComponent, (Vector3)(&num), m_Camera);
		if (num5 != -1 && num5 != m_selectedLink)
		{
			m_selectedLink = num5;
			TMP_TextInfo textInfo8 = m_TextComponent.textInfo;
			TMP_LinkInfo tMP_LinkInfo = default(TMP_LinkInfo);
			string linkID = tMP_LinkInfo.GetLinkID();
			string linkText = tMP_LinkInfo.GetLinkText();
			if (m_OnLinkSelection != null)
			{
				object obj24 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180941A60");
			}
		}
		return;
		IL_063b:
		int arg = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 64));
		char arg2 = (char)(ushort)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
		unityEvent.Invoke(arg2, arg);
		goto IL_01ca;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}

	private unsafe void SendOnCharacterSelection(char character, int characterIndex)
	{
		if (m_OnCharacterSelection != null)
		{
			object obj = default(object);
			object obj2 = default(object);
			m_OnCharacterSelection.Invoke((char)(ushort)(&obj), (int)(&obj2));
		}
	}

	private unsafe void SendOnSpriteSelection(char character, int characterIndex)
	{
		if (m_OnSpriteSelection != null)
		{
			object obj = default(object);
			object obj2 = default(object);
			m_OnSpriteSelection.Invoke((char)(ushort)(&obj), (int)(&obj2));
		}
	}

	private void SendOnWordSelection(string word, int charIndex, int length)
	{
		if (m_OnWordSelection != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180941A60");
		}
	}

	private void SendOnLineSelection(string line, int charIndex, int length)
	{
		if (m_OnLineSelection != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180941A60");
		}
	}

	private void SendOnLinkSelection(string linkID, string linkText, int linkIndex)
	{
		if (m_OnLinkSelection != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180941A60");
		}
	}
}
