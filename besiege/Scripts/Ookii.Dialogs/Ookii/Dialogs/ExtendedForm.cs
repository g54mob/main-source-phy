using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Permissions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Ookii.Dialogs
{
	public class ExtendedForm : Form
	{
		private bool _useSystemFont;

		private Padding _glassMargin;

		private bool _allowGlassDragging = true;

		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Indicates whether or not the form automatically uses the system default font.")]
		public bool UseSystemFont
		{
			get
			{
				return _useSystemFont;
			}
			set
			{
				_useSystemFont = value;
			}
		}

		[Category("Appearance")]
		[Description("The glass margins of the form.")]
		public Padding GlassMargin
		{
			get
			{
				return _glassMargin;
			}
			set
			{
				if (_glassMargin != value)
				{
					_glassMargin = value;
					EnableGlass();
				}
			}
		}

		[Category("Behavior")]
		[Description("Indicates whether the form can be dragged by the glass areas inside the client area.")]
		[DefaultValue(true)]
		public bool AllowGlassDragging
		{
			get
			{
				return _allowGlassDragging;
			}
			set
			{
				_allowGlassDragging = value;
			}
		}

		public event EventHandler DwmCompositionChanged;

		protected virtual void OnDwmCompositionChanged(EventArgs e)
		{
			EventHandler eventHandler = this.DwmCompositionChanged;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			if (!base.DesignMode && _useSystemFont)
			{
				Font = SystemFonts.IconTitleFont;
				SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
			}
			base.OnLoad(e);
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
			SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;
		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			if (base.DesignMode || Glass.IsDwmCompositionEnabled)
			{
				if (base.DesignMode)
				{
					using (HatchBrush brush = new HatchBrush(HatchStyle.OutlinedDiamond, Color.SkyBlue, BackColor))
					{
						PaintGlassArea(pevent, brush);
						return;
					}
				}
				PaintGlassArea(pevent, Brushes.Black);
			}
			else
			{
				base.OnPaintBackground(pevent);
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (_glassMargin.All != 0)
			{
				Invalidate();
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			EnableGlass();
			base.OnHandleCreated(e);
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			switch (m.Msg)
			{
			case 132:
			{
				if (!_allowGlassDragging || !(m.Result == new IntPtr(1)) || !Glass.IsDwmCompositionEnabled)
				{
					break;
				}
				if (_glassMargin.Left == -1 && _glassMargin.Top == -1 && _glassMargin.Right == -1 && _glassMargin.Bottom == -1)
				{
					m.Result = new IntPtr(2);
					break;
				}
				Point p = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16);
				p = PointToClient(p);
				if (p.X < _glassMargin.Left || p.X > base.ClientSize.Width - _glassMargin.Right || p.Y < _glassMargin.Top || p.Y > base.ClientSize.Height - _glassMargin.Bottom)
				{
					m.Result = new IntPtr(2);
				}
				break;
			}
			case 798:
				if (_glassMargin.All != 0)
				{
					EnableGlass();
				}
				OnDwmCompositionChanged(EventArgs.Empty);
				m.Result = IntPtr.Zero;
				break;
			}
		}

		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			float width = factor.Width;
			Padding glassMargin = GlassMargin;
			if (width != 1f)
			{
				if (glassMargin.Left > 0)
				{
					glassMargin.Left = (int)Math.Round((float)glassMargin.Left * width);
				}
				if (glassMargin.Right > 0)
				{
					glassMargin.Right = (int)Math.Round((float)glassMargin.Right * width);
				}
			}
			float height = factor.Height;
			if (height != 1f)
			{
				if (glassMargin.Top > 0)
				{
					glassMargin.Top = (int)Math.Round((float)glassMargin.Top * height);
				}
				if (glassMargin.Bottom > 0)
				{
					glassMargin.Bottom = (int)Math.Round((float)glassMargin.Bottom * height);
				}
			}
			GlassMargin = glassMargin;
			base.ScaleControl(factor, specified);
		}

		private void EnableGlass()
		{
			if (!base.DesignMode && Glass.IsDwmCompositionEnabled)
			{
				this.ExtendFrameIntoClientArea(GlassMargin);
				Invalidate();
			}
		}

		private void PaintGlassArea(PaintEventArgs pevent, Brush brush)
		{
			if (_glassMargin.Left == -1 && _glassMargin.Top == -1 && _glassMargin.Right == -1 && _glassMargin.Bottom == -1)
			{
				pevent.Graphics.FillRectangle(brush, pevent.ClipRectangle);
				return;
			}
			Rectangle rect = new Rectangle(_glassMargin.Left, _glassMargin.Top, base.ClientSize.Width - _glassMargin.Right, base.ClientSize.Height - _glassMargin.Bottom);
			pevent.Graphics.FillRectangle(new SolidBrush(BackColor), rect);
			if (_glassMargin.Left != 0)
			{
				pevent.Graphics.FillRectangle(brush, new Rectangle(0, 0, _glassMargin.Left, base.ClientSize.Height));
			}
			if (_glassMargin.Right != 0)
			{
				pevent.Graphics.FillRectangle(brush, new Rectangle(base.ClientSize.Width - _glassMargin.Right, 0, base.ClientSize.Width, base.ClientSize.Height));
			}
			if (_glassMargin.Top != 0)
			{
				pevent.Graphics.FillRectangle(brush, new Rectangle(0, 0, base.ClientSize.Width, _glassMargin.Top));
			}
			if (_glassMargin.Bottom != 0)
			{
				pevent.Graphics.FillRectangle(brush, new Rectangle(0, base.ClientSize.Height - _glassMargin.Bottom, base.ClientSize.Width, base.ClientSize.Height));
			}
		}

		private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			if (e.Category == UserPreferenceCategory.Window && _useSystemFont)
			{
				Font = SystemFonts.IconTitleFont;
			}
		}
	}
}
