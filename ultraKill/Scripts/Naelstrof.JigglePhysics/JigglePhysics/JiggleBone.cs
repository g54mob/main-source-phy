using System.Collections.Generic;
using UnityEngine;

namespace JigglePhysics
{
	public class JiggleBone
	{
		private struct PositionFrame
		{
			public Vector3 position;

			public double time;

			public PositionFrame(Vector3 position, double time)
			{
				this.position = position;
				this.time = time;
			}
		}

		private static SphereCollider sphereCollider;

		private PositionFrame currentTargetAnimatedBoneFrame;

		private PositionFrame lastTargetAnimatedBoneFrame;

		private Vector3 currentFixedAnimatedBonePosition;

		public JiggleBone parent;

		public JiggleBone child;

		private Quaternion boneRotationChangeCheck;

		private Vector3 bonePositionChangeCheck;

		public Quaternion lastValidPoseBoneRotation;

		private Vector3 lastValidPoseBoneLocalPosition;

		private float normalizedIndex;

		public Transform transform;

		private double updateTime;

		private double previousUpdateTime;

		public Vector3 position;

		public Vector3 previousPosition;

		public Vector3 preTeleportPosition;

		private Vector3 extrapolatedPosition;

		private float GetLengthToParent()
		{
			if (parent == null)
			{
				return 0.1f;
			}
			return Vector3.Distance(currentFixedAnimatedBonePosition, parent.currentFixedAnimatedBonePosition);
		}

		private Vector3 GetTargetBonePosition(PositionFrame prev, PositionFrame next, double time)
		{
			double num = next.time - prev.time;
			if (num == 0.0)
			{
				return next.position;
			}
			double num2 = (time - prev.time) / num;
			return Vector3.Lerp(prev.position, next.position, (float)num2);
		}

		public JiggleBone(Transform transform, JiggleBone parent, Vector3 position)
		{
			if (sphereCollider == null)
			{
				GameObject gameObject = new GameObject("JiggleBoneSphereCollider", typeof(SphereCollider))
				{
					hideFlags = HideFlags.HideAndDontSave
				};
				if (Application.isPlaying)
				{
					Object.DontDestroyOnLoad(gameObject);
				}
				sphereCollider = gameObject.GetComponent<SphereCollider>();
			}
			this.transform = transform;
			this.parent = parent;
			this.position = position;
			previousPosition = position;
			if (transform != null)
			{
				lastValidPoseBoneRotation = transform.localRotation;
				lastValidPoseBoneLocalPosition = transform.localPosition;
			}
			updateTime = Time.time;
			previousUpdateTime = Time.time;
			lastTargetAnimatedBoneFrame = new PositionFrame(position, Time.time);
			currentTargetAnimatedBoneFrame = lastTargetAnimatedBoneFrame;
			if (parent != null)
			{
				this.parent.child = this;
			}
		}

		public void CalculateNormalizedIndex()
		{
			int num = 0;
			JiggleBone jiggleBone = this;
			while (jiggleBone.parent != null)
			{
				jiggleBone = jiggleBone.parent;
				num++;
			}
			int num2 = 0;
			jiggleBone = this;
			while (jiggleBone.child != null)
			{
				jiggleBone = jiggleBone.child;
				num2++;
			}
			int num3 = num + num2;
			float num4 = (float)num / (float)num3;
			normalizedIndex = num4;
		}

