using UnityEngine;

namespace Battlehub.Cubeman
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class CubemanCharacter : MonoBehaviour
	{
		public float m_MovingTurnSpeed = 360f;

		public float m_StationaryTurnSpeed = 180f;

		public float m_JumpPower = 12f;

		[Range(1f, 4f)]
		public float m_GravityMultiplier = 2f;

		public float m_RunCycleLegOffset = 0.2f;

		public float m_MoveSpeedMultiplier = 1f;

		public float m_AnimSpeedMultiplier = 1f;

		public float m_GroundCheckDistance = 0.1f;

		private Rigidbody m_Rigidbody;

		private Animator m_Animator;

		private bool m_IsGrounded;

		private float m_OrigGroundCheckDistance;

		private const float k_Half = 0.5f;

		private float m_TurnAmount;

		private float m_ForwardAmount;

		private Vector3 m_GroundNormal;

		private float m_CapsuleHeight;

		private Vector3 m_CapsuleCenter;

		private CapsuleCollider m_Capsule;

		private bool m_Crouching;

		public bool Enabled;

		private void Awake()
		{
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Capsule = GetComponent<CapsuleCollider>();
			m_CapsuleHeight = m_Capsule.height;
			m_CapsuleCenter = m_Capsule.center;
			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
			m_OrigGroundCheckDistance = m_GroundCheckDistance;
		}

		private void Start()
		{
			if (Enabled)
			{
				CheckGroundStatus();
				return;
			}
			m_GroundNormal = Vector3.up;
			m_IsGrounded = true;
			m_Animator.applyRootMotion = true;
		}

		public void Move(Vector3 move, bool crouch, bool jump)
		{
			if (Enabled)
			{
				if (move.magnitude > 1f)
				{
					move.Normalize();
				}
				move = base.transform.InverseTransformDirection(move);
				CheckGroundStatus();
				move = Vector3.ProjectOnPlane(move, m_GroundNormal);
				m_TurnAmount = Mathf.Atan2(move.x, move.z);
				m_ForwardAmount = move.z;
				ApplyExtraTurnRotation();
				if (m_IsGrounded)
				{
					HandleGroundedMovement(crouch, jump);
				}
				else
				{
					HandleAirborneMovement();
				}
				ScaleCapsuleForCrouching(crouch);
				PreventStandingInLowHeadroom();
			}
			if (m_Animator != null && m_Animator.isInitialized)
			{
				UpdateAnimator(move);
			}
		}

		private void ScaleCapsuleForCrouching(bool crouch)
		{
			if (m_IsGrounded && crouch)
			{
				if (!m_Crouching)
				{
					m_Capsule.height /= 1.5f;
					m_Capsule.center /= 1.5f;
					m_Crouching = true;
				}
			}
			else if (Physics.SphereCast(new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * 0.5f, Vector3.up), maxDistance: m_CapsuleHeight - m_Capsule.radius * 0.5f, radius: m_Capsule.radius * 0.5f, layerMask: -1, queryTriggerInteraction: QueryTriggerInteraction.Ignore))
			{
				m_Crouching = true;
			}
			else
			{
				m_Capsule.height = m_CapsuleHeight;
				m_Capsule.center = m_CapsuleCenter;
				m_Crouching = false;
			}
		}

		private void PreventStandingInLowHeadroom()
		{
			if (!m_Crouching && Physics.SphereCast(new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * 0.5f, Vector3.up), maxDistance: m_CapsuleHeight - m_Capsule.radius * 0.5f, radius: m_Capsule.radius * 0.5f, layerMask: -1, queryTriggerInteraction: QueryTriggerInteraction.Ignore))
			{
				m_Crouching = true;
			}
		}

		private void UpdateAnimator(Vector3 move)
		{
			m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
			m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
			m_Animator.SetBool("Crouch", m_Crouching);
			m_Animator.SetBool("OnGround", m_IsGrounded);
			if (!m_IsGrounded)
			{
				m_Animator.SetFloat("Jump", m_Rigidbody.linearVelocity.y);
			}
			float value = (float)((Mathf.Repeat(m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1f) < 0.5f) ? 1 : (-1)) * m_ForwardAmount;
			if (m_IsGrounded)
			{
				m_Animator.SetFloat("JumpLeg", value);
			}
			if (m_IsGrounded && move.magnitude > 0f)
			{
				m_Animator.speed = m_AnimSpeedMultiplier;
			}
			else
			{
				m_Animator.speed = 1f;
			}
		}

		private void HandleAirborneMovement()
		{
			Vector3 force = Physics.gravity * m_GravityMultiplier - Physics.gravity;
			m_Rigidbody.AddForce(force);
			m_GroundCheckDistance = ((m_Rigidbody.linearVelocity.y < 0f) ? m_OrigGroundCheckDistance : 0.01f);
		}

		private void HandleGroundedMovement(bool crouch, bool jump)
		{
			if (jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
			{
				m_OrigGroundCheckDistance = m_GroundCheckDistance;
				m_Rigidbody.linearVelocity = new Vector3(m_Rigidbody.linearVelocity.x, m_JumpPower, m_Rigidbody.linearVelocity.z);
				m_IsGrounded = false;
				m_Animator.applyRootMotion = false;
				m_GroundCheckDistance = 0.1f;
			}
		}

		private void ApplyExtraTurnRotation()
		{
			float num = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			base.transform.Rotate(0f, m_TurnAmount * num * Time.deltaTime, 0f);
		}

		public void OnAnimatorMove()
		{
			if (m_IsGrounded && Time.deltaTime > 0f)
			{
				Vector3 linearVelocity = m_Animator.deltaPosition * m_MoveSpeedMultiplier / Time.deltaTime;
				if (m_Rigidbody != null && !m_Rigidbody.isKinematic)
				{
					linearVelocity.y = m_Rigidbody.linearVelocity.y;
					m_Rigidbody.linearVelocity = linearVelocity;
				}
			}
		}

		private void CheckGroundStatus()
		{
			if (Physics.Raycast(base.transform.position + Vector3.up * 0.1f, Vector3.down, out var hitInfo, m_GroundCheckDistance))
			{
				m_GroundCheckDistance = m_OrigGroundCheckDistance;
				m_GroundNormal = hitInfo.normal;
				m_IsGrounded = true;
				m_Animator.applyRootMotion = true;
			}
			else
			{
				m_IsGrounded = false;
				m_GroundNormal = Vector3.up;
				m_Animator.applyRootMotion = false;
			}
		}
	}
}
