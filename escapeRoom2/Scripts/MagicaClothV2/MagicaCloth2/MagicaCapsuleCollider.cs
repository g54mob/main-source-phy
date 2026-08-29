using UnityEngine;

namespace MagicaCloth2
{
	[AddComponentMenu("MagicaCloth2/MagicaCapsuleCollider")]
	[HelpURL("https://magicasoft.jp/en/mc2_capsulecollidercomponent/")]
	public class MagicaCapsuleCollider : ColliderComponent
	{
		public enum Direction
		{
			[InspectorName("X-Axis")]
			X = 0,
			[InspectorName("Y-Axis")]
			Y = 1,
			[InspectorName("Z-Axis")]
			Z = 2
		}

		public Direction direction;

		public bool reverseDirection;

		public bool radiusSeparation;

		public bool alignedOnCenter = true;

		public override ColliderManager.ColliderType GetColliderType()
		{
			if (direction == Direction.X)
			{
				if (!alignedOnCenter)
				{
					return ColliderManager.ColliderType.CapsuleX_Start;
				}
				return ColliderManager.ColliderType.CapsuleX_Center;
			}
			if (direction == Direction.Y)
			{
				if (!alignedOnCenter)
				{
					return ColliderManager.ColliderType.CapsuleY_Start;
				}
				return ColliderManager.ColliderType.CapsuleY_Center;
			}
			if (!alignedOnCenter)
			{
				return ColliderManager.ColliderType.CapsuleZ_Start;
			}
			return ColliderManager.ColliderType.CapsuleZ_Center;
		}

		public void SetSize(float startRadius, float endRadius, float length)
		{
			SetSize(new Vector3(startRadius, endRadius, length));
			radiusSeparation = startRadius != endRadius;
		}

		public override Vector3 GetSize()
		{
			if (radiusSeparation)
			{
				return size;
			}
			return new Vector3(size.x, size.x, size.z);
		}

		public Vector3 GetLocalDir()
		{
			float num = ((!reverseDirection) ? 1 : (-1));
			if (direction == Direction.X)
			{
				return Vector3.right * num;
			}
			if (direction == Direction.Y)
			{
				return Vector3.up * num;
			}
			return Vector3.forward * num;
		}

		public Vector3 GetLocalUp()
		{
			if (direction == Direction.X)
			{
				return Vector3.up;
			}
			if (direction == Direction.Y)
			{
				return Vector3.forward;
			}
			return Vector3.up;
		}

		public override bool IsReverseDirection()
		{
			return reverseDirection;
		}

		public override void DataValidate()
		{
			size.x = Mathf.Max(size.x, 0.001f);
			size.y = Mathf.Max(size.y, 0.001f);
			size.z = Mathf.Max(size.z, 0.001f);
		}
	}
}
