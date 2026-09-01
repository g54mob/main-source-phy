using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace MTAssets.UltimateLODSystem.MeshSimplifier;

public static class MeshUtils
{
	public static readonly int UVChannelCount = 8;

	public static Mesh CreateMesh(Vector3[] vertices, int[][] indices, Vector3[] normals, Vector4[] tangents, Color[] colors, BoneWeight[] boneWeights, List<Vector2>[] uvs, Matrix4x4[] bindposes, BlendShape[] blendShapes)
	{
		//IL_0036: Expected O, but got I
		Color[] colors2 = default(Color[]);
		BoneWeight[] boneWeights2 = default(BoneWeight[]);
		List<Vector2>[] uvs2D = default(List<Vector2>[]);
		List<Vector3>[] uvs3D = default(List<Vector3>[]);
		IntPtr intPtr = default(IntPtr);
		Matrix4x4[] bindposes2 = default(Matrix4x4[]);
		BlendShape[] blendShapes2 = default(BlendShape[]);
		return CreateMesh(vertices, indices, normals, tangents, colors2, boneWeights2, uvs2D, uvs3D, (List<Vector4>[])(nint)intPtr, bindposes2, blendShapes2);
	}

	public static Mesh CreateMesh(Vector3[] vertices, int[][] indices, Vector3[] normals, Vector4[] tangents, Color[] colors, BoneWeight[] boneWeights, List<Vector4>[] uvs, Matrix4x4[] bindposes, BlendShape[] blendShapes)
	{
		//IL_0033: Expected O, but got I
		Color[] colors2 = default(Color[]);
		BoneWeight[] boneWeights2 = default(BoneWeight[]);
		List<Vector2>[] uvs2D = default(List<Vector2>[]);
		List<Vector3>[] uvs3D = default(List<Vector3>[]);
		IntPtr intPtr = default(IntPtr);
		Matrix4x4[] bindposes2 = default(Matrix4x4[]);
		return CreateMesh(vertices, indices, normals, tangents, colors2, boneWeights2, uvs2D, uvs3D, (List<Vector4>[])(nint)intPtr, bindposes2, null);
	}

