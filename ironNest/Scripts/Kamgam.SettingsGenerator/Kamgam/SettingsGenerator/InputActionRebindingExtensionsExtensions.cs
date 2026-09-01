using UnityEngine.InputSystem;

namespace Kamgam.SettingsGenerator
{
	public static class InputActionRebindingExtensionsExtensions
	{
		public static bool FindBinding(this InputActionAsset inputActionAsset, string bindingId, out InputBinding binding)
		{
			binding = default(InputBinding);
			return false;
		}

		public static InputAction GetActionOfBinding(this InputActionAsset inputActionAsset, string bindingId)
		{
			return null;
		}

		public static InputActionMap GetActionMapOfBinding(this InputActionAsset inputActionAsset, string bindingId)
		{
			return null;
		}

		public static int GetBindingIndexWithinActionMap(this InputActionAsset inputActionAsset, string bindingId)
		{
			return 0;
		}

		public static int GetBindingIndexWithinAction(this InputActionAsset inputActionAsset, string bindingId)
		{
			return 0;
		}

		public static void ApplyBindingOverride(this InputActionAsset inputActionAsset, string bindingId, string overridePath, string overrideInteractions = null, string overrideProcessors = null)
		{
		}

		public static bool ApplyBindingOverrideWithResult(this InputActionAsset inputActionAsset, string bindingId, string overridePath, string overrideInteractions = null, string overrideProcessors = null)
		{
			return false;
		}

		public static void ClearOverride(this InputActionAsset inputActionAsset, string bindingId)
		{
		}
	}
}
