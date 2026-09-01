using System;
using UnityEngine;

namespace BitCode.MeshTool.DataTypes
{
	public struct BoneWeightData : IEquatable<BoneWeightData>
	{
		public float Weight0;

		public float Weight1;

		public float Weight2;

		public float Weight3;

		public int Bone0;

		public int Bone1;

		public int Bone2;

		public int Bone3;

		public static BoneWeightData Null => default(BoneWeightData);

		public static BoneWeightData FullToFirst
		{
			get
			{
				BoneWeightData result = default(BoneWeightData);
				while (true)
				{
					int num = -914450865;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -443794580)) % 4)
						{
						case 0u:
							break;
						case 3u:
							result.Bone0 = 0;
							num = (int)((num2 * 786710470) ^ 0x125F4C2B);
							continue;
						case 1u:
							result.Weight0 = 1f;
							num = (int)((num2 * 294489871) ^ 0x3B699ED);
							continue;
						default:
							return result;
						}
						break;
					}
				}
			}
		}

		public BoneWeightData(BoneWeight source)
		{
			Bone0 = source.boneIndex0;
			Bone1 = source.boneIndex1;
			Bone2 = source.boneIndex2;
			Bone3 = source.boneIndex3;
			Weight0 = source.weight0;
			Weight1 = source.weight1;
			Weight2 = source.weight2;
			Weight3 = source.weight3;
		}

		public BoneWeight ToBoneWeight()
		{
			BoneWeight result = default(BoneWeight);
			while (true)
			{
				int num = 303596464;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x53953BAC)) % 8)
					{
					case 0u:
						break;
					case 5u:
						result.weight0 = Weight0;
						num = ((int)num2 * -358754474) ^ -917363527;
						continue;
					case 3u:
						result.weight1 = Weight1;
						num = (int)((num2 * 1243576283) ^ 0x72538C7F);
						continue;
					case 6u:
						result.boneIndex2 = Bone2;
						num = ((int)num2 * -768614436) ^ 0xA3D689D;
						continue;
					case 4u:
						result.boneIndex0 = Bone0;
						result.boneIndex1 = Bone1;
						num = (int)((num2 * 417918116) ^ 0x5E3E119A);
						continue;
					case 2u:
						result.weight2 = Weight2;
						num = (int)((num2 * 1957659283) ^ 0x401D3365);
						continue;
					case 1u:
						result.boneIndex3 = Bone3;
						num = ((int)num2 * -1292609250) ^ 0x208388AF;
						continue;
					default:
						result.weight3 = Weight3;
						return result;
					}
					break;
				}
			}
		}

		public static bool operator ==(BoneWeightData b1, BoneWeightData b2)
		{
			return b1.Equals(b2);
		}

		public static bool operator !=(BoneWeightData b1, BoneWeightData b2)
		{
			return !b1.Equals(b2);
		}

		public override string ToString()
		{
			return $"{Bone0}:{Weight0}, {Bone1}:{Weight1}, {Bone2}:{Weight2}, {Bone3}:{Weight3}";
		}

		public bool Equals(BoneWeightData other)
		{
			if (Weight0.Equals(other.Weight0))
			{
				while (true)
				{
					int num = 1097108236;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x5D470CFB)) % 9)
						{
						case 0u:
							break;
						case 7u:
						{
							int num13;
							int num14;
							if (Weight1.Equals(other.Weight1))
							{
								num13 = 1740115294;
								num14 = num13;
							}
							else
							{
								num13 = 177796728;
								num14 = num13;
							}
							num = num13 ^ (int)(num2 * 1176673491);
							continue;
						}
						case 6u:
						{
							int num5;
							int num6;
							if (!Weight2.Equals(other.Weight2))
							{
								num5 = 1852791149;
								num6 = num5;
							}
							else
							{
								num5 = 1128808203;
								num6 = num5;
							}
							num = num5 ^ ((int)num2 * -1649230232);
							continue;
						}
						case 1u:
						{
							int num9;
							int num10;
							if (Weight3.Equals(other.Weight3))
							{
								num9 = -397152253;
								num10 = num9;
							}
							else
							{
								num9 = -1241022435;
								num10 = num9;
							}
							num = num9 ^ ((int)num2 * -1231427815);
							continue;
						}
						case 5u:
						{
							int num7;
							int num8;
							if (Bone0 == other.Bone0)
							{
								num7 = 1502622172;
								num8 = num7;
							}
							else
							{
								num7 = 1441533717;
								num8 = num7;
							}
							num = num7 ^ ((int)num2 * -728870529);
							continue;
						}
						case 8u:
						{
							int num11;
							int num12;
							if (Bone1 != other.Bone1)
							{
								num11 = -515973702;
								num12 = num11;
							}
							else
							{
								num11 = -1720489075;
								num12 = num11;
							}
							num = num11 ^ ((int)num2 * -1792243575);
							continue;
						}
						case 3u:
							return Bone3 == other.Bone3;
						case 2u:
						{
							int num3;
							int num4;
							if (Bone2 == other.Bone2)
							{
								num3 = -529525860;
								num4 = num3;
							}
							else
							{
								num3 = -918731441;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 1653738338);
							continue;
						}
						default:
							goto end_IL_0016;
						}
						break;
					}
					continue;
					end_IL_0016:
					break;
				}
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is BoneWeightData)
			{
				BoneWeightData other = default(BoneWeightData);
				while (true)
				{
					int num = -522637308;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -131361727)) % 4)
						{
						case 0u:
							break;
						case 1u:
							other = (BoneWeightData)obj;
							num = (int)((num2 * 1070203992) ^ 0x2DAE6CB6);
							continue;
						case 3u:
							return Equals(other);
						default:
							goto end_IL_0008;
						}
						break;
					}
					continue;
					end_IL_0008:
					break;
				}
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
