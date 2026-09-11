using UnityEngine;

namespace JigglePhysics
{
	public class JigglePoint
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

		private Vector3 position;

		private Transform transform;

		private Vector3 previousPosition;

		private double previousUpdateTime;

		private double updateTime;

		private Vector3 parentPosition;

		private Vector3 previousParentPosition;

		private PositionFrame currentTargetAnimatedBoneFrame;

		private PositionFrame lastTargetAnimatedBoneFrame;

		public Vector3 extrapolatedPosition;

		public JigglePoint(Transform transform)
		{
			this.transform = transform;
			position = transform.position;
			previousPosition = position;
			updateTime = Time.time;
			previousUpdateTime = Time.time;
			parentPosition = position;
			previousParentPosition = parentPosition;
			currentTargetAnimatedBoneFrame = new PositionFrame(position, Time.time);
			lastTargetAnimatedBoneFrame = new PositionFrame(position, Time.time);
		}

		public void PrepareSimulate()
		{
			lastTargetAnimatedBoneFrame = currentTargetAnimatedBoneFrame;
			currentTargetAnimatedBoneFrame = new PositionFrame(transform.position, Time.time);
		}

		private Vector3 GetTargetBonePosition(PositionFrame prev, PositionFrame next, double time)
		{
			double num = next.time - prev.time;
			if (num == 0.0)
			{
				return next.position;
			}
			double num2 = (time - prev.time) / num;
			return Vector3.LerpUnclamped(prev.position, next.position, (float)num2);
		}

		public void Simulate(JiggleSettingsBase jiggleSettings, Vector3 force, double time)
		{
			parentPosition = GetTargetBonePosition(lastTargetAnimatedBoneFrame, currentTargetAnimatedBoneFrame, time);
			Vector3 localSpaceVelocity = position - previousPosition - (parentPosition - previousParentPosition);
			Vector3 newPosition = JiggleBone.NextPhysicsPosition(position, previousPosition, localSpaceVelocity, Time.fixedDeltaTime, jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.Gravity), jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.Friction), jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.AirFriction));
			newPosition += force * (Time.deltaTime * jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.AirFriction));
			newPosition = ConstrainSpring(newPosition, jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.LengthElasticity) * jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.LengthElasticity));
			SetNewPosition(newPosition, time);
		}

		public void SetNewPosition(Vector3 newPosition, double time)
		{
			previousPosition = position;
			previousParentPosition = parentPosition;
			previousUpdateTime = updateTime;
			position = newPosition;
			updateTime = time;
		}

		public void DeriveFinalSolvePosition(float smoothing)
		{
			Vector3 vector = transform.position - GetTargetBonePosition(lastTargetAnimatedBoneFrame, currentTargetAnimatedBoneFrame, Time.time - Time.fixedDeltaTime * smoothing);
			double num = ((double)(Time.time - Time.fixedDeltaTime * smoothing) - previousUpdateTime) / (double)Time.fixedDeltaTime;
			extrapolatedPosition = vector + Vector3.LerpUnclamped(previousPosition, position, (float)num);
		}

		public Vector3 ConstrainSpring(Vector3 newPosition, float elasticity)
		{
			return Vector3.Lerp(newPosition, parentPosition, elasticity);
		}

		public void DrawGizmos(Color color)
		{
			Gizmos.color = color;
			Gizmos.DrawSphere(extrapolatedPosition, 0.15f);
		}

		public void DebugDraw(Color color)
		{
			Debug.DrawLine(extrapolatedPosition, transform.position, color, Time.deltaTime, depthTest: false);
		}
	}
}
