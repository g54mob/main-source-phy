using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "CameraClipConnection", menuName = "SettingsGenerator/Connection/CameraClipConnection", order = 4)]
	public class CameraClipConnectionSO : FloatConnectionSO
	{
		public CameraClipConnection.ClippingMode Mode;

		public float ClipMin;

		public float ClipMax;

		public bool UseMain;

		public bool UseMarkers;

		protected CameraClipConnection _connection;

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
