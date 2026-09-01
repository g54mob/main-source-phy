using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Gyroscope/MMGyroscope")]
	public class MMGyroscope : MonoBehaviour
	{
		public enum TimeScales
		{
			Scaled = 0,
			Unscaled = 1
		}

		public static bool GyroscopeActive;

		public static TimeScales TimeScale;

		public static Vector2 Clamps;

		public static float LerpSpeed;

		public static bool TestMode;

		[Header("Debug")]
		public bool _TestMode;

		[Range(-1f, 1f)]
		public float TestXAcceleration;

		[Range(-1f, 1f)]
		public float TestYAcceleration;

		[Range(-1f, 1f)]
		public float TestZAcceleration;

		private static Quaternion m_GyroscopeAttitude;

		private static Vector3 m_GyroscopeRotationRate;

		private static Vector3 m_GyroscopeAcceleration;

		private static Vector3 m_InputAcceleration;

		private static Vector3 m_GyroscopeGravity;

		private static Quaternion m_InitialGyroscopeAttitude;

		private static Vector3 m_InitialGyroscopeRotationRate;

		private static Vector3 m_InitialGyroscopeAcceleration;

		private static Vector3 m_InitialInputAcceleration;

		private static Vector3 m_InitialGyroscopeGravity;

		private static Vector3 m_CalibratedInputAcceleration;

		private static Vector3 m_CalibratedGyroscopeGravity;

		private static Vector3 m_LerpedCalibratedInputAcceleration;

		private static Vector3 m_LerpedCalibratedGyroscopeGravity;

		[Header("Settings")]
		[SerializeField]
		private TimeScales _TimeScale;

		[SerializeField]
		private Vector2 _Clamps;

		[SerializeField]
		private float _LerpSpeed;

		[Header("Raw Values")]
		[MMReadOnly]
		[SerializeField]
		private Quaternion _GyroscopeAttitude;

		[MMReadOnly]
		[SerializeField]
		private Vector3 _GyroscopeRotationRate;

		[MMReadOnly]
		[SerializeField]
		private Vector3 _GyroscopeAcceleration;

		[MMReadOnly]
		[SerializeField]
		private Vector3 _InputAcceleration;

		[MMReadOnly]
		[SerializeField]
		private Vector3 _GyroscopeGravity;

		[Header("AutoCalibration Values")]
		[MMReadOnly]
		[SerializeField]
		private Quaternion _InitialGyroscopeAttitude;

		[MMReadOnly]
		[SerializeField]
		private Vector3 _InitialGyroscopeRotationRate;

		[MMReadOnly]
		[SerializeField]
		private Vector3 _InitialGyroscopeAcceleration;

		[MMReadOnly]
		[SerializeField]
		private Vector3 _InitialInputAcceleration;

		[MMReadOnly]
		[SerializeField]
		private Vector3 _InitialGyroscopeGravity;

		[Header("Relative Values")]
		[MMReadOnly]
		[SerializeField]
		private Vector3 _CalibratedInputAcceleration;

		[MMReadOnly]
		[SerializeField]
		private Vector3 _CalibratedGyroscopeGravity;

		[Header("Lerped Values")]
		[MMReadOnly]
		[SerializeField]
		private Vector3 _LerpedCalibratedInputAcceleration;

		[MMReadOnly]
		[SerializeField]
		private Vector3 _LerpedCalibratedGyroscopeGravity;

		[MMInspectorButton("Calibrate")]
		public bool CalibrateButton;

		private static Gyroscope _gyroscope;

		protected static Vector3 _testVector;

		private static bool _initialized;

		private static Matrix4x4 _accelerationMatrix;

		private static Matrix4x4 _gravityMatrix;

		private static float _lastGetValuesAt;

		public static Quaternion GyroscopeAttitude => default(Quaternion);

		public static Vector3 GyroscopeRotationRate => default(Vector3);

		public static Vector3 GyroscopeAcceleration => default(Vector3);

		public static Vector3 InputAcceleration => default(Vector3);

		public static Vector3 GyroscopeGravity => default(Vector3);

		public static Quaternion InitialGyroscopeAttitude => default(Quaternion);

		public static Vector3 InitialGyroscopeRotationRate => default(Vector3);

		public static Vector3 InitialGyroscopeAcceleration => default(Vector3);

		public static Vector3 InitialInputAcceleration => default(Vector3);

		public static Vector3 InitialGyroscopeGravity => default(Vector3);

		public static Vector3 CalibratedInputAcceleration => default(Vector3);

		public static Vector3 CalibratedGyroscopeGravity => default(Vector3);

		public static Vector3 LerpedCalibratedInputAcceleration => default(Vector3);

		public static Vector3 LerpedCalibratedGyroscopeGravity => default(Vector3);

		protected virtual void Start()
		{
		}

		public static void GyroscopeInitialization()
		{
		}

		protected virtual void Update()
		{
		}

		public static void GetValues()
		{
		}

		private static void GetGyroValues()
		{
		}

		private static void AutoCalibration()
		{
		}

		protected static Quaternion GyroscopeToUnity(Quaternion q)
		{
			return default(Quaternion);
		}

		private static void ClampAcceleration()
		{
		}

		protected virtual void HandleTestMode()
		{
		}

		private static void GetAccelerationAndGravity()
		{
		}

		private static void Calibrate()
		{
		}

		private static Matrix4x4 CalibrateAcceleration(Vector3 initialAcceleration)
		{
			return default(Matrix4x4);
		}

		private static Vector3 CalibratedAcceleration(Vector3 accelerator, Matrix4x4 matrix)
		{
			return default(Vector3);
		}
	}
}
