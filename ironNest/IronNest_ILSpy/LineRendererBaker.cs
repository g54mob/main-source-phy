using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

public class LineRendererBaker : MonoBehaviour
{
	public bool includeInactive;

	public string bakedChildName = "BakedLines";

	public bool verboseLogging;

	public bool copyNormals;

	public bool copyTangents;

	public bool forceSingleMaterial = true;

	public Material overrideSingleMaterial;

	private bool baked;

	private GameObject bakedChild;

	private List<GameObject> disabledOriginals;

	public bool IsBaked => baked;

	public unsafe void Bake()
	{
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00de: Expected O, but got I4
		//IL_0100: Expected O, but got I4
		//IL_02e0: Expected I, but got O
		//IL_0148: Expected I, but got O
		//IL_2ca5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2caa: Expected O, but got Unknown
		//IL_2cb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cb8: Expected O, but got Unknown
		//IL_0172: Expected I, but got O
		//IL_01c2: Expected I, but got O
		//IL_01ca: Expected I, but got O
		//IL_0201: Expected I, but got O
		//IL_0206: Expected I, but got O
		//IL_0230: Expected I, but got O
		//IL_0235: Expected I, but got O
		//IL_0266: Expected I, but got O
		//IL_304b: Expected O, but got Ref
		//IL_1c79: Expected I, but got O
		//IL_1c7e: Expected I, but got O
		//IL_1db3: Expected O, but got Ref
		//IL_1ddc: Expected O, but got Ref
		//IL_1cc8: Expected I, but got O
		//IL_1cd1: Expected I, but got O
		//IL_1e05: Expected O, but got Ref
		//IL_2d39: Expected I, but got O
		//IL_0611: Expected I, but got O
		//IL_2c0f: Expected I4, but got O
		//IL_0659: Expected O, but got I4
		//IL_065e: Expected I, but got O
		//IL_0666: Expected I, but got O
		//IL_066e: Expected O, but got Ref
		//IL_0673: Expected I, but got O
		//IL_0683: Expected O, but got Ref
		//IL_1feb: Expected I, but got O
		//IL_0f35: Expected I, but got O
		//IL_2d5f: Expected O, but got I
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Expected O, but got Unknown
		//IL_0795: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Expected O, but got Unknown
		//IL_07b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bc: Expected O, but got Unknown
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f8: Expected O, but got Unknown
		//IL_0815: Unknown result type (might be due to invalid IL or missing references)
		//IL_081a: Expected O, but got Unknown
		//IL_089d: Expected O, but got Ref
		//IL_213c: Expected O, but got I4
		//IL_2149: Expected I4, but got I8
		//IL_0961: Expected F4, but got I4
		//IL_0f78: Expected I, but got O
		//IL_2d9a: Expected O, but got Ref
		//IL_0f9c: Expected I, but got O
		//IL_0978: Expected O, but got Ref
		//IL_08fc: Expected O, but got Ref
		//IL_309d: Unknown result type (might be due to invalid IL or missing references)
		//IL_30a2: Expected O, but got Unknown
		//IL_0a3c: Expected F4, but got I4
		//IL_2dc5: Expected O, but got Ref
		//IL_0922: Expected O, but got I
		//IL_0932: Expected F4, but got I
		//IL_229f: Expected I, but got O
		//IL_0a53: Expected O, but got Ref
		//IL_09d7: Expected O, but got Ref
		//IL_0b17: Expected F4, but got I4
		//IL_1245: Expected O, but got I
		//IL_1264: Expected O, but got I
		//IL_1269: Expected I, but got O
		//IL_127e: Expected O, but got I
		//IL_1214: Expected I, but got O
		//IL_2df0: Expected O, but got Ref
		//IL_09fd: Expected O, but got I
		//IL_0a0d: Expected F4, but got I
		//IL_2345: Expected I, but got O
		//IL_12a0: Expected I, but got O
		//IL_103e: Expected I, but got O
		//IL_1046: Expected O, but got I4
		//IL_0b2e: Expected O, but got Ref
		//IL_0ab2: Expected O, but got Ref
		//IL_10ea: Expected I, but got O
		//IL_10f3: Expected O, but got I4
		//IL_0bf2: Expected F4, but got I4
		//IL_12cc: Expected I, but got O
		//IL_11e1: Expected I, but got O
		//IL_106c: Expected O, but got I
		//IL_2e1b: Expected O, but got Ref
		//IL_0ad8: Expected O, but got I
		//IL_0ae8: Expected F4, but got I
		//IL_23fd: Expected I, but got O
		//IL_12f8: Expected I, but got O
		//IL_1301: Expected O, but got I4
		//IL_1311: Expected O, but got I
		//IL_143d: Expected O, but got I
		//IL_0c09: Expected O, but got Ref
		//IL_0b8d: Expected O, but got Ref
		//IL_14f9: Expected I, but got O
		//IL_0c91: Expected O, but got I8
		//IL_2bdb: Expected O, but got I
		//IL_1187: Expected O, but got I
		//IL_2e46: Expected O, but got Ref
		//IL_0bb3: Expected O, but got I
		//IL_0bc3: Expected F4, but got I
		//IL_1336: Expected O, but got I
		//IL_151a: Expected O, but got I4
		//IL_152a: Expected O, but got I4
		//IL_24b6: Expected I, but got O
		//IL_13d4: Expected I, but got O
		//IL_0ca4: Expected O, but got Ref
		//IL_0cc0: Expected O, but got F4
		//IL_0cce: Expected O, but got Ref
		//IL_0c68: Expected O, but got Ref
		//IL_1371: Expected I, but got O
		//IL_137a: Expected O, but got I4
		//IL_138a: Expected O, but got I
		//IL_0eac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb1: Expected O, but got Unknown
		//IL_0ee1: Expected I, but got O
		//IL_13a8: Expected O, but got I
		//IL_13ad: Expected I, but got O
		//IL_13c2: Expected O, but got I
		//IL_1613: Expected O, but got I4
		//IL_1621: Expected I, but got O
		//IL_1569: Expected O, but got I4
		//IL_0e76: Expected O, but got I
		//IL_1648: Expected O, but got I4
		//IL_2eab: Expected O, but got Ref
		//IL_159d: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a2: Expected O, but got Unknown
		//IL_15b0: Expected O, but got Ref
		//IL_0d4b: Expected O, but got Ref
		//IL_2a6f: Expected I4, but got O
		//IL_1675: Expected I, but got O
		//IL_1685: Expected O, but got I
		//IL_0e88: Expected O, but got Ref
		//IL_0da4: Expected O, but got Ref
		//IL_0dc2: Expected O, but got Ref
		//IL_2a48: Expected I4, but got O
		//IL_2a55: Expected O, but got I4
		//IL_0d6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6f: Expected O, but got Unknown
		//IL_0d8c: Expected O, but got I
		//IL_0e3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e44: Expected O, but got Unknown
		//IL_0e61: Expected O, but got I
		//IL_1736: Expected I, but got O
		//IL_17e8: Expected O, but got I
		//IL_1824: Expected I, but got O
		//IL_18ce: Expected O, but got I
		//IL_190a: Expected I, but got O
		//IL_19be: Expected O, but got I
		//IL_19fa: Expected I, but got O
		//IL_1bc1: Expected O, but got I4
		//IL_1bc9: Expected I, but got O
		//IL_1b3f: Expected I, but got O
		//IL_2b9b: Expected O, but got I4
		//IL_2ba3: Expected I, but got O
		//IL_1c2c: Expected I, but got O
		if (baked)
		{
			if (verboseLogging)
			{
				Debug.Log("[LineRendererBaker] Already baked; Unbake first (auto).");
			}
			Unbake();
		}
		LineRenderer[] componentsInChildren = GetComponentsInChildren<LineRenderer>(includeInactive);
		List<LineRenderer> list = new List<LineRenderer>();
		bool flag = componentsInChildren == null;
		nint num = 0;
		nint num2 = 0;
		List<LineRenderer> list2 = list;
		if (!flag)
		{
			object obj = componentsInChildren + 32;
			object obj2 = 0;
			num = 0;
			num2 = 0;
			list2 = list;
			LineRendererBaker lineRendererBaker = this;
			object obj3 = 0;
			LineRendererBaker lineRendererBaker2 = default(LineRendererBaker);
			Vector2 vector2 = default(Vector2);
			List<LineRenderer>.Enumerator enumerator2 = default(List<LineRenderer>.Enumerator);
			List<LineRenderer>.Enumerator enumerator3 = default(List<LineRenderer>.Enumerator);
			UnityEngine.Object obj5 = default(UnityEngine.Object);
			List<LineRenderer>.Enumerator enumerator4 = default(List<LineRenderer>.Enumerator);
			object obj8 = default(object);
			List<LineRenderer>.Enumerator enumerator5 = default(List<LineRenderer>.Enumerator);
			int num17 = default(int);
			int num18 = default(int);
			Vector2 vector6 = default(Vector2);
			float num19 = default(float);
			int num21 = default(int);
			float num22 = default(float);
			int num24 = default(int);
			float num25 = default(float);
			int num27 = default(int);
			float num28 = default(float);
			UnityEngine.Object obj26 = default(UnityEngine.Object);
			UnityEngine.Object obj27 = default(UnityEngine.Object);
			Vector3 vector10 = default(Vector3);
			object obj32 = default(object);
			Vector2[] array4 = default(Vector2[]);
			int num39 = default(int);
			List<List<int>> list28 = default(List<List<int>>);
			int num45 = default(int);
			List<List<int>> list33 = default(List<List<int>>);
			int num48 = default(int);
			List<List<int>> list37 = default(List<List<int>>);
			object obj37 = default(object);
			List<LineRenderer>.Enumerator enumerator6 = default(List<LineRenderer>.Enumerator);
			List<int> triangles2 = default(List<int>);
			List<List<int>> list45 = default(List<List<int>>);
			List<List<int>> list49 = default(List<List<int>>);
			List<List<int>> list53 = default(List<List<int>>);
			List<LineRenderer>.Enumerator enumerator7 = default(List<LineRenderer>.Enumerator);
			object arg = default(object);
			object arg2 = default(object);
			object arg3 = default(object);
			object arg4 = default(object);
			object arg5 = default(object);
			while (true)
			{
				if ((nint)obj3 < componentsInChildren.Length)
				{
					if ((nint)obj2 < componentsInChildren.Length)
					{
						bool flag2 = (UnityEngine.Object)obj;
						bool flag3 = !flag2;
						num2 = unchecked((nint)null);
						list2 = (List<LineRenderer>)obj;
						if (!flag3)
						{
							bool flag4 = obj == null;
							num2 = unchecked((nint)null);
							list2 = (List<LineRenderer>)obj;
							if (flag4)
							{
								break;
							}
							GameObject gameObject = ((Component)obj).gameObject;
							GameObject gameObject2 = lineRendererBaker.gameObject;
							bool flag5 = gameObject != gameObject2;
							num = unchecked((nint)null);
							num2 = (nint)gameObject2;
							list2 = (List<LineRenderer>)(object)gameObject;
							if (flag5)
							{
								int positionCount = ((LineRenderer)obj).positionCount;
								bool flag6 = positionCount == 0;
								num = unchecked((nint)null);
								num2 = unchecked((nint)null);
								list2 = (List<LineRenderer>)obj;
								if (!flag6)
								{
									bool flag7 = list == null;
									num = unchecked((nint)null);
									num2 = unchecked((nint)null);
									list2 = (List<LineRenderer>)obj;
									if (flag7)
									{
										break;
									}
									list.Add((LineRenderer)obj);
									num = 0;
									num2 = (nint)obj;
									list2 = list;
								}
							}
							lineRendererBaker = lineRendererBaker2;
						}
						obj2++;
						obj += 8;
						obj3 = obj2;
						continue;
					}
					throw new IndexOutOfRangeException();
				}
				if (list == null)
				{
					break;
				}
				if (list._size != 0)
				{
					Transform transform = lineRendererBaker.transform;
					bool flag8 = (object)transform == null;
					num2 = unchecked((nint)null);
					list2 = (List<LineRenderer>)(object)lineRendererBaker;
					if (flag8)
					{
						break;
					}
					Matrix4x4 worldToLocalMatrix = transform.worldToLocalMatrix;
					List<Material> list3 = new List<Material>();
					List<List<int>> list4 = new List<List<int>>();
					bool flag9 = !lineRendererBaker.forceSingleMaterial;
					Material material = null;
					if (!flag9)
					{
						material = lineRendererBaker.overrideSingleMaterial;
					}
					List<Vector3> list5 = new List<Vector3>();
					List<Vector2> list6 = new List<Vector2>();
					list6._002Ector();
					List<Vector2> list7 = new List<Vector2>();
					List<Vector2> list8 = new List<Vector2>();
					List<Vector2> list9 = new List<Vector2>();
					List<Color32> list10 = new List<Color32>();
					List<Vector3> list11 = new List<Vector3>();
					List<Vector4> list12 = new List<Vector4>();
					Mesh mesh = new Mesh();
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					Vector2 vector = vector2;
					UnityEngine.Object obj4 = material;
					List<LineRenderer>.Enumerator enumerator = enumerator2;
					Mesh mesh2 = mesh;
					num = 0;
					while (enumerator3.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						bool flag10 = obj5;
						bool flag11 = !flag10;
						num = 0;
						if (flag11)
						{
							continue;
						}
						bool flag12 = (object)mesh2 == null;
						nint num3 = 0;
						int num4 = 0;
						UnityEngine.Object obj6 = obj5;
						if (!flag12)
						{
							mesh2.Clear();
							bool flag13 = (object)obj5 == null;
							num3 = 0;
							num4 = 0;
							obj6 = mesh2;
							if (!flag13)
							{
								((LineRenderer)obj5).BakeMesh(mesh2, false);
								if (mesh2.vertexCount != 0)
								{
									Vector3[] vertices = mesh2.vertices;
									Color32[] colors = mesh2.colors32;
									Vector2[] uv = mesh2.uv;
									Vector2[] uv2 = mesh2.uv2;
									Vector2[] uv3 = mesh2.uv3;
									Vector2[] uv4 = mesh2.uv4;
									Vector3[] array;
									Mesh mesh3;
									if (lineRendererBaker.copyNormals)
									{
										Vector3[] normals = mesh2.normals;
										array = normals;
										num4 = 0;
										mesh3 = mesh2;
									}
									else
									{
										array = null;
										num4 = 0;
										mesh3 = mesh2;
									}
									Vector4[] array2;
									if (lineRendererBaker.copyTangents)
									{
										Vector4[] tangents = mesh.tangents;
										num4 = 0;
										mesh3 = mesh;
										array2 = tangents;
									}
									else
									{
										array2 = null;
									}
									bool flag14 = list5 == null;
									num3 = unchecked((nint)null);
									obj6 = mesh3;
									if (flag14)
									{
										throw new NullReferenceException();
									}
									Transform transform2 = ((Component)obj5).transform;
									bool flag15 = (object)transform2 == null;
									num3 = unchecked((nint)null);
									Vector3[] array3 = null;
									obj6 = obj5;
									if (flag15)
									{
										num4 = (int)array3;
										throw new NullReferenceException();
									}
									Matrix4x4 localToWorldMatrix = transform2.localToWorldMatrix;
									bool flag16 = vertices == null;
									enumerator = enumerator4;
									object obj7 = 0;
									num = unchecked((nint)null);
									num2 = (nint)vertices;
									list2 = (List<LineRenderer>)(&obj8);
									num3 = unchecked((nint)null);
									array3 = vertices;
									obj6 = (UnityEngine.Object)(&obj8);
									if (flag16)
									{
										throw new NullReferenceException();
									}
									while (true)
									{
										object obj9 = obj7;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ rdx_v20 (Il2CppMethodInfo)+18]");
										if ((nint)obj9 >= 0)
										{
											break;
										}
										object obj10 = obj7;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ rdx_v20 (Il2CppMethodInfo)+18]");
										nint num15;
										Vector3 vector4;
										List<Vector3> list13;
										if ((nint)obj10 < 0)
										{
											object obj11 = obj7 * 2;
											object obj12 = obj7 + obj11;
											float num5 = (float)enumerator4 * localToWorldMatrix.m01;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ rdx_v20 (Il2CppMethodInfo)+20+v10449 @ rcx_v341*4]");
											float num6 = 0f * localToWorldMatrix.m00;
											float num7 = num5 + num6;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ rdx_v20 (Il2CppMethodInfo)+28+v10449 @ rcx_v341*4]");
											float num8 = 0f * localToWorldMatrix.m02;
											float num9 = num7 + num8;
											float num10 = num9 + localToWorldMatrix.m03;
											object obj13 = (object)enumerator4 * (object)enumerator4;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ rdx_v20 (Il2CppMethodInfo)+20+v10449 @ rcx_v341*4]");
											object obj14 = 0 * enumerator4;
											object obj15 = obj13 + obj14;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ rdx_v20 (Il2CppMethodInfo)+28+v10449 @ rcx_v341*4]");
											object obj16 = 0 * enumerator4;
											object obj17 = obj15 + obj16;
											object obj18 = obj17 + (object)enumerator4;
											object obj19 = (object)enumerator4 * (object)enumerator4;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ rdx_v20 (Il2CppMethodInfo)+20+v10449 @ rcx_v341*4]");
											object obj20 = 0 * enumerator4;
											object obj21 = obj19 + obj20;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ rdx_v20 (Il2CppMethodInfo)+28+v10449 @ rcx_v341*4]");
											object obj22 = 0 * enumerator4;
											object obj23 = obj21 + obj22;
											object obj24 = obj23 + (object)enumerator4;
											object obj25 = obj18 * (object)enumerator4;
											float num11 = num10 * (float)enumerator4;
											float num12 = (float)obj25 + num11;
											Vector2 vector3 = (Vector2)(obj24 * (object)enumerator4);
											float num13 = num12 + (float)vector3;
											float num14 = num13 + (float)enumerator4;
											list5.Add((Vector3)(&enumerator5));
											float num16;
											if (uv != null && (nint)obj7 < uv.Length)
											{
												bool flag17 = (nint)obj7 >= uv.Length;
												num15 = 0;
												vector4 = (Vector3)(&enumerator5);
												list13 = list5;
												if (flag17)
												{
													throw new IndexOutOfRangeException();
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8319 @ rax_v345 (UnityEngine.Vector2[])+24+v923 @ rbx_v104*8]");
												Vector2 vector5 = (Vector2)0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8319 @ rax_v345 (UnityEngine.Vector2[])+20+v923 @ rbx_v104*8]");
												num16 = 0f;
												list13 = list5;
											}
											else
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371660");
												num17 = num18;
												Vector2 vector5 = vector6;
												num16 = num18;
												list13 = null;
											}
											bool flag18 = list6 == null;
											num15 = 0;
											vector4 = (Vector3)(&enumerator5);
											if (!flag18)
											{
												list6.Add((Vector2)(&num19));
												float num20;
												if (uv2 != null && (nint)obj7 < uv2.Length)
												{
													bool flag19 = (nint)obj7 >= uv2.Length;
													num15 = 0;
													vector4 = (Vector3)(&num19);
													list13 = (List<Vector3>)(object)list6;
													if (flag19)
													{
														throw new IndexOutOfRangeException();
													}
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8473 @ rax_v346 (UnityEngine.Vector2[])+24+v923 @ rbx_v104*8]");
													Vector2 vector7 = (Vector2)0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8473 @ rax_v346 (UnityEngine.Vector2[])+20+v923 @ rbx_v104*8]");
													num20 = 0f;
													list13 = (List<Vector3>)(object)list6;
												}
												else
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371660");
													num17 = num21;
													Vector2 vector7 = vector6;
													num20 = num21;
													list13 = null;
												}
												bool flag20 = list7 == null;
												num15 = 0;
												vector4 = (Vector3)(&num19);
												if (!flag20)
												{
													list7.Add((Vector2)(&num22));
													float num23;
													if (uv3 != null && (nint)obj7 < uv3.Length)
													{
														bool flag21 = (nint)obj7 >= uv3.Length;
														num15 = 0;
														vector4 = (Vector3)(&num22);
														list13 = (List<Vector3>)(object)list7;
														if (flag21)
														{
															throw new IndexOutOfRangeException();
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8622 @ rax_v347 (UnityEngine.Vector2[])+24+v923 @ rbx_v104*8]");
														Vector2 vector8 = (Vector2)0;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8622 @ rax_v347 (UnityEngine.Vector2[])+20+v923 @ rbx_v104*8]");
														num23 = 0f;
														list13 = (List<Vector3>)(object)list7;
													}
													else
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371660");
														num17 = num24;
														Vector2 vector8 = vector6;
														num23 = num24;
														list13 = null;
													}
													bool flag22 = list8 == null;
													num15 = 0;
													vector4 = (Vector3)(&num22);
													if (!flag22)
													{
														list8.Add((Vector2)(&num25));
														float num26;
														if (uv4 != null && (nint)obj7 < uv4.Length)
														{
															bool flag23 = (nint)obj7 >= uv4.Length;
															num15 = 0;
															vector4 = (Vector3)(&num25);
															list13 = (List<Vector3>)(object)list8;
															if (flag23)
															{
																throw new IndexOutOfRangeException();
															}
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8918 @ rax_v348 (UnityEngine.Vector2[])+24+v923 @ rbx_v104*8]");
															vector = (Vector2)0;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8918 @ rax_v348 (UnityEngine.Vector2[])+20+v923 @ rbx_v104*8]");
															num26 = 0f;
															list13 = (List<Vector3>)(object)list8;
														}
														else
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371660");
															num17 = num27;
															vector = vector6;
															num26 = num27;
															list13 = null;
														}
														bool flag24 = list9 == null;
														num15 = 0;
														vector4 = (Vector3)(&num25);
														if (!flag24)
														{
															list9.Add((Vector2)(&num28));
															if (colors != null && (nint)obj7 < colors.Length)
															{
																bool flag25 = (nint)obj7 >= colors.Length;
																num15 = 0;
																vector4 = (Vector3)(&num28);
																list13 = (List<Vector3>)(object)colors;
																if (flag25)
																{
																	throw new IndexOutOfRangeException();
																}
															}
															else
															{
																obj26 = (UnityEngine.Object)4294967295L;
															}
															bool flag26 = list10 == null;
															num15 = 0;
															vector4 = (Vector3)(&num28);
															list13 = (List<Vector3>)(object)list10;
															if (!flag26)
															{
																list10.Add((Color32)(&obj27));
																bool flag27 = !lineRendererBaker2.copyNormals;
																enumerator = (List<LineRenderer>.Enumerator)num26;
																num15 = 0;
																vector4 = (Vector3)(&obj27);
																list13 = (List<Vector3>)(object)list10;
																LineRendererBaker lineRendererBaker3 = lineRendererBaker2;
																if (!flag27)
																{
																	Vector3 vector9;
																	if (array != null && (nint)obj7 < array.Length)
																	{
																		bool flag28 = (nint)obj7 >= array.Length;
																		num15 = 0;
																		vector4 = (Vector3)(&obj27);
																		list13 = (List<Vector3>)(object)list10;
																		if (flag28)
																		{
																			throw new IndexOutOfRangeException();
																		}
																		object obj28 = obj7 * 2;
																		object obj29 = obj7 + obj28;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v927 @ rdi_v81 (UnityEngine.Vector3[])+20+v12101 @ rcx_v361*4]");
																		vector9 = (Vector3)0;
																	}
																	else
																	{
																		vector9 = Vector3.upVector;
																	}
																	bool flag29 = list11 == null;
																	num15 = 0;
																	vector4 = (Vector3)(&obj27);
																	list13 = list11;
																	if (flag29)
																	{
																		throw new NullReferenceException();
																	}
																	list11.Add((Vector3)(&vector10));
																	vector10 = vector9;
																	enumerator = (List<LineRenderer>.Enumerator)vector9;
																	num15 = 0;
																	vector4 = (Vector3)(&vector10);
																	list13 = list11;
																	lineRendererBaker3 = lineRendererBaker2;
																}
																bool flag30 = !lineRendererBaker3.copyTangents;
																num = num15;
																list2 = (List<LineRenderer>)(object)list13;
																if (!flag30)
																{
																	if (array2 != null && (nint)obj7 < array2.Length)
																	{
																		if ((nint)obj7 >= array2.Length)
																		{
																			throw new IndexOutOfRangeException();
																		}
																		object obj30 = obj7 + 2;
																		object obj31 = obj30 + obj30;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v961 @ r13_v67 (UnityEngine.Vector4[])+v12127 @ rax_v478*8]");
																		enumerator = (List<LineRenderer>.Enumerator)0;
																	}
																	else
																	{
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C40]");
																		enumerator = (List<LineRenderer>.Enumerator)0;
																	}
																	if (list12 == null)
																	{
																		throw new NullReferenceException();
																	}
																	list12.Add((Vector4)(&enumerator2));
																	enumerator2 = enumerator;
																	num = 0;
																	list2 = (List<LineRenderer>)(object)list12;
																}
																obj7++;
																num28 = num26;
																num25 = num23;
																num22 = num20;
																num19 = num16;
																enumerator5 = enumerator4;
																num2 = (nint)vertices;
																continue;
															}
															throw new NullReferenceException();
														}
														throw new NullReferenceException();
													}
													throw new NullReferenceException();
												}
												throw new NullReferenceException();
											}
											throw new NullReferenceException();
										}
										num15 = num;
										vector4 = (Vector3)num2;
										list13 = (List<Vector3>)(object)list2;
										throw new IndexOutOfRangeException();
									}
									int subMeshCount = mesh.subMeshCount;
									bool flag31 = (object)obj5 == null;
									num3 = num;
									array3 = null;
									obj6 = mesh;
									if (flag31)
									{
										throw new NullReferenceException();
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9AD10");
									nint num29 = unchecked((nint)null);
									Renderer renderer = (Renderer)obj5;
									mesh2 = mesh;
									int num30 = 0;
									while (num30 < subMeshCount)
									{
										int[] triangles = mesh2.GetTriangles(num30);
										bool flag32 = triangles == null;
										num = unchecked((nint)null);
										UnityEngine.Object obj34;
										UnityEngine.Object item;
										nint num15;
										if (!flag32)
										{
											bool flag33 = triangles.Length == 0;
											num = unchecked((nint)null);
											if (!flag33)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1539 @ stack_8 (LineRendererBaker)+33]");
												if ((nint)0 == 0)
												{
													bool flag34 = obj32 == null;
													UnityEngine.Object obj33 = null;
													if (!flag34)
													{
														int num31 = num30;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10530 @ rax_v357+18]");
														bool flag35 = (nint)num31 >= (nint)0;
														obj33 = null;
														if (!flag35)
														{
															int num32 = num30;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10530 @ rax_v357+18]");
															bool flag36 = (nint)num32 >= (nint)0;
															num15 = unchecked((nint)null);
															Vector3 vector4 = (Vector3)num30;
															List<Vector3> list13 = (List<Vector3>)(object)mesh2;
															if (flag36)
															{
																throw new IndexOutOfRangeException();
															}
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10530 @ rax_v357+20+v3805 @ r15_v71 (System.Int32)*8]");
															obj33 = (UnityEngine.Object)0;
														}
													}
													bool flag37 = obj33 == null;
													bool flag38 = !flag37;
													Material material2 = (Material)obj33;
													if (!flag38)
													{
														Material sharedMaterial = renderer.GetSharedMaterial();
														material2 = sharedMaterial;
													}
													if (material2 != null)
													{
														bool flag39 = list3 == null;
														num15 = unchecked((nint)null);
														Vector3 vector4 = (Vector3)0;
														List<Vector3> list13 = (List<Vector3>)(object)material2;
														if (!flag39)
														{
															int num33 = list3.IndexOf(material2);
															bool flag40 = num33 >= 0;
															obj34 = material2;
															num3 = 0;
															array3 = (Vector3[])(object)material2;
															if (!flag40)
															{
																list3.Add(material2);
																List<int> list14 = new List<int>();
																bool flag41 = list4 == null;
																num15 = 0;
																vector4 = (Vector3)0;
																list13 = (List<Vector3>)(object)list14;
																if (flag41)
																{
																	throw new NullReferenceException();
																}
																list4.Add(list14);
																obj34 = material2;
																num3 = 0;
																array3 = (Vector3[])(object)list14;
															}
															goto IL_1473;
														}
														throw new NullReferenceException();
													}
													num30++;
													mesh2 = mesh;
													num = unchecked((nint)null);
													continue;
												}
												if (!(obj4 == null))
												{
													obj34 = obj4;
													num3 = unchecked((nint)null);
													array3 = null;
													obj6 = obj4;
													item = obj4;
													goto IL_2f48;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1539 @ stack_8 (LineRendererBaker)+38]");
												bool flag42 = (UnityEngine.Object)0 == null;
												bool flag43 = !flag42;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1539 @ stack_8 (LineRendererBaker)+38]");
												obj34 = (UnityEngine.Object)0;
												num3 = unchecked((nint)null);
												array3 = null;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1539 @ stack_8 (LineRendererBaker)+38]");
												obj6 = (UnityEngine.Object)0;
												if (!flag43)
												{
													bool flag44 = obj32 == null;
													num3 = unchecked((nint)null);
													if (!flag44)
													{
														int num34 = num30;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10530 @ rax_v357+18]");
														bool flag45 = (nint)num34 >= (nint)0;
														num3 = unchecked((nint)null);
														if (!flag45)
														{
															int num35 = num30;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10530 @ rax_v357+18]");
															bool flag46 = (nint)num35 >= (nint)0;
															num15 = unchecked((nint)null);
															Vector3 vector4 = (Vector3)0;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1539 @ stack_8 (LineRendererBaker)+38]");
															List<Vector3> list13 = (List<Vector3>)0;
															if (flag46)
															{
																throw new IndexOutOfRangeException();
															}
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10530 @ rax_v357+20+v3805 @ r15_v71 (System.Int32)*8]");
															if ((UnityEngine.Object)0 != null)
															{
																int num36 = num30;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10530 @ rax_v357+18]");
																bool flag47 = (nint)num36 >= (nint)0;
																num15 = unchecked((nint)null);
																vector4 = (Vector3)0;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10530 @ rax_v357+20+v3805 @ r15_v71 (System.Int32)*8]");
																list13 = (List<Vector3>)0;
																if (!flag47)
																{
																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10530 @ rax_v357+20+v3805 @ r15_v71 (System.Int32)*8]");
																	obj34 = (UnityEngine.Object)0;
																	num3 = unchecked((nint)null);
																	array3 = null;
																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10530 @ rax_v357+20+v3805 @ r15_v71 (System.Int32)*8]");
																	obj6 = (UnityEngine.Object)0;
																	goto IL_2f65;
																}
																throw new IndexOutOfRangeException();
															}
															renderer = (Renderer)obj5;
															num3 = unchecked((nint)null);
														}
													}
													Material sharedMaterial2 = renderer.GetSharedMaterial();
													obj34 = sharedMaterial2;
													array3 = null;
													obj6 = renderer;
												}
												goto IL_2f65;
											}
										}
										goto IL_2f0c;
										IL_2f48:
										List<Material> list15;
										if (list3 != null)
										{
											bool flag48 = list3._size != 0;
											list15 = list3;
											if (!flag48)
											{
												list3.Add((Material)item);
												List<int> list16 = new List<int>();
												bool flag49 = list4 == null;
												num15 = 0;
												Vector3 vector4 = (Vector3)0;
												List<Vector3> list13 = (List<Vector3>)(object)list16;
												if (!flag49)
												{
													list4.Add(list16);
													num3 = 0;
													array3 = (Vector3[])(object)list16;
													goto IL_1473;
												}
												throw new NullReferenceException();
											}
											goto IL_2fa1;
										}
										throw new NullReferenceException();
										IL_2f0c:
										num30++;
										continue;
										IL_2f65:
										obj4 = obj34;
										item = obj34;
										goto IL_2f48;
										IL_1473:
										list15 = list3;
										goto IL_2fa1;
										IL_1c31:
										renderer = (Renderer)obj5;
										mesh2 = mesh;
										goto IL_2f0c;
										IL_2fa1:
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1539 @ stack_8 (LineRendererBaker)+33]");
										Material material3;
										int num37;
										if ((nint)0 != 0)
										{
											num15 = num3;
											material3 = (Material)(object)array3;
											num37 = 0;
										}
										else
										{
											num37 = list15.IndexOf((Material)obj34);
											bool flag50 = num37 < 0;
											num15 = 0;
											material3 = (Material)obj34;
											if (flag50)
											{
												Debug.LogError("[LineRendererBaker] Internal material indexing error.");
												num = 0;
												goto IL_1c31;
											}
										}
										bool flag51 = list4 == null;
										array3 = (Vector3[])(object)material3;
										nint num38 = (nint)list4;
										if (!flag51)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
											object obj35 = 0;
											num15 = (nint)(&array4);
											Vector3 vector4 = (Vector3)num37;
											List<Vector3> list13 = (List<Vector3>)(object)list4;
											while ((nint)obj35 < triangles.Length)
											{
												if ((nint)obj35 < triangles.Length)
												{
													list13 = (List<Vector3>)triangles[obj35];
													if (array4 != null)
													{
														((List<int>)(object)array4).Add((int)(&num39));
														obj35++;
														num15 = 0;
														vector4 = (Vector3)(&num39);
														list13 = (List<Vector3>)(object)array4;
														continue;
													}
													throw new NullReferenceException();
												}
												throw new IndexOutOfRangeException();
											}
											bool flag52 = !lineRendererBaker2.verboseLogging;
											num29 = 0;
											num = num15;
											if (!flag52)
											{
												object[] array5 = new object[6];
												bool flag53 = array5 == null;
												Vector3 vector11 = (Vector3)6;
												num38 = (nint)typeof(object[]);
												if (flag53)
												{
													array3 = (Vector3[])vector11;
													throw new NullReferenceException();
												}
												bool flag54 = "LineRendererBaker" == null;
												vector4 = (Vector3)6;
												List<Vector3> list17 = (List<Vector3>)(object)typeof(object[]);
												List<Vector3> list18 = (List<Vector3>)(object)"LineRendererBaker";
												List<int> list21;
												if (!flag54)
												{
													nint num40 = (nint)array5;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11774 @ rdx_v272 (Il2CppClass<System.Object[]>)+40]");
													vector4 = (Vector3)0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11774 @ rdx_v272 (Il2CppClass<System.Object[]>)+40]");
													List<int> list19 = ((List<List<int>>)(object)"LineRendererBaker").get_Item(0);
													bool flag55 = list19 == null;
													list13 = (List<Vector3>)(object)"LineRendererBaker";
													if (flag55)
													{
														List<int> list20 = ((List<List<int>>)(object)list13).get_Item((int)vector4);
														vector4 = (Vector3)0;
														list21 = list20;
														throw list20;
													}
													list17 = (List<Vector3>)(object)"LineRendererBaker";
													list18 = (List<Vector3>)(object)"LineRendererBaker";
												}
												bool flag56 = array5.Length <= 0;
												list21 = (List<int>)(object)list17;
												if (flag56)
												{
													int num41 = (int)vector4;
													List<List<int>> list22 = (List<List<int>>)(object)list21;
													throw new IndexOutOfRangeException();
												}
												array5[0] = list18;
												string text = obj5.name;
												bool flag57 = text == null;
												int num42 = 0;
												string text2 = (string)(object)obj5;
												List<int> list25;
												if (!flag57)
												{
													nint num43 = (nint)array5;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12054 @ rdx_v270 (Il2CppClass<System.Object[]>)+40]");
													num42 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12054 @ rdx_v270 (Il2CppClass<System.Object[]>)+40]");
													List<int> list23 = ((List<List<int>>)(object)text).get_Item(0);
													bool flag58 = list23 == null;
													text2 = text;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12054 @ rdx_v270 (Il2CppClass<System.Object[]>)+40]");
													int num41 = 0;
													List<List<int>> list22 = (List<List<int>>)(object)text;
													if (flag58)
													{
														List<int> list24 = list22.get_Item(num41);
														num42 = 0;
														list25 = list24;
														throw list24;
													}
												}
												bool flag59 = array5.Length <= 1;
												list25 = (List<int>)(object)text2;
												if (flag59)
												{
													List<List<int>> list26 = (List<List<int>>)(object)list25;
													throw new IndexOutOfRangeException();
												}
												array5[1] = text;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
												List<int> list27 = (List<int>)0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
												bool flag60 = list28 == null;
												int num44 = (int)(&num45);
												if (!flag60)
												{
													nint num46 = (nint)array5;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12205 @ rdx_v268 (Il2CppClass<System.Object[]>)+40]");
													num44 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12205 @ rdx_v268 (Il2CppClass<System.Object[]>)+40]");
													List<int> list29 = list28.get_Item(0);
													bool flag61 = list29 == null;
													list27 = (List<int>)(object)list28;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12205 @ rdx_v268 (Il2CppClass<System.Object[]>)+40]");
													num42 = 0;
													List<List<int>> list26 = list28;
													if (flag61)
													{
														List<int> list30 = list26.get_Item(num42);
														num44 = 0;
														list27 = list30;
														throw list30;
													}
												}
												if (array5.Length <= 2)
												{
													List<List<int>> list31 = (List<List<int>>)(object)list27;
													throw new IndexOutOfRangeException();
												}
												array5[2] = list28;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
												List<int> list32 = (List<int>)0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
												bool flag62 = list33 == null;
												int num47 = (int)(&num48);
												if (!flag62)
												{
													nint num49 = (nint)array5;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12272 @ rdx_v266 (Il2CppClass<System.Object[]>)+40]");
													num47 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12272 @ rdx_v266 (Il2CppClass<System.Object[]>)+40]");
													List<int> list34 = list33.get_Item(0);
													bool flag63 = list34 == null;
													list32 = (List<int>)(object)list33;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12272 @ rdx_v266 (Il2CppClass<System.Object[]>)+40]");
													num44 = 0;
													List<List<int>> list31 = list33;
													if (flag63)
													{
														List<int> list35 = list31.get_Item(num44);
														num47 = 0;
														list32 = list35;
														throw list35;
													}
												}
												if (array5.Length <= 3)
												{
													List<List<int>> list36 = (List<List<int>>)(object)list32;
													throw new IndexOutOfRangeException();
												}
												array5[3] = list33;
												Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul dword ptr [rsi+18h]\"");
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
												UnityEngine.Object obj36 = (UnityEngine.Object)0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
												bool flag64 = list37 == null;
												int num50 = (int)(&obj37);
												if (!flag64)
												{
													nint num51 = (nint)array5;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12340 @ rdx_v264 (Il2CppClass<System.Object[]>)+40]");
													num50 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12340 @ rdx_v264 (Il2CppClass<System.Object[]>)+40]");
													List<int> list38 = list37.get_Item(0);
													bool flag65 = list38 == null;
													obj36 = (UnityEngine.Object)(object)list37;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12340 @ rdx_v264 (Il2CppClass<System.Object[]>)+40]");
													num47 = 0;
													List<List<int>> list36 = list37;
													if (flag65)
													{
														List<int> list39 = list36.get_Item(num47);
														num50 = 0;
														obj36 = (UnityEngine.Object)(object)list39;
														throw list39;
													}
												}
												if (array5.Length <= 4)
												{
													throw new IndexOutOfRangeException();
												}
												array5[4] = list37;
												string text4;
												int num52;
												UnityEngine.Object obj38;
												if ((bool)obj34)
												{
													bool flag66 = (object)obj34 == null;
													num50 = 0;
													obj36 = obj34;
													if (flag66)
													{
														throw new NullReferenceException();
													}
													string text3 = obj34.name;
													text4 = text3;
													num52 = 0;
													obj38 = obj34;
												}
												else
												{
													text4 = "NULL";
													num52 = 0;
													obj38 = obj34;
												}
												if (text4 != null)
												{
													nint num53 = (nint)array5;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12569 @ rdx_v261 (Il2CppClass<System.Object[]>)+40]");
													num52 = 0;
													string text5 = text4;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12569 @ rdx_v261 (Il2CppClass<System.Object[]>)+40]");
													List<int> list40 = ((List<List<int>>)(object)text5).get_Item(0);
													bool flag67 = list40 == null;
													obj38 = (UnityEngine.Object)(object)text4;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12569 @ rdx_v261 (Il2CppClass<System.Object[]>)+40]");
													num50 = 0;
													obj36 = (UnityEngine.Object)(object)text4;
													if (flag67)
													{
														List<int> list41 = ((List<List<int>>)(object)obj36).get_Item(num50);
														vector11 = (Vector3)0;
														num38 = (nint)list41;
														throw list41;
													}
												}
												bool flag68 = array5.Length <= 5;
												vector11 = (Vector3)num52;
												num38 = (nint)obj38;
												if (flag68)
												{
													throw new IndexOutOfRangeException();
												}
												array5[5] = text4;
												string message = string.Format("[{0}] Added '{1}' submesh {2}, verts {3}, tris {4}, using material '{5}'.", array5);
												Debug.Log(message);
												num48 = vertices.Length;
												num45 = num30;
												num29 = 0;
												num = unchecked((nint)null);
											}
											goto IL_1c31;
										}
										num3 = num15;
										obj6 = (UnityEngine.Object)num38;
										throw new NullReferenceException();
									}
									num39 = num30;
									num17 = subMeshCount;
									lineRendererBaker = lineRendererBaker2;
								}
								else
								{
									bool flag69 = !lineRendererBaker.verboseLogging;
									nint num29 = unchecked((nint)null);
									num = unchecked((nint)null);
									if (!flag69)
									{
										string text6 = obj5.name;
										string message2 = "[LineRendererBaker] Line '" + text6 + "' produced 0 verts, skipped.";
										Debug.Log(message2);
										num29 = unchecked((nint)null);
										num = unchecked((nint)"' produced 0 verts, skipped.");
									}
								}
								continue;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					enumerator3.Dispose();
					bool flag70 = list5 == null;
					num2 = 0;
					list2 = (List<LineRenderer>)(&enumerator3);
					if (flag70)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2812 @ rax_v154 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
					if ((nint)0 != 0)
					{
						bool flag71 = string.IsNullOrWhiteSpace(lineRendererBaker.bakedChildName);
						string text7 = "BakedLines";
						if (!flag71)
						{
							text7 = lineRendererBaker.bakedChildName;
						}
						GameObject gameObject3 = new GameObject(text7);
						bakedChild = gameObject3;
						Transform transform3 = lineRendererBaker.bakedChild.transform;
						Transform parent = lineRendererBaker.transform;
						transform3.SetParent(parent, worldPositionStays: false);
						Transform transform4 = lineRendererBaker.bakedChild.transform;
						transform4.localPosition = (Vector3)(&vector10);
						Transform transform5 = lineRendererBaker.bakedChild.transform;
						transform5.localRotation = (Quaternion)(&enumerator2);
						Transform transform6 = lineRendererBaker.bakedChild.transform;
						transform6.localScale = (Vector3)(&enumerator6);
						GameObject gameObject4 = lineRendererBaker.gameObject;
						int layer = gameObject4.layer;
						lineRendererBaker.bakedChild.layer = layer;
						Mesh mesh4 = new Mesh();
						mesh4.name = "BakedLineRenderer_Mesh";
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2812 @ rax_v154 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
						if ((nint)0 > (nint)65535)
						{
							mesh4.indexFormat = IndexFormat.UInt32;
						}
						mesh4.SetVertices(list5);
						mesh4.SetUVs(0, list6);
						mesh4.SetUVs(1, list7);
						mesh4.SetUVs(2, list8);
						mesh4.SetUVs(3, list9);
						mesh4.SetColors(list10);
						if (lineRendererBaker.copyNormals)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4583 @ rax_v166 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
							nint num54 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2812 @ rax_v154 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
							if (num54 == 0)
							{
								mesh4.SetNormals(list11);
							}
						}
						if (lineRendererBaker.copyTangents)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4880 @ rax_v168 (System.Collections.Generic.List`1<UnityEngine.Vector4>)+18]");
							nint num55 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2812 @ rax_v154 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
							if (num55 == 0)
							{
								mesh4.SetTangents(list12);
							}
						}
						bool flag72 = list3._size != 0;
						nint num56 = unchecked((nint)null);
						if (!flag72)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							bool flag73 = (object)lineRendererBaker.overrideSingleMaterial != null;
							UnityEngine.Object obj39 = lineRendererBaker.overrideSingleMaterial;
							if (!flag73)
							{
								if ((bool)obj26)
								{
									Material sharedMaterial3 = ((Renderer)obj26).GetSharedMaterial();
									obj39 = sharedMaterial3;
								}
								else
								{
									obj39 = null;
								}
							}
							if (obj39 == null)
							{
								Debug.LogError("[LineRendererBaker] No material found; assigning Unity built-in default material.");
								Material builtinResource = Resources.GetBuiltinResource<Material>("Default-Line.mat");
								obj39 = builtinResource;
							}
							list3.Add((Material)obj39);
							List<int> item2 = new List<int>();
							list4.Add(item2);
							num56 = 0;
						}
						mesh4.subMeshCount = list3._size;
						int num57 = 0;
						List<List<int>> list42 = list4;
						int num58 = 0;
						int index3;
						List<List<int>> list55;
						while (true)
						{
							if (num58 < list3._size)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								nint num59 = 0;
								object obj40 = 0;
								int num60 = -1;
								nint num3 = (nint)(&triangles2);
								while (true)
								{
									object obj41 = obj40;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7218 @ stack_-360_v18 (System.Collections.Generic.List`1<System.Int32>)+18]");
									if ((nint)obj41 >= 0)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									bool flag74 = (nint)obj26 <= num60;
									num59 = 0;
									num3 = (nint)(&obj26);
									if (!flag74)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
										num59 = 0;
										num60 = num17;
										num3 = (nint)(&num17);
									}
									obj40++;
								}
								int num61 = num60;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2812 @ rax_v154 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
								if ((nint)num61 < (nint)0)
								{
									mesh4.SetTriangles(triangles2, num57, calculateBounds: true);
									num57++;
									num56 = 1;
									list42 = list4;
									num58 = num57;
									continue;
								}
								object[] array6 = new object[4];
								if (array6 != null)
								{
									bool flag75 = "LineRendererBaker" == null;
									UnityEngine.Object obj42 = (UnityEngine.Object)(object)"LineRendererBaker";
									if (!flag75)
									{
										nint num62 = (nint)array6;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12245 @ rdx_v189 (Il2CppClass<System.Object[]>)+40]");
										int num4 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12245 @ rdx_v189 (Il2CppClass<System.Object[]>)+40]");
										List<int> list43 = ((List<List<int>>)(object)"LineRendererBaker").get_Item(0);
										bool flag76 = list43 == null;
										UnityEngine.Object obj6 = (UnityEngine.Object)(object)"LineRendererBaker";
										if (flag76)
										{
											List<int> list44 = ((List<List<int>>)(object)obj6).get_Item(num4);
											throw list44;
										}
										obj42 = (UnityEngine.Object)(object)"LineRendererBaker";
									}
									if (array6.Length > 0)
									{
										array6[0] = obj42;
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										if (list45 != null)
										{
											nint num63 = (nint)array6;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12311 @ rdx_v187 (Il2CppClass<System.Object[]>)+40]");
											int index = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12311 @ rdx_v187 (Il2CppClass<System.Object[]>)+40]");
											List<int> list46 = list45.get_Item(0);
											bool flag77 = list46 == null;
											List<List<int>> list47 = list45;
											if (flag77)
											{
												List<int> list48 = list47.get_Item(index);
												throw list48;
											}
										}
										if (array6.Length > 1)
										{
											array6[1] = list45;
											Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
											if (list49 != null)
											{
												nint num64 = (nint)array6;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12434 @ rdx_v185 (Il2CppClass<System.Object[]>)+40]");
												int index2 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12434 @ rdx_v185 (Il2CppClass<System.Object[]>)+40]");
												List<int> list50 = list49.get_Item(0);
												bool flag78 = list50 == null;
												List<List<int>> list51 = list49;
												if (flag78)
												{
													List<int> list52 = list51.get_Item(index2);
													throw list52;
												}
											}
											if (array6.Length > 2)
											{
												array6[2] = list49;
												Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
												if (list53 != null)
												{
													nint num65 = (nint)array6;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12551 @ rdx_v183 (Il2CppClass<System.Object[]>)+40]");
													index3 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12551 @ rdx_v183 (Il2CppClass<System.Object[]>)+40]");
													List<int> list54 = list53.get_Item(0);
													bool flag79 = list54 == null;
													list55 = list53;
													if (flag79)
													{
														break;
													}
												}
												if (array6.Length > 3)
												{
													array6[3] = list53;
													string message3 = string.Format("[{0}] Submesh {1} max tri index {2} >= vertex count {3}. Clearing submesh.", array6);
													Debug.LogError(message3);
													int[] triangles3 = Array.Empty<int>();
													mesh4.SetTriangles(triangles3, num57, calculateBounds: true);
													num57++;
													num56 = 1;
													list42 = list4;
													num58 = num57;
													continue;
												}
											}
										}
									}
									throw new IndexOutOfRangeException();
								}
								throw new NullReferenceException();
							}
							mesh4.RecalculateBounds();
							MeshFilter meshFilter = lineRendererBaker.bakedChild.AddComponent<MeshFilter>();
							meshFilter.sharedMesh = mesh4;
							MeshRenderer meshRenderer = lineRendererBaker.bakedChild.AddComponent<MeshRenderer>();
							Material[] array7 = list3.ToArray();
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
							lineRendererBaker.disabledOriginals.Clear();
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
							while (true)
							{
								if (enumerator7.MoveNext())
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
									if (!obj26)
									{
										continue;
									}
									if ((object)obj26 != null)
									{
										GameObject gameObject5 = ((Component)obj26).gameObject;
										if ((object)gameObject5 != null)
										{
											if (gameObject5.activeSelf)
											{
												GameObject gameObject6 = ((Component)obj26).gameObject;
												if ((object)gameObject6 == null)
												{
													throw new NullReferenceException();
												}
												gameObject6.SetActive(value: false);
												GameObject item3 = ((Component)obj26).gameObject;
												if (lineRendererBaker.disabledOriginals == null)
												{
													break;
												}
												lineRendererBaker.disabledOriginals.Add(item3);
											}
											continue;
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								enumerator7.Dispose();
								lineRendererBaker.baked = true;
								if (mesh != null)
								{
									UnityEngine.Object.Destroy(mesh);
								}
								string[] array8 = new string[5];
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								string text8 = string.Format("[{0}] Bake complete. Lines: {1}. ", "LineRendererBaker", arg);
								array8[0] = text8;
								array8[1] = "Mode: ";
								bool flag80 = !lineRendererBaker.forceSingleMaterial;
								object obj43 = "Multi-material";
								if (!flag80)
								{
									obj43 = "Single Material";
								}
								array8[2] = (string)obj43;
								array8[3] = ". ";
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								string text9 = $"Final submeshes: {arg2}. Source submeshes: {arg3}. Skipped: {arg4}.";
								array8[4] = text9;
								string message4 = string.Concat(array8);
								Debug.Log(message4);
								return;
							}
							throw new NullReferenceException();
						}
						List<int> list56 = list55.get_Item(index3);
						throw list56;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					string message5 = string.Format("[{0}] No vertex data produced. Skipped submeshes: {1}.", "LineRendererBaker", arg5);
					Debug.LogWarning(message5);
					lineRendererBaker.DestroyTempMesh(mesh2);
				}
				else
				{
					Debug.LogWarning("[LineRendererBaker] No LineRenderer components found.");
				}
				return;
			}
		}
		throw new NullReferenceException();
	}

	public unsafe void Unbake()
	{
		if (baked)
		{
			if ((bool)bakedChild)
			{
				UnityEngine.Object.Destroy(bakedChild);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			object obj2 = default(object);
			while (true)
			{
				if (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if ((bool)obj)
					{
						if ((object)obj == null)
						{
							break;
						}
						((GameObject)obj).SetActive(true);
					}
					continue;
				}
				enumerator.Dispose();
				List<GameObject> list = disabledOriginals;
				int version = list._version + 1;
				list._version = version;
				((List<GameObject>.Enumerator*)null)->Dispose();
				if (obj2 == null)
				{
					list._size = 0;
				}
				else
				{
					list._size = 0;
					if (list._size > 0)
					{
						Array.Clear(list._items, 0, list._size);
					}
				}
				bakedChild = null;
				baked = false;
				Debug.Log("[LineRendererBaker] Unbaked. Originals restored.");
				return;
			}
			throw new NullReferenceException();
		}
		Debug.LogWarning("[LineRendererBaker] Not currently baked.");
	}

	private void DestroyTempMesh(Mesh m)
	{
		if (m != null)
		{
			UnityEngine.Object.Destroy(m);
		}
	}

	public LineRendererBaker()
	{
		List<GameObject> list = new List<GameObject>();
		disabledOriginals = list;
		base._002Ector();
	}
}
