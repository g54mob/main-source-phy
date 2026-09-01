using System;
using UnityEngine;

public class GradientSky : MonoBehaviour
{
	public Color m_ColorTop;

	public Color m_ColorMid;

	public Color m_ColorBot;

	[Range(0.001f, 0.999f)]
	public float m_Middle;

	public const float MIN_MIDDLE = 0.001f;

	public const float MAX_MIDDLE = 0.999f;

	[NonSerialized]
	public string m_ThemeStubID;

	private MeshRenderer m_MeshRenderer;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private static string TOP_COLOR_SHADER_ID = "_ColorTop";

	private static string MID_COLOR_SHADER_ID = "_ColorMid";

	private static string BOT_COLOR_SHADER_ID = "_ColorBot";

	private static string MIDDLE_SHADER_ID = "_Middle";

	private Color m_LastTopColor;

	private Color m_LastMidColor;

	private Color m_LastBotColor;

	private float m_LastMiddle;

	private bool m_Dirty;

	private Animator m_Animator;

	public void Awake()
	{
		m_MeshRenderer = GetComponent<MeshRenderer>();
		m_Animator = GetComponent<Animator>();
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		m_Dirty = true;
	}

	public void OnEnable()
	{
		if (m_Animator != null)
		{
			m_Animator.Update(0f);
		}
		Refresh();
	}

	public void Update()
	{
		if (m_Animator != null)
		{
			if (DumpReplays.m_Dumping && m_Animator.enabled)
			{
				m_Animator.enabled = false;
			}
			else if (!DumpReplays.m_Dumping && !m_Animator.enabled)
			{
				m_Animator.enabled = true;
			}
		}
		Refresh();
	}

	private void Refresh()
	{
		ThemePreloadStub themePreloadStub = ((ThemeStubs.m_Instance != null) ? ThemeStubs.m_Instance.GetPreloadStubFromId(m_ThemeStubID) : null);
		if ((bool)themePreloadStub && themePreloadStub.m_ThemeSkyOverride != null)
		{
			m_ColorTop = themePreloadStub.m_ThemeSkyOverride.m_Top;
			m_ColorMid = themePreloadStub.m_ThemeSkyOverride.m_Middle;
			m_ColorBot = themePreloadStub.m_ThemeSkyOverride.m_Bottom;
			m_Middle = themePreloadStub.m_ThemeSkyOverride.m_MiddleOffset;
		}
		if (ColorsDifferent(m_ColorTop, m_LastTopColor))
		{
			m_MaterialPropertyBlock.SetColor(TOP_COLOR_SHADER_ID, m_ColorTop);
			CopyColor(ref m_LastTopColor, m_ColorTop);
			m_Dirty = true;
		}
		if (ColorsDifferent(m_ColorMid, m_LastMidColor))
		{
			m_MaterialPropertyBlock.SetColor(MID_COLOR_SHADER_ID, m_ColorMid);
			CopyColor(ref m_LastMidColor, m_ColorMid);
			m_Dirty = true;
		}
		if (ColorsDifferent(m_ColorBot, m_LastBotColor))
		{
			m_MaterialPropertyBlock.SetColor(BOT_COLOR_SHADER_ID, m_ColorBot);
			CopyColor(ref m_LastBotColor, m_ColorBot);
			m_Dirty = true;
		}
		if (m_Middle != m_LastMiddle)
		{
			m_MaterialPropertyBlock.SetFloat(MIDDLE_SHADER_ID, Mathf.Clamp(m_Middle, 0.001f, 0.999f));
			m_LastMiddle = m_Middle;
			m_Dirty = true;
		}
		if (m_Dirty)
		{
			m_MeshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}

	private void CopyColor(ref Color dest, Color src)
	{
		dest.r = src.r;
		dest.g = src.g;
		dest.b = src.b;
	}

	private bool ColorsDifferent(Color colorA, Color colorB)
	{
		if (!Mathf.Approximately(colorA.r, colorB.r))
		{
			return true;
		}
		if (!Mathf.Approximately(colorA.g, colorB.g))
		{
			return true;
		}
		if (!Mathf.Approximately(colorA.b, colorB.b))
		{
			return true;
		}
		return false;
	}
}
