using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class Attributes
	{
		public int POSITION = -1;

		public int NORMAL = -1;

		public int TANGENT = -1;

		public int TEXCOORD_0 = -1;

		public int TEXCOORD_1 = -1;

		public int TEXCOORD_2 = -1;

		public int TEXCOORD_3 = -1;

		public int TEXCOORD_4 = -1;

		public int TEXCOORD_5 = -1;

		public int TEXCOORD_6 = -1;

		public int TEXCOORD_7 = -1;

		public int TEXCOORD_8 = -1;

		public int COLOR_0 = -1;

		public int JOINTS_0 = -1;

		public int WEIGHTS_0 = -1;

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public int GetTexCoordsCount()
		{
			if (TEXCOORD_0 < 0)
			{
				return 0;
			}
			if (TEXCOORD_1 < 0)
			{
				return 1;
			}
			if (TEXCOORD_2 < 0)
			{
				return 2;
			}
			if (TEXCOORD_3 < 0)
			{
				return 3;
			}
			if (TEXCOORD_4 < 0)
			{
				return 4;
			}
			if (TEXCOORD_5 < 0)
			{
				return 5;
			}
			if (TEXCOORD_6 < 0)
			{
				return 6;
			}
			if (TEXCOORD_7 < 0)
			{
				return 7;
			}
			if (TEXCOORD_8 >= 0)
			{
				return 9;
			}
			return 8;
		}

		public bool TryGetAllUVAccessors(out int[] uvAccessors, out bool limitExceeded)
		{
			if (TEXCOORD_0 >= 0)
			{
				int texCoordsCount = GetTexCoordsCount();
				uvAccessors = new int[texCoordsCount];
				uvAccessors[0] = TEXCOORD_0;
				if (TEXCOORD_1 >= 0)
				{
					uvAccessors[1] = TEXCOORD_1;
				}
				if (TEXCOORD_2 >= 0)
				{
					uvAccessors[2] = TEXCOORD_2;
				}
				if (TEXCOORD_3 >= 0)
				{
					uvAccessors[3] = TEXCOORD_3;
				}
				if (TEXCOORD_4 >= 0)
				{
					uvAccessors[4] = TEXCOORD_4;
				}
				if (TEXCOORD_5 >= 0)
				{
					uvAccessors[5] = TEXCOORD_5;
				}
				if (TEXCOORD_6 >= 0)
				{
					uvAccessors[6] = TEXCOORD_6;
				}
				if (TEXCOORD_7 >= 0)
				{
					uvAccessors[7] = TEXCOORD_7;
				}
				limitExceeded = TEXCOORD_8 >= 0;
				return true;
			}
			uvAccessors = null;
			limitExceeded = false;
			return false;
		}

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (POSITION >= 0)
			{
				writer.AddProperty("POSITION", POSITION);
			}
			if (NORMAL >= 0)
			{
				writer.AddProperty("NORMAL", NORMAL);
			}
			if (TANGENT >= 0)
			{
				writer.AddProperty("TANGENT", TANGENT);
			}
			if (TEXCOORD_0 >= 0)
			{
				writer.AddProperty("TEXCOORD_0", TEXCOORD_0);
			}
			if (TEXCOORD_1 >= 0)
			{
				writer.AddProperty("TEXCOORD_1", TEXCOORD_1);
			}
			if (TEXCOORD_2 >= 0)
			{
				writer.AddProperty("TEXCOORD_2", TEXCOORD_2);
			}
			if (TEXCOORD_3 >= 0)
			{
				writer.AddProperty("TEXCOORD_3", TEXCOORD_3);
			}
			if (TEXCOORD_4 >= 0)
			{
				writer.AddProperty("TEXCOORD_4", TEXCOORD_4);
			}
			if (TEXCOORD_5 >= 0)
			{
				writer.AddProperty("TEXCOORD_5", TEXCOORD_5);
			}
			if (TEXCOORD_6 >= 0)
			{
				writer.AddProperty("TEXCOORD_6", TEXCOORD_6);
			}
			if (TEXCOORD_7 >= 0)
			{
				writer.AddProperty("TEXCOORD_7", TEXCOORD_7);
			}
			if (COLOR_0 >= 0)
			{
				writer.AddProperty("COLOR_0", COLOR_0);
			}
			if (JOINTS_0 >= 0)
			{
				writer.AddProperty("JOINTS_0", JOINTS_0);
			}
			if (WEIGHTS_0 >= 0)
			{
				writer.AddProperty("WEIGHTS_0", WEIGHTS_0);
			}
			writer.Close();
		}
	}
}
