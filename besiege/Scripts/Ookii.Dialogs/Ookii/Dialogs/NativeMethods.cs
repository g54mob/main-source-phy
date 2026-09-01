using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;

namespace Ookii.Dialogs
{
	internal static class NativeMethods
	{
		[Flags]
		public enum LoadLibraryExFlags : uint
		{
			DontResolveDllReferences = 1u,
			LoadLibraryAsDatafile = 2u,
			LoadWithAlteredSearchPath = 8u,
			LoadIgnoreCodeAuthzLevel = 0x10u
		}

		public delegate uint TaskDialogCallback(IntPtr hwnd, uint uNotification, IntPtr wParam, IntPtr lParam, IntPtr dwRefData);

		public enum TaskDialogNotifications
		{
			Created = 0,
			Navigated = 1,
			ButtonClicked = 2,
			HyperlinkClicked = 3,
			Timer = 4,
			Destroyed = 5,
			RadioButtonClicked = 6,
			DialogConstructed = 7,
			VerificationClicked = 8,
			Help = 9,
			ExpandoButtonClicked = 10
		}

		[Flags]
		public enum TaskDialogCommonButtonFlags
		{
			OkButton = 1,
			YesButton = 2,
			NoButton = 4,
			CancelButton = 8,
			RetryButton = 0x10,
			CloseButton = 0x20
		}

		[Flags]
		public enum TaskDialogFlags
		{
			EnableHyperLinks = 1,
			UseHIconMain = 2,
			UseHIconFooter = 4,
			AllowDialogCancellation = 8,
			UseCommandLinks = 0x10,
			UseCommandLinksNoIcon = 0x20,
			ExpandFooterArea = 0x40,
			ExpandedByDefault = 0x80,
			VerificationFlagChecked = 0x100,
			ShowProgressBar = 0x200,
			ShowMarqueeProgressBar = 0x400,
			CallbackTimer = 0x800,
			PositionRelativeToWindow = 0x1000,
			RtlLayout = 0x2000,
			NoDefaultRadioButton = 0x4000,
			CanBeMinimized = 0x8000
		}

		public enum TaskDialogMessages
		{
			NavigatePage = 1125,
			ClickButton = 1126,
			SetMarqueeProgressBar = 1127,
			SetProgressBarState = 1128,
			SetProgressBarRange = 1129,
			SetProgressBarPos = 1130,
			SetProgressBarMarquee = 1131,
			SetElementText = 1132,
			ClickRadioButton = 1134,
			EnableButton = 1135,
			EnableRadioButton = 1136,
			ClickVerification = 1137,
			UpdateElementText = 1138,
			SetButtonElevationRequiredState = 1139,
			UpdateIcon = 1140
		}

