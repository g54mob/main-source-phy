using System;
using System.Collections.Generic;
using BitCode.MeshTool.DataTypes;
using BitCode.MeshTool.Enums;
using UnityEngine;
using sWvrUdmInExPDDknNfxYVMRoTZkI;

namespace BitCode.MeshTool
{
	public static class MeshDataCombiner
	{
		public static MeshData MergeMeshes(MeshData[] meshDataToMerge, CombinerSettingsFlag mergeSettings)
		{
			if (meshDataToMerge == null)
			{
				goto IL_0006;
			}
			goto IL_00d8;
			IL_0006:
			int num = -739195057;
			goto IL_000b;
			IL_000b:
			MeshData meshData = default(MeshData);
			int num3 = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -750280752)) % 12)
				{
				case 3u:
					break;
				case 6u:
					goto IL_0050;
				case 9u:
					meshData = meshDataToMerge[0];
					eAYBRlvzFYFswosDHgfIrTgzqBbD(meshData, "0");
					num = -1287594795;
					continue;
				case 5u:
					num = ((int)num2 * -1945476709) ^ -796610147;
					continue;
				case 2u:
				{
					MeshData meshData2 = meshDataToMerge[num3];
					eAYBRlvzFYFswosDHgfIrTgzqBbD(meshData2, num3.ToString());
					meshData = ANApziPxSvCOsZzhUGhSZCTFSSe(meshData, meshData2, mergeSettings);
					num3++;
					num = -1347509534;
					continue;
				}
				case 0u:
					MtnDJKrkWTCzGZlFAfHTHgOFDtShA(ref meshData);
					num = (int)(num2 * 540710538) ^ -1617604528;
					continue;
				case 11u:
					goto IL_00d8;
				case 4u:
					throw new ArgumentException("MeshData array must contain at least two entries.");
				case 10u:
				{
					int num4;
					int num5;
					if (meshData.IsSkinned())
					{
						num4 = 1163377248;
						num5 = num4;
					}
					else
					{
						num4 = 1910568376;
						num5 = num4;
					}
					num = num4 ^ ((int)num2 * -1096848436);
					continue;
				}
				case 1u:
					num3 = 1;
					num = (int)(num2 * 1592390732) ^ -1649007807;
					continue;
				case 7u:
					throw new ArgumentNullException("meshDataToMerge");
				default:
					return meshData;
				}
				break;
				IL_0050:
				int num6;
				if (num3 >= meshDataToMerge.Length)
				{
					num = -871031670;
					num6 = num;
				}
				else
				{
					num = -1790577710;
					num6 = num;
				}
			}
			goto IL_0006;
			IL_00d8:
			int num7;
			if (meshDataToMerge.Length >= 2)
			{
				num = -533667147;
				num7 = num;
			}
			else
			{
				num = -192754972;
				num7 = num;
			}
			goto IL_000b;
		}

		private static MeshData ANApziPxSvCOsZzhUGhSZCTFSSe(MeshData P_0, MeshData P_1, CombinerSettingsFlag P_2)
		{
			MeshData result = default(MeshData);
			int num = P_0.VertexCount() + P_1.VertexCount();
			while (true)
			{
				int num2 = 1862592828;
				while (true)
				{
					uint num3;
					switch ((num3 = (uint)(num2 ^ 0x62E1FABA)) % 21)
					{
					case 11u:
						break;
					case 3u:
						OUGmQMJYOCCkPkYlLJZJpewsRyQl(P_0, P_1, ref result);
						num2 = (int)(num3 * 1717299933) ^ -32590739;
						continue;
					case 16u:
						result.UV3 = ZWkyIoAeDxigTMFDQZWEVEJzwgfS(P_0.UV3, P_1.UV3, Vector2.zero, num);
						num2 = (int)((num3 * 1764136710) ^ 0x50AD0A12);
						continue;
					case 10u:
					{
						int num11;
						if (!fRBfIAIBXQkpRItZTYaquwHNzhaRA(P_2, CombinerSettingsFlag.Tangents))
						{
							num2 = 1635360978;
							num11 = num2;
						}
						else
						{
							num2 = 3679183;
							num11 = num2;
						}
						continue;
					}
					case 7u:
					{
						int num5;
						if (fRBfIAIBXQkpRItZTYaquwHNzhaRA(P_2, CombinerSettingsFlag.UV0))
						{
							num2 = 1696981589;
							num5 = num2;
						}
						else
						{
							num2 = 1806382661;
							num5 = num2;
						}
						continue;
					}
					case 9u:
						result.VertexColors = ZWkyIoAeDxigTMFDQZWEVEJzwgfS(P_0.VertexColors, P_1.VertexColors, Color.white, num);
						num2 = ((int)num3 * -1938156891) ^ 0x64F1DF4F;
						continue;
					case 18u:
						result.Normals = ZWkyIoAeDxigTMFDQZWEVEJzwgfS(P_0.Normals, P_1.Normals, Vector3.zero, num);
						num2 = (int)((num3 * 1000122422) ^ 0x378D7403);
						continue;
					case 15u:
					{
						int num8;
						int num9;
						if (!fRBfIAIBXQkpRItZTYaquwHNzhaRA(P_2, CombinerSettingsFlag.Normals))
						{
							num8 = -1012245457;
							num9 = num8;
						}
						else
						{
							num8 = -1440323032;
							num9 = num8;
						}
						num2 = num8 ^ (int)(num3 * 594814826);
						continue;
					}
					case 13u:
						result.UV0 = ZWkyIoAeDxigTMFDQZWEVEJzwgfS(P_0.UV0, P_1.UV0, Vector2.zero, num);
						num2 = (int)(num3 * 1447345937) ^ -676278630;
						continue;
					case 4u:
						result.UV2 = ZWkyIoAeDxigTMFDQZWEVEJzwgfS(P_0.UV2, P_1.UV2, Vector2.zero, num);
						num2 = ((int)num3 * -1608172721) ^ -1769221414;
						continue;
					case 12u:
						result.UV1 = ZWkyIoAeDxigTMFDQZWEVEJzwgfS(P_0.UV1, P_1.UV1, Vector2.zero, num);
						num2 = ((int)num3 * -252207641) ^ 0x25DDCCCF;
						continue;
					case 0u:
					{
						int num13;
						if (fRBfIAIBXQkpRItZTYaquwHNzhaRA(P_2, CombinerSettingsFlag.UV2))
						{
							num2 = 1572889933;
							num13 = num2;
						}
						else
						{
							num2 = 796563683;
							num13 = num2;
						}
						continue;
					}
					case 20u:
						result.BlendShapes = XFkTehtTdADWyqJshZZllkWsqwAG(P_0, P_1);
						num2 = (int)(num3 * 174772955) ^ -555813521;
						continue;
					case 8u:
					{
						int num12;
						if (!fRBfIAIBXQkpRItZTYaquwHNzhaRA(P_2, CombinerSettingsFlag.BlendShapes))
						{
							num2 = 2110730028;
							num12 = num2;
						}
						else
						{
							num2 = 826242051;
							num12 = num2;
						}
						continue;
					}
					case 19u:
						result.Tangents = ZWkyIoAeDxigTMFDQZWEVEJzwgfS(P_0.Tangents, P_1.Tangents, Vector4.zero, num);
						num2 = (int)((num3 * 1327938228) ^ 0x4AF04896);
						continue;
					case 1u:
					{
						int num10;
						if (!fRBfIAIBXQkpRItZTYaquwHNzhaRA(P_2, CombinerSettingsFlag.VertColors))
						{
							num2 = 20404187;
							num10 = num2;
						}
						else
						{
							num2 = 883367102;
							num10 = num2;
						}
						continue;
					}
					case 14u:
					{
						int num7;
						if (fRBfIAIBXQkpRItZTYaquwHNzhaRA(P_2, CombinerSettingsFlag.UV3))
						{
							num2 = 1964247614;
							num7 = num2;
						}
						else
						{
							num2 = 680817930;
							num7 = num2;
						}
						continue;
					}
					case 17u:
					{
						int num6;
						if (!fRBfIAIBXQkpRItZTYaquwHNzhaRA(P_2, CombinerSettingsFlag.UV1))
						{
							num2 = 1058764026;
							num6 = num2;
						}
						else
						{
							num2 = 1322100537;
							num6 = num2;
						}
						continue;
					}
					case 6u:
					{
						int num4;
						if (!fRBfIAIBXQkpRItZTYaquwHNzhaRA(P_2, CombinerSettingsFlag.Skinning))
						{
							num2 = 1315081092;
							num4 = num2;
						}
						else
						{
							num2 = 1912553671;
							num4 = num2;
						}
						continue;
					}
					case 2u:
						result.Vertices = qbpFVfghxXeopRSAodKufgIjsyyV(P_0.Vertices, P_1.Vertices);
						result.Submeshes = TxueiogIGxDPSpvSEupTuIMwnbAb(P_0, P_1);
						num2 = ((int)num3 * -2106452805) ^ -1681440696;
						continue;
					default:
						return result;
					}
					break;
				}
			}
		}

		private static void eAYBRlvzFYFswosDHgfIrTgzqBbD(MeshData P_0, string P_1)
		{
			if (P_0.Vertices == null)
			{
				goto IL_000b;
			}
			goto IL_0130;
			IL_000b:
			int num = 1450254302;
			goto IL_0010;
			IL_0010:
			int num3 = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x12CF0964)) % 13)
				{
				case 2u:
					break;
				default:
					return;
				case 12u:
					throw new NullReferenceException("MeshData " + P_1 + " does not contain a submesh array.");
				case 11u:
					num3 = 0;
					num = 402307550;
					continue;
				case 9u:
					num3++;
					num = 1570066606;
					continue;
				case 5u:
					throw new ArgumentException($"Triangle list array is empty for submesh at index {num3}.", P_1);
				case 3u:
					goto IL_00be;
				case 7u:
					throw new ArgumentException("MeshData vertex array is empty.", P_1);
				case 10u:
					num = ((int)num2 * -425548955) ^ 0x72FC77CC;
					continue;
				case 4u:
					goto IL_0130;
				case 6u:
					goto IL_014c;
				case 8u:
					throw new NullReferenceException("MeshData " + P_1 + " does not contain a vertex array.");
				case 0u:
					goto IL_0191;
				case 1u:
					return;
				}
				break;
				IL_0191:
				int num4;
				if (num3 >= P_0.Submeshes.Length)
				{
					num = 796078561;
					num4 = num;
				}
				else
				{
					num = 1710163682;
					num4 = num;
				}
				continue;
				IL_014c:
				int num5;
				if (P_0.VertexCount() != 0)
				{
					num = 1908438241;
					num5 = num;
				}
				else
				{
					num = 150785703;
					num5 = num;
				}
				continue;
				IL_00be:
				int num6;
				if ((P_0.Submeshes[num3].TriangleList ?? throw new ArgumentException($"Submesh at index {num3} does not contain a triangle list.", P_1)).Length == 0)
				{
					num = 467161932;
					num6 = num;
				}
				else
				{
					num = 932973014;
					num6 = num;
				}
			}
			goto IL_000b;
			IL_0130:
			int num7;
			if (P_0.Submeshes == null)
			{
				num = 718813708;
				num7 = num;
			}
			else
			{
				num = 1933134704;
				num7 = num;
			}
			goto IL_0010;
		}

		private static void aRkGSMIGeieLsDqBcTBrPOpkjHAu(BlendshapeFrameData P_0, string P_1)
		{
			if (P_0.VertexDelta == null)
			{
				goto IL_0008;
			}
			goto IL_0082;
			IL_0008:
			int num = 1155561451;
			goto IL_000d;
			IL_000d:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x760E47BE)) % 7)
				{
				case 2u:
					break;
				default:
					return;
				case 3u:
					throw new NullReferenceException("NormalDelta array in BlendshapeData for frame " + P_0.Name + " in input " + P_1 + " is not initialized.");
				case 4u:
					goto IL_0082;
				case 6u:
					goto IL_009e;
				case 1u:
					throw new NullReferenceException("TangentDelta array in BlendshapeData for frame " + P_0.Name + " in input " + P_1 + " is not initialized.");
				case 5u:
					throw new NullReferenceException("VertexDelta array in BlendshapeData for frame " + P_0.Name + " in input " + P_1 + " is not initialized.");
				case 0u:
					return;
				}
				break;
				IL_009e:
				int num3;
				if (P_0.TangentDelta == null)
				{
					num = 760619892;
					num3 = num;
				}
				else
				{
					num = 2093002610;
					num3 = num;
				}
			}
			goto IL_0008;
			IL_0082:
			int num4;
			if (P_0.NormalDelta == null)
			{
				num = 855931587;
				num4 = num;
			}
			else
			{
				num = 1091245641;
				num4 = num;
			}
			goto IL_000d;
		}

		private static _0001[] ZWkyIoAeDxigTMFDQZWEVEJzwgfS<_0001>(_0001[] P_0, _0001[] P_1, _0001 P_2, int P_3)
		{
			if (P_0 == null)
			{
				goto IL_0003;
			}
			goto IL_005d;
			IL_0003:
			int num = 718078114;
			goto IL_0008;
			IL_0008:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x1BC8C4F5)) % 9)
				{
				case 4u:
					break;
				case 1u:
				{
					int num5;
					int num6;
					if (P_1 != null)
					{
						num5 = -1201330614;
						num6 = num5;
					}
					else
					{
						num5 = -1656047975;
						num6 = num5;
					}
					num = num5 ^ ((int)num2 * -162724106);
					continue;
				}
				case 2u:
					goto IL_005d;
				case 0u:
					goto IL_0072;
				case 7u:
				{
					int num3;
					int num4;
					if (P_1 == null)
					{
						num3 = -1159563531;
						num4 = num3;
					}
					else
					{
						num3 = -1334567838;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 1722101823);
					continue;
				}
				case 6u:
					return qbpFVfghxXeopRSAodKufgIjsyyV(P_0, P_1);
				case 8u:
					return null;
				case 5u:
					return nQmwWnCHPYVCLuzrvmvPgngUutpO(P_0, P_2, P_3 - P_0.Length, true);
				default:
					return nQmwWnCHPYVCLuzrvmvPgngUutpO(P_1, P_2, P_3 - P_1.Length, false);
				}
				break;
				IL_0072:
				int num7;
				if (P_0 == null)
				{
					num = 1603600749;
					num7 = num;
				}
				else
				{
					num = 185333386;
					num7 = num;
				}
			}
			goto IL_0003;
			IL_005d:
			int num8;
			if (P_3 <= 0)
			{
				num = 2063926019;
				num8 = num;
			}
			else
			{
				num = 290887388;
				num8 = num;
			}
			goto IL_0008;
		}

		private static _0001[] nQmwWnCHPYVCLuzrvmvPgngUutpO<_0001>(_0001[] P_0, _0001 P_1, int P_2, bool P_3)
		{
			if (P_2 <= 0)
			{
				goto IL_0007;
			}
			goto IL_00ae;
			IL_0007:
			int num = 2093287594;
			goto IL_000c;
			IL_000c:
			_0001[] array = default(_0001[]);
			int destinationIndex = default(int);
			int destinationIndex2 = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x59D7DB0A)) % 9)
				{
				case 5u:
					break;
				case 4u:
				{
					_0001[] sourceArray = hHxcBadBAzEtepFFOSuOCGpePeZx(P_2, P_1);
					Array.Copy(P_0, 0, array, destinationIndex, P_0.Length);
					Array.Copy(sourceArray, 0, array, destinationIndex2, P_2);
					num = 1192999778;
					continue;
				}
				case 0u:
				{
					int num3;
					int num4;
					if (!P_3)
					{
						num3 = -932791202;
						num4 = num3;
					}
					else
					{
						num3 = -1999218880;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 1085803529);
					continue;
				}
				case 3u:
					destinationIndex2 = 0;
					num = ((int)num2 * -1847260959) ^ 0x22311327;
					continue;
				case 1u:
					destinationIndex2 = P_0.Length;
					num = (int)((num2 * 1922781753) ^ 0x25E9CAAB);
					continue;
				case 8u:
					goto IL_00ae;
				case 7u:
					destinationIndex = P_2;
					num = ((int)num2 * -1934661435) ^ 0x1A23C87D;
					continue;
				case 6u:
					return P_0;
				default:
					return array;
				}
				break;
			}
			goto IL_0007;
			IL_00ae:
			array = new _0001[P_0.Length + P_2];
			destinationIndex = 0;
			num = 1974163880;
			goto IL_000c;
		}

		private static _0001[] qbpFVfghxXeopRSAodKufgIjsyyV<_0001>(_0001[] P_0, _0001[] P_1)
		{
			if (P_0 == null)
			{
				goto IL_0006;
			}
			goto IL_0088;
			IL_0006:
			int num = 283644339;
			goto IL_000b;
			IL_000b:
			_0001[] array = default(_0001[]);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x53A34D35)) % 11)
				{
				case 2u:
					break;
				case 8u:
					array = new _0001[P_0.Length + P_1.Length];
					Array.Copy(P_0, 0, array, 0, P_0.Length);
					num = 155790141;
					continue;
				case 7u:
				{
					int num5;
					int num6;
					if (P_1 == null)
					{
						num5 = 1725279600;
						num6 = num5;
					}
					else
					{
						num5 = 907129420;
						num6 = num5;
					}
					num = num5 ^ (int)(num2 * 1479452267);
					continue;
				}
				case 9u:
					goto IL_0088;
				case 5u:
					goto IL_009f;
				case 3u:
					return null;
				case 6u:
					return P_0;
				case 1u:
				{
					int num3;
					int num4;
					if (P_1 != null)
					{
						num3 = 101335490;
						num4 = num3;
					}
					else
					{
						num3 = 1255629663;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 1632623479);
					continue;
				}
				case 4u:
					return P_1;
				case 10u:
					Array.Copy(P_1, 0, array, P_0.Length, P_1.Length);
					num = (int)(num2 * 2010323487) ^ -2017783014;
					continue;
				default:
					return array;
				}
				break;
				IL_009f:
				int num7;
				if (P_0 == null)
				{
					num = 354176062;
					num7 = num;
				}
				else
				{
					num = 1649201123;
					num7 = num;
				}
			}
			goto IL_0006;
			IL_0088:
			int num8;
			if (P_0 != null)
			{
				num = 7438077;
				num8 = num;
			}
			else
			{
				num = 1380902612;
				num8 = num;
			}
			goto IL_000b;
		}

		private static _0001[] hHxcBadBAzEtepFFOSuOCGpePeZx<_0001>(int P_0, _0001 P_1)
		{
			if (P_0 <= 0)
			{
				goto IL_0004;
			}
			goto IL_006d;
			IL_0004:
			int num = -593652641;
			goto IL_0009;
			IL_0009:
			_0001[] array = default(_0001[]);
			int num3 = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -817945405)) % 7)
				{
				case 2u:
					break;
				case 4u:
					goto IL_0036;
				case 5u:
					array[num3] = P_1;
					num3++;
					num = -1537786965;
					continue;
				case 1u:
					num = (int)((num2 * 881656790) ^ 0x4EE74F23);
					continue;
				case 0u:
					goto IL_006d;
				case 6u:
					return null;
				default:
					return array;
				}
				break;
				IL_0036:
				int num4;
				if (num3 >= P_0)
				{
					num = -175431542;
					num4 = num;
				}
				else
				{
					num = -617946856;
					num4 = num;
				}
			}
			goto IL_0004;
			IL_006d:
			array = new _0001[P_0];
			num3 = 0;
			num = -2025489137;
			goto IL_0009;
		}

		private static SubmeshData[] TxueiogIGxDPSpvSEupTuIMwnbAb(MeshData P_0, MeshData P_1)
		{
			List<SubmeshData> list = new List<SubmeshData>(P_0.Submeshes);
			SubmeshData submeshData = default(SubmeshData);
			SubmeshData[] submeshes = default(SubmeshData[]);
			int num6 = default(int);
			bool flag = default(bool);
			SubmeshData submeshData2 = default(SubmeshData);
			int num5 = default(int);
			while (true)
			{
				int num = 1692855120;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x41B0F4AD)) % 18)
					{
					case 0u:
						break;
					case 13u:
						submeshData = submeshes[num6];
						flag = false;
						num = 296226810;
						continue;
					case 12u:
					{
						SubmeshData value = new SubmeshData(gBqTmlISjSPJOgfOMyeIbxJcPgve(submeshData2.TriangleList, submeshData.TriangleList, P_0.VertexCount()), submeshData2.Identifier);
						list[num5] = value;
						num = (int)(num2 * 641857193) ^ -953730805;
						continue;
					}
					case 9u:
					{
						int num9;
						if (num6 < submeshes.Length)
						{
							num = 1469777104;
							num9 = num;
						}
						else
						{
							num = 2021961197;
							num9 = num;
						}
						continue;
					}
					case 7u:
						num5 = 0;
						num = ((int)num2 * -1380728025) ^ -1595970804;
						continue;
					case 14u:
					{
						int num7;
						if (!flag)
						{
							num = 1102476893;
							num7 = num;
						}
						else
						{
							num = 598864384;
							num7 = num;
						}
						continue;
					}
					case 2u:
					{
						int num8;
						if (num5 < P_0.Submeshes.Length)
						{
							num = 110961932;
							num8 = num;
						}
						else
						{
							num = 280623169;
							num8 = num;
						}
						continue;
					}
					case 5u:
						num6++;
						num = 773768580;
						continue;
					case 15u:
						submeshData2 = list[num5];
						num = 1760005975;
						continue;
					case 1u:
						num5++;
						num = 1690952781;
						continue;
					case 11u:
						num = ((int)num2 * -677251586) ^ 0x2F9B461A;
						continue;
					case 16u:
						flag = true;
						num = ((int)num2 * -814787917) ^ 0x647D3A0;
						continue;
					case 8u:
						num6 = 0;
						num = (int)(num2 * 378084995) ^ -1316286230;
						continue;
					case 3u:
						num = ((int)num2 * -561524336) ^ 0x54431C51;
						continue;
					case 10u:
					{
						SubmeshData item = new SubmeshData(uXOrBEjcYHpPaTUstbsWGInccHCDA(submeshData.TriangleList, P_0.VertexCount()), submeshData.Identifier);
						list.Add(item);
						num = (int)((num2 * 1384240841) ^ 0x72309C70);
						continue;
					}
					case 17u:
						submeshes = P_1.Submeshes;
						num = ((int)num2 * -171625272) ^ 0xC6277D7;
						continue;
					case 6u:
					{
						int num3;
						int num4;
						if (submeshData.Identifier.Equals(submeshData2.Identifier))
						{
							num3 = -494183713;
							num4 = num3;
						}
						else
						{
							num3 = -703816654;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 856547204);
						continue;
					}
					default:
						return list.ToArray();
					}
					break;
				}
			}
		}

		private static void OUGmQMJYOCCkPkYlLJZJpewsRyQl(MeshData P_0, MeshData P_1, ref MeshData P_2)
		{
			bool flag = P_0.IsSkinned();
			bool flag2 = P_1.IsSkinned();
			if (!flag)
			{
				goto IL_0016;
			}
			goto IL_0214;
			IL_0016:
			int num = -1327841925;
			goto IL_001b;
			IL_001b:
			int num4 = default(int);
			int num6 = default(int);
			int num5 = default(int);
			int num7 = default(int);
			BindposeData bindposeData = default(BindposeData);
			int num3 = default(int);
			List<RiVCtleVXwSSVbUXJhvGLXJjIgiKA> list = default(List<RiVCtleVXwSSVbUXJhvGLXJjIgiKA>);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -1556662197)) % 33)
				{
				case 3u:
					break;
				default:
					return;
				case 23u:
				{
					int num10;
					int num11;
					if (!flag)
					{
						num10 = -224841354;
						num11 = num10;
					}
					else
					{
						num10 = -324234266;
						num11 = num10;
					}
					num = num10 ^ ((int)num2 * -188219990);
					continue;
				}
				case 29u:
					goto IL_00d5;
				case 8u:
					P_2.BindPoses = new BindposeData[P_0.BindPoses.Length];
					num = (int)((num2 * 1840232257) ^ 0x529C1EB9);
					continue;
				case 21u:
					num4 = 0;
					num = (int)(num2 * 1837526445) ^ -1199169603;
					continue;
				case 2u:
				{
					int num12;
					int num13;
					if (flag2)
					{
						num12 = -1032047474;
						num13 = num12;
					}
					else
					{
						num12 = -1468018068;
						num13 = num12;
					}
					num = num12 ^ (int)(num2 * 186457169);
					continue;
				}
				case 24u:
					Array.Copy(P_1.BindPoses, 0, P_2.BindPoses, 0, P_1.BindPoses.Length);
					num = ((int)num2 * -1890630532) ^ -1899884766;
					continue;
				case 31u:
					return;
				case 13u:
				{
					int num8;
					int num9;
					if (!flag2)
					{
						num8 = -1491196846;
						num9 = num8;
					}
					else
					{
						num8 = -518283844;
						num9 = num8;
					}
					num = num8 ^ (int)(num2 * 301345847);
					continue;
				}
				case 27u:
					P_2.BoneWeights = nQmwWnCHPYVCLuzrvmvPgngUutpO(P_1.BoneWeights, BoneWeightData.Null, P_0.VertexCount(), false);
					return;
				case 22u:
					num6 = 0;
					num = ((int)num2 * -1262325386) ^ 0x74BD1E64;
					continue;
				case 6u:
					goto IL_01f4;
				case 0u:
					goto IL_0214;
				case 5u:
					num5 = num7;
					num = (int)((num2 * 1312706531) ^ 0x5A237165);
					continue;
				case 30u:
					P_2.BindPoses = P_0.BindPoses;
					num7 = P_0.BindPoses.Length;
					num = ((int)num2 * -1756176026) ^ 0x74CAB05C;
					continue;
				case 4u:
					bindposeData = P_1.BindPoses[num6];
					num3 = -1;
					num = -180612527;
					continue;
				case 10u:
					list.Add(new RiVCtleVXwSSVbUXJhvGLXJjIgiKA
					{
						jZWlxsfPRxtufPRcbICXHIJMMziG = num6,
						gGxjWHMXXJuUvYCcLlPSUJVOhWqD = num3
					});
					num = ((int)num2 * -1056928545) ^ -470019280;
					continue;
				case 26u:
					num3 = num5;
					P_2.BindPoses = nQmwWnCHPYVCLuzrvmvPgngUutpO(P_2.BindPoses, bindposeData, 1, true);
					num = (int)(num2 * 1487847492) ^ -340870902;
					continue;
				case 7u:
					goto IL_02fb;
				case 32u:
					goto IL_032a;
				case 14u:
					num4++;
					num = -1164797393;
					continue;
				case 11u:
					P_2.BoneWeights = nQmwWnCHPYVCLuzrvmvPgngUutpO(P_0.BoneWeights, BoneWeightData.Null, P_1.VertexCount(), true);
					return;
				case 19u:
					P_2.BoneWeights = qbpFVfghxXeopRSAodKufgIjsyyV(P_0.BoneWeights, ECZNDbfRQZwXRYlCNwTnRfbjFiNM(P_1.BoneWeights, list));
					num = ((int)num2 * -931507346) ^ -1903363488;
					continue;
				case 18u:
					num6++;
					num = -775499562;
					continue;
				case 1u:
					num5++;
					num = (int)(num2 * 945554798) ^ -381444748;
					continue;
				case 25u:
					num = ((int)num2 * -492779072) ^ 0x20E83373;
					continue;
				case 15u:
					Array.Copy(P_0.BindPoses, 0, P_2.BindPoses, 0, P_0.BindPoses.Length);
					num = ((int)num2 * -555841276) ^ -7036771;
					continue;
				case 12u:
					list = new List<RiVCtleVXwSSVbUXJhvGLXJjIgiKA>();
					num = -1845863453;
					continue;
				case 16u:
					P_2.BindPoses = new BindposeData[P_1.BindPoses.Length];
					num = (int)(num2 * 251443178) ^ -1974435992;
					continue;
				case 28u:
					num3 = num4;
					num = (int)((num2 * 884151306) ^ 0x7C76C968);
					continue;
				case 9u:
					goto IL_046a;
				case 20u:
					goto IL_0484;
				case 17u:
					return;
				}
				break;
				IL_0484:
				int num14;
				if (num3 >= 0)
				{
					num = -598361126;
					num14 = num;
				}
				else
				{
					num = -1393864573;
					num14 = num;
				}
				continue;
				IL_032a:
				int num15;
				if (flag2)
				{
					num = -399950957;
					num15 = num;
				}
				else
				{
					num = -2034017309;
					num15 = num;
				}
				continue;
				IL_01f4:
				int num16;
				if (num6 < P_1.BindPoses.Length)
				{
					num = -1813433889;
					num16 = num;
				}
				else
				{
					num = -1468294115;
					num16 = num;
				}
				continue;
				IL_046a:
				int num17;
				if (num6 != num3)
				{
					num = -241015691;
					num17 = num;
				}
				else
				{
					num = -1083092686;
					num17 = num;
				}
				continue;
				IL_00d5:
				int num18;
				if (num4 < num7)
				{
					num = -65845126;
					num18 = num;
				}
				else
				{
					num = -523911245;
					num18 = num;
				}
				continue;
				IL_02fb:
				int num19;
				if (P_0.BindPoses[num4].NameHash != bindposeData.NameHash)
				{
					num = -878378702;
					num19 = num;
				}
				else
				{
					num = -658915244;
					num19 = num;
				}
			}
			goto IL_0016;
			IL_0214:
			P_2.SkinningQuality = ((P_0.SkinningQuality > P_1.SkinningQuality) ? P_0.SkinningQuality : P_1.SkinningQuality);
			num = -1828847991;
			goto IL_001b;
		}

		private static BlendshapeFrameData[] XFkTehtTdADWyqJshZZllkWsqwAG(MeshData P_0, MeshData P_1)
		{
			bool flag = P_0.BlendShapes != null;
			bool flag2 = P_1.BlendShapes != null;
			BlendshapeFrameData blendshapeFrameData2 = default(BlendshapeFrameData);
			BlendshapeFrameData blendshapeFrameData4 = default(BlendshapeFrameData);
			int num7 = default(int);
			int num5 = default(int);
			BlendshapeFrameData[] array = default(BlendshapeFrameData[]);
			int num3 = default(int);
			BlendshapeFrameData blendshapeFrameData = default(BlendshapeFrameData);
			int num4 = default(int);
			BlendshapeFrameData[] array2 = default(BlendshapeFrameData[]);
			while (true)
			{
				int num = 600598275;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x290D4D01)) % 21)
					{
					case 20u:
						break;
					case 18u:
						blendshapeFrameData2 = new BlendshapeFrameData(blendshapeFrameData4.Name, blendshapeFrameData4.FrameNumber, blendshapeFrameData4.Weight, nQmwWnCHPYVCLuzrvmvPgngUutpO(blendshapeFrameData4.VertexDelta, Vector3.zero, num7, true), nQmwWnCHPYVCLuzrvmvPgngUutpO(blendshapeFrameData4.NormalDelta, Vector3.zero, num7, true), nQmwWnCHPYVCLuzrvmvPgngUutpO(blendshapeFrameData4.TangentDelta, Vector3.zero, num7, true));
						num = (int)(num2 * 1389487017) ^ -1781545992;
						continue;
					case 3u:
						blendshapeFrameData4 = P_0.BlendShapes[num5];
						num = 479783561;
						continue;
					case 13u:
					{
						int num14;
						int num15;
						if (!flag)
						{
							num14 = -490280831;
							num15 = num14;
						}
						else
						{
							num14 = -474623726;
							num15 = num14;
						}
						num = num14 ^ (int)(num2 * 1986119494);
						continue;
					}
					case 2u:
					{
						int num13;
						if (num5 < P_0.BlendShapes.Length)
						{
							num = 668291100;
							num13 = num;
						}
						else
						{
							num = 1088647570;
							num13 = num;
						}
						continue;
					}
					case 8u:
					{
						int num9;
						int num10;
						if (flag)
						{
							num9 = 34588463;
							num10 = num9;
						}
						else
						{
							num9 = 1041787613;
							num10 = num9;
						}
						num = num9 ^ ((int)num2 * -1604987345);
						continue;
					}
					case 11u:
						return null;
					case 15u:
						array = null;
						num = 626437860;
						continue;
					case 14u:
						num5 = 0;
						num = (int)(num2 * 150455031) ^ -1855484582;
						continue;
					case 7u:
						aRkGSMIGeieLsDqBcTBrPOpkjHAu(blendshapeFrameData4, "m1");
						num = (int)((num2 * 1447874476) ^ 0x252158CA);
						continue;
					case 10u:
					{
						int num11;
						int num12;
						if (!flag2)
						{
							num11 = -1291618878;
							num12 = num11;
						}
						else
						{
							num11 = -1435767538;
							num12 = num11;
						}
						num = num11 ^ (int)(num2 * 924202380);
						continue;
					}
					case 12u:
					{
						int num8;
						if (num3 >= P_1.BlendShapes.Length)
						{
							num = 1152307077;
							num8 = num;
						}
						else
						{
							num = 1555507251;
							num8 = num;
						}
						continue;
					}
					case 4u:
						num7 = P_1.VertexCount();
						num = ((int)num2 * -2010125881) ^ 0x4BCCB79D;
						continue;
					case 0u:
						array = new BlendshapeFrameData[P_0.BlendShapes.Length];
						num = (int)((num2 * 2010187298) ^ 0x2481AB9);
						continue;
					case 19u:
					{
						BlendshapeFrameData blendshapeFrameData3 = P_1.BlendShapes[num3];
						aRkGSMIGeieLsDqBcTBrPOpkjHAu(blendshapeFrameData3, "m2");
						blendshapeFrameData = new BlendshapeFrameData(blendshapeFrameData3.Name, blendshapeFrameData3.FrameNumber, blendshapeFrameData3.Weight, nQmwWnCHPYVCLuzrvmvPgngUutpO(blendshapeFrameData3.VertexDelta, Vector3.zero, num4, false), nQmwWnCHPYVCLuzrvmvPgngUutpO(blendshapeFrameData3.NormalDelta, Vector3.zero, num4, false), nQmwWnCHPYVCLuzrvmvPgngUutpO(blendshapeFrameData3.TangentDelta, Vector3.zero, num4, false));
						num = 1843108654;
						continue;
					}
					case 16u:
					{
						int num6;
						if (!flag2)
						{
							num = 1152307077;
							num6 = num;
						}
						else
						{
							num = 451871181;
							num6 = num;
						}
						continue;
					}
					case 5u:
						array[num5] = blendshapeFrameData2;
						num5++;
						num = ((int)num2 * -1332719938) ^ 0x1FAAC450;
						continue;
					case 1u:
						array2 = null;
						num = ((int)num2 * -1458197597) ^ 0x3630B82F;
						continue;
					case 17u:
						array2 = new BlendshapeFrameData[P_1.BlendShapes.Length];
						num4 = P_0.VertexCount();
						num3 = 0;
						num = ((int)num2 * -1583251799) ^ 0x1B27ECC2;
						continue;
					case 6u:
						array2[num3] = blendshapeFrameData;
						num3++;
						num = ((int)num2 * -1530918726) ^ 0x5B53E048;
						continue;
					default:
						return qbpFVfghxXeopRSAodKufgIjsyyV(array, array2);
					}
					break;
				}
			}
		}

		private static int[] uXOrBEjcYHpPaTUstbsWGInccHCDA(int[] P_0, int P_1)
		{
			int[] array = new int[P_0.Length];
			int num3 = default(int);
			while (true)
			{
				int num = 1306655591;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x15A66441)) % 7)
					{
					case 3u:
						break;
					case 4u:
					{
						int num4;
						if (num3 >= P_0.Length)
						{
							num = 1415695610;
							num4 = num;
						}
						else
						{
							num = 364430132;
							num4 = num;
						}
						continue;
					}
					case 0u:
						num = (int)((num2 * 245157483) ^ 0x67E0F29E);
						continue;
					case 1u:
						num3 = 0;
						num = (int)((num2 * 1293268993) ^ 0x4B5D3B24);
						continue;
					case 2u:
						num3++;
						num = (int)((num2 * 2023531009) ^ 0xEF61A35);
						continue;
					case 5u:
						array[num3] = P_0[num3] + P_1;
						num = 882675435;
						continue;
					default:
						return array;
					}
					break;
				}
			}
		}

		private static int[] gBqTmlISjSPJOgfOMyeIbxJcPgve(int[] P_0, int[] P_1, int P_2)
		{
			return qbpFVfghxXeopRSAodKufgIjsyyV(P_0, uXOrBEjcYHpPaTUstbsWGInccHCDA(P_1, P_2));
		}

		private static void MtnDJKrkWTCzGZlFAfHTHgOFDtShA(ref MeshData P_0)
		{
			List<int> list = new List<int>();
			BoneWeightData boneWeightData = BoneWeightData.Null;
			int num = 0;
			int num10 = default(int);
			int num7 = default(int);
			List<RiVCtleVXwSSVbUXJhvGLXJjIgiKA> list2 = default(List<RiVCtleVXwSSVbUXJhvGLXJjIgiKA>);
			while (true)
			{
				int num2;
				int num3;
				if (num < P_0.BoneWeights.Length)
				{
					num2 = 2041722328;
					num3 = num2;
				}
				else
				{
					num2 = 598380606;
					num3 = num2;
				}
				while (true)
				{
					uint num4;
					switch ((num4 = (uint)(num2 ^ 0x787AFB27)) % 17)
					{
					case 15u:
						num2 = 2041722328;
						continue;
					case 10u:
					{
						int num14;
						if (num10 != -1)
						{
							num2 = 147338010;
							num14 = num2;
						}
						else
						{
							num2 = 1244756470;
							num14 = num2;
						}
						continue;
					}
					case 0u:
					{
						int num12;
						if (!P_0.BindPoses[num7].IsRoot)
						{
							num2 = 615149218;
							num12 = num2;
						}
						else
						{
							num2 = 340770758;
							num12 = num2;
						}
						continue;
					}
					case 1u:
						num++;
						num2 = 506762945;
						continue;
					case 5u:
						throw new ArgumentException("No root bone was found in the bindpose list for this mesh. Ensure you have specified a valid root transform.");
					case 2u:
						return;
					case 4u:
						num7++;
						num2 = 445899945;
						continue;
					case 16u:
					{
						int num13;
						if (P_0.BoneWeights[num] == boneWeightData)
						{
							num2 = 1433996316;
							num13 = num2;
						}
						else
						{
							num2 = 597532663;
							num13 = num2;
						}
						continue;
					}
					case 12u:
						num10 = num7;
						num2 = ((int)num4 * -1431461249) ^ 0xFF5824A;
						continue;
					case 14u:
						list.Add(num);
						num2 = ((int)num4 * -1715996301) ^ 0x31F0ED76;
						continue;
					case 9u:
						break;
					case 7u:
						list2 = new List<RiVCtleVXwSSVbUXJhvGLXJjIgiKA>
						{
							new RiVCtleVXwSSVbUXJhvGLXJjIgiKA
							{
								jZWlxsfPRxtufPRcbICXHIJMMziG = 0,
								gGxjWHMXXJuUvYCcLlPSUJVOhWqD = num10
							}
						};
						num2 = 1132075654;
						continue;
					case 11u:
					{
						int num11;
						if (num7 < P_0.BindPoses.Length)
						{
							num2 = 1430072347;
							num11 = num2;
						}
						else
						{
							num2 = 1441075157;
							num11 = num2;
						}
						continue;
					}
					case 13u:
						num10 = -1;
						num2 = 1107530815;
						continue;
					case 6u:
					{
						int num8;
						int num9;
						if (list.Count == 0)
						{
							num8 = -956325802;
							num9 = num8;
						}
						else
						{
							num8 = -1916212216;
							num9 = num8;
						}
						num2 = num8 ^ (int)(num4 * 137033727);
						continue;
					}
					case 8u:
						num7 = 0;
						num2 = (int)(num4 * 696041301) ^ -1320735151;
						continue;
					default:
					{
						using (List<int>.Enumerator enumerator = list.GetEnumerator())
						{
							while (true)
							{
								int num5;
								int num6;
								if (enumerator.MoveNext())
								{
									num5 = 481816816;
									num6 = num5;
								}
								else
								{
									num5 = 2132272302;
									num6 = num5;
								}
								while (true)
								{
									switch ((num4 = (uint)(num5 ^ 0x787AFB27)) % 4)
									{
									case 2u:
										num5 = 481816816;
										continue;
									default:
										return;
									case 3u:
									{
										int current = enumerator.Current;
										P_0.BoneWeights[current] = vXtlFiMQgWHvDIkzONOckHRfiVYN(BoneWeightData.FullToFirst, list2);
										num5 = 1578957923;
										continue;
									}
									case 0u:
										break;
									case 1u:
										return;
									}
									break;
								}
							}
						}
					}
					}
					break;
				}
			}
		}

		private static BoneWeightData[] ECZNDbfRQZwXRYlCNwTnRfbjFiNM(BoneWeightData[] P_0, List<RiVCtleVXwSSVbUXJhvGLXJjIgiKA> P_1)
		{
			if (P_1.Count == 0)
			{
				goto IL_0008;
			}
			goto IL_0054;
			IL_0008:
			int num = -208889230;
			goto IL_000d;
			IL_000d:
			int num3 = default(int);
			BoneWeightData[] array = default(BoneWeightData[]);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -222462418)) % 8)
				{
				case 7u:
					break;
				case 5u:
					num3++;
					num = ((int)num2 * -125275711) ^ 0x690B5F0B;
					continue;
				case 6u:
					goto IL_0054;
				case 4u:
					return P_0;
				case 0u:
					goto IL_0077;
				case 3u:
					array[num3] = vXtlFiMQgWHvDIkzONOckHRfiVYN(P_0[num3], P_1);
					num = -743369269;
					continue;
				case 1u:
					num = (int)((num2 * 341883047) ^ 0x878B0E9);
					continue;
				default:
					return array;
				}
				break;
				IL_0077:
				int num4;
				if (num3 >= P_0.Length)
				{
					num = -145458892;
					num4 = num;
				}
				else
				{
					num = -1026936171;
					num4 = num;
				}
			}
			goto IL_0008;
			IL_0054:
			array = new BoneWeightData[P_0.Length];
			num3 = 0;
			num = -1515328945;
			goto IL_000d;
		}

		private static BoneWeightData vXtlFiMQgWHvDIkzONOckHRfiVYN(BoneWeightData P_0, List<RiVCtleVXwSSVbUXJhvGLXJjIgiKA> P_1)
		{
			BoneWeightData boneWeightData = new BoneWeightData
			{
				Weight0 = P_0.Weight0,
				Weight1 = P_0.Weight1
			};
			BoneWeightData result = default(BoneWeightData);
			while (true)
			{
				int num = -613288926;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1270719948)) % 6)
					{
					case 5u:
						break;
					case 4u:
						boneWeightData.Weight2 = P_0.Weight2;
						boneWeightData.Weight3 = P_0.Weight3;
						num = ((int)num2 * -2035944410) ^ -1052797956;
						continue;
					case 2u:
						result = boneWeightData;
						result.Bone0 = rbSsFTkVIlbDPMWxwXSuODgevDVJ(P_0.Bone0, P_1);
						num = (int)((num2 * 1286176163) ^ 0x446A8B7D);
						continue;
					case 1u:
						result.Bone1 = rbSsFTkVIlbDPMWxwXSuODgevDVJ(P_0.Bone1, P_1);
						num = (int)(num2 * 1104685966) ^ -2124403293;
						continue;
					case 3u:
						result.Bone2 = rbSsFTkVIlbDPMWxwXSuODgevDVJ(P_0.Bone2, P_1);
						result.Bone3 = rbSsFTkVIlbDPMWxwXSuODgevDVJ(P_0.Bone3, P_1);
						num = (int)(num2 * 648378359) ^ -362437985;
						continue;
					default:
						return result;
					}
					break;
				}
			}
		}

		private static int rbSsFTkVIlbDPMWxwXSuODgevDVJ(int P_0, List<RiVCtleVXwSSVbUXJhvGLXJjIgiKA> P_1)
		{
			using (List<RiVCtleVXwSSVbUXJhvGLXJjIgiKA>.Enumerator enumerator = P_1.GetEnumerator())
			{
				int gGxjWHMXXJuUvYCcLlPSUJVOhWqD = default(int);
				RiVCtleVXwSSVbUXJhvGLXJjIgiKA current = default(RiVCtleVXwSSVbUXJhvGLXJjIgiKA);
				while (true)
				{
					IL_0087:
					int num;
					int num2;
					if (!enumerator.MoveNext())
					{
						num = 1269764864;
						num2 = num;
					}
					else
					{
						num = 557099342;
						num2 = num;
					}
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num ^ 0x5842D505)) % 7)
						{
						case 5u:
							num = 557099342;
							continue;
						default:
							goto end_IL_000e;
						case 6u:
							gGxjWHMXXJuUvYCcLlPSUJVOhWqD = current.gGxjWHMXXJuUvYCcLlPSUJVOhWqD;
							num = (int)(num3 * 1868083598) ^ -2047181894;
							continue;
						case 3u:
						{
							int num4;
							int num5;
							if (current.jZWlxsfPRxtufPRcbICXHIJMMziG != P_0)
							{
								num4 = -907972303;
								num5 = num4;
							}
							else
							{
								num4 = -1649990120;
								num5 = num4;
							}
							num = num4 ^ (int)(num3 * 1593756899);
							continue;
						}
						case 0u:
							break;
						case 4u:
							current = enumerator.Current;
							num = 1625949838;
							continue;
						case 2u:
							goto end_IL_000e;
						case 1u:
							return gGxjWHMXXJuUvYCcLlPSUJVOhWqD;
						}
						goto IL_0087;
						continue;
						end_IL_000e:
						break;
					}
					break;
				}
			}
			return P_0;
		}

		private static bool fRBfIAIBXQkpRItZTYaquwHNzhaRA(CombinerSettingsFlag P_0, CombinerSettingsFlag P_1)
		{
			return (P_0 & P_1) == P_1;
		}
	}
}
