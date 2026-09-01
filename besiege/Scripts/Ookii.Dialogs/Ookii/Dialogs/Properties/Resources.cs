using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;

namespace Ookii.Dialogs.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (resourceMan == null)
				{
					ResourceManager resourceManager = new ResourceManager("Ookii.Dialogs.Properties.Resources", typeof(Resources).Assembly);
					resourceMan = resourceManager;
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal static string AnimationLoadErrorFormat
		{
			get
			{
				return ResourceManager.GetString("AnimationLoadErrorFormat", resourceCulture);
			}
		}

		internal static string CredentialEmptyTargetError
		{
			get
			{
				return ResourceManager.GetString("CredentialEmptyTargetError", resourceCulture);
			}
		}

		internal static string CredentialError
		{
			get
			{
				return ResourceManager.GetString("CredentialError", resourceCulture);
			}
		}

		internal static string CredentialPromptNotCalled
		{
			get
			{
				return ResourceManager.GetString("CredentialPromptNotCalled", resourceCulture);
			}
		}

		internal static string DuplicateButtonTypeError
		{
			get
			{
				return ResourceManager.GetString("DuplicateButtonTypeError", resourceCulture);
			}
		}

		internal static string DuplicateItemIdError
		{
			get
			{
				return ResourceManager.GetString("DuplicateItemIdError", resourceCulture);
			}
		}

		internal static string FileNotFoundFormat
		{
			get
			{
				return ResourceManager.GetString("FileNotFoundFormat", resourceCulture);
			}
		}

		internal static string GlassNotSupportedError
		{
			get
			{
				return ResourceManager.GetString("GlassNotSupportedError", resourceCulture);
			}
		}

		internal static string Help
		{
			get
			{
				return ResourceManager.GetString("Help", resourceCulture);
			}
		}

		internal static string InvalidFilterString
		{
			get
			{
				return ResourceManager.GetString("InvalidFilterString", resourceCulture);
			}
		}

		internal static string InvalidTaskDialogItemIdError
		{
			get
			{
				return ResourceManager.GetString("InvalidTaskDialogItemIdError", resourceCulture);
			}
		}

		internal static string NoAssociatedTaskDialogError
		{
			get
			{
				return ResourceManager.GetString("NoAssociatedTaskDialogError", resourceCulture);
			}
		}

		internal static string NonCustomTaskDialogButtonIdError
		{
			get
			{
				return ResourceManager.GetString("NonCustomTaskDialogButtonIdError", resourceCulture);
			}
		}

		internal static string Preview
		{
			get
			{
				return ResourceManager.GetString("Preview", resourceCulture);
			}
		}

		internal static string ProgressDialogNotRunningError
		{
			get
			{
				return ResourceManager.GetString("ProgressDialogNotRunningError", resourceCulture);
			}
		}

		internal static string ProgressDialogRunning
		{
			get
			{
				return ResourceManager.GetString("ProgressDialogRunning", resourceCulture);
			}
		}

		internal static string TaskDialogEmptyButtonLabelError
		{
			get
			{
				return ResourceManager.GetString("TaskDialogEmptyButtonLabelError", resourceCulture);
			}
		}

		internal static string TaskDialogIllegalCrossThreadCallError
		{
			get
			{
				return ResourceManager.GetString("TaskDialogIllegalCrossThreadCallError", resourceCulture);
			}
		}

		internal static string TaskDialogItemHasOwnerError
		{
			get
			{
				return ResourceManager.GetString("TaskDialogItemHasOwnerError", resourceCulture);
			}
		}

		internal static string TaskDialogNoButtonsError
		{
			get
			{
				return ResourceManager.GetString("TaskDialogNoButtonsError", resourceCulture);
			}
		}

		internal static string TaskDialogNotRunningError
		{
			get
			{
				return ResourceManager.GetString("TaskDialogNotRunningError", resourceCulture);
			}
		}

		internal static string TaskDialogRunningError
		{
			get
			{
				return ResourceManager.GetString("TaskDialogRunningError", resourceCulture);
			}
		}

		internal static string TaskDialogsNotSupportedError
		{
			get
			{
				return ResourceManager.GetString("TaskDialogsNotSupportedError", resourceCulture);
			}
		}

		internal Resources()
		{
		}
	}
}
