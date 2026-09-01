using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using VLB;

namespace VLB_Samples;

public class LightGenerator : MonoBehaviour
{
	private int CountX = 10;

	private int CountY = 10;

	private float OffsetUnits = 1f;

	private float PositionY = 1f;

	private bool NoiseEnabled;

	private bool AddLight = true;

	public unsafe void Generate()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0016: Expected O, but got I4
		//IL_0c00: Expected F4, but got I4
		//IL_0aba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abf: Expected O, but got Unknown
		//IL_02e4: Expected I, but got O
		//IL_02f4: Expected O, but got I
		//IL_00f4: Expected I, but got O
		//IL_0104: Expected O, but got I
		//IL_0383: Expected I, but got O
		//IL_0393: Expected O, but got I
		//IL_0193: Expected I, but got O
		//IL_01a3: Expected O, but got I
		//IL_0422: Expected I, but got O
		//IL_0432: Expected O, but got I
		//IL_04f7: Expected I4, but got I8
		//IL_0541: Expected O, but got Ref
		//IL_058c: Expected O, but got Ref
		//IL_05b5: Expected O, but got Ref
		//IL_05d0: Expected O, but got Ref
		//IL_05de: Expected O, but got Ref
		//IL_05fd: Expected O, but got I
		//IL_06f5: Expected O, but got I
		//IL_06fd: Expected O, but got Ref
		//IL_0669: Expected O, but got I
		//IL_0672: Expected O, but got I4
		//IL_0753: Expected O, but got Ref
		//IL_06c4: Expected O, but got I4
		//IL_07f9: Expected O, but got Ref
		//IL_0802: Expected O, but got I4
		//IL_099f: Expected I4, but got I8
		//IL_0835: Expected O, but got Ref
		//IL_083e: Expected O, but got I4
		//IL_09bf: Expected F4, but got I4
		//IL_09d2: Expected O, but got I4
		//IL_087e: Expected O, but got Ref
		//IL_0887: Expected O, but got I4
		//IL_09f5: Expected I4, but got F4
		//IL_0a19: Expected O, but got I
		//IL_0a3e: Expected F4, but got I4
		//IL_08cf: Expected O, but got I4
		//IL_0a64: Expected O, but got I
		//IL_0a75: Expected O, but got F4
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		if (CountX <= 0)
		{
			return;
		}
		object obj3 = 0;
		int num2 = default(int);
		float num4 = default(float);
		object obj4 = default(object);
		Type type = default(Type);
		object obj6 = default(object);
		Type type3 = default(Type);
		object obj8 = default(object);
		Type type5 = default(Type);
		object obj10 = default(object);
		Type type7 = default(Type);
		object obj12 = default(object);
		object obj13 = default(object);
		Vector3 euler = default(Vector3);
		float num18 = default(float);
		Light light = default(Light);
		float num21 = default(float);
		LightGenerator lightGenerator = default(LightGenerator);
		do
		{
			_ = 0;
			bool flag = CountY <= 0;
			float num = num2;
			float num3 = num4;
			if (!flag)
			{
				float num15;
				while (true)
				{
					int num5 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 144));
					Type[] array2;
					string text7;
					string text4;
					string text5;
					string text6;
					RuntimeTypeHandle typeFromHandle2;
					if (!AddLight)
					{
						string text = ((int*)num5)->ToString();
						int num6 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 128));
						string text2 = ((int*)num6)->ToString();
						string text3 = "Light_" + text + "_" + text2;
						Type[] array = new Type[2];
						Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(VolumetricLightBeamSD));
						bool flag2 = array == null;
						text4 = text2;
						text5 = "_";
						text6 = null;
						typeFromHandle2 = (RuntimeTypeHandle)typeof(VolumetricLightBeamSD);
						if (flag2)
						{
							goto IL_0a7d;
						}
						if ((object)typeFromHandle != null)
						{
							nint num7 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v848 @ rdx_v63 (Il2CppClass<System.Type[]>)+40]");
							text6 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							bool flag3 = obj4 == null;
							text4 = text2;
							text5 = "_";
							typeFromHandle2 = (RuntimeTypeHandle)typeFromHandle;
							if (flag3)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								throw type;
							}
						}
						array[0] = typeFromHandle;
						Type typeFromHandle3 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Rotater));
						if ((object)typeFromHandle3 != null)
						{
							nint num8 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1192 @ rdx_v61 (Il2CppClass<System.Type[]>)+40]");
							object obj5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							bool flag4 = obj6 == null;
							text4 = text2;
							text5 = "_";
							Type type2 = typeFromHandle3;
							if (flag4)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								throw type3;
							}
						}
						array[1] = typeFromHandle3;
						array2 = array;
						text7 = text3;
					}
					else
					{
						string text8 = ((int*)num5)->ToString();
						int num9 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 128));
						string text9 = ((int*)num9)->ToString();
						string text10 = "Light_" + text8 + "_" + text9;
						Type[] array3 = new Type[3];
						Type typeFromHandle4 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Light));
						bool flag5 = array3 == null;
						text4 = text9;
						text5 = "_";
						text6 = null;
						typeFromHandle2 = (RuntimeTypeHandle)typeof(Light);
						if (flag5)
						{
							goto IL_0a7d;
						}
						if ((object)typeFromHandle4 != null)
						{
							nint num10 = (nint)array3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ rdx_v51 (Il2CppClass<System.Type[]>)+40]");
							object obj7 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							bool flag6 = obj8 == null;
							text4 = text9;
							text5 = "_";
							Type type4 = typeFromHandle4;
							if (flag6)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								throw type5;
							}
						}
						array3[0] = typeFromHandle4;
						Type typeFromHandle5 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(VolumetricLightBeamSD));
						if ((object)typeFromHandle5 != null)
						{
							nint num11 = (nint)array3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1222 @ rdx_v49 (Il2CppClass<System.Type[]>)+40]");
							object obj9 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							bool flag7 = obj10 == null;
							text4 = text9;
							text5 = "_";
							Type type6 = typeFromHandle5;
							if (flag7)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								throw type7;
							}
						}
						array3[1] = typeFromHandle5;
						Type typeFromHandle6 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Rotater));
						if ((object)typeFromHandle6 != null)
						{
							nint num12 = (nint)array3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1349 @ rdx_v47 (Il2CppClass<System.Type[]>)+40]");
							object obj11 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							bool flag8 = obj12 == null;
							text4 = text9;
							text5 = "_";
							Type type8 = typeFromHandle6;
							if (flag8)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								throw obj13;
							}
						}
						array3[2] = typeFromHandle6;
						array2 = array3;
						text7 = text10;
					}
					GameObject gameObject = new GameObject(text7, array2);
					bool flag9 = (object)gameObject == null;
					text4 = null;
					text5 = (string)(object)array2;
					text6 = text7;
					typeFromHandle2 = (RuntimeTypeHandle)gameObject;
					if (!flag9)
					{
						Transform transform = gameObject.transform;
						int num13 = UnityEngine.Random.Range(-45, 45);
						float num14 = (float)num13 + 90f;
						num15 = num14 * ((float)Math.PI / 180f);
						int num16 = UnityEngine.Random.Range(0, 360);
						typeFromHandle2 = (RuntimeTypeHandle)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 112));
						float num17 = (float)num16 * ((float)Math.PI / 180f);
						Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
						bool flag10 = (object)transform == null;
						num3 = num17;
						text4 = null;
						text5 = null;
						text6 = (string)(&euler);
						if (!flag10)
						{
							num3 = quaternion.x;
							Quaternion rotation = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
							_ = quaternion.x;
							transform.SetPositionAndRotation((Vector3)(&num18), rotation);
							text6 = (string)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 152));
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+98]");
							object obj14 = 0;
							if (!AddLight)
							{
								float value = UnityEngine.Random.value;
								float value2 = UnityEngine.Random.value;
								num3 = UnityEngine.Random.value;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+98]");
								bool flag11 = (nint)0 == 0;
								num = num4;
								text4 = null;
								text5 = (string)0;
								typeFromHandle2 = (RuntimeTypeHandle)0;
								if (!flag11)
								{
									float num19 = UnityEngine.Random.Range(3f, 8f);
									float num20 = UnityEngine.Random.Range(10f, 90f);
									typeFromHandle2 = (RuntimeTypeHandle)0;
									goto IL_08dc;
								}
							}
							else
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
								bool flag12 = (object)light == null;
								num = num4;
								text4 = null;
								text5 = (string)0;
								text6 = (string)(&light);
								typeFromHandle2 = (RuntimeTypeHandle)gameObject;
								if (!flag12)
								{
									light.type = LightType.Spot;
									float value3 = UnityEngine.Random.value;
									float value4 = UnityEngine.Random.value;
									float value5 = UnityEngine.Random.value;
									light.color = (Color)(&num21);
									float range = UnityEngine.Random.Range(3f, 8f);
									light.range = range;
									float intensity = UnityEngine.Random.Range(0.2f, 5f);
									light.intensity = intensity;
									num3 = (light.spotAngle = UnityEngine.Random.Range(10f, 90f));
									Config instance = Config.Instance;
									bool flag13 = (object)instance == null;
									num = num3;
									text4 = null;
									text5 = null;
									text6 = (string)(&num21);
									typeFromHandle2 = (RuntimeTypeHandle)0;
									if (!flag13)
									{
										bool flag14 = !instance.geometryOverrideLayer;
										num21 = value3;
										text6 = (string)(&num21);
										typeFromHandle2 = (RuntimeTypeHandle)0;
										if (!flag14)
										{
											Config instance2 = Config.Instance;
											bool flag15 = (object)instance2 == null;
											num = num3;
											text4 = null;
											text5 = null;
											text6 = (string)(&num21);
											typeFromHandle2 = (RuntimeTypeHandle)0;
											if (flag15)
											{
												goto IL_0a7d;
											}
											int num23 = 1 << instance2.geometryLayerID;
											int num24 = (light.cullingMask = ~num23);
											num21 = value3;
											text6 = (string)num24;
											typeFromHandle2 = (RuntimeTypeHandle)light;
										}
										goto IL_08dc;
									}
								}
							}
						}
					}
					goto IL_0a7d;
					IL_08dc:
					num3 = UnityEngine.Random.Range(0f, 0.1f);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+98]");
					bool flag16 = (nint)0 == 0;
					num = 0.1f;
					text4 = null;
					text5 = null;
					if (!flag16)
					{
						int num26 = UnityEngine.Random.Range(12, 36);
						num3 = UnityEngine.Random.Range(1f, 7.5f);
						bool flag17 = !NoiseEnabled;
						bool flag18 = !flag17;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
						num2 = UnityEngine.Random.Range(-500, 500);
						bool flag19 = (object)lightGenerator == null;
						num = num2;
						text4 = null;
						text5 = null;
						text6 = (string)500;
						typeFromHandle2 = (RuntimeTypeHandle)lightGenerator;
						if (!flag19)
						{
							lightGenerator.CountX = (int)num4;
							lightGenerator.OffsetUnits = 0f;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+80]");
							object obj15 = (nint)0 + (nint)1;
							bool flag20 = (nint)obj15 < CountY;
							num18 = num4;
							num = num2;
							num3 = num4;
							if (!flag20)
							{
								break;
							}
							continue;
						}
					}
					goto IL_0a7d;
					IL_0a7d:
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+90]");
				obj3 = 0;
				num18 = num4;
				euler = (Vector3)num15;
			}
			obj3++;
		}
		while ((nint)obj3 < CountX);
	}
}
