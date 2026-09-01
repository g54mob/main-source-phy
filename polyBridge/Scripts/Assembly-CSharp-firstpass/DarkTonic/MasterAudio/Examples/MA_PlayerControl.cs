using UnityEngine;

namespace DarkTonic.MasterAudio.Examples
{
	public class MA_PlayerControl : MonoBehaviour
	{
		public GameObject ProjectilePrefab;

		public bool canShoot = true;

		private const float MoveSpeed = 10f;

		private Transform _trans;

		private float _lastMoveAmt;

		private void Awake()
		{
			base.useGUILayout = false;
			_trans = base.transform;
			Debug.LogError("MA_PlayerControl and this example Scene will not work properly without Physics3D package installed. Please enable it in the Master Audio Welcome Window if it's already installed.");
		}

		private void OnDisable()
		{
		}

		private void OnBecameInvisible()
		{
		}

		private void OnBecameVisible()
		{
		}

		private void Update()
		{
			float num = Input.GetAxis("Horizontal") * 10f * AudioUtil.FrameTime;
			if (num != 0f)
			{
				if (_lastMoveAmt == 0f)
				{
					MasterAudio.FireCustomEvent("PlayerMoved", _trans);
				}
			}
			else if (_lastMoveAmt != 0f)
			{
				MasterAudio.FireCustomEvent("PlayerStoppedMoving", _trans);
			}
			_lastMoveAmt = num;
			Vector3 position = _trans.position;
			position.x += num;
			_trans.position = position;
			if (canShoot && Input.GetMouseButtonDown(0))
			{
				Vector3 position2 = _trans.position;
				position2.y += 1f;
				Object.Instantiate(ProjectilePrefab, position2, ProjectilePrefab.transform.rotation);
			}
		}
	}
}