		public void Simulate(JiggleSettingsBase jiggleSettings, Vector3 wind, double time, ICollection<Collider> colliders)
		{
			currentFixedAnimatedBonePosition = GetTargetBonePosition(lastTargetAnimatedBoneFrame, currentTargetAnimatedBoneFrame, time);
			if (parent == null)
			{
				SetNewPosition(currentFixedAnimatedBonePosition, time);
				return;
			}
			Vector3 localSpaceVelocity = position - previousPosition - (parent.position - parent.previousPosition);
			Vector3 newPosition = NextPhysicsPosition(position, previousPosition, localSpaceVelocity, Time.fixedDeltaTime, jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.Gravity), jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.Friction), jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.AirFriction));
			newPosition += wind * (Time.fixedDeltaTime * jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.AirFriction));
			newPosition = ConstrainAngle(newPosition, jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.AngleElasticity) * jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.AngleElasticity), jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.ElasticitySoften));
			newPosition = ConstrainLength(newPosition, jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.LengthElasticity) * jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.LengthElasticity));
			foreach (Collider collider in colliders)
			{
				sphereCollider.radius = jiggleSettings.GetRadius(normalizedIndex);
				if (!(sphereCollider.radius <= 0f) && Physics.ComputePenetration(sphereCollider, newPosition, Quaternion.identity, collider, collider.transform.position, collider.transform.rotation, out var direction, out var distance))
				{
					newPosition += direction * distance;
				}
			}
			SetNewPosition(newPosition, time);
		}

		public void CacheAnimationPosition()
		{
			lastTargetAnimatedBoneFrame = currentTargetAnimatedBoneFrame;
			if (transform == null)
			{
				if (parent != null && !(parent.transform == null))
				{
					Vector3 vector = parent.transform.position;
					if (parent.parent != null)
					{
						Vector3 vector2 = parent.transform.TransformPoint(parent.parent.transform.InverseTransformPoint(vector));
						currentTargetAnimatedBoneFrame = new PositionFrame(vector2, Time.time);
					}
					else
					{
						Vector3 vector3 = parent.transform.TransformPoint(parent.transform.parent.InverseTransformPoint(vector));
						currentTargetAnimatedBoneFrame = new PositionFrame(vector3, Time.time);
					}
				}
			}
			else
			{
				currentTargetAnimatedBoneFrame = new PositionFrame(transform.position, Time.time);
				lastValidPoseBoneRotation = transform.localRotation;
				lastValidPoseBoneLocalPosition = transform.localPosition;
			}
		}

		public Vector3 ConstrainLength(Vector3 newPosition, float elasticity)
		{
			Vector3 normalized = (newPosition - parent.position).normalized;
			return Vector3.Lerp(newPosition, parent.position + normalized * GetLengthToParent(), elasticity);
		}

		public void PrepareTeleport()
		{
			if (transform == null)
			{
				Vector3 vector = parent.transform.position;
				if (parent.parent != null)
				{
					preTeleportPosition = parent.transform.TransformPoint(parent.parent.transform.InverseTransformPoint(vector));
				}
				else
				{
					preTeleportPosition = parent.transform.TransformPoint(parent.transform.parent.InverseTransformPoint(vector));
				}
			}
			else
			{
				preTeleportPosition = transform.position;
			}
		}

		public void FinishTeleport()
		{
			Vector3 zero = Vector3.zero;
			if (transform == null)
			{
				Vector3 vector = parent.transform.position;
				zero = ((parent.parent == null) ? parent.transform.TransformPoint(parent.transform.parent.InverseTransformPoint(vector)) : parent.transform.TransformPoint(parent.parent.transform.InverseTransformPoint(vector)));
			}
			else
			{
				zero = transform.position;
			}
			Vector3 vector2 = zero - preTeleportPosition;
			lastTargetAnimatedBoneFrame = new PositionFrame(lastTargetAnimatedBoneFrame.position + vector2, lastTargetAnimatedBoneFrame.time);
			currentTargetAnimatedBoneFrame = new PositionFrame(currentTargetAnimatedBoneFrame.position + vector2, currentTargetAnimatedBoneFrame.time);
			position += vector2;
			previousPosition += vector2;
		}

		public Vector3 ConstrainAngle(Vector3 newPosition, float elasticity, float elasticitySoften)
		{
			Vector3 vector;
			Vector3 vector2;
			if (parent.parent == null)
			{
				vector = parent.currentFixedAnimatedBonePosition + (parent.currentFixedAnimatedBonePosition - currentFixedAnimatedBonePosition);
				vector2 = vector;
			}
			else
			{
				vector2 = parent.parent.position;
				vector = parent.parent.currentFixedAnimatedBonePosition;
			}
			Vector3 fromDirection = parent.currentFixedAnimatedBonePosition - vector;
			Vector3 toDirection = parent.position - vector2;
			Quaternion quaternion = Quaternion.FromToRotation(fromDirection, toDirection);
			Vector3 vector3 = currentFixedAnimatedBonePosition - vector;
			Vector3 vector4 = quaternion * vector3;
			float num = Vector3.Distance(newPosition, vector2 + vector4);
			num /= GetLengthToParent();
			num = Mathf.Clamp01(num);
			num = Mathf.Pow(num, elasticitySoften * 2f);
			return Vector3.Lerp(newPosition, vector2 + vector4, elasticity * num);
		}

		public void SetNewPosition(Vector3 newPosition, double time)
		{
			previousUpdateTime = updateTime;
			previousPosition = position;
			updateTime = time;
			position = newPosition;
		}

		public static Vector3 NextPhysicsPosition(Vector3 newPosition, Vector3 previousPosition, Vector3 localSpaceVelocity, float deltaTime, float gravityMultiplier, float friction, float airFriction)
		{
			float num = deltaTime * deltaTime;
			Vector3 vector = newPosition - previousPosition - localSpaceVelocity;
			return newPosition + vector * (1f - airFriction) + localSpaceVelocity * (1f - friction) + Physics.gravity * (gravityMultiplier * num);
		}

		public void DebugDraw(Color simulateColor, Color targetColor, bool interpolated)
		{
			if (parent != null)
			{
				if (interpolated)
				{
					Debug.DrawLine(extrapolatedPosition, parent.extrapolatedPosition, simulateColor, 0f, depthTest: false);
				}
				else
				{
					Debug.DrawLine(position, parent.position, simulateColor, 0f, depthTest: false);
				}
				Debug.DrawLine(currentFixedAnimatedBonePosition, parent.currentFixedAnimatedBonePosition, targetColor, 0f, depthTest: false);
			}
		}

		public Vector3 DeriveFinalSolvePosition(Vector3 offset, float smoothing)
		{
			double num = ((double)(Time.time - smoothing * Time.fixedDeltaTime) - previousUpdateTime) / (double)Time.fixedDeltaTime;
			extrapolatedPosition = offset + Vector3.LerpUnclamped(previousPosition, position, (float)num);
			return extrapolatedPosition;
		}

		public void PrepareBone()
		{
			if (transform != null)
			{
				if (boneRotationChangeCheck == transform.localRotation)
				{
					transform.localRotation = lastValidPoseBoneRotation;
				}
				if (bonePositionChangeCheck == transform.localPosition)
				{
					transform.localPosition = lastValidPoseBoneLocalPosition;
				}
			}
			CacheAnimationPosition();
		}

		public void OnDrawGizmos(JiggleSettingsBase jiggleSettings)
		{
			if (child != null)
			{
				Gizmos.DrawLine(position, child.position);
			}
			if (jiggleSettings != null)
			{
				Gizmos.DrawWireSphere(position, jiggleSettings.GetRadius(normalizedIndex));
			}
		}

		public void PoseBone(float blend)
		{
			if (transform == null)
			{
				return;
			}
			if (child != null)
			{
				Vector3 vector = Vector3.Lerp(currentTargetAnimatedBoneFrame.position, extrapolatedPosition, blend);
				Vector3 vector2 = Vector3.Lerp(child.currentTargetAnimatedBoneFrame.position, child.extrapolatedPosition, blend);
				if (parent != null)
				{
					transform.position = vector;
				}
				Vector3 vector3 = ((!(child.transform == null)) ? child.transform.position : ((parent == null) ? transform.TransformPoint(transform.parent.InverseTransformPoint(transform.position)) : transform.TransformPoint(parent.transform.InverseTransformPoint(transform.position))));
				Vector3 fromDirection = vector3 - transform.position;
				Vector3 toDirection = vector2 - vector;
				Quaternion quaternion = Quaternion.FromToRotation(fromDirection, toDirection);
				transform.rotation = quaternion * transform.rotation;
			}
			if (transform != null)
			{
				boneRotationChangeCheck = transform.localRotation;
				bonePositionChangeCheck = transform.localPosition;
			}
		}
	}
}
