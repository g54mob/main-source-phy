using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	[DefaultEvent("DoWork")]
	[DefaultProperty("Text")]
	[Description("Represents a dialog that can be used to report progress to the user.")]
	public class ProgressDialog : Component
	{
		private class ProgressChangedData
		{
			public string Text { get; set; }

			public string Description { get; set; }

			public object UserState { get; set; }
		}

		private string _windowTitle;

		private string _text;

		private string _description;

		private IProgressDialog _dialog;

		private string _cancellationText;

		private bool _useCompactPathsForText;

		private bool _useCompactPathsForDescription;

		private SafeModuleHandle _currentAnimationModuleHandle;

		private bool _cancellationPending;

		private IContainer components = null;

		private BackgroundWorker _backgroundWorker;

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text in the progress dialog's title bar.")]
		[DefaultValue("")]
		public string WindowTitle
		{
			get
			{
				return _windowTitle ?? string.Empty;
			}
			set
			{
				_windowTitle = value;
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("A short description of the operation being carried out.")]
		public string Text
		{
			get
			{
				return _text ?? string.Empty;
			}
			set
			{
				_text = value;
				if (_dialog != null)
				{
					_dialog.SetLine(1u, Text, UseCompactPathsForText, IntPtr.Zero);
				}
			}
		}

		[Category("Behavior")]
		[Description("Indicates whether path strings in the Text property should be compacted if they are too large to fit on one line.")]
		[DefaultValue(false)]
		public bool UseCompactPathsForText
		{
			get
			{
				return _useCompactPathsForText;
			}
			set
			{
				_useCompactPathsForText = value;
				if (_dialog != null)
				{
					_dialog.SetLine(1u, Text, UseCompactPathsForText, IntPtr.Zero);
				}
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("Additional details about the operation being carried out.")]
		[DefaultValue("")]
		public string Description
		{
			get
			{
				return _description ?? string.Empty;
			}
			set
			{
				_description = value;
				if (_dialog != null)
				{
					_dialog.SetLine(2u, Description, UseCompactPathsForDescription, IntPtr.Zero);
				}
			}
		}

		[Category("Behavior")]
		[Description("Indicates whether path strings in the Description property should be compacted if they are too large to fit on one line.")]
		[DefaultValue(false)]
		public bool UseCompactPathsForDescription
		{
			get
			{
				return _useCompactPathsForDescription;
			}
			set
			{
				_useCompactPathsForDescription = value;
				if (_dialog != null)
				{
					_dialog.SetLine(2u, Description, UseCompactPathsForDescription, IntPtr.Zero);
				}
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text that will be shown after the Cancel button is pressed.")]
		[DefaultValue("")]
		public string CancellationText
		{
			get
			{
				return _cancellationText ?? string.Empty;
			}
			set
			{
				_cancellationText = value;
			}
		}

		[Category("Appearance")]
		[Description("Indicates whether an estimate of the remaining time will be shown.")]
		[DefaultValue(false)]
		public bool ShowTimeRemaining { get; set; }

		[Category("Appearance")]
		[Description("Indicates whether the dialog has a cancel button. Do not set to false unless absolutely necessary.")]
		[DefaultValue(true)]
		public bool ShowCancelButton { get; set; }

		[Category("Window Style")]
		[Description("Indicates whether the progress dialog has a minimize button.")]
		[DefaultValue(true)]
		public bool MinimizeBox { get; set; }

		[Browsable(false)]
		public bool CancellationPending
		{
			get
			{
				_backgroundWorker.ReportProgress(-1);
				return _cancellationPending;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AnimationResource Animation { get; set; }

		[Category("Appearance")]
		[Description("Indicates the style of the progress bar.")]
		[DefaultValue(ProgressBarStyle.ProgressBar)]
		public ProgressBarStyle ProgressBarStyle { get; set; }

		[Browsable(false)]
		public bool IsBusy
		{
			get
			{
				return _backgroundWorker.IsBusy;
			}
		}

		public event DoWorkEventHandler DoWork;

		public event RunWorkerCompletedEventHandler RunWorkerCompleted;

		public event ProgressChangedEventHandler ProgressChanged;

		public ProgressDialog()
			: this(null)
		{
		}

		public ProgressDialog(IContainer container)
		{
			if (container != null)
			{
				container.Add(this);
			}
			InitializeComponent();
			ProgressBarStyle = ProgressBarStyle.ProgressBar;
			ShowCancelButton = true;
			MinimizeBox = true;
			if (!NativeMethods.IsWindowsVistaOrLater)
			{
				Animation = AnimationResource.GetShellAnimation(ShellAnimation.FlyingPapers);
			}
		}

		public void Show()
		{
			Show(null);
		}

		public void Show(object argument)
		{
			RunProgressDialog(IntPtr.Zero, argument);
		}

		public void ShowDialog()
		{
			ShowDialog(null, null);
		}

		public void ShowDialog(IWin32Window owner)
		{
			ShowDialog(owner, null);
		}

		public void ShowDialog(IWin32Window owner, object argument)
		{
			RunProgressDialog((owner == null) ? NativeMethods.GetActiveWindow() : owner.Handle, argument);
		}

		public void ReportProgress(int percentProgress)
		{
			ReportProgress(percentProgress, null, null, null);
		}

		public void ReportProgress(int percentProgress, string text, string description)
		{
			ReportProgress(percentProgress, text, description, null);
		}

		public void ReportProgress(int percentProgress, string text, string description, object userState)
		{
			if (percentProgress < 0 || percentProgress > 100)
			{
				throw new ArgumentOutOfRangeException("percentProgress");
			}
			if (_dialog == null)
			{
				throw new InvalidOperationException(Resources.ProgressDialogNotRunningError);
			}
			_backgroundWorker.ReportProgress(percentProgress, new ProgressChangedData
			{
				Text = text,
				Description = description,
				UserState = userState
			});
		}

		protected virtual void OnDoWork(DoWorkEventArgs e)
		{
			DoWorkEventHandler doWorkEventHandler = this.DoWork;
			if (doWorkEventHandler != null)
			{
				doWorkEventHandler(this, e);
			}
		}

		protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
		{
			RunWorkerCompletedEventHandler runWorkerCompletedEventHandler = this.RunWorkerCompleted;
			if (runWorkerCompletedEventHandler != null)
			{
				runWorkerCompletedEventHandler(this, e);
			}
		}

		protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
		{
			ProgressChangedEventHandler progressChangedEventHandler = this.ProgressChanged;
			if (progressChangedEventHandler != null)
			{
				progressChangedEventHandler(this, e);
			}
		}

		private void RunProgressDialog(IntPtr owner, object argument)
		{
			if (_backgroundWorker.IsBusy)
			{
				throw new InvalidOperationException(Resources.ProgressDialogRunning);
			}
			if (Animation != null)
			{
				try
				{
					_currentAnimationModuleHandle = Animation.LoadLibrary();
				}
				catch (Win32Exception ex)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.AnimationLoadErrorFormat, ex.Message), ex);
				}
				catch (FileNotFoundException ex2)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.AnimationLoadErrorFormat, ex2.Message), ex2);
				}
			}
			_cancellationPending = false;
			_dialog = (Ookii.Dialogs.Interop.ProgressDialog)new ProgressDialogRCW();
			_dialog.SetTitle(WindowTitle);
			if (Animation != null)
			{
				_dialog.SetAnimation(_currentAnimationModuleHandle, (ushort)Animation.ResourceId);
			}
			if (CancellationText.Length > 0)
			{
				_dialog.SetCancelMsg(CancellationText, null);
			}
			_dialog.SetLine(1u, Text, UseCompactPathsForText, IntPtr.Zero);
			_dialog.SetLine(2u, Description, UseCompactPathsForDescription, IntPtr.Zero);
			ProgressDialogFlags progressDialogFlags = ProgressDialogFlags.Normal;
			if (owner != IntPtr.Zero)
			{
				progressDialogFlags |= ProgressDialogFlags.Modal;
			}
			switch (ProgressBarStyle)
			{
			case ProgressBarStyle.None:
				progressDialogFlags |= ProgressDialogFlags.NoProgressBar;
				break;
			case ProgressBarStyle.MarqueeProgressBar:
				progressDialogFlags = ((!NativeMethods.IsWindowsVistaOrLater) ? (progressDialogFlags | ProgressDialogFlags.NoProgressBar) : (progressDialogFlags | ProgressDialogFlags.MarqueeProgress));
				break;
			}
			if (ShowTimeRemaining)
			{
				progressDialogFlags |= ProgressDialogFlags.AutoTime;
			}
			if (!ShowCancelButton)
			{
				progressDialogFlags |= ProgressDialogFlags.NoCancel;
			}
			if (!MinimizeBox)
			{
				progressDialogFlags |= ProgressDialogFlags.NoMinimize;
			}
			_dialog.StartProgressDialog(owner, null, progressDialogFlags, IntPtr.Zero);
			_backgroundWorker.RunWorkerAsync(argument);
		}

		private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			OnDoWork(e);
		}

		private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			_dialog.StopProgressDialog();
			Marshal.ReleaseComObject(_dialog);
			_dialog = null;
			if (_currentAnimationModuleHandle != null)
			{
				_currentAnimationModuleHandle.Dispose();
				_currentAnimationModuleHandle = null;
			}
			OnRunWorkerCompleted(new RunWorkerCompletedEventArgs((!e.Cancelled && e.Error == null) ? e.Result : null, e.Error, e.Cancelled));
		}

		private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			_cancellationPending = _dialog.HasUserCancelled();
			if (e.ProgressPercentage < 0 || e.ProgressPercentage > 100)
			{
				return;
			}
			_dialog.SetProgress((uint)e.ProgressPercentage, 100u);
			ProgressChangedData progressChangedData = e.UserState as ProgressChangedData;
			if (progressChangedData != null)
			{
				if (progressChangedData.Text != null)
				{
					Text = progressChangedData.Text;
				}
				if (progressChangedData.Description != null)
				{
					Description = progressChangedData.Description;
				}
				OnProgressChanged(new ProgressChangedEventArgs(e.ProgressPercentage, progressChangedData.UserState));
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (components != null)
					{
						components.Dispose();
					}
					if (_currentAnimationModuleHandle != null)
					{
						_currentAnimationModuleHandle.Dispose();
						_currentAnimationModuleHandle = null;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		private void InitializeComponent()
		{
			_backgroundWorker = new BackgroundWorker();
			_backgroundWorker.WorkerReportsProgress = true;
			_backgroundWorker.WorkerSupportsCancellation = true;
			_backgroundWorker.DoWork += _backgroundWorker_DoWork;
			_backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
			_backgroundWorker.ProgressChanged += _backgroundWorker_ProgressChanged;
		}
	}
}
