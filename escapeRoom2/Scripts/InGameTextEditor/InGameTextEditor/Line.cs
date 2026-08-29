using System.Collections.Generic;
using InGameTextEditor.Format;
using UnityEngine;
using UnityEngine.UI;

namespace InGameTextEditor
{
	public class Line
	{
		public class Label
		{
			public enum DeleteCondition
			{
				NONE = 0,
				THIS_LINE_CHANGES = 1,
				PREVIOUS_LINE_CHANGES = 2,
				ANYTHING_CHANGES = 3
			}

			public readonly int startIndex;

			public readonly int endIndex;

			public readonly Sprite backgroundSprite;

			public readonly Color backgroundColor;

			public readonly Sprite iconSprite;

			public readonly Color iconColor;

			public readonly string tooltipMessage;

			public readonly DeleteCondition deleteCondition;

			public readonly List<GameObject> labelRects = new List<GameObject>();

			public GameObject icon;

			public Label(int startIndex, int endIndex, Sprite backgroundSprite, Color backgroundColor, Sprite iconSprite, Color iconColor, string tooltipMessage, DeleteCondition deleteCondition)
			{
				this.startIndex = startIndex;
				this.endIndex = endIndex;
				this.backgroundSprite = backgroundSprite;
				this.backgroundColor = backgroundColor;
				this.iconSprite = iconSprite;
				this.iconColor = iconColor;
				this.tooltipMessage = tooltipMessage;
				this.deleteCondition = deleteCondition;
			}

			public void SetVisible(bool visible)
			{
				foreach (GameObject labelRect in labelRects)
				{
					labelRect.SetActive(visible);
				}
				if (icon != null)
				{
					icon.SetActive(visible);
				}
			}

			public void Destroy()
			{
				foreach (GameObject labelRect in labelRects)
				{
					labelRect.SetActive(value: false);
					Object.Destroy(labelRect);
				}
				if (icon != null)
				{
					Object.Destroy(icon);
				}
			}
		}

		private class TextBlock
		{
			public readonly Line line;

			public readonly int startIndex;

			public readonly int endIndex;

			public readonly Vector2 offset;

			public readonly string text;

			public GameObject gameObject;

			public TextBlock(Line line, int startIndex, int endIndex, Vector2 offset, string text)
			{
				this.line = line;
				this.startIndex = startIndex;
				this.endIndex = endIndex;
				this.offset = offset;
				this.text = text;
			}

			public void SetVisible(bool visible)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(visible);
				}
				else if (visible)
				{
					gameObject = line.CreateTextBlockGameObject(this);
					gameObject.SetActive(value: true);
				}
			}

