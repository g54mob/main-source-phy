using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Kamgam.SettingsGenerator
{
	public class InputBindingConnection : Connection<string>, IConnectionWithProviderAccess
	{
		public static List<InputBindingConnection> Connections;

		public static bool LogErrorOnBindingFail;

		protected InputActionAsset _inputActionAsset;

		protected string _bindingId;

		private static List<InputActionAsset> _tmpAssets;

		protected SettingsProvider _provider;

		public void SetBindingId(string id)
		{
		}

		public string GetBindingId()
		{
			return null;
		}

		public void SetInputActionAsset(InputActionAsset asset)
		{
		}

		public InputActionAsset GetInputActionAsset()
		{
			return null;
		}

		public void ClearOverride()
		{
		}

		public override string Get()
		{
			return null;
		}

		public override string GetDefault()
		{
			return null;
		}

		protected string getBindingPath(bool getDefault)
		{
			return null;
		}

		public bool IsComposite()
		{
			return false;
		}

		protected string getPathsFromComposite(InputBinding binding, bool getDefault)
		{
			return null;
		}

		private static void logNoInputAssetError()
		{
		}

		private void logNoBindingError()
		{
		}

		public override void Set(string overridePath)
		{
		}

		private string applyOverridesToActionAsset(InputActionAsset inputActionAsset, string overridePath)
		{
			return null;
		}

		protected void setPathsOnComposite(InputBinding binding, string compositePath)
		{
		}

		public void SetProvider(SettingsProvider provider)
		{
		}

		public SettingsProvider GetProvider()
		{
			return null;
		}
	}
}
