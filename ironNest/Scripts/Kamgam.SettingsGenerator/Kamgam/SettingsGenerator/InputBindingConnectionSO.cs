using UnityEngine;
using UnityEngine.InputSystem;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "InputBindingConnection", menuName = "SettingsGenerator/Connection/InputBindingConnection", order = 4)]
	public class InputBindingConnectionSO : StringConnectionSO
	{
		public InputActionAsset InputActionAsset;

		public string BindingId;

		protected InputBindingConnection _connection;

		public override IConnection<string> GetConnection()
		{
			return null;
		}

		public void Create()
		{
		}

		public override void DestroyConnection()
		{
		}
	}
}
