using Poly.Math;
using Poly.UI;
using UnityEngine;

namespace Poly.Game
{
	public class BusArticulationAnimator : MonoBehaviour
	{
		public struct BoneInfo
		{
			public Transform transform;

			public Vec2 fromHinge;

			public float angleOffset;

			public float interpolationBetweenChassisParts;
		}

		public Transform frontChassis;

		public Transform rearChassis;

		public Transform hingeInitialPosition;

		public Transform[] upperArticulationBones;

		public Transform[] lowerArticulationBones;

		public InspectorButton initButton;

		public InspectorButton updateButton;

		[Tooltip("This shrinks interpolated range. Use this to 'glue' some outer-most bones to the driving transforms.")]
		public Range interpolatedRange = new Range(0f, 1f);

		private BoneInfo[] infos;

		private Vec2 hingeInFront;

		private Vec2 hingeInBack;

		public bool isFlipped { get; set; }

		public BusArticulationAnimator()
		{
			initButton = new InspectorButton("Init Articulation", Init);
			updateButton = new InspectorButton("Update Articulation", UpdateArticulation);
		}

		public void Init()
		{
			Transform2 transform = frontChassis;
			Transform2 transform2 = frontChassis;
			Vec2 v = (Vec2)hingeInitialPosition.position;
			hingeInFront = transform.InvMul(v);
			hingeInBack = transform2.InvMul(v);
			int num = upperArticulationBones.Length;
			int num2 = lowerArticulationBones.Length;
			infos = new BoneInfo[num + num2];
			Range invalid = Range.invalid;
			for (int i = 0; i < num; i++)
			{
				Transform transform3 = upperArticulationBones[i];
				ref BoneInfo reference = ref infos[i];
				reference.transform = transform3;
				reference.fromHinge = transform.InvMul((Vec2)transform3.position) - hingeInFront;
				reference.angleOffset = transform3.rotation.eulerAngles.z - transform.angle_slow;
				invalid.Encapsulate(reference.fromHinge.x);
			}
			for (int j = 0; j < num2; j++)
			{
				Transform transform4 = lowerArticulationBones[j];
				ref BoneInfo reference2 = ref infos[num + j];
				reference2.transform = transform4;
				reference2.fromHinge = transform.InvMul((Vec2)transform4.position) - hingeInFront;
				reference2.angleOffset = transform4.rotation.eulerAngles.z - transform.angle_slow;
				invalid.Encapsulate(reference2.fromHinge.x);
			}
			if (isFlipped)
			{
				hingeInFront.x *= -1f;
				hingeInBack.x *= -1f;
				for (int k = 0; k < infos.Length; k++)
				{
					ref BoneInfo reference3 = ref infos[k];
					reference3.fromHinge.x *= -1f;
					reference3.angleOffset *= -1f;
				}
				invalid.min *= -1f;
				invalid.max *= -1f;
				Values.Swap(ref invalid.min, ref invalid.max);
			}
			for (int l = 0; l < infos.Length; l++)
			{
				ref BoneInfo reference4 = ref infos[l];
				reference4.interpolationBetweenChassisParts = 1f - (reference4.fromHinge.x - invalid.min + 5.877472E-39f) / (invalid.max - invalid.min + 1.1754944E-38f);
				reference4.interpolationBetweenChassisParts = interpolatedRange.MapFrom(new Range(0f, 1f), reference4.interpolationBetweenChassisParts);
				reference4.interpolationBetweenChassisParts = Mathf.Clamp01(reference4.interpolationBetweenChassisParts);
			}
		}

		public void UpdateArticulation()
		{
			Transform2 transform = frontChassis;
			Transform2 transform2 = rearChassis;
			bool flag = base.transform.localScale.x < 0f;
			if (isFlipped && !flag)
			{
				transform.angle_slow = 0f - transform.angle_slow;
				transform2.angle_slow = 0f - transform2.angle_slow;
			}
			Vec2 vec = hingeInFront;
			Vec2 vec2 = hingeInBack;
			if (isFlipped)
			{
				vec.x *= -1f;
				vec2.x *= -1f;
			}
			Vec2 a = transform * vec;
			Vec2 b = transform2 * vec2;
			float angle_slow = transform.angle_slow;
			float num = transform2.angle_slow;
			if (num <= angle_slow - 180f)
			{
				num += 360f;
			}
			if (angle_slow + 180f < num)
			{
				num -= 360f;
			}
			for (int i = 0; i < infos.Length; i++)
			{
				ref BoneInfo reference = ref infos[i];
				Vec2 fromHinge = reference.fromHinge;
				if (isFlipped)
				{
					fromHinge.x *= -1f;
				}
				float interpolationBetweenChassisParts = reference.interpolationBetweenChassisParts;
				float num2 = Mathf.LerpUnclamped(angle_slow, num, interpolationBetweenChassisParts);
				Quaternion quaternion = Quaternion.Euler(0f, 0f, num2);
				reference.transform.position = Vec2.LerpUnclamped(in a, in b, interpolationBetweenChassisParts) + quaternion * fromHinge;
				reference.transform.rotation = Quaternion.Euler(0f, 0f, num2 + (isFlipped ? (-1f) : 1f) * reference.angleOffset) * ((isFlipped && !flag) ? Quaternion.AngleAxis(180f, Vector3.up) : Quaternion.identity);
			}
		}
	}
}
