using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	[DefaultProperty("MainInstruction")]
	[DefaultEvent("UserNameChanged")]
	[Description("Allows access to credential UI for generic credentials.")]
	public class CredentialDialog : Component
	{
		private string _confirmTarget;

		private NetworkCredential _credentials = new NetworkCredential();

		private bool _isSaveChecked;

		private string _target;

		private static readonly Dictionary<string, NetworkCredential> _applicationInstanceCredentialCache = new Dictionary<string, NetworkCredential>();

		private string _caption;

		private string _text;

		private string _windowTitle;

		private IContainer components = null;

		[Category("Behavior")]
		[Description("Indicates whether to use the application instance credential cache.")]
		[DefaultValue(false)]
		public bool UseApplicationInstanceCredentialCache { get; set; }

		[Category("Appearance")]
		[Description("Indicates whether the \"Save password\" checkbox is checked.")]
		[DefaultValue(false)]
		public bool IsSaveChecked
		{
			get
			{
				return _isSaveChecked;
			}
			set
			{
				_confirmTarget = null;
				_isSaveChecked = value;
			}
		}

		[Browsable(false)]
		public string Password
		{
			get
			{
				return _credentials.Password;
			}
			private set
			{
				_confirmTarget = null;
				_credentials.Password = value;
				OnPasswordChanged(EventArgs.Empty);
			}
		}

		[Browsable(false)]
		public NetworkCredential Credentials
		{
			get
			{
				return _credentials;
			}
		}

		[Browsable(false)]
		public string UserName
		{
			get
			{
				return _credentials.UserName ?? string.Empty;
			}
			private set
			{
				_confirmTarget = null;
				_credentials.UserName = value;
				OnUserNameChanged(EventArgs.Empty);
			}
		}

		[Category("Behavior")]
		[Description("The target for the credentials, typically the server name prefixed by an application-specific identifier.")]
		[DefaultValue("")]
		public string Target
		{
			get
			{
				return _target ?? string.Empty;
			}
			set
			{
				_target = value;
				_confirmTarget = null;
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The title of the credentials dialog.")]
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
		[Description("A brief message that will be displayed in the dialog box.")]
		[DefaultValue("")]
		public string MainInstruction
		{
			get
			{
				return _caption ?? string.Empty;
			}
			set
			{
				_caption = value;
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("Additional text to display in the dialog.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string Content
		{
			get
			{
				return _text ?? string.Empty;
			}
			set
			{
				_text = value;
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("Indicates how the text of the MainInstruction and Content properties is displayed on Windows XP.")]
		[DefaultValue(DownlevelTextMode.MainInstructionAndContent)]
		public DownlevelTextMode DownlevelTextMode { get; set; }

		[Category("Appearance")]
		[Description("Indicates whether a check box is shown on the dialog that allows the user to choose whether to save the credentials or not.")]
		[DefaultValue(false)]
		public bool ShowSaveCheckBox { get; set; }

		[Category("Behavior")]
		[Description("Indicates whether the dialog should be displayed even when saved credentials exist for the specified target.")]
		[DefaultValue(false)]
		public bool ShowUIForSavedCredentials { get; set; }

		public bool IsStoredCredential { get; private set; }

		[Category("Property Changed")]
		[Description("Event raised when the value of the UserName property changes.")]
		public event EventHandler UserNameChanged;

		[Category("Property Changed")]
		[Description("Event raised when the value of the Password property changes.")]
		public event EventHandler PasswordChanged;

		public CredentialDialog()
		{
			InitializeComponent();
		}

		public CredentialDialog(IContainer container)
		{
			if (container != null)
			{
				container.Add(this);
			}
			InitializeComponent();
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public DialogResult ShowDialog()
		{
			return ShowDialog(null);
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public DialogResult ShowDialog(IWin32Window owner)
		{
			if (string.IsNullOrEmpty(_target))
			{
				throw new InvalidOperationException(Resources.CredentialEmptyTargetError);
			}
			UserName = "";
			Password = "";
			IsStoredCredential = false;
			if (RetrieveCredentialsFromApplicationInstanceCache())
			{
				IsStoredCredential = true;
				_confirmTarget = Target;
				return DialogResult.OK;
			}
			bool storedCredentials = false;
			if (ShowSaveCheckBox && RetrieveCredentials())
			{
				IsSaveChecked = true;
				if (!ShowUIForSavedCredentials)
				{
					IsStoredCredential = true;
					_confirmTarget = Target;
					return DialogResult.OK;
				}
				storedCredentials = true;
			}
			IntPtr owner2 = ((owner == null) ? NativeMethods.GetActiveWindow() : owner.Handle);
			bool flag = ((!NativeMethods.IsWindowsVistaOrLater) ? PromptForCredentialsCredUI(owner2, storedCredentials) : PromptForCredentialsCredUIWin(owner2, storedCredentials));
			return flag ? DialogResult.OK : DialogResult.Cancel;
		}

		public void ConfirmCredentials(bool confirm)
		{
			if (_confirmTarget == null || _confirmTarget != Target)
			{
				throw new InvalidOperationException(Resources.CredentialPromptNotCalled);
			}
			_confirmTarget = null;
			if (!(IsSaveChecked && confirm))
			{
				return;
			}
			if (UseApplicationInstanceCredentialCache)
			{
				lock (_applicationInstanceCredentialCache)
				{
					_applicationInstanceCredentialCache[Target] = new NetworkCredential(UserName, Password);
				}
			}
			StoreCredential(Target, Credentials);
		}

		public static void StoreCredential(string target, NetworkCredential credential)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (target.Length == 0)
			{
				throw new ArgumentException(Resources.CredentialEmptyTargetError, "target");
			}
			if (credential == null)
			{
				throw new ArgumentNullException("credential");
			}
			NativeMethods.CREDENTIAL Credential = new NativeMethods.CREDENTIAL
			{
				UserName = credential.UserName,
				TargetName = target,
				Persist = NativeMethods.CredPersist.Enterprise
			};
			byte[] array = EncryptPassword(credential.Password);
			Credential.CredentialBlob = Marshal.AllocHGlobal(array.Length);
			try
			{
				Marshal.Copy(array, 0, Credential.CredentialBlob, array.Length);
				Credential.CredentialBlobSize = (uint)array.Length;
				Credential.Type = NativeMethods.CredTypes.CRED_TYPE_GENERIC;
				if (!NativeMethods.CredWrite(ref Credential, 0))
				{
					throw new CredentialException(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				Marshal.FreeCoTaskMem(Credential.CredentialBlob);
			}
		}

		public static NetworkCredential RetrieveCredential(string target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (target.Length == 0)
			{
				throw new ArgumentException(Resources.CredentialEmptyTargetError, "target");
			}
			NetworkCredential networkCredential = RetrieveCredentialFromApplicationInstanceCache(target);
			if (networkCredential != null)
			{
				return networkCredential;
			}
			IntPtr Credential;
			bool flag = NativeMethods.CredRead(target, NativeMethods.CredTypes.CRED_TYPE_GENERIC, 0, out Credential);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (flag)
			{
				try
				{
					NativeMethods.CREDENTIAL cREDENTIAL = (NativeMethods.CREDENTIAL)Marshal.PtrToStructure(Credential, typeof(NativeMethods.CREDENTIAL));
					byte[] array = new byte[cREDENTIAL.CredentialBlobSize];
					Marshal.Copy(cREDENTIAL.CredentialBlob, array, 0, array.Length);
					networkCredential = new NetworkCredential(cREDENTIAL.UserName, DecryptPassword(array));
				}
				finally
				{
					NativeMethods.CredFree(Credential);
				}
				return networkCredential;
			}
			if (lastWin32Error == 1168)
			{
				return null;
			}
			throw new CredentialException(lastWin32Error);
		}

		public static NetworkCredential RetrieveCredentialFromApplicationInstanceCache(string target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (target.Length == 0)
			{
				throw new ArgumentException(Resources.CredentialEmptyTargetError, "target");
			}
			lock (_applicationInstanceCredentialCache)
			{
				NetworkCredential value;
				if (_applicationInstanceCredentialCache.TryGetValue(target, out value))
				{
					return value;
				}
			}
			return null;
		}

		public static bool DeleteCredential(string target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (target.Length == 0)
			{
				throw new ArgumentException(Resources.CredentialEmptyTargetError, "target");
			}
			bool result = false;
			lock (_applicationInstanceCredentialCache)
			{
				result = _applicationInstanceCredentialCache.Remove(target);
			}
			if (NativeMethods.CredDelete(target, NativeMethods.CredTypes.CRED_TYPE_GENERIC, 0))
			{
				result = true;
			}
			else
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 1168)
				{
					throw new CredentialException(lastWin32Error);
				}
			}
			return result;
		}

		protected virtual void OnUserNameChanged(EventArgs e)
		{
			if (this.UserNameChanged != null)
			{
				this.UserNameChanged(this, e);
			}
		}

		protected virtual void OnPasswordChanged(EventArgs e)
		{
			if (this.PasswordChanged != null)
			{
				this.PasswordChanged(this, e);
			}
		}

		private bool PromptForCredentialsCredUI(IntPtr owner, bool storedCredentials)
		{
			NativeMethods.CREDUI_INFO pUiInfo = CreateCredUIInfo(owner, true);
			NativeMethods.CREDUI_FLAGS cREDUI_FLAGS = NativeMethods.CREDUI_FLAGS.DO_NOT_PERSIST | NativeMethods.CREDUI_FLAGS.ALWAYS_SHOW_UI | NativeMethods.CREDUI_FLAGS.GENERIC_CREDENTIALS;
			if (ShowSaveCheckBox)
			{
				cREDUI_FLAGS |= NativeMethods.CREDUI_FLAGS.SHOW_SAVE_CHECK_BOX;
			}
			StringBuilder stringBuilder = new StringBuilder(513);
			stringBuilder.Append(UserName);
			StringBuilder stringBuilder2 = new StringBuilder(256);
			stringBuilder2.Append(Password);
			NativeMethods.CredUIReturnCodes credUIReturnCodes = NativeMethods.CredUIPromptForCredentials(ref pUiInfo, Target, IntPtr.Zero, 0, stringBuilder, 513u, stringBuilder2, 256u, ref _isSaveChecked, cREDUI_FLAGS);
			switch (credUIReturnCodes)
			{
			case NativeMethods.CredUIReturnCodes.NO_ERROR:
				UserName = stringBuilder.ToString();
				Password = stringBuilder2.ToString();
				if (ShowSaveCheckBox)
				{
					_confirmTarget = Target;
					if (storedCredentials && !IsSaveChecked)
					{
						DeleteCredential(Target);
					}
				}
				return true;
			case NativeMethods.CredUIReturnCodes.ERROR_CANCELLED:
				return false;
			default:
				throw new CredentialException((int)credUIReturnCodes);
			}
		}

		private bool PromptForCredentialsCredUIWin(IntPtr owner, bool storedCredentials)
		{
			NativeMethods.CREDUI_INFO pUiInfo = CreateCredUIInfo(owner, false);
			NativeMethods.CredUIWinFlags credUIWinFlags = NativeMethods.CredUIWinFlags.Generic;
			if (ShowSaveCheckBox)
			{
				credUIWinFlags |= NativeMethods.CredUIWinFlags.Checkbox;
			}
			IntPtr intPtr = IntPtr.Zero;
			IntPtr ppvOutAuthBuffer = IntPtr.Zero;
			try
			{
				uint pcbPackedCredentials = 0u;
				if (UserName.Length > 0)
				{
					NativeMethods.CredPackAuthenticationBuffer(0u, UserName, Password, IntPtr.Zero, ref pcbPackedCredentials);
					if (pcbPackedCredentials != 0)
					{
						intPtr = Marshal.AllocCoTaskMem((int)pcbPackedCredentials);
						if (!NativeMethods.CredPackAuthenticationBuffer(0u, UserName, Password, intPtr, ref pcbPackedCredentials))
						{
							throw new CredentialException(Marshal.GetLastWin32Error());
						}
					}
				}
				uint pulAuthPackage = 0u;
				uint pulOutAuthBufferSize;
				NativeMethods.CredUIReturnCodes credUIReturnCodes = NativeMethods.CredUIPromptForWindowsCredentials(ref pUiInfo, 0u, ref pulAuthPackage, intPtr, pcbPackedCredentials, out ppvOutAuthBuffer, out pulOutAuthBufferSize, ref _isSaveChecked, credUIWinFlags);
				switch (credUIReturnCodes)
				{
				case NativeMethods.CredUIReturnCodes.NO_ERROR:
				{
					StringBuilder stringBuilder = new StringBuilder(513);
					StringBuilder stringBuilder2 = new StringBuilder(256);
					uint pcchMaxUserName = (uint)stringBuilder.Capacity;
					uint pcchMaxPassword = (uint)stringBuilder2.Capacity;
					uint pcchMaxDomainName = 0u;
					if (!NativeMethods.CredUnPackAuthenticationBuffer(0u, ppvOutAuthBuffer, pulOutAuthBufferSize, stringBuilder, ref pcchMaxUserName, null, ref pcchMaxDomainName, stringBuilder2, ref pcchMaxPassword))
					{
						throw new CredentialException(Marshal.GetLastWin32Error());
					}
					UserName = stringBuilder.ToString();
					Password = stringBuilder2.ToString();
					if (ShowSaveCheckBox)
					{
						_confirmTarget = Target;
						if (storedCredentials && !IsSaveChecked)
						{
							DeleteCredential(Target);
						}
					}
					return true;
				}
				case NativeMethods.CredUIReturnCodes.ERROR_CANCELLED:
					return false;
				default:
					throw new CredentialException((int)credUIReturnCodes);
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
				if (ppvOutAuthBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ppvOutAuthBuffer);
				}
			}
		}

		private NativeMethods.CREDUI_INFO CreateCredUIInfo(IntPtr owner, bool downlevelText)
		{
			NativeMethods.CREDUI_INFO cREDUI_INFO = default(NativeMethods.CREDUI_INFO);
			cREDUI_INFO.cbSize = Marshal.SizeOf(cREDUI_INFO);
			cREDUI_INFO.hwndParent = owner;
			if (downlevelText)
			{
				cREDUI_INFO.pszCaptionText = WindowTitle;
				switch (DownlevelTextMode)
				{
				case DownlevelTextMode.MainInstructionAndContent:
					if (MainInstruction.Length == 0)
					{
						cREDUI_INFO.pszMessageText = Content;
					}
					else if (Content.Length == 0)
					{
						cREDUI_INFO.pszMessageText = MainInstruction;
					}
					else
					{
						cREDUI_INFO.pszMessageText = MainInstruction + Environment.NewLine + Environment.NewLine + Content;
					}
					break;
				case DownlevelTextMode.MainInstructionOnly:
					cREDUI_INFO.pszMessageText = MainInstruction;
					break;
				case DownlevelTextMode.ContentOnly:
					cREDUI_INFO.pszMessageText = Content;
					break;
				}
			}
			else
			{
				cREDUI_INFO.pszMessageText = Content;
				cREDUI_INFO.pszCaptionText = MainInstruction;
			}
			return cREDUI_INFO;
		}

		private bool RetrieveCredentials()
		{
			NetworkCredential networkCredential = RetrieveCredential(Target);
			if (networkCredential != null)
			{
				UserName = networkCredential.UserName;
				Password = networkCredential.Password;
				return true;
			}
			return false;
		}

		private static byte[] EncryptPassword(string password)
		{
			return ProtectedData.Protect(Encoding.UTF8.GetBytes(password), null, DataProtectionScope.CurrentUser);
		}

		private static string DecryptPassword(byte[] encrypted)
		{
			try
			{
				return Encoding.UTF8.GetString(ProtectedData.Unprotect(encrypted, null, DataProtectionScope.CurrentUser));
			}
			catch (CryptographicException)
			{
				return string.Empty;
			}
		}

		private bool RetrieveCredentialsFromApplicationInstanceCache()
		{
			if (UseApplicationInstanceCredentialCache)
			{
				NetworkCredential networkCredential = RetrieveCredentialFromApplicationInstanceCache(Target);
				if (networkCredential != null)
				{
					UserName = networkCredential.UserName;
					Password = networkCredential.Password;
					return true;
				}
			}
			return false;
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
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
