using CodeAnimo.UnityExtensionMethods;
using UnityEngine;

namespace CodeAnimo
{
	public class CameraFreeFlight : MonoBehaviour
	{
		public float movementSpeedOrAcceleration = 100f;

		public Vector2 mouseSensitivity = Vector2.one;

		public float rollCorrectionSpeed = 0.3f;

		protected Vector3 yawAxis = Vector3.up;

		public MouseButton rotationMouseButton = MouseButton.Right;

		public string sidewaysAxis = "Horizontal";

		public string forwardsAxis = "Vertical";

		public string verticalAxis = "Jump";

		public string mouseXAxis = "Mouse X";

		public string mouseYAxis = "Mouse Y";

		public Vector3 upVector = Vector3.up;

		[SerializeField]
		protected bool m_invertMouseX;

		[SerializeField]
		protected bool m_invertMouseY = true;

		public bool smoothedMovement = true;

		public bool smoothedRotation = true;

		public bool rollCorrection = true;

		[SerializeField]
		[HideInInspector]
		private float m_mouseXModifier = 1f;

		[SerializeField]
		[HideInInspector]
		private float m_mouseYModifier = -1f;

		private Rigidbody m_cachedRigidBody;

		private Vector3 m_TranslationAccelerationDirection = Vector3.zero;

		private float m_YawPerUpdate;

		private float m_tiltPerUpdate;

		private int m_framesSinceUpdate;

		public bool invertMouseX
		{
			get
			{
				return m_invertMouseX;
			}
			set
			{
				m_invertMouseX = value;
				if (m_invertMouseX)
				{
					m_mouseXModifier = -1f;
				}
				else
				{
					m_mouseXModifier = 1f;
				}
			}
		}

		public bool invertMouseY
		{
			get
			{
				return m_invertMouseY;
			}
			set
			{
				m_invertMouseY = value;
				if (m_invertMouseY)
				{
					m_mouseYModifier = -1f;
				}
				else
				{
					m_mouseYModifier = 1f;
				}
			}
		}

		protected float mouseXModifier
		{
			get
			{
				return mouseSensitivity.x * m_mouseXModifier;
			}
		}

		protected float mouseYModifier
		{
			get
			{
				return mouseSensitivity.y * m_mouseYModifier;
			}
		}

		protected void OnValidate()
		{
			invertMouseX = m_invertMouseX;
			invertMouseY = m_invertMouseY;
		}

		protected void Reset()
		{
			Rigidbody rigidbody = base.gameObject.AddComponentIfMissing<Rigidbody>();
			if (rigidbody != null)
			{
				rigidbody.useGravity = false;
				rigidbody.drag = 1f;
				rigidbody.angularDrag = 6f;
			}
			mouseSensitivity = new Vector2(4f, 4f);
			movementSpeedOrAcceleration = 100f;
		}

		protected void OnEnable()
		{
			m_cachedRigidBody = GetComponent<Rigidbody>();
		}

		protected void Update()
		{
			m_TranslationAccelerationDirection = GetMovementDirection();
			if (Input.GetMouseButton((int)rotationMouseButton))
			{
				ProcessRotationInput();
			}
		}

		protected void FixedUpdate()
		{
			MoveCamera();
			RotateCamera();
			if (rollCorrection)
			{
				RemoveRoll(upVector, rollCorrectionSpeed);
			}
		}

		protected virtual void MoveCamera()
		{
			Vector3 translationAccelerationDirection = m_TranslationAccelerationDirection;
			if (smoothedMovement)
			{
				m_cachedRigidBody.AddRelativeForce(movementSpeedOrAcceleration * translationAccelerationDirection, ForceMode.Acceleration);
			}
			else
			{
				m_cachedRigidBody.velocity = movementSpeedOrAcceleration * base.transform.TransformDirection(translationAccelerationDirection);
			}
		}

		protected void ProcessRotationInput()
		{
			m_YawPerUpdate += mouseXModifier * Input.GetAxisRaw(mouseXAxis);
			m_tiltPerUpdate += mouseYModifier * Input.GetAxisRaw(mouseYAxis);
			m_framesSinceUpdate++;
		}

		protected virtual void RotateCamera()
		{
			if (m_framesSinceUpdate > 1)
			{
				m_YawPerUpdate /= m_framesSinceUpdate;
				m_tiltPerUpdate /= m_framesSinceUpdate;
			}
			float yawPerUpdate = m_YawPerUpdate;
			float tiltPerUpdate = m_tiltPerUpdate;
			m_YawPerUpdate = 0f;
			m_tiltPerUpdate = 0f;
			m_framesSinceUpdate = 0;
			if (smoothedRotation)
			{
				m_cachedRigidBody.AddTorque(new Vector3(0f, yawPerUpdate, 0f), ForceMode.Acceleration);
				m_cachedRigidBody.AddRelativeTorque(new Vector3(tiltPerUpdate, 0f, 0f), ForceMode.Acceleration);
				return;
			}
			Quaternion quaternion = Quaternion.AngleAxis(yawPerUpdate, yawAxis);
			Quaternion quaternion2 = Quaternion.AngleAxis(tiltPerUpdate, Vector3.right);
			m_cachedRigidBody.rotation = quaternion * m_cachedRigidBody.rotation * quaternion2;
			m_cachedRigidBody.angularVelocity = new Vector3(0f, 0f, 0f);
		}

		protected void RemoveRoll(Vector3 upVector, float correctionSpeed)
		{
			upVector.Normalize();
			Quaternion rotation = m_cachedRigidBody.rotation;
			Vector3 vector = rotation * Vector3.right;
			Vector3 vector2 = Vector3.Cross(vector, upVector);
			Vector3 vector3 = Vector3.Cross(upVector, vector2);
			float num = Vector3.Angle(vector, vector3);
			num *= Mathf.Sign(Vector3.Cross(vector, vector3).z) * Mathf.Sign(vector3.x);
			m_cachedRigidBody.AddTorque(vector2 * num * correctionSpeed, ForceMode.Acceleration);
		}

		protected Vector3 GetMovementDirection()
		{
			Vector3 result = new Vector3(Input.GetAxisRaw(sidewaysAxis), Input.GetAxisRaw(verticalAxis), Input.GetAxisRaw(forwardsAxis));
			result.Normalize();
			return result;
		}

		protected void OnDrawGizmosSelected()
		{
			if (!(m_cachedRigidBody == null))
			{
				Vector3 position = m_cachedRigidBody.position;
				Gizmos.DrawLine(position, position + upVector);
			}
		}
	}
}
