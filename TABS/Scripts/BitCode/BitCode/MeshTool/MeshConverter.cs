using System;
using System.Collections.Generic;
using BitCode.MeshTool.DataTypes;
using UnityEngine;

namespace BitCode.MeshTool
{
	public static class MeshConverter
	{
		public static MeshData[] ConvertToMeshData(RendererInput[] inputRenderers, Transform rootTransform, out Material[] materialMap, Transform animationRoot = null, bool calculateNewBinds = false)
		{
			if (inputRenderers == null)
			{
				goto IL_0006;
			}
			goto IL_024c;
			IL_0006:
			int num = -1452015090;
			goto IL_000b;
			IL_000b:
			List<Material> list2 = default(List<Material>);
			Renderer renderer = default(Renderer);
			RendererInput rendererInput = default(RendererInput);
			MeshData meshData = default(MeshData);
			SkinnedMeshRenderer skinnedMeshRenderer = default(SkinnedMeshRenderer);
			bool flag = default(bool);
			Mesh sharedMesh = default(Mesh);
			MeshData[] array = default(MeshData[]);
			int num5 = default(int);
			List<int> list = default(List<int>);
			Dictionary<string, BindposeData> dictionary = default(Dictionary<string, BindposeData>);
			Transform[] componentsInChildren = default(Transform[]);
			string[] array2 = default(string[]);
			int num6 = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -98011607)) % 41)
				{
				case 40u:
					break;
				case 18u:
					goto IL_00c5;
				case 14u:
					goto IL_00e8;
				case 1u:
					throw new ArgumentNullException("inputRenderers");
				case 23u:
					materialMap = list2.ToArray();
					num = -2001493954;
					continue;
				case 11u:
					renderer = rendererInput.Renderer;
					num = -707799211;
					continue;
				case 8u:
					goto IL_0146;
				case 6u:
					meshData.SkinningQuality = skinnedMeshRenderer.quality;
					num = ((int)num2 * -608653481) ^ 0x455600F0;
					continue;
				case 36u:
				{
					int num11;
					int num12;
					if (!flag)
					{
						num11 = 888770243;
						num12 = num11;
					}
					else
					{
						num11 = 871253734;
						num12 = num11;
					}
					num = num11 ^ (int)(num2 * 1899145420);
					continue;
				}
				case 24u:
					sharedMesh = skinnedMeshRenderer.sharedMesh;
					num = -434231175;
					continue;
				case 37u:
					array[num5] = meshData;
					num = -2105780690;
					continue;
				case 19u:
					array = new MeshData[inputRenderers.Length];
					list = new List<int>(inputRenderers.Length);
					dictionary = new Dictionary<string, BindposeData>();
					num = -1573624196;
					continue;
				case 35u:
					skinnedMeshRenderer = renderer as SkinnedMeshRenderer;
					flag = skinnedMeshRenderer != null;
					num = (int)((num2 * 174878985) ^ 0x4D363CF4);
					continue;
				case 17u:
					num = (int)((num2 * 2055813687) ^ 0x3D7D7CD8);
					continue;
				case 38u:
					goto IL_024c;
				case 31u:
					throw new ArgumentException(string.Format("Cannot include SkinnedMeshRenderer at index {0} without a valid {1} specified.", num5, "animationRoot"));
				case 34u:
					UrlZpOodTsQlCVPSNfopXCTBnLRu(ref meshData, rendererInput.OverrideColor);
					num = ((int)num2 * -1925401818) ^ -1248389000;
					continue;
				case 9u:
					componentsInChildren = animationRoot.GetComponentsInChildren<Transform>();
					num = ((int)num2 * -1305562507) ^ -859380499;
					continue;
				case 28u:
					num5++;
					num = (int)(num2 * 328786531) ^ -522288006;
					continue;
				case 32u:
					array2[num6] = skinnedMeshRenderer.bones[num6].gameObject.name;
					num = -727282477;
					continue;
				case 13u:
					num = ((int)num2 * -403885186) ^ -477916310;
					continue;
				case 25u:
					list2 = new List<Material>();
					num = ((int)num2 * -616258863) ^ 0x68AE58A2;
					continue;
				case 33u:
					num = (int)(num2 * 1099808655) ^ -1853548583;
					continue;
				case 20u:
					rendererInput = inputRenderers[num5];
					num = -692623551;
					continue;
				case 15u:
					num6++;
					num = ((int)num2 * -964796496) ^ 0x167304E6;
					continue;
				case 22u:
					throw new InvalidOperationException(string.Format("No renderer defined in {0} at index {1}", "inputRenderer", num5));
				case 39u:
					meshData = HhMNtTiFDxGKXBYNXcFjJhmVpiCQ(sharedMesh, renderer.transform, rootTransform, animationRoot, renderer.sharedMaterials, array2, list2, dictionary);
					num = -1202973346;
					continue;
				case 5u:
					uiLoBudeNDfoSFBdNYwJiCiVCOLP(array, list, dictionary, calculateNewBinds, rootTransform, componentsInChildren);
					WdAOmcoAjbzWEshtxlWMLRAvyGwE(array, componentsInChildren);
					num = (int)(num2 * 2131169526) ^ -1112788533;
					continue;
				case 26u:
					list.Add(num5);
					meshData.OverrideBoneName = rendererInput.OverrideBone.name;
					num = (int)(num2 * 2005118687) ^ -1722099408;
					continue;
				case 7u:
				{
					int num13;
					int num14;
					if (!(animationRoot == null))
					{
						num13 = -308064717;
						num14 = num13;
					}
					else
					{
						num13 = -868301728;
						num14 = num13;
					}
					num = num13 ^ (int)(num2 * 2057161558);
					continue;
				}
				case 10u:
					num5 = 0;
					num = (int)((num2 * 1137681007) ^ 0x1401E53A);
					continue;
				case 0u:
					throw new ArgumentNullException("rootTransform");
				case 29u:
				{
					int num9;
					int num10;
					if (!flag)
					{
						num9 = 354239965;
						num10 = num9;
					}
					else
					{
						num9 = 2110615220;
						num10 = num9;
					}
					num = num9 ^ ((int)num2 * -1274687709);
					continue;
				}
				case 27u:
				{
					int num7;
					int num8;
					if (!(rendererInput.Renderer == null))
					{
						num7 = 987861420;
						num8 = num7;
					}
					else
					{
						num7 = 1612305231;
						num8 = num7;
					}
					num = num7 ^ (int)(num2 * 1774782219);
					continue;
				}
				case 21u:
					array2 = new string[skinnedMeshRenderer.bones.Length];
					num = ((int)num2 * -1103249818) ^ -1649813917;
					continue;
				case 3u:
					goto IL_04f2;
				case 16u:
					num6 = 0;
					num = (int)((num2 * 184295086) ^ 0x4D7A2014);
					continue;
				case 2u:
					array2 = null;
					num = (int)(num2 * 1126846976) ^ -1238325393;
					continue;
				case 4u:
					goto IL_053b;
				case 30u:
				{
					int num3;
					int num4;
					if (animationRoot != null)
					{
						num3 = 1995534249;
						num4 = num3;
					}
					else
					{
						num3 = 1581401427;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 1736983616);
					continue;
				}
				default:
					return array;
				}
				break;
				IL_053b:
				int num15;
				if (num6 >= array2.Length)
				{
					num = -87279627;
					num15 = num;
				}
				else
				{
					num = -1015760590;
					num15 = num;
				}
				continue;
				IL_00c5:
				int num16;
				if (!(rendererInput.OverrideBone != null))
				{
					num = -784836146;
					num16 = num;
				}
				else
				{
					num = -1139019029;
					num16 = num;
				}
				continue;
				IL_0146:
				MeshFilter component = renderer.transform.GetComponent<MeshFilter>();
				if (component == null)
				{
					throw new InvalidOperationException("Unable to find MeshFilter component for MeshRenderer " + renderer.name + ".");
				}
				sharedMesh = component.sharedMesh;
				num = -2053067486;
				continue;
				IL_04f2:
				int num17;
				if (!rendererInput.ReplaceVertColor)
				{
					num = -462388550;
					num17 = num;
				}
				else
				{
					num = -74583150;
					num17 = num;
				}
				continue;
				IL_00e8:
				int num18;
				if (num5 < inputRenderers.Length)
				{
					num = -1918503288;
					num18 = num;
				}
				else
				{
					num = -123745908;
					num18 = num;
				}
			}
			goto IL_0006;
			IL_024c:
			int num19;
			if (!(rootTransform == null))
			{
				num = -244617813;
				num19 = num;
			}
			else
			{
				num = -1357989751;
				num19 = num;
			}
			goto IL_000b;
		}

		public static GameObject ConvertFromMeshData(MeshData inputData, Material[] materialMap, Transform[] boneTransforms = null, string objectName = "")
		{
			bool num = inputData.IsSkinned();
			if (num && boneTransforms == null)
			{
				throw new ArgumentNullException("boneTransforms");
			}
			GameObject gameObject = new GameObject();
			gameObject.name = (string.IsNullOrEmpty(objectName) ? gameObject.name : objectName);
			string name = gameObject.name + "_Mesh";
			GameObject gameObject2 = new GameObject();
			gameObject2.transform.SetParent(gameObject.transform);
			gameObject2.name = name;
			Mesh mesh = XPFgNIehttnayrduYnVyIhysGTtIA(inputData);
			mesh.name = name;
			if (num)
			{
				goto IL_0074;
			}
			goto IL_00c6;
			IL_0079:
			int num3;
			MeshRenderer meshRenderer = default(MeshRenderer);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num3 ^ 0x74DD1502)) % 5)
				{
				case 0u:
					break;
				case 4u:
				{
					MeshFilter meshFilter = gameObject2.AddComponent<MeshFilter>();
					meshRenderer.sharedMaterials = materialMap;
					meshFilter.sharedMesh = mesh;
					num3 = (int)((num2 * 1275802827) ^ 0x1A27D94E);
					continue;
				}
				case 1u:
					goto IL_00c6;
				case 2u:
				{
					SkinnedMeshRenderer skinnedMeshRenderer = gameObject2.AddComponent<SkinnedMeshRenderer>();
					skinnedMeshRenderer.sharedMesh = mesh;
					skinnedMeshRenderer.sharedMaterials = materialMap;
					skinnedMeshRenderer.quality = inputData.SkinningQuality;
					skinnedMeshRenderer.bones = ObhArVxBbMDqwcSQmzEZyZTJOmzE(boneTransforms, inputData.BindPoses, out var rootBone);
					skinnedMeshRenderer.rootBone = rootBone;
					num3 = ((int)num2 * -535546888) ^ -1782554457;
					continue;
				}
				default:
					return gameObject;
				}
				break;
			}
			goto IL_0074;
			IL_00c6:
			meshRenderer = gameObject2.AddComponent<MeshRenderer>();
			num3 = 1257286625;
			goto IL_0079;
			IL_0074:
			num3 = 1581035009;
			goto IL_0079;
		}

		private static MeshData HhMNtTiFDxGKXBYNXcFjJhmVpiCQ(Mesh P_0, Transform P_1, Transform P_2, Transform P_3, Material[] P_4, string[] P_5, List<Material> P_6, Dictionary<string, BindposeData> P_7)
		{
			MeshData result = default(MeshData);
			Dictionary<int, List<int>> dictionary = default(Dictionary<int, List<int>>);
			int num62 = default(int);
			int[] triangles = default(int[]);
			int num5 = default(int);
			Material item = default(Material);
			int num58 = default(int);
			int vertexCount = default(int);
			int num59 = default(int);
			int subMeshCount = default(int);
			Vector3 vector3 = default(Vector3);
			Matrix4x4 matrix4x = default(Matrix4x4);
			Vector3[] vertices = default(Vector3[]);
			int num61 = default(int);
			List<BlendshapeFrameData> list = default(List<BlendshapeFrameData>);
			Vector2[] uv4 = default(Vector2[]);
			int num21 = default(int);
			Matrix4x4[] bindposes = default(Matrix4x4[]);
			int num18 = default(int);
			BindposeData bindposeData = default(BindposeData);
			Vector2[] uv = default(Vector2[]);
			int num30 = default(int);
			int blendShapeCount = default(int);
			BoneWeight[] boneWeights = default(BoneWeight[]);
			BoneWeight source = default(BoneWeight);
			int num9 = default(int);
			int num13 = default(int);
			Vector4 vector = default(Vector4);
			Vector4[] tangents = default(Vector4[]);
			int num44 = default(int);
			Vector3[] array = default(Vector3[]);
			Vector3[] array2 = default(Vector3[]);
			Vector3[] array3 = default(Vector3[]);
			string blendShapeName = default(string);
			float blendShapeFrameWeight = default(float);
			float w = default(float);
			Vector4 vector2 = default(Vector4);
			Vector3[] normals = default(Vector3[]);
			Vector2[] uv3 = default(Vector2[]);
			Vector2[] uv2 = default(Vector2[]);
			int blendShapeFrameCount = default(int);
			while (true)
			{
				int num = 1647301516;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x374E75AA)) % 27)
					{
					case 0u:
						break;
					case 12u:
						dictionary[num62].AddRange(triangles);
						num = 893363450;
						continue;
					case 9u:
						num5 = 0;
						num = (int)((num2 * 88079206) ^ 0x602A4F1D);
						continue;
					case 14u:
						num = ((int)num2 * -62217525) ^ -1301723718;
						continue;
					case 3u:
						num62 = P_6.Count;
						P_6.Add(item);
						num = ((int)num2 * -1907666586) ^ 0x7CF48366;
						continue;
					case 10u:
					{
						int num69;
						if (num58 < vertexCount)
						{
							num = 614300470;
							num69 = num;
						}
						else
						{
							num = 933238591;
							num69 = num;
						}
						continue;
					}
					case 17u:
						num59++;
						num = 1023100014;
						continue;
					case 23u:
					{
						int num67;
						if (num59 >= subMeshCount)
						{
							num = 1772952179;
							num67 = num;
						}
						else
						{
							num = 690829770;
							num67 = num;
						}
						continue;
					}
					case 19u:
					{
						triangles = P_0.GetTriangles(num59);
						Vector3 lossyScale = P_1.lossyScale;
						int num60;
						if (Mathf.Sign(lossyScale.x * lossyScale.y * lossyScale.z) >= 0f)
						{
							num = 1198300316;
							num60 = num;
						}
						else
						{
							num = 115298093;
							num60 = num;
						}
						continue;
					}
					case 7u:
						result.Vertices[num58] = vector3 + (Vector3)(matrix4x * vertices[num58]);
						num58++;
						num = 1355425029;
						continue;
					case 13u:
					{
						int num68;
						if (!dictionary.ContainsKey(num62))
						{
							num = 2132598063;
							num68 = num;
						}
						else
						{
							num = 207587815;
							num68 = num;
						}
						continue;
					}
					case 11u:
						subMeshCount = P_0.subMeshCount;
						dictionary = new Dictionary<int, List<int>>();
						num59 = 0;
						num = (int)(num2 * 25207514) ^ -1206290292;
						continue;
					case 26u:
						vertexCount = P_0.vertexCount;
						num = ((int)num2 * -1649476194) ^ 0x2AAA987E;
						continue;
					case 16u:
					{
						Matrix4x4 worldToLocalMatrix = P_2.worldToLocalMatrix;
						matrix4x = worldToLocalMatrix * P_1.localToWorldMatrix;
						vector3 = worldToLocalMatrix * (P_1.position - P_2.position);
						num = ((int)num2 * -475868211) ^ -991415332;
						continue;
					}
					case 25u:
						item = P_4[num59];
						num = 1251961284;
						continue;
					case 4u:
						result.Submeshes = new SubmeshData[dictionary.Count];
						num = ((int)num2 * -1575579189) ^ -1392895928;
						continue;
					case 1u:
					{
						num62 = P_6.IndexOf(item);
						int num65;
						int num66;
						if (num62 != -1)
						{
							num65 = 854027614;
							num66 = num65;
						}
						else
						{
							num65 = 275999546;
							num66 = num65;
						}
						num = num65 ^ ((int)num2 * -1485701204);
						continue;
					}
					case 6u:
					{
						int num64;
						if (num61 < triangles.Length)
						{
							num = 1787197657;
							num64 = num;
						}
						else
						{
							num = 1198300316;
							num64 = num;
						}
						continue;
					}
					case 5u:
						vertices = P_0.vertices;
						num = ((int)num2 * -1998147210) ^ 0x4AD959E1;
						continue;
					case 21u:
						num = (int)(num2 * 1320303460) ^ -1503423927;
						continue;
					case 22u:
					{
						int num63 = triangles[num61];
						triangles[num61] = triangles[num61 + 2];
						triangles[num61 + 2] = num63;
						num = 1148384323;
						continue;
					}
					case 8u:
						num58 = 0;
						num = ((int)num2 * -1606453931) ^ 0xE1FC52C;
						continue;
					case 18u:
						num61 = 0;
						num = ((int)num2 * -89890841) ^ -1206966556;
						continue;
					case 15u:
						result.Vertices = new Vector3[vertexCount];
						num = (int)(num2 * 1379745220) ^ -632858195;
						continue;
					case 20u:
						dictionary.Add(num62, new List<int>(triangles));
						num = ((int)num2 * -719494119) ^ 0x2CC8CC07;
						continue;
					case 24u:
						num61 += 3;
						num = ((int)num2 * -1183527196) ^ -2008608469;
						continue;
					default:
					{
						using (Dictionary<int, List<int>>.Enumerator enumerator = dictionary.GetEnumerator())
						{
							while (true)
							{
								IL_03f6:
								int num3;
								int num4;
								if (!enumerator.MoveNext())
								{
									num3 = 985234055;
									num4 = num3;
								}
								else
								{
									num3 = 356419485;
									num4 = num3;
								}
								while (true)
								{
									switch ((num2 = (uint)(num3 ^ 0x374E75AA)) % 5)
									{
									case 2u:
										num3 = 356419485;
										continue;
									default:
										goto end_IL_03d0;
									case 1u:
										break;
									case 4u:
										num5++;
										num3 = (int)((num2 * 690411195) ^ 0x2564FB10);
										continue;
									case 3u:
									{
										KeyValuePair<int, List<int>> current = enumerator.Current;
										result.Submeshes[num5] = new SubmeshData(current.Value.ToArray(), current.Key);
										num3 = 468309250;
										continue;
									}
									case 0u:
										goto end_IL_03d0;
									}
									goto IL_03f6;
									continue;
									end_IL_03d0:
									break;
								}
								break;
							}
						}
						Color[] colors = P_0.colors;
						while (true)
						{
							int num6 = 1315979340;
							while (true)
							{
								switch ((num2 = (uint)(num6 ^ 0x374E75AA)) % 76)
								{
								case 36u:
									break;
								case 52u:
									list = new List<BlendshapeFrameData>();
									num6 = (int)((num2 * 810405497) ^ 0x250DC478);
									continue;
								case 27u:
									uv4 = P_0.uv4;
									num6 = 1916298456;
									continue;
								case 22u:
									result.BoneWeights = new BoneWeightData[vertexCount];
									num6 = 2099834564;
									continue;
								case 33u:
								{
									int num43;
									if (num21 >= bindposes.Length)
									{
										num6 = 1001018884;
										num43 = num6;
									}
									else
									{
										num6 = 919153777;
										num43 = num6;
									}
									continue;
								}
								case 26u:
								{
									int num57;
									if (num18 >= vertexCount)
									{
										num6 = 249772634;
										num57 = num6;
									}
									else
									{
										num6 = 1470906553;
										num57 = num6;
									}
									continue;
								}
								case 20u:
								{
									int num28;
									int num29;
									if (!P_7.ContainsKey(bindposeData.BoneName))
									{
										num28 = 682984338;
										num29 = num28;
									}
									else
									{
										num28 = 1677621734;
										num29 = num28;
									}
									num6 = num28 ^ ((int)num2 * -648103802);
									continue;
								}
								case 32u:
								{
									int num55;
									int num56;
									if (bindposes.Length != P_5.Length)
									{
										num55 = -1030435036;
										num56 = num55;
									}
									else
									{
										num55 = -21403607;
										num56 = num55;
									}
									num6 = num55 ^ (int)(num2 * 654443576);
									continue;
								}
								case 56u:
									uv = P_0.uv2;
									num6 = 2127259776;
									continue;
								case 65u:
									Array.Copy(uv4, result.UV3, vertexCount);
									num6 = ((int)num2 * -402549945) ^ 0x205D0BE4;
									continue;
								case 60u:
									result.Normals = new Vector3[vertexCount];
									num6 = (int)(num2 * 1417481189) ^ -1269348459;
									continue;
								case 3u:
								{
									int num42;
									if (num30 >= blendShapeCount)
									{
										num6 = 707059241;
										num42 = num6;
									}
									else
									{
										num6 = 1668219443;
										num42 = num6;
									}
									continue;
								}
								case 38u:
									result.UV0 = new Vector2[vertexCount];
									num6 = (int)((num2 * 70381110) ^ 0x3E2CB771);
									continue;
								case 35u:
									result.Tangents = new Vector4[vertexCount];
									num6 = ((int)num2 * -986168897) ^ 0x7A0A23C;
									continue;
								case 30u:
									result.UV2 = new Vector2[vertexCount];
									num6 = ((int)num2 * -1880544191) ^ -1850189347;
									continue;
								case 39u:
								{
									int num22;
									int num23;
									if (colors.Length == 0)
									{
										num22 = 1982603496;
										num23 = num22;
									}
									else
									{
										num22 = 1331551022;
										num23 = num22;
									}
									num6 = num22 ^ (int)(num2 * 767534666);
									continue;
								}
								case 13u:
									bindposes = P_0.bindposes;
									boneWeights = P_0.boneWeights;
									num6 = 807832711;
									continue;
								case 57u:
									result.BindPoses[num21] = bindposeData;
									num6 = (int)((num2 * 1822723693) ^ 0x37F20233);
									continue;
								case 66u:
									result.BoneWeights[num18] = new BoneWeightData(source);
									num18++;
									num6 = (int)((num2 * 248182151) ^ 0x114273FE);
									continue;
								case 67u:
								{
									int num11;
									int num12;
									if (boneWeights != null)
									{
										num11 = 180397276;
										num12 = num11;
									}
									else
									{
										num11 = 155353678;
										num12 = num11;
									}
									num6 = num11 ^ ((int)num2 * -1992041268);
									continue;
								}
								case 71u:
									num9 = 0;
									num6 = ((int)num2 * -1710659516) ^ 0x1EBD595F;
									continue;
								case 47u:
									result.BlendShapes = list.ToArray();
									num6 = (int)(num2 * 2138232957) ^ -1100470348;
									continue;
								case 24u:
								{
									int num50;
									if (num13 < vertexCount)
									{
										num6 = 1852350136;
										num50 = num6;
									}
									else
									{
										num6 = 1399961392;
										num50 = num6;
									}
									continue;
								}
								case 15u:
									num13 = 0;
									num6 = ((int)num2 * -1457712901) ^ -224739841;
									continue;
								case 58u:
									vector = tangents[num13];
									num6 = 414300678;
									continue;
								case 75u:
									source = boneWeights[num18];
									num6 = 973736932;
									continue;
								case 11u:
								{
									string text = P_5[num21];
									bindposeData = new BindposeData(text, bindposes[num21], text == P_3.name);
									num6 = 102243223;
									continue;
								}
								case 17u:
									num6 = ((int)num2 * -310860003) ^ -260719816;
									continue;
								case 73u:
									P_0.GetBlendShapeFrameVertices(num30, num44, array, array2, array3);
									list.Add(new BlendshapeFrameData(blendShapeName, num44, blendShapeFrameWeight, array, array2, array3));
									num44++;
									num6 = ((int)num2 * -1709424298) ^ -1076323756;
									continue;
								case 51u:
									result.BindPoses = new BindposeData[bindposes.Length];
									num6 = ((int)num2 * -1615711322) ^ 0x39EEC0A2;
									continue;
								case 72u:
								{
									int num38;
									int num39;
									if (uv4.Length != 0)
									{
										num38 = 633992168;
										num39 = num38;
									}
									else
									{
										num38 = 1656585391;
										num39 = num38;
									}
									num6 = num38 ^ ((int)num2 * -2094542602);
									continue;
								}
								case 10u:
								{
									int num34;
									int num35;
									if (uv4 == null)
									{
										num34 = 355311849;
										num35 = num34;
									}
									else
									{
										num34 = 1896927088;
										num35 = num34;
									}
									num6 = num34 ^ (int)(num2 * 566311175);
									continue;
								}
								case 48u:
									w = vector.w;
									vector2 = matrix4x.MultiplyVector(vector);
									num6 = (int)(num2 * 1556451435) ^ -931752623;
									continue;
								case 70u:
								{
									int num26;
									int num27;
									if (!(P_3 != null))
									{
										num26 = 530819908;
										num27 = num26;
									}
									else
									{
										num26 = 1411314696;
										num27 = num26;
									}
									num6 = num26 ^ (int)(num2 * 453021);
									continue;
								}
								case 74u:
									num30 = 0;
									num6 = ((int)num2 * -318246122) ^ -1687491293;
									continue;
								case 69u:
									array = new Vector3[vertexCount];
									array2 = new Vector3[vertexCount];
									array3 = new Vector3[vertexCount];
									num6 = (int)(num2 * 1124387752) ^ -1418993029;
									continue;
								case 9u:
								{
									int num16;
									int num17;
									if (normals != null)
									{
										num16 = -1352988006;
										num17 = num16;
									}
									else
									{
										num16 = -2083601444;
										num17 = num16;
									}
									num6 = num16 ^ ((int)num2 * -1317336655);
									continue;
								}
								case 12u:
								{
									int num10;
									if (num9 < vertexCount)
									{
										num6 = 1257016434;
										num10 = num6;
									}
									else
									{
										num6 = 409107165;
										num10 = num6;
									}
									continue;
								}
								case 53u:
								{
									int num53;
									int num54;
									if (tangents.Length == 0)
									{
										num53 = -597947687;
										num54 = num53;
									}
									else
									{
										num53 = -1387231384;
										num54 = num53;
									}
									num6 = num53 ^ ((int)num2 * -1938564503);
									continue;
								}
								case 8u:
								{
									int num51;
									int num52;
									if (uv.Length == 0)
									{
										num51 = 802827483;
										num52 = num51;
									}
									else
									{
										num51 = 304173293;
										num52 = num51;
									}
									num6 = num51 ^ ((int)num2 * -1141899338);
									continue;
								}
								case 34u:
									result.VertexColors = new Color[vertexCount];
									Array.Copy(colors, result.VertexColors, vertexCount);
									num6 = ((int)num2 * -139184397) ^ -784361000;
									continue;
								case 25u:
									num30++;
									num6 = (int)((num2 * 1861192597) ^ 0x2C21C13C);
									continue;
								case 55u:
									result.UV1 = new Vector2[vertexCount];
									Array.Copy(uv, result.UV1, vertexCount);
									num6 = ((int)num2 * -643195783) ^ 0x7500C584;
									continue;
								case 46u:
									uv3 = P_0.uv;
									num6 = 955025861;
									continue;
								case 21u:
								{
									uv2 = P_0.uv3;
									int num49;
									if (uv2 == null)
									{
										num6 = 1120504685;
										num49 = num6;
									}
									else
									{
										num6 = 686093770;
										num49 = num6;
									}
									continue;
								}
								case 37u:
									Array.Copy(uv2, result.UV2, vertexCount);
									num6 = (int)(num2 * 1372446549) ^ -917007424;
									continue;
								case 1u:
									blendShapeFrameCount = P_0.GetBlendShapeFrameCount(num30);
									num6 = 1827708492;
									continue;
								case 23u:
									num6 = ((int)num2 * -1376426388) ^ -139220809;
									continue;
								case 31u:
									vector2.w = w;
									result.Tangents[num13] = vector2;
									num6 = (int)(num2 * 1955210147) ^ -1985997318;
									continue;
								case 49u:
									blendShapeFrameWeight = P_0.GetBlendShapeFrameWeight(num30, num44);
									num6 = 673863987;
									continue;
								case 6u:
									num18 = 0;
									num6 = ((int)num2 * -452148631) ^ -1422413690;
									continue;
								case 0u:
									result.Normals[num9] = matrix4x.MultiplyVector(normals[num9]);
									num6 = 1535427285;
									continue;
								case 14u:
									num21 = 0;
									num6 = (int)(num2 * 1678824300) ^ -408293479;
									continue;
								case 50u:
								{
									int num47;
									int num48;
									if (P_5 == null)
									{
										num47 = 1005342184;
										num48 = num47;
									}
									else
									{
										num47 = 1625444398;
										num48 = num47;
									}
									num6 = num47 ^ ((int)num2 * -1085185111);
									continue;
								}
								case 19u:
									Array.Copy(uv3, result.UV0, vertexCount);
									num6 = (int)((num2 * 159865412) ^ 0x7083C286);
									continue;
								case 68u:
								{
									blendShapeCount = P_0.blendShapeCount;
									int num46;
									if (blendShapeCount > 0)
									{
										num6 = 1032310246;
										num46 = num6;
									}
									else
									{
										num6 = 88760643;
										num46 = num6;
									}
									continue;
								}
								case 64u:
								{
									int num45;
									if (num44 < blendShapeFrameCount)
									{
										num6 = 1914404659;
										num45 = num6;
									}
									else
									{
										num6 = 1078510643;
										num45 = num6;
									}
									continue;
								}
								case 4u:
									num44 = 0;
									num6 = (int)(num2 * 839462899) ^ -946965942;
									continue;
								case 41u:
								{
									int num40;
									int num41;
									if (normals.Length != 0)
									{
										num40 = 903962738;
										num41 = num40;
									}
									else
									{
										num40 = 1542191417;
										num41 = num40;
									}
									num6 = num40 ^ ((int)num2 * -1046891228);
									continue;
								}
								case 59u:
								{
									int num36;
									int num37;
									if (uv3.Length != 0)
									{
										num36 = -658240625;
										num37 = num36;
									}
									else
									{
										num36 = -1666601659;
										num37 = num36;
									}
									num6 = num36 ^ ((int)num2 * -1664480331);
									continue;
								}
								case 18u:
									blendShapeName = P_0.GetBlendShapeName(num30);
									num6 = ((int)num2 * -387877688) ^ -843620366;
									continue;
								case 40u:
									P_7.Add(bindposeData.BoneName, bindposeData);
									num6 = (int)(num2 * 197303972) ^ -954606778;
									continue;
								case 63u:
									num9++;
									num6 = ((int)num2 * -405424077) ^ -1498833757;
									continue;
								case 43u:
								{
									tangents = P_0.tangents;
									int num33;
									if (tangents != null)
									{
										num6 = 2077565483;
										num33 = num6;
									}
									else
									{
										num6 = 1399961392;
										num33 = num6;
									}
									continue;
								}
								case 7u:
								{
									int num31;
									int num32;
									if (uv3 == null)
									{
										num31 = 18661431;
										num32 = num31;
									}
									else
									{
										num31 = 1551757252;
										num32 = num31;
									}
									num6 = num31 ^ (int)(num2 * 556992867);
									continue;
								}
								case 16u:
								{
									int num24;
									int num25;
									if (uv2.Length == 0)
									{
										num24 = -1282224659;
										num25 = num24;
									}
									else
									{
										num24 = -1854417116;
										num25 = num24;
									}
									num6 = num24 ^ (int)(num2 * 1907734716);
									continue;
								}
								case 44u:
									num21++;
									num6 = 316018963;
									continue;
								case 2u:
								{
									int num19;
									int num20;
									if (colors != null)
									{
										num19 = 1622706731;
										num20 = num19;
									}
									else
									{
										num19 = 955027524;
										num20 = num19;
									}
									num6 = num19 ^ ((int)num2 * -1530363533);
									continue;
								}
								case 54u:
								{
									int num14;
									int num15;
									if (uv == null)
									{
										num14 = 304087675;
										num15 = num14;
									}
									else
									{
										num14 = 1587501598;
										num15 = num14;
									}
									num6 = num14 ^ ((int)num2 * -1702485900);
									continue;
								}
								case 29u:
									num13++;
									num6 = ((int)num2 * -618872290) ^ -2030622392;
									continue;
								case 62u:
									num6 = ((int)num2 * -976878866) ^ -528129376;
									continue;
								case 28u:
									normals = P_0.normals;
									num6 = 652566011;
									continue;
								case 45u:
									num6 = (int)(num2 * 776029214) ^ -726947392;
									continue;
								case 42u:
									result.UV3 = new Vector2[vertexCount];
									num6 = (int)((num2 * 1142654738) ^ 0x12F17DAB);
									continue;
								case 5u:
								{
									int num7;
									int num8;
									if (bindposes != null)
									{
										num7 = 1584778604;
										num8 = num7;
									}
									else
									{
										num7 = 398305147;
										num8 = num7;
									}
									num6 = num7 ^ ((int)num2 * -559823803);
									continue;
								}
								default:
									return result;
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

		private static Mesh XPFgNIehttnayrduYnVyIhysGTtIA(MeshData P_0)
		{
			Mesh mesh = new Mesh();
			int num = P_0.Submeshes.Length;
			mesh.vertices = P_0.Vertices;
			BlendshapeFrameData blendshapeFrameData = default(BlendshapeFrameData);
			BlendshapeFrameData[] blendShapes = default(BlendshapeFrameData[]);
			int num6 = default(int);
			BoneWeight[] array2 = default(BoneWeight[]);
			Matrix4x4[] array = default(Matrix4x4[]);
			int num9 = default(int);
			int num13 = default(int);
			int num4 = default(int);
			while (true)
			{
				int num2 = -1923449507;
				while (true)
				{
					uint num3;
					switch ((num3 = (uint)(num2 ^ -1605820537)) % 42)
					{
					case 41u:
						break;
					case 27u:
						mesh.uv2 = P_0.UV1;
						num2 = (int)(num3 * 847845103) ^ -1448924020;
						continue;
					case 4u:
						mesh.uv3 = P_0.UV2;
						num2 = (int)(num3 * 479260082) ^ -1575012959;
						continue;
					case 17u:
						mesh.AddBlendShapeFrame(blendshapeFrameData.Name, blendshapeFrameData.Weight, blendshapeFrameData.VertexDelta, blendshapeFrameData.NormalDelta, blendshapeFrameData.TangentDelta);
						num2 = ((int)num3 * -1778541221) ^ 0x6824C12E;
						continue;
					case 18u:
						blendShapes = P_0.BlendShapes;
						num2 = ((int)num3 * -1923650471) ^ 0x7F426BD2;
						continue;
					case 6u:
						num6++;
						num2 = -1703823078;
						continue;
					case 5u:
						array2 = new BoneWeight[P_0.VertexCount()];
						num2 = (int)(num3 * 810060165) ^ -57348784;
						continue;
					case 32u:
						array = new Matrix4x4[P_0.BindPoses.Length];
						num2 = ((int)num3 * -24267707) ^ 0x4C4E7A4A;
						continue;
					case 19u:
						mesh.uv4 = P_0.UV3;
						num2 = ((int)num3 * -59529310) ^ 0x12A9BD86;
						continue;
					case 37u:
					{
						int num23;
						if (P_0.UV1 != null)
						{
							num2 = -88327130;
							num23 = num2;
						}
						else
						{
							num2 = -692701757;
							num23 = num2;
						}
						continue;
					}
					case 40u:
						array2[num9] = P_0.BoneWeights[num9].ToBoneWeight();
						num9++;
						num2 = -786522120;
						continue;
					case 38u:
					{
						int num20;
						if (P_0.UV3 == null)
						{
							num2 = -85425012;
							num20 = num2;
						}
						else
						{
							num2 = -701074958;
							num20 = num2;
						}
						continue;
					}
					case 14u:
						mesh.subMeshCount = num;
						num13 = 0;
						num2 = (int)(num3 * 646696945) ^ -1807174120;
						continue;
					case 26u:
						array[num4] = P_0.BindPoses[num4].BindPose;
						num4++;
						num2 = -1939875526;
						continue;
					case 33u:
					{
						int num17;
						if (num4 < array.Length)
						{
							num2 = -255124089;
							num17 = num2;
						}
						else
						{
							num2 = -1969929145;
							num17 = num2;
						}
						continue;
					}
					case 2u:
						mesh.bindposes = array;
						num2 = ((int)num3 * -1055262980) ^ 0x2C23D342;
						continue;
					case 34u:
						mesh.normals = P_0.Normals;
						num2 = (int)(num3 * 631518459) ^ -2050760888;
						continue;
					case 30u:
						mesh.SetTriangles(P_0.GetTriangles(num13), num13);
						num2 = -781448043;
						continue;
					case 29u:
					{
						int num12;
						if (P_0.Normals == null)
						{
							num2 = -1983604884;
							num12 = num2;
						}
						else
						{
							num2 = -816191317;
							num12 = num2;
						}
						continue;
					}
					case 10u:
					{
						int num7;
						if (P_0.UV0 != null)
						{
							num2 = -1697474475;
							num7 = num2;
						}
						else
						{
							num2 = -748525496;
							num7 = num2;
						}
						continue;
					}
					case 36u:
						blendshapeFrameData = blendShapes[num6];
						num2 = -1882453251;
						continue;
					case 7u:
						num6 = 0;
						num2 = (int)(num3 * 754860377) ^ -89887579;
						continue;
					case 24u:
					{
						int num22;
						if (P_0.UV2 == null)
						{
							num2 = -1694591371;
							num22 = num2;
						}
						else
						{
							num2 = -945517187;
							num22 = num2;
						}
						continue;
					}
					case 31u:
					{
						int num21;
						if (P_0.Tangents == null)
						{
							num2 = -310909511;
							num21 = num2;
						}
						else
						{
							num2 = -31350501;
							num21 = num2;
						}
						continue;
					}
					case 35u:
					{
						int num19;
						if (P_0.BlendShapes != null)
						{
							num2 = -2027216523;
							num19 = num2;
						}
						else
						{
							num2 = -221196728;
							num19 = num2;
						}
						continue;
					}
					case 20u:
						mesh.boneWeights = array2;
						num2 = (int)(num3 * 1149153395) ^ -821572297;
						continue;
					case 9u:
						num2 = ((int)num3 * -1089515480) ^ -799177088;
						continue;
					case 12u:
						mesh.uv = P_0.UV0;
						num2 = ((int)num3 * -375547057) ^ 0x7C676786;
						continue;
					case 23u:
					{
						int num18;
						if (num9 < array2.Length)
						{
							num2 = -1687029427;
							num18 = num2;
						}
						else
						{
							num2 = -1268986753;
							num18 = num2;
						}
						continue;
					}
					case 39u:
					{
						int num15;
						int num16;
						if (P_0.VertexColors == null)
						{
							num15 = 994093027;
							num16 = num15;
						}
						else
						{
							num15 = 395102476;
							num16 = num15;
						}
						num2 = num15 ^ ((int)num3 * -1421326815);
						continue;
					}
					case 22u:
						num13++;
						num2 = (int)((num3 * 2121904170) ^ 0x2E8835CA);
						continue;
					case 15u:
					{
						int num14;
						if (num13 >= num)
						{
							num2 = -753762398;
							num14 = num2;
						}
						else
						{
							num2 = -1745325399;
							num14 = num2;
						}
						continue;
					}
					case 13u:
						num2 = (int)((num3 * 673890275) ^ 0x657C1CD);
						continue;
					case 8u:
					{
						int num10;
						int num11;
						if (mesh.GetBlendShapeIndex(blendshapeFrameData.Name) >= 0)
						{
							num10 = -781958859;
							num11 = num10;
						}
						else
						{
							num10 = -533079410;
							num11 = num10;
						}
						num2 = num10 ^ ((int)num3 * -900223934);
						continue;
					}
					case 0u:
						mesh.colors = P_0.VertexColors;
						num2 = ((int)num3 * -172302519) ^ -873015016;
						continue;
					case 16u:
						num9 = 0;
						num2 = (int)((num3 * 580024396) ^ 0x5E28C354);
						continue;
					case 11u:
					{
						int num8;
						if (num6 >= blendShapes.Length)
						{
							num2 = -221196728;
							num8 = num2;
						}
						else
						{
							num2 = -224919849;
							num8 = num2;
						}
						continue;
					}
					case 21u:
					{
						int num5;
						if (P_0.BoneWeights != null)
						{
							num2 = -2046612352;
							num5 = num2;
						}
						else
						{
							num2 = -1963523518;
							num5 = num2;
						}
						continue;
					}
					case 1u:
						num2 = (int)(num3 * 1540761372) ^ -1949732814;
						continue;
					case 28u:
						mesh.tangents = P_0.Tangents;
						num2 = ((int)num3 * -735512380) ^ 0x544EEAC9;
						continue;
					case 25u:
						num4 = 0;
						num2 = ((int)num3 * -1434188002) ^ 0x198848C;
						continue;
					default:
						return mesh;
					}
					break;
				}
			}
		}

		private static Transform[] ObhArVxBbMDqwcSQmzEZyZTJOmzE(Transform[] P_0, BindposeData[] P_1, out Transform P_2)
		{
			P_2 = null;
			Transform[] array = new Transform[P_1.Length];
			BindposeData bindposeData = default(BindposeData);
			Transform transform = default(Transform);
			Transform[] array2 = default(Transform[]);
			int num3 = default(int);
			int num4 = default(int);
			while (true)
			{
				int num = 1599916252;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x30E1932E)) % 17)
					{
					case 16u:
						break;
					case 11u:
					{
						int num8;
						int num9;
						if (!bindposeData.IsRoot)
						{
							num8 = -2068170384;
							num9 = num8;
						}
						else
						{
							num8 = -922911843;
							num9 = num8;
						}
						num = num8 ^ (int)(num2 * 1542898169);
						continue;
					}
					case 1u:
					{
						int num7;
						if (!(transform.name == bindposeData.BoneName))
						{
							num = 1556607445;
							num7 = num;
						}
						else
						{
							num = 2134832008;
							num7 = num;
						}
						continue;
					}
					case 13u:
						num = ((int)num2 * -278505721) ^ -521933821;
						continue;
					case 7u:
						array2 = P_0;
						num = 250123497;
						continue;
					case 0u:
					{
						int num6;
						if (num3 < array2.Length)
						{
							num = 863389644;
							num6 = num;
						}
						else
						{
							num = 219540157;
							num6 = num;
						}
						continue;
					}
					case 2u:
						num3++;
						num = 154780783;
						continue;
					case 4u:
						num4 = 0;
						num = ((int)num2 * -93005623) ^ 0x22155AFF;
						continue;
					case 8u:
					{
						int num5;
						if (num4 < array.Length)
						{
							num = 193198775;
							num5 = num;
						}
						else
						{
							num = 139531906;
							num5 = num;
						}
						continue;
					}
					case 12u:
						num4++;
						num = 508658547;
						continue;
					case 3u:
						array[num4] = transform;
						num = ((int)num2 * -682896393) ^ -1360788266;
						continue;
					case 15u:
						P_2 = transform;
						num = (int)(num2 * 1129040508) ^ -1107270497;
						continue;
					case 9u:
						num = (int)((num2 * 1769971961) ^ 0x15C0F271);
						continue;
					case 14u:
						transform = array2[num3];
						bindposeData = P_1[num4];
						num = 1415847129;
						continue;
					case 5u:
						num3 = 0;
						num = (int)(num2 * 361032247) ^ -1899147679;
						continue;
					case 6u:
						num = (int)(num2 * 1138721923) ^ -111760246;
						continue;
					default:
						return array;
					}
					break;
				}
			}
		}

		private static void UrlZpOodTsQlCVPSNfopXCTBnLRu(ref MeshData P_0, Color P_1)
		{
			Color[] array = new Color[P_0.VertexCount()];
			int num = 0;
			while (true)
			{
				int num2;
				int num3;
				if (num >= array.Length)
				{
					num2 = -115076567;
					num3 = num2;
				}
				else
				{
					num2 = -522115787;
					num3 = num2;
				}
				while (true)
				{
					uint num4;
					switch ((num4 = (uint)(num2 ^ -1531898276)) % 5)
					{
					case 0u:
						num2 = -522115787;
						continue;
					default:
						return;
					case 3u:
						P_0.VertexColors = array;
						num2 = ((int)num4 * -604338639) ^ -334208532;
						continue;
					case 1u:
						break;
					case 4u:
						array[num] = P_1;
						num++;
						num2 = -1606171396;
						continue;
					case 2u:
						return;
					}
					break;
				}
			}
		}

		private static void uiLoBudeNDfoSFBdNYwJiCiVCOLP(MeshData[] P_0, List<int> P_1, Dictionary<string, BindposeData> P_2, bool P_3, Transform P_4, Transform[] P_5)
		{
			using (List<int>.Enumerator enumerator = P_1.GetEnumerator())
			{
				int current = default(int);
				MeshData meshData = default(MeshData);
				Matrix4x4 bindPose = default(Matrix4x4);
				Transform transform = default(Transform);
				int num4 = default(int);
				BoneWeightData[] array = default(BoneWeightData[]);
				int num11 = default(int);
				while (true)
				{
					int num;
					int num2;
					if (!enumerator.MoveNext())
					{
						num = -1098188133;
						num2 = num;
					}
					else
					{
						num = -1813980661;
						num2 = num;
					}
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num ^ -1486284211)) % 25)
						{
						case 21u:
							num = -1813980661;
							continue;
						default:
							return;
						case 11u:
							current = enumerator.Current;
							num = -1094554589;
							continue;
						case 2u:
							throw new InvalidOperationException("Unable to calculate bindpose for override bone \"" + meshData.OverrideBoneName + "\" because it is not a child of the provided rig root.");
						case 12u:
							num = (int)(num3 * 1256422247) ^ -919746916;
							continue;
						case 9u:
							bindPose = transform.worldToLocalMatrix * P_4.localToWorldMatrix;
							num = (int)((num3 * 1439037680) ^ 0x53A5BB27);
							continue;
						case 22u:
							meshData.BindPoses = new BindposeData[1] { P_2[meshData.OverrideBoneName] };
							num = ((int)num3 * -1455578607) ^ -2146163478;
							continue;
						case 0u:
						{
							int num9;
							int num10;
							if (!P_2.ContainsKey(meshData.OverrideBoneName))
							{
								num9 = -519614976;
								num10 = num9;
							}
							else
							{
								num9 = -1998372332;
								num10 = num9;
							}
							num = num9 ^ (int)(num3 * 1003607147);
							continue;
						}
						case 6u:
							num = (int)(num3 * 1468935284) ^ -1169030067;
							continue;
						case 8u:
						{
							int num6;
							if (num4 >= P_5.Length)
							{
								num = -200250963;
								num6 = num;
							}
							else
							{
								num = -1641047049;
								num6 = num;
							}
							continue;
						}
						case 13u:
							array[num11] = BoneWeightData.FullToFirst;
							num11++;
							num = -492531297;
							continue;
						case 24u:
							meshData = P_0[current];
							num = (int)((num3 * 161027286) ^ 0x6FE9A3C7);
							continue;
						case 23u:
						{
							int num13;
							if (meshData.BindPoses != null)
							{
								num = -695404643;
								num13 = num;
							}
							else
							{
								num = -1183575430;
								num13 = num;
							}
							continue;
						}
						case 18u:
							num4++;
							num = -728709465;
							continue;
						case 15u:
							num = ((int)num3 * -956175019) ^ 0x5060E02E;
							continue;
						case 7u:
							array = new BoneWeightData[meshData.VertexCount()];
							num11 = 0;
							num = -1686779744;
							continue;
						case 17u:
							transform = P_5[num4];
							num = -2114848719;
							continue;
						case 20u:
							meshData.BindPoses = new BindposeData[1]
							{
								new BindposeData(meshData.OverrideBoneName, bindPose, isRoot: false)
							};
							num = (int)((num3 * 1512202740) ^ 0x4350A6E2);
							continue;
						case 3u:
						{
							int num12;
							if (num11 >= array.Length)
							{
								num = -363592369;
								num12 = num;
							}
							else
							{
								num = -1049679225;
								num12 = num;
							}
							continue;
						}
						case 10u:
						{
							int num7;
							int num8;
							if (!(transform.name == meshData.OverrideBoneName))
							{
								num7 = 1990894972;
								num8 = num7;
							}
							else
							{
								num7 = 518488933;
								num8 = num7;
							}
							num = num7 ^ (int)(num3 * 1331285828);
							continue;
						}
						case 16u:
							meshData.BoneWeights = array;
							P_0[current] = meshData;
							num = ((int)num3 * -2081046963) ^ -696985403;
							continue;
						case 1u:
							break;
						case 14u:
							throw new ArgumentException("Unable to find override bone \"" + meshData.OverrideBoneName + "\" in combined bindpose information. Please ensure at least one of your input meshes contains a reference to this bone, or allow override bone bind calculation.");
						case 19u:
						{
							int num5;
							if (!P_3)
							{
								num = -1944277036;
								num5 = num;
							}
							else
							{
								num = -1270528772;
								num5 = num;
							}
							continue;
						}
						case 4u:
							num4 = 0;
							num = (int)((num3 * 1088064642) ^ 0x379A2745);
							continue;
						case 5u:
							return;
						}
						break;
					}
				}
			}
		}

		private static void WdAOmcoAjbzWEshtxlWMLRAvyGwE(MeshData[] P_0, Transform[] P_1)
		{
			List<int> list = new List<int>(P_1.Length);
			int num4 = default(int);
			int num7 = default(int);
			MeshData meshData = default(MeshData);
			int num3 = default(int);
			MeshData[] array = default(MeshData[]);
			BindposeData bindposeData = default(BindposeData);
			BindposeData[] bindPoses = default(BindposeData[]);
			while (true)
			{
				int num = 1002064249;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x1FCC0A4B)) % 18)
					{
					case 14u:
						break;
					default:
						return;
					case 10u:
						num4 = 0;
						num = ((int)num2 * -640515779) ^ -1396001246;
						continue;
					case 15u:
						num7 = 0;
						num = (int)(num2 * 566982828) ^ -462478070;
						continue;
					case 2u:
						list.Add(P_1[num4].name.GetHashCode());
						num4++;
						num = 1707377352;
						continue;
					case 16u:
					{
						int num10;
						int num11;
						if (meshData.BindPoses == null)
						{
							num10 = 1526884723;
							num11 = num10;
						}
						else
						{
							num10 = 552966226;
							num11 = num10;
						}
						num = num10 ^ ((int)num2 * -1829105606);
						continue;
					}
					case 6u:
						num3++;
						num = 1426886005;
						continue;
					case 4u:
					{
						int num6;
						if (num3 >= array.Length)
						{
							num = 1755204640;
							num6 = num;
						}
						else
						{
							num = 295342540;
							num6 = num;
						}
						continue;
					}
					case 11u:
						num7++;
						num = 801080331;
						continue;
					case 8u:
					{
						bindposeData = bindPoses[num7];
						int num9;
						if (!list.Contains(bindposeData.NameHash))
						{
							num = 1301076735;
							num9 = num;
						}
						else
						{
							num = 2005553046;
							num9 = num;
						}
						continue;
					}
					case 5u:
						array = P_0;
						num = (int)((num2 * 182897263) ^ 0x22823971);
						continue;
					case 12u:
						throw new InvalidOperationException("Could not find bone \"" + bindposeData.BoneName + "\" in the hierarchy for the provided root bone. Please ensure your submitted skinned meshes will map to the desired hierarchy, or override the bone.");
					case 0u:
					{
						int num8;
						if (num7 < bindPoses.Length)
						{
							num = 682535179;
							num8 = num;
						}
						else
						{
							num = 97382571;
							num8 = num;
						}
						continue;
					}
					case 7u:
					{
						int num5;
						if (num4 >= P_1.Length)
						{
							num = 563084946;
							num5 = num;
						}
						else
						{
							num = 1910771087;
							num5 = num;
						}
						continue;
					}
					case 3u:
						meshData = array[num3];
						num = 2138828439;
						continue;
					case 13u:
						bindPoses = meshData.BindPoses;
						num = ((int)num2 * -1393750728) ^ 0x7E6FCD8A;
						continue;
					case 1u:
						num = (int)(num2 * 1677938120) ^ -1384188125;
						continue;
					case 17u:
						num3 = 0;
						num = (int)(num2 * 588093979) ^ -1294872886;
						continue;
					case 9u:
						return;
					}
					break;
				}
			}
		}
	}
}
