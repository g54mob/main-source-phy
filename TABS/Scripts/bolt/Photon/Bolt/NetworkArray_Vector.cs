using UnityEngine;

namespace Photon.Bolt
{
	[Documentation]
	public class NetworkArray_Vector : NetworkArray_Values<Vector3>
	{
		internal NetworkArray_Vector(int length, int stride)
			: base(length, stride)
		{
		}

		protected override Vector3 GetValue(int index)
		{
			return Storage.Values[index].Vector3;
		}

		protected override bool SetValue(int index, Vector3 value)
		{
			if (Storage.Values[index].Vector3 != value)
			{
				Storage.Values[index].Vector3 = value;
				return true;
			}
			return false;
		}
	}
}