	public static Mesh CreateMesh(Vector3[] vertices, int[][] indices, Vector3[] normals, Vector4[] tangents, Color[] colors, BoneWeight[] boneWeights, List<Vector2>[] uvs2D, List<Vector3>[] uvs3D, List<Vector4>[] uvs4D, Matrix4x4[] bindposes, BlendShape[] blendShapes)
	{
		//IL_0b88: Expected O, but got I4
		//IL_0ba0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba5: Expected O, but got Unknown
		//IL_006c: Expected O, but got I
		//IL_00a6: Expected O, but got I
		//IL_00bc: Expected O, but got I
		//IL_00c5: Expected O, but got I4
		//IL_00d2: Expected O, but got I8
		//IL_00db: Expected O, but got I4
		//IL_03ca: Expected O, but got I
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Expected O, but got Unknown
		//IL_0c8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c92: Expected O, but got Unknown
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Expected O, but got Unknown
		//IL_090a: Unknown result type (might be due to invalid IL or missing references)
		//IL_090f: Expected O, but got Unknown
		//IL_0925: Unknown result type (might be due to invalid IL or missing references)
		//IL_092a: Expected O, but got Unknown
		//IL_0933: Expected O, but got I4
		//IL_0e7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e82: Expected O, but got Unknown
		//IL_0e8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e90: Expected O, but got Unknown
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Expected O, but got Unknown
		//IL_0959: Expected O, but got I
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Expected O, but got Unknown
		//IL_043d: Expected I, but got O
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Expected O, but got Unknown
		//IL_054f: Expected I, but got O
		//IL_0773: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Expected O, but got Unknown
		//IL_0781: Expected O, but got I4
		//IL_078f: Expected O, but got I4
		//IL_0661: Expected I, but got O
		//IL_0476: Expected O, but got I
		//IL_047e: Expected O, but got I
		//IL_048b: Expected O, but got I4
		//IL_0588: Expected O, but got I
		//IL_0590: Expected O, but got I
		//IL_059d: Expected O, but got I4
		//IL_069a: Expected O, but got I
		//IL_06a2: Expected O, but got I
		//IL_06af: Expected O, but got I4
		//IL_0dc7: Expected O, but got I
		//IL_0dde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de3: Expected O, but got Unknown
		//IL_0dec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df1: Expected O, but got Unknown
		//IL_0e09: Expected O, but got I4
		//IL_0e1e: Expected O, but got I
		//IL_09fb: Expected O, but got I
		//IL_0a23: Expected O, but got I4
		//IL_08bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c4: Expected O, but got Unknown
		//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d2: Expected O, but got Unknown
		//IL_07f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fb: Expected O, but got Unknown
		//IL_0a70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a75: Expected O, but got Unknown
		//IL_0a7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a83: Expected O, but got Unknown
		//IL_0874: Expected O, but got I4
		//IL_0874: Expected O, but got I
		//IL_0874: Expected F4, but got I
		//IL_0874: Expected O, but got I
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0890: Expected O, but got Unknown
		//IL_08a1: Expected O, but got I
		//IL_08b1: Expected O, but got I
		if (vertices != null)
		{
			int[][] array = default(int[][]);
			if (array != null)
			{
				Mesh mesh = new Mesh();
				int[][] array2 = (int[][])array.Length;
				Vector2Int[] array3 = new Vector2Int[array.Length];
				object obj = array3 + 32;
				object obj2 = (object)array - (object)array3;
				Vector3[] array5 = default(Vector3[]);
				Vector3[] array4 = array5;
				Vector4[] array7 = default(Vector4[]);
				Vector4[] array6 = array7;
				int num = 0;
				IndexFormat indexFormat = IndexFormat.UInt16;
				int num2 = 0;
				int[][] array8 = array;
				Matrix4x4[] array9 = default(Matrix4x4[]);
				nint num3 = default(nint);
				object obj6 = default(object);
				object obj7 = default(object);
				object obj8 = default(object);
				int num20 = default(int);
				Vector3[] deltaTangents = default(Vector3[]);
				while (true)
				{
					if (num2 < array8.Length)
					{
						if (num < array8.Length)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v254 @ rsi_v6+v240 @ rbp_v6]");
							object obj3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v254 @ rsi_v6+v240 @ rbp_v6]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v450 @ rbx_v33+18]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v450 @ rbx_v33+18]");
									array6 = (Vector4[])0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v254 @ rsi_v6+v240 @ rbp_v6]");
									object obj4 = (nint)0 + (nint)32;
									array4 = (Vector3[])2147483647;
									array2 = (int[][])2147483648L;
									object obj5 = 0;
									while (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) < System.Runtime.CompilerServices.Unsafe.As<Vector4[], UIntPtr>(ref array6))
									{
										if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) < System.Runtime.CompilerServices.Unsafe.As<Vector4[], UIntPtr>(ref array6))
										{
											if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) < System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref array4))
											{
												array4 = (Vector3[])obj4;
											}
											if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) > System.Runtime.CompilerServices.Unsafe.As<int[][], UIntPtr>(ref array2))
											{
												array2 = (int[][])obj4;
											}
											obj5++;
											obj4 += 4;
											continue;
										}
										goto IL_0bea;
									}
									goto IL_0c1b;
								}
							}
							array4 = null;
							array2 = null;
							goto IL_0c1b;
						}
					}
					else
					{
						if ((object)mesh == null)
						{
							goto IL_0ca7;
						}
						mesh.indexFormat = indexFormat;
						if (array9 != null && array9.Length != 0)
						{
							mesh.bindposes = array9;
						}
						mesh.subMeshCount = array.Length;
						mesh.vertices = vertices;
						bool flag = array5 == null;
						array4 = null;
						if (!flag)
						{
							bool flag2 = array5.Length == 0;
							array4 = null;
							if (!flag2)
							{
								mesh.normals = array5;
								array4 = null;
							}
						}
						if (array7 != null && array7.Length != 0)
						{
							mesh.tangents = array7;
							array4 = null;
						}
						if (bindposes != null && bindposes.Length != 0)
						{
							mesh.colors = (Color[])(object)bindposes;
							array4 = null;
						}
						if (blendShapes != null && blendShapes.Length != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D93C50");
							array4 = null;
						}
						bool flag3 = num3 == 0;
						List<Vector3>[] array10 = (List<Vector3>[])(object)blendShapes;
						if (!flag3)
						{
							array8 = (int[][])(num3 + 32);
							num = 0;
							array10 = (List<Vector3>[])(object)blendShapes;
							int num4 = 0;
							while (true)
							{
								int num5 = num4;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ stack_38 (Il2CppMethodInfo)+18]");
								if ((nint)num5 >= (nint)0)
								{
									break;
								}
								int num6 = num;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ stack_38 (Il2CppMethodInfo)+18]");
								bool flag4 = (nint)num6 >= (nint)0;
								array2 = (int[][])(object)array10;
								if (!flag4)
								{
									if (array8 != null)
									{
										nint num7 = (nint)array8;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1341 @ rax_v86 (Il2CppClass<System.Int32[][]>)+18]");
										if ((nint)0 > (nint)0)
										{
											mesh.SetUVs(num, (List<Vector2>)num7);
											array4 = (Vector3[])num7;
											array6 = null;
											array10 = (List<Vector3>[])num;
										}
									}
									num++;
									array8 = (int[][])(array8 + 8);
									num4 = num;
									continue;
								}
								goto IL_0bea;
							}
						}
						if (obj6 != null)
						{
							array8 = (int[][])(obj6 + 32);
							num = 0;
							int num8 = 0;
							while (true)
							{
								int num9 = num8;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1075 @ stack_40+18]");
								if ((nint)num9 >= (nint)0)
								{
									break;
								}
								int num10 = num;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1075 @ stack_40+18]");
								bool flag5 = (nint)num10 >= (nint)0;
								array2 = (int[][])(object)array10;
								if (!flag5)
								{
									if (array8 != null)
									{
										nint num11 = (nint)array8;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1418 @ rax_v80 (Il2CppClass<System.Int32[][]>)+18]");
										if ((nint)0 > (nint)0)
										{
											mesh.SetUVs(num, (List<Vector3>)num11);
											array4 = (Vector3[])num11;
											array6 = null;
											array10 = (List<Vector3>[])num;
										}
									}
									num++;
									array8 = (int[][])(array8 + 8);
									num8 = num;
									continue;
								}
								goto IL_0bea;
							}
						}
						if (obj7 != null)
						{
							array8 = (int[][])(obj7 + 32);
							num = 0;
							int num12 = 0;
							while (true)
							{
								int num13 = num12;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1147 @ stack_48+18]");
								if ((nint)num13 >= (nint)0)
								{
									break;
								}
								int num14 = num;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1147 @ stack_48+18]");
								bool flag6 = (nint)num14 >= (nint)0;
								array2 = (int[][])(object)array10;
								if (!flag6)
								{
									if (array8 != null)
									{
										nint num15 = (nint)array8;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1475 @ rax_v74 (Il2CppClass<System.Int32[][]>)+18]");
										if ((nint)0 > (nint)0)
										{
											mesh.SetUVs(num, (List<Vector4>)num15);
											array4 = (Vector3[])num15;
											array6 = null;
											array10 = (List<Vector3>[])num;
										}
									}
									num++;
									array8 = (int[][])(array8 + 8);
									num12 = num;
									continue;
								}
								goto IL_0bea;
							}
						}
						if (obj8 != null)
						{
							bool flag7 = mesh != null;
							Vector3[] array11 = null;
							array7 = array6;
							UnityEngine.Object obj9 = null;
							if (!flag7)
							{
								break;
							}
							mesh.ClearBlendShapes();
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1216 @ stack_58+18]");
							bool flag8 = (nint)0 == 0;
							array4 = null;
							array10 = null;
							if (!flag8)
							{
								object obj10 = obj8 + 40;
								object obj11 = 0;
								array10 = null;
								object obj12 = 0;
								while (true)
								{
									object obj13 = obj12;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1216 @ stack_58+18]");
									bool flag9 = (nint)obj13 >= 0;
									array4 = null;
									if (flag9)
									{
										break;
									}
									object obj14 = obj11;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1216 @ stack_58+18]");
									bool flag10 = (nint)obj14 >= 0;
									array4 = null;
									array2 = (int[][])(object)array10;
									if (!flag10)
									{
										object obj15 = obj10;
										if (obj10 != null)
										{
											array8 = (int[][])(obj10 + 48);
											num = 0;
											while (true)
											{
												int num16 = num;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v498 @ rsi_v17+18]");
												if ((nint)num16 >= (nint)0)
												{
													break;
												}
												int num17 = num;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v498 @ rsi_v17+18]");
												bool flag11 = (nint)num17 >= (nint)0;
												array4 = null;
												array2 = (int[][])(object)array10;
												if (!flag11)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v581 @ r14_v10-8]");
													nint num18 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1567 @ rbx_v20 (System.Int32[][])-10]");
													nint num19 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1567 @ rbx_v20 (System.Int32[][])-8]");
													mesh.AddBlendShapeFrame((string)num18, num19, (Vector3[])0, (Vector3[])num20, deltaTangents);
													num++;
													array8 = (int[][])(array8 + 32);
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1567 @ rbx_v20 (System.Int32[][])-8]");
													array6 = (Vector4[])0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v581 @ r14_v10-8]");
													array10 = (List<Vector3>[])0;
													continue;
												}
												goto IL_0bea;
											}
										}
										obj11++;
										obj10 += 16;
										obj12 = obj11;
										continue;
									}
									goto IL_0bea;
								}
							}
						}
						if (array.Length <= 0)
						{
							goto IL_0a88;
						}
						object obj16 = array + 24;
						object obj17 = (object)array - (object)array3;
						object obj18 = array3 + 32;
						object obj19 = 0;
						array2 = (int[][])(object)array10;
						int num21 = 0;
						while (num21 < (nint)obj16)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v512 @ rdi_v14+v567 @ r13_v10]");
							int[] array12 = (int[])0;
							if (array3 != null)
							{
								if (num21 >= array3.Length)
								{
									break;
								}
								if (indexFormat == IndexFormat.UInt16)
								{
									object obj20 = obj18 >> 32;
									if ((nint)obj20 > 65535)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v512 @ rdi_v14+v567 @ r13_v10]");
										array2 = (int[][])((nint)0 + (nint)32);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v512 @ rdi_v14+v567 @ r13_v10]");
										bool flag12 = (nint)0 == 0;
										int[][] array13 = array2;
										object obj21 = 0;
										if (flag12)
										{
											goto IL_0ca7;
										}
										while ((nint)obj21 < array12.Length)
										{
											bool flag13 = (nint)obj21 >= array12.Length;
											array2 = array13;
											if (flag13)
											{
												goto end_IL_0e55;
											}
											array13 = (int[][])((object)array13 - obj18);
											obj21++;
											array13 = (int[][])(array13 + 4);
										}
									}
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v512 @ rdi_v14+v567 @ r13_v10]");
								mesh.SetTriangles((int[])0, num21, calculateBounds: false, num20);
								int num22 = num21 + 1;
								obj19++;
								obj18 += 8;
								bool flag14 = (nint)obj19 < array.Length;
								array4 = (Vector3[])num21;
								array6 = null;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v512 @ rdi_v14+v567 @ r13_v10]");
								array2 = (int[][])0;
								num21 = num22;
								if (flag14)
								{
									continue;
								}
								goto IL_0a88;
							}
							goto IL_0ca7;
							continue;
							end_IL_0e55:
							break;
						}
					}
					goto IL_0bea;
					IL_0ca7:
					throw new NullReferenceException();
					IL_0c1b:
					if (array3 != null)
					{
						if (num < array3.Length)
						{
							array2 = (int[][])((object)array2 - (object)array4);
							obj = array4;
							if ((nint)array2 > 65535)
							{
								indexFormat = IndexFormat.UInt32;
							}
							num++;
							obj += 8;
							num2 = num;
							array8 = array;
							continue;
						}
						goto IL_0bea;
					}
					goto IL_0ca7;
					IL_0a88:
					mesh.RecalculateBounds();
					return mesh;
					IL_0bea:
					throw new IndexOutOfRangeException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				ArgumentNullException ex = new ArgumentNullException("mesh");
				ex._002Ector("mesh");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				throw ex;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentNullException ex2 = new ArgumentNullException("indices");
			ex2._002Ector("indices");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex3 = new ArgumentNullException("vertices");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex3;
	}

	public static BlendShape[] GetMeshBlendShapes(Mesh mesh)
	{
		//IL_00b8: Expected O, but got I4
		//IL_00c1: Expected O, but got I4
		//IL_00ca: Expected O, but got I4
		//IL_00e4: Expected O, but got I4
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Expected O, but got Unknown
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_0164: Expected O, but got F4
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		if (mesh != null)
		{
			if ((object)mesh != null)
			{
				int vertexCount = mesh.vertexCount;
				int blendShapeCount = mesh.blendShapeCount;
				if (blendShapeCount != 0)
				{
					BlendShape[] array = new BlendShape[blendShapeCount];
					bool flag = blendShapeCount <= 0;
					BlendShape[] result = array;
					if (!flag)
					{
						object obj = 32;
						object obj2 = 0;
						object obj3 = 0;
						BlendShape[] array2 = array;
						int num = 0;
						object obj4 = 32;
						Mesh mesh2 = mesh;
						Vector3[] deltaNormals = default(Vector3[]);
						Vector3[] deltaTangents = default(Vector3[]);
						object obj7 = default(object);
						bool flag3;
						do
						{
							string blendShapeName = mesh2.GetBlendShapeName(num);
							int blendShapeFrameCount = mesh2.GetBlendShapeFrameCount(num);
							BlendShapeFrame[] array3 = new BlendShapeFrame[blendShapeFrameCount];
							if (blendShapeFrameCount > 0)
							{
								object obj5 = array3 + 32;
								int num2 = 0;
								bool flag2;
								object obj6;
								do
								{
									float blendShapeFrameWeight = mesh2.GetBlendShapeFrameWeight(num, num2);
									Vector3[] deltaVertices = new Vector3[vertexCount];
									Vector3[] array4 = new Vector3[vertexCount];
									Vector3[] array5 = new Vector3[vertexCount];
									mesh.GetBlendShapeFrameVertices(num, num2, deltaVertices, deltaNormals, deltaTangents);
									obj5 = blendShapeFrameWeight;
									num2++;
									obj5 += 32;
									flag2 = num2 < blendShapeFrameCount;
									obj6 = obj7;
									mesh2 = mesh;
								}
								while (flag2);
								obj6 = obj7;
								obj3 = obj2;
								array2 = array;
								obj4 = obj;
								mesh2 = mesh;
							}
							obj3++;
							obj4 += 16;
							num++;
							flag3 = (nint)obj3 < blendShapeCount;
							result = array2;
							obj = obj4;
							obj2 = obj3;
						}
						while (flag3);
					}
					return result;
				}
				return null;
			}
			return (BlendShape[])(object)new NullReferenceException();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex = new ArgumentNullException("mesh");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public static void ApplyMeshBlendShapes(Mesh mesh, BlendShape[] blendShapes)
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_00a4: Expected O, but got I4
		//IL_00b2: Expected O, but got I4
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Expected O, but got Unknown
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		//IL_0114: Expected O, but got I4
		//IL_0177: Expected O, but got I
		//IL_0177: Expected F4, but got I
		//IL_0177: Expected O, but got I
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_01a4: Expected O, but got I
		bool flag = mesh != null;
		UnityEngine.Object obj = null;
		if (flag)
		{
			bool flag2 = (object)mesh == null;
			obj = null;
			if (!flag2)
			{
				mesh.ClearBlendShapes();
				if (blendShapes == null || blendShapes.Length == 0)
				{
					return;
				}
				object obj2 = blendShapes + 40;
				object obj3 = 0;
				obj = null;
				object obj4 = 0;
				Vector3[] deltaNormals = default(Vector3[]);
				Vector3[] deltaTangents = default(Vector3[]);
				while ((nint)obj4 < blendShapes.Length)
				{
					if ((nint)obj3 < blendShapes.Length)
					{
						object obj5 = obj2;
						if (obj2 != null)
						{
							object obj6 = obj2 + 48;
							object obj7 = 0;
							while (true)
							{
								object obj8 = obj7;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v318 @ rdi_v7+18]");
								if ((nint)obj8 >= 0)
								{
									break;
								}
								object obj9 = obj7;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v318 @ rdi_v7+18]");
								if ((nint)obj9 < 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r14_v5-8]");
									nint num = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v313 @ rsi_v7-10]");
									nint num2 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v313 @ rsi_v7-8]");
									mesh.AddBlendShapeFrame((string)num, num2, (Vector3[])0, deltaNormals, deltaTangents);
									obj7++;
									obj6 += 32;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r14_v5-8]");
									obj = (UnityEngine.Object)0;
									continue;
								}
								goto IL_01d2;
							}
						}
						obj3++;
						obj2 += 16;
						obj4 = obj3;
						continue;
					}
					goto IL_01d2;
					IL_01d2:
					throw new IndexOutOfRangeException();
				}
				return;
			}
			throw new NullReferenceException();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex = new ArgumentNullException("mesh");
		ex._002Ector("mesh");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public static IList<Vector4>[] GetMeshUVs(Mesh mesh)
	{
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_007e: Expected I, but got O
		//IL_008e: Expected O, but got I
		//IL_00b0: Expected O, but got I4
		if (!(mesh != null))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentNullException ex = new ArgumentNullException("mesh");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
		IList<Vector4>[] array = new IList<Vector4>[UVChannelCount];
		object obj = array + 32;
		int num = 0;
		object obj3 = default(object);
		while (true)
		{
			if (num < UVChannelCount)
			{
				IList<Vector4> meshUVs = GetMeshUVs(mesh, num);
				if (array != null)
				{
					if (meshUVs != null)
					{
						nint num2 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v324 @ rdx_v16 (Il2CppClass<System.Collections.Generic.IList`1<UnityEngine.Vector4>[]>)+40]");
						object obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						bool flag = obj3 == null;
						object obj4 = 0;
						ArgumentNullException ex2 = (ArgumentNullException)meshUVs;
						if (flag)
						{
							break;
						}
					}
					obj = meshUVs;
					num++;
					obj += 8;
					continue;
				}
				return (IList<Vector4>[])(object)new NullReferenceException();
			}
			return array;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
		object obj5 = default(object);
		throw obj5;
	}

	public static IList<Vector2> GetMeshUVs2D(Mesh mesh, int channel)
	{
		//IL_003e: Expected O, but got I4
		//IL_016e: Expected O, but got I4
		//IL_006e: Expected O, but got I4
		if (mesh != null)
		{
			bool flag = channel < 0;
			UnityEngine.Object obj = null;
			object obj2 = 0;
			if (!flag)
			{
				bool flag2 = channel >= UVChannelCount;
				obj = null;
				obj2 = 0;
				if (!flag2)
				{
					bool flag3 = (object)mesh == null;
					obj = null;
					obj2 = 0;
					if (!flag3)
					{
						int vertexCount = mesh.vertexCount;
						List<Vector2> list = new List<Vector2>(vertexCount);
						mesh.GetUVs(channel, list);
						return list;
					}
					throw new NullReferenceException();
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("channel");
			ex._002Ector("channel");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex2 = new ArgumentNullException("mesh");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex2;
	}

	public static IList<Vector3> GetMeshUVs3D(Mesh mesh, int channel)
	{
		//IL_003e: Expected O, but got I4
		//IL_016e: Expected O, but got I4
		//IL_006e: Expected O, but got I4
		if (mesh != null)
		{
			bool flag = channel < 0;
			UnityEngine.Object obj = null;
			object obj2 = 0;
			if (!flag)
			{
				bool flag2 = channel >= UVChannelCount;
				obj = null;
				obj2 = 0;
				if (!flag2)
				{
					bool flag3 = (object)mesh == null;
					obj = null;
					obj2 = 0;
					if (!flag3)
					{
						int vertexCount = mesh.vertexCount;
						List<Vector3> list = new List<Vector3>(vertexCount);
						mesh.GetUVs(channel, list);
						return list;
					}
					throw new NullReferenceException();
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("channel");
			ex._002Ector("channel");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex2 = new ArgumentNullException("mesh");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex2;
	}

	public static IList<Vector4> GetMeshUVs(Mesh mesh, int channel)
	{
		//IL_003e: Expected O, but got I4
		//IL_016e: Expected O, but got I4
		//IL_006e: Expected O, but got I4
		if (mesh != null)
		{
			bool flag = channel < 0;
			UnityEngine.Object obj = null;
			object obj2 = 0;
			if (!flag)
			{
				bool flag2 = channel >= UVChannelCount;
				obj = null;
				obj2 = 0;
				if (!flag2)
				{
					bool flag3 = (object)mesh == null;
					obj = null;
					obj2 = 0;
					if (!flag3)
					{
						int vertexCount = mesh.vertexCount;
						List<Vector4> list = new List<Vector4>(vertexCount);
						mesh.GetUVs(channel, list);
						return list;
					}
					throw new NullReferenceException();
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("channel");
			ex._002Ector("channel");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex2 = new ArgumentNullException("mesh");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex2;
	}

	public unsafe static int GetUsedUVComponents(IList<Vector4> uvs)
	{
		//IL_0046: Expected O, but got Ref
		//IL_0065: Expected I, but got O
		//IL_00f8: Expected O, but got I4
		//IL_009d: Expected O, but got I
		//IL_049b: Expected O, but got I4
		//IL_0105: Expected I, but got O
		//IL_01df: Expected O, but got I4
		//IL_01f5: Expected O, but got I
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Expected O, but got Unknown
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Expected O, but got Unknown
		//IL_013d: Expected O, but got I
		//IL_04d2: Expected O, but got I4
		//IL_0222: Expected O, but got I4
		//IL_0238: Expected O, but got I
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Expected O, but got Unknown
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Expected O, but got Unknown
		//IL_0386: Expected O, but got Ref
		//IL_03d5: Expected O, but got Ref
		if (uvs != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = default(object);
			if (obj != null)
			{
				IEnumerator<Vector4> enumerator = uvs.GetEnumerator();
				IEnumerable<Vector4> enumerable = default(IEnumerable<Vector4>);
				object obj2 = (object)(&enumerable);
				int num = 0;
				int num2 = 0;
				object obj10 = default(object);
				object obj11 = default(object);
				object obj12 = default(object);
				object obj15 = default(object);
				object obj16 = default(object);
				object obj17 = default(object);
				object obj18 = default(object);
				while (true)
				{
					object obj8;
					object obj3;
					if (enumerable != null)
					{
						nint num3 = (nint)enumerable;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v393 @ r10_v5 (Il2CppClass<System.Collections.Generic.IEnumerable`1<UnityEngine.Vector4>>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_00dd;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v393 @ r10_v5 (Il2CppClass<System.Collections.Generic.IEnumerable`1<UnityEngine.Vector4>>)+B0]");
						obj3 = 0;
						int num4 = 0;
						while (true)
						{
							object obj4 = num4 + num4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v408 @ r8_v8+v427 @ rax_v37*8]");
							if (0 == (nint)typeof(IEnumerator))
							{
								break;
							}
							num4++;
							int num5 = num4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v393 @ r10_v5 (Il2CppClass<System.Collections.Generic.IEnumerable`1<UnityEngine.Vector4>>)+12E]");
							if ((nint)num5 < (nint)0)
							{
								continue;
							}
							goto IL_00dd;
						}
						object obj5 = num4 + num4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v408 @ r8_v8+8+v483 @ rcx_v31*8]");
						object obj6 = (nint)0 << 4;
						object obj7 = obj6 + 312;
						obj8 = obj7 + num3;
						goto IL_04fc;
					}
					throw new NullReferenceException();
					IL_02de:
					int num6;
					if (num6 < 3)
					{
						goto IL_02fe;
					}
					bool flag = num6 >= 4;
					object obj9 = obj10;
					int num7 = num6;
					num = num6;
					num2 = num6;
					IEnumerable<Vector4> enumerable2 = (IEnumerable<Vector4>)(&obj11);
					if (flag)
					{
						continue;
					}
					goto IL_0394;
					IL_0394:
					bool flag2 = obj12 == null;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803CE2C3h\"");
					object obj13 = obj12;
					obj10 = obj9;
					num = num7;
					num2 = num6;
					enumerable2 = (IEnumerable<Vector4>)(&obj11);
					if (!flag2)
					{
						if (obj2 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						}
						num2 = 4;
						break;
					}
					continue;
					IL_017d:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					object obj14 = obj15;
					goto IL_0523;
					IL_0523:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v626 @ r8_v12] (should have been resolved before IL gen)");
					obj13 = obj16;
					if (num2 < 1)
					{
						bool flag3 = obj16 == null;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803CE1F9h\"");
						if (!flag3)
						{
							num = 1;
							num2 = 1;
						}
					}
					else
					{
						bool flag4 = num2 >= 2;
						num7 = num;
						num6 = num2;
						if (flag4)
						{
							goto IL_02de;
						}
					}
					bool flag5 = obj12 == null;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803CE273h\"");
					obj10 = obj12;
					num7 = num;
					num6 = num2;
					if (flag5)
					{
						goto IL_02de;
					}
					num7 = 2;
					num6 = 2;
					goto IL_02fe;
					IL_00dd:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					obj8 = obj17;
					obj3 = 0;
					goto IL_04fc;
					IL_04fc:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v488 @ rdx_v10] (should have been resolved before IL gen)");
					if (obj18 != null)
					{
						nint num8 = (nint)enumerable;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v332 @ r10_v7 (Il2CppClass<System.Collections.Generic.IEnumerable`1<UnityEngine.Vector4>>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_017d;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v332 @ r10_v7 (Il2CppClass<System.Collections.Generic.IEnumerable`1<UnityEngine.Vector4>>)+B0]");
						object obj19 = 0;
						int num9 = 0;
						while (true)
						{
							object obj20 = num9 + num9;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v544 @ r8_v19+v547 @ rax_v32*8]");
							if (0 == (nint)typeof(IEnumerator<Vector4>))
							{
								break;
							}
							num9++;
							int num10 = num9;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v332 @ r10_v7 (Il2CppClass<System.Collections.Generic.IEnumerable`1<UnityEngine.Vector4>>)+12E]");
							if ((nint)num10 < (nint)0)
							{
								continue;
							}
							goto IL_017d;
						}
						object obj21 = num9 + num9;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v544 @ r8_v19+8+v620 @ rcx_v25*8]");
						object obj22 = (nint)0 << 4;
						object obj23 = obj22 + 312;
						obj14 = obj23 + num8;
						goto IL_0523;
					}
					if (obj2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					}
					break;
					IL_02fe:
					bool flag6 = obj12 == null;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803CE296h\"");
					obj9 = obj12;
					if (!flag6)
					{
						obj9 = obj12;
						num7 = 3;
						num6 = 3;
					}
					goto IL_0394;
				}
				return num2;
			}
		}
		return 0;
	}

	public static Vector2[] ConvertUVsTo2D(IList<Vector4> uvs)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_008b: Expected O, but got F4
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		if (uvs != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = default(object);
			Vector2[] array = new Vector2[obj];
			if (array != null)
			{
				object obj2 = array + 32;
				int num = 0;
				for (int num2 = 0; num2 < array.Length; num2 = num)
				{
					obj2 = uvs.get_Item(num).x;
					num++;
					obj2 += 8;
				}
				return array;
			}
			return (Vector2[])(object)new NullReferenceException();
		}
		return null;
	}

	public static Vector3[] ConvertUVsTo3D(IList<Vector4> uvs)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		if (uvs != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = default(object);
			Vector3[] array = new Vector3[obj];
			if (array != null)
			{
				object obj2 = array + 32;
				int num = 0;
				object obj3 = default(object);
				for (int num2 = 0; num2 < array.Length; num2 = num)
				{
					Vector4 vector = uvs.get_Item(num);
					num++;
					obj2 = obj3;
					obj2 += 12;
				}
				return array;
			}
			return (Vector3[])(object)new NullReferenceException();
		}
		return null;
	}

	public unsafe static Vector2Int[] GetSubMeshIndexMinMax(int[][] indices, out IndexFormat indexFormat)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_004f: Expected O, but got I4
		//IL_0058: Expected O, but got I4
		//IL_008c: Expected O, but got I
		//IL_017e: Expected O, but got I4
		//IL_0187: Expected O, but got I4
		//IL_00f1: Expected O, but got I
		//IL_00fa: Expected O, but got I4
		//IL_0107: Expected O, but got I8
		//IL_0110: Expected O, but got I4
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Expected O, but got Unknown
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Expected O, but got Unknown
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Expected O, but got Unknown
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Expected O, but got Unknown
		if (indices != null)
		{
			Vector2Int[] array = new Vector2Int[indices.Length];
			ref IndexFormat reference = ref *(IndexFormat*)null;
			object obj = indices + 24;
			object obj2 = (object)indices - (object)array;
			object obj3 = array + 32;
			object obj4 = 0;
			object obj5 = 0;
			while (true)
			{
				object obj8;
				object obj9;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
				{
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rsi_v4+v61 @ r14_v4]");
					object obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rsi_v4+v61 @ r14_v4]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rbx_v6+18]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rsi_v4+v61 @ r14_v4]");
							object obj7 = (nint)0 + (nint)32;
							obj8 = 2147483647;
							obj9 = 2147483648L;
							object obj10 = 0;
							while (true)
							{
								object obj11 = obj10;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rbx_v6+18]");
								if ((nint)obj11 >= 0)
								{
									break;
								}
								object obj12 = obj10;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rbx_v6+18]");
								if ((nint)obj12 >= 0)
								{
									goto end_IL_02f9;
								}
								if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8))
								{
									obj8 = obj7;
								}
								if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9))
								{
									obj9 = obj7;
								}
								obj10++;
								obj7 += 4;
							}
							goto IL_018c;
						}
					}
					obj8 = 0;
					obj9 = 0;
					goto IL_018c;
				}
				return array;
				IL_018c:
				if ((nint)obj4 >= array.Length)
				{
					break;
				}
				object obj13 = obj9 - obj8;
				obj3 = obj8;
				if ((nint)obj13 > 65535)
				{
					reference = ref *(IndexFormat*)1;
				}
				obj4++;
				obj3 += 8;
				obj5 = obj4;
				continue;
				end_IL_02f9:
				break;
			}
			return (Vector2Int[])(object)new IndexOutOfRangeException();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex = new ArgumentNullException("indices");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	private unsafe static void GetIndexMinMax(int[] indices, out int minIndex, out int maxIndex)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0069: Expected O, but got I4
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		if (indices != null && indices.Length != 0)
		{
			ref int reference = ref *(int*)2147483647;
			ref int reference2 = ref *(int*)2147483648L;
			object obj = indices + 32;
			object obj2 = 0;
			while ((nint)obj2 < indices.Length)
			{
				if ((nint)obj < minIndex)
				{
					reference = ref *(int*)obj;
				}
				if ((nint)obj > maxIndex)
				{
					reference2 = ref *(int*)obj;
				}
				obj2++;
				obj += 4;
			}
		}
		else
		{
			ref int reference2 = ref *(int*)null;
			ref int reference = ref *(int*)null;
		}
	}
}
