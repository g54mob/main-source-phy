using System;
using System.Collections.Generic;
using BitCode.MeshTool.DataTypes;
using UnityEngine;

namespace BitCode.MeshTool
{
	public static class AnimationRigConverter
	{
		public static TransformData[] ConvertChildrenToRigData(Transform root)
		{
			if (root == null)
			{
				goto IL_0009;
			}
			goto IL_0049;
			IL_0009:
			int num = -1662999962;
			goto IL_000e;
			IL_000e:
			uint num2;
			Transform[] componentsInChildren = default(Transform[]);
			switch ((num2 = (uint)(num ^ -1022919212)) % 4)
			{
			case 0u:
				break;
			case 2u:
				throw new ArgumentNullException("root");
			case 3u:
				goto IL_0049;
			default:
				return dHOAcWGpsDLwIAFRpAJtqeRgOLjCA(root, componentsInChildren);
			}
			goto IL_0009;
			IL_0049:
			componentsInChildren = root.GetComponentsInChildren<Transform>();
			num = -1824331951;
			goto IL_000e;
		}

		public static TransformData[] ConvertSkinRenderersToRigData(Transform root, SkinnedMeshRenderer[] boundRenderers)
		{
			if (root == null)
			{
				goto IL_000c;
			}
			goto IL_008c;
			IL_000c:
			int num = 1975748440;
			goto IL_0011;
			IL_0011:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x6DBD45FD)) % 7)
				{
				case 5u:
					break;
				case 6u:
					throw new ArgumentNullException("root");
				case 1u:
					throw new ArgumentNullException("boundRenderers");
				case 2u:
					throw new ArgumentException("boundRenderers has a length of zero. Please ensure this array is populated.");
				case 0u:
					goto IL_008c;
				case 4u:
					goto IL_00a3;
				default:
					return dHOAcWGpsDLwIAFRpAJtqeRgOLjCA(root, CAWnYsQnpRoTrTNysmjKTFnoMvOV(root, boundRenderers));
				}
				break;
				IL_00a3:
				int num3;
				if (boundRenderers.Length != 0)
				{
					num = 153890665;
					num3 = num;
				}
				else
				{
					num = 1456404066;
					num3 = num;
				}
			}
			goto IL_000c;
			IL_008c:
			int num4;
			if (boundRenderers != null)
			{
				num = 1900213494;
				num4 = num;
			}
			else
			{
				num = 850670557;
				num4 = num;
			}
			goto IL_0011;
		}

		public static Transform ConvertToRig(TransformData[] rigData, out Transform[] bones)
		{
			if (rigData == null)
			{
				goto IL_0006;
			}
			goto IL_0255;
			IL_0006:
			int num = -200678450;
			goto IL_000b;
			IL_000b:
			Transform transform2 = default(Transform);
			Dictionary<string, Transform> dictionary = default(Dictionary<string, Transform>);
			TransformData transformData = default(TransformData);
			Transform transform3 = default(Transform);
			TransformData transformData2 = default(TransformData);
			int num3 = default(int);
			int num6 = default(int);
			TransformData[] array = default(TransformData[]);
			Transform transform = default(Transform);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -2145332917)) % 26)
				{
				case 24u:
					break;
				case 13u:
					transform2.parent = dictionary[transformData.ParentName];
					num = (int)((num2 * 1859420302) ^ 0x51189A3);
					continue;
				case 23u:
					goto IL_00b0;
				case 2u:
					throw new InvalidOperationException("Could not derive root bone from provided data.");
				case 19u:
					goto IL_0109;
				case 4u:
					goto IL_0124;
				case 25u:
					transform3.name = transformData2.BoneName;
					dictionary.Add(transform3.name, transform3);
					bones[num3] = transform3;
					num = ((int)num2 * -649628225) ^ 0x7B21600B;
					continue;
				case 15u:
					num = (int)(num2 * 1593590403) ^ -1643479691;
					continue;
				case 5u:
					transform3 = new GameObject().transform;
					num = ((int)num2 * -1862314653) ^ -268382553;
					continue;
				case 14u:
					transform2.localPosition = transformData.LocalPosition;
					transform2.localRotation = transformData.LocalRotation;
					num6++;
					num = -418933017;
					continue;
				case 22u:
					bones = new Transform[rigData.Length];
					num = ((int)num2 * -1733034612) ^ 0x1DA80659;
					continue;
				case 18u:
					array = rigData;
					num6 = 0;
					num = (int)(num2 * 704660936) ^ -897023138;
					continue;
				case 3u:
					throw new ArgumentNullException("rigData");
				case 11u:
					num6++;
					num = ((int)num2 * -400191477) ^ 0x7CC38A6B;
					continue;
				case 17u:
					num6 = 0;
					num = ((int)num2 * -1590982667) ^ -1852335848;
					continue;
				case 10u:
					goto IL_0255;
				case 7u:
					num3++;
					num = ((int)num2 * -2077638016) ^ -104903160;
					continue;
				case 9u:
					transformData2 = array[num6];
					num = -723362282;
					continue;
				case 16u:
					num = ((int)num2 * -457470066) ^ -2045021973;
					continue;
				case 1u:
					throw new ArgumentException("rigData has a length of zero. Please ensure this array is populated.");
				case 12u:
					dictionary = new Dictionary<string, Transform>(rigData.Length);
					num = -1463289073;
					continue;
				case 21u:
					transform = null;
					array = rigData;
					num = (int)((num2 * 571707640) ^ 0x395DD6);
					continue;
				case 8u:
					transform = transform2;
					num = -860925763;
					continue;
				case 6u:
				{
					int num4;
					int num5;
					if (transform == null)
					{
						num4 = 1244828943;
						num5 = num4;
					}
					else
					{
						num4 = 798774057;
						num5 = num4;
					}
					num = num4 ^ (int)(num2 * 1250053451);
					continue;
				}
				case 20u:
					num3 = 0;
					num = (int)((num2 * 228045179) ^ 0x691F0075);
					continue;
				default:
					return transform;
				}
				break;
				IL_0124:
				int num7;
				if (num6 < array.Length)
				{
					num = -671480566;
					num7 = num;
				}
				else
				{
					num = -1499022571;
					num7 = num;
				}
				continue;
				IL_0109:
				int num8;
				if (num6 < array.Length)
				{
					num = -313124166;
					num8 = num;
				}
				else
				{
					num = -964260940;
					num8 = num;
				}
				continue;
				IL_00b0:
				transformData = array[num6];
				transform2 = dictionary[transformData.BoneName];
				int num9;
				if (!string.IsNullOrEmpty(transformData.ParentName))
				{
					num = -1458975694;
					num9 = num;
				}
				else
				{
					num = -129831123;
					num9 = num;
				}
			}
			goto IL_0006;
			IL_0255:
			int num10;
			if (rigData.Length != 0)
			{
				num = -850927033;
				num10 = num;
			}
			else
			{
				num = -1260309220;
				num10 = num;
			}
			goto IL_000b;
		}

		private static Transform[] CAWnYsQnpRoTrTNysmjKTFnoMvOV(Transform P_0, SkinnedMeshRenderer[] P_1)
		{
			Transform[] componentsInChildren = P_0.GetComponentsInChildren<Transform>();
			Dictionary<string, Transform> dictionary = new Dictionary<string, Transform>();
			Transform[] array = componentsInChildren;
			int num11 = default(int);
			SkinnedMeshRenderer skinnedMeshRenderer = default(SkinnedMeshRenderer);
			SkinnedMeshRenderer[] array3 = default(SkinnedMeshRenderer[]);
			int num9 = default(int);
			List<string> list = default(List<string>);
			Transform transform3 = default(Transform);
			List<string> list3 = default(List<string>);
			List<string> list2 = default(List<string>);
			Transform transform = default(Transform);
			string current = default(string);
			int num7 = default(int);
			Transform[] array2 = default(Transform[]);
			while (true)
			{
				int num = 1113429807;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x5CEA48C4)) % 24)
					{
					case 5u:
						break;
					case 20u:
					{
						int num13;
						if (num11 < array.Length)
						{
							num = 2044914637;
							num13 = num;
						}
						else
						{
							num = 191658077;
							num13 = num;
						}
						continue;
					}
					case 13u:
						skinnedMeshRenderer = array3[num11];
						num = 1558221038;
						continue;
					case 10u:
						array = skinnedMeshRenderer.bones;
						num9 = 0;
						num = ((int)num2 * -2052385675) ^ 0x2BB1ED1B;
						continue;
					case 4u:
						num = (int)(num2 * 983415073) ^ -1759399276;
						continue;
					case 11u:
						num11 = 0;
						num = (int)((num2 * 332923928) ^ 0x6BB57740);
						continue;
					case 23u:
						list.Add(transform3.name);
						num = ((int)num2 * -915735704) ^ -1355501761;
						continue;
					case 21u:
						num = ((int)num2 * -223196308) ^ 0x23218B6A;
						continue;
					case 2u:
					{
						int num10;
						if (num9 < array.Length)
						{
							num = 2038673114;
							num10 = num;
						}
						else
						{
							num = 400786371;
							num10 = num;
						}
						continue;
					}
					case 6u:
					{
						transform3 = array[num9];
						int num15;
						if (dictionary.ContainsKey(transform3.name))
						{
							num = 1063825314;
							num15 = num;
						}
						else
						{
							num = 572579703;
							num15 = num;
						}
						continue;
					}
					case 9u:
					{
						int num14;
						if (num11 < array3.Length)
						{
							num = 184252105;
							num14 = num;
						}
						else
						{
							num = 72799414;
							num14 = num;
						}
						continue;
					}
					case 19u:
						throw new InvalidOperationException("SkinnedMeshRenderer " + skinnedMeshRenderer.name + " contains a reference to bone " + transform3.name + ", which is not a child of the provided root bone " + P_0.name + ".");
					case 15u:
						num11++;
						num = (int)((num2 * 1534881079) ^ 0x4C4DD1EC);
						continue;
					case 3u:
						num9++;
						num = 1816859030;
						continue;
					case 18u:
						list3 = new List<string> { P_0.name };
						num = (int)((num2 * 702276914) ^ 0x369465E0);
						continue;
					case 22u:
					{
						int num12;
						if (!list.Contains(transform3.name))
						{
							num = 1258095723;
							num12 = num;
						}
						else
						{
							num = 1445466919;
							num12 = num;
						}
						continue;
					}
					case 17u:
						list = new List<string>();
						num = (int)(num2 * 1552151098) ^ -396140904;
						continue;
					case 7u:
						num11 = 0;
						num = ((int)num2 * -783434659) ^ 0x5BB0CBC3;
						continue;
					case 0u:
						list2 = new List<string>(P_1.Length);
						num = ((int)num2 * -1387254930) ^ 0x504E7E04;
						continue;
					case 16u:
						num11++;
						num = ((int)num2 * -1739635201) ^ -264749256;
						continue;
					case 12u:
						num = (int)(num2 * 779028950) ^ -210750587;
						continue;
					case 14u:
						array3 = P_1;
						num = ((int)num2 * -2037613538) ^ -2098982521;
						continue;
					case 1u:
					{
						Transform transform2 = array[num11];
						dictionary.Add(transform2.name, transform2);
						num = 1311771044;
						continue;
					}
					default:
					{
						using (List<string>.Enumerator enumerator = list.GetEnumerator())
						{
							while (true)
							{
								IL_0373:
								int num3;
								int num4;
								if (!enumerator.MoveNext())
								{
									num3 = 966807342;
									num4 = num3;
								}
								else
								{
									num3 = 283198574;
									num4 = num3;
								}
								while (true)
								{
									switch ((num2 = (uint)(num3 ^ 0x5CEA48C4)) % 11)
									{
									case 0u:
										num3 = 283198574;
										continue;
									default:
										goto end_IL_0321;
									case 10u:
										num3 = ((int)num2 * -1438606283) ^ 0x75DEBE98;
										continue;
									case 2u:
										break;
									case 8u:
										transform = transform.parent;
										num3 = (int)(num2 * 69298622) ^ -1950648972;
										continue;
									case 6u:
										list3.AddRange(list2);
										num3 = ((int)num2 * -858046654) ^ 0x7FDB2EA2;
										continue;
									case 9u:
										list2.Add(transform.name);
										num3 = 1097574800;
										continue;
									case 4u:
									{
										int num5;
										if (list3.Contains(transform.name))
										{
											num3 = 167278543;
											num5 = num3;
										}
										else
										{
											num3 = 1830740063;
											num5 = num3;
										}
										continue;
									}
									case 3u:
										current = enumerator.Current;
										num3 = 2142033818;
										continue;
									case 5u:
										list2.Clear();
										num3 = ((int)num2 * -1012665623) ^ -2091061638;
										continue;
									case 1u:
										transform = dictionary[current];
										num3 = (int)(num2 * 1533896594) ^ -235239876;
										continue;
									case 7u:
										goto end_IL_0321;
									}
									goto IL_0373;
									continue;
									end_IL_0321:
									break;
								}
								break;
							}
						}
						int count = list3.Count;
						while (true)
						{
							int num6 = 315330391;
							while (true)
							{
								switch ((num2 = (uint)(num6 ^ 0x5CEA48C4)) % 7)
								{
								case 6u:
									break;
								case 1u:
									num7++;
									num6 = ((int)num2 * -25163956) ^ -1273357189;
									continue;
								case 4u:
									array2[num7] = dictionary[list3[num7]];
									num6 = 1558064396;
									continue;
								case 3u:
									num7 = 0;
									num6 = ((int)num2 * -121832822) ^ -408122937;
									continue;
								case 2u:
									array2 = new Transform[count];
									num6 = (int)((num2 * 570082217) ^ 0x65AC3A59);
									continue;
								case 0u:
								{
									int num8;
									if (num7 >= count)
									{
										num6 = 1050487862;
										num8 = num6;
									}
									else
									{
										num6 = 381099953;
										num8 = num6;
									}
									continue;
								}
								default:
									return array2;
								}
								break;
							}
						}
					}
					}
					break;
				}
			}
		}

		private static TransformData[] dHOAcWGpsDLwIAFRpAJtqeRgOLjCA(Transform P_0, Transform[] P_1)
		{
			TransformData[] array = new TransformData[P_1.Length];
			int num3 = default(int);
			Transform transform = default(Transform);
			while (true)
			{
				int num = -224823491;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1127765863)) % 7)
					{
					case 2u:
						break;
					case 6u:
						num3 = 0;
						num = ((int)num2 * -1204800254) ^ -1470657464;
						continue;
					case 0u:
						transform = P_1[num3];
						num = -1759460669;
						continue;
					case 4u:
						array[num3] = new TransformData(transform.name, transform.localPosition, transform.localRotation, (transform == P_0) ? string.Empty : transform.parent.name);
						num = -165808988;
						continue;
					case 5u:
					{
						int num4;
						if (num3 < P_1.Length)
						{
							num = -1259051883;
							num4 = num;
						}
						else
						{
							num = -1148998500;
							num4 = num;
						}
						continue;
					}
					case 1u:
						num3++;
						num = (int)((num2 * 1151575672) ^ 0x3A9CF598);
						continue;
					default:
						return array;
					}
					break;
				}
			}
		}
	}
}
