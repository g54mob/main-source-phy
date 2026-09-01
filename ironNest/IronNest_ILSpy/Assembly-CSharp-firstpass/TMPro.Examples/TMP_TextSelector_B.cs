using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TMPro.Examples;

public class TMP_TextSelector_B : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
	public RectTransform TextPopup_Prefab_01;

	private RectTransform m_TextPopup_RectTransform;

	private TextMeshProUGUI m_TextPopup_TMPComponent;

	private const string k_LinkText = "You have selected link <#ffff00>";

	private const string k_WordText = "Word Index: <#ffff00>";

	private TextMeshProUGUI m_TextMeshPro;

	private Canvas m_Canvas;

	private Camera m_Camera;

	private bool isHoveringObject;

	private int m_selectedWord;

	private int m_selectedLink;

	private int m_lastIndex;

	private Matrix4x4 m_matrix;

	private TMP_MeshInfo[] m_cachedMeshInfoVertexData;

	private void Awake()
	{
		GameObject gameObject = base.gameObject;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		TextMeshProUGUI textMeshProUGUI = default(TextMeshProUGUI);
		m_TextMeshPro = textMeshProUGUI;
		GameObject gameObject2 = base.gameObject;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9300");
		Canvas canvas = default(Canvas);
		m_Canvas = canvas;
		Camera camera = ((m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : m_Canvas.worldCamera);
		m_Camera = camera;
		RectTransform textPopup_RectTransform = UnityEngine.Object.Instantiate(TextPopup_Prefab_01);
		m_TextPopup_RectTransform = textPopup_RectTransform;
		Transform parent = m_Canvas.transform;
		m_TextPopup_RectTransform.SetParent(parent, worldPositionStays: false);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
		m_TextPopup_TMPComponent = textMeshProUGUI;
		GameObject gameObject3 = m_TextPopup_RectTransform.gameObject;
		gameObject3.SetActive(value: false);
	}

	private void OnEnable()
	{
		Action<UnityEngine.Object> rhs = ON_TEXT_CHANGED;
		TMPro_EventManager.TEXT_CHANGED_EVENT.Add(rhs);
	}

	private void OnDisable()
	{
		Action<UnityEngine.Object> rhs = ON_TEXT_CHANGED;
		TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(rhs);
	}

	private void ON_TEXT_CHANGED(UnityEngine.Object obj)
	{
		if (obj == m_TextMeshPro)
		{
			TMP_TextInfo textInfo = m_TextMeshPro.textInfo;
			TMP_MeshInfo[] cachedMeshInfoVertexData = textInfo.CopyMeshInfoVertexData();
			m_cachedMeshInfoVertexData = cachedMeshInfoVertexData;
		}
	}

	private unsafe void LateUpdate()
	{
		//IL_0008: Expected O, but got Ref
		//IL_1517: Expected O, but got I4
		//IL_0071: Expected O, but got Ref
		//IL_00cb: Expected I4, but got I8
		//IL_0047: Expected I4, but got I8
		//IL_0c50: Expected O, but got Ref
		//IL_0c7c: Expected O, but got F4
		//IL_0ca3: Expected O, but got F4
		//IL_1203: Expected O, but got Ref
		//IL_018c: Expected O, but got I4
		//IL_0ce6: Expected O, but got F4
		//IL_1297: Expected I4, but got I8
		//IL_0d2a: Expected O, but got I4
		//IL_0d34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d39: Expected O, but got Unknown
		//IL_0d49: Expected O, but got I
		//IL_0f43: Expected I4, but got I8
		//IL_01c5: Expected O, but got I4
		//IL_0d99: Expected O, but got I
		//IL_0da9: Expected O, but got I
		//IL_0206: Expected O, but got I
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Expected O, but got Unknown
		//IL_0238: Expected O, but got I
		//IL_12fd: Expected O, but got I4
		//IL_1305: Unknown result type (might be due to invalid IL or missing references)
		//IL_130a: Expected O, but got Unknown
		//IL_0fe1: Expected O, but got I4
		//IL_0fe9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fee: Expected O, but got Unknown
		//IL_0ffe: Expected O, but got I
		//IL_0ddd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de2: Expected O, but got Unknown
		//IL_0253: Expected O, but got I
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Expected O, but got Unknown
		//IL_0283: Expected O, but got I
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Expected O, but got Unknown
		//IL_02b5: Expected O, but got I
		//IL_104e: Expected O, but got I
		//IL_105e: Expected O, but got I
		//IL_02e0: Expected O, but got I
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Expected O, but got Unknown
		//IL_030b: Expected O, but got I
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Expected O, but got Unknown
		//IL_1382: Expected O, but got Ref
		//IL_0e16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1b: Expected O, but got Unknown
		//IL_0e2b: Expected O, but got I
		//IL_034d: Expected O, but got I
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Expected O, but got Unknown
		//IL_0378: Expected O, but got I
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Expected O, but got Unknown
		//IL_1092: Unknown result type (might be due to invalid IL or missing references)
		//IL_1097: Expected O, but got Unknown
		//IL_03c5: Expected O, but got I
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Expected O, but got Unknown
		//IL_03f0: Expected O, but got I
		//IL_13ce: Expected O, but got Ref
		//IL_0e6c: Expected O, but got I
		//IL_0e7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e81: Expected O, but got Unknown
		//IL_0e9e: Expected O, but got I
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Expected O, but got Unknown
		//IL_147c: Expected O, but got Ref
		//IL_0eb9: Expected O, but got I
		//IL_043d: Expected O, but got I
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Expected O, but got Unknown
		//IL_0469: Expected O, but got I
		//IL_10cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d0: Expected O, but got Unknown
		//IL_10e0: Expected O, but got I
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Expected O, but got Unknown
		//IL_140c: Expected I, but got O
		//IL_141c: Expected O, but got I
		//IL_142c: Expected O, but got I
		//IL_14ba: Expected I, but got O
		//IL_14ca: Expected O, but got I
		//IL_14da: Expected O, but got I
		//IL_1121: Expected O, but got I
		//IL_1131: Unknown result type (might be due to invalid IL or missing references)
		//IL_1136: Expected O, but got Unknown
		//IL_1153: Expected O, but got I
		//IL_0ee9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eee: Expected O, but got Unknown
		//IL_157e: Expected O, but got F4
		//IL_116e: Expected O, but got I
		//IL_04b6: Expected O, but got I
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Expected O, but got Unknown
		//IL_04e1: Expected O, but got I
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Expected O, but got Unknown
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Expected O, but got Unknown
		//IL_0528: Expected O, but got I
		//IL_0552: Expected O, but got I
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Expected O, but got Unknown
		//IL_0599: Expected O, but got I
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Expected O, but got Unknown
		//IL_05c4: Expected O, but got I
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Expected O, but got Unknown
		//IL_05f6: Expected O, but got I
		//IL_0620: Expected O, but got I
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Expected O, but got Unknown
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Expected O, but got Unknown
		//IL_0687: Expected O, but got I
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Expected O, but got Unknown
		//IL_06b2: Expected O, but got I
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Expected O, but got Unknown
		//IL_06e4: Expected O, but got I
		//IL_070e: Expected O, but got I
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Expected O, but got Unknown
		//IL_119e: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a3: Expected O, but got Unknown
		//IL_073e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0743: Expected O, but got Unknown
		//IL_0775: Expected O, but got I
		//IL_0785: Unknown result type (might be due to invalid IL or missing references)
		//IL_078a: Expected O, but got Unknown
		//IL_07a0: Expected O, but got I
		//IL_07b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b5: Expected O, but got Unknown
		//IL_07d2: Expected O, but got I
		//IL_07fc: Expected O, but got I
		//IL_0819: Unknown result type (might be due to invalid IL or missing references)
		//IL_081e: Expected O, but got Unknown
		//IL_082c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0831: Expected O, but got Unknown
		//IL_0863: Expected O, but got I
		//IL_0873: Unknown result type (might be due to invalid IL or missing references)
		//IL_0878: Expected O, but got Unknown
		//IL_088e: Expected O, but got I
		//IL_089e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a3: Expected O, but got Unknown
		//IL_08d0: Expected O, but got I
		//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e5: Expected O, but got Unknown
		//IL_08fb: Expected O, but got I
		//IL_0909: Unknown result type (might be due to invalid IL or missing references)
		//IL_090e: Expected O, but got Unknown
		//IL_0948: Expected O, but got I
		//IL_0958: Unknown result type (might be due to invalid IL or missing references)
		//IL_095d: Expected O, but got Unknown
		//IL_0973: Expected O, but got I
		//IL_0981: Unknown result type (might be due to invalid IL or missing references)
		//IL_0986: Expected O, but got Unknown
		//IL_09c0: Expected O, but got I
		//IL_09d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d5: Expected O, but got Unknown
		//IL_09eb: Expected O, but got I
		//IL_0a10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a15: Expected O, but got Unknown
		//IL_0a7f: Expected O, but got I
		//IL_0a8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a94: Expected O, but got Unknown
		//IL_0ab1: Expected O, but got I
		//IL_0acc: Expected O, but got I
		//IL_0b01: Expected O, but got I
		//IL_0b29: Expected O, but got I
		//IL_0b7c: Expected O, but got I
		//IL_0b8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b91: Expected O, but got Unknown
		//IL_0be2: Expected O, but got I
		//IL_0bf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfa: Expected I4, but got Unknown
		//IL_0c08: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		obj = 0;
		_ = 0;
		_ = 0;
		if (!isHoveringObject)
		{
			if (m_lastIndex != -1)
			{
				RestoreCachedVertexAttributes(m_lastIndex);
				m_lastIndex = -1;
			}
			return;
		}
		Vector3 mousePosition = Input.mousePosition;
		Vector3 s = default(Vector3);
		int num = TMP_TextUtilities.FindIntersectingCharacter(m_TextMeshPro, (Vector3)(&s), m_Camera, visibleOnly: true);
		Vector2 vector = default(Vector2);
		float num12 = default(float);
		if (num == -1 || num != m_lastIndex)
		{
			RestoreCachedVertexAttributes(m_lastIndex);
			m_lastIndex = -1;
			if (num != -1 && num != m_lastIndex && (Input.GetKeyInt(KeyCode.LeftShift) || Input.GetKeyInt(KeyCode.RightShift)))
			{
				m_lastIndex = num;
				TMP_TextInfo textInfo = m_TextMeshPro.textInfo;
				TMP_CharacterInfo[] characterInfo = textInfo.characterInfo;
				object obj3 = num * 376;
				TMP_TextInfo textInfo2 = m_TextMeshPro.textInfo;
				TMP_CharacterInfo[] characterInfo2 = textInfo2.characterInfo;
				object obj4 = num * 376;
				TMP_TextInfo textInfo3 = m_TextMeshPro.textInfo;
				TMP_MeshInfo[] meshInfo = textInfo3.meshInfo;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2379 @ rcx_v80+50+v746 @ rdx_v59 (TMPro.TMP_CharacterInfo[])]");
				object obj5 = (nint)0 * (nint)4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2379 @ rcx_v80+50+v746 @ rdx_v59 (TMPro.TMP_CharacterInfo[])]");
				object obj6 = 0 + obj5;
				object obj7 = obj6 + obj6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v716 @ rdi_v25 (TMPro.TMP_MeshInfo[])+30+v826 @ rcx_v85*8]");
				TMP_MeshInfo tMP_MeshInfo = (TMP_MeshInfo)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj8 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj9 = 0 + obj8;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj10 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj11 = 0 + obj10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+38+v2640 @ rax_v84*4]");
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+20+v1777 @ rcx_v86*4]");
				object obj12 = num2 + 0;
				float num3 = (float)obj12 * 0.5f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj13 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj14 = 0 + obj13;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj15 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj16 = 0 + obj15;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+28+v2666 @ rcx_v87*4]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj17 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj18 = 0 + obj17;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj19 = (nint)0 + (nint)1;
				object obj20 = obj19 * 2;
				object obj21 = obj19 + obj20;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+34+v2685 @ rax_v88*4]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj22 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj23 = 0 + obj22;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj24 = (nint)0 + (nint)2;
				object obj25 = obj24 * 2;
				object obj26 = obj24 + obj25;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+40+v2705 @ rax_v91*4]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj27 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj28 = 0 + obj27;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj29 = (nint)0 + (nint)3;
				object obj30 = obj29 * 2;
				object obj31 = obj29 + obj30;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+4C+v2714 @ rax_v94*4]");
				_ = 0;
				Vector3 pos = default(Vector3);
				Quaternion q = default(Quaternion);
				Matrix4x4 matrix4x = Matrix4x4.Internal_TRS(ref pos, ref q, ref s);
				m_matrix = (Matrix4x4)matrix4x.m00;
				_ = matrix4x.m01;
				_ = matrix4x.m02;
				_ = matrix4x.m03;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj32 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj33 = 0 + obj32;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj34 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj35 = 0 + obj34;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+78]");
				object obj36 = vector * 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+20+v2808 @ rcx_v98*4]");
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+68]");
				object obj37 = num4 * 0;
				object obj38 = obj36 + obj37;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+28+v2808 @ rcx_v98*4]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+88]");
				object obj39 = num5 * 0;
				object obj40 = obj38 + obj39;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+98]");
				object obj41 = obj40 + 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj42 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj43 = 0 + obj42;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj44 = (nint)0 + (nint)1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+78]");
				object obj45 = vector * 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+2C+v2845 @ rax_v104*4]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+68]");
				object obj46 = num6 * 0;
				object obj47 = obj45 + obj46;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+34+v2845 @ rax_v104*4]");
				nint num7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+88]");
				object obj48 = num7 * 0;
				object obj49 = obj47 + obj48;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+98]");
				object obj50 = obj49 + 0;
				object obj51 = obj44 * 2;
				object obj52 = obj44 + obj51;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj53 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj54 = 0 + obj53;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj55 = (nint)0 + (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+78]");
				object obj56 = vector * 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+38+v2873 @ rax_v107*4]");
				nint num8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+68]");
				object obj57 = num8 * 0;
				object obj58 = obj56 + obj57;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+40+v2873 @ rax_v107*4]");
				nint num9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+88]");
				object obj59 = num9 * 0;
				object obj60 = obj58 + obj59;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+98]");
				object obj61 = obj60 + 0;
				object obj62 = obj55 * 2;
				object obj63 = obj55 + obj62;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj64 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj65 = 0 + obj64;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj66 = (nint)0 + (nint)3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+78]");
				object obj67 = vector * 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+44+v2899 @ rax_v110*4]");
				nint num10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+68]");
				object obj68 = num10 * 0;
				object obj69 = obj67 + obj68;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+4C+v2899 @ rax_v110*4]");
				nint num11 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+88]");
				object obj70 = num11 * 0;
				object obj71 = obj69 + obj70;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.TMP_TextSelector_B)+98]");
				object obj72 = obj71 + 0;
				object obj73 = obj66 * 2;
				object obj74 = obj66 + obj73;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj75 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj76 = 0 + obj75;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj77 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj78 = 0 + obj77;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+28+v2926 @ rcx_v103*4]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj79 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj80 = 0 + obj79;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj81 = (nint)0 + (nint)1;
				object obj82 = obj81 * 2;
				object obj83 = obj81 + obj82;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+34+v2939 @ rax_v113*4]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj84 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj85 = 0 + obj84;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj86 = (nint)0 + (nint)2;
				object obj87 = obj86 * 2;
				object obj88 = obj86 + obj87;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+40+v2947 @ rax_v116*4]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj89 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj90 = 0 + obj89;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj91 = (nint)0 + (nint)3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+44+v2955 @ rax_v119*4]");
				num12 = 0f + num3;
				object obj92 = obj91 * 2;
				object obj93 = obj91 + obj92;
				_ = 4290838527L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v717 @ rdi_v26 (TMPro.TMP_MeshInfo)+4C+v2955 @ rax_v119*4]");
				_ = 0;
				TMP_TextInfo textInfo4 = m_TextMeshPro.textInfo;
				TMP_MeshInfo[] meshInfo2 = textInfo4.meshInfo;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2379 @ rcx_v80+50+v746 @ rdx_v59 (TMPro.TMP_CharacterInfo[])]");
				object obj94 = (nint)0 * (nint)4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2379 @ rcx_v80+50+v746 @ rdx_v59 (TMPro.TMP_CharacterInfo[])]");
				object obj95 = 0 + obj94;
				object obj96 = obj95 + obj95;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v751 @ rdx_v65 (TMPro.TMP_MeshInfo[])+58+v828 @ rcx_v110*8]");
				TMP_MeshInfo tMP_MeshInfo2 = (TMP_MeshInfo)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj97 = (nint)0 + (nint)1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+D0]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+D0]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj98 = (nint)0 + (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+D0]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				object obj99 = (nint)0 + (nint)3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+D0]");
				_ = 0;
				TMP_TextInfo textInfo5 = m_TextMeshPro.textInfo;
				TMP_MeshInfo[] meshInfo3 = textInfo5.meshInfo;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2379 @ rcx_v80+50+v746 @ rdx_v59 (TMPro.TMP_CharacterInfo[])]");
				object obj100 = (nint)0 * (nint)4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2379 @ rcx_v80+50+v746 @ rdx_v59 (TMPro.TMP_CharacterInfo[])]");
				object obj101 = 0 + obj100;
				object obj102 = obj101 + obj101;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v754 @ rdx_v68 (TMPro.TMP_MeshInfo[])+20+v2965 @ rcx_v116*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v754 @ rdx_v68 (TMPro.TMP_MeshInfo[])+30+v2965 @ rcx_v116*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v754 @ rdx_v68 (TMPro.TMP_MeshInfo[])+40+v2965 @ rcx_v116*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v754 @ rdx_v68 (TMPro.TMP_MeshInfo[])+50+v2965 @ rcx_v116*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v754 @ rdx_v68 (TMPro.TMP_MeshInfo[])+60+v2965 @ rcx_v116*8]");
				obj = 0;
				int dst = tMP_MeshInfo.normals - 4;
				TMP_MeshInfo tMP_MeshInfo3 = (TMP_MeshInfo)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 64));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2541 @ rcx_v82+64+v688 @ rsi_v21 (TMPro.TMP_CharacterInfo[])]");
				((TMP_MeshInfo*)tMP_MeshInfo3)->SwapVertexData(0, dst);
				m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
			}
		}
		Vector3 mousePosition2 = Input.mousePosition;
		float num14 = default(float);
		int num13 = TMP_TextUtilities.FindIntersectingWord(m_TextMeshPro, (Vector3)(&num14), m_Camera);
		bool flag = m_TextPopup_RectTransform != null;
		bool flag2 = !flag;
		TMP_WordInfo tMP_WordInfo = (TMP_WordInfo)num12;
		if (!flag2)
		{
			bool flag3 = m_selectedWord == -1;
			tMP_WordInfo = (TMP_WordInfo)num12;
			if (!flag3)
			{
				if (num13 != -1)
				{
					bool flag4 = num13 == m_selectedWord;
					tMP_WordInfo = (TMP_WordInfo)num12;
					if (flag4)
					{
						goto IL_15be;
					}
				}
				TMP_TextInfo textInfo6 = m_TextMeshPro.textInfo;
				TMP_WordInfo[] wordInfo = textInfo6.wordInfo;
				object obj103 = m_selectedWord * 2;
				object obj104 = m_selectedWord + obj103;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v757 @ rdx_v46 (TMPro.TMP_WordInfo[])+20+v2459 @ rcx_v60*8]");
				tMP_WordInfo = (TMP_WordInfo)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v757 @ rdx_v46 (TMPro.TMP_WordInfo[])+30+v2459 @ rcx_v60*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-80]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm2,8\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v757 @ rdx_v46 (TMPro.TMP_WordInfo[])+20+v2459 @ rcx_v60*8]");
					object obj105 = -0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v757 @ rdx_v46 (TMPro.TMP_WordInfo[])+20+v2459 @ rcx_v60*8]");
					TMP_WordInfo tMP_WordInfo2 = (TMP_WordInfo)0;
					object obj112;
					do
					{
						TMP_TextInfo textInfo7 = m_TextMeshPro.textInfo;
						TMP_CharacterInfo[] characterInfo3 = textInfo7.characterInfo;
						object obj106 = tMP_WordInfo2 * 376;
						TMP_TextInfo textInfo8 = m_TextMeshPro.textInfo;
						TMP_CharacterInfo[] characterInfo4 = textInfo8.characterInfo;
						object obj107 = tMP_WordInfo2 * 376;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2690 @ rcx_v67+64+v690 @ rsi_v18 (TMPro.TMP_CharacterInfo[])]");
						object obj108 = 0;
						TMP_TextInfo textInfo9 = m_TextMeshPro.textInfo;
						TMP_MeshInfo[] meshInfo4 = textInfo9.meshInfo;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2612 @ rcx_v65+50+v760 @ rdx_v51 (TMPro.TMP_CharacterInfo[])]");
						object obj109 = (nint)0 * (nint)4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2612 @ rcx_v65+50+v760 @ rdx_v51 (TMPro.TMP_CharacterInfo[])]");
						object obj110 = 0 + obj109;
						object obj111 = obj110 + obj110;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v719 @ rdi_v22 (TMPro.TMP_MeshInfo[])+58+v834 @ rcx_v70*8]");
						TMP_MeshInfo tMP_MeshInfo4 = (TMP_MeshInfo)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v720 @ rdi_v23 (TMPro.TMP_MeshInfo)+20+v691 @ rsi_v19*4]");
						Color32 color = TMPro_ExtensionMethods.Tint((Color32)0, 1.33333f);
						tMP_WordInfo2 = (TMP_WordInfo)(tMP_WordInfo2 + 1);
						obj112 = (object)tMP_WordInfo2 + obj105;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v757 @ rdx_v46 (TMPro.TMP_WordInfo[])+30+v2459 @ rcx_v60*8]");
					}
					while ((nint)obj112 < 0);
				}
				m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
				m_selectedWord = -1;
			}
		}
		if (num13 != -1 && num13 != m_selectedWord && !Input.GetKeyInt(KeyCode.LeftShift) && !Input.GetKeyInt(KeyCode.RightShift))
		{
			m_selectedWord = num13;
			TMP_TextInfo textInfo10 = m_TextMeshPro.textInfo;
			TMP_WordInfo[] wordInfo2 = textInfo10.wordInfo;
			object obj113 = num13 * 2;
			object obj114 = num13 + obj113;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v766 @ rdx_v37 (TMPro.TMP_WordInfo[])+20+v2556 @ rcx_v44*8]");
			tMP_WordInfo = (TMP_WordInfo)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v766 @ rdx_v37 (TMPro.TMP_WordInfo[])+30+v2556 @ rcx_v44*8]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-80]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm2,8\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v766 @ rdx_v37 (TMPro.TMP_WordInfo[])+20+v2556 @ rcx_v44*8]");
				object obj115 = -0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v766 @ rdx_v37 (TMPro.TMP_WordInfo[])+20+v2556 @ rcx_v44*8]");
				TMP_WordInfo tMP_WordInfo3 = (TMP_WordInfo)0;
				object obj122;
				do
				{
					TMP_TextInfo textInfo11 = m_TextMeshPro.textInfo;
					TMP_CharacterInfo[] characterInfo5 = textInfo11.characterInfo;
					object obj116 = tMP_WordInfo3 * 376;
					TMP_TextInfo textInfo12 = m_TextMeshPro.textInfo;
					TMP_CharacterInfo[] characterInfo6 = textInfo12.characterInfo;
					object obj117 = tMP_WordInfo3 * 376;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2719 @ rcx_v51+64+v695 @ rsi_v14 (TMPro.TMP_CharacterInfo[])]");
					object obj118 = 0;
					TMP_TextInfo textInfo13 = m_TextMeshPro.textInfo;
					TMP_MeshInfo[] meshInfo5 = textInfo13.meshInfo;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2680 @ rcx_v49+50+v769 @ rdx_v42 (TMPro.TMP_CharacterInfo[])]");
					object obj119 = (nint)0 * (nint)4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2680 @ rcx_v49+50+v769 @ rdx_v42 (TMPro.TMP_CharacterInfo[])]");
					object obj120 = 0 + obj119;
					object obj121 = obj120 + obj120;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v724 @ rdi_v18 (TMPro.TMP_MeshInfo[])+58+v840 @ rcx_v54*8]");
					TMP_MeshInfo tMP_MeshInfo5 = (TMP_MeshInfo)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v725 @ rdi_v19 (TMPro.TMP_MeshInfo)+20+v696 @ rsi_v15*4]");
					Color32 color2 = TMPro_ExtensionMethods.Tint((Color32)0, 0.75f);
					tMP_WordInfo3 = (TMP_WordInfo)(tMP_WordInfo3 + 1);
					obj122 = obj115 + (object)tMP_WordInfo3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v766 @ rdx_v37 (TMPro.TMP_WordInfo[])+30+v2556 @ rcx_v44*8]");
				}
				while ((nint)obj122 < 0);
			}
			m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
		}
		goto IL_15be;
		IL_15be:
		Vector3 mousePosition3 = Input.mousePosition;
		int num15 = TMP_TextUtilities.FindIntersectingLink(m_TextMeshPro, (Vector3)(&num14), m_Camera);
		if ((num15 == -1 && m_selectedLink != num15) || num15 != m_selectedLink)
		{
			GameObject gameObject = m_TextPopup_RectTransform.gameObject;
			gameObject.SetActive(value: false);
			m_selectedLink = -1;
		}
		if (num15 == -1 || num15 == m_selectedLink)
		{
			return;
		}
		m_selectedLink = num15;
		TMP_TextInfo textInfo14 = m_TextMeshPro.textInfo;
		TMP_LinkInfo[] linkInfo = textInfo14.linkInfo;
		object obj123 = num15 * 4;
		object obj124 = num15 + obj123;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v777 @ rdx_v15 (TMPro.TMP_LinkInfo[])+20+v2609 @ rcx_v20*8]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v777 @ rdx_v15 (TMPro.TMP_LinkInfo[])+30+v2609 @ rcx_v20*8]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v777 @ rdx_v15 (TMPro.TMP_LinkInfo[])+40+v2609 @ rcx_v20*8]");
		_ = 0;
		RectTransform rectTransform = m_TextMeshPro.rectTransform;
		Vector3 mousePosition4 = Input.mousePosition;
		bool flag5 = RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, vector, m_Camera, out var worldPoint);
		TMP_LinkInfo tMP_LinkInfo = (TMP_LinkInfo)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 112));
		string linkID = ((TMP_LinkInfo*)tMP_LinkInfo)->GetLinkID();
		if (linkID == "id_01")
		{
			m_TextPopup_RectTransform.position = (Vector3)(&num14);
			GameObject gameObject2 = m_TextPopup_RectTransform.gameObject;
			gameObject2.SetActive(value: true);
			TextMeshProUGUI textPopup_TMPComponent = m_TextPopup_TMPComponent;
			nint num16 = (nint)textPopup_TMPComponent;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2802 @ r8_v21 (Il2CppClass<TMPro.TextMeshProUGUI>)+558]");
			object obj125 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2802 @ r8_v21 (Il2CppClass<TMPro.TextMeshProUGUI>)+560]");
			object obj126 = 0;
			Vector3 vector2 = worldPoint;
			object obj127 = "You have selected link <#ffff00> ID 01";
		}
		else
		{
			if (!(linkID == "id_02"))
			{
				return;
			}
			m_TextPopup_RectTransform.position = (Vector3)(&num14);
			GameObject gameObject3 = m_TextPopup_RectTransform.gameObject;
			gameObject3.SetActive(value: true);
			TextMeshProUGUI textPopup_TMPComponent = m_TextPopup_TMPComponent;
			nint num17 = (nint)textPopup_TMPComponent;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2868 @ r8_v17 (Il2CppClass<TMPro.TextMeshProUGUI>)+558]");
			object obj125 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2868 @ r8_v17 (Il2CppClass<TMPro.TextMeshProUGUI>)+560]");
			object obj126 = 0;
			Vector3 vector2 = worldPoint;
			object obj127 = "You have selected link <#ffff00> ID 02";
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v2842 @ rax_v28 (should have been resolved before IL gen)");
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isHoveringObject = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isHoveringObject = false;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}

	private void RestoreCachedVertexAttributes(int index)
	{
		//IL_0044: Expected O, but got I4
		//IL_0094: Expected O, but got I4
		//IL_00d7: Expected O, but got I4
		//IL_00e7: Expected O, but got I
		//IL_0102: Expected O, but got I
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_0134: Expected O, but got I
		//IL_0175: Expected O, but got I
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Expected O, but got Unknown
		//IL_01a7: Expected O, but got I
		//IL_01c2: Expected O, but got I
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Expected O, but got Unknown
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Expected O, but got Unknown
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_0215: Expected O, but got I
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Expected O, but got Unknown
		//IL_025a: Expected O, but got I
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Expected O, but got Unknown
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Expected O, but got Unknown
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Expected O, but got Unknown
		//IL_02b3: Expected O, but got I
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Expected O, but got Unknown
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Expected O, but got Unknown
		//IL_0311: Expected O, but got I
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Expected O, but got Unknown
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Expected O, but got Unknown
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Expected O, but got Unknown
		//IL_036a: Expected O, but got I
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Expected O, but got Unknown
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Expected O, but got Unknown
		//IL_03c8: Expected O, but got I
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Expected O, but got Unknown
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Expected O, but got Unknown
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Expected O, but got Unknown
		//IL_0421: Expected O, but got I
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Expected O, but got Unknown
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Expected O, but got Unknown
		//IL_04b4: Expected O, but got I
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Expected O, but got Unknown
		//IL_04e6: Expected O, but got I
		//IL_0501: Expected O, but got I
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Expected O, but got Unknown
		//IL_0533: Expected O, but got I
		//IL_055b: Expected O, but got I
		//IL_0576: Expected O, but got I
		//IL_059e: Expected O, but got I
		//IL_05b9: Expected O, but got I
		//IL_05e1: Expected O, but got I
		//IL_05fc: Expected O, but got I
		//IL_0633: Expected O, but got I
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Expected O, but got Unknown
		//IL_0665: Expected O, but got I
		//IL_06a6: Expected O, but got I
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bb: Expected O, but got Unknown
		//IL_06d8: Expected O, but got I
		//IL_06f3: Expected O, but got I
		//IL_071b: Expected O, but got I
		//IL_074b: Expected O, but got I
		//IL_0759: Unknown result type (might be due to invalid IL or missing references)
		//IL_075e: Expected O, but got Unknown
		//IL_0781: Expected O, but got I
		//IL_078f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0794: Expected O, but got Unknown
		//IL_07c4: Expected O, but got I
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d7: Expected O, but got Unknown
		//IL_07fa: Expected O, but got I
		//IL_0808: Unknown result type (might be due to invalid IL or missing references)
		//IL_080d: Expected O, but got Unknown
		//IL_083d: Expected O, but got I
		//IL_084b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0850: Expected O, but got Unknown
		//IL_0873: Expected O, but got I
		//IL_0881: Unknown result type (might be due to invalid IL or missing references)
		//IL_0886: Expected O, but got Unknown
		//IL_08c5: Expected O, but got I
		//IL_08ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d3: Expected O, but got Unknown
		//IL_08fd: Expected O, but got I
		//IL_093e: Expected O, but got I
		//IL_0947: Unknown result type (might be due to invalid IL or missing references)
		//IL_094c: Expected O, but got Unknown
		//IL_0976: Expected O, but got I
		//IL_0991: Expected O, but got I
		//IL_09c6: Expected O, but got I
		//IL_09fb: Expected O, but got I
		//IL_0a16: Expected O, but got I
		//IL_0a4b: Expected O, but got I
		//IL_0a66: Expected O, but got I
		//IL_0aa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa6: Expected O, but got Unknown
		//IL_0acf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad4: Expected O, but got Unknown
		//IL_0add: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae2: Expected O, but got Unknown
		//IL_0af0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af5: Expected O, but got Unknown
		//IL_0b0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b10: Expected O, but got Unknown
		//IL_0b20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b25: Expected O, but got Unknown
		//IL_0b33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b38: Expected O, but got Unknown
		//IL_0b68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6d: Expected O, but got Unknown
		//IL_0b7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b80: Expected O, but got Unknown
		//IL_0b96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9b: Expected O, but got Unknown
		//IL_0bab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb0: Expected O, but got Unknown
		//IL_0bb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbe: Expected O, but got Unknown
		//IL_0bcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd1: Expected O, but got Unknown
		//IL_0be7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bec: Expected O, but got Unknown
		//IL_0c0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c14: Expected O, but got Unknown
		//IL_0c22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c27: Expected O, but got Unknown
		//IL_0c3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c42: Expected O, but got Unknown
		//IL_0c52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c57: Expected O, but got Unknown
		//IL_0c60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c65: Expected O, but got Unknown
		//IL_0c73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c78: Expected O, but got Unknown
		//IL_0c8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c93: Expected O, but got Unknown
		//IL_0cb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbb: Expected O, but got Unknown
		//IL_0cc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cce: Expected O, but got Unknown
		//IL_0ce4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce9: Expected O, but got Unknown
		//IL_0cf9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfe: Expected O, but got Unknown
		//IL_0d07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0c: Expected O, but got Unknown
		//IL_0d1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1f: Expected O, but got Unknown
		//IL_0d35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3a: Expected O, but got Unknown
		//IL_0d79: Expected O, but got I
		//IL_0d89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8e: Expected O, but got Unknown
		//IL_0dab: Expected O, but got I
		//IL_0dec: Expected O, but got I
		//IL_0dfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e01: Expected O, but got Unknown
		//IL_0e1e: Expected O, but got I
		//IL_0e39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3e: Expected O, but got Unknown
		//IL_0e4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e51: Expected O, but got Unknown
		//IL_0e6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e71: Expected O, but got Unknown
		//IL_0e7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e84: Expected O, but got Unknown
		//IL_0e9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea4: Expected O, but got Unknown
		//IL_0eb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb7: Expected O, but got Unknown
		//IL_0eee: Expected O, but got I
		//IL_0efe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f03: Expected O, but got Unknown
		//IL_0f20: Expected O, but got I
		//IL_0f61: Expected O, but got I
		//IL_0f71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f76: Expected O, but got Unknown
		//IL_0f93: Expected O, but got I
		//IL_0fa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa6: Expected O, but got Unknown
		//IL_0fc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc6: Expected O, but got Unknown
		//IL_0fe9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fee: Expected O, but got Unknown
		//IL_0ffc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1001: Expected O, but got Unknown
		//IL_1017: Unknown result type (might be due to invalid IL or missing references)
		//IL_101c: Expected O, but got Unknown
		//IL_102a: Unknown result type (might be due to invalid IL or missing references)
		//IL_102f: Expected O, but got Unknown
		//IL_1052: Unknown result type (might be due to invalid IL or missing references)
		//IL_1057: Expected O, but got Unknown
		//IL_1065: Unknown result type (might be due to invalid IL or missing references)
		//IL_106a: Expected O, but got Unknown
		//IL_1073: Unknown result type (might be due to invalid IL or missing references)
		//IL_1078: Expected O, but got Unknown
		//IL_1093: Unknown result type (might be due to invalid IL or missing references)
		//IL_1098: Expected O, but got Unknown
		//IL_10bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c0: Expected O, but got Unknown
		//IL_10ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d3: Expected O, but got Unknown
		//IL_10dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e1: Expected O, but got Unknown
		//IL_10fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1101: Expected O, but got Unknown
		//IL_1140: Expected O, but got I
		//IL_1149: Unknown result type (might be due to invalid IL or missing references)
		//IL_114e: Expected O, but got Unknown
		//IL_1178: Expected O, but got I
		//IL_11b9: Expected O, but got I
		//IL_11c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c7: Expected O, but got Unknown
		//IL_11f1: Expected O, but got I
		//IL_1219: Unknown result type (might be due to invalid IL or missing references)
		//IL_121e: Expected O, but got Unknown
		//IL_122c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1231: Expected O, but got Unknown
		//IL_1259: Unknown result type (might be due to invalid IL or missing references)
		//IL_125e: Expected O, but got Unknown
		//IL_126c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1271: Expected O, but got Unknown
		//IL_1299: Unknown result type (might be due to invalid IL or missing references)
		//IL_129e: Expected O, but got Unknown
		//IL_12ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b1: Expected O, but got Unknown
		if (index != -1)
		{
			TMP_TextInfo textInfo = m_TextMeshPro.textInfo;
			object obj = textInfo.characterCount - 1;
			if (index <= (nint)obj)
			{
				TMP_TextInfo textInfo2 = m_TextMeshPro.textInfo;
				TMP_CharacterInfo[] characterInfo = textInfo2.characterInfo;
				object obj2 = index * 376;
				TMP_TextInfo textInfo3 = m_TextMeshPro.textInfo;
				TMP_CharacterInfo[] characterInfo2 = textInfo3.characterInfo;
				TMP_MeshInfo[] cachedMeshInfoVertexData = m_cachedMeshInfoVertexData;
				object obj3 = index * 376;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj5 = (nint)0 * (nint)4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj6 = 0 + obj5;
				object obj7 = obj6 + obj6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rsi_v6 (TMPro.TMP_MeshInfo[])+30+v1789 @ rcx_v11*8]");
				TMP_MeshInfo tMP_MeshInfo = (TMP_MeshInfo)0;
				TMP_TextInfo textInfo4 = m_TextMeshPro.textInfo;
				TMP_MeshInfo[] meshInfo = textInfo4.meshInfo;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj8 = (nint)0 * (nint)4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj9 = 0 + obj8;
				object obj10 = obj9 + obj9;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ rbp_v6 (TMPro.TMP_MeshInfo[])+30+v216 @ rcx_v14*8]");
				TMP_MeshInfo tMP_MeshInfo2 = (TMP_MeshInfo)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj11 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj12 = 0 + obj11;
				object obj13 = obj12 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rsi_v6 (TMPro.TMP_MeshInfo[])+30+v1789 @ rcx_v11*8]");
				object obj14 = 0 + obj13;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj15 = (nint)0 * (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj16 = 0 + obj15;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rdx_v10+20]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rdx_v10+28]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj17 = (nint)0 + (nint)1;
				object obj18 = obj17 * 2;
				object obj19 = obj17 + obj18;
				object obj20 = obj19 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rsi_v6 (TMPro.TMP_MeshInfo[])+30+v1789 @ rcx_v11*8]");
				object obj21 = 0 + obj20;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj22 = (nint)0 + (nint)1;
				object obj23 = obj22 * 2;
				object obj24 = obj22 + obj23;
				object obj25 = obj24 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v958 @ rdx_v11+20]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v958 @ rdx_v11+28]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj26 = (nint)0 + (nint)2;
				object obj27 = obj26 * 2;
				object obj28 = obj26 + obj27;
				object obj29 = obj28 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rsi_v6 (TMPro.TMP_MeshInfo[])+30+v1789 @ rcx_v11*8]");
				object obj30 = 0 + obj29;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj31 = (nint)0 + (nint)2;
				object obj32 = obj31 * 2;
				object obj33 = obj31 + obj32;
				object obj34 = obj33 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v959 @ rdx_v12+20]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v959 @ rdx_v12+28]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj35 = (nint)0 + (nint)3;
				object obj36 = obj35 * 2;
				object obj37 = obj35 + obj36;
				object obj38 = obj37 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rsi_v6 (TMPro.TMP_MeshInfo[])+30+v1789 @ rcx_v11*8]");
				object obj39 = 0 + obj38;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj40 = (nint)0 + (nint)3;
				object obj41 = obj40 * 2;
				object obj42 = obj40 + obj41;
				object obj43 = obj42 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ rdx_v13+20]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ rdx_v13+28]");
				_ = 0;
				TMP_TextInfo textInfo5 = m_TextMeshPro.textInfo;
				TMP_MeshInfo[] meshInfo2 = textInfo5.meshInfo;
				TMP_MeshInfo[] cachedMeshInfoVertexData2 = m_cachedMeshInfoVertexData;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj44 = (nint)0 * (nint)4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj45 = 0 + obj44;
				object obj46 = obj45 + obj45;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ r8_v5 (TMPro.TMP_MeshInfo[])+58+v219 @ rcx_v22*8]");
				TMP_MeshInfo tMP_MeshInfo3 = (TMP_MeshInfo)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj47 = (nint)0 * (nint)4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj48 = 0 + obj47;
				object obj49 = obj48 + obj48;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v197 @ rdx_v15 (TMPro.TMP_MeshInfo[])+58+v220 @ rcx_v24*8]");
				TMP_MeshInfo tMP_MeshInfo4 = (TMP_MeshInfo)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rdx_v16 (TMPro.TMP_MeshInfo)+20+v752 @ rbx_v7*4]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj50 = (nint)0 + (nint)1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj51 = (nint)0 + (nint)1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rdx_v16 (TMPro.TMP_MeshInfo)+20+v916 @ rax_v31*4]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj52 = (nint)0 + (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj53 = (nint)0 + (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rdx_v16 (TMPro.TMP_MeshInfo)+20+v918 @ rax_v33*4]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj54 = (nint)0 + (nint)3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj55 = (nint)0 + (nint)3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rdx_v16 (TMPro.TMP_MeshInfo)+20+v920 @ rax_v35*4]");
				_ = 0;
				TMP_MeshInfo[] cachedMeshInfoVertexData3 = m_cachedMeshInfoVertexData;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj56 = (nint)0 * (nint)4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj57 = 0 + obj56;
				object obj58 = obj57 + obj57;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ r14_v6 (TMPro.TMP_MeshInfo[])+48+v1814 @ rcx_v29*8]");
				TMP_MeshInfo tMP_MeshInfo5 = (TMP_MeshInfo)0;
				TMP_TextInfo textInfo6 = m_TextMeshPro.textInfo;
				TMP_MeshInfo[] meshInfo3 = textInfo6.meshInfo;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj59 = (nint)0 * (nint)4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj60 = 0 + obj59;
				object obj61 = obj60 + obj60;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v200 @ rdx_v18 (TMPro.TMP_MeshInfo[])+48+v223 @ rcx_v32*8]");
				TMP_MeshInfo tMP_MeshInfo6 = (TMP_MeshInfo)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj62 = (nint)0 + (nint)2;
				object obj63 = obj62 + obj62;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj64 = (nint)0 + (nint)2;
				object obj65 = obj64 + obj64;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ r14_v7 (TMPro.TMP_MeshInfo)+v224 @ rcx_v34*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj66 = (nint)0 + (nint)1;
				object obj67 = obj66 + 2;
				object obj68 = obj67 + obj67;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj69 = (nint)0 + (nint)1;
				object obj70 = obj69 + 2;
				object obj71 = obj70 + obj70;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ r14_v7 (TMPro.TMP_MeshInfo)+v969 @ rcx_v36*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj72 = (nint)0 + (nint)2;
				object obj73 = obj72 + 2;
				object obj74 = obj73 + obj73;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj75 = (nint)0 + (nint)2;
				object obj76 = obj75 + 2;
				object obj77 = obj76 + obj76;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ r14_v7 (TMPro.TMP_MeshInfo)+v970 @ rcx_v38*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj78 = (nint)0 + (nint)3;
				object obj79 = obj78 + 2;
				object obj80 = obj79 + obj79;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj81 = (nint)0 + (nint)3;
				object obj82 = obj81 + 2;
				object obj83 = obj82 + obj82;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ r14_v7 (TMPro.TMP_MeshInfo)+v225 @ rcx_v40*8]");
				_ = 0;
				TMP_MeshInfo[] cachedMeshInfoVertexData4 = m_cachedMeshInfoVertexData;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj84 = (nint)0 + (nint)1;
				object obj85 = obj84 * 4;
				object obj86 = obj84 + obj85;
				object obj87 = obj86 + obj86;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r14_v8 (TMPro.TMP_MeshInfo[])+v176 @ rax_v54*8]");
				TMP_MeshInfo tMP_MeshInfo7 = (TMP_MeshInfo)0;
				TMP_TextInfo textInfo7 = m_TextMeshPro.textInfo;
				TMP_MeshInfo[] meshInfo4 = textInfo7.meshInfo;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj88 = (nint)0 + (nint)1;
				object obj89 = obj88 * 4;
				object obj90 = obj88 + obj89;
				object obj91 = obj90 + obj90;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ rcx_v42 (TMPro.TMP_MeshInfo[])+v178 @ rax_v58*8]");
				TMP_MeshInfo tMP_MeshInfo8 = (TMP_MeshInfo)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj92 = (nint)0 + (nint)1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ r14_v9 (TMPro.TMP_MeshInfo)+20+v752 @ rbx_v7*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ r14_v9 (TMPro.TMP_MeshInfo)+24+v752 @ rbx_v7*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj93 = (nint)0 + (nint)1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ r14_v9 (TMPro.TMP_MeshInfo)+20+v927 @ rax_v59*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ r14_v9 (TMPro.TMP_MeshInfo)+24+v927 @ rax_v59*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj94 = (nint)0 + (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj95 = (nint)0 + (nint)2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ r14_v9 (TMPro.TMP_MeshInfo)+20+v929 @ rax_v61*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ r14_v9 (TMPro.TMP_MeshInfo)+24+v929 @ rax_v61*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj96 = (nint)0 + (nint)3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v9+64+v192 @ rdx_v8 (TMPro.TMP_CharacterInfo[])]");
				object obj97 = (nint)0 + (nint)3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ r14_v9 (TMPro.TMP_MeshInfo)+20+v931 @ rax_v63*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ r14_v9 (TMPro.TMP_MeshInfo)+24+v931 @ rax_v63*8]");
				_ = 0;
				object obj98 = (object)tMP_MeshInfo.normals >> 31;
				object obj99 = obj98 & 3;
				object obj100 = (object)tMP_MeshInfo.normals + obj99;
				object obj101 = obj100 >> 2;
				object obj102 = obj101 * 4;
				object obj103 = obj102 - 4;
				object obj104 = obj103 * 2;
				object obj105 = obj103 + obj104;
				object obj106 = obj105 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rsi_v6 (TMPro.TMP_MeshInfo[])+30+v1789 @ rcx_v11*8]");
				object obj107 = 0 + obj106;
				object obj108 = obj103 * 2;
				object obj109 = obj103 + obj108;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v961 @ rdx_v23+20]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v961 @ rdx_v23+28]");
				_ = 0;
				object obj110 = obj103 + 1;
				object obj111 = obj110 * 2;
				object obj112 = obj110 + obj111;
				object obj113 = obj112 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rsi_v6 (TMPro.TMP_MeshInfo[])+30+v1789 @ rcx_v11*8]");
				object obj114 = 0 + obj113;
				object obj115 = obj103 + 1;
				object obj116 = obj115 * 2;
				object obj117 = obj115 + obj116;
				object obj118 = obj117 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v962 @ rdx_v24+20]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v962 @ rdx_v24+28]");
				_ = 0;
				object obj119 = obj103 + 2;
				object obj120 = obj119 * 2;
				object obj121 = obj119 + obj120;
				object obj122 = obj121 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rsi_v6 (TMPro.TMP_MeshInfo[])+30+v1789 @ rcx_v11*8]");
				object obj123 = 0 + obj122;
				object obj124 = obj103 + 2;
				object obj125 = obj124 * 2;
				object obj126 = obj124 + obj125;
				object obj127 = obj126 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v963 @ rdx_v25+20]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v963 @ rdx_v25+28]");
				_ = 0;
				object obj128 = obj103 + 3;
				object obj129 = obj128 * 2;
				object obj130 = obj128 + obj129;
				object obj131 = obj130 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rsi_v6 (TMPro.TMP_MeshInfo[])+30+v1789 @ rcx_v11*8]");
				object obj132 = 0 + obj131;
				object obj133 = obj103 + 3;
				object obj134 = obj133 * 2;
				object obj135 = obj133 + obj134;
				object obj136 = obj135 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ rdx_v26+20]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ rdx_v26+28]");
				_ = 0;
				TMP_MeshInfo[] cachedMeshInfoVertexData5 = m_cachedMeshInfoVertexData;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj137 = (nint)0 * (nint)4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj138 = 0 + obj137;
				object obj139 = obj138 + obj138;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ rsi_v8 (TMPro.TMP_MeshInfo[])+58+v1863 @ rcx_v50*8]");
				TMP_MeshInfo tMP_MeshInfo9 = (TMP_MeshInfo)0;
				TMP_TextInfo textInfo8 = m_TextMeshPro.textInfo;
				TMP_MeshInfo[] meshInfo5 = textInfo8.meshInfo;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj140 = (nint)0 * (nint)4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj141 = 0 + obj140;
				object obj142 = obj141 + obj141;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ rdx_v28 (TMPro.TMP_MeshInfo[])+58+v231 @ rcx_v53*8]");
				TMP_MeshInfo tMP_MeshInfo10 = (TMP_MeshInfo)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ rsi_v9 (TMPro.TMP_MeshInfo)+20+v753 @ rbx_v9*4]");
				_ = 0;
				object obj143 = obj103 + 1;
				object obj144 = obj103 + 1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ rsi_v9 (TMPro.TMP_MeshInfo)+20+v942 @ rax_v96*4]");
				_ = 0;
				object obj145 = obj103 + 2;
				object obj146 = obj103 + 2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ rsi_v9 (TMPro.TMP_MeshInfo)+20+v944 @ rax_v99*4]");
				_ = 0;
				object obj147 = obj103 + 3;
				object obj148 = obj103 + 3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ rsi_v9 (TMPro.TMP_MeshInfo)+20+v946 @ rax_v102*4]");
				_ = 0;
				TMP_MeshInfo[] cachedMeshInfoVertexData6 = m_cachedMeshInfoVertexData;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj149 = (nint)0 * (nint)4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj150 = 0 + obj149;
				object obj151 = obj150 + obj150;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rsi_v10 (TMPro.TMP_MeshInfo[])+48+v1873 @ rcx_v62*8]");
				TMP_MeshInfo tMP_MeshInfo11 = (TMP_MeshInfo)0;
				TMP_TextInfo textInfo9 = m_TextMeshPro.textInfo;
				TMP_MeshInfo[] meshInfo6 = textInfo9.meshInfo;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj152 = (nint)0 * (nint)4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj153 = 0 + obj152;
				object obj154 = obj153 + obj153;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v208 @ rdx_v31 (TMPro.TMP_MeshInfo[])+48+v234 @ rcx_v65*8]");
				TMP_MeshInfo tMP_MeshInfo12 = (TMP_MeshInfo)0;
				object obj155 = obj103 + 2;
				object obj156 = obj155 + obj155;
				object obj157 = obj103 + 2;
				object obj158 = obj157 + obj157;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ rsi_v11 (TMPro.TMP_MeshInfo)+v235 @ rcx_v68*8]");
				_ = 0;
				object obj159 = obj103 + 1;
				object obj160 = obj159 + 2;
				object obj161 = obj160 + obj160;
				object obj162 = obj103 + 1;
				object obj163 = obj162 + 2;
				object obj164 = obj163 + obj163;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ rsi_v11 (TMPro.TMP_MeshInfo)+v978 @ rcx_v70*8]");
				_ = 0;
				object obj165 = obj103 + 2;
				object obj166 = obj165 + 2;
				object obj167 = obj103 + 2;
				object obj168 = obj166 + obj166;
				object obj169 = obj167 + 2;
				object obj170 = obj169 + obj169;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ rsi_v11 (TMPro.TMP_MeshInfo)+v979 @ rcx_v72*8]");
				_ = 0;
				object obj171 = obj103 + 3;
				object obj172 = obj171 + 2;
				object obj173 = obj103 + 3;
				object obj174 = obj172 + obj172;
				object obj175 = obj173 + 2;
				object obj176 = obj175 + obj175;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ rsi_v11 (TMPro.TMP_MeshInfo)+v236 @ rcx_v74*8]");
				_ = 0;
				TMP_MeshInfo[] cachedMeshInfoVertexData7 = m_cachedMeshInfoVertexData;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj177 = (nint)0 + (nint)1;
				object obj178 = obj177 * 4;
				object obj179 = obj177 + obj178;
				object obj180 = obj179 + obj179;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ rsi_v12 (TMPro.TMP_MeshInfo[])+v184 @ rax_v128*8]");
				TMP_MeshInfo tMP_MeshInfo13 = (TMP_MeshInfo)0;
				TMP_TextInfo textInfo10 = m_TextMeshPro.textInfo;
				TMP_MeshInfo[] meshInfo7 = textInfo10.meshInfo;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1787 @ rcx_v7+50+v132 @ rdi_v6 (TMPro.TMP_CharacterInfo[])]");
				object obj181 = (nint)0 + (nint)1;
				object obj182 = obj181 * 4;
				object obj183 = obj181 + obj182;
				object obj184 = obj183 + obj183;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v238 @ rcx_v76 (TMPro.TMP_MeshInfo[])+v186 @ rax_v132*8]");
				TMP_MeshInfo tMP_MeshInfo14 = (TMP_MeshInfo)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rsi_v13 (TMPro.TMP_MeshInfo)+20+v753 @ rbx_v9*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rsi_v13 (TMPro.TMP_MeshInfo)+24+v753 @ rbx_v9*8]");
				_ = 0;
				object obj185 = obj103 + 1;
				object obj186 = obj103 + 1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rsi_v13 (TMPro.TMP_MeshInfo)+20+v953 @ rax_v136*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rsi_v13 (TMPro.TMP_MeshInfo)+24+v953 @ rax_v136*8]");
				_ = 0;
				object obj187 = obj103 + 2;
				object obj188 = obj103 + 2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rsi_v13 (TMPro.TMP_MeshInfo)+20+v955 @ rax_v140*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rsi_v13 (TMPro.TMP_MeshInfo)+24+v955 @ rax_v140*8]");
				_ = 0;
				object obj189 = obj103 + 3;
				object obj190 = obj103 + 3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rsi_v13 (TMPro.TMP_MeshInfo)+20+v957 @ rax_v144*8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rsi_v13 (TMPro.TMP_MeshInfo)+24+v957 @ rax_v144*8]");
				_ = 0;
				m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
			}
		}
	}

	public TMP_TextSelector_B()
	{
		//IL_001a: Expected I4, but got I8
		m_selectedWord = -1;
		m_lastIndex = -1;
		base._002Ector();
	}
}
