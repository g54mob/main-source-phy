using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TMPro.Examples;

public class TMP_TextSelector_A : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	private TextMeshPro m_TextMeshPro;

	private Camera m_Camera;

	private bool m_isHoveringObject;

	private int m_selectedLink;

	private int m_lastCharIndex;

	private int m_lastWordIndex;

	private void Awake()
	{
		//IL_004d: Expected I, but got O
		//IL_005d: Expected O, but got I
		//IL_006d: Expected O, but got I
		TextMeshPro textMeshPro = default(TextMeshPro);
		while (true)
		{
			GameObject gameObject = base.gameObject;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			m_TextMeshPro = textMeshPro;
			Camera main = Camera.main;
			m_Camera = main;
			TextMeshPro textMeshPro2 = m_TextMeshPro;
			nint num = (nint)textMeshPro2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ r9_v1 (Il2CppClass<TMPro.TextMeshPro>)+7D8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ r9_v1 (Il2CppClass<TMPro.TextMeshPro>)+7E0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v87 @ rax_v9 (should have been resolved before IL gen)");
		}
	}

	private unsafe void LateUpdate()
	{
		//IL_0008: Expected O, but got Ref
		//IL_005a: Expected O, but got Ref
		//IL_00d6: Expected O, but got Ref
		//IL_03e6: Expected O, but got Ref
		//IL_046d: Expected I4, but got I8
		//IL_0600: Expected O, but got Ref
		//IL_01b3: Expected O, but got I4
		//IL_04d3: Expected O, but got I4
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Expected O, but got Unknown
		//IL_01ec: Expected O, but got I4
		//IL_0695: Expected O, but got I4
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Expected O, but got Unknown
		//IL_0579: Expected O, but got Ref
		//IL_0280: Expected O, but got I
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Expected O, but got Unknown
		//IL_02bd: Expected O, but got I
		//IL_02f2: Expected O, but got I
		//IL_031a: Expected O, but got I
		//IL_0713: Expected O, but got I
		//IL_0726: Expected O, but got Ref
		//IL_0777: Expected O, but got Ref
		//IL_036d: Expected O, but got I
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Expected O, but got Unknown
		//IL_03b1: Expected O, but got I
		//IL_03b1: Expected O, but got I
		//IL_0952: Expected O, but got I
		//IL_0849: Expected O, but got I
		//IL_0859: Expected O, but got I
		//IL_088d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0892: Expected O, but got Unknown
		//IL_08a2: Expected O, but got I
		//IL_08e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08eb: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		m_isHoveringObject = false;
		_ = 0;
		_ = 0;
		RectTransform rectTransform = m_TextMeshPro.rectTransform;
		Vector3 mousePosition = Input.mousePosition;
		Camera main = Camera.main;
		_ = mousePosition.x;
		_ = mousePosition.z;
		Vector3 position = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
		if (TMP_TextUtilities.IsIntersectingRectTransform(rectTransform, position, main))
		{
			m_isHoveringObject = true;
		}
		if (!m_isHoveringObject)
		{
			return;
		}
		Vector3 mousePosition2 = Input.mousePosition;
		Camera main2 = Camera.main;
		_ = mousePosition2.x;
		_ = mousePosition2.z;
		Vector3 position2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
		int num = TMP_TextUtilities.FindIntersectingCharacter(m_TextMeshPro, position2, main2, visibleOnly: true);
		if (num != -1 && num != m_lastCharIndex && (Input.GetKeyInt(KeyCode.LeftShift) || Input.GetKeyInt(KeyCode.RightShift)))
		{
			m_lastCharIndex = num;
			TMP_TextInfo textInfo = m_TextMeshPro.textInfo;
			TMP_CharacterInfo[] characterInfo = textInfo.characterInfo;
			object obj3 = num * 376;
			TMP_TextInfo textInfo2 = m_TextMeshPro.textInfo;
			TMP_CharacterInfo[] characterInfo2 = textInfo2.characterInfo;
			object obj4 = num * 376;
			int num2 = Random.Range(0, 255);
			int num3 = Random.Range(0, 255);
			int num4 = Random.Range(0, 255);
			_ = 255;
			TMP_TextInfo textInfo3 = m_TextMeshPro.textInfo;
			TMP_MeshInfo[] meshInfo = textInfo3.meshInfo;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1226 @ rcx_v54+50+v114 @ r14_v14 (TMPro.TMP_CharacterInfo[])]");
			object obj5 = (nint)0 * (nint)4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1226 @ rcx_v54+50+v114 @ r14_v14 (TMPro.TMP_CharacterInfo[])]");
			object obj6 = 0 + obj5;
			object obj7 = obj6 + obj6;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1242 @ rcx_v56+64+v123 @ rdi_v14 (TMPro.TMP_CharacterInfo[])]");
			object obj8 = (nint)0 + (nint)1;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1242 @ rcx_v56+64+v123 @ rdi_v14 (TMPro.TMP_CharacterInfo[])]");
			object obj9 = (nint)0 + (nint)2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1242 @ rcx_v56+64+v123 @ rdi_v14 (TMPro.TMP_CharacterInfo[])]");
			object obj10 = (nint)0 + (nint)3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
			_ = 0;
			TMP_TextInfo textInfo4 = m_TextMeshPro.textInfo;
			TMP_MeshInfo[] meshInfo2 = textInfo4.meshInfo;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1226 @ rcx_v54+50+v114 @ r14_v14 (TMPro.TMP_CharacterInfo[])]");
			object obj11 = (nint)0 * (nint)4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1226 @ rcx_v54+50+v114 @ r14_v14 (TMPro.TMP_CharacterInfo[])]");
			object obj12 = 0 + obj11;
			object obj13 = obj12 + obj12;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ rdx_v51 (TMPro.TMP_MeshInfo[])+20+v1299 @ rcx_v68*8]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ rbx_v20 (TMPro.TMP_MeshInfo[])+58+v196 @ rcx_v62*8]");
			((Mesh)num5).colors32 = (Color32[])0;
		}
		Vector3 mousePosition3 = Input.mousePosition;
		_ = mousePosition3.x;
		_ = mousePosition3.z;
		Vector3 position3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
		int num6 = TMP_TextUtilities.FindIntersectingLink(m_TextMeshPro, position3, m_Camera);
		if ((num6 == -1 && m_selectedLink != num6) || num6 != m_selectedLink)
		{
			m_selectedLink = -1;
		}
		if (num6 != -1 && num6 != m_selectedLink)
		{
			m_selectedLink = num6;
			TMP_TextInfo textInfo5 = m_TextMeshPro.textInfo;
			TMP_LinkInfo[] linkInfo = textInfo5.linkInfo;
			object obj14 = num6 * 4;
			object obj15 = num6 + obj14;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rdx_v35 (TMPro.TMP_LinkInfo[])+20+v1240 @ rcx_v42*8]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rdx_v35 (TMPro.TMP_LinkInfo[])+30+v1240 @ rcx_v42*8]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rdx_v35 (TMPro.TMP_LinkInfo[])+40+v1240 @ rcx_v42*8]");
			_ = 0;
			RectTransform rectTransform2 = m_TextMeshPro.rectTransform;
			Vector3 mousePosition4 = Input.mousePosition;
			_ = mousePosition4.z;
			_ = mousePosition4.x;
			Vector2 screenPoint = default(Vector2);
			bool flag = RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform2, screenPoint, m_Camera, out System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73)));
			TMP_LinkInfo tMP_LinkInfo = (TMP_LinkInfo)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 17));
			string linkID = ((TMP_LinkInfo*)tMP_LinkInfo)->GetLinkID();
			if (linkID != "id_01")
			{
				bool flag2 = linkID == "id_02";
			}
		}
		Vector3 mousePosition5 = Input.mousePosition;
		Camera main3 = Camera.main;
		_ = mousePosition5.x;
		_ = mousePosition5.z;
		Vector3 position4 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
		int num7 = TMP_TextUtilities.FindIntersectingWord(m_TextMeshPro, position4, main3);
		if (num7 == -1 || num7 == m_lastWordIndex)
		{
			return;
		}
		m_lastWordIndex = num7;
		TMP_TextInfo textInfo6 = m_TextMeshPro.textInfo;
		TMP_WordInfo[] wordInfo = textInfo6.wordInfo;
		object obj16 = num7 * 2;
		object obj17 = num7 + obj16;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ rdx_v16 (TMPro.TMP_WordInfo[])+30+v1292 @ rcx_v24*8]");
		_ = 0;
		Transform transform = m_TextMeshPro.transform;
		TMP_TextInfo textInfo7 = m_TextMeshPro.textInfo;
		TMP_CharacterInfo[] characterInfo3 = textInfo7.characterInfo;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm6,8\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ rdx_v16 (TMPro.TMP_WordInfo[])+20+v1292 @ rcx_v24*8]");
		object obj18 = (nint)0 * (nint)376;
		Vector3 position5 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ rdx_v19+114+v184 @ rax_v29 (TMPro.TMP_CharacterInfo[])]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ rdx_v19+11C+v184 @ rax_v29 (TMPro.TMP_CharacterInfo[])]");
		_ = 0;
		Vector3 vector = transform.TransformPoint(position5);
		Camera main4 = Camera.main;
		_ = vector.x;
		Vector3 position6 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
		_ = vector.z;
		Vector3 vector2 = main4.WorldToScreenPoint(position6);
		TMP_TextInfo textInfo8 = m_TextMeshPro.textInfo;
		TMP_MeshInfo[] meshInfo3 = textInfo8.meshInfo;
		int num8 = Random.Range(0, 255);
		int num9 = Random.Range(0, 255);
		int num10 = Random.Range(0, 255);
		_ = 255;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-19]");
		if ((nint)0 > (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ rdx_v16 (TMPro.TMP_WordInfo[])+20+v1292 @ rcx_v24*8]");
			object obj19 = -0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ rdx_v16 (TMPro.TMP_WordInfo[])+20+v1292 @ rcx_v24*8]");
			TMP_WordInfo tMP_WordInfo = (TMP_WordInfo)0;
			object obj22;
			do
			{
				TMP_TextInfo textInfo9 = m_TextMeshPro.textInfo;
				TMP_CharacterInfo[] characterInfo4 = textInfo9.characterInfo;
				object obj20 = tMP_WordInfo * 376;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v633 @ rcx_v40+64+v167 @ rdx_v32 (TMPro.TMP_CharacterInfo[])]");
				object obj21 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
				_ = 0;
				tMP_WordInfo = (TMP_WordInfo)(tMP_WordInfo + 1);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
				_ = 0;
				obj22 = obj19 + (object)tMP_WordInfo;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-19]");
			}
			while ((nint)obj22 < 0);
		}
		Mesh mesh = m_TextMeshPro.mesh;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rdi_v11 (TMPro.TMP_MeshInfo[])+58]");
		mesh.colors32 = (Color32[])0;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("OnPointerEnter()");
		m_isHoveringObject = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("OnPointerExit()");
		m_isHoveringObject = false;
	}

	public TMP_TextSelector_A()
	{
		//IL_001a: Expected I4, but got I8
		m_selectedLink = -1;
		m_lastWordIndex = -1;
		base._002Ector();
	}
}
