using UnityEngine;

namespace Battlehub.Cubeman
{
	public class GameCameraFollow : MonoBehaviour
	{
		private Transform m_target;

		public float distance = 5f;

		public float height = 5f;

		public float rotationDamping = 12f;

		public float heightDamping = 2f;

		[SerializeField]
		public Transform target
		{
			get
			{
				return m_target;
			}
			set
			{
				m_target = value;
				float num = rotationDamping;
				float num2 = heightDamping;
				rotationDamping = float.MaxValue;
				heightDamping = float.MaxValue;
				heightDamping = num2;
				rotationDamping = num;
			}
		}

		private void Start()
		{
		}

		private void LateUpdate()
		{
			if ((bool)target)
			{
				Follow();
			}
		}

		public void Follow()
		{
			float y = target.eulerAngles.y;
			float b = target.position.y + height;
			float y2 = base.transform.eulerAngles.y;
			float y3 = base.transform.position.y;
			y2 = Mathf.LerpAngle(y2, y, rotationDamping * Time.deltaTime);
			y3 = Mathf.Lerp(y3, b, heightDamping * Time.deltaTime);
			Quaternion quaternion = Quaternion.Euler(0f, y2, 0f);
			base.transform.position = target.position;
			base.transform.position -= quaternion * Vector3.forward * distance;
			base.transform.position = new Vector3(base.transform.position.x, y3, base.transform.position.z);
			base.transform.LookAt(target);
		}
	}
}
