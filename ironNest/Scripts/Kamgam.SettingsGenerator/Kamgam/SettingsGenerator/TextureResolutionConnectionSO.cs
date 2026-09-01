using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "TextureResolutionConnection", menuName = "SettingsGenerator/Connection/TextureResolutionConnection", order = 4)]
	public class TextureResolutionConnectionSO : OptionConnectionSO
	{
		protected TextureResolutionConnection _connection;

		public override IConnectionWithOptions<string> GetConnection()
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
