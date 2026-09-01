using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "ColorGradeConnection", menuName = "SettingsGenerator/Connection/ColorGradeConnection", order = 4)]
	public class ColorGradeConnectionSO : FloatConnectionSO
	{
		public ColorGradeConnection.ColorGradeEffect Effect;

		protected ColorGradeConnection _connection;

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
