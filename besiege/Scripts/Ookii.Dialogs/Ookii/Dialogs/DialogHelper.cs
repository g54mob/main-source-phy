using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Ookii.Dialogs
{
	internal static class DialogHelper
	{
		public static bool IsTaskDialogThemeSupported
		{
			get
			{
				return NativeMethods.IsWindowsVistaOrLater && VisualStyleRenderer.IsSupported && Application.RenderWithVisualStyles;
			}
		}

		public static int GetTextHeight(IDeviceContext dc, string mainInstruction, string content, Font mainInstructionFallbackFont, Font contentFallbackFont, int width)
		{
			Point location = Point.Empty;
			DrawText(dc, mainInstruction, content, ref location, mainInstructionFallbackFont, contentFallbackFont, true, width);
			return location.Y;
		}

		public static Size SizeDialog(IDeviceContext dc, string mainInstruction, string content, Screen screen, Font mainInstructionFallbackFont, Font contentFallbackFont, int horizontalSpacing, int verticalSpacing, int minimumWidth, int textMinimumHeight)
		{
			int num = minimumWidth - horizontalSpacing;
			int num2;
			for (num2 = GetTextHeight(dc, mainInstruction, content, mainInstructionFallbackFont, contentFallbackFont, num); num2 > num; num2 = GetTextHeight(dc, mainInstruction, content, mainInstructionFallbackFont, contentFallbackFont, num))
			{
				int num3 = num2 * num;
				num = (int)(Math.Sqrt(num3) * 1.1);
			}
			if (num2 < textMinimumHeight)
			{
				num2 = textMinimumHeight;
			}
			int num4 = num + horizontalSpacing;
			int num5 = num2 + verticalSpacing;
			Rectangle workingArea = screen.WorkingArea;
			if ((double)num5 > 0.9 * (double)workingArea.Height)
			{
				int num6 = num2 * num;
				num5 = (int)(0.9 * (double)workingArea.Height);
				num2 = num5 - verticalSpacing;
				num = num6 / num2;
				num4 = num + horizontalSpacing;
			}
			if ((double)num4 > 0.9 * (double)workingArea.Width)
			{
				num4 = (int)(0.9 * (double)workingArea.Width);
			}
			return new Size(num4, num5);
		}

		public static void DrawText(IDeviceContext dc, string text, VisualStyleElement element, Font fallbackFont, ref Point location, bool measureOnly, int width)
		{
			Rectangle bounds = new Rectangle(location.X, location.Y, width, NativeMethods.IsWindowsXPOrLater ? int.MaxValue : 100000);
			TextFormatFlags flags = TextFormatFlags.WordBreak;
			if (IsTaskDialogThemeSupported)
			{
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(element);
				Rectangle textExtent = visualStyleRenderer.GetTextExtent(dc, bounds, text, flags);
				location += new Size(0, textExtent.Height);
				if (!measureOnly)
				{
					visualStyleRenderer.DrawText(dc, textExtent, text, false, flags);
				}
			}
			else
			{
				if (!measureOnly)
				{
					TextRenderer.DrawText(dc, text, fallbackFont, bounds, SystemColors.WindowText, flags);
				}
				location += new Size(0, TextRenderer.MeasureText(dc, text, fallbackFont, new Size(bounds.Width, bounds.Height), flags).Height);
			}
		}

		public static void DrawText(IDeviceContext dc, string mainInstruction, string content, ref Point location, Font mainInstructionFallbackFont, Font contentFallbackFont, bool measureOnly, int width)
		{
			if (!string.IsNullOrEmpty(mainInstruction))
			{
				DrawText(dc, mainInstruction, AdditionalVisualStyleElements.TextStyle.MainInstruction, mainInstructionFallbackFont, ref location, measureOnly, width);
			}
			if (!string.IsNullOrEmpty(content))
			{
				if (!string.IsNullOrEmpty(mainInstruction))
				{
					content = Environment.NewLine + content;
				}
				DrawText(dc, content, AdditionalVisualStyleElements.TextStyle.BodyText, contentFallbackFont, ref location, measureOnly, width);
			}
		}
	}
}
