using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	[DefaultProperty("MainInstruction")]
	[DefaultEvent("ButtonClicked")]
	[Description("Displays a task dialog.")]
	[Designer(typeof(TaskDialogDesigner))]
	public class TaskDialog : Component, IWin32Window
	{
		private TaskDialogItemCollection<TaskDialogButton> _buttons;

		private TaskDialogItemCollection<TaskDialogRadioButton> _radioButtons;

		private NativeMethods.TASKDIALOGCONFIG _config = default(NativeMethods.TASKDIALOGCONFIG);

		private TaskDialogIcon _mainIcon;

		private Icon _customMainIcon;

		private Icon _customFooterIcon;

		private TaskDialogIcon _footerIcon;

		private Dictionary<int, TaskDialogButton> _buttonsById;

		private Dictionary<int, TaskDialogRadioButton> _radioButtonsById;

		private IntPtr _handle;

		private int _progressBarMarqueeAnimationSpeed = 100;

		private int _progressBarMinimimum;

		private int _progressBarMaximum = 100;

		private int _progressBarValue;

		private ProgressBarState _progressBarState = ProgressBarState.Normal;

		private int _inEventHandler;

		private bool _updatePending;

		private object _tag;

		private Icon _windowIcon;

		private IContainer components = null;

		public static bool OSSupportsTaskDialogs
		{
			get
			{
				return NativeMethods.IsWindowsVistaOrLater;
			}
		}

		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Appearance")]
		[Description("A list of the buttons on the Task Dialog.")]
		public TaskDialogItemCollection<TaskDialogButton> Buttons
		{
			get
			{
				return _buttons ?? (_buttons = new TaskDialogItemCollection<TaskDialogButton>(this));
			}
		}

		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Appearance")]
		[Description("A list of the radio buttons on the Task Dialog.")]
		public TaskDialogItemCollection<TaskDialogRadioButton> RadioButtons
		{
			get
			{
				return _radioButtons ?? (_radioButtons = new TaskDialogItemCollection<TaskDialogRadioButton>(this));
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The window title of the task dialog.")]
		[DefaultValue("")]
		public string WindowTitle
		{
			get
			{
				return _config.pszWindowTitle ?? string.Empty;
			}
			set
			{
				_config.pszWindowTitle = (string.IsNullOrEmpty(value) ? null : value);
				UpdateDialog();
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The dialog's main instruction.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string MainInstruction
		{
			get
			{
				return _config.pszMainInstruction ?? string.Empty;
			}
			set
			{
				_config.pszMainInstruction = (string.IsNullOrEmpty(value) ? null : value);
				SetElementText(NativeMethods.TaskDialogElements.MainInstruction, MainInstruction);
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The dialog's primary content.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string Content
		{
			get
			{
				return _config.pszContent ?? string.Empty;
			}
			set
			{
				_config.pszContent = (string.IsNullOrEmpty(value) ? null : value);
				SetElementText(NativeMethods.TaskDialogElements.Content, Content);
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The icon to be used in the title bar of the dialog. Used only when the dialog is shown as a modeless dialog.")]
		[DefaultValue(null)]
		public Icon WindowIcon
		{
			get
			{
				if (IsDialogRunning)
				{
					IntPtr handle = NativeMethods.SendMessage(Handle, 127, new IntPtr(0), IntPtr.Zero);
					return Icon.FromHandle(handle);
				}
				return _windowIcon;
			}
			set
			{
				_windowIcon = value;
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The icon to display in the task dialog.")]
		[DefaultValue(TaskDialogIcon.Custom)]
		public TaskDialogIcon MainIcon
		{
			get
			{
				return _mainIcon;
			}
			set
			{
				if (_mainIcon != value)
				{
					_mainIcon = value;
					UpdateDialog();
				}
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("A custom icon to display in the dialog.")]
		[DefaultValue(null)]
		public Icon CustomMainIcon
		{
			get
			{
				return _customMainIcon;
			}
			set
			{
				if (_customMainIcon != value)
				{
					_customMainIcon = value;
					UpdateDialog();
				}
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The icon to display in the footer area of the task dialog.")]
		[DefaultValue(TaskDialogIcon.Custom)]
		public TaskDialogIcon FooterIcon
		{
			get
			{
				return _footerIcon;
			}
			set
			{
				if (_footerIcon != value)
				{
					_footerIcon = value;
					UpdateDialog();
				}
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("A custom icon to display in the footer area of the task dialog.")]
		[DefaultValue(null)]
		public Icon CustomFooterIcon
		{
			get
			{
				return _customFooterIcon;
			}
			set
			{
				if (_customFooterIcon != value)
				{
					_customFooterIcon = value;
					UpdateDialog();
				}
			}
		}

		[Category("Behavior")]
		[Description("Indicates whether custom buttons should be displayed as normal buttons or command links.")]
		[DefaultValue(TaskDialogButtonStyle.Standard)]
		public TaskDialogButtonStyle ButtonStyle
		{
			get
			{
				return GetFlag(NativeMethods.TaskDialogFlags.UseCommandLinksNoIcon) ? TaskDialogButtonStyle.CommandLinksNoIcon : (GetFlag(NativeMethods.TaskDialogFlags.UseCommandLinks) ? TaskDialogButtonStyle.CommandLinks : TaskDialogButtonStyle.Standard);
			}
			set
			{
				SetFlag(NativeMethods.TaskDialogFlags.UseCommandLinks, value == TaskDialogButtonStyle.CommandLinks);
				SetFlag(NativeMethods.TaskDialogFlags.UseCommandLinksNoIcon, value == TaskDialogButtonStyle.CommandLinksNoIcon);
				UpdateDialog();
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The label for the verification checkbox.")]
		[DefaultValue("")]
		public string VerificationText
		{
			get
			{
				return _config.pszVerificationText ?? string.Empty;
			}
			set
			{
				string text = (string.IsNullOrEmpty(value) ? null : value);
				if (_config.pszVerificationText != text)
				{
					_config.pszVerificationText = text;
					UpdateDialog();
				}
			}
		}

		[Category("Behavior")]
		[Description("Indicates whether the verification checkbox is checked ot not.")]
		[DefaultValue(false)]
		public bool IsVerificationChecked
		{
			get
			{
				return GetFlag(NativeMethods.TaskDialogFlags.VerificationFlagChecked);
			}
			set
			{
				if (value != IsVerificationChecked)
				{
					SetFlag(NativeMethods.TaskDialogFlags.VerificationFlagChecked, value);
					if (IsDialogRunning)
					{
						ClickVerification(value, false);
					}
				}
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("Additional information to be displayed on the dialog.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string ExpandedInformation
		{
			get
			{
				return _config.pszExpandedInformation ?? string.Empty;
			}
			set
			{
				_config.pszExpandedInformation = (string.IsNullOrEmpty(value) ? null : value);
				SetElementText(NativeMethods.TaskDialogElements.ExpandedInformation, ExpandedInformation);
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text to use for the control for collapsing the expandable information.")]
		[DefaultValue("")]
		public string ExpandedControlText
		{
			get
			{
				return _config.pszExpandedControlText ?? string.Empty;
			}
			set
			{
				string text = (string.IsNullOrEmpty(value) ? null : value);
				if (_config.pszExpandedControlText != text)
				{
					_config.pszExpandedControlText = text;
					UpdateDialog();
				}
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text to use for the control for expanding the expandable information.")]
		[DefaultValue("")]
		public string CollapsedControlText
		{
			get
			{
				return _config.pszCollapsedControlText ?? string.Empty;
			}
			set
			{
				string text = (string.IsNullOrEmpty(value) ? null : value);
				if (_config.pszCollapsedControlText != text)
				{
					_config.pszCollapsedControlText = (string.IsNullOrEmpty(value) ? null : value);
					UpdateDialog();
				}
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text to be used in the footer area of the task dialog.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string Footer
		{
			get
			{
				return _config.pszFooterText ?? string.Empty;
			}
			set
			{
				_config.pszFooterText = (string.IsNullOrEmpty(value) ? null : value);
				SetElementText(NativeMethods.TaskDialogElements.Footer, Footer);
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("the width of the task dialog's client area in DLU's. If 0, task dialog will calculate the ideal width.")]
		[DefaultValue(0)]
		public int Width
		{
			get
			{
				return (int)_config.cxWidth;
			}
			set
			{
				if (_config.cxWidth != (uint)value)
				{
					_config.cxWidth = (uint)value;
					UpdateDialog();
				}
			}
		}

		[Category("Behavior")]
		[Description("Indicates whether hyperlinks are allowed for the Content, ExpandedInformation and Footer properties.")]
		[DefaultValue(false)]
		public bool EnableHyperlinks
		{
			get
			{
				return GetFlag(NativeMethods.TaskDialogFlags.EnableHyperLinks);
			}
			set
			{
				if (EnableHyperlinks != value)
				{
					SetFlag(NativeMethods.TaskDialogFlags.EnableHyperLinks, value);
					UpdateDialog();
				}
			}
		}

		[Category("Behavior")]
		[Description("Indicates that the dialog should be able to be closed using Alt-F4, Escape and the title bar's close button even if no cancel button is specified.")]
		[DefaultValue(false)]
		public bool AllowDialogCancellation
		{
			get
			{
				return GetFlag(NativeMethods.TaskDialogFlags.AllowDialogCancellation);
			}
			set
			{
				if (AllowDialogCancellation != value)
				{
					SetFlag(NativeMethods.TaskDialogFlags.AllowDialogCancellation, value);
					UpdateDialog();
				}
			}
		}

		[Category("Behavior")]
		[Description("Indicates that the string specified by the ExpandedInformation property should be displayed at the bottom of the dialog's footer area instead of immediately after the dialog's content.")]
		[DefaultValue(false)]
		public bool ExpandFooterArea
		{
			get
			{
				return GetFlag(NativeMethods.TaskDialogFlags.ExpandFooterArea);
			}
			set
			{
				if (ExpandFooterArea != value)
				{
					SetFlag(NativeMethods.TaskDialogFlags.ExpandFooterArea, value);
					UpdateDialog();
				}
			}
		}

		[Category("Behavior")]
		[Description("Indicates that the string specified by the ExpandedInformation property should be displayed by default.")]
		[DefaultValue(false)]
		public bool ExpandedByDefault
		{
			get
			{
				return GetFlag(NativeMethods.TaskDialogFlags.ExpandedByDefault);
			}
			set
			{
				if (ExpandedByDefault != value)
				{
					SetFlag(NativeMethods.TaskDialogFlags.ExpandedByDefault, value);
					UpdateDialog();
				}
			}
		}

		[Category("Behavior")]
		[Description("Indicates whether the Timer event is raised periodically while the dialog is visible.")]
		[DefaultValue(false)]
		public bool RaiseTimerEvent
		{
			get
			{
				return GetFlag(NativeMethods.TaskDialogFlags.CallbackTimer);
			}
			set
			{
				if (RaiseTimerEvent != value)
				{
					SetFlag(NativeMethods.TaskDialogFlags.CallbackTimer, value);
					UpdateDialog();
				}
			}
		}

		[Category("Layout")]
		[Description("Indicates whether the dialog is centered in the parent window instead of the screen.")]
		[DefaultValue(false)]
		public bool CenterParent
		{
			get
			{
				return GetFlag(NativeMethods.TaskDialogFlags.PositionRelativeToWindow);
			}
			set
			{
				if (CenterParent != value)
				{
					SetFlag(NativeMethods.TaskDialogFlags.PositionRelativeToWindow, value);
					UpdateDialog();
				}
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("Indicates whether text is displayed right to left.")]
		[DefaultValue(false)]
		public bool RightToLeft
		{
			get
			{
				return GetFlag(NativeMethods.TaskDialogFlags.RtlLayout);
			}
			set
			{
				if (RightToLeft != value)
				{
					SetFlag(NativeMethods.TaskDialogFlags.RtlLayout, value);
					UpdateDialog();
				}
			}
		}

		[Category("Window Style")]
		[Description("Indicates whether the dialog has a minimize box on its caption bar.")]
		[DefaultValue(false)]
		public bool MinimizeBox
		{
			get
			{
				return GetFlag(NativeMethods.TaskDialogFlags.CanBeMinimized);
			}
			set
			{
				if (MinimizeBox != value)
				{
					SetFlag(NativeMethods.TaskDialogFlags.CanBeMinimized, value);
					UpdateDialog();
				}
			}
		}

		[Category("Behavior")]
		[Description("The type of progress bar displayed on the dialog.")]
		[DefaultValue(ProgressBarStyle.None)]
		public ProgressBarStyle ProgressBarStyle
		{
			get
			{
				if (GetFlag(NativeMethods.TaskDialogFlags.ShowMarqueeProgressBar))
				{
					return ProgressBarStyle.MarqueeProgressBar;
				}
				if (GetFlag(NativeMethods.TaskDialogFlags.ShowProgressBar))
				{
					return ProgressBarStyle.ProgressBar;
				}
				return ProgressBarStyle.None;
			}
			set
			{
				SetFlag(NativeMethods.TaskDialogFlags.ShowMarqueeProgressBar, value == ProgressBarStyle.MarqueeProgressBar);
				SetFlag(NativeMethods.TaskDialogFlags.ShowProgressBar, value == ProgressBarStyle.ProgressBar);
				UpdateProgressBarStyle();
			}
		}

		[Category("Behavior")]
		[Description("The marquee animation speed of the progress bar in milliseconds.")]
		[DefaultValue(100)]
		public int ProgressBarMarqueeAnimationSpeed
		{
			get
			{
				return _progressBarMarqueeAnimationSpeed;
			}
			set
			{
				_progressBarMarqueeAnimationSpeed = value;
				UpdateProgressBarMarqueeSpeed();
			}
		}

		[Category("Behavior")]
		[Description("The lower bound of the range of the task dialog's progress bar.")]
		[DefaultValue(0)]
		public int ProgressBarMinimum
		{
			get
			{
				return _progressBarMinimimum;
			}
			set
			{
				if (_progressBarMaximum <= value)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				_progressBarMinimimum = value;
				UpdateProgressBarRange();
			}
		}

		[Category("Behavior")]
		[Description("The upper bound of the range of the task dialog's progress bar.")]
		[DefaultValue(100)]
		public int ProgressBarMaximum
		{
			get
			{
				return _progressBarMaximum;
			}
			set
			{
				if (value <= _progressBarMinimimum)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				_progressBarMaximum = value;
				UpdateProgressBarRange();
			}
		}

		[Category("Behavior")]
		[Description("The current value of the task dialog's progress bar.")]
		[DefaultValue(0)]
		public int ProgressBarValue
		{
			get
			{
				return _progressBarValue;
			}
			set
			{
				if (value < ProgressBarMinimum || value > ProgressBarMaximum)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				_progressBarValue = value;
				UpdateProgressBarValue();
			}
		}

		[Category("Behavior")]
		[Description("The state of the task dialog's progress bar.")]
		[DefaultValue(ProgressBarState.Normal)]
		public ProgressBarState ProgressBarState
		{
			get
			{
				return _progressBarState;
			}
			set
			{
				_progressBarState = value;
				UpdateProgressBarState();
			}
		}

		[Category("Data")]
		[Description("User-defined data about the component.")]
		[DefaultValue(null)]
		public object Tag
		{
			get
			{
				return _tag;
			}
			set
			{
				_tag = value;
			}
		}

		private bool IsDialogRunning
		{
			get
			{
				return _handle != IntPtr.Zero;
			}
		}

		[Browsable(false)]
		public IntPtr Handle
		{
			get
			{
				CheckCrossThreadCall();
				return _handle;
			}
		}

		[Category("Behavior")]
		[Description("Event raised when the task dialog has been created.")]
		public event EventHandler Created;

		[Category("Behavior")]
		[Description("Event raised when the task dialog has been destroyed.")]
		public event EventHandler Destroyed;

		[Category("Action")]
		[Description("Event raised when the user clicks a button.")]
		public event EventHandler<TaskDialogItemClickedEventArgs> ButtonClicked;

		[Category("Action")]
		[Description("Event raised when the user clicks a button.")]
		public event EventHandler<TaskDialogItemClickedEventArgs> RadioButtonClicked;

		[Category("Action")]
		[Description("Event raised when the user clicks a hyperlink.")]
		public event EventHandler<HyperlinkClickedEventArgs> HyperlinkClicked;

		[Category("Action")]
		[Description("Event raised when the user clicks the verification check box.")]
		public event EventHandler VerificationClicked;

		[Category("Behavior")]
		[Description("Event raised periodically while the dialog is displayed.")]
		public event EventHandler<TimerEventArgs> Timer;

		[Category("Action")]
		[Description("Event raised when the user clicks the expand button on the task dialog.")]
		public event EventHandler<ExpandButtonClickedEventArgs> ExpandButtonClicked;

		[Category("Action")]
		[Description("Event raised when the user presses F1 while the dialog has focus.")]
		public event EventHandler HelpRequested;

		public TaskDialog()
		{
			InitializeComponent();
			_config.cbSize = (uint)Marshal.SizeOf(_config);
			_config.pfCallback = TaskDialogCallback;
		}

		public TaskDialog(IContainer container)
		{
			if (container != null)
			{
				container.Add(this);
			}
			InitializeComponent();
			_config.cbSize = (uint)Marshal.SizeOf(_config);
			_config.pfCallback = TaskDialogCallback;
		}

		public TaskDialogButton Show()
		{
			return ShowDialog(IntPtr.Zero);
		}

		public TaskDialogButton ShowDialog()
		{
			return ShowDialog(null);
		}

		public TaskDialogButton ShowDialog(IWin32Window owner)
		{
			IntPtr owner2 = ((owner != null) ? owner.Handle : NativeMethods.GetActiveWindow());
			return ShowDialog(owner2);
		}

		public void ClickVerification(bool checkState, bool setFocus)
		{
			if (!IsDialogRunning)
			{
				throw new InvalidOperationException(Resources.TaskDialogNotRunningError);
			}
			NativeMethods.SendMessage(Handle, 1137, new IntPtr(checkState ? 1 : 0), new IntPtr(setFocus ? 1 : 0));
		}

		protected virtual void OnHyperlinkClicked(HyperlinkClickedEventArgs e)
		{
			if (this.HyperlinkClicked != null)
			{
				this.HyperlinkClicked(this, e);
			}
		}

		protected virtual void OnButtonClicked(TaskDialogItemClickedEventArgs e)
		{
			if (this.ButtonClicked != null)
			{
				this.ButtonClicked(this, e);
			}
		}

		protected virtual void OnRadioButtonClicked(TaskDialogItemClickedEventArgs e)
		{
			if (this.RadioButtonClicked != null)
			{
				this.RadioButtonClicked(this, e);
			}
		}

		protected virtual void OnVerificationClicked(EventArgs e)
		{
			if (this.VerificationClicked != null)
			{
				this.VerificationClicked(this, e);
			}
		}

		protected virtual void OnCreated(EventArgs e)
		{
			if (this.Created != null)
			{
				this.Created(this, e);
			}
		}

		protected virtual void OnTimer(TimerEventArgs e)
		{
			if (this.Timer != null)
			{
				this.Timer(this, e);
			}
		}

		protected virtual void OnDestroyed(EventArgs e)
		{
			if (this.Destroyed != null)
			{
				this.Destroyed(this, e);
			}
		}

		protected virtual void OnExpandButtonClicked(ExpandButtonClickedEventArgs e)
		{
			if (this.ExpandButtonClicked != null)
			{
				this.ExpandButtonClicked(this, e);
			}
		}

		protected virtual void OnHelpRequested(EventArgs e)
		{
			if (this.HelpRequested != null)
			{
				this.HelpRequested(this, e);
			}
		}

		internal void SetItemEnabled(TaskDialogItem item)
		{
			if (IsDialogRunning)
			{
				NativeMethods.SendMessage(Handle, (item is TaskDialogButton) ? 1135 : 1136, new IntPtr(item.Id), new IntPtr(item.Enabled ? 1 : 0));
			}
		}

		internal void SetButtonElevationRequired(TaskDialogButton button)
		{
			if (IsDialogRunning)
			{
				NativeMethods.SendMessage(Handle, 1139, new IntPtr(button.Id), new IntPtr(button.ElevationRequired ? 1 : 0));
			}
		}

		internal void ClickItem(TaskDialogItem item)
		{
			if (!IsDialogRunning)
			{
				throw new InvalidOperationException(Resources.TaskDialogNotRunningError);
			}
			NativeMethods.SendMessage(Handle, (item is TaskDialogButton) ? 1126 : 1134, new IntPtr(item.Id), IntPtr.Zero);
		}

		private TaskDialogButton ShowDialog(IntPtr owner)
		{
			if (!OSSupportsTaskDialogs)
			{
				throw new NotSupportedException(Resources.TaskDialogsNotSupportedError);
			}
			if (IsDialogRunning)
			{
				throw new InvalidOperationException(Resources.TaskDialogRunningError);
			}
			if (_buttons.Count == 0)
			{
				throw new InvalidOperationException(Resources.TaskDialogNoButtonsError);
			}
			_config.hwndParent = owner;
			_config.dwCommonButtons = (NativeMethods.TaskDialogCommonButtonFlags)0;
			_config.pButtons = IntPtr.Zero;
			_config.cButtons = 0u;
			List<NativeMethods.TASKDIALOG_BUTTON> buttons = SetupButtons();
			List<NativeMethods.TASKDIALOG_BUTTON> buttons2 = SetupRadioButtons();
			SetupIcon();
			try
			{
				MarshalButtons(buttons, out _config.pButtons, out _config.cButtons);
				MarshalButtons(buttons2, out _config.pRadioButtons, out _config.cRadioButtons);
				int pnButton;
				int pnRadioButton;
				bool pfVerificationFlagChecked;
				using (new ComCtlv6ActivationContext(true))
				{
					NativeMethods.TaskDialogIndirect(ref _config, out pnButton, out pnRadioButton, out pfVerificationFlagChecked);
				}
				IsVerificationChecked = pfVerificationFlagChecked;
				TaskDialogRadioButton value;
				if (_radioButtonsById.TryGetValue(pnRadioButton, out value))
				{
					value.Checked = true;
				}
				TaskDialogButton value2;
				if (_buttonsById.TryGetValue(pnButton, out value2))
				{
					return value2;
				}
				return null;
			}
			finally
			{
				CleanUpButtons(ref _config.pButtons, ref _config.cButtons);
				CleanUpButtons(ref _config.pRadioButtons, ref _config.cRadioButtons);
			}
		}

		internal void UpdateDialog()
		{
			if (!IsDialogRunning)
			{
				return;
			}
			if (_inEventHandler > 0)
			{
				_updatePending = true;
				return;
			}
			_updatePending = false;
			CleanUpButtons(ref _config.pButtons, ref _config.cButtons);
			CleanUpButtons(ref _config.pRadioButtons, ref _config.cRadioButtons);
			_config.dwCommonButtons = (NativeMethods.TaskDialogCommonButtonFlags)0;
			List<NativeMethods.TASKDIALOG_BUTTON> buttons = SetupButtons();
			List<NativeMethods.TASKDIALOG_BUTTON> buttons2 = SetupRadioButtons();
			SetupIcon();
			MarshalButtons(buttons, out _config.pButtons, out _config.cButtons);
			MarshalButtons(buttons2, out _config.pRadioButtons, out _config.cRadioButtons);
			int cb = Marshal.SizeOf(_config);
			IntPtr intPtr = Marshal.AllocHGlobal(cb);
			try
			{
				Marshal.StructureToPtr(_config, intPtr, false);
				NativeMethods.SendMessage(Handle, 1125, IntPtr.Zero, intPtr);
			}
			finally
			{
				Marshal.DestroyStructure(intPtr, typeof(NativeMethods.TASKDIALOGCONFIG));
				Marshal.FreeHGlobal(intPtr);
			}
		}

		private void SetElementText(NativeMethods.TaskDialogElements element, string text)
		{
			if (!IsDialogRunning)
			{
				return;
			}
			IntPtr intPtr = Marshal.StringToHGlobalUni(text);
			try
			{
				IntPtr intPtr2 = NativeMethods.SendMessage(Handle, 1132, new IntPtr((int)element), intPtr);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		private void SetupIcon()
		{
			SetupIcon(MainIcon, CustomMainIcon, NativeMethods.TaskDialogFlags.UseHIconMain);
			SetupIcon(FooterIcon, CustomFooterIcon, NativeMethods.TaskDialogFlags.UseHIconFooter);
		}

		private void SetupIcon(TaskDialogIcon icon, Icon customIcon, NativeMethods.TaskDialogFlags flag)
		{
			SetFlag(flag, false);
			if (icon == TaskDialogIcon.Custom)
			{
				if (customIcon != null)
				{
					SetFlag(flag, true);
					if (flag == NativeMethods.TaskDialogFlags.UseHIconMain)
					{
						_config.hMainIcon = customIcon.Handle;
					}
					else
					{
						_config.hFooterIcon = customIcon.Handle;
					}
				}
			}
			else if (flag == NativeMethods.TaskDialogFlags.UseHIconMain)
			{
				_config.hMainIcon = new IntPtr((int)icon);
			}
			else
			{
				_config.hFooterIcon = new IntPtr((int)icon);
			}
		}

		private static void CleanUpButtons(ref IntPtr buttons, ref uint count)
		{
			if (buttons != IntPtr.Zero)
			{
				int num = Marshal.SizeOf(typeof(NativeMethods.TASKDIALOG_BUTTON));
				for (int i = 0; i < count; i++)
				{
					IntPtr ptr = new IntPtr(buttons.ToInt64() + i * num);
					Marshal.DestroyStructure(ptr, typeof(NativeMethods.TASKDIALOG_BUTTON));
				}
				Marshal.FreeHGlobal(buttons);
				buttons = IntPtr.Zero;
				count = 0u;
			}
		}

		private static void MarshalButtons(List<NativeMethods.TASKDIALOG_BUTTON> buttons, out IntPtr buttonsPtr, out uint count)
		{
			buttonsPtr = IntPtr.Zero;
			count = 0u;
			if (buttons.Count > 0)
			{
				int num = Marshal.SizeOf(typeof(NativeMethods.TASKDIALOG_BUTTON));
				buttonsPtr = Marshal.AllocHGlobal(num * buttons.Count);
				for (int i = 0; i < buttons.Count; i++)
				{
					Marshal.StructureToPtr(ptr: new IntPtr(buttonsPtr.ToInt64() + i * num), structure: buttons[i], fDeleteOld: false);
				}
				count = (uint)buttons.Count;
			}
		}

		private List<NativeMethods.TASKDIALOG_BUTTON> SetupButtons()
		{
			_buttonsById = new Dictionary<int, TaskDialogButton>();
			List<NativeMethods.TASKDIALOG_BUTTON> list = new List<NativeMethods.TASKDIALOG_BUTTON>();
			_config.nDefaultButton = 0;
			foreach (TaskDialogButton button in Buttons)
			{
				if (button.Id < 1)
				{
					throw new InvalidOperationException(Resources.InvalidTaskDialogItemIdError);
				}
				_buttonsById.Add(button.Id, button);
				if (button.Default)
				{
					_config.nDefaultButton = button.Id;
				}
				if (button.ButtonType == ButtonType.Custom)
				{
					if (string.IsNullOrEmpty(button.Text))
					{
						throw new InvalidOperationException(Resources.TaskDialogEmptyButtonLabelError);
					}
					NativeMethods.TASKDIALOG_BUTTON item = new NativeMethods.TASKDIALOG_BUTTON
					{
						nButtonID = button.Id,
						pszButtonText = button.Text
					};
					if (ButtonStyle == TaskDialogButtonStyle.CommandLinks || (ButtonStyle == TaskDialogButtonStyle.CommandLinksNoIcon && !string.IsNullOrEmpty(button.CommandLinkNote)))
					{
						ref NativeMethods.TASKDIALOG_BUTTON reference = ref item;
						reference.pszButtonText = reference.pszButtonText + "\n" + button.CommandLinkNote;
					}
					list.Add(item);
				}
				else
				{
					_config.dwCommonButtons |= button.ButtonFlag;
				}
			}
			return list;
		}

		private List<NativeMethods.TASKDIALOG_BUTTON> SetupRadioButtons()
		{
			_radioButtonsById = new Dictionary<int, TaskDialogRadioButton>();
			List<NativeMethods.TASKDIALOG_BUTTON> list = new List<NativeMethods.TASKDIALOG_BUTTON>();
			_config.nDefaultRadioButton = 0;
			foreach (TaskDialogRadioButton radioButton in RadioButtons)
			{
				if (string.IsNullOrEmpty(radioButton.Text))
				{
					throw new InvalidOperationException(Resources.TaskDialogEmptyButtonLabelError);
				}
				if (radioButton.Id < 1)
				{
					throw new InvalidOperationException(Resources.InvalidTaskDialogItemIdError);
				}
				_radioButtonsById.Add(radioButton.Id, radioButton);
				if (radioButton.Checked)
				{
					_config.nDefaultRadioButton = radioButton.Id;
				}
				list.Add(new NativeMethods.TASKDIALOG_BUTTON
				{
					nButtonID = radioButton.Id,
					pszButtonText = radioButton.Text
				});
			}
			SetFlag(NativeMethods.TaskDialogFlags.NoDefaultRadioButton, _config.nDefaultRadioButton == 0);
			return list;
		}

		private void SetFlag(NativeMethods.TaskDialogFlags flag, bool value)
		{
			if (value)
			{
				_config.dwFlags |= flag;
			}
			else
			{
				_config.dwFlags &= ~flag;
			}
		}

		private bool GetFlag(NativeMethods.TaskDialogFlags flag)
		{
			return (_config.dwFlags & flag) != 0;
		}

		private uint TaskDialogCallback(IntPtr hwnd, uint uNotification, IntPtr wParam, IntPtr lParam, IntPtr dwRefData)
		{
			Interlocked.Increment(ref _inEventHandler);
			try
			{
				switch ((NativeMethods.TaskDialogNotifications)uNotification)
				{
				case NativeMethods.TaskDialogNotifications.Created:
					_handle = hwnd;
					DialogCreated();
					OnCreated(EventArgs.Empty);
					break;
				case NativeMethods.TaskDialogNotifications.Destroyed:
					_handle = IntPtr.Zero;
					OnDestroyed(EventArgs.Empty);
					break;
				case NativeMethods.TaskDialogNotifications.Navigated:
					DialogCreated();
					break;
				case NativeMethods.TaskDialogNotifications.HyperlinkClicked:
				{
					string href = Marshal.PtrToStringUni(lParam);
					OnHyperlinkClicked(new HyperlinkClickedEventArgs(href));
					break;
				}
				case NativeMethods.TaskDialogNotifications.ButtonClicked:
				{
					TaskDialogButton value2;
					if (_buttonsById.TryGetValue((int)wParam, out value2))
					{
						TaskDialogItemClickedEventArgs e3 = new TaskDialogItemClickedEventArgs(value2);
						OnButtonClicked(e3);
						if (e3.Cancel)
						{
							return 1u;
						}
					}
					break;
				}
				case NativeMethods.TaskDialogNotifications.VerificationClicked:
					IsVerificationChecked = (int)wParam == 1;
					OnVerificationClicked(EventArgs.Empty);
					break;
				case NativeMethods.TaskDialogNotifications.RadioButtonClicked:
				{
					TaskDialogRadioButton value;
					if (_radioButtonsById.TryGetValue((int)wParam, out value))
					{
						value.Checked = true;
						TaskDialogItemClickedEventArgs e2 = new TaskDialogItemClickedEventArgs(value);
						OnRadioButtonClicked(e2);
					}
					break;
				}
				case NativeMethods.TaskDialogNotifications.Timer:
				{
					TimerEventArgs e = new TimerEventArgs(wParam.ToInt32());
					OnTimer(e);
					return e.ResetTickCount ? 1u : 0u;
				}
				case NativeMethods.TaskDialogNotifications.ExpandoButtonClicked:
					OnExpandButtonClicked(new ExpandButtonClickedEventArgs(wParam.ToInt32() != 0));
					break;
				case NativeMethods.TaskDialogNotifications.Help:
					OnHelpRequested(EventArgs.Empty);
					break;
				}
				return 0u;
			}
			finally
			{
				Interlocked.Decrement(ref _inEventHandler);
				if (_updatePending)
				{
					UpdateDialog();
				}
			}
		}

		private void DialogCreated()
		{
			if (_config.hwndParent == IntPtr.Zero && _windowIcon != null)
			{
				NativeMethods.SendMessage(Handle, 128, new IntPtr(0), _windowIcon.Handle);
			}
			foreach (TaskDialogButton button in Buttons)
			{
				if (!button.Enabled)
				{
					SetItemEnabled(button);
				}
				if (button.ElevationRequired)
				{
					SetButtonElevationRequired(button);
				}
			}
			UpdateProgressBarStyle();
			UpdateProgressBarMarqueeSpeed();
			UpdateProgressBarRange();
			UpdateProgressBarValue();
			UpdateProgressBarState();
		}

		private void UpdateProgressBarStyle()
		{
			if (IsDialogRunning)
			{
				NativeMethods.SendMessage(Handle, 1127, new IntPtr((ProgressBarStyle == ProgressBarStyle.MarqueeProgressBar) ? 1 : 0), IntPtr.Zero);
			}
		}

		private void UpdateProgressBarMarqueeSpeed()
		{
			if (IsDialogRunning)
			{
				NativeMethods.SendMessage(Handle, 1131, new IntPtr((ProgressBarMarqueeAnimationSpeed > 0) ? 1 : 0), new IntPtr(ProgressBarMarqueeAnimationSpeed));
			}
		}

		private void UpdateProgressBarRange()
		{
			if (IsDialogRunning)
			{
				NativeMethods.SendMessage(Handle, 1129, IntPtr.Zero, new IntPtr((ProgressBarMaximum << 16) | ProgressBarMinimum));
			}
			if (ProgressBarValue < ProgressBarMinimum)
			{
				ProgressBarValue = ProgressBarMinimum;
			}
			if (ProgressBarValue > ProgressBarMaximum)
			{
				ProgressBarValue = ProgressBarMaximum;
			}
		}

		private void UpdateProgressBarValue()
		{
			if (IsDialogRunning)
			{
				NativeMethods.SendMessage(Handle, 1130, new IntPtr(ProgressBarValue), IntPtr.Zero);
			}
		}

		private void UpdateProgressBarState()
		{
			if (IsDialogRunning)
			{
				NativeMethods.SendMessage(Handle, 1128, new IntPtr((int)(ProgressBarState + 1)), IntPtr.Zero);
			}
		}

		private void CheckCrossThreadCall()
		{
			IntPtr handle = _handle;
			if (handle != IntPtr.Zero)
			{
				int lpdwProcessId;
				int windowThreadProcessId = NativeMethods.GetWindowThreadProcessId(handle, out lpdwProcessId);
				int currentThreadId = NativeMethods.GetCurrentThreadId();
				if (windowThreadProcessId != currentThreadId)
				{
					throw new InvalidOperationException(Resources.TaskDialogIllegalCrossThreadCallError);
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!disposing)
				{
					return;
				}
				if (components != null)
				{
					components.Dispose();
					components = null;
				}
				if (_buttons != null)
				{
					foreach (TaskDialogButton button in _buttons)
					{
						button.Dispose();
					}
					_buttons.Clear();
				}
				if (_radioButtons == null)
				{
					return;
				}
				foreach (TaskDialogRadioButton radioButton in _radioButtons)
				{
					radioButton.Dispose();
				}
				_radioButtons.Clear();
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		private void InitializeComponent()
		{
			components = new Container();
		}
	}
}
