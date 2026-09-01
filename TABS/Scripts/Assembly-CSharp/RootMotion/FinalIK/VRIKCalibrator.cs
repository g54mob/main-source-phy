using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	public static class VRIKCalibrator
	{
		[Serializable]
		public class Settings
		{
			[Tooltip("Multiplies character scale")]
			public float scaleMlp = 1f;

			[Tooltip("Local axis of the HMD facing forward.")]
			public Vector3 headTrackerForward = Vector3.forward;

			[Tooltip("Local axis of the HMD facing up.")]
			public Vector3 headTrackerUp = Vector3.up;

			[Tooltip("Local axis of the body tracker towards the player's forward direction.")]
			public Vector3 bodyTrackerForward = Vector3.forward;

			[Tooltip("Local axis of the body tracker towards the up direction.")]
			public Vector3 bodyTrackerUp = Vector3.up;

			[Tooltip("Local axis of the hand trackers pointing from the wrist towards the palm.")]
			public Vector3 handTrackerForward = Vector3.forward;

			[Tooltip("Local axis of the hand trackers pointing in the direction of the surface normal of the back of the hand.")]
			public Vector3 handTrackerUp = Vector3.up;

			[Tooltip("Local axis of the foot trackers towards the player's forward direction.")]
			public Vector3 footTrackerForward = Vector3.forward;

			[Tooltip("Local axis of the foot tracker towards the up direction.")]
			public Vector3 footTrackerUp = Vector3.up;

			[Space(10f)]
			[Tooltip("Offset of the head bone from the HMD in (headTrackerForward, headTrackerUp) space relative to the head tracker.")]
			public Vector3 headOffset;

			[Tooltip("Offset of the hand bones from the hand trackers in (handTrackerForward, handTrackerUp) space relative to the hand trackers.")]
			public Vector3 handOffset;

			[Tooltip("Forward offset of the foot bones from the foot trackers.")]
			public float footForwardOffset;

			[Tooltip("Inward offset of the foot bones from the foot trackers.")]
			public float footInwardOffset;

			[Tooltip("Used for adjusting foot heading relative to the foot trackers.")]
			[Range(-180f, 180f)]
			public float footHeadingOffset;

			[Range(0f, 1f)]
			public float pelvisPositionWeight = 1f;

			[Range(0f, 1f)]
			public float pelvisRotationWeight = 1f;
		}

		[Serializable]
		public class CalibrationData
		{
			[Serializable]
			public class Target
			{
				public bool used;

				public Vector3 localPosition;

				public Quaternion localRotation;

				public Target(Transform t)
				{
					used = t != null;
					if (used)
					{
						localPosition = t.localPosition;
						localRotation = t.localRotation;
					}
				}

				public void SetTo(Transform t)
				{
					if (used)
					{
						t.localPosition = localPosition;
						t.localRotation = localRotation;
					}
				}
			}

			public float scale;

			public Target head;

			public Target leftHand;

			public Target rightHand;

			public Target pelvis;

			public Target leftFoot;

			public Target rightFoot;

			public Target leftLegGoal;

			public Target rightLegGoal;

			public Vector3 pelvisTargetRight;

			public float pelvisPositionWeight;

			public float pelvisRotationWeight;
		}

		public static void RecalibrateScale(VRIK ik, Settings settings)
		{
			float num = (ik.solver.spine.headTarget.position.y - ik.references.root.position.y) / (ik.references.head.position.y - ik.references.root.position.y);
			ik.references.root.localScale *= num * settings.scaleMlp;
		}

		public static CalibrationData Calibrate(VRIK ik, Settings settings, Transform headTracker, Transform bodyTracker = null, Transform leftHandTracker = null, Transform rightHandTracker = null, Transform leftFootTracker = null, Transform rightFootTracker = null)
		{
			if (!ik.solver.initiated)
			{
				Debug.LogError("Can not calibrate before VRIK has initiated.");
				return null;
			}
			if (headTracker == null)
			{
				Debug.LogError("Can not calibrate VRIK without the head tracker.");
				return null;
			}
			CalibrationData calibrationData = new CalibrationData();
			ik.solver.FixTransforms();
			Vector3 position = headTracker.position + headTracker.rotation * Quaternion.LookRotation(settings.headTrackerForward, settings.headTrackerUp) * settings.headOffset;
			ik.references.root.position = new Vector3(position.x, ik.references.root.position.y, position.z);
			Vector3 forward = headTracker.rotation * settings.headTrackerForward;
			forward.y = 0f;
			ik.references.root.rotation = Quaternion.LookRotation(forward);
			Transform transform = ((ik.solver.spine.headTarget == null) ? new GameObject("Head Target").transform : ik.solver.spine.headTarget);
			transform.position = position;
			transform.rotation = ik.references.head.rotation;
			transform.parent = headTracker;
			ik.solver.spine.headTarget = transform;
			float num = (transform.position.y - ik.references.root.position.y) / (ik.references.head.position.y - ik.references.root.position.y);
			ik.references.root.localScale *= num * settings.scaleMlp;
			if (bodyTracker != null)
			{
				Transform transform2 = ((ik.solver.spine.pelvisTarget == null) ? new GameObject("Pelvis Target").transform : ik.solver.spine.pelvisTarget);
				transform2.position = ik.references.pelvis.position;
				transform2.rotation = ik.references.pelvis.rotation;
				transform2.parent = bodyTracker;
				ik.solver.spine.pelvisTarget = transform2;
				ik.solver.spine.pelvisPositionWeight = settings.pelvisPositionWeight;
				ik.solver.spine.pelvisRotationWeight = settings.pelvisRotationWeight;
				ik.solver.plantFeet = false;
				ik.solver.spine.maxRootAngle = 180f;
			}
			else if (leftFootTracker != null && rightFootTracker != null)
			{
				ik.solver.spine.maxRootAngle = 0f;
			}
			if (leftHandTracker != null)
			{
				Transform transform3 = ((ik.solver.leftArm.target == null) ? new GameObject("Left Hand Target").transform : ik.solver.leftArm.target);
				transform3.position = leftHandTracker.position + leftHandTracker.rotation * Quaternion.LookRotation(settings.handTrackerForward, settings.handTrackerUp) * settings.handOffset;
				transform3.rotation = QuaTools.MatchRotation(upAxis: Vector3.Cross(ik.solver.leftArm.wristToPalmAxis, ik.solver.leftArm.palmToThumbAxis), targetRotation: leftHandTracker.rotation * Quaternion.LookRotation(settings.handTrackerForward, settings.handTrackerUp), targetforwardAxis: settings.handTrackerForward, targetUpAxis: settings.handTrackerUp, forwardAxis: ik.solver.leftArm.wristToPalmAxis);
				transform3.parent = leftHandTracker;
				ik.solver.leftArm.target = transform3;
				ik.solver.leftArm.positionWeight = 1f;
				ik.solver.leftArm.rotationWeight = 1f;
			}
			else
			{
				ik.solver.leftArm.positionWeight = 0f;
				ik.solver.leftArm.rotationWeight = 0f;
			}
			if (rightHandTracker != null)
			{
				Transform transform4 = ((ik.solver.rightArm.target == null) ? new GameObject("Right Hand Target").transform : ik.solver.rightArm.target);
				transform4.position = rightHandTracker.position + rightHandTracker.rotation * Quaternion.LookRotation(settings.handTrackerForward, settings.handTrackerUp) * settings.handOffset;
				transform4.rotation = QuaTools.MatchRotation(upAxis: -Vector3.Cross(ik.solver.rightArm.wristToPalmAxis, ik.solver.rightArm.palmToThumbAxis), targetRotation: rightHandTracker.rotation * Quaternion.LookRotation(settings.handTrackerForward, settings.handTrackerUp), targetforwardAxis: settings.handTrackerForward, targetUpAxis: settings.handTrackerUp, forwardAxis: ik.solver.rightArm.wristToPalmAxis);
				transform4.parent = rightHandTracker;
				ik.solver.rightArm.target = transform4;
				ik.solver.rightArm.positionWeight = 1f;
				ik.solver.rightArm.rotationWeight = 1f;
			}
			else
			{
				ik.solver.rightArm.positionWeight = 0f;
				ik.solver.rightArm.rotationWeight = 0f;
			}
			if (leftFootTracker != null)
			{
				CalibrateLeg(settings, leftFootTracker, ik.solver.leftLeg, (ik.references.leftToes != null) ? ik.references.leftToes : ik.references.leftFoot, ik.references.root.forward, isLeft: true);
			}
			if (rightFootTracker != null)
			{
				CalibrateLeg(settings, rightFootTracker, ik.solver.rightLeg, (ik.references.rightToes != null) ? ik.references.rightToes : ik.references.rightFoot, ik.references.root.forward, isLeft: false);
			}
			bool num2 = bodyTracker != null || (leftFootTracker != null && rightFootTracker != null);
			VRIKRootController vRIKRootController = ik.references.root.GetComponent<VRIKRootController>();
			if (num2)
			{
				if (vRIKRootController == null)
				{
					vRIKRootController = ik.references.root.gameObject.AddComponent<VRIKRootController>();
				}
				vRIKRootController.Calibrate();
			}
			else if (vRIKRootController != null)
			{
				UnityEngine.Object.Destroy(vRIKRootController);
			}
			ik.solver.spine.minHeadHeight = 0f;
			ik.solver.locomotion.weight = ((bodyTracker == null && leftFootTracker == null && rightFootTracker == null) ? 1f : 0f);
			calibrationData.scale = ik.references.root.localScale.y;
			calibrationData.head = new CalibrationData.Target(ik.solver.spine.headTarget);
			calibrationData.pelvis = new CalibrationData.Target(ik.solver.spine.pelvisTarget);
			calibrationData.leftHand = new CalibrationData.Target(ik.solver.leftArm.target);
			calibrationData.rightHand = new CalibrationData.Target(ik.solver.rightArm.target);
			calibrationData.leftFoot = new CalibrationData.Target(ik.solver.leftLeg.target);
			calibrationData.rightFoot = new CalibrationData.Target(ik.solver.rightLeg.target);
			calibrationData.leftLegGoal = new CalibrationData.Target(ik.solver.leftLeg.bendGoal);
			calibrationData.rightLegGoal = new CalibrationData.Target(ik.solver.rightLeg.bendGoal);
			calibrationData.pelvisTargetRight = vRIKRootController.pelvisTargetRight;
			calibrationData.pelvisPositionWeight = ik.solver.spine.pelvisPositionWeight;
			calibrationData.pelvisRotationWeight = ik.solver.spine.pelvisRotationWeight;
			return calibrationData;
		}

		private static void CalibrateLeg(Settings settings, Transform tracker, IKSolverVR.Leg leg, Transform lastBone, Vector3 rootForward, bool isLeft)
		{
			string text = (isLeft ? "Left" : "Right");
			Transform transform = ((leg.target == null) ? new GameObject(text + " Foot Target").transform : leg.target);
			Quaternion quaternion = tracker.rotation * Quaternion.LookRotation(settings.footTrackerForward, settings.footTrackerUp);
			Vector3 vector = quaternion * Vector3.forward;
			vector.y = 0f;
			quaternion = Quaternion.LookRotation(vector);
			float x = (isLeft ? settings.footInwardOffset : (0f - settings.footInwardOffset));
			transform.position = tracker.position + quaternion * new Vector3(x, 0f, settings.footForwardOffset);
			transform.position = new Vector3(transform.position.x, lastBone.position.y, transform.position.z);
			transform.rotation = lastBone.rotation;
			Vector3 vector2 = AxisTools.GetAxisVectorToDirection(lastBone, rootForward);
			if (Vector3.Dot(lastBone.rotation * vector2, rootForward) < 0f)
			{
				vector2 = -vector2;
			}
			Vector3 vector3 = Quaternion.Inverse(Quaternion.LookRotation(transform.rotation * vector2)) * vector;
			float num = Mathf.Atan2(vector3.x, vector3.z) * 57.29578f;
			float num2 = (isLeft ? settings.footHeadingOffset : (0f - settings.footHeadingOffset));
			transform.rotation = Quaternion.AngleAxis(num + num2, Vector3.up) * transform.rotation;
			transform.parent = tracker;
			leg.target = transform;
			leg.positionWeight = 1f;
			leg.rotationWeight = 1f;
			Transform transform2 = ((leg.bendGoal == null) ? new GameObject(text + " Leg Bend Goal").transform : leg.bendGoal);
			transform2.position = lastBone.position + quaternion * Vector3.forward + quaternion * Vector3.up;
			transform2.parent = tracker;
			leg.bendGoal = transform2;
			leg.bendGoalWeight = 1f;
		}

		public static void Calibrate(VRIK ik, CalibrationData data, Transform headTracker, Transform bodyTracker = null, Transform leftHandTracker = null, Transform rightHandTracker = null, Transform leftFootTracker = null, Transform rightFootTracker = null)
		{
			if (!ik.solver.initiated)
			{
				Debug.LogError("Can not calibrate before VRIK has initiated.");
				return;
			}
			if (headTracker == null)
			{
				Debug.LogError("Can not calibrate VRIK without the head tracker.");
				return;
			}
			ik.solver.FixTransforms();
			Transform transform = ((ik.solver.spine.headTarget == null) ? new GameObject("Head Target").transform : ik.solver.spine.headTarget);
			transform.parent = headTracker;
			data.head.SetTo(transform);
			ik.solver.spine.headTarget = transform;
			ik.references.root.localScale = data.scale * Vector3.one;
			if (bodyTracker != null)
			{
				Transform transform2 = ((ik.solver.spine.pelvisTarget == null) ? new GameObject("Pelvis Target").transform : ik.solver.spine.pelvisTarget);
				transform2.parent = bodyTracker;
				data.pelvis.SetTo(transform2);
				ik.solver.spine.pelvisTarget = transform2;
				ik.solver.spine.pelvisPositionWeight = data.pelvisPositionWeight;
				ik.solver.spine.pelvisRotationWeight = data.pelvisRotationWeight;
				ik.solver.plantFeet = false;
				ik.solver.spine.maxRootAngle = 180f;
			}
			else if (leftFootTracker != null && rightFootTracker != null)
			{
				ik.solver.spine.maxRootAngle = 0f;
			}
			if (leftHandTracker != null)
			{
				Transform transform3 = ((ik.solver.leftArm.target == null) ? new GameObject("Left Hand Target").transform : ik.solver.leftArm.target);
				transform3.parent = leftHandTracker;
				data.leftHand.SetTo(transform3);
				ik.solver.leftArm.target = transform3;
				ik.solver.leftArm.positionWeight = 1f;
				ik.solver.leftArm.rotationWeight = 1f;
			}
			else
			{
				ik.solver.leftArm.positionWeight = 0f;
				ik.solver.leftArm.rotationWeight = 0f;
			}
			if (rightHandTracker != null)
			{
				Transform transform4 = ((ik.solver.rightArm.target == null) ? new GameObject("Right Hand Target").transform : ik.solver.rightArm.target);
				transform4.parent = rightHandTracker;
				data.rightHand.SetTo(transform4);
				ik.solver.rightArm.target = transform4;
				ik.solver.rightArm.positionWeight = 1f;
				ik.solver.rightArm.rotationWeight = 1f;
			}
			else
			{
				ik.solver.rightArm.positionWeight = 0f;
				ik.solver.rightArm.rotationWeight = 0f;
			}
			if (leftFootTracker != null)
			{
				CalibrateLeg(data, leftFootTracker, ik.solver.leftLeg, (ik.references.leftToes != null) ? ik.references.leftToes : ik.references.leftFoot, ik.references.root.forward, isLeft: true);
			}
			if (rightFootTracker != null)
			{
				CalibrateLeg(data, rightFootTracker, ik.solver.rightLeg, (ik.references.rightToes != null) ? ik.references.rightToes : ik.references.rightFoot, ik.references.root.forward, isLeft: false);
			}
			bool num = bodyTracker != null || (leftFootTracker != null && rightFootTracker != null);
			VRIKRootController vRIKRootController = ik.references.root.GetComponent<VRIKRootController>();
			if (num)
			{
				if (vRIKRootController == null)
				{
					vRIKRootController = ik.references.root.gameObject.AddComponent<VRIKRootController>();
				}
				vRIKRootController.Calibrate(data);
			}
			else if (vRIKRootController != null)
			{
				UnityEngine.Object.Destroy(vRIKRootController);
			}
			ik.solver.spine.minHeadHeight = 0f;
			ik.solver.locomotion.weight = ((bodyTracker == null && leftFootTracker == null && rightFootTracker == null) ? 1f : 0f);
		}

		private static void CalibrateLeg(CalibrationData data, Transform tracker, IKSolverVR.Leg leg, Transform lastBone, Vector3 rootForward, bool isLeft)
		{
			string text = (isLeft ? "Left" : "Right");
			Transform transform = ((leg.target == null) ? new GameObject(text + " Foot Target").transform : leg.target);
			transform.parent = tracker;
			if (isLeft)
			{
				data.leftFoot.SetTo(transform);
			}
			else
			{
				data.rightFoot.SetTo(transform);
			}
			leg.target = transform;
			leg.positionWeight = 1f;
			leg.rotationWeight = 1f;
			Transform transform2 = ((leg.bendGoal == null) ? new GameObject(text + " Leg Bend Goal").transform : leg.bendGoal);
			transform2.parent = tracker;
			if (isLeft)
			{
				data.leftLegGoal.SetTo(transform2);
			}
			else
			{
				data.rightLegGoal.SetTo(transform2);
			}
			leg.bendGoal = transform2;
			leg.bendGoalWeight = 1f;
		}
	}
}
