using UnityEngine;

namespace TMPro.Examples
{
	public class ObjectSpin : MonoBehaviour
	{
		public enum MotionType
		{
			Rotation = 0,
			SearchLight = 1,
			Translation = 2
		}

		public MotionType Motion;

		public Vector3 TranslationDistance;

		public float TranslationSpeed;

		public float SpinSpeed;

		public int RotationRange;

		private Transform m_transform;

		private float m_time;

		private Vector3 m_prevPOS;

		private Vector3 m_initial_Rotation;

		private Vector3 m_initial_Position;

		private Color32 m_lightColor;

		private void Awake()
		{
		}

		private void Update()
		{
		}
	}
}
