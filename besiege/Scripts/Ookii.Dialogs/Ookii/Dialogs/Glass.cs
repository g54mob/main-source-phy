using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	public static class Glass
	{
		public static bool OSSupportsDwmComposition
		{
			get
			{
				return NativeMethods.IsWindowsVistaOrLater;
			}
		}

		public static bool IsDwmCompositionEnabled
		{
			get
			{
				return OSSupportsDwmComposition && NativeMethods.DwmIsCompositionEnabled();
			}
		}

		public static void ExtendFrameIntoClientArea(this IWin32Window window, Padding glassMargin)
		{
			if (!IsDwmCompositionEnabled)
			{
				throw new NotSupportedException(Resources.GlassNotSupportedError);
			}
			if (window == null)
			{
				throw new ArgumentNullException("window");
			}
			NativeMethods.MARGINS pMarInset = new NativeMethods.MARGINS(glassMargin);
			NativeMethods.DwmExtendFrameIntoClientArea(window.Handle, ref pMarInset);
		}

		public static void DrawCompositedText(IDeviceContext dc, string text, Font font, Rectangle bounds, Padding padding, Color foreColor, int glowSize, TextFormatFlags textFormat)
		{
			if (!IsDwmCompositionEnabled)
			{
				throw new NotSupportedException(Resources.GlassNotSupportedError);
			}
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			IntPtr hdc = dc.GetHdc();
			try
			{
				using (SafeDeviceHandle safeDeviceHandle = NativeMethods.CreateCompatibleDC(hdc))
				{
					using (SafeGDIHandle hObject = new SafeGDIHandle(font.ToHfont(), true))
					{
						using (NativeMethods.CreateDib(bounds, hdc, safeDeviceHandle))
						{
							NativeMethods.SelectObject(safeDeviceHandle, hObject);
							VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
							NativeMethods.DTTOPTS pOptions = new NativeMethods.DTTOPTS
							{
								dwSize = Marshal.SizeOf(typeof(NativeMethods.DTTOPTS)),
								dwFlags = (NativeMethods.DrawThemeTextFlags.TextColor | NativeMethods.DrawThemeTextFlags.GlowSize | NativeMethods.DrawThemeTextFlags.Composited),
								crText = ColorTranslator.ToWin32(foreColor),
								iGlowSize = glowSize
							};
							NativeMethods.RECT pRect = new NativeMethods.RECT(padding.Left, padding.Top, bounds.Width - padding.Right, bounds.Height - padding.Bottom);
							NativeMethods.DrawThemeTextEx(visualStyleRenderer.Handle, safeDeviceHandle, 0, 0, text, text.Length, (int)textFormat, ref pRect, ref pOptions);
							NativeMethods.BitBlt(hdc, bounds.Left, bounds.Top, bounds.Width, bounds.Height, safeDeviceHandle, 0, 0, 13369376u);
						}
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
		}

		public static Size MeasureCompositedText(IDeviceContext dc, string text, Font font, TextFormatFlags textFormat)
		{
			if (!IsDwmCompositionEnabled)
			{
				throw new NotSupportedException(Resources.GlassNotSupportedError);
			}
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			IntPtr hdc = dc.GetHdc();
			try
			{
				Rectangle rectangle = new Rectangle(0, 0, int.MaxValue, int.MaxValue);
				using (SafeDeviceHandle safeDeviceHandle = NativeMethods.CreateCompatibleDC(hdc))
				{
					using (SafeGDIHandle hObject = new SafeGDIHandle(font.ToHfont(), true))
					{
						using (NativeMethods.CreateDib(rectangle, hdc, safeDeviceHandle))
						{
							NativeMethods.SelectObject(safeDeviceHandle, hObject);
							VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
							NativeMethods.RECT bounds = new NativeMethods.RECT(rectangle);
							NativeMethods.RECT rect;
							NativeMethods.GetThemeTextExtent(visualStyleRenderer.Handle, safeDeviceHandle, 0, 0, text, text.Length, (int)textFormat, ref bounds, out rect);
							return new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
						}
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
		}
	}
}
