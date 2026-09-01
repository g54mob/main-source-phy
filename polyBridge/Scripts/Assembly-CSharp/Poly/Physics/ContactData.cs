using UnityEngine;

namespace Poly.Physics
{
	public struct ContactData
	{
		public Transform receivingObject;

		public Transform otherObject;

		public Layer receivingLayer;

		public Layer otherLayer;

		internal Transform marker0;

		internal Transform marker1;

		public object userData0;

		public object userData1;

		public bool isInTouch0;

		public bool isInTouch1;

		public float normalSign;

		public int debug_extraRef;

		private NormalAndDistance normalAndDistance0;

		private NormalAndDistance normalAndDistance1;

		public object this[int i]
		{
			get
			{
				return i switch
				{
					0 => userData0, 
					1 => userData1, 
					_ => null, 
				};
			}
			set
			{
				switch (i)
				{
				case 0:
					userData0 = value;
					break;
				case 1:
					userData1 = value;
					break;
				}
			}
		}

		public static ContactData CreateFromEvent(in CollisionEvent e)
		{
			ContactData result = default(ContactData);
			ShapeHandle value = e.a.Value;
			ShapeHandle value2 = e.b.Value;
			if (e.receivingHandle == ReceivingHandle.A)
			{
				result.normalSign = 1f;
				result.receivingObject = value.GetUnityComponent().transform;
				result.otherObject = value2.GetUnityComponent().transform;
				result.receivingLayer = value.layer;
				result.otherLayer = value2.layer;
			}
			else
			{
				result.normalSign = -1f;
				result.receivingObject = value2.GetUnityComponent().transform;
				result.otherObject = value.GetUnityComponent().transform;
				result.receivingLayer = value2.layer;
				result.otherLayer = value.layer;
			}
			return result;
		}

		public void SetNormal(int idx, Vec2 normal, float distance)
		{
			if (idx == 0)
			{
				normalAndDistance0.normal = normal * normalSign;
				normalAndDistance0.distance = distance;
			}
			else
			{
				normalAndDistance1.normal = normal * normalSign;
				normalAndDistance1.distance = distance;
			}
		}

		public NormalAndDistance GetNormal(int idx)
		{
			if (idx != 0)
			{
				return normalAndDistance1;
			}
			return normalAndDistance0;
		}
	}
}
