using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Localisation
{
	public class LocalisationChild : MonoBehaviour
	{
		private enum TextType
		{
			None = 0,
			TextMesh = 1,
			DynamicText = 2,
			SimpleText = 3
		}

		public int translationID;

		public bool clampWidth;

		public float maxWidth = -1f;

		public bool toUpperCase;

		protected string fileText;

		protected string localisationFilePath;

		protected TextMesh textMesh;

		protected DynamicText dynamicText;

		protected Text simpleText;

		private float initialScaleX;

		private float initialWidth;

		private TextType textType;

		private float CurrentWidth
		{
			get
			{
				Renderer component = GetComponent<Renderer>();
				if (component != null)
				{
					return component.bounds.size.x;
				}
				return 0f;
			}
		}

		protected virtual void Awake()
		{
			InitText();
			if (clampWidth && textType != TextType.None)
			{
				initialWidth = CurrentWidth;
				initialScaleX = base.transform.localScale.x;
				if (maxWidth == -1f)
				{
					maxWidth = initialWidth;
				}
			}
			Recaption();
		}

		public virtual void Recaption()
		{
			string text = LocalisationManager.GetTranslation(translationID);
			if (string.IsNullOrEmpty(text) || textType == TextType.None)
			{
				return;
			}
			if (toUpperCase)
			{
				text = text.ToUpper();
			}
			if (clampWidth)
			{
				Vector3 localScale = base.transform.localScale;
				base.transform.localScale = new Vector3(initialScaleX, localScale.y, localScale.z);
			}
			switch (textType)
			{
			case TextType.TextMesh:
				textMesh.text = text;
				break;
			case TextType.DynamicText:
				ReferenceMaster.SetDynamicText(dynamicText, text);
				break;
			case TextType.SimpleText:
				simpleText.text = text;
				break;
			}
			if (clampWidth)
			{
				float currentWidth = CurrentWidth;
				if (currentWidth > maxWidth)
				{
					Vector3 localScale2 = base.transform.localScale;
					base.transform.localScale = new Vector3(maxWidth / currentWidth * initialScaleX, localScale2.y, localScale2.z);
				}
			}
		}

		protected void InitText()
		{
			textType = TextType.None;
			textMesh = GetComponent<TextMesh>();
			if (textMesh != null)
			{
				textType = TextType.TextMesh;
				return;
			}
			dynamicText = GetComponent<DynamicText>();
			if (dynamicText != null)
			{
				textType = TextType.DynamicText;
				return;
			}
			simpleText = GetComponent<Text>();
			if (simpleText != null)
			{
				textType = TextType.SimpleText;
			}
		}

		protected string GetText()
		{
			switch (textType)
			{
			case TextType.TextMesh:
				return textMesh.text;
			case TextType.DynamicText:
				return dynamicText.GetText();
			case TextType.SimpleText:
				return simpleText.text;
			default:
				return null;
			}
		}

		public virtual int GenerateLocalisationEntry()
		{
			return GenerateLocalisationEntry(true);
		}

		public virtual int GenerateLocalisationEntry(bool saveTranslationFile)
		{
			if (textType == TextType.None)
			{
				InitText();
			}
			string text = GetText();
			text = Regex.Replace(text, "[ ]?[\\r]?\\n", "\\n", RegexOptions.Multiline);
			TranslationFile currentTranslationFile = SingleInstance<LocalisationManager>.Instance.CurrentTranslationFile;
			int num = currentTranslationFile.GetTranslationID(text);
			if (num != -1)
			{
				translationID = num;
			}
			else
			{
				int num2 = currentTranslationFile.TranslationIds.Max();
				translationID = num2 + 1;
				CreateTextEntry(text);
			}
			return translationID;
		}

		protected virtual void CreateTextEntry(string localisationText)
		{
			LocalisationManager instance = SingleInstance<LocalisationManager>.Instance;
			TranslationEntry translationEntry = new TranslationEntry();
			translationEntry.Comment = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(localisationText.ToLower());
			translationEntry.Translation = localisationText;
			translationEntry.TranslationID = translationID;
			TranslationEntry newTranslationEntry = translationEntry;
			instance.CurrentTranslationFile.AddTranslation(newTranslationEntry);
			instance.CurrentTranslationFile.Save(string.Empty);
		}
	}
}
