using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class TMP_UiFrameRateCounter : MonoBehaviour
{
	public enum FpsCounterAnchorPositions
	{
		TopLeft,
		BottomLeft,
		TopRight,
		BottomRight
	}

	public float UpdateInterval = 5f;

	private float m_LastInterval;

	private int m_Frames;

	public FpsCounterAnchorPositions AnchorPosition = FpsCounterAnchorPositions.TopRight;

	private string htmlColorTag;

	private const string fpsLabel = "{0:2}</color> <#8080ff>FPS \n<#FF8000>{1:2} <#8080ff>MS";

	private TextMeshProUGUI m_TextMeshPro;

	private RectTransform m_frameCounter_transform;

	private FpsCounterAnchorPositions last_AnchorPosition;

	private void Awake()
	{
		if (base.enabled)
		{
			Application.targetFrameRate = 1000;
			GameObject gameObject = new GameObject("Frame Counter");
			RectTransform frameCounter_transform = gameObject.AddComponent<RectTransform>();
			m_frameCounter_transform = frameCounter_transform;
			Transform parent = base.transform;
			m_frameCounter_transform.SetParent(parent, worldPositionStays: false);
			TextMeshProUGUI textMeshPro = gameObject.AddComponent<TextMeshProUGUI>();
			m_TextMeshPro = textMeshPro;
			TMP_FontAsset font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
			m_TextMeshPro.font = font;
			Material fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - Overlay");
			m_TextMeshPro.fontSharedMaterial = fontSharedMaterial;
			m_TextMeshPro.textWrappingMode = TextWrappingModes.NoWrap;
			m_TextMeshPro.fontSize = 36f;
			m_TextMeshPro.isOverlay = true;
			Set_FrameCounter_Position(AnchorPosition);
			last_AnchorPosition = AnchorPosition;
		}
	}

	private void Start()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		m_LastInterval = realtimeSinceStartup;
		m_Frames = 0;
	}

	private void Update()
	{
		//IL_00d7: Invalid comparison between F4 and I
		//IL_01ca: Invalid comparison between F4 and I
		//IL_0106: Expected F4, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39DE4]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (AnchorPosition != last_AnchorPosition)
		{
			Set_FrameCounter_Position(AnchorPosition);
		}
		int frames = m_Frames + 1;
		m_Frames = frames;
		last_AnchorPosition = AnchorPosition;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = m_LastInterval + UpdateInterval;
		if (!(realtimeSinceStartup > num))
		{
			return;
		}
		float num2 = realtimeSinceStartup - m_LastInterval;
		float num3 = (float)m_Frames / num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BBC]");
		bool flag = !(num3 < 0f);
		float num4 = num3;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BBC]");
			num4 = 0f;
		}
		float arg = 1000f / num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BBC]");
		if (!(num3 > 0f))
		{
			if (!(10f > num3))
			{
				htmlColorTag = "<color=green>";
			}
			else
			{
				htmlColorTag = "<color=red>";
			}
		}
		else
		{
			htmlColorTag = "<color=yellow>";
		}
		string sourceText = htmlColorTag + "{0:2}</color> <#8080ff>FPS \n<#FF8000>{1:2} <#8080ff>MS";
		m_TextMeshPro.SetText(sourceText, num3, arg);
		m_LastInterval = realtimeSinceStartup;
		m_Frames = 0;
	}

	private void Set_FrameCounter_Position(FpsCounterAnchorPositions anchor_position)
	{
		//IL_002b: Expected O, but got I4
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		bool flag = anchor_position == FpsCounterAnchorPositions.TopLeft;
		Vector2 vector = default(Vector2);
		RectTransform frameCounter_transform;
		Vector2 anchoredPosition;
		RectTransform frameCounter_transform2;
		Vector2 anchorMax;
		if (!flag)
		{
			object obj = anchor_position - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 == 1)
					{
						m_TextMeshPro.alignment = TextAlignmentOptions.BottomRight;
						m_frameCounter_transform.pivot = vector;
						m_frameCounter_transform.anchorMin = vector;
						m_frameCounter_transform.anchorMax = vector;
						frameCounter_transform = m_frameCounter_transform;
						anchoredPosition = vector;
						goto IL_01e6;
					}
					return;
				}
				m_TextMeshPro.alignment = TextAlignmentOptions.TopRight;
				m_frameCounter_transform.pivot = vector;
				m_frameCounter_transform.anchorMin = vector;
				m_frameCounter_transform.anchorMax = vector;
				frameCounter_transform = m_frameCounter_transform;
				goto IL_01f8;
			}
			m_TextMeshPro.alignment = TextAlignmentOptions.BottomLeft;
			m_frameCounter_transform.pivot = vector;
			m_frameCounter_transform.anchorMin = vector;
			frameCounter_transform2 = m_frameCounter_transform;
			anchorMax = vector;
		}
		else
		{
			m_TextMeshPro.alignment = TextAlignmentOptions.TopLeft;
			m_frameCounter_transform.pivot = vector;
			m_frameCounter_transform.anchorMin = vector;
			frameCounter_transform2 = m_frameCounter_transform;
			anchorMax = vector;
		}
		frameCounter_transform2.anchorMax = anchorMax;
		frameCounter_transform = m_frameCounter_transform;
		goto IL_01f8;
		IL_01e6:
		frameCounter_transform.anchoredPosition = anchoredPosition;
		return;
		IL_01f8:
		anchoredPosition = vector;
		goto IL_01e6;
	}
}
