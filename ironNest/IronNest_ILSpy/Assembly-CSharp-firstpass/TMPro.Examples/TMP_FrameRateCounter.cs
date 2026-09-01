using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class TMP_FrameRateCounter : MonoBehaviour
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

	private TextMeshPro m_TextMeshPro;

	private Transform m_frameCounter_transform;

	private Camera m_camera;

	private FpsCounterAnchorPositions last_AnchorPosition;

	private unsafe void Awake()
	{
		//IL_00f8: Expected O, but got Ref
		if (base.enabled)
		{
			Camera main = Camera.main;
			m_camera = main;
			Application.targetFrameRate = 9999;
			GameObject gameObject = new GameObject("Frame Counter");
			TextMeshPro textMeshPro = gameObject.AddComponent<TextMeshPro>();
			m_TextMeshPro = textMeshPro;
			TMP_FontAsset font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
			m_TextMeshPro.font = font;
			Material fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - Overlay");
			m_TextMeshPro.fontSharedMaterial = fontSharedMaterial;
			Transform frameCounter_transform = gameObject.transform;
			m_frameCounter_transform = frameCounter_transform;
			Transform parentInternal = m_camera.transform;
			m_frameCounter_transform.parentInternal = parentInternal;
			object obj = default(object);
			m_frameCounter_transform.localRotation = (Quaternion)(&obj);
			m_TextMeshPro.textWrappingMode = TextWrappingModes.NoWrap;
			m_TextMeshPro.fontSize = 24f;
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39DD2]");
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
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0055: Expected O, but got I4
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Expected O, but got Unknown
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Expected O, but got Unknown
		object obj = default(object);
		Vector4 margin = (Vector4)(obj - 48);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		_ = 0;
		m_TextMeshPro.margin = margin;
		bool flag = anchor_position == FpsCounterAnchorPositions.TopLeft;
		Vector2 vector = default(Vector2);
		Camera camera;
		Transform frameCounter_transform;
		RectTransform rectTransform3;
		float num;
		Vector2 vector2;
		if (!flag)
		{
			object obj2 = anchor_position - 1;
			if (!flag)
			{
				object obj3 = obj2 - 1;
				if (!flag)
				{
					if ((nint)obj3 == 1)
					{
						m_TextMeshPro.alignment = TextAlignmentOptions.BottomRight;
						RectTransform rectTransform = m_TextMeshPro.rectTransform;
						rectTransform.pivot = vector;
						camera = m_camera;
						frameCounter_transform = m_frameCounter_transform;
						num = 100f;
						vector2 = vector;
						goto IL_021c;
					}
					return;
				}
				m_TextMeshPro.alignment = TextAlignmentOptions.TopRight;
				RectTransform rectTransform2 = m_TextMeshPro.rectTransform;
				rectTransform2.pivot = vector;
				camera = m_camera;
				frameCounter_transform = m_frameCounter_transform;
				num = 100f;
				goto IL_024a;
			}
			m_TextMeshPro.alignment = TextAlignmentOptions.BottomLeft;
			rectTransform3 = m_TextMeshPro.rectTransform;
		}
		else
		{
			m_TextMeshPro.alignment = TextAlignmentOptions.TopLeft;
			rectTransform3 = m_TextMeshPro.rectTransform;
		}
		rectTransform3.pivot = vector;
		camera = m_camera;
		frameCounter_transform = m_frameCounter_transform;
		num = 100f;
		goto IL_024a;
		IL_021c:
		Vector3 position = (Vector3)(obj - 64);
		Vector3 vector3 = camera.ViewportToWorldPoint(position);
		Vector3 position2 = (Vector3)(obj - 64);
		_ = vector3.x;
		_ = vector3.z;
		frameCounter_transform.position = position2;
		return;
		IL_024a:
		vector2 = vector;
		goto IL_021c;
	}
}
