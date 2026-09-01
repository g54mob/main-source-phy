using System;

namespace ImmersiveVRTools.Runtime.Common.Utilities
{
	[Serializable]
	public struct Boolean3
	{
		public bool x;

		public bool y;

		public bool z;

		public bool this[int index]
		{
			get
			{
				return index switch
				{
					0 => x, 
					1 => y, 
					2 => z, 
					_ => throw new IndexOutOfRangeException(), 
				};
			}
			set
			{
				switch (index)
				{
				case 0:
					x = value;
					break;
				case 1:
					y = value;
					break;
				case 2:
					z = value;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		public Boolean3(bool all)
		{
			x = all;
			y = all;
			z = all;
		}

		public Boolean3(bool x, bool y, bool z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public bool AnyTrue()
		{
			if (!x && !y)
			{
				return z;
			}
			return true;
		}

		public override string ToString()
		{
			return "Boolean3(" + x + ", " + y + ", " + z + ")";
		}
	}
}