		public enum TaskDialogElements
		{
			Content = 0,
			ExpandedInformation = 1,
			Footer = 2,
			MainInstruction = 3
		}

		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public struct TASKDIALOG_BUTTON
		{
			public int nButtonID;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszButtonText;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public struct TASKDIALOGCONFIG
		{
			public uint cbSize;

			public IntPtr hwndParent;

			public IntPtr hInstance;

			public TaskDialogFlags dwFlags;

			public TaskDialogCommonButtonFlags dwCommonButtons;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszWindowTitle;

			public IntPtr hMainIcon;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszMainInstruction;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszContent;

			public uint cButtons;

			public IntPtr pButtons;

			public int nDefaultButton;

			public uint cRadioButtons;

			public IntPtr pRadioButtons;

			public int nDefaultRadioButton;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszVerificationText;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszExpandedInformation;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszExpandedControlText;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszCollapsedControlText;

			public IntPtr hFooterIcon;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszFooterText;

			[MarshalAs(UnmanagedType.FunctionPtr)]
			public TaskDialogCallback pfCallback;

			public IntPtr lpCallbackData;

			public uint cxWidth;
		}

		public struct ACTCTX
		{
			public int cbSize;

			public uint dwFlags;

			public string lpSource;

			public ushort wProcessorArchitecture;

			public ushort wLangId;

			public string lpAssemblyDirectory;

			public string lpResourceName;

			public string lpApplicationName;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
		internal struct COMDLG_FILTERSPEC
		{
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszName;

			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszSpec;
		}

		internal enum FDAP
		{
			FDAP_BOTTOM = 0,
			FDAP_TOP = 1
		}

		internal enum FDE_SHAREVIOLATION_RESPONSE
		{
			FDESVR_DEFAULT = 0,
			FDESVR_ACCEPT = 1,
			FDESVR_REFUSE = 2
		}

		internal enum FDE_OVERWRITE_RESPONSE
		{
			FDEOR_DEFAULT = 0,
			FDEOR_ACCEPT = 1,
			FDEOR_REFUSE = 2
		}

		internal enum SIATTRIBFLAGS
		{
			SIATTRIBFLAGS_AND = 1,
			SIATTRIBFLAGS_OR = 2,
			SIATTRIBFLAGS_APPCOMPAT = 3
		}

		internal enum SIGDN : uint
		{
			SIGDN_NORMALDISPLAY = 0u,
			SIGDN_PARENTRELATIVEPARSING = 2147581953u,
			SIGDN_DESKTOPABSOLUTEPARSING = 2147647488u,
			SIGDN_PARENTRELATIVEEDITING = 2147684353u,
			SIGDN_DESKTOPABSOLUTEEDITING = 2147794944u,
			SIGDN_FILESYSPATH = 2147844096u,
			SIGDN_URL = 2147909632u,
			SIGDN_PARENTRELATIVEFORADDRESSBAR = 2147991553u,
			SIGDN_PARENTRELATIVE = 2148007937u
		}

		[Flags]
		internal enum FOS : uint
		{
			FOS_OVERWRITEPROMPT = 2u,
			FOS_STRICTFILETYPES = 4u,
			FOS_NOCHANGEDIR = 8u,
			FOS_PICKFOLDERS = 0x20u,
			FOS_FORCEFILESYSTEM = 0x40u,
			FOS_ALLNONSTORAGEITEMS = 0x80u,
			FOS_NOVALIDATE = 0x100u,
			FOS_ALLOWMULTISELECT = 0x200u,
			FOS_PATHMUSTEXIST = 0x800u,
			FOS_FILEMUSTEXIST = 0x1000u,
			FOS_CREATEPROMPT = 0x2000u,
			FOS_SHAREAWARE = 0x4000u,
			FOS_NOREADONLYRETURN = 0x8000u,
			FOS_NOTESTFILECREATE = 0x10000u,
			FOS_HIDEMRUPLACES = 0x20000u,
			FOS_HIDEPINNEDPLACES = 0x40000u,
			FOS_NODEREFERENCELINKS = 0x100000u,
			FOS_DONTADDTORECENT = 0x2000000u,
			FOS_FORCESHOWHIDDEN = 0x10000000u,
			FOS_DEFAULTNOMINIMODE = 0x20000000u
		}

		internal enum CDCONTROLSTATE
		{
			CDCS_INACTIVE = 0,
			CDCS_ENABLED = 1,
			CDCS_VISIBLE = 2
		}

		internal enum FFFP_MODE
		{
			FFFP_EXACTMATCH = 0,
			FFFP_NEARESTPARENTMATCH = 1
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
		internal struct KNOWNFOLDER_DEFINITION
		{
			internal KF_CATEGORY category;

			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszName;

			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszCreator;

			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszDescription;

			internal Guid fidParent;

			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszRelativePath;

			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszParsingName;

			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszToolTip;

			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszLocalizedName;

			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszIcon;

			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszSecurity;

			internal uint dwAttributes;

			internal KF_DEFINITION_FLAGS kfdFlags;

			internal Guid ftidType;
		}

		internal enum KF_CATEGORY
		{
			KF_CATEGORY_VIRTUAL = 1,
			KF_CATEGORY_FIXED = 2,
			KF_CATEGORY_COMMON = 3,
			KF_CATEGORY_PERUSER = 4
		}

		[Flags]
		internal enum KF_DEFINITION_FLAGS
		{
			KFDF_PERSONALIZE = 1,
			KFDF_LOCAL_REDIRECT_ONLY = 2,
			KFDF_ROAMABLE = 4
		}

		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PROPERTYKEY
		{
			internal Guid fmtid;

			internal uint pid;
		}

		[Flags]
		public enum FormatMessageFlags
		{
			FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x100,
			FORMAT_MESSAGE_IGNORE_INSERTS = 0x200,
			FORMAT_MESSAGE_FROM_STRING = 0x400,
			FORMAT_MESSAGE_FROM_HMODULE = 0x800,
			FORMAT_MESSAGE_FROM_SYSTEM = 0x1000,
			FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x2000
		}

		public enum HitTestResult
		{
			Error = -2,
			Transparent = -1,
			Nowhere = 0,
			Client = 1,
			Caption = 2,
			SysMenu = 3,
			GrowBox = 4,
			Size = 4,
			Menu = 5,
			HScroll = 6,
			VScroll = 7,
			MinButton = 8,
			MaxButton = 9,
			Left = 10,
			Right = 11,
			Top = 12,
			TopLeft = 13,
			TopRight = 14,
			Bottom = 15,
			BottomLeft = 16,
			BottomRight = 17,
			Border = 18,
			Reduce = 8,
			Zoom = 9,
			SizeFirst = 10,
			SizeLast = 17,
			Object = 19,
			Close = 20,
			Help = 21
		}

		public struct MARGINS
		{
			public int Left;

			public int Right;

			public int Top;

			public int Bottom;

			public MARGINS(Padding value)
			{
				Left = value.Left;
				Right = value.Right;
				Top = value.Top;
				Bottom = value.Bottom;
			}
		}

		public struct DTTOPTS
		{
			public int dwSize;

			[MarshalAs(UnmanagedType.U4)]
			public DrawThemeTextFlags dwFlags;

			public int crText;

			public int crBorder;

			public int crShadow;

			public int iTextShadowType;

			public Point ptShadowOffset;

			public int iBorderSize;

			public int iFontPropId;

			public int iColorPropId;

			public int iStateId;

			public bool fApplyOverlay;

			public int iGlowSize;

			public int pfnDrawTextCallback;

			public IntPtr lParam;
		}

		[Flags]
		public enum DrawThemeTextFlags
		{
			TextColor = 1,
			BorderColor = 2,
			ShadowColor = 4,
			ShadowType = 8,
			ShadowOffset = 0x10,
			BorderSize = 0x20,
			FontProp = 0x40,
			ColorProp = 0x80,
			StateId = 0x100,
			CalcRect = 0x200,
			ApplyOverlay = 0x400,
			GlowSize = 0x800,
			Callback = 0x1000,
			Composited = 0x2000
		}

		[StructLayout(LayoutKind.Sequential)]
		public class BITMAPINFO
		{
			public int biSize;

			public int biWidth;

			public int biHeight;

			public short biPlanes;

			public short biBitCount;

			public int biCompression;

			public int biSizeImage;

			public int biXPelsPerMeter;

			public int biYPelsPerMeter;

			public int biClrUsed;

			public int biClrImportant;

			public byte bmiColors_rgbBlue;

			public byte bmiColors_rgbGreen;

			public byte bmiColors_rgbRed;

			public byte bmiColors_rgbReserved;
		}

		public struct RECT
		{
			public int Left;

			public int Top;

			public int Right;

			public int Bottom;

			public RECT(int left, int top, int right, int bottom)
			{
				Left = left;
				Top = top;
				Right = right;
				Bottom = bottom;
			}

			public RECT(Rectangle rectangle)
			{
				Left = rectangle.X;
				Top = rectangle.Y;
				Right = rectangle.Right;
				Bottom = rectangle.Bottom;
			}

			public override string ToString()
			{
				return "Left: " + Left + ", Top: " + Top + ", Right: " + Right + ", Bottom: " + Bottom;
			}
		}

		[Flags]
		public enum CREDUI_FLAGS
		{
			INCORRECT_PASSWORD = 1,
			DO_NOT_PERSIST = 2,
			REQUEST_ADMINISTRATOR = 4,
			EXCLUDE_CERTIFICATES = 8,
			REQUIRE_CERTIFICATE = 0x10,
			SHOW_SAVE_CHECK_BOX = 0x40,
			ALWAYS_SHOW_UI = 0x80,
			REQUIRE_SMARTCARD = 0x100,
			PASSWORD_ONLY_OK = 0x200,
			VALIDATE_USERNAME = 0x400,
			COMPLETE_USERNAME = 0x800,
			PERSIST = 0x1000,
			SERVER_CREDENTIAL = 0x4000,
			EXPECT_CONFIRMATION = 0x20000,
			GENERIC_CREDENTIALS = 0x40000,
			USERNAME_TARGET_CREDENTIALS = 0x80000,
			KEEP_USERNAME = 0x100000
		}

		[Flags]
		public enum CredUIWinFlags
		{
			Generic = 1,
			Checkbox = 2,
			AutoPackageOnly = 0x10,
			InCredOnly = 0x20,
			EnumerateAdmins = 0x100,
			EnumerateCurrentUser = 0x200,
			SecurePrompt = 0x1000,
			Pack32Wow = 0x10000000
		}

		internal enum CredUIReturnCodes
		{
			NO_ERROR = 0,
			ERROR_CANCELLED = 1223,
			ERROR_NO_SUCH_LOGON_SESSION = 1312,
			ERROR_NOT_FOUND = 1168,
			ERROR_INVALID_ACCOUNT_NAME = 1315,
			ERROR_INSUFFICIENT_BUFFER = 122,
			ERROR_INVALID_PARAMETER = 87,
			ERROR_INVALID_FLAGS = 1004
		}

		internal enum CredTypes
		{
			CRED_TYPE_GENERIC = 1,
			CRED_TYPE_DOMAIN_PASSWORD = 2,
			CRED_TYPE_DOMAIN_CERTIFICATE = 3,
			CRED_TYPE_DOMAIN_VISIBLE_PASSWORD = 4
		}

		internal enum CredPersist
		{
			Session = 1,
			LocalMachine = 2,
			Enterprise = 3
		}

		internal struct CREDUI_INFO
		{
			public int cbSize;

			public IntPtr hwndParent;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszMessageText;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszCaptionText;

			public IntPtr hbmBanner;
		}

		public struct CREDENTIAL
		{
			public int Flags;

			public CredTypes Type;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string TargetName;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string Comment;

			public long LastWritten;

			public uint CredentialBlobSize;

			public IntPtr CredentialBlob;

			[MarshalAs(UnmanagedType.U4)]
			public CredPersist Persist;

			public int AttributeCount;

			public IntPtr Attributes;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string TargetAlias;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string UserName;
		}

		public const int ErrorFileNotFound = 2;

		public const int WM_USER = 1024;

		public const int WM_GETICON = 127;

		public const int WM_SETICON = 128;

		public const int ICON_SMALL = 0;

		public const int ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID = 4;

		public const int WM_NCHITTEST = 132;

		public const int WM_DWMCOMPOSITIONCHANGED = 798;

		internal const int CREDUI_MAX_USERNAME_LENGTH = 513;

		internal const int CREDUI_MAX_PASSWORD_LENGTH = 256;

		public static bool IsWindowsVistaOrLater
		{
			get
			{
				return Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version >= new Version(6, 0, 6000);
			}
		}

		public static bool IsWindowsXPOrLater
		{
			get
			{
				return Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version >= new Version(5, 1, 2600);
			}
		}

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern SafeModuleHandle LoadLibraryEx(string lpFileName, IntPtr hFile, LoadLibraryExFlags dwFlags);

		[DllImport("kernel32", SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool FreeLibrary(IntPtr hModule);

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetActiveWindow();

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetCurrentThreadId();

		[DllImport("comctl32.dll", PreserveSig = false)]
		public static extern void TaskDialogIndirect([In] ref TASKDIALOGCONFIG pTaskConfig, out int pnButton, out int pnRadioButton, [MarshalAs(UnmanagedType.Bool)] out bool pfVerificationFlagChecked);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

		[DllImport("Kernel32.dll", SetLastError = true)]
		public static extern ActivationContextSafeHandle CreateActCtx(ref ACTCTX actctx);

		[DllImport("kernel32.dll")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static extern void ReleaseActCtx(IntPtr hActCtx);

		[DllImport("Kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ActivateActCtx(ActivationContextSafeHandle hActCtx, out IntPtr lpCookie);

		[DllImport("Kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeactivateActCtx(uint dwFlags, IntPtr lpCookie);

		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		public static extern int SHCreateItemFromParsingName([MarshalAs(UnmanagedType.LPWStr)] string pszPath, IntPtr pbc, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);

		public static IShellItem CreateItemFromParsingName(string path)
		{
			Guid riid = new Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe");
			object ppv;
			int num = SHCreateItemFromParsingName(path, IntPtr.Zero, ref riid, out ppv);
			if (num != 0)
			{
				throw new Win32Exception(num);
			}
			return (IShellItem)ppv;
		}

		[DllImport("user32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
		public static extern int LoadString(SafeModuleHandle hInstance, uint uID, StringBuilder lpBuffer, int nBufferMax);

		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern uint FormatMessage([MarshalAs(UnmanagedType.U4)] FormatMessageFlags dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer, uint nSize, string[] Arguments);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, [In] ref MARGINS pMarInset);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DwmIsCompositionEnabled();

		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern SafeDeviceHandle CreateCompatibleDC(IntPtr hDC);

		[DllImport("gdi32.dll", ExactSpelling = true)]
		public static extern IntPtr SelectObject(SafeDeviceHandle hDC, SafeGDIHandle hObject);

		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteObject(IntPtr hObject);

		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteDC(IntPtr hdc);

		[DllImport("gdi32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, SafeDeviceHandle hdcSrc, int nXSrc, int nYSrc, uint dwRop);

		[DllImport("UxTheme.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
		public static extern void DrawThemeTextEx(IntPtr hTheme, SafeDeviceHandle hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref RECT pRect, ref DTTOPTS pOptions);

		[DllImport("gdi32.dll")]
		public static extern SafeGDIHandle CreateDIBSection(IntPtr hdc, BITMAPINFO pbmi, uint iUsage, IntPtr ppvBits, IntPtr hSection, uint dwOffset);

		[DllImport("UxTheme.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
		public static extern void GetThemeTextExtent(IntPtr hTheme, SafeDeviceHandle hdc, int iPartId, int iStateId, string text, int iCharCount, int dwTextFlags, [In] ref RECT bounds, out RECT rect);

		public static SafeGDIHandle CreateDib(Rectangle bounds, IntPtr primaryHdc, SafeDeviceHandle memoryHdc)
		{
			BITMAPINFO bITMAPINFO = new BITMAPINFO();
			bITMAPINFO.biSize = Marshal.SizeOf(bITMAPINFO);
			bITMAPINFO.biWidth = bounds.Width;
			bITMAPINFO.biHeight = -bounds.Height;
			bITMAPINFO.biPlanes = 1;
			bITMAPINFO.biBitCount = 32;
			bITMAPINFO.biCompression = 0;
			SafeGDIHandle safeGDIHandle = CreateDIBSection(primaryHdc, bITMAPINFO, 0u, IntPtr.Zero, IntPtr.Zero, 0u);
			SelectObject(memoryHdc, safeGDIHandle);
			return safeGDIHandle;
		}

		[DllImport("credui.dll", CharSet = CharSet.Unicode)]
		internal static extern CredUIReturnCodes CredUIPromptForCredentials(ref CREDUI_INFO pUiInfo, string targetName, IntPtr Reserved, int dwAuthError, StringBuilder pszUserName, uint ulUserNameMaxChars, StringBuilder pszPassword, uint ulPaswordMaxChars, [In][Out][MarshalAs(UnmanagedType.Bool)] ref bool pfSave, CREDUI_FLAGS dwFlags);

		[DllImport("credui.dll", CharSet = CharSet.Unicode)]
		public static extern CredUIReturnCodes CredUIPromptForWindowsCredentials(ref CREDUI_INFO pUiInfo, uint dwAuthError, ref uint pulAuthPackage, IntPtr pvInAuthBuffer, uint ulInAuthBufferSize, out IntPtr ppvOutAuthBuffer, out uint pulOutAuthBufferSize, [MarshalAs(UnmanagedType.Bool)] ref bool pfSave, CredUIWinFlags dwFlags);

		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CredReadW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CredRead(string TargetName, CredTypes Type, int Flags, out IntPtr Credential);

		[DllImport("advapi32.dll")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static extern void CredFree(IntPtr Buffer);

		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CredDeleteW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CredDelete(string TargetName, CredTypes Type, int Flags);

		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "CredWriteW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CredWrite(ref CREDENTIAL Credential, int Flags);

		[DllImport("credui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CredPackAuthenticationBuffer(uint dwFlags, string pszUserName, string pszPassword, IntPtr pPackedCredentials, ref uint pcbPackedCredentials);

		[DllImport("credui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CredUnPackAuthenticationBuffer(uint dwFlags, IntPtr pAuthBuffer, uint cbAuthBuffer, StringBuilder pszUserName, ref uint pcchMaxUserName, StringBuilder pszDomainName, ref uint pcchMaxDomainName, StringBuilder pszPassword, ref uint pcchMaxPassword);
	}
}
