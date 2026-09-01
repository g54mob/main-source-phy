using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class TMPro_InstructionOverlay : MonoBehaviour
{
	public enum FpsCounterAnchorPositions
	{
		TopLeft,
		BottomLeft,
		TopRight,
		BottomRight
	}

	public FpsCounterAnchorPositions AnchorPosition = FpsCounterAnchorPositions.BottomLeft;

	private const string instructions = "Camera Control - <#ffff00>Shift + RMB\n</color>Zoom - <#ffff00>Mouse wheel.";

	private TextMeshPro m_TextMeshPro;

	private TextContainer m_textContainer;

	private Transform m_frameCounter_transform;

	private Camera m_camera;

	private unsafe void Awake()
	{
		//IL_0084: Expected O, but got Ref
		if (base.enabled)
		{
			Camera main = Camera.main;
			m_camera = main;
			GameObject gameObject = new GameObject("Frame Counter");
			Transform frameCounter_transform = gameObject.transform;
			m_frameCounter_transform = frameCounter_transform;
			Transform parent = m_camera.transform;
			m_frameCounter_transform.parent = parent;
			object obj = default(object);
			m_frameCounter_transform.localRotation = (Quaternion)(&obj);
			TextMeshPro textMeshPro = gameObject.AddComponent<TextMeshPro>();
			m_TextMeshPro = textMeshPro;
			TMP_FontAsset font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
			m_TextMeshPro.font = font;
			Material fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - Overlay");
			m_TextMeshPro.fontSharedMaterial = fontSharedMaterial;
			m_TextMeshPro.fontSize = 30f;
			m_TextMeshPro.isOverlay = true;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			TextContainer textContainer = default(TextContainer);
			m_textContainer = textContainer;
			Set_FrameCounter_Position(AnchorPosition);
			m_TextMeshPro.text = "Camera Control - <#ffff00>Shift + RMB\n</color>Zoom - <#ffff00>Mouse wheel.";
		}
	}

	private void Set_FrameCounter_Position(FpsCounterAnchorPositions anchor_position)
	{
		//IL_002b: Expected O, but got I4
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Expected O, but got Unknown
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		bool flag = anchor_position == FpsCounterAnchorPositions.TopLeft;
		Camera camera;
		Transform frameCounter_transform;
		float num;
		if (!flag)
		{
			object obj = anchor_position - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 != 1)
					{
						return;
					}
					m_textContainer.anchorPosition = TextContainerAnchors.BottomRight;
					camera = m_camera;
					frameCounter_transform = m_frameCounter_transform;
					num = 100f;
				}
				else
				{
					m_textContainer.anchorPosition = TextContainerAnchors.TopRight;
					camera = m_camera;
					frameCounter_transform = m_frameCounter_transform;
					num = 100f;
				}
				goto IL_0178;
			}
			m_textContainer.anchorPosition = TextContainerAnchors.BottomLeft;
			camera = m_camera;
			frameCounter_transform = m_frameCounter_transform;
		}
		else
		{
			m_textContainer.anchorPosition = TextContainerAnchors.TopLeft;
			camera = m_camera;
			frameCounter_transform = m_frameCounter_transform;
		}
		num = 100f;
		goto IL_0178;
		IL_0178:
		object obj3 = default(object);
		Vector3 position = (Vector3)(obj3 - 32);
		Vector3 vector = camera.ViewportToWorldPoint(position);
		Vector3 position2 = (Vector3)(obj3 - 32);
		_ = vector.x;
		_ = vector.z;
		frameCounter_transform.position = position2;
	}
}
