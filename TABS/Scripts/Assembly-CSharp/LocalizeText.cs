using TMPro;
using UnityEngine;

public class LocalizeText : MonoBehaviour
{
	[SerializeField]
	protected string m_localeID = "$none";

	[SerializeField]
	private int m_fontIndex;

	[SerializeField]
	private string[] m_args;

	[SerializeField]
	private string m_glyphText;

	[SerializeField]
	private bool m_glyphRightAligned;

	private bool m_localized = true;

	private TextMeshProUGUI m_text;

	public string LocaleID
	{
		get
		{
			return m_localeID;
		}
		set
		{
			m_localeID = value;
			UpdateText();
		}
	}

	public bool Localized
	{
		get
		{
			return m_localized;
		}
		set
		{
			m_localized = value;
		}
	}

	public TextMeshProUGUI Text => m_text;

	public string[] Args
	{
		get
		{
			return m_args;
		}
		set
		{
			m_args = value;
		}
	}

	public string GlyphText
	{
		get
		{
			return m_glyphText;
		}
		set
		{
			m_glyphText = value;
		}
	}

	public bool GlyphRightAligned
	{
		get
		{
			return m_glyphRightAligned;
		}
		set
		{
			m_glyphRightAligned = value;
		}
	}

	protected virtual void Awake()
	{
		m_text = GetComponent<TextMeshProUGUI>();
		if (m_text == null)
		{
			Object.Destroy(this);
		}
	}

	private void Start()
	{
		Localizer.RegisterCallback(this, UpdateText);
		UpdateText();
	}

	private void OnDestroy()
	{
		Localizer.UnregisterCallback(this);
	}

	private void UpdateText()
	{
		if (this == null)
		{
			return;
		}
		if (m_text == null)
		{
			m_text = GetComponent<TextMeshProUGUI>();
		}
		if (!m_localized)
		{
			m_text.text = m_localeID;
			return;
		}
		TMP_FontAsset currentFont = Localizer.GetCurrentFont(m_fontIndex);
		if (currentFont != null)
		{
			m_text.font = currentFont;
		}
		if (string.IsNullOrEmpty(m_localeID))
		{
			if (!string.IsNullOrEmpty(GlyphText))
			{
				m_text.text = GlyphText;
			}
			else
			{
				m_text.text = string.Empty;
			}
		}
		else if (!(m_localeID == "$none"))
		{
			string singlePhrase = Localizer.GetSinglePhrase(m_localeID, m_args);
			singlePhrase = (m_glyphRightAligned ? (singlePhrase + GlyphText) : (GlyphText + singlePhrase));
			m_text.text = singlePhrase;
		}
	}
}