			public void Destroy()
			{
				if (gameObject != null)
				{
					gameObject.SetActive(value: false);
					Object.Destroy(gameObject);
				}
			}
		}

		private readonly int maxCharactersPerBlock = 10000;

		private int lineNumber;

		private string text;

		private float verticalOffset;

		private TextEditor textEditor;

		private List<TextBlock> textBlocks = new List<TextBlock>();

		private GameObject lineNumberGameObject;

		private List<Label> labels = new List<Label>();

		private List<string> lineFragments = new List<string>();

		private int lineIndent;

		private int wrappedLineIndent;

		private List<float> characterWidth = new List<float>();

		private List<Vector2> characterOffset = new List<Vector2>();

		private List<int> indexMap = new List<int>();

		private List<int> reverseStartIndexMap = new List<int>();

		private List<int> reverseEndIndexMap = new List<int>();

		private float lineWidth;

		private List<TextFormatGroup> textFormat = new List<TextFormatGroup>();

		private List<TextFormatGroup> adjustedTextFormat = new List<TextFormatGroup>();

		private bool visible;

		private Dictionary<string, object> properties = new Dictionary<string, object>();

		public int LineNumber
		{
			get
			{
				return lineNumber;
			}
			set
			{
				if (value != lineNumber)
				{
					lineNumber = value;
					if (lineNumberGameObject != null)
					{
						lineNumberGameObject.GetComponent<Text>().text = (lineNumber + 1).ToString();
					}
				}
			}
		}

		public List<Label> Labels => labels;

		public string Text
		{
			get
			{
				return text;
			}
			set
			{
				bool thisLineChanged = !text.Equals(value);
				text = value;
				textFormat.Clear();
				CreateTextBlocks();
				if (lineNumberGameObject != null)
				{
					Object.Destroy(lineNumberGameObject);
					if (visible)
					{
						lineNumberGameObject = CreateLineNumberGameObject();
						lineNumberGameObject.SetActive(value: true);
					}
					else
					{
						lineNumberGameObject = null;
					}
				}
				RedoLabels(thisLineChanged, previousLineChanged: false, nextLineChanged: false);
				textEditor.OnLineChanged(this);
			}
		}

		public float VerticalOffset
		{
			get
			{
				return verticalOffset;
			}
			set
			{
				if (Mathf.Approximately(verticalOffset, value))
				{
					return;
				}
				float num = verticalOffset;
				verticalOffset = value;
				if (lineNumberGameObject != null)
				{
					lineNumberGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(lineNumberGameObject.GetComponent<RectTransform>().anchoredPosition.x, 0f - textEditor.mainMarginTop + verticalOffset);
				}
				foreach (TextBlock textBlock in textBlocks)
				{
					if (textBlock.gameObject != null)
					{
						Vector2 offset = textBlock.offset;
						textBlock.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(textBlock.gameObject.GetComponent<RectTransform>().anchoredPosition.x, 0f - textEditor.mainMarginTop + verticalOffset + offset.y);
					}
				}
				foreach (Label label in labels)
				{
					foreach (GameObject labelRect in label.labelRects)
					{
						Vector2 anchoredPosition = labelRect.GetComponent<RectTransform>().anchoredPosition;
						labelRect.GetComponent<RectTransform>().anchoredPosition = new Vector2(anchoredPosition.x, anchoredPosition.y - num + verticalOffset);
					}
				}
			}
		}

		public float Width => lineWidth;

		public float Height => (float)lineFragments.Count * textEditor.CharacterHeight;

		public bool Visible
		{
			get
			{
				return visible;
			}
			set
			{
				if (value == visible)
				{
					return;
				}
				visible = value;
				foreach (TextBlock textBlock in textBlocks)
				{
					textBlock.SetVisible(visible);
				}
				foreach (Label label in labels)
				{
					label.SetVisible(visible);
				}
				if (lineNumberGameObject != null)
				{
					lineNumberGameObject.SetActive(visible);
				}
				else if (visible)
				{
					lineNumberGameObject = CreateLineNumberGameObject();
					lineNumberGameObject.SetActive(value: true);
				}
			}
		}

		public Line PreviousLine
		{
			get
			{
				if (lineNumber > 0)
				{
					return textEditor.Lines[lineNumber - 1];
				}
				return null;
			}
		}

		public Line NextLine
		{
			get
			{
				if (lineNumber < textEditor.Lines.Count - 1)
				{
					return textEditor.Lines[lineNumber + 1];
				}
				return null;
			}
		}

		public int LineIndent => lineIndent;

		public Line(int lineNumber, string text, float verticalOffset, TextEditor textEditor)
		{
			this.lineNumber = lineNumber;
			this.text = text;
			this.verticalOffset = verticalOffset;
			this.textEditor = textEditor;
			CreateTextBlocks();
		}

		public void SetProperty<T>(string name, T value)
		{
			if (properties.ContainsKey(name))
			{
				properties[name] = value;
			}
			else
			{
				properties.Add(name, value);
			}
		}

		public T GetProperty<T>(string name, T defaultValue)
		{
			if (properties.TryGetValue(name, out var value) && value is T)
			{
				return (T)value;
			}
			return defaultValue;
		}

		public TextPosition GetTextPosition(Vector2 coordinates)
		{
			TextPosition textPosition = new TextPosition(lineNumber, 0);
			if (lineFragments.Count == 1)
			{
				textPosition.colIndex = -1;
				for (int i = 0; i < text.Length; i++)
				{
					_ = textEditor.CharacterWidth;
					if (coordinates.x <= characterOffset[i].x + characterWidth[i] / 2f)
					{
						textPosition.colIndex = i;
						break;
					}
				}
				if (textPosition.colIndex == -1)
				{
					textPosition.colIndex = text.Length;
				}
			}
			else
			{
				int num = -1;
				float num2 = verticalOffset;
				for (int j = 0; j < lineFragments.Count; j++)
				{
					float characterHeight = textEditor.CharacterHeight;
					if (coordinates.y >= num2 - characterHeight)
					{
						num = j;
						break;
					}
					num2 -= characterHeight;
				}
				if (num == -1)
				{
					num = lineFragments.Count - 1;
				}
				int num3 = 0;
				for (int k = 0; k < num; k++)
				{
					num3 += lineFragments[k].Length;
				}
				textPosition.colIndex = -1;
				for (int l = num3; l < num3 + lineFragments[num].Length; l++)
				{
					if (coordinates.x <= characterOffset[l].x + characterWidth[l] / 2f)
					{
						textPosition.colIndex = l;
						break;
					}
				}
				if (textPosition.colIndex == -1)
				{
					textPosition.colIndex = num3 + lineFragments[num].Length;
				}
				if (textPosition.colIndex == num3 && num > 0)
				{
					textPosition.preferNextLine = true;
				}
			}
			return textPosition;
		}

		public TextPosition FindWordStart(TextPosition textPosition, bool forward)
		{
			TextPosition textPosition2 = null;
			if (forward)
			{
				int num = -1;
				for (int i = Mathf.Min(textPosition.colIndex, text.Length - 1); i < text.Length - 1; i++)
				{
					if (textEditor.WordDelimiters.Contains(text[i]) && !textEditor.WordDelimiters.Contains(text[i + 1]))
					{
						num = i + 1;
						break;
					}
				}
				if (num == -1)
				{
					num = text.Length;
				}
				return new TextPosition(lineNumber, num);
			}
			int num2 = -1;
			for (int num3 = Mathf.Min(textPosition.colIndex, text.Length - 1); num3 > 0; num3--)
			{
				if (!textEditor.WordDelimiters.Contains(text[num3]) && textEditor.WordDelimiters.Contains(text[num3 - 1]))
				{
					num2 = num3;
					break;
				}
			}
			if (num2 == -1)
			{
				num2 = 0;
			}
			return new TextPosition(lineNumber, num2, preferNextLine: true);
		}

		public TextPosition FindWordEnd(TextPosition textPosition, bool forward)
		{
			TextPosition textPosition2 = null;
			if (forward)
			{
				int num = -1;
				for (int i = Mathf.Min(textPosition.colIndex, text.Length - 1); i < text.Length - 1; i++)
				{
					if (!textEditor.WordDelimiters.Contains(text[i]) && textEditor.WordDelimiters.Contains(text[i + 1]))
					{
						num = i + 1;
						break;
					}
				}
				if (num == -1)
				{
					num = text.Length;
				}
				return new TextPosition(lineNumber, num);
			}
			int num2 = -1;
			for (int num3 = Mathf.Min(textPosition.colIndex, text.Length - 1); num3 > 0; num3--)
			{
				if (textEditor.WordDelimiters.Contains(text[num3]) && !textEditor.WordDelimiters.Contains(text[num3 - 1]))
				{
					num2 = num3;
					break;
				}
			}
			if (num2 == -1)
			{
				num2 = 0;
			}
			return new TextPosition(lineNumber, num2, preferNextLine: true);
		}

		public TextPosition FindWordStartOrEnd(TextPosition textPosition, bool forward)
		{
			TextPosition textPosition2 = FindWordStart(textPosition, forward);
			TextPosition textPosition3 = FindWordEnd(textPosition, forward);
			if (textPosition2.colIndex <= textPosition3.colIndex)
			{
				if (!forward)
				{
					return textPosition3;
				}
				return textPosition2;
			}
			if (!forward)
			{
				return textPosition2;
			}
			return textPosition3;
		}

		public Vector2 GetCaretPosition(TextPosition textPosition)
		{
			if (textPosition.lineIndex != lineNumber)
			{
				throw new UnityException("The given text position does not belong to this line.");
			}
			if (textPosition.colIndex < 0 || textPosition.colIndex > text.Length)
			{
				throw new UnityException("Invalid col index: " + textPosition.colIndex);
			}
			if (textPosition.colIndex == 0)
			{
				return new Vector2(0f, verticalOffset);
			}
			if (textPosition.colIndex == text.Length)
			{
				return new Vector2(characterOffset[textPosition.colIndex - 1].x + characterWidth[textPosition.colIndex - 1], verticalOffset + characterOffset[textPosition.colIndex - 1].y);
			}
			if (lineFragments.Count == 1)
			{
				return new Vector2(characterOffset[textPosition.colIndex].x, verticalOffset + characterOffset[textPosition.colIndex].y);
			}
			int i = 0;
			int num;
			for (num = textPosition.colIndex; num > lineFragments[i].Length; i++)
			{
				num -= lineFragments[i].Length;
			}
			if (num == lineFragments[i].Length && !textPosition.preferNextLine)
			{
				return new Vector2(characterOffset[textPosition.colIndex - 1].x + characterWidth[textPosition.colIndex - 1], verticalOffset + characterOffset[textPosition.colIndex - 1].y);
			}
			return new Vector2(characterOffset[textPosition.colIndex].x, verticalOffset + characterOffset[textPosition.colIndex].y);
		}

		public void AddLabel(int startColIndex, int endColIndex, Sprite backgroundSprite, Color backgroundColor, Sprite iconSprite, Color iconColor, string tooltipMessage, Label.DeleteCondition deleteCondition)
		{
			int colIndex = Mathf.Clamp(startColIndex, 0, text.Length);
			int colIndex2 = Mathf.Clamp(endColIndex, 0, text.Length);
			Label label = new Label(startColIndex, endColIndex, backgroundSprite, backgroundColor, iconSprite, iconColor, tooltipMessage, deleteCondition);
			labels.Add(label);
			if (startColIndex < endColIndex)
			{
				Vector2 caretPosition = GetCaretPosition(new TextPosition(lineNumber, colIndex, preferNextLine: true));
				Vector2 caretPosition2 = GetCaretPosition(new TextPosition(lineNumber, colIndex2));
				if (1 + Mathf.RoundToInt((caretPosition2.y - caretPosition.y) / textEditor.CharacterHeight) == 1)
				{
					GameObject item = AddLabelRect(caretPosition, caretPosition2 - new Vector2(0f, textEditor.CharacterHeight / textEditor.lineSpacing), backgroundSprite, backgroundColor);
					label.labelRects.Add(item);
				}
				else
				{
					float x = Mathf.Max((float)Mathf.FloorToInt(textEditor.HorizontalSpaceAvailable / textEditor.CharacterWidth) * textEditor.CharacterWidth, textEditor.LongestLineWidth);
					GameObject item2 = AddLabelRect(caretPosition, new Vector2(x, caretPosition.y - textEditor.CharacterHeight / textEditor.lineSpacing), backgroundSprite, backgroundColor);
					label.labelRects.Add(item2);
					if (!Mathf.Approximately(caretPosition.y - textEditor.CharacterHeight, caretPosition2.y))
					{
						GameObject item3 = AddLabelRect(new Vector2(0f, caretPosition.y - textEditor.CharacterHeight / textEditor.lineSpacing), new Vector2(x, caretPosition2.y), backgroundSprite, backgroundColor);
						label.labelRects.Add(item3);
					}
					GameObject item4 = AddLabelRect(new Vector2(0f, caretPosition2.y), caretPosition2 - new Vector2(0f, textEditor.CharacterHeight / textEditor.lineSpacing), backgroundSprite, backgroundColor);
					label.labelRects.Add(item4);
				}
			}
			if (iconSprite != null)
			{
				label.icon = AddLabelIcon(iconSprite, iconColor, GetCaretPosition(new TextPosition(lineNumber, colIndex, preferNextLine: true)).y);
			}
		}

		public void RemoveLabels()
		{
			foreach (Label label in labels)
			{
				label.Destroy();
			}
			labels.Clear();
		}

		private void RedoLabels(bool thisLineChanged, bool previousLineChanged, bool nextLineChanged)
		{
			int count = labels.Count;
			for (int i = 0; i < count; i++)
			{
				Label label = labels[0];
				int startIndex = label.startIndex;
				int endIndex = label.endIndex;
				Sprite backgroundSprite = label.backgroundSprite;
				Color backgroundColor = label.backgroundColor;
				Sprite iconSprite = label.iconSprite;
				Color iconColor = label.iconColor;
				string tooltipMessage = label.tooltipMessage;
				Label.DeleteCondition deleteCondition = label.deleteCondition;
				if ((0u | ((thisLineChanged && deleteCondition != Label.DeleteCondition.NONE) ? 1u : 0u) | ((previousLineChanged && (deleteCondition == Label.DeleteCondition.PREVIOUS_LINE_CHANGES || deleteCondition == Label.DeleteCondition.ANYTHING_CHANGES)) ? 1u : 0u) | ((nextLineChanged && deleteCondition == Label.DeleteCondition.ANYTHING_CHANGES) ? 1u : 0u)) == 0)
				{
					AddLabel(startIndex, endIndex, backgroundSprite, backgroundColor, iconSprite, iconColor, tooltipMessage, deleteCondition);
				}
				label.Destroy();
				labels.RemoveAt(0);
			}
		}

		private GameObject AddLabelRect(Vector2 topLeft, Vector2 bottomRight, Sprite sprite, Color color)
		{
			GameObject gameObject = new GameObject("Label");
			gameObject.transform.SetParent(textEditor.labelContainer);
			gameObject.AddComponent<Image>();
			gameObject.GetComponent<Image>().color = color;
			if (sprite != null)
			{
				gameObject.GetComponent<Image>().sprite = sprite;
				gameObject.GetComponent<Image>().type = Image.Type.Tiled;
			}
			gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
			gameObject.GetComponent<RectTransform>().localRotation = Quaternion.identity;
			gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
			gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 1f);
			gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 1f);
			gameObject.GetComponent<RectTransform>().pivot = new Vector2(0f, 1f);
			gameObject.GetComponent<RectTransform>().pivot = new Vector2(0f, 1f);
			gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(textEditor.mainMarginLeft + topLeft.x, 0f - textEditor.mainMarginTop + topLeft.y);
			if (sprite != null)
			{
				float num = bottomRight.x - topLeft.x;
				float num2 = topLeft.y - bottomRight.y;
				float num3 = num2 / (float)sprite.texture.height;
				gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(num / num3, num2 / num3);
				gameObject.GetComponent<RectTransform>().localScale = new Vector3(num3, num3, 1f);
			}
			else
			{
				gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(bottomRight.x - topLeft.x, topLeft.y - bottomRight.y);
			}
			gameObject.transform.SetAsLastSibling();
			return gameObject;
		}

		private GameObject AddLabelIcon(Sprite iconSprite, Color iconColor, float positionY)
		{
			GameObject gameObject = new GameObject("Icon");
			gameObject.transform.SetParent(textEditor.lineLabelIconsContent);
			gameObject.AddComponent<Image>();
			gameObject.GetComponent<Image>().color = iconColor;
			gameObject.GetComponent<Image>().sprite = iconSprite;
			gameObject.GetComponent<Image>().type = Image.Type.Simple;
			gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
			gameObject.GetComponent<RectTransform>().localRotation = Quaternion.identity;
			gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
			gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 1f);
			gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 1f);
			gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
			gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f - textEditor.mainMarginTop + positionY);
			float num = textEditor.CharacterHeight / textEditor.lineSpacing / (float)iconSprite.texture.height;
			gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(iconSprite.texture.width, iconSprite.texture.height);
			gameObject.GetComponent<RectTransform>().localScale = new Vector3(num, num, 1f);
			gameObject.transform.SetAsFirstSibling();
			return gameObject;
		}

		public void ApplyTextFormat(List<TextFormatGroup> textFormatGroups)
		{
			textFormat.Clear();
			adjustedTextFormat.Clear();
			textFormat.AddRange(textFormatGroups);
			textFormat.Sort(TextFormatGroup.Sort);
			for (int i = 0; i < textFormat.Count; i++)
			{
				if (textFormat[i].startIndex < 0 || textFormat[i].endIndex < 0)
				{
					throw new UnityException("Invalid index");
				}
				if (i < textFormat.Count - 1 && textFormat[i].endIndex >= textFormat[i + 1].startIndex)
				{
					throw new UnityException("Overlapping text format groups");
				}
				int startIndex = ((textFormat[i].startIndex < text.Length) ? reverseStartIndexMap[textFormat[i].startIndex] : int.MaxValue);
				int endIndex = ((textFormat[i].endIndex < text.Length) ? reverseEndIndexMap[textFormat[i].endIndex] : int.MaxValue);
				adjustedTextFormat.Add(new TextFormatGroup(startIndex, endIndex, textFormat[i].textStyle));
			}
			CreateTextBlocks();
		}

		public void OnPreviousLineChanged()
		{
			if (labels.Count > 0)
			{
				RedoLabels(thisLineChanged: false, previousLineChanged: true, nextLineChanged: false);
			}
		}

		public void OnNextLineChanged()
		{
			if (labels.Count > 0)
			{
				RedoLabels(thisLineChanged: false, previousLineChanged: false, nextLineChanged: true);
			}
		}

		public void Destroy()
		{
			foreach (TextBlock textBlock in textBlocks)
			{
				textBlock.Destroy();
			}
			foreach (Label label in labels)
			{
				label.Destroy();
			}
			if (lineNumberGameObject != null)
			{
				lineNumberGameObject.SetActive(value: false);
				Object.Destroy(lineNumberGameObject);
			}
		}

		private void CreateTextBlocks()
		{
			foreach (TextBlock textBlock in textBlocks)
			{
				textBlock.Destroy();
			}
			textBlocks.Clear();
			lineFragments.Clear();
			characterWidth.Clear();
			characterOffset.Clear();
			indexMap.Clear();
			reverseStartIndexMap.Clear();
			reverseEndIndexMap.Clear();
			lineWidth = 0f;
			visible = false;
			lineIndent = 0;
			if (this.text.Length > 0)
			{
				if (this.text[0] == ' ')
				{
					for (int i = 0; i < this.text.Length && this.text[i] == ' '; i++)
					{
						lineIndent++;
					}
				}
				if (this.text[0] == '\t')
				{
					for (int j = 0; j < this.text.Length && this.text[j] == '\t'; j++)
					{
						lineIndent += textEditor.tabStopWidth;
					}
				}
			}
			wrappedLineIndent = lineIndent;
			if (wrappedLineIndent >= Mathf.FloorToInt(textEditor.HorizontalSpaceAvailable / textEditor.CharacterWidth) - textEditor.tabStopWidth)
			{
				wrappedLineIndent = 0;
			}
			float num = Mathf.Max(textEditor.HorizontalSpaceAvailable, (float)(textEditor.tabStopWidth + wrappedLineIndent + 1) * textEditor.CharacterWidth);
			if (this.text.Length > 0)
			{
				HashSet<int> hashSet = new HashSet<int>();
				for (int k = 0; k < this.text.Length; k++)
				{
					if (this.text[k] != ' ' && this.text[k] != '\t' && !Util.IsPrintableCharacter(this.text[k]))
					{
						throw new UnityException("Invalid character: (Unicode " + $"0x{(int)this.text[k]:X4}" + ")");
					}
					if (k > 0 && (this.text[k - 1] == ' ' || this.text[k - 1] == '\t') && this.text[k] != ' ' && this.text[k] != '\t')
					{
						hashSet.Add(k);
					}
					else if (k < this.text.Length - 1 && textEditor.WordDelimiters.Contains(this.text[k]) && this.text[k] != ' ' && this.text[k] != '\t' && !textEditor.WordDelimiters.Contains(this.text[k + 1]) && this.text[k + 1] != ' ' && this.text[k + 1] != '\t')
					{
						hashSet.Add(k);
					}
				}
				HashSet<int> hashSet2 = new HashSet<int>();
				if (textEditor.wrapLines)
				{
					string text = "";
					int num2 = -1;
					int num3 = 0;
					bool flag = true;
					for (int l = 0; l < this.text.Length; l++)
					{
						string text2 = Util.ReplaceTabsWithSpaces(this.text[l].ToString(), textEditor.tabStopWidth, text.Length + ((!flag) ? wrappedLineIndent : 0));
						if (hashSet.Contains(l))
						{
							num2 = l;
							if (text.Length > 0)
							{
								num3++;
							}
						}
						if ((float)(text.Length + text2.Length + ((!flag) ? wrappedLineIndent : 0)) * textEditor.CharacterWidth <= num)
						{
							text += text2;
						}
						else if (num3 > 0)
						{
							hashSet2.Add(num2);
							text = "";
							num3 = 0;
							flag = false;
							l = num2 - 1;
						}
						else
						{
							hashSet2.Add(l);
							text = "";
							num3 = 0;
							flag = false;
							l--;
						}
					}
				}
				string text3 = "";
				string text4 = "";
				string text5 = "";
				float num4 = 0f;
				for (int m = 0; m < this.text.Length; m++)
				{
					string text6 = Util.ReplaceTabsWithSpaces(this.text[m].ToString(), textEditor.tabStopWidth, text5.Length + ((lineFragments.Count != 0) ? wrappedLineIndent : 0));
					text4 += this.text[m];
					text5 += text6;
					reverseStartIndexMap.Add(indexMap.Count);
					for (int n = 0; n < text6.Length; n++)
					{
						indexMap.Add(m);
					}
					reverseEndIndexMap.Add(indexMap.Count - 1);
					characterWidth.Add((float)text6.Length * textEditor.CharacterWidth);
					characterOffset.Add(new Vector2(num4, (float)(-lineFragments.Count) * textEditor.CharacterHeight));
					num4 += (float)text6.Length * textEditor.CharacterWidth;
					if (m == this.text.Length - 1 || hashSet2.Contains(m + 1))
					{
						CreateTextBlocksForLine(text3.Length, text3.Length + text5.Length - 1, text5, new Vector2((float)((lineFragments.Count != 0) ? wrappedLineIndent : 0) * textEditor.CharacterWidth, (float)(-lineFragments.Count) * textEditor.CharacterHeight));
						lineWidth = Mathf.Max(lineWidth, (float)(text5.Length + ((lineFragments.Count != 0) ? wrappedLineIndent : 0)) * textEditor.CharacterWidth);
						text3 += text5;
						lineFragments.Add(text4);
						text4 = "";
						text5 = "";
						num4 = (float)wrappedLineIndent * textEditor.CharacterWidth;
					}
				}
			}
			else
			{
				lineFragments.Add("");
			}
		}

		private void CreateTextBlocksForLine(int startIndex, int endIndex, string lineText, Vector2 lineOffset)
		{
			if (lineText.Length <= maxCharactersPerBlock)
			{
				textBlocks.Add(new TextBlock(this, startIndex, endIndex, lineOffset, lineText));
				return;
			}
			string text = lineText;
			Vector2 offset = lineOffset;
			int num = startIndex;
			while (text.Length > 0)
			{
				string text2 = text.Substring(0, Mathf.Min(maxCharactersPerBlock, text.Length));
				textBlocks.Add(new TextBlock(this, num, num + text2.Length - 1, offset, text2));
				num += text2.Length;
				text = text.Substring(text2.Length);
				offset = new Vector2(offset.x + (float)text2.Length * textEditor.CharacterWidth, offset.y);
			}
		}

		private GameObject CreateTextBlockGameObject(TextBlock textBlock)
		{
			string text = textBlock.text.Replace('\t', ' ').Replace('<', '\u001b');
			string text2 = "";
			int num = 0;
			foreach (TextFormatGroup item in adjustedTextFormat)
			{
				if (item.startIndex < textBlock.startIndex && item.endIndex >= textBlock.startIndex)
				{
					text2 = item.textStyle.RichtTextOpenTag;
				}
				if (item.startIndex >= textBlock.startIndex && item.startIndex <= textBlock.endIndex)
				{
					text2 = text2 + text.Substring(num, item.startIndex - textBlock.startIndex - num) + item.textStyle.RichtTextOpenTag;
					num = item.startIndex - textBlock.startIndex;
				}
				if (item.endIndex >= textBlock.startIndex && item.endIndex <= textBlock.endIndex)
				{
					text2 = text2 + text.Substring(num, item.endIndex - textBlock.startIndex - num + 1) + item.textStyle.RichtTextCloseTag;
					num = item.endIndex - textBlock.startIndex + 1;
				}
				if (item.startIndex <= textBlock.endIndex && item.endIndex > textBlock.endIndex)
				{
					text2 = text2 + text.Substring(num, textBlock.endIndex - textBlock.startIndex - num + 1) + item.textStyle.RichtTextCloseTag;
					num = textBlock.endIndex + 1;
				}
			}
			if (num <= textBlock.endIndex)
			{
				text2 += text.Substring(num, textBlock.endIndex - textBlock.startIndex - num + 1);
			}
			text = text2.Replace("\u001b", "<\u001b");
			GameObject gameObject = new GameObject("Text");
			gameObject.transform.SetParent(textEditor.textContainer);
			gameObject.AddComponent<Text>();
			gameObject.GetComponent<Text>().font = textEditor.font;
			gameObject.GetComponent<Text>().fontSize = textEditor.fontSize;
			gameObject.GetComponent<Text>().lineSpacing = textEditor.lineSpacing;
			gameObject.GetComponent<Text>().fontStyle = textEditor.mainFontStyle;
			gameObject.GetComponent<Text>().color = textEditor.mainFontColor;
			gameObject.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
			gameObject.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
			gameObject.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
			gameObject.GetComponent<Text>().text = text;
			gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
			gameObject.GetComponent<RectTransform>().localRotation = Quaternion.identity;
			gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
			gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 1f);
			gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 1f);
			gameObject.GetComponent<RectTransform>().pivot = new Vector2(0f, 1f);
			gameObject.GetComponent<RectTransform>().pivot = new Vector2(0f, 1f);
			gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(textEditor.mainMarginLeft + textBlock.offset.x, 0f - textEditor.mainMarginTop + verticalOffset + textBlock.offset.y);
			gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 0f);
			gameObject.transform.SetAsLastSibling();
			return gameObject;
		}

		private GameObject CreateLineNumberGameObject()
		{
			GameObject gameObject = new GameObject("Line Number");
			gameObject.transform.SetParent(textEditor.lineNumberContent);
			gameObject.AddComponent<Text>();
			gameObject.GetComponent<Text>().font = textEditor.font;
			gameObject.GetComponent<Text>().fontSize = textEditor.fontSize;
			gameObject.GetComponent<Text>().lineSpacing = textEditor.lineSpacing;
			gameObject.GetComponent<Text>().fontStyle = textEditor.lineNumberFontStyle;
			gameObject.GetComponent<Text>().color = textEditor.lineNumberFontColor;
			gameObject.GetComponent<Text>().alignment = TextAnchor.UpperRight;
			gameObject.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
			gameObject.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
			gameObject.GetComponent<Text>().text = (lineNumber + 1).ToString();
			gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
			gameObject.GetComponent<RectTransform>().localRotation = Quaternion.identity;
			gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
			gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 1f);
			gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 1f);
			gameObject.GetComponent<RectTransform>().pivot = new Vector2(0f, 1f);
			gameObject.GetComponent<RectTransform>().pivot = new Vector2(0f, 1f);
			gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f - textEditor.lineNumberMarginRight, 0f - textEditor.mainMarginTop + verticalOffset);
			gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 0f);
			gameObject.transform.SetAsLastSibling();
			return gameObject;
		}
	}
}
