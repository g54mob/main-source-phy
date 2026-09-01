using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "AlteregoDLSSConnection", menuName = "SettingsGenerator/Connection/AlteregoDLSSConnection", order = 4)]
	public class AlteregoDLSSConnectionSO : OptionConnectionSO
	{
		protected AlteregoDLSSConnection _connection;

		[Tooltip("If enabled then the camera detection will  search (an prefer) cameras with the SettingsMainCameraMarker component on it.")]
		public bool CheckForCameraMarker;

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
