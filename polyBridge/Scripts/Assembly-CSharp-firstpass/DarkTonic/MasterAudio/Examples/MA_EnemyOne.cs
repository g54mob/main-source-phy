using UnityEngine;

namespace DarkTonic.MasterAudio.Examples
{
	public class MA_EnemyOne : MonoBehaviour
	{
		public GameObject ExplosionParticlePrefab;

		private Transform _trans;

		private float _speed;

		private float _horizSpeed;

		private void Awake()
		{
			base.useGUILayout = false;
			_trans = base.transform;
			_speed = (float)Random.Range(-3, -8) * AudioUtil.FrameTime;
			_horizSpeed = (float)Random.Range(-3, 3) * AudioUtil.FrameTime;
			Debug.LogError("MA_EnemyOne and this example Scene will not work properly without Physics3D package installed. Please enable it in the Master Audio Welcome Window if it's already installed.");
		}

		private void Update()
		{
			Vector3 position = _trans.position;
			position.x += _horizSpeed;
			position.y += _speed;
			_trans.position = position;
			_trans.Rotate(Vector3.down * 300f * AudioUtil.FrameTime);
			if (_trans.position.y < -5f)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}
}
