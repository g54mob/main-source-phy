using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(Text))]
	public class TextOverflowEllipses : MonoBehaviour
	{
		private string m_lastText = string.Empty;

		private Text m_text;

		private void Start()
		{
			m_text = GetComponent<Text>();
		}

		private void OnGUI()
		{
			if (!(m_text.text != m_lastText))
			{
				return;
			}
			string text = m_text.text;
			int characterCountVisible = m_text.cachedTextGenerator.characterCountVisible;
			float width = GetComponent<RectTransform>().rect.width;
			if (characterCountVisible < text.Length || width < m_text.preferredWidth)
			{
				Font font = m_text.font;
				font.GetCharacterInfo('.', out var info, m_text.fontSize, m_text.fontStyle);
				int num = 3 * info.advance;
				for (int i = 0; i < text.Length; i++)
				{
					font.GetCharacterInfo(text[i], out info, m_text.fontSize, m_text.fontStyle);
					int advance = info.advance;
					if ((float)(num + advance) > width)
					{
						if (i > 0)
						{
							i--;
						}
						text = text.Substring(0, i) + "...";
						break;
					}
					num += advance;
				}
				m_text.text = text;
			}
			m_lastText = text;
		}
	}
}
