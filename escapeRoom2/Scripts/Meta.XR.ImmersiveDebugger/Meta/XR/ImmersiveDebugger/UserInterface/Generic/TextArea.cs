using UnityEngine;
using UnityEngine.UI;

namespace Meta.XR.ImmersiveDebugger.UserInterface.Generic
{
	public class TextArea : Value
	{
		private Text Text => base.Label.Text;

		internal override string Content
		{
			get
			{
				return Text.text;
			}
			set
			{
				Text.text = value;
				UpdateLayoutSize();
			}
		}

		internal float TextAreaHeight => CalculateHeight(base.LayoutStyle.size.x);

		protected override void Setup(Controller owner)
		{
			base.Setup(owner);
			Text.horizontalOverflow = HorizontalWrapMode.Wrap;
			Text.verticalOverflow = VerticalWrapMode.Overflow;
			Text.text = "";
		}

		protected override void OnTransparencyChanged()
		{
			base.OnTransparencyChanged();
			Text.color = (base.Transparent ? Color.white : base.TextStyle.color);
		}

		internal void UpdateLayoutSize()
		{
			base.LayoutStyle.size.y = TextAreaHeight + base.Owner.LayoutStyle.spacing + base.Label.LayoutStyle.margin.y * 2f;
			RefreshLayout();
		}

		private float CalculateHeight(float textWidth)
		{
			TextGenerationSettings settings = new TextGenerationSettings
			{
				generationExtents = new Vector2(textWidth, 0f),
				fontSize = Text.fontSize,
				textAnchor = Text.alignment,
				alignByGeometry = Text.alignByGeometry,
				scaleFactor = Text.pixelsPerUnit,
				color = Text.color,
				font = Text.font,
				pivot = base.RectTransform.pivot,
				richText = false,
				lineSpacing = Text.lineSpacing,
				fontStyle = Text.fontStyle,
				resizeTextForBestFit = false,
				updateBounds = true,
				horizontalOverflow = Text.horizontalOverflow,
				verticalOverflow = Text.verticalOverflow
			};
			TextGenerator textGenerator = new TextGenerator();
			textGenerator.Populate(Text.text, settings);
			return textGenerator.rectExtents.height / Text.pixelsPerUnit;
		}
	}
}
