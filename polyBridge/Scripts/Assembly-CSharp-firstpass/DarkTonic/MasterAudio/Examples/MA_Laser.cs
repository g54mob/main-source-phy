using UnityEngine;

namespace DarkTonic.MasterAudio.Examples
{
	public class MA_Laser : MonoBehaviour
	{
		private Transform _trans;

		private void Awake()
		{
			base.useGUILayout = false;
			_trans = base.transform;
			Debug.LogError("MA_Laser and this example Scene will not work properly without Physics3D package installed. Please enable it in the Master Audio Welcome Window if it's already installed.");
		}

		private void Update()
		{
			float num = 10f * AudioUtil.FrameTime;
			Vector3 position = _trans.position;
			position.y += num;
			_trans.position = position;
			if (_trans.position.y > 7f)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}
}
