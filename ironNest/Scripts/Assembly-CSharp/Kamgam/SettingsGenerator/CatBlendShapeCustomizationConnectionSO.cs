using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "CatBlendShapeCustomizationConnection", menuName = "SettingsGenerator/Connection/Cat/CatBlendShapeCustomization")]
	public class CatBlendShapeCustomizationConnectionSO : FloatConnectionSO
	{
		public bool Eyes;

		public bool Body;

		public bool Fur;

		public bool Whiskers;

		public int BlendShapeIndex;

		protected CatBlendShapeCustomizationConnection _connection;

		public override IConnection<float> GetConnection()
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
