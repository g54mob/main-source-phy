using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace MTAssets.UltimateLODSystem.MeshSimplifier;

public static class MeshCombiner
{
	public unsafe static Mesh CombineMeshes(Transform rootTransform, MeshRenderer[] renderers, out Material[] resultMaterials)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0046: Expected O, but got I4
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00e0: Expected O, but got I4
		//IL_012d: Expected O, but got I
		//IL_0914: Expected O, but got Ref
		//IL_017f: Expected O, but got I
		//IL_0954: Expected O, but got Ref
		//IL_09b1: Expected O, but got Ref
		//IL_023c: Expected O, but got I4
		//IL_09e5: Expected O, but got Ref
		//IL_0295: Expected O, but got I4
		//IL_02eb: Expected O, but got I
		//IL_034d: Expected O, but got Ref
		//IL_035b: Expected O, but got Ref
		//IL_0372: Expected O, but got F4
		//IL_03a0: Expected F4, but got I
		//IL_03ba: Expected F4, but got I
		//IL_03fe: Expected I, but got O
		//IL_042c: Expected I, but got O
		//IL_044f: Expected O, but got F4
		//IL_04a5: Expected I, but got O
		//IL_04d3: Expected O, but got I4
		//IL_0555: Expected I, but got O
		//IL_04e9: Expected I, but got O
		//IL_04f9: Expected O, but got I
		//IL_0517: Expected I, but got O
		//IL_0527: Expected O, but got I
		//IL_0580: Expected O, but got I
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Expected O, but got Unknown
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Expected O, but got Unknown
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Expected O, but got Unknown
		//IL_05af: Expected I, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if (rootTransform != null)
		{
			bool flag = renderers == null;
			UnityEngine.Object obj3 = null;
			Matrix4x4 matrix4x = (Matrix4x4)0;
			if (!flag)
			{
				Mesh[] array = new Mesh[renderers.Length];
				Matrix4x4[] array2 = new Matrix4x4[renderers.Length];
				Material[][] array3 = new Material[renderers.Length][];
				object obj4 = array2 + 32;
				object obj5 = renderers + 24;
				object obj6 = (object)renderers - (object)array;
				object obj7 = array + 32;
				Matrix4x4 matrix4x2 = (Matrix4x4)((object)array3 - (object)array);
				Matrix4x4 matrix4x3 = matrix4x2;
				Matrix4x4 matrix4x4 = (Matrix4x4)0;
				ArgumentNullException ex = null;
				ArgumentNullException ex2 = null;
				UnityEngine.Object obj8 = default(UnityEngine.Object);
				object arg = default(object);
				object arg2 = default(object);
				object arg3 = default(object);
				ArgumentNullException ex6 = default(ArgumentNullException);
				object obj12 = default(object);
				object arg4 = default(object);
				ref Material[] resultMaterials2 = default(ref Material[]);
				ref Transform[] resultBones = default(ref Transform[]);
				while (true)
				{
					if (System.Runtime.CompilerServices.Unsafe.As<ArgumentNullException, UIntPtr>(ref ex) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
					{
						bool flag2 = System.Runtime.CompilerServices.Unsafe.As<ArgumentNullException, UIntPtr>(ref ex2) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5);
						string text = (string)matrix4x4;
						if (flag2)
						{
							goto IL_08fa;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v256 @ rdi_v18+v238 @ r14_v13]");
						bool flag3 = (UnityEngine.Object)0 != null;
						string text2 = null;
						if (flag3)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v256 @ rdi_v18+v238 @ r14_v13]");
							bool flag4 = (nint)0 == 0;
							text2 = null;
							if (!flag4)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v256 @ rdi_v18+v238 @ r14_v13]");
								Transform transform = ((Component)0).transform;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
								bool flag5 = obj8 != null;
								string text3 = null;
								if (!flag5)
								{
									object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 176));
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									string message = $"The renderer at index {arg} has no mesh filter.";
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
									ArgumentException ex3 = new ArgumentException(message, "renderers");
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
									throw ex3;
								}
								bool flag6 = (object)obj8 == null;
								text2 = null;
								if (!flag6)
								{
									Mesh sharedMesh = ((MeshFilter)obj8).sharedMesh;
									bool flag7 = sharedMesh != null;
									string text4 = null;
									string text5 = (string)(object)ex2;
									if (!flag7)
									{
										object obj10 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 176));
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										string message2 = $"The mesh filter for renderer at index {arg2} has no mesh.";
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
										ArgumentException ex4 = new ArgumentException(message2, "renderers");
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
										throw ex4;
									}
									Mesh sharedMesh2 = ((MeshFilter)obj8).sharedMesh;
									bool flag8 = (object)sharedMesh2 == null;
									text2 = null;
									if (!flag8)
									{
										bool isReadable = sharedMesh2.isReadable;
										bool flag9 = !isReadable;
										matrix4x4 = (Matrix4x4)0;
										if (flag9)
										{
											object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 176));
											Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
											string message3 = $"The mesh in the mesh filter for renderer at index {arg3} is not readable.";
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
											ArgumentException ex5 = new ArgumentException(message3, "renderers");
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
											throw ex5;
										}
										Mesh sharedMesh3 = ((MeshFilter)obj8).sharedMesh;
										bool flag10 = array == null;
										text2 = null;
										if (!flag10)
										{
											bool flag11 = (nint)ex2 >= array.Length;
											matrix4x3 = (Matrix4x4)0;
											text = null;
											if (flag11)
											{
												goto IL_08fa;
											}
											obj7 = sharedMesh3;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+B0]");
											bool flag12 = (nint)0 == 0;
											text2 = null;
											if (!flag12)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+B0]");
												Matrix4x4 worldToLocalMatrix = ((Transform)0).worldToLocalMatrix;
												_ = worldToLocalMatrix.m02;
												float m = worldToLocalMatrix.m03;
												_ = worldToLocalMatrix.m03;
												bool flag13 = (object)transform == null;
												text2 = null;
												if (!flag13)
												{
													Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
													matrix4x4 = (Matrix4x4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 16));
													matrix4x3 = (Matrix4x4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 80));
													_ = localToWorldMatrix.m00;
													obj = localToWorldMatrix.m01;
													_ = localToWorldMatrix.m02;
													_ = localToWorldMatrix.m03;
													_ = worldToLocalMatrix.m00;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-78]");
													m = 0f;
													_ = worldToLocalMatrix.m01;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-68]");
													float num = 0f;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-78]");
													_ = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-68]");
													_ = 0;
													Matrix4x4 matrix4x5 = matrix4x3 * matrix4x4;
													bool flag14 = array2 == null;
													nint num2 = unchecked((nint)null);
													text2 = (string)matrix4x4;
													if (!flag14)
													{
														bool flag15 = (nint)ex2 >= array2.Length;
														num2 = unchecked((nint)null);
														text = (string)matrix4x4;
														if (!flag15)
														{
															obj4 = matrix4x5.m00;
															_ = matrix4x5.m01;
															m = matrix4x5.m02;
															_ = matrix4x5.m02;
															num = matrix4x5.m03;
															_ = matrix4x5.m03;
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9AD10");
															bool flag16 = array3 == null;
															num2 = unchecked((nint)null);
															text2 = (string)matrix4x4;
															if (flag16)
															{
																goto IL_093a;
															}
															bool flag17 = ex6 == null;
															matrix4x3 = (Matrix4x4)0;
															if (!flag17)
															{
																nint num3 = (nint)array3;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1181 @ rdx_v63 (Il2CppClass<UnityEngine.Material[][]>)+40]");
																matrix4x3 = (Matrix4x4)0;
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
																bool flag18 = obj12 == null;
																num2 = unchecked((nint)null);
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1181 @ rdx_v63 (Il2CppClass<UnityEngine.Material[][]>)+40]");
																Matrix4x4 matrix4x6 = (Matrix4x4)0;
																ArgumentNullException ex7 = ex6;
																if (flag18)
																{
																	break;
																}
															}
															bool flag19 = (nint)ex2 >= array3.Length;
															num2 = unchecked((nint)null);
															text = (string)matrix4x4;
															if (!flag19)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+B8]");
																obj6 = 0;
																ex2 = (ArgumentNullException)(ex2 + 1);
																obj7 += 8;
																obj4 += 64;
																num2 = unchecked((nint)null);
																matrix4x3 = (Matrix4x4)ex6;
																ex = ex2;
																continue;
															}
														}
														goto IL_08fa;
													}
												}
											}
										}
									}
								}
							}
							goto IL_093a;
						}
						object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 176));
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						string message4 = $"The renderer at index {arg4} is null.";
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						ArgumentException ex8 = new ArgumentException(message4, "renderers");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						throw ex8;
					}
					_ = 0;
					if (array != null)
					{
						if (array2 != null)
						{
							bool flag20 = array3 == null;
							string text = (string)matrix4x4;
							if (!flag20)
							{
								return CombineMeshes(array, array2, array3, null, out resultMaterials2, out resultBones);
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
							ArgumentNullException ex9 = new ArgumentNullException("materials");
							ex9._002Ector("materials");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
							throw ex9;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						ArgumentNullException ex10 = new ArgumentNullException("transforms");
						ex10._002Ector("transforms");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						throw ex10;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					ArgumentNullException ex11 = new ArgumentNullException("meshes");
					ex11._002Ector("meshes");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex11;
					IL_08fa:
					throw new IndexOutOfRangeException();
					IL_093a:
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				object obj14 = default(object);
				throw obj14;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentNullException ex12 = new ArgumentNullException("renderers");
			ex12._002Ector("renderers");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex12;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex13 = new ArgumentNullException("rootTransform");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex13;
	}

	public unsafe static Mesh CombineMeshes(Transform rootTransform, SkinnedMeshRenderer[] renderers, out Material[] resultMaterials, out Transform[] resultBones)
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00f0: Expected O, but got I4
		//IL_013d: Expected O, but got I
		//IL_019f: Expected O, but got I
		//IL_01ea: Expected O, but got I
		//IL_0241: Expected O, but got I4
		//IL_0260: Expected O, but got I
		//IL_0275: Expected O, but got I
		//IL_033e: Expected O, but got Ref
		//IL_033e: Expected O, but got Ref
		//IL_0373: Expected O, but got Ref
		//IL_03ae: Expected O, but got Ref
		//IL_03b6: Expected O, but got Ref
		//IL_03d1: Expected O, but got F4
		//IL_0437: Expected O, but got Ref
		//IL_04f3: Expected O, but got Ref
		//IL_046f: Expected I, but got O
		//IL_047f: Expected O, but got I
		//IL_04b5: Expected O, but got I
		//IL_04bd: Expected O, but got Ref
		//IL_0517: Expected O, but got I
		//IL_0544: Expected O, but got Ref
		//IL_0600: Expected O, but got Ref
		//IL_057c: Expected I, but got O
		//IL_058c: Expected O, but got I
		//IL_05c2: Expected O, but got I
		//IL_05ca: Expected O, but got Ref
		//IL_061c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Expected O, but got Unknown
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Expected O, but got Unknown
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Expected O, but got Unknown
		//IL_066c: Expected O, but got Ref
		if (rootTransform != null)
		{
			bool flag = renderers == null;
			UnityEngine.Object obj = null;
			string text = null;
			if (!flag)
			{
				Mesh[] array = new Mesh[renderers.Length];
				Matrix4x4[] array2 = new Matrix4x4[renderers.Length];
				Material[][] array3 = new Material[renderers.Length][];
				Transform[][] array4 = new Transform[renderers.Length][];
				object obj2 = renderers + 24;
				object obj3 = (object)array3 - (object)array;
				object obj4 = array2 + 32;
				object obj5 = (object)renderers - (object)array;
				object obj6 = array + 32;
				UnityEngine.Object obj7 = (UnityEngine.Object)((object)array4 - (object)array);
				obj = obj7;
				Matrix4x4 matrix4x = (Matrix4x4)0;
				ArgumentNullException ex = null;
				ArgumentNullException ex2 = null;
				object arg = default(object);
				object arg2 = default(object);
				float m2 = default(float);
				float m3 = default(float);
				ArgumentNullException ex6 = default(ArgumentNullException);
				object obj8 = default(object);
				Transform[] array6 = default(Transform[]);
				object obj10 = default(object);
				object arg3 = default(object);
				ref Material[] resultMaterials2 = default(ref Material[]);
				ref Transform[] resultBones2 = default(ref Transform[]);
				while (true)
				{
					if (System.Runtime.CompilerServices.Unsafe.As<ArgumentNullException, UIntPtr>(ref ex) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
					{
						bool flag2 = System.Runtime.CompilerServices.Unsafe.As<ArgumentNullException, UIntPtr>(ref ex2) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2);
						text = (string)matrix4x;
						if (flag2)
						{
							goto IL_0853;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ rsi_v11+v318 @ rdi_v14]");
						bool flag3 = (UnityEngine.Object)0 != null;
						Matrix4x4[] array5 = array2;
						string text2 = null;
						if (flag3)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ rsi_v11+v318 @ rdi_v14]");
							bool flag4 = (nint)0 == 0;
							array5 = array2;
							text2 = null;
							if (!flag4)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ rsi_v11+v318 @ rdi_v14]");
								Mesh sharedMesh = ((SkinnedMeshRenderer)0).sharedMesh;
								bool flag5 = sharedMesh != null;
								ArgumentNullException ex3 = (ArgumentNullException)(object)array2;
								string text3 = null;
								string text4 = (string)(object)ex2;
								if (!flag5)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									string message = $"The renderer at index {arg} has no mesh.";
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
									ArgumentException ex4 = new ArgumentException(message, "renderers");
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
									throw ex4;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ rsi_v11+v318 @ rdi_v14]");
								Mesh sharedMesh2 = ((SkinnedMeshRenderer)0).sharedMesh;
								bool flag6 = (object)sharedMesh2 == null;
								array5 = array2;
								text2 = null;
								if (!flag6)
								{
									bool isReadable = sharedMesh2.isReadable;
									bool flag7 = !isReadable;
									array5 = array2;
									Matrix4x4 matrix4x2 = (Matrix4x4)0;
									if (flag7)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										string message2 = $"The mesh in the renderer at index {arg2} is not readable.";
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
										ArgumentException ex5 = new ArgumentException(message2, "renderers");
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
										throw ex5;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ rsi_v11+v318 @ rdi_v14]");
									Transform transform = ((Component)0).transform;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ rsi_v11+v318 @ rdi_v14]");
									Mesh sharedMesh3 = ((SkinnedMeshRenderer)0).sharedMesh;
									bool flag8 = array == null;
									array5 = array2;
									text2 = null;
									if (!flag8)
									{
										bool flag9 = (nint)ex2 >= array.Length;
										obj = null;
										text = null;
										if (flag9)
										{
											goto IL_0853;
										}
										obj6 = sharedMesh3;
										bool flag10 = (object)transform == null;
										array5 = array2;
										text2 = null;
										if (!flag10)
										{
											Matrix4x4 worldToLocalMatrix = transform.worldToLocalMatrix;
											float m = worldToLocalMatrix.m02;
											Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
											Matrix4x4 matrix4x3 = (Matrix4x4)(&m2) * (Matrix4x4)(&m3);
											bool flag11 = array2 == null;
											float m4 = worldToLocalMatrix.m03;
											ref Transform[] reference = ref *(Transform[]*)null;
											array5 = array2;
											text2 = (string)(&m3);
											if (!flag11)
											{
												bool flag12 = (nint)ex2 >= array2.Length;
												m4 = worldToLocalMatrix.m03;
												reference = ref *(Transform[]*)null;
												obj = (UnityEngine.Object)(&m2);
												text = (string)(&m3);
												if (!flag12)
												{
													obj4 = matrix4x3.m00;
													_ = matrix4x3.m01;
													m = matrix4x3.m02;
													_ = matrix4x3.m02;
													m4 = matrix4x3.m03;
													_ = matrix4x3.m03;
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9AD10");
													bool flag13 = array3 == null;
													reference = ref *(Transform[]*)null;
													array5 = array2;
													text2 = (string)(&m3);
													if (flag13)
													{
														goto IL_0880;
													}
													bool flag14 = ex6 == null;
													obj = null;
													if (!flag14)
													{
														nint num = (nint)array3;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1102 @ rdx_v57 (Il2CppClass<UnityEngine.Material[][]>)+40]");
														obj = (UnityEngine.Object)0;
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
														bool flag15 = obj8 == null;
														reference = ref *(Transform[]*)null;
														array5 = array2;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1102 @ rdx_v57 (Il2CppClass<UnityEngine.Material[][]>)+40]");
														UnityEngine.Object obj9 = (UnityEngine.Object)0;
														matrix4x2 = (Matrix4x4)(&m3);
														ArgumentNullException ex7 = ex6;
														if (flag15)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
															throw array6;
														}
													}
													bool flag16 = (nint)ex2 >= array3.Length;
													reference = ref *(Transform[]*)null;
													text = (string)(&m3);
													if (!flag16)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ rsi_v11+v318 @ rdi_v14]");
														Transform[] bones = ((SkinnedMeshRenderer)0).bones;
														bool flag17 = array4 == null;
														reference = ref *(Transform[]*)null;
														array5 = array2;
														text2 = (string)(&m3);
														if (flag17)
														{
															goto IL_0880;
														}
														bool flag18 = bones == null;
														obj = null;
														if (!flag18)
														{
															nint num2 = (nint)array4;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1128 @ rdx_v55 (Il2CppClass<UnityEngine.Transform[][]>)+40]");
															obj = (UnityEngine.Object)0;
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
															bool flag19 = obj10 == null;
															reference = ref *(Transform[]*)null;
															array5 = array2;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1128 @ rdx_v55 (Il2CppClass<UnityEngine.Transform[][]>)+40]");
															UnityEngine.Object obj11 = (UnityEngine.Object)0;
															matrix4x2 = (Matrix4x4)(&m3);
															Transform[] array7 = bones;
															if (flag19)
															{
																break;
															}
														}
														bool flag20 = (nint)ex2 >= array4.Length;
														reference = ref *(Transform[]*)null;
														text = (string)(&m3);
														if (!flag20)
														{
															ex2 = (ArgumentNullException)(ex2 + 1);
															obj6 += 8;
															obj4 += 64;
															m2 = worldToLocalMatrix.m00;
															m3 = localToWorldMatrix.m00;
															reference = ref *(Transform[]*)null;
															obj = (UnityEngine.Object)(object)bones;
															matrix4x = (Matrix4x4)(&m3);
															ex = ex2;
															continue;
														}
													}
												}
												goto IL_0853;
											}
										}
									}
								}
							}
							goto IL_0880;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						string message3 = $"The renderer at index {arg3} is null.";
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						ArgumentException ex8 = new ArgumentException(message3, "renderers");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						throw ex8;
					}
					return CombineMeshes(array, array2, array3, array4, out resultMaterials2, out resultBones2);
					IL_0853:
					throw new IndexOutOfRangeException();
					IL_0880:
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				object obj12 = default(object);
				throw obj12;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentNullException ex9 = new ArgumentNullException("renderers");
			ex9._002Ector("renderers");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex9;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex10 = new ArgumentNullException("rootTransform");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex10;
	}

	public static Mesh CombineMeshes(Mesh[] meshes, Matrix4x4[] transforms, Material[][] materials, out Material[] resultMaterials)
	{
		if (meshes != null)
		{
			Matrix4x4[] array = default(Matrix4x4[]);
			if (array != null)
			{
				Material[][] array2 = default(Material[][]);
				ref Material[] resultMaterials2 = default(ref Material[]);
				ref Transform[] resultBones = default(ref Transform[]);
				if (array2 != null)
				{
					return CombineMeshes(meshes, array, array2, null, out resultMaterials2, out resultBones);
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				ArgumentNullException ex = new ArgumentNullException("materials");
				ex._002Ector("materials");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				throw ex;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentNullException ex2 = new ArgumentNullException("transforms");
			ex2._002Ector("transforms");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex3 = new ArgumentNullException("meshes");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex3;
	}

	public unsafe static Mesh CombineMeshes(Mesh[] meshes, Matrix4x4[] transforms, Material[][] materials, Transform[][] bones, out Material[] resultMaterials, out Transform[] resultBones)
	{
		//IL_0008: Expected O, but got Ref
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0182: Expected O, but got I
		//IL_0199: Expected O, but got I
		//IL_0581: Expected O, but got I
		//IL_0591: Expected O, but got I
		//IL_05a7: Expected O, but got I
		//IL_05bd: Expected O, but got I
		//IL_05da: Expected O, but got I
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Expected O, but got Unknown
		//IL_0614: Expected O, but got I
		//IL_0630: Expected O, but got I4
		//IL_0639: Expected O, but got I4
		//IL_0642: Expected O, but got I4
		//IL_1d7b: Expected O, but got Ref
		//IL_01f0: Expected O, but got I
		//IL_1dbb: Expected O, but got Ref
		//IL_1667: Expected O, but got I
		//IL_1685: Expected O, but got I
		//IL_0223: Expected O, but got I
		//IL_0245: Expected O, but got I
		//IL_1b76: Expected I4, but got O
		//IL_025b: Expected O, but got I4
		//IL_026b: Expected O, but got I
		//IL_16d6: Expected O, but got I
		//IL_1def: Expected O, but got Ref
		//IL_072a: Expected O, but got I
		//IL_02d7: Expected O, but got I
		//IL_1702: Expected O, but got I
		//IL_1e23: Expected O, but got Ref
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Expected O, but got Unknown
		//IL_172e: Expected O, but got I
		//IL_07a6: Expected O, but got I
		//IL_07bb: Expected O, but got I
		//IL_0786: Expected O, but got I
		//IL_03c1: Expected O, but got I
		//IL_2242: Expected O, but got I
		//IL_175a: Expected O, but got I
		//IL_07d5: Expected O, but got I
		//IL_07ef: Expected O, but got I
		//IL_1ea4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea9: Expected O, but got Unknown
		//IL_1eb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eb7: Expected O, but got Unknown
		//IL_1ec7: Expected O, but got I
		//IL_1ed7: Expected I4, but got O
		//IL_179b: Expected O, but got I
		//IL_080e: Expected O, but got I
		//IL_1786: Expected O, but got I
		//IL_1790: Expected O, but got I
		//IL_082d: Expected O, but got I
		//IL_084a: Expected O, but got I
		//IL_0869: Expected O, but got I
		//IL_0883: Expected O, but got I
		//IL_0417: Expected O, but got I
		//IL_0427: Expected O, but got I
		//IL_17b1: Expected O, but got I
		//IL_17c5: Expected O, but got I
		//IL_1eea: Expected O, but got Ref
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Expected O, but got Unknown
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Expected O, but got Unknown
		//IL_1fdc: Expected O, but got I
		//IL_1fff: Expected O, but got I
		//IL_2022: Expected O, but got I
		//IL_046a: Expected O, but got I
		//IL_1e56: Expected O, but got Ref
		//IL_0f58: Expected O, but got I
		//IL_0f58: Expected O, but got I
		//IL_0f66: Expected O, but got Ref
		//IL_22a0: Expected O, but got I
		//IL_0f8e: Expected O, but got Ref
		//IL_0fb3: Expected O, but got Ref
		//IL_050c: Expected O, but got I
		//IL_0fdd: Expected O, but got Ref
		//IL_0ffc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1001: Expected O, but got Unknown
		//IL_1021: Expected O, but got I4
		//IL_103a: Expected O, but got I4
		//IL_104b: Expected O, but got I4
		//IL_1846: Expected O, but got I
		//IL_20e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ea: Expected O, but got Unknown
		//IL_20f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f8: Expected O, but got Unknown
		//IL_210e: Expected O, but got I
		//IL_211e: Expected O, but got I
		//IL_2134: Expected O, but got I
		//IL_2144: Expected O, but got I
		//IL_2154: Expected O, but got I
		//IL_2164: Expected O, but got I
		//IL_2174: Expected O, but got I
		//IL_2184: Expected O, but got I
		//IL_109a: Expected O, but got I
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Expected O, but got Unknown
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Expected O, but got Unknown
		//IL_1223: Expected O, but got I
		//IL_1239: Expected O, but got I
		//IL_1259: Expected O, but got I4
		//IL_1f1e: Expected O, but got Ref
		//IL_12f1: Expected O, but got I
		//IL_130f: Expected O, but got I4
		//IL_1409: Expected O, but got I4
		//IL_117f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1184: Expected O, but got Unknown
		//IL_1194: Unknown result type (might be due to invalid IL or missing references)
		//IL_1199: Expected O, but got Unknown
		//IL_11a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a7: Expected O, but got Unknown
		//IL_11ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_11bf: Expected O, but got Unknown
		//IL_11c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cd: Expected O, but got Unknown
		//IL_11de: Expected O, but got I4
		//IL_2044: Unknown result type (might be due to invalid IL or missing references)
		//IL_2049: Expected O, but got Unknown
		//IL_2052: Unknown result type (might be due to invalid IL or missing references)
		//IL_2057: Expected O, but got Unknown
		//IL_2060: Unknown result type (might be due to invalid IL or missing references)
		//IL_2065: Expected O, but got Unknown
		//IL_207b: Expected O, but got I4
		//IL_1326: Unknown result type (might be due to invalid IL or missing references)
		//IL_132b: Expected O, but got Unknown
		//IL_1350: Expected O, but got I4
		//IL_1361: Expected O, but got I4
		//IL_1430: Expected O, but got I
		//IL_0f19: Expected O, but got I
		//IL_0f2e: Expected O, but got I
		//IL_0a0a: Expected O, but got I
		//IL_154e: Expected O, but got I
		//IL_155e: Expected O, but got Ref
		//IL_145f: Expected O, but got I
		//IL_146f: Expected O, but got Ref
		//IL_157a: Expected O, but got Ref
		//IL_1599: Expected O, but got I
		//IL_13a0: Expected O, but got I4
		//IL_15b9: Expected O, but got I
		//IL_15c1: Expected O, but got I4
		//IL_14ae: Expected O, but got I
		//IL_14d3: Expected O, but got I
		//IL_14e3: Expected O, but got I4
		//IL_15ec: Expected O, but got I
		//IL_15f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_15fa: Expected O, but got Unknown
		//IL_1603: Unknown result type (might be due to invalid IL or missing references)
		//IL_1608: Expected O, but got Unknown
		//IL_13c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c9: Expected O, but got Unknown
		//IL_13d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d7: Expected O, but got Unknown
		//IL_0a8a: Expected I, but got O
		//IL_0a90: Expected O, but got I
		//IL_1506: Expected O, but got I
		//IL_0ccc: Expected O, but got I
		//IL_151e: Expected O, but got I
		//IL_152c: Expected O, but got I
		//IL_0aae: Expected O, but got I
		//IL_0ad5: Expected O, but got I
		//IL_0cff: Expected O, but got I
		//IL_0d20: Expected O, but got I
		//IL_0d26: Expected O, but got I
		//IL_0afb: Expected O, but got I
		//IL_0b0b: Expected O, but got I
		//IL_0b1b: Expected O, but got I
		//IL_0b40: Expected O, but got I
		//IL_0d51: Expected O, but got I
		//IL_0b5c: Expected O, but got Ref
		//IL_0b74: Expected O, but got Ref
		//IL_0b82: Expected O, but got Ref
		//IL_0d6d: Expected O, but got Ref
		//IL_0dce: Expected O, but got I
		//IL_0df6: Expected O, but got I
		//IL_0dfc: Expected O, but got I
		//IL_0c3b: Expected O, but got I
		//IL_0c4b: Expected O, but got I
		//IL_0c5b: Expected O, but got I
		//IL_0c89: Expected O, but got I
		//IL_0e11: Expected O, but got I
		//IL_0e87: Expected O, but got I
		//IL_0e90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e95: Expected O, but got Unknown
		//IL_0e9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea3: Expected O, but got Unknown
		//IL_0eab: Expected O, but got I4
		//IL_0eb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb9: Expected O, but got Unknown
		//IL_0ec2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec7: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		Mesh[] array = default(Mesh[]);
		if (array != null)
		{
			bool flag = transforms == null;
			int[] array2 = null;
			if (!flag)
			{
				Material[][] array3 = default(Material[][]);
				bool flag2 = array3 == null;
				int[] array4 = null;
				string text = (string)(object)array3;
				if (!flag2)
				{
					bool flag3 = transforms.Length != array.Length;
					int[] array5 = null;
					string text2 = (string)(object)array3;
					if (!flag3)
					{
						bool flag4 = array3.Length != array.Length;
						int[] array6 = null;
						if (!flag4)
						{
							Transform[][] array7 = default(Transform[][]);
							if (array7 != null)
							{
								bool flag5 = array7.Length != array.Length;
								int[] array8 = null;
								if (flag5)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
									ArgumentException ex = new ArgumentException("The array of bones doesn't have the same length as the array of meshes.", "bones");
									ex._002Ector("The array of bones doesn't have the same length as the array of meshes.", "bones");
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
									throw ex;
								}
							}
							object obj3 = array3 + 32;
							object obj4 = (object)array - (object)array3;
							UnityEngine.Object obj5 = (UnityEngine.Object)((object)array7 - (object)array3);
							array6 = null;
							int[] array9 = null;
							int num = 0;
							int[] array10 = null;
							int num2 = 0;
							Transform[][] array11 = array7;
							object arg = default(object);
							object arg2 = default(object);
							object arg3 = default(object);
							object arg4 = default(object);
							object arg5 = default(object);
							object arg6 = default(object);
							object arg7 = default(object);
							object obj35 = default(object);
							object obj36 = default(object);
							object obj37 = default(object);
							object obj38 = default(object);
							Color[] colors2 = default(Color[]);
							BoneWeight[] boneWeights2 = default(BoneWeight[]);
							List<Vector2>[] uvs2D = default(List<Vector2>[]);
							List<Vector3>[] uvs3D = default(List<Vector3>[]);
							while (true)
							{
								int[] array12;
								if ((nint)array10 < array.Length)
								{
									if ((nint)array9 < array.Length)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ r12_v18+v548 @ r13_v18]");
										UnityEngine.Object obj6 = (UnityEngine.Object)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ r12_v18+v548 @ r13_v18]");
										bool flag6 = (UnityEngine.Object)0 != null;
										string text3 = null;
										if (!flag6)
										{
											object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 544));
											Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
											string text4 = $"The mesh at index {arg} is null.";
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
											ArgumentException ex2 = new ArgumentException(text4, "meshes");
											Transform transform = (Transform)((List<Matrix4x4>)(object)text4).get_Item((int)"meshes");
											throw ex2;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ r12_v18+v548 @ r13_v18]");
										bool flag7 = (nint)0 == 0;
										array6 = null;
										array3 = null;
										if (flag7)
										{
											goto IL_1da1;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ r12_v18+v548 @ r13_v18]");
										bool isReadable = ((Mesh)0).isReadable;
										bool flag8 = !isReadable;
										string text5 = null;
										if (flag8)
										{
											object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 544));
											Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
											string message = $"The mesh at index {arg2} is not readable.";
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
											ArgumentException ex3 = new ArgumentException(message, "meshes");
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
											throw ex3;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ r12_v18+v548 @ r13_v18]");
										int vertexCount = ((Mesh)0).vertexCount;
										num += vertexCount;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ r12_v18+v548 @ r13_v18]");
										int subMeshCount = ((Mesh)0).subMeshCount;
										array12 = (int[])(num2 + subMeshCount);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+230]");
										object obj9 = 0;
										int[] array13 = array9;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v896 @ rax_v257+18]");
										bool flag9 = (nint)array13 >= 0;
										array6 = null;
										array3 = null;
										if (!flag9)
										{
											UnityEngine.Object obj10 = (UnityEngine.Object)obj3;
											bool flag10 = obj3 == null;
											string text6 = null;
											if (flag10)
											{
												object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 544));
												Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
												string message2 = $"The materials for mesh at index {arg3} is null.";
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
												ArgumentException ex4 = new ArgumentException(message2, "materials");
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
												throw ex4;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ r12_v18+v548 @ r13_v18]");
											int subMeshCount2 = ((Mesh)0).subMeshCount;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1969 @ r14_v5 (UnityEngine.Object)+18]");
											bool flag11 = (nint)0 != subMeshCount2;
											string text7 = null;
											if (flag11)
											{
												object obj12 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 544));
												Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
												throw new NullReferenceException();
											}
											object obj13 = obj3 + 32;
											array6 = null;
											array3 = null;
											string text8 = null;
											while (true)
											{
												string text9 = text8;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1969 @ r14_v5 (UnityEngine.Object)+18]");
												if ((nint)text9 >= 0)
												{
													break;
												}
												string text10 = text8;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1969 @ r14_v5 (UnityEngine.Object)+18]");
												if ((nint)text10 < 0)
												{
													obj5 = (UnityEngine.Object)obj13;
													bool flag12 = (UnityEngine.Object)obj13 != null;
													int[] array14 = array12;
													string text11 = null;
													if (flag12)
													{
														text8++;
														obj13 += 8;
														array6 = null;
														array3 = null;
														continue;
													}
													object obj14 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 544));
													Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
													string message3 = $"The material at index {arg4} for mesh at index {arg5} is null.";
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
													ArgumentException ex5 = new ArgumentException(message3, "materials");
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
													throw ex5;
												}
												goto IL_1d61;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+238]");
											array11 = (Transform[][])0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+238]");
											if ((nint)0 == 0)
											{
												goto IL_1e9b;
											}
											if ((nint)array9 < array11.Length)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-70]");
												object obj15 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ r12_v18+v2290 @ rax_v262]");
												obj5 = (UnityEngine.Object)0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ r12_v18+v2290 @ rax_v262]");
												bool flag13 = (nint)0 == 0;
												string text12 = (string)(object)array3;
												if (flag13)
												{
													break;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ r12_v18+v2290 @ rax_v262]");
												object obj16 = (nint)0 + (nint)32;
												string text13 = null;
												while (true)
												{
													string text14 = text13;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v606 @ rsi_v46 (UnityEngine.Object)+18]");
													if ((nint)text14 >= 0)
													{
														break;
													}
													string text15 = text13;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v606 @ rsi_v46 (UnityEngine.Object)+18]");
													if ((nint)text15 < 0)
													{
														obj10 = (UnityEngine.Object)obj16;
														bool flag14 = (UnityEngine.Object)obj16 != null;
														int[] array14 = array12;
														string text16 = null;
														if (flag14)
														{
															text13++;
															obj16 += 8;
															array6 = null;
															array3 = null;
															continue;
														}
														object obj17 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 544));
														Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
														Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
														string message4 = $"The bone at index {arg6} for mesh at index {arg7} is null.";
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
														ArgumentException ex6 = new ArgumentException(message4, "meshBones");
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
														throw ex6;
													}
													goto IL_1d61;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+238]");
												array11 = (Transform[][])0;
												goto IL_1e9b;
											}
										}
									}
								}
								else
								{
									List<Vector3> list = new List<Vector3>(num);
									List<int[]> list2 = new List<int[]>(num2);
									_ = 0;
									_ = 0;
									_ = 0;
									_ = 0;
									List<Vector4>[] array15 = new List<Vector4>[MeshUtils.UVChannelCount];
									_ = 0;
									List<Material> list3 = new List<Material>(num2);
									Dictionary<Material, int> dictionary = new Dictionary<Material, int>(num2);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+230]");
									object obj18 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+228]");
									object obj19 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+220]");
									object obj20 = (nint)0 + (nint)24;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+230]");
									array3 = (Material[][])((nint)0 + (nint)32);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+220]");
									nint num3 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+230]");
									array7 = (Transform[][])(num3 - 0);
									Transform[][] array16 = array11;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+230]");
									object obj21 = array16 - 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+228]");
									array6 = (int[])((nint)0 + (nint)32);
									int value = 0;
									List<Transform> list4 = null;
									Material[][] array17 = null;
									object obj22 = 0;
									object obj23 = 0;
									object obj24 = 0;
									while (true)
									{
										int vertexCount2;
										int num12;
										int num13;
										int num4;
										if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj24) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj20))
										{
											if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj23) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj20))
											{
												break;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ r8_v72 (UnityEngine.Material[][])+v797 @ r9_v50 (UnityEngine.Transform[][])]");
											_ = 0;
											object obj25 = obj23;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v871 @ rsi_v22+18]");
											if ((nint)obj25 >= 0)
											{
												break;
											}
											num4 = array6[0];
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v814 @ rdx_v154 (System.Int32[])+10]");
											_ = 0;
											int num5 = array6[4];
											_ = array6[0];
											_ = array6[4];
											object obj26 = obj23;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v793 @ r11_v11+18]");
											if ((nint)obj26 >= 0)
											{
												break;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+238]");
											object obj27 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+238]");
											Material[] array18;
											if ((nint)0 != 0)
											{
												object obj28 = obj23;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v912 @ rcx_v160+18]");
												if ((nint)obj28 >= 0)
												{
													break;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ r8_v72 (UnityEngine.Material[][])+v940 @ rdi_v35]");
												array18 = (Material[])0;
											}
											else
											{
												array18 = null;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ r8_v72 (UnityEngine.Material[][])+v797 @ r9_v50 (UnityEngine.Transform[][])]");
											if ((nint)0 != 0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ r8_v72 (UnityEngine.Material[][])+v797 @ r9_v50 (UnityEngine.Transform[][])]");
												int subMeshCount3 = ((Mesh)0).subMeshCount;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ r8_v72 (UnityEngine.Material[][])+v797 @ r9_v50 (UnityEngine.Transform[][])]");
												vertexCount2 = ((Mesh)0).vertexCount;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ r8_v72 (UnityEngine.Material[][])+v797 @ r9_v50 (UnityEngine.Transform[][])]");
												Vector3[] vertices = ((Mesh)0).vertices;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ r8_v72 (UnityEngine.Material[][])+v797 @ r9_v50 (UnityEngine.Transform[][])]");
												Vector3[] normals = ((Mesh)0).normals;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ r8_v72 (UnityEngine.Material[][])+v797 @ r9_v50 (UnityEngine.Transform[][])]");
												Vector4[] tangents = ((Mesh)0).tangents;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ r8_v72 (UnityEngine.Material[][])+v797 @ r9_v50 (UnityEngine.Transform[][])]");
												IList<Vector4>[] meshUVs = MeshUtils.GetMeshUVs((Mesh)0);
												obj = meshUVs;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ r8_v72 (UnityEngine.Material[][])+v797 @ r9_v50 (UnityEngine.Transform[][])]");
												Color[] colors = ((Mesh)0).colors;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ r8_v72 (UnityEngine.Material[][])+v797 @ r9_v50 (UnityEngine.Transform[][])]");
												BoneWeight[] boneWeights = ((Mesh)0).boneWeights;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ r8_v72 (UnityEngine.Material[][])+v797 @ r9_v50 (UnityEngine.Transform[][])]");
												Matrix4x4[] bindposes = ((Mesh)0).bindposes;
												bool flag15 = array18 == null;
												IList<Vector4>[] array19 = meshUVs;
												BoneWeight[] array20 = boneWeights;
												if (!flag15)
												{
													bool flag16 = boneWeights == null;
													array19 = meshUVs;
													array20 = boneWeights;
													if (!flag16)
													{
														bool flag17 = boneWeights.Length == 0;
														array19 = meshUVs;
														array20 = boneWeights;
														if (!flag17)
														{
															bool flag18 = bindposes == null;
															array19 = meshUVs;
															array20 = boneWeights;
															if (!flag18)
															{
																bool flag19 = bindposes.Length == 0;
																array19 = meshUVs;
																array20 = boneWeights;
																if (!flag19)
																{
																	bool flag20 = array18.Length != bindposes.Length;
																	array19 = meshUVs;
																	array20 = boneWeights;
																	if (!flag20)
																	{
																		List<Transform> list7;
																		if (obj22 == null)
																		{
																			List<Matrix4x4> list5 = new List<Matrix4x4>(bindposes);
																			List<Transform> list6 = new List<Transform>((IEnumerable<Transform>)(object)array18);
																			list4 = list6;
																			list7 = list6;
																			array3 = (Material[][])0;
																		}
																		else
																		{
																			list7 = list4;
																		}
																		int[] array21 = new int[array18.Length];
																		object obj29 = array18 + 32;
																		object obj30 = bindposes + 32;
																		object obj31 = array21 + 32;
																		array6 = array21;
																		object obj32 = 0;
																		while ((nint)obj32 < array18.Length)
																		{
																			if ((nint)obj32 >= array18.Length)
																			{
																				goto end_IL_22d3;
																			}
																			object obj40;
																			object obj41;
																			object obj42;
																			object obj43;
																			int num8;
																			int num9;
																			int num10;
																			if (list7 != null)
																			{
																				Transform transform2 = (Transform)obj29;
																				int num6 = list7.IndexOf((Transform)obj29);
																				bool flag21 = num6 == -1;
																				nint num7 = (nint)array7;
																				string text17 = (string)0;
																				if (!flag21)
																				{
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-78]");
																					object obj33 = 0;
																					object obj34 = obj32;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v901 @ rax_v242+18]");
																					bool flag22 = (nint)obj34 >= 0;
																					array6 = (int[])obj29;
																					array3 = (Material[][])0;
																					if (flag22)
																					{
																						goto end_IL_22d3;
																					}
																					obj35 = obj30;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rbx_v56+10]");
																					obj36 = 0;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rbx_v56+20]");
																					obj37 = 0;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rbx_v56+30]");
																					obj38 = 0;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-80]");
																					bool flag23 = (nint)0 == 0;
																					array6 = (int[])obj29;
																					array3 = (Material[][])0;
																					if (flag23)
																					{
																						goto IL_1da1;
																					}
																					object obj39 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 96));
																					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
																					Matrix4x4 matrix4x = (Matrix4x4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 160));
																					Matrix4x4 matrix4x2 = (Matrix4x4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 96));
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+60]");
																					_ = 0;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+80]");
																					num4 = 0;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+70]");
																					_ = 0;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+90]");
																					num5 = 0;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+80]");
																					_ = 0;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+90]");
																					_ = 0;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rbx_v56+10]");
																					_ = 0;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rbx_v56+20]");
																					_ = 0;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rbx_v56+30]");
																					_ = 0;
																					bool flag24 = matrix4x2 == matrix4x;
																					num7 = 0;
																					transform2 = (Transform)matrix4x;
																					text17 = null;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rbx_v56+30]");
																					obj40 = 0;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rbx_v56+20]");
																					obj41 = 0;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rbx_v56+10]");
																					obj42 = 0;
																					obj43 = obj30;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+90]");
																					num8 = 0;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+80]");
																					num9 = 0;
																					array7 = (Transform[][])0;
																					num10 = num6;
																					array3 = null;
																					if (flag24)
																					{
																						goto IL_0e01;
																					}
																				}
																				num10 = list4._size;
																				bool flag25 = (nint)obj32 >= array18.Length;
																				array7 = (Transform[][])num7;
																				if (!flag25)
																				{
																					transform2 = (Transform)obj29;
																					list4.Add((Transform)obj29);
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-78]");
																					object obj44 = 0;
																					object obj45 = obj32;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2236 @ rax_v239+18]");
																					bool flag26 = (nint)obj45 >= 0;
																					array7 = (Transform[][])num7;
																					text17 = (string)0;
																					if (!flag26)
																					{
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-80]");
																						bool flag27 = (nint)0 == 0;
																						text17 = (string)0;
																						if (!flag27)
																						{
																							Matrix4x4 item = (Matrix4x4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 160));
																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rbx_v56+20]");
																							num9 = 0;
																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rbx_v56+10]");
																							_ = 0;
																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rbx_v56+30]");
																							num8 = 0;
																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rbx_v56+20]");
																							_ = 0;
																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rbx_v56+30]");
																							_ = 0;
																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-80]");
																							((List<Matrix4x4>)0).Add(item);
																							obj40 = obj38;
																							obj41 = obj37;
																							obj42 = obj36;
																							obj43 = obj35;
																							array7 = (Transform[][])num7;
																							array3 = (Material[][])0;
																							goto IL_0e01;
																						}
																						throw new NullReferenceException();
																					}
																				}
																				throw new IndexOutOfRangeException();
																			}
																			goto IL_1da1;
																			IL_0e01:
																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-10]");
																			array6 = (int[])0;
																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-10]");
																			bool flag28 = (nint)0 == 0;
																			num5 = num8;
																			num4 = num9;
																			if (!flag28)
																			{
																				bool flag29 = (nint)obj32 >= array6.Length;
																				num5 = num8;
																				num4 = num9;
																				if (flag29)
																				{
																					goto end_IL_22d3;
																				}
																				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-48]");
																				object obj46 = 0;
																				obj32++;
																				obj29 += 8;
																				obj46 = num10;
																				object obj47 = obj46 + 4;
																				obj30 += 64;
																				obj38 = obj40;
																				obj37 = obj41;
																				obj36 = obj42;
																				obj35 = obj43;
																				num5 = num8;
																				num4 = num9;
																				list7 = list4;
																				continue;
																			}
																			goto IL_1da1;
																		}
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-8]");
																		array20 = (BoneWeight[])0;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-8]");
																		RemapBones((BoneWeight[])0, array6);
																		array19 = (IList<Vector4>[])obj;
																	}
																}
															}
														}
													}
												}
												ref Matrix4x4 transform3 = ref System.Runtime.CompilerServices.Unsafe.As<object, Matrix4x4>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 272));
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+8]");
												TransformVertices((Vector3[])0, ref transform3);
												ref Matrix4x4 transform4 = ref System.Runtime.CompilerServices.Unsafe.As<object, Matrix4x4>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 272));
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+10]");
												TransformNormals((Vector3[])0, ref transform4);
												ref Matrix4x4 transform5 = ref System.Runtime.CompilerServices.Unsafe.As<object, Matrix4x4>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 272));
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+18]");
												TransformTangents((Vector4[])0, ref transform5);
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-20]");
												nint num11 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+8]");
												CopyVertexPositions((ICollection<Vector3>)num11, (Vector3[])0);
												object obj48 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 104));
												_ = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807264F0");
												object obj49 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 96));
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206E40]");
												_ = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807264F0");
												object obj50 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 88));
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
												_ = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807264F0");
												object obj51 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 80));
												_ = 0;
												_ = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807264F0");
												object obj52 = array19 + 32;
												bool flag30 = array19 == null;
												num12 = 0;
												Transform[][] array22 = (Transform[][])vertexCount2;
												int[] array23 = (int[])(object)array20;
												Material[][] array24 = array17;
												object obj53 = 0;
												num4 = 0;
												array7 = (Transform[][])vertexCount2;
												array6 = (int[])(object)array20;
												array3 = array17;
												if (!flag30)
												{
													while ((nint)obj53 < array19.Length)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-18]");
														object obj54 = 0;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-18]");
														bool flag31 = (nint)0 == 0;
														num4 = num12;
														array7 = array22;
														array6 = array23;
														array3 = array24;
														if (!flag31)
														{
															bool flag32 = (nint)obj53 >= array19.Length;
															num4 = num12;
															array7 = array22;
															array6 = array23;
															array3 = array24;
															if (flag32)
															{
																goto end_IL_22d3;
															}
															object obj55 = obj53;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v916 @ rcx_v198+18]");
															bool flag33 = (nint)obj55 >= 0;
															num4 = 0;
															array7 = array22;
															array6 = array23;
															array3 = array24;
															if (!flag33)
															{
																array23 = (int[])obj52;
																_ = 0;
																object obj56 = obj53 * 8;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-18]");
																object obj57 = 0 + obj56;
																object obj58 = obj57 + 32;
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807264F0");
																obj53++;
																obj52 += 8;
																num12 = 0;
																array22 = (Transform[][])vertexCount2;
																array24 = array17;
																continue;
															}
															throw new IndexOutOfRangeException();
														}
														goto IL_1da1;
													}
													bool flag34 = subMeshCount3 <= 0;
													num13 = vertexCount2;
													if (flag34)
													{
														goto IL_20dd;
													}
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-38]");
													object obj59 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-38]");
													object obj60 = (nint)0 + (nint)32;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-38]");
													bool flag35 = (nint)0 == 0;
													object obj61 = 0;
													int num14 = 0;
													num4 = num12;
													array7 = array22;
													array6 = array23;
													array3 = array24;
													if (!flag35)
													{
														while (true)
														{
															int num15 = num14;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v904 @ rax_v209+18]");
															bool flag36 = (nint)num15 >= (nint)0;
															num4 = num12;
															array7 = array22;
															array6 = array23;
															array3 = array24;
															if (flag36)
															{
																break;
															}
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+28]");
															int[] triangles = ((Mesh)0).GetTriangles(num14, applyBaseVertex: true);
															bool flag37 = (nint)array17 <= 0;
															array6 = (int[])num14;
															if (!flag37)
															{
																array6 = (int[])(triangles + 32);
																bool flag38 = triangles == null;
																num4 = num12;
																array7 = null;
																array3 = (Material[][])1;
																int[] array25 = array6;
																object obj62 = 0;
																if (flag38)
																{
																	goto IL_1da1;
																}
																while (true)
																{
																	bool flag39 = (nint)obj62 >= triangles.Length;
																	array6 = array25;
																	if (flag39)
																	{
																		break;
																	}
																	bool flag40 = (nint)obj62 >= triangles.Length;
																	num4 = num12;
																	array7 = null;
																	array6 = array25;
																	array3 = (Material[][])1;
																	if (flag40)
																	{
																		goto end_IL_1290;
																	}
																	array25 = (int[])(object)((object)array25 + (object)array17);
																	obj62++;
																	array25 = (int[])(array25 + 4);
																}
															}
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-40]");
															bool flag41 = (nint)0 == 0;
															num4 = num12;
															array7 = null;
															array3 = (Material[][])1;
															if (!flag41)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-40]");
																if (!((Dictionary<Material, int>)0).TryGetValue((Material)obj60, out value))
																{
																	bool flag42 = list2 == null;
																	num4 = num12;
																	array7 = (Transform[][])0;
																	array6 = (int[])obj60;
																	array3 = (Material[][])(&value);
																	if (!flag42)
																	{
																		int num16 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 120));
																		_ = list2._size;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-40]");
																		((Dictionary<Material, int>)0).Add((Material)obj60, num16);
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-70]");
																		bool flag43 = (nint)0 == 0;
																		num4 = num12;
																		array7 = (Transform[][])0;
																		array6 = (int[])obj60;
																		array3 = (Material[][])num16;
																		if (!flag43)
																		{
																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-70]");
																			((List<Material>)0).Add((Material)obj60);
																			list2.Add(triangles);
																			array22 = (Transform[][])0;
																			array23 = triangles;
																			array24 = (Material[][])0;
																			goto IL_15ce;
																		}
																	}
																}
																else
																{
																	bool flag44 = list2 == null;
																	num4 = num12;
																	array7 = (Transform[][])0;
																	array6 = (int[])obj60;
																	array3 = (Material[][])(&value);
																	if (!flag44)
																	{
																		object obj63 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
																		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+30]");
																		int[] array26 = MergeArrays((int[])0, triangles);
																		list2.set_Item(value, array26);
																		array22 = (Transform[][])0;
																		array23 = (int[])value;
																		array24 = (Material[][])(object)array26;
																		goto IL_15ce;
																	}
																}
															}
															goto IL_1da1;
															IL_15ce:
															num14++;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-38]");
															obj59 = 0;
															obj61++;
															obj60 += 8;
															if ((nint)obj61 < subMeshCount3)
															{
																continue;
															}
															goto IL_1624;
															continue;
															end_IL_1290:
															break;
														}
														break;
													}
												}
											}
										}
										else
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-20]");
											if ((nint)0 != 0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-20]");
												Vector3[] array27 = ((List<Vector3>)0).ToArray();
												bool flag45 = list2 == null;
												array6 = (int[])0;
												if (!flag45)
												{
													int[][] indices = list2.ToArray();
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-68]");
													Vector3[] normals2;
													if ((nint)0 != 0)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-68]");
														Vector3[] array28 = ((List<Vector3>)0).ToArray();
														normals2 = array28;
													}
													else
													{
														normals2 = null;
													}
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-60]");
													Vector4[] tangents2;
													if ((nint)0 != 0)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-60]");
														Vector4[] array29 = ((List<Vector4>)0).ToArray();
														tangents2 = array29;
													}
													else
													{
														tangents2 = null;
													}
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-58]");
													Color[] uvs4D;
													if ((nint)0 != 0)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-58]");
														Color[] array30 = ((List<Color>)0).ToArray();
														uvs4D = array30;
													}
													else
													{
														uvs4D = null;
													}
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-50]");
													BoneWeight[] bindposes2;
													if ((nint)0 != 0)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-50]");
														BoneWeight[] array31 = ((List<BoneWeight>)0).ToArray();
														bindposes2 = array31;
													}
													else
													{
														bindposes2 = null;
													}
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-18]");
													List<Vector4>[] array32 = Enumerable.ToArray((IEnumerable<List<Vector4>>)0);
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-80]");
													if ((nint)0 != 0)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-80]");
														Matrix4x4[] array33 = ((List<Matrix4x4>)0).ToArray();
														array6 = (int[])0;
													}
													else
													{
														array6 = (int[])0;
													}
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-70]");
													if ((nint)0 != 0)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-70]");
														Material[] array34 = ((List<Material>)0).ToArray();
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+240]");
														object obj64 = 0;
														obj64 = array34;
														Transform[] array35 = list4?.ToArray();
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+248]");
														object obj65 = 0;
														obj65 = array35;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+220]");
														return MeshUtils.CreateMesh((Vector3[])0, indices, normals2, tangents2, colors2, boneWeights2, uvs2D, uvs3D, (List<Vector4>[])(object)uvs4D, (Matrix4x4[])(object)bindposes2, null);
													}
												}
											}
										}
										goto IL_1da1;
										IL_1624:
										num13 = vertexCount2;
										goto IL_20dd;
										IL_20dd:
										array17 = (Material[][])(array17 + num13);
										obj23++;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-30]");
										array3 = (Material[][])((nint)0 + (nint)8);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-80]");
										obj22 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1-28]");
										array6 = (int[])((nint)0 + (nint)64);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+220]");
										array7 = (Transform[][])0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+38]");
										obj20 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+230]");
										obj18 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+40]");
										obj21 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+228]");
										obj19 = 0;
										num4 = num12;
										obj24 = obj23;
										continue;
										end_IL_22d3:
										break;
									}
								}
								goto IL_1d61;
								IL_1d61:
								throw new IndexOutOfRangeException();
								IL_1e9b:
								array9 = (int[])(array9 + 1);
								obj3 += 8;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+220]");
								array = (Mesh[])0;
								array10 = array9;
								num2 = (int)array12;
								continue;
								IL_1da1:
								throw new NullReferenceException();
							}
							object obj66 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 544));
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object arg8 = default(object);
							string message5 = $"The bones for mesh at index {arg8} is null.";
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
							ArgumentException ex7 = new ArgumentException(message5, "meshBones");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
							throw ex7;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						ArgumentException ex8 = new ArgumentException("The array of materials doesn't have the same length as the array of meshes.", "materials");
						ex8._002Ector("The array of materials doesn't have the same length as the array of meshes.", "materials");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						throw ex8;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					ArgumentException ex9 = new ArgumentException("The array of transforms doesn't have the same length as the array of meshes.", "transforms");
					ex9._002Ector("The array of transforms doesn't have the same length as the array of meshes.", "transforms");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex9;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				ArgumentNullException ex10 = new ArgumentNullException("materials");
				ex10._002Ector("materials");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				throw ex10;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentNullException ex11 = new ArgumentNullException("transforms");
			ex11._002Ector("transforms");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex11;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex12 = new ArgumentNullException("meshes");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex12;
	}

	private unsafe static void CopyVertexPositions(ICollection<Vector3> list, Vector3[] arr)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_003b: Expected O, but got I4
		//IL_0044: Expected O, but got I4
		//IL_0051: Expected I, but got O
		//IL_0160: Expected O, but got Ref
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_0089: Expected O, but got I
		//IL_0092: Expected O, but got I4
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		if (arr == null || arr.Length == 0)
		{
			return;
		}
		object obj = arr + 32;
		object obj2 = 0;
		object obj8 = default(object);
		for (object obj3 = 0; (nint)obj3 < arr.Length; list.Add((Vector3)(&obj8)), obj2++, obj += 12, obj3 = obj2)
		{
			nint num = (nint)list;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ r10_v3 (Il2CppClass<System.Collections.Generic.ICollection`1<UnityEngine.Vector3>>)+12E]");
			if ((nint)0 < (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ r10_v3 (Il2CppClass<System.Collections.Generic.ICollection`1<UnityEngine.Vector3>>)+B0]");
				object obj4 = 0;
				object obj5 = 0;
				while (true)
				{
					object obj6 = obj5 + obj5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v244 @ r8_v7+v249 @ rcx_v10*8]");
					if (0 != (nint)typeof(ICollection<Vector3>))
					{
						obj5++;
						object obj7 = obj5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ r10_v3 (Il2CppClass<System.Collections.Generic.ICollection`1<UnityEngine.Vector3>>)+12E]");
						if ((nint)obj7 < 0)
						{
							continue;
						}
						goto IL_00c9;
					}
					break;
				}
				continue;
			}
			goto IL_00c9;
			IL_00c9:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		}
	}

	private unsafe static void CopyVertexAttributes<T>(ref List<T> dest, IEnumerable<T> src, int previousVertexCount, int meshVertexCount, int totalVertexCount, T defaultValue)
	{
		//IL_0008: Expected O, but got Ref
		//IL_007e: Expected O, but got I
		//IL_008e: Expected O, but got I
		//IL_00a4: Expected O, but got I
		//IL_032a: Expected O, but got Ref
		//IL_00d3: Expected O, but got I
		//IL_0237: Expected O, but got I
		//IL_0133: Expected O, but got I
		//IL_028e: Expected O, but got I4
		//IL_014d: Expected O, but got I
		//IL_0433: Expected O, but got I
		//IL_0441: Expected O, but got Ref
		//IL_0451: Expected O, but got I
		//IL_0222: Expected O, but got I
		//IL_02b8: Expected O, but got I
		//IL_02c8: Expected O, but got I
		//IL_02de: Expected O, but got I
		//IL_02f8: Expected O, but got Ref
		//IL_02a3: Expected O, but got I
		//IL_018d: Expected O, but got I4
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Expected O, but got Unknown
		//IL_039a: Expected O, but got I
		//IL_03a8: Expected O, but got Ref
		//IL_03b8: Expected O, but got I
		//IL_01b7: Expected O, but got I
		//IL_01c7: Expected O, but got I
		//IL_01dd: Expected O, but got I
		//IL_01f7: Expected O, but got Ref
		//IL_01a2: Expected O, but got I
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+60]");
		ref List<T> reference = ref *(List<T>*)null;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1 (System.Collections.Generic.List`1<T>&)+38]");
		bool flag = (nint)0 != 0;
		ref List<T> reference2 = ref dest;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			reference2 = ref reference;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1 (System.Collections.Generic.List`1<T>&)+38]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rax_v2+18]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r10_v1+FC]");
		object obj5 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r10_v1+FC]");
		object obj6 = default(object);
		if ((nint)obj5 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			obj6 = (object)(&obj2);
			if (src == null)
			{
				goto IL_0247;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1 (System.Collections.Generic.List`1<T>&)+38]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806ADFA0");
		object obj8 = default(object);
		if (obj8 != null)
		{
			bool flag2 = dest != null;
			IEnumerable<T> enumerable = src;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1 (System.Collections.Generic.List`1<T>&)+38]");
				object obj9 = 0;
				object obj10 = null;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1 (System.Collections.Generic.List`1<T>&)+38]");
				object obj11 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6AD0");
				ref List<T> reference3 = ref *(List<T>*)obj10;
				if (previousVertexCount > 0)
				{
					object obj12 = 0;
					do
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1 (System.Collections.Generic.List`1<T>&)+38]");
						object obj13 = 0;
						object obj14 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 88));
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rax_v29+18]");
						object obj15 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v443 @ rcx_v19+28]");
						if ((nint)0 < (nint)0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+58]");
							obj14 = 0;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1 (System.Collections.Generic.List`1<T>&)+38]");
						object obj16 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v455 @ rax_v31+18]");
						object obj17 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v457 @ rcx_v21+28]");
						object obj18 = (nint)0 >> 31;
						bool flag3 = obj18 != null;
						object obj19 = (object)(&obj2);
						if (!flag3)
						{
							obj19 = obj6;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371280");
						obj12++;
					}
					while ((nint)obj12 < previousVertexCount);
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+38]");
				enumerable = (IEnumerable<T>)0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1 (System.Collections.Generic.List`1<T>&)+38]");
			object obj20 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A3850");
			return;
		}
		goto IL_0247;
		IL_0247:
		if (dest == null || meshVertexCount <= 0)
		{
			return;
		}
		object obj21 = 0;
		do
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1 (System.Collections.Generic.List`1<T>&)+38]");
			object obj22 = 0;
			object obj23 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 88));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v313 @ rax_v11+18]");
			object obj24 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ rcx_v5+28]");
			if ((nint)0 < (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+58]");
				obj23 = 0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1 (System.Collections.Generic.List`1<T>&)+38]");
			object obj25 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v375 @ rax_v13+18]");
			object obj26 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v377 @ rcx_v7+28]");
			object obj27 = (nint)0 >> 31;
			bool flag4 = obj27 != null;
			object obj28 = (object)(&obj2);
			if (!flag4)
			{
				obj28 = obj6;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371280");
			obj21++;
		}
		while ((nint)obj21 < meshVertexCount);
	}

	private static T[] MergeArrays<T>(T[] arr1, T[] arr2)
	{
		//IL_0050: Expected O, but got I4
		//IL_005a: Expected I, but got O
		//IL_0075: Expected O, but got I
		//IL_0096: Expected O, but got I
		//IL_009b: Expected O, but got I
		if (arr1 != null && arr2 != null)
		{
			object obj = arr1.Length + arr2.Length;
			nint num = unchecked((nint)null);
			int length = default(int);
			Array.Copy(arr1, 0, (Array)num, 0, length);
			Array.Copy(arr2, 0, (Array)num, arr1.Length, length);
			return (T[])num;
		}
		return (T[])(object)new NullReferenceException();
	}

	private static void TransformVertices(Vector3[] vertices, ref Matrix4x4 transform)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0017: Expected O, but got I4
		//IL_0020: Expected O, but got I4
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		//IL_0087: Expected O, but got I
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		object obj = vertices + 32;
		object obj2 = 0;
		object obj3 = 0;
		object obj5 = default(object);
		while ((nint)obj2 < vertices.Length)
		{
			obj3++;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [transform @ rdx (UnityEngine.Matrix4x4&)+18]");
			object obj4 = obj5 * 0;
			object obj6 = obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [transform @ rdx (UnityEngine.Matrix4x4&)+8]");
			object obj7 = obj6 * 0;
			object obj8 = obj4 + obj7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ r9_v3+8]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [transform @ rdx (UnityEngine.Matrix4x4&)+28]");
			object obj9 = num * 0;
			object obj10 = obj8 + obj9;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [transform @ rdx (UnityEngine.Matrix4x4&)+38]");
			object obj11 = obj10 + 0;
			obj = obj5;
			obj += 12;
			obj2 = obj3;
		}
	}

	private static void TransformNormals(Vector3[] normals, ref Matrix4x4 transform)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_0034: Expected O, but got I4
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_009b: Expected O, but got I
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		if (normals != null)
		{
			object obj = normals + 32;
			object obj2 = 0;
			object obj4 = default(object);
			while ((nint)obj2 < normals.Length)
			{
				obj2++;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [transform @ rdx (UnityEngine.Matrix4x4&)+18]");
				object obj3 = obj4 * 0;
				object obj5 = obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [transform @ rdx (UnityEngine.Matrix4x4&)+8]");
				object obj6 = obj5 * 0;
				object obj7 = obj3 + obj6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ r9_v3+8]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [transform @ rdx (UnityEngine.Matrix4x4&)+28]");
				object obj8 = num * 0;
				object obj9 = obj7 + obj8;
				obj = obj4;
				obj += 12;
			}
		}
	}

	private static void TransformTangents(Vector4[] tangents, ref Matrix4x4 transform)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_0034: Expected O, but got I4
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_007e: Expected O, but got I
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		if (tangents != null)
		{
			object obj = tangents + 40;
			object obj2 = 0;
			while ((nint)obj2 < tangents.Length)
			{
				obj2++;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ r8_v3-8]");
				object obj3 = 0 * transform;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ r8_v3-4]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [transform @ rdx (UnityEngine.Matrix4x4&)+10]");
				object obj4 = num * 0;
				object obj5 = obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [transform @ rdx (UnityEngine.Matrix4x4&)+20]");
				object obj6 = obj5 * 0;
				object obj7 = obj4 + obj3;
				object obj8 = obj7 + obj6;
				obj += 16;
			}
		}
	}

	private static void RemapBones(BoneWeight[] boneWeights, int[] boneIndices)
	{
		//IL_0009: Expected O, but got I4
		//IL_0012: Expected O, but got I4
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Expected O, but got Unknown
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Expected O, but got Unknown
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Expected O, but got Unknown
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Expected O, but got Unknown
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Expected O, but got Unknown
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Expected O, but got Unknown
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Expected O, but got Unknown
		object obj = 0;
		object obj2 = 0;
		object obj6 = default(object);
		object obj16 = default(object);
		object obj26 = default(object);
		object obj36 = default(object);
		while ((nint)obj < boneWeights.Length)
		{
			object obj3 = obj2 + 1;
			object obj4 = obj3 << 5;
			object obj5 = obj4 + (object)boneWeights;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
			if ((nint)obj6 > 0)
			{
				object obj7 = obj2 + 1;
				object obj8 = obj7 << 5;
				object obj9 = obj8 + (object)boneWeights;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B90");
				object obj10 = obj2 + 1;
				object obj11 = obj10 << 5;
				object obj12 = obj11 + (object)boneWeights;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803DB8C0");
			}
			object obj13 = obj2 + 1;
			object obj14 = obj13 << 5;
			object obj15 = obj14 + (object)boneWeights;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
			if ((nint)obj16 > 0)
			{
				object obj17 = obj2 + 1;
				object obj18 = obj17 << 5;
				object obj19 = obj18 + (object)boneWeights;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D7C520");
				object obj20 = obj2 + 1;
				object obj21 = obj20 << 5;
				object obj22 = obj21 + (object)boneWeights;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803DB8B0");
			}
			object obj23 = obj2 + 1;
			object obj24 = obj23 << 5;
			object obj25 = obj24 + (object)boneWeights;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B70");
			if ((nint)obj26 > 0)
			{
				object obj27 = obj2 + 1;
				object obj28 = obj27 << 5;
				object obj29 = obj28 + (object)boneWeights;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CF520");
				object obj30 = obj2 + 1;
				object obj31 = obj30 << 5;
				object obj32 = obj31 + (object)boneWeights;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180832B00");
			}
			object obj33 = obj2 + 1;
			object obj34 = obj33 << 5;
			object obj35 = obj34 + (object)boneWeights;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D4F790");
			if ((nint)obj36 > 0)
			{
				object obj37 = obj2 + 1;
				object obj38 = obj37 << 5;
				object obj39 = obj38 + (object)boneWeights;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808746F0");
				object obj40 = obj2 + 1;
				object obj41 = obj40 << 5;
				object obj42 = obj41 + (object)boneWeights;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D96C80");
			}
			obj2++;
			obj = obj2;
		}
	}

	private static bool CanReadMesh(Mesh mesh)
	{
		//IL_003d: Expected I4, but got O
		if ((object)mesh != null)
		{
			return mesh.isReadable;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}
}
