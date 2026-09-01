using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

public class TMPTextBaker : MonoBehaviour
{
	public bool skipLocText = true;

	public bool includeInactive;

	public bool only3DText;

	public string bakedChildName = "BakedText";

	public bool verboseLogging;

	private bool baked;

	private List<GameObject> disabledOriginals;

	private GameObject bakedChild;

	public bool IsBaked => baked;

	public unsafe void Bake()
	{
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00de: Expected O, but got I4
		//IL_0103: Expected O, but got I4
		//IL_04ac: Expected I, but got O
		//IL_0149: Expected I, but got O
		//IL_014e: Expected I, but got O
		//IL_0178: Expected I, but got O
		//IL_017d: Expected I, but got O
		//IL_2c2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c2f: Expected O, but got Unknown
		//IL_2c38: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c3d: Expected O, but got Unknown
		//IL_01ca: Expected I, but got O
		//IL_01d2: Expected I, but got O
		//IL_01fd: Expected I, but got O
		//IL_0205: Expected I, but got O
		//IL_056d: Expected O, but got I4
		//IL_0223: Expected I, but got O
		//IL_0231: Expected I, but got O
		//IL_0241: Expected O, but got I
		//IL_026d: Expected I, but got O
		//IL_0354: Expected I, but got O
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Expected O, but got Unknown
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Expected O, but got Unknown
		//IL_042d: Expected I, but got O
		//IL_0293: Expected O, but got I
		//IL_02c8: Expected I, but got O
		//IL_02d8: Expected O, but got I
		//IL_02e6: Expected I, but got O
		//IL_02f6: Expected O, but got I
		//IL_2e69: Expected O, but got Ref
		//IL_05aa: Expected I, but got O
		//IL_038c: Expected I, but got O
		//IL_0391: Expected I, but got O
		//IL_039e: Expected I, but got O
		//IL_03a3: Expected I, but got O
		//IL_05cc: Expected I, but got O
		//IL_1989: Expected O, but got I
		//IL_05ef: Expected I, but got O
		//IL_062a: Expected I, but got O
		//IL_2e96: Expected I, but got O
		//IL_2e9e: Expected I, but got O
		//IL_19b4: Expected O, but got I
		//IL_0651: Expected I, but got O
		//IL_19df: Expected I, but got O
		//IL_19e7: Expected I, but got O
		//IL_19f7: Expected O, but got I
		//IL_0680: Expected I, but got O
		//IL_1a16: Expected O, but got I
		//IL_1a2e: Expected I, but got O
		//IL_1a33: Expected I, but got O
		//IL_1a43: Expected O, but got I
		//IL_06c5: Expected I, but got O
		//IL_06ca: Expected I, but got O
		//IL_06d2: Expected O, but got Ref
		//IL_1a7f: Expected I, but got O
		//IL_1a87: Expected I, but got O
		//IL_1a97: Expected O, but got I
		//IL_2e0f: Expected O, but got I
		//IL_1ab6: Expected O, but got I
		//IL_1aca: Expected O, but got I
		//IL_2ec8: Expected I, but got O
		//IL_2ecd: Expected I, but got O
		//IL_0717: Expected O, but got I
		//IL_1add: Expected O, but got Ref
		//IL_1af9: Expected I, but got O
		//IL_1b11: Expected O, but got I
		//IL_1b30: Expected O, but got I
		//IL_2eef: Expected I, but got O
		//IL_2ef4: Expected I, but got O
		//IL_0779: Expected O, but got I
		//IL_0781: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Expected O, but got Unknown
		//IL_0793: Expected I, but got O
		//IL_07a3: Expected F4, but got I
		//IL_07b4: Expected O, but got I
		//IL_07c4: Expected O, but got I
		//IL_07eb: Expected O, but got I
		//IL_1b46: Expected O, but got Ref
		//IL_1b62: Expected I, but got O
		//IL_1b7a: Expected O, but got I
		//IL_0817: Expected O, but got I
		//IL_0827: Expected O, but got I
		//IL_0837: Expected O, but got I
		//IL_0856: Expected O, but got I
		//IL_1b99: Expected O, but got I
		//IL_0898: Expected O, but got I
		//IL_2f24: Expected I, but got O
		//IL_2f29: Expected I, but got O
		//IL_08c3: Expected O, but got I
		//IL_08d3: Expected F4, but got I
		//IL_08e3: Expected O, but got I
		//IL_08f3: Expected O, but got I
		//IL_0903: Expected O, but got I
		//IL_0913: Expected O, but got I
		//IL_1baf: Expected O, but got Ref
		//IL_2cc7: Expected O, but got I
		//IL_1bd5: Expected I, but got O
		//IL_1bda: Expected I, but got O
		//IL_1c19: Expected I, but got O
		//IL_1c1e: Expected I, but got O
		//IL_099b: Expected O, but got I8
		//IL_09a8: Expected O, but got I
		//IL_1c49: Expected O, but got I
		//IL_1c6c: Expected I, but got O
		//IL_1c71: Expected I, but got O
		//IL_0a75: Invalid comparison between F4 and I4
		//IL_0a83: Expected O, but got I
		//IL_1545: Expected O, but got I4
		//IL_1cf1: Expected I, but got O
		//IL_1cf9: Expected I, but got O
		//IL_0ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ace: Expected O, but got Unknown
		//IL_2cf0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cf5: Expected O, but got Unknown
		//IL_0ae7: Expected O, but got F4
		//IL_1d26: Expected I, but got O
		//IL_1d33: Expected I, but got O
		//IL_1d38: Expected I, but got O
		//IL_1d52: Expected I, but got O
		//IL_0b0d: Expected I, but got O
		//IL_0b12: Expected I, but got O
		//IL_0b1a: Expected O, but got F4
		//IL_15a7: Expected I, but got O
		//IL_148c: Expected I, but got O
		//IL_1491: Expected I, but got O
		//IL_1499: Expected O, but got F4
		//IL_0a67: Expected O, but got I
		//IL_2383: Expected I, but got O
		//IL_2393: Expected O, but got I
		//IL_1630: Expected O, but got I
		//IL_2a77: Expected I, but got O
		//IL_0b35: Expected O, but got F4
		//IL_0b61: Expected I, but got F4
		//IL_23b2: Expected O, but got I
		//IL_23db: Expected O, but got I
		//IL_0b84: Expected O, but got F4
		//IL_14d5: Expected I, but got O
		//IL_14ee: Expected I, but got O
		//IL_1d9c: Expected O, but got I4
		//IL_1da9: Expected O, but got I8
		//IL_166c: Expected I, but got O
		//IL_0c07: Expected I, but got O
		//IL_0c30: Expected O, but got I4
		//IL_2412: Expected I, but got O
		//IL_241a: Expected I, but got O
		//IL_242a: Expected O, but got I
		//IL_1716: Expected O, but got I
		//IL_1505: Unknown result type (might be due to invalid IL or missing references)
		//IL_150a: Expected O, but got Unknown
		//IL_0bf2: Expected I, but got O
		//IL_2449: Expected O, but got I
		//IL_0c56: Expected I, but got F4
		//IL_2473: Expected I, but got O
		//IL_1f0a: Expected O, but got I4
		//IL_1f18: Expected I, but got O
		//IL_1752: Expected I, but got O
		//IL_2f84: Expected O, but got I
		//IL_1ec7: Expected I, but got O
		//IL_2f4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f53: Expected O, but got Unknown
		//IL_17f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_17fa: Expected O, but got Unknown
		//IL_0c6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c72: Expected O, but got Unknown
		//IL_0d1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1f: Expected O, but got Unknown
		//IL_0d3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d41: Expected O, but got Unknown
		//IL_0d78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7d: Expected O, but got Unknown
		//IL_0d9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9f: Expected O, but got Unknown
		//IL_0e2e: Expected O, but got Ref
		//IL_0e2e: Expected O, but got I
		//IL_0e5b: Expected O, but got I
		//IL_24b5: Expected I, but got O
		//IL_24bd: Expected I, but got O
		//IL_24cd: Expected O, but got I
		//IL_1f3f: Expected O, but got I4
		//IL_1197: Expected O, but got I
		//IL_11a0: Expected O, but got I4
		//IL_2fb3: Expected I, but got O
		//IL_24ec: Expected O, but got I
		//IL_1f6c: Expected I, but got O
		//IL_1f7c: Expected O, but got I
		//IL_18e4: Expected O, but got I4
		//IL_1299: Expected O, but got I
		//IL_2fce: Expected I4, but got O
		//IL_1862: Expected I, but got O
		//IL_1433: Expected O, but got I
		//IL_0ebf: Expected O, but got I
		//IL_2023: Expected O, but got Ref
		//IL_1940: Expected I, but got O
		//IL_11f4: Expected O, but got I
		//IL_0f13: Expected O, but got Ref
		//IL_0ed5: Expected F4, but got O
		//IL_0ee5: Expected O, but got I
		//IL_2039: Expected I, but got O
		//IL_2081: Expected O, but got I
		//IL_2089: Expected I, but got O
		//IL_2533: Expected I, but got O
		//IL_1314: Invalid comparison between F4 and I4
		//IL_1322: Expected I, but got O
		//IL_132b: Expected I, but got O
		//IL_1333: Expected O, but got F4
		//IL_122e: Expected O, but got I
		//IL_1244: Unknown result type (might be due to invalid IL or missing references)
		//IL_1249: Expected O, but got Unknown
		//IL_2119: Expected O, but got Ref
		//IL_26c7: Expected I, but got O
		//IL_26d7: Expected O, but got I
		//IL_2555: Expected I, but got O
		//IL_134a: Expected O, but got F4
		//IL_30db: Expected I4, but got O
		//IL_212f: Expected I, but got O
		//IL_2177: Expected O, but got I
		//IL_217f: Expected I, but got O
		//IL_1375: Expected O, but got I
		//IL_1382: Expected O, but got Ref
		//IL_276d: Expected I, but got O
		//IL_2591: Expected I, but got O
		//IL_1002: Expected O, but got Ref
		//IL_0fb4: Expected O, but got I
		//IL_0fc4: Expected O, but got I
		//IL_2210: Expected O, but got Ref
		//IL_13eb: Expected I, but got O
		//IL_1408: Expected I, but got O
		//IL_2802: Expected I, but got O
		//IL_25c9: Expected I, but got O
		//IL_2226: Expected I, but got O
		//IL_226e: Expected O, but got I
		//IL_2276: Expected I, but got O
		//IL_10bb: Expected O, but got I8
		//IL_10c8: Expected O, but got I8
		//IL_2896: Expected I, but got O
		//IL_25f8: Expected I, but got O
		//IL_10da: Expected O, but got Ref
		//IL_10e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e8: Expected O, but got Unknown
		//IL_10f8: Expected F4, but got O
		//IL_1108: Expected I, but got O
		//IL_10a9: Expected O, but got I
		//IL_264f: Expected I, but got O
		//IL_2349: Expected I, but got O
		//IL_267f: Expected O, but got I
		if (baked)
		{
			if (verboseLogging)
			{
				Debug.Log("[TMPTextBaker] Already baked, performing Unbake first.");
			}
			Unbake();
		}
		TMP_Text[] componentsInChildren = GetComponentsInChildren<TMP_Text>(includeInactive);
		List<TMP_Text> list = new List<TMP_Text>();
		bool flag = componentsInChildren == null;
		nint num = 0;
		nint num2 = 0;
		List<TMP_Text> list2 = list;
		if (!flag)
		{
			object obj = componentsInChildren + 32;
			object obj2 = 0;
			List<TMP_Text> list3 = list;
			num = 0;
			num2 = 0;
			list2 = list;
			object obj3 = 0;
			List<TMP_Text>.Enumerator enumerator2 = default(List<TMP_Text>.Enumerator);
			List<TMP_Text>.Enumerator enumerator4 = default(List<TMP_Text>.Enumerator);
			List<TMP_Text>.Enumerator enumerator5 = default(List<TMP_Text>.Enumerator);
			UnityEngine.Object obj8 = default(UnityEngine.Object);
			List<TMP_Text>.Enumerator enumerator6 = default(List<TMP_Text>.Enumerator);
			object obj12 = default(object);
			List<TMP_Text>.Enumerator enumerator10 = default(List<TMP_Text>.Enumerator);
			float num24 = default(float);
			float num25 = default(float);
			List<TMP_Text>.Enumerator enumerator11 = default(List<TMP_Text>.Enumerator);
			List<TMP_Text>.Enumerator enumerator12 = default(List<TMP_Text>.Enumerator);
			List<TMP_Text>.Enumerator enumerator13 = default(List<TMP_Text>.Enumerator);
			UnityEngine.Object obj41 = default(UnityEngine.Object);
			List<int> list20 = default(List<int>);
			List<Color32> list23 = default(List<Color32>);
			object arg = default(object);
			object obj47 = default(object);
			object arg2 = default(object);
			object obj49 = default(object);
			object obj50 = default(object);
			List<List<int>> list31 = default(List<List<int>>);
			List<Color32> list32 = default(List<Color32>);
			List<List<int>> list37 = default(List<List<int>>);
			object obj51 = default(object);
			List<TMP_Text>.Enumerator enumerator14 = default(List<TMP_Text>.Enumerator);
			List<int> list45 = default(List<int>);
			List<List<int>> list48 = default(List<List<int>>);
			IntPtr intPtr2 = default(IntPtr);
			List<List<int>> list52 = default(List<List<int>>);
			List<List<int>> list56 = default(List<List<int>>);
			List<TMP_Text>.Enumerator enumerator15 = default(List<TMP_Text>.Enumerator);
			List<List<int>> list62 = default(List<List<int>>);
			List<List<int>> list66 = default(List<List<int>>);
			List<List<int>> list70 = default(List<List<int>>);
			object arg3 = default(object);
			UnityEngine.Object obj6 = default(UnityEngine.Object);
			while (true)
			{
				nint num3;
				nint num4;
				List<TMP_Text> list4;
				if ((nint)obj3 < componentsInChildren.Length)
				{
					if ((nint)obj2 < componentsInChildren.Length)
					{
						UnityEngine.Object obj4 = (UnityEngine.Object)obj;
						bool flag2 = (UnityEngine.Object)obj != null;
						num = unchecked((nint)null);
						num2 = unchecked((nint)null);
						list2 = (List<TMP_Text>)obj;
						if (!flag2)
						{
							goto IL_2c21;
						}
						bool flag3 = obj == null;
						num = unchecked((nint)null);
						num2 = unchecked((nint)null);
						list2 = (List<TMP_Text>)obj;
						if (flag3)
						{
							break;
						}
						GameObject gameObject = ((Component)obj).gameObject;
						GameObject gameObject2 = base.gameObject;
						bool flag4 = gameObject == gameObject2;
						num = unchecked((nint)null);
						num2 = (nint)gameObject2;
						list2 = (List<TMP_Text>)(object)gameObject;
						if (!flag4)
						{
							bool flag5 = only3DText == flag4;
							num3 = unchecked((nint)null);
							num4 = (nint)gameObject2;
							list4 = (List<TMP_Text>)(object)gameObject;
							if (flag5)
							{
								goto IL_0304;
							}
							num = (nint)obj4;
							nint num5 = (nint)typeof(TextMeshPro);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2896 @ rdx_v264 (Il2CppClass<TMPro.TextMeshPro>)+130]");
							List<TMP_Text> list5 = (List<TMP_Text>)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6249 @ r8_v113 (Il2CppMethodInfo)+130]");
							nint num6 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2896 @ rdx_v264 (Il2CppClass<TMPro.TextMeshPro>)+130]");
							bool flag6 = num6 < 0;
							num2 = (nint)typeof(TextMeshPro);
							list2 = (List<TMP_Text>)(object)gameObject;
							if (!flag6)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6249 @ r8_v113 (Il2CppMethodInfo)+C8]");
								object obj5 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2934 @ rax_v405+FFFFFFF8+v2933 @ rax_v404 (System.Collections.Generic.List`1<TMPro.TMP_Text>)*8]");
								bool flag7 = 0 != (nint)typeof(TextMeshPro);
								num3 = num;
								num4 = (nint)typeof(TextMeshPro);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2896 @ rdx_v264 (Il2CppClass<TMPro.TextMeshPro>)+130]");
								list4 = (List<TMP_Text>)0;
								num2 = (nint)typeof(TextMeshPro);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2896 @ rdx_v264 (Il2CppClass<TMPro.TextMeshPro>)+130]");
								list2 = (List<TMP_Text>)0;
								if (!flag7)
								{
									goto IL_0304;
								}
							}
						}
						goto IL_0442;
					}
					throw new IndexOutOfRangeException();
				}
				if (list3 == null)
				{
					break;
				}
				object message3;
				if (list3._size != 0)
				{
					Transform transform = base.transform;
					bool flag8 = (object)transform == null;
					num2 = unchecked((nint)null);
					list2 = (List<TMP_Text>)(object)this;
					if (flag8)
					{
						break;
					}
					Matrix4x4 worldToLocalMatrix = transform.worldToLocalMatrix;
					float m = worldToLocalMatrix.m02;
					float m2 = worldToLocalMatrix.m03;
					List<Material> list6 = new List<Material>();
					List<List<int>> list7 = new List<List<int>>();
					List<Vector3> list8 = new List<Vector3>();
					List<Vector2> list9 = new List<Vector2>();
					List<Vector2> list10 = new List<Vector2>();
					List<Color32> list11 = new List<Color32>();
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					List<TMP_Text>.Enumerator enumerator = enumerator2;
					List<TMP_Text>.Enumerator enumerator3 = enumerator4;
					obj6 = null;
					object obj7 = 0;
					List<Color32> list12 = list11;
					num = 0;
					List<List<int>> list13 = list7;
					while (enumerator5.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						bool flag9 = obj8 != null;
						num = unchecked((nint)null);
						if (!flag9)
						{
							continue;
						}
						bool flag10 = (object)obj8 == null;
						num = unchecked((nint)null);
						UnityEngine.Object obj9 = null;
						UnityEngine.Object obj10 = obj8;
						if (!flag10)
						{
							nint num7 = (nint)obj8;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4948 @ r9_v74 (Il2CppClass<UnityEngine.Object>)+7E0]");
							nint num8 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v4948 @ r9_v74 (Il2CppClass<UnityEngine.Object>)+7D8] (should have been resolved before IL gen)");
							TMP_TextInfo textInfo = ((TMP_Text)obj8).textInfo;
							bool flag11 = textInfo == null;
							num = unchecked((nint)null);
							if (flag11)
							{
								continue;
							}
							bool flag12 = textInfo.meshInfo == null;
							num = unchecked((nint)null);
							if (flag12)
							{
								continue;
							}
							Transform transform2 = ((TMP_Text)obj8).transform;
							bool flag13 = (object)transform2 == null;
							num = unchecked((nint)null);
							obj9 = null;
							obj10 = obj8;
							if (!flag13)
							{
								Matrix4x4 localToWorldMatrix = transform2.localToWorldMatrix;
								enumerator3 = enumerator6;
								UnityEngine.Object obj11 = obj8;
								TMP_TextInfo tMP_TextInfo = textInfo;
								num = unchecked((nint)null);
								nint num9 = unchecked((nint)null);
								obj10 = (UnityEngine.Object)(&obj12);
								while (true)
								{
									List<TMP_Text> meshInfo = (List<TMP_Text>)(object)tMP_TextInfo.meshInfo;
									bool flag14 = tMP_TextInfo.meshInfo == null;
									obj9 = (UnityEngine.Object)num9;
									if (!flag14)
									{
										if (num9 >= meshInfo._size)
										{
											break;
										}
										bool flag15 = tMP_TextInfo.meshInfo == null;
										obj9 = (UnityEngine.Object)num9;
										obj10 = (UnityEngine.Object)(object)tMP_TextInfo.meshInfo;
										if (!flag15)
										{
											bool flag16 = num9 >= meshInfo._size;
											num2 = num9;
											list2 = (List<TMP_Text>)(object)tMP_TextInfo.meshInfo;
											if (!flag16)
											{
												object obj13 = num9 * 4;
												object obj14 = num9 + obj13;
												num2 = (nint)(obj14 + obj14);
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+60+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
												m2 = 0f;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+40+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
												enumerator3 = (List<TMP_Text>.Enumerator)0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+50+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
												enumerator = (List<TMP_Text>.Enumerator)0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+60+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
												bool flag17 = (nint)0 == 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+20+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
												obj12 = 0;
												List<TMP_Text> list14 = (List<TMP_Text>)(object)tMP_TextInfo.meshInfo;
												if (!flag17)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+40+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
													enumerator3 = (List<TMP_Text>.Enumerator)0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+50+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
													enumerator = (List<TMP_Text>.Enumerator)0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+60+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
													object obj15 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+60+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
													bool flag18 = (nint)0 == 0;
													obj9 = (UnityEngine.Object)num2;
													obj10 = (UnityEngine.Object)(object)tMP_TextInfo.meshInfo;
													if (flag18)
													{
														throw new NullReferenceException();
													}
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7179 @ rax_v298+18]");
													bool flag19 = (nint)0 == 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+20+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
													obj12 = 0;
													list14 = (List<TMP_Text>)(object)tMP_TextInfo.meshInfo;
													if (!flag19)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+50+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
														enumerator = (List<TMP_Text>.Enumerator)0;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+30+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
														m = 0f;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+48+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
														object obj16 = 0;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+50+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
														list3 = (List<TMP_Text>)0;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+40+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
														enumerator3 = (List<TMP_Text>.Enumerator)0;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+58+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
														obj11 = (UnityEngine.Object)0;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+30+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
														bool flag20 = (nint)0 == 0;
														list14 = (List<TMP_Text>)(object)tMP_TextInfo.meshInfo;
														if (!flag20)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6235 @ xmm2_v64 (System.Single)+18]");
															bool flag21 = (nint)0 == 0;
															list14 = (List<TMP_Text>)(object)tMP_TextInfo.meshInfo;
															if (!flag21)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+60+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
																bool flag22 = (nint)0 == 0;
																list12 = (List<Color32>)4294967295L;
																list2 = null;
																obj9 = (UnityEngine.Object)num2;
																obj10 = null;
																if (flag22)
																{
																	throw new NullReferenceException();
																}
																while (true)
																{
																	List<TMP_Text> list15 = list2;
																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7179 @ rax_v298+18]");
																	if ((nint)list15 >= 0)
																	{
																		break;
																	}
																	List<TMP_Text> list16 = list2;
																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7179 @ rax_v298+18]");
																	if ((nint)list16 < 0)
																	{
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7179 @ rax_v298+20+v1741 @ rcx_v240 (System.Collections.Generic.List`1<TMPro.TMP_Text>)*4]");
																		if (0 > (nint)list12)
																		{
																			List<TMP_Text> list17 = list2;
																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7179 @ rax_v298+18]");
																			if ((nint)list17 >= 0)
																			{
																				throw new IndexOutOfRangeException();
																			}
																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7179 @ rax_v298+20+v1741 @ rcx_v240 (System.Collections.Generic.List`1<TMPro.TMP_Text>)*4]");
																			list12 = (List<Color32>)0;
																		}
																		list2 = (List<TMP_Text>)(list2 + 1);
																		continue;
																	}
																	throw new IndexOutOfRangeException();
																}
																bool flag23 = m == 0f;
																obj9 = (UnityEngine.Object)num2;
																obj10 = (UnityEngine.Object)(object)list2;
																if (flag23)
																{
																	throw new NullReferenceException();
																}
																List<Color32> list18 = list12;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6235 @ xmm2_v64 (System.Single)+18]");
																string text8;
																if ((nint)list18 < 0)
																{
																	object obj17 = list12 + 1;
																	Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm3,8\"");
																	if ((UnityEngine.Object)m2 != null)
																	{
																		bool flag24 = list6 == null;
																		num = unchecked((nint)null);
																		num2 = unchecked((nint)null);
																		list2 = (List<TMP_Text>)m2;
																		if (!flag24)
																		{
																			int num10 = list6.IndexOf((Material)m2);
																			bool flag25 = num10 >= 0;
																			int num11 = num10;
																			num = 0;
																			num2 = (nint)m2;
																			list2 = (List<TMP_Text>)(object)list6;
																			if (!flag25)
																			{
																				list6.Add((Material)m2);
																				List<int> list19 = new List<int>();
																				bool flag26 = list7 == null;
																				num = 0;
																				num2 = 0;
																				list2 = (List<TMP_Text>)(object)list7;
																				if (flag26)
																				{
																					throw new NullReferenceException();
																				}
																				list7.Add(list19);
																				num11 = list6._size - 1;
																				num = 0;
																				num2 = (nint)list19;
																				list2 = (List<TMP_Text>)(object)list7;
																			}
																			if (list8 != null)
																			{
																				nint num12 = (nint)list8;
																				float num13 = m2;
																				float num14 = m;
																				List<TMP_Text>.Enumerator enumerator7 = enumerator;
																				List<TMP_Text>.Enumerator enumerator8 = enumerator3;
																				object obj18 = 0;
																				while (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj18) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj17))
																				{
																					object obj19 = obj18;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6235 @ xmm2_v64 (System.Single)+18]");
																					bool flag27 = (nint)obj19 >= 0;
																					num2 = (nint)m;
																					if (!flag27)
																					{
																						object obj20 = obj18 * 2;
																						object obj21 = obj18 + obj20;
																						float num15 = (float)enumerator6 * localToWorldMatrix.m01;
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6235 @ xmm2_v64 (System.Single)+20+v8956 @ rcx_v258*4]");
																						float num16 = 0f * localToWorldMatrix.m00;
																						float num17 = num15 + num16;
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6235 @ xmm2_v64 (System.Single)+28+v8956 @ rcx_v258*4]");
																						float num18 = 0f * localToWorldMatrix.m02;
																						float num19 = num17 + num18;
																						float num20 = num19 + localToWorldMatrix.m03;
																						object obj22 = (object)enumerator6 * (object)enumerator6;
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6235 @ xmm2_v64 (System.Single)+20+v8956 @ rcx_v258*4]");
																						object obj23 = 0 * enumerator6;
																						object obj24 = obj22 + obj23;
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6235 @ xmm2_v64 (System.Single)+28+v8956 @ rcx_v258*4]");
																						object obj25 = 0 * enumerator6;
																						object obj26 = obj24 + obj25;
																						object obj27 = obj26 + (object)enumerator6;
																						object obj28 = (object)enumerator6 * (object)enumerator6;
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6235 @ xmm2_v64 (System.Single)+20+v8956 @ rcx_v258*4]");
																						object obj29 = 0 * enumerator6;
																						object obj30 = obj28 + obj29;
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6235 @ xmm2_v64 (System.Single)+28+v8956 @ rcx_v258*4]");
																						object obj31 = 0 * enumerator6;
																						object obj32 = obj30 + obj31;
																						object obj33 = obj32 + (object)enumerator6;
																						object obj34 = (object)enumerator6 * obj27;
																						float num21 = (float)enumerator6 * num20;
																						float num22 = (float)obj34 + num21;
																						object obj35 = (object)enumerator6 * obj33;
																						float num23 = num22 + (float)obj35;
																						num13 = num23 + (float)enumerator6;
																						List<TMP_Text>.Enumerator enumerator9 = (List<TMP_Text>.Enumerator)((object)enumerator6 * obj33);
																						((List<Vector3>)num12).Add((Vector3)(&enumerator10));
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+48+v6235 @ xmm2_v64 (System.Single)*8]");
																						bool flag28 = (nint)0 == 0;
																						num = 0;
																						num2 = (nint)(&enumerator10);
																						list2 = (List<TMP_Text>)num12;
																						if (!flag28)
																						{
																							object obj36 = obj18;
																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1341 @ r13_v67+18]");
																							if ((nint)obj36 < 0)
																							{
																								object obj37 = obj18;
																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1341 @ r13_v67+18]");
																								bool flag29 = (nint)obj37 >= 0;
																								num = 0;
																								num2 = (nint)(&enumerator10);
																								list2 = (List<TMP_Text>)num12;
																								if (flag29)
																								{
																									throw new IndexOutOfRangeException();
																								}
																								num14 = (float)enumerator6;
																								enumerator9 = enumerator6;
																								list2 = (List<TMP_Text>)num12;
																							}
																							else
																							{
																								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371660");
																								num14 = num24;
																								list2 = null;
																							}
																							bool flag30 = list9 == null;
																							num = 0;
																							num2 = (nint)(&enumerator10);
																							if (!flag30)
																							{
																								list9.Add((Vector2)(&num25));
																								bool flag31 = list3 == null;
																								num = 0;
																								num2 = (nint)(&num25);
																								list2 = (List<TMP_Text>)(object)list9;
																								if (!flag31)
																								{
																									if ((nint)obj18 < list3._size)
																									{
																										bool flag32 = (nint)obj18 >= list3._size;
																										num = 0;
																										num2 = (nint)(&num25);
																										list2 = (List<TMP_Text>)(object)list9;
																										if (flag32)
																										{
																											throw new IndexOutOfRangeException();
																										}
																										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6246 @ r15_v68 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+20+v1732 @ rdi_v93*8]");
																										enumerator8 = (List<TMP_Text>.Enumerator)0;
																										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6246 @ r15_v68 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+24+v1732 @ rdi_v93*8]");
																										enumerator7 = (List<TMP_Text>.Enumerator)0;
																										list2 = (List<TMP_Text>)(object)list9;
																									}
																									else
																									{
																										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371660");
																										enumerator7 = enumerator11;
																										enumerator8 = enumerator12;
																										list2 = null;
																									}
																									bool flag33 = list10 == null;
																									num = 0;
																									num2 = (nint)(&num25);
																									if (!flag33)
																									{
																										list10.Add((Vector2)(&enumerator13));
																										bool flag34 = (object)obj11 == null;
																										num = 0;
																										num2 = (nint)(&enumerator13);
																										list2 = (List<TMP_Text>)(object)list10;
																										if (!flag34)
																										{
																											object obj38 = obj18;
																											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6239 @ r12_v75 (UnityEngine.Object)+18]");
																											object obj40;
																											if ((nint)obj38 < 0)
																											{
																												object obj39 = obj18;
																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6239 @ r12_v75 (UnityEngine.Object)+18]");
																												bool flag35 = (nint)obj39 >= 0;
																												num = 0;
																												num2 = (nint)(&enumerator13);
																												list2 = (List<TMP_Text>)(object)list10;
																												if (flag35)
																												{
																													throw new IndexOutOfRangeException();
																												}
																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6239 @ r12_v75 (UnityEngine.Object)+20+v1732 @ rdi_v93*4]");
																												obj40 = 0;
																											}
																											else
																											{
																												obj41 = (UnityEngine.Object)4294967295L;
																												obj40 = 4294967295L;
																											}
																											bool flag36 = list11 == null;
																											num = 0;
																											num2 = (nint)(&enumerator13);
																											list2 = (List<TMP_Text>)(object)list11;
																											if (!flag36)
																											{
																												list11.Add((Color32)(&num25));
																												obj18++;
																												enumerator13 = enumerator8;
																												num25 = (float)obj40;
																												enumerator10 = enumerator6;
																												num12 = (nint)list8;
																												num = 0;
																												num2 = (nint)(&num25);
																												list2 = (List<TMP_Text>)(object)list11;
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
																						throw new NullReferenceException();
																					}
																					throw new IndexOutOfRangeException();
																				}
																				if (list7 != null)
																				{
																					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+60+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
																					bool flag37 = (nint)0 == 0;
																					num = (nint)(&list20);
																					num2 = num11;
																					list2 = (List<TMP_Text>)(object)list7;
																					if (!flag37)
																					{
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2762 @ rax_v151 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
																						List<TMP_Text> list21 = (List<TMP_Text>)0;
																						object obj42 = 0;
																						num = (nint)(&list20);
																						num2 = num11;
																						list2 = (List<TMP_Text>)(object)list7;
																						while (true)
																						{
																							object obj43 = obj42;
																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7179 @ rax_v298+18]");
																							if ((nint)obj43 >= 0)
																							{
																								break;
																							}
																							object obj44 = obj42;
																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7179 @ rax_v298+18]");
																							if ((nint)obj44 < 0)
																							{
																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7179 @ rax_v298+20+v3676 @ rdi_v95*4]");
																								list2 = (List<TMP_Text>)0;
																								if (list20 != null)
																								{
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7179 @ rax_v298+20+v3676 @ rdi_v95*4]");
																									nint num26 = 0;
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2762 @ rax_v151 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
																									List<TMP_Text> list22 = (List<TMP_Text>)(num26 + 0);
																									list20.Add((int)(&list21));
																									obj42++;
																									list21 = list22;
																									num = 0;
																									num2 = (nint)(&list21);
																									list2 = (List<TMP_Text>)(object)list20;
																									continue;
																								}
																								throw new NullReferenceException();
																							}
																							throw new IndexOutOfRangeException();
																						}
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+30]");
																						bool flag38 = (nint)0 == 0;
																						num8 = 0;
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2762 @ rax_v151 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
																						list3 = (List<TMP_Text>)0;
																						list12 = list23;
																						object obj45 = list2;
																						if (!flag38)
																						{
																							if ((object)obj8 == null)
																							{
																								throw new NullReferenceException();
																							}
																							string text = obj8.name;
																							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
																							string text2 = string.Format("[{0}] Added submesh from '{1}' (sm #{2}) ", "TMPTextBaker", text, arg);
																							bool flag39 = m2 == 0f;
																							num = (nint)text;
																							num2 = unchecked((nint)"TMPTextBaker");
																							list2 = (List<TMP_Text>)m2;
																							if (flag39)
																							{
																								throw new NullReferenceException();
																							}
																							string text3 = ((UnityEngine.Object)m2).name;
																							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
																							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul dword ptr [rsi+18h]\"");
																							object obj46 = (nint)(&obj47) >> 31;
																							object obj48 = (ref *(_003F*)(&obj47)) + (ref *(_003F*)obj46);
																							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
																							string text4 = $"mat '{text3}', verts copied: {arg2}, tris: {obj49}";
																							string text5 = text2 + text4;
																							Debug.Log(text5);
																							obj50 = obj48;
																							obj47 = obj17;
																							IntPtr intPtr = num9;
																							num8 = (nint)obj49;
																							obj11 = obj8;
																							list3 = (List<TMP_Text>)(object)text2;
																							list12 = (List<Color32>)(object)text3;
																							num = unchecked((nint)null);
																							obj45 = text5;
																						}
																						num9++;
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+20+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
																						obj12 = 0;
																						m2 = num13;
																						m = num14;
																						enumerator = enumerator7;
																						enumerator3 = enumerator8;
																						tMP_TextInfo = textInfo;
																						obj7 = obj6;
																						obj10 = (UnityEngine.Object)obj45;
																						list13 = list7;
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
																	bool flag40 = (object)obj8 == null;
																	num = unchecked((nint)null);
																	num2 = unchecked((nint)null);
																	list2 = (List<TMP_Text>)m2;
																	if (flag40)
																	{
																		throw new NullReferenceException();
																	}
																	string text6 = obj8.name;
																	string text7 = "[TMPTextBaker] Submesh with null material on " + text6 + " skipped.";
																	num8 = unchecked((nint)null);
																	obj11 = obj8;
																	text8 = text7;
																	num = unchecked((nint)" skipped.");
																}
																else
																{
																	object[] array = new object[4];
																	bool flag41 = array == null;
																	obj9 = (UnityEngine.Object)4;
																	obj10 = (UnityEngine.Object)(object)typeof(object[]);
																	if (flag41)
																	{
																		throw new NullReferenceException();
																	}
																	bool flag42 = "TMPTextBaker" == null;
																	num2 = 4;
																	List<TMP_Text> list24 = (List<TMP_Text>)(object)typeof(object[]);
																	List<TMP_Text> list25 = (List<TMP_Text>)(object)"TMPTextBaker";
																	List<int> list28;
																	if (!flag42)
																	{
																		nint num27 = (nint)array;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8550 @ rdx_v220 (Il2CppClass<System.Object[]>)+40]");
																		num2 = 0;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8550 @ rdx_v220 (Il2CppClass<System.Object[]>)+40]");
																		List<int> list26 = ((List<List<int>>)(object)"TMPTextBaker").get_Item(0);
																		bool flag43 = list26 == null;
																		list2 = (List<TMP_Text>)(object)"TMPTextBaker";
																		if (flag43)
																		{
																			List<int> list27 = ((List<List<int>>)(object)list2).get_Item((int)num2);
																			num2 = unchecked((nint)null);
																			list28 = list27;
																			throw list27;
																		}
																		list24 = (List<TMP_Text>)(object)"TMPTextBaker";
																		list25 = (List<TMP_Text>)(object)"TMPTextBaker";
																	}
																	bool flag44 = array.Length <= 0;
																	list28 = (List<int>)(object)list24;
																	if (flag44)
																	{
																		int num28 = (int)num2;
																		List<List<int>> list29 = (List<List<int>>)(object)list28;
																		throw new IndexOutOfRangeException();
																	}
																	array[0] = list25;
																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
																	List<int> list30 = (List<int>)0;
																	Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
																	bool flag45 = list31 == null;
																	int num29 = (int)(&list32);
																	if (!flag45)
																	{
																		nint num30 = (nint)array;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8776 @ rdx_v218 (Il2CppClass<System.Object[]>)+40]");
																		num29 = 0;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8776 @ rdx_v218 (Il2CppClass<System.Object[]>)+40]");
																		List<int> list33 = list31.get_Item(0);
																		bool flag46 = list33 == null;
																		list30 = (List<int>)(object)list31;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8776 @ rdx_v218 (Il2CppClass<System.Object[]>)+40]");
																		int num28 = 0;
																		List<List<int>> list29 = list31;
																		if (flag46)
																		{
																			List<int> list34 = list29.get_Item(num28);
																			num29 = 0;
																			list30 = list34;
																			throw list34;
																		}
																	}
																	if (array.Length <= 1)
																	{
																		List<List<int>> list35 = (List<List<int>>)(object)list30;
																		throw new IndexOutOfRangeException();
																	}
																	array[1] = list31;
																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
																	List<int> list36 = (List<int>)0;
																	Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
																	bool flag47 = list37 == null;
																	int num31 = (int)(&obj51);
																	if (!flag47)
																	{
																		nint num32 = (nint)array;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9144 @ rdx_v216 (Il2CppClass<System.Object[]>)+40]");
																		num31 = 0;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9144 @ rdx_v216 (Il2CppClass<System.Object[]>)+40]");
																		List<int> list38 = list37.get_Item(0);
																		bool flag48 = list38 == null;
																		list36 = (List<int>)(object)list37;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9144 @ rdx_v216 (Il2CppClass<System.Object[]>)+40]");
																		num29 = 0;
																		List<List<int>> list35 = list37;
																		if (flag48)
																		{
																			List<int> list39 = list35.get_Item(num29);
																			num31 = 0;
																			list36 = list39;
																			throw list39;
																		}
																	}
																	if (array.Length <= 2)
																	{
																		List<List<int>> list40 = (List<List<int>>)(object)list36;
																		throw new IndexOutOfRangeException();
																	}
																	array[2] = list37;
																	obj10 = (UnityEngine.Object)(array + 48);
																	bool flag49 = (object)obj8 == null;
																	obj9 = (UnityEngine.Object)(object)list37;
																	if (flag49)
																	{
																		throw new NullReferenceException();
																	}
																	string text9 = obj8.name;
																	bool flag50 = text9 == null;
																	int num33 = 0;
																	string text10 = (string)(object)obj8;
																	List<int> list43;
																	if (!flag50)
																	{
																		nint num34 = (nint)array;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9386 @ rdx_v214 (Il2CppClass<System.Object[]>)+40]");
																		num33 = 0;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9386 @ rdx_v214 (Il2CppClass<System.Object[]>)+40]");
																		List<int> list41 = ((List<List<int>>)(object)text9).get_Item(0);
																		bool flag51 = list41 == null;
																		text10 = text9;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9386 @ rdx_v214 (Il2CppClass<System.Object[]>)+40]");
																		num31 = 0;
																		List<List<int>> list40 = (List<List<int>>)(object)text9;
																		if (flag51)
																		{
																			List<int> list42 = list40.get_Item(num31);
																			obj9 = null;
																			list43 = list42;
																			throw list42;
																		}
																	}
																	bool flag52 = array.Length <= 3;
																	obj9 = (UnityEngine.Object)num33;
																	list43 = (List<int>)(object)text10;
																	if (flag52)
																	{
																		obj10 = (UnityEngine.Object)(object)list43;
																		throw new IndexOutOfRangeException();
																	}
																	array[3] = text9;
																	string text11 = string.Format("[{0}] Submesh has triangle index {1} >= vertex array length {2}. Skipping this submesh (object: {3}).", array);
																	list32 = list12;
																	obj11 = obj8;
																	text8 = text11;
																	num = unchecked((nint)null);
																}
																Debug.LogWarning(text8);
																obj7 = obj6 + 1;
																obj6 = (UnityEngine.Object)obj7;
																list14 = (List<TMP_Text>)(object)text8;
															}
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1194 @ rax_v295 (System.Collections.Generic.List`1<TMPro.TMP_Text>)+20+v4387 @ rdx_v227 (Il2CppMethodInfo)*8]");
														obj12 = 0;
														tMP_TextInfo = textInfo;
														list13 = list7;
													}
												}
												num9++;
												obj10 = (UnityEngine.Object)(object)list14;
												continue;
											}
											throw new IndexOutOfRangeException();
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								continue;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					enumerator5.Dispose();
					bool flag53 = list8 == null;
					num2 = 0;
					list2 = (List<TMP_Text>)(&enumerator5);
					if (flag53)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2762 @ rax_v151 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+28]");
						bool flag54 = string.IsNullOrWhiteSpace((string)0);
						string text12 = "BakedText";
						if (!flag54)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+28]");
							text12 = (string)0;
						}
						GameObject gameObject3 = new GameObject(text12);
						bool flag55 = (object)this == null;
						num = unchecked((nint)null);
						num2 = (nint)text12;
						list2 = (List<TMP_Text>)(object)gameObject3;
						if (flag55)
						{
							break;
						}
						bakedChild = gameObject3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						bool flag56 = (nint)0 == 0;
						num = unchecked((nint)null);
						num2 = (nint)gameObject3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						list2 = (List<TMP_Text>)0;
						if (flag56)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						Transform transform3 = ((GameObject)0).transform;
						bool flag57 = (object)transform3 == null;
						num = unchecked((nint)null);
						num2 = unchecked((nint)null);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						list2 = (List<TMP_Text>)0;
						if (flag57)
						{
							break;
						}
						transform3.SetParent(transform, worldPositionStays: false);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						bool flag58 = (nint)0 == 0;
						num = unchecked((nint)null);
						num2 = (nint)transform;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						list2 = (List<TMP_Text>)0;
						if (flag58)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						Transform transform4 = ((GameObject)0).transform;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						list2 = (List<TMP_Text>)0;
						bool flag59 = (object)transform4 == null;
						num = unchecked((nint)null);
						num2 = unchecked((nint)null);
						if (flag59)
						{
							break;
						}
						transform4.localPosition = (Vector3)(&enumerator10);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						bool flag60 = (nint)0 == 0;
						num = unchecked((nint)null);
						num2 = (nint)(&enumerator10);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						list2 = (List<TMP_Text>)0;
						if (flag60)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						Transform transform5 = ((GameObject)0).transform;
						bool flag61 = (object)transform5 == null;
						num = unchecked((nint)null);
						num2 = unchecked((nint)null);
						list2 = (List<TMP_Text>)(object)typeof(Quaternion);
						if (flag61)
						{
							break;
						}
						transform5.localRotation = (Quaternion)(&enumerator14);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						bool flag62 = (nint)0 == 0;
						num = unchecked((nint)null);
						num2 = (nint)(&enumerator14);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						list2 = (List<TMP_Text>)0;
						if (flag62)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						Transform transform6 = ((GameObject)0).transform;
						bool flag63 = (object)transform6 == null;
						num = unchecked((nint)null);
						num2 = unchecked((nint)null);
						list2 = (List<TMP_Text>)(object)typeof(Vector3);
						if (flag63)
						{
							break;
						}
						transform6.localScale = (Vector3)(&enumerator10);
						GameObject gameObject4 = transform.gameObject;
						bool flag64 = (object)gameObject4 == null;
						num = unchecked((nint)null);
						num2 = unchecked((nint)null);
						list2 = (List<TMP_Text>)(object)transform;
						if (flag64)
						{
							break;
						}
						int layer = gameObject4.layer;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						bool flag65 = (nint)0 == 0;
						num = unchecked((nint)null);
						num2 = unchecked((nint)null);
						list2 = (List<TMP_Text>)(object)gameObject4;
						if (flag65)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
						((GameObject)0).layer = layer;
						Mesh mesh = new Mesh();
						bool flag66 = (object)mesh == null;
						num = unchecked((nint)null);
						num2 = unchecked((nint)null);
						list2 = (List<TMP_Text>)(object)mesh;
						if (flag66)
						{
							break;
						}
						mesh.name = "BakedTMP_Mesh";
						mesh.SetVertices(list8);
						mesh.SetUVs(0, list9);
						mesh.SetUVs(1, list10);
						mesh.SetColors(list11);
						bool flag67 = list6 == null;
						num = unchecked((nint)null);
						num2 = (nint)list11;
						list2 = (List<TMP_Text>)(object)mesh;
						if (flag67)
						{
							break;
						}
						mesh.subMeshCount = list6._size;
						nint num35 = unchecked((nint)null);
						List<Material> list44 = list6;
						nint num36 = unchecked((nint)null);
						num = unchecked((nint)null);
						num2 = list6._size;
						list2 = (List<TMP_Text>)(object)mesh;
						nint num37 = unchecked((nint)null);
						while (true)
						{
							if (num37 < list44._size)
							{
								if (list13 == null)
								{
									break;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								bool flag68 = list45 == null;
								nint num38 = 0;
								object obj52 = 0;
								object obj53 = 4294967295L;
								nint num39 = (nint)(&list45);
								num = (nint)(&list45);
								num2 = num36;
								list2 = (List<TMP_Text>)(object)list13;
								if (flag68)
								{
									break;
								}
								while (true)
								{
									object obj54 = obj52;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v402 @ stack_-320_v63 (System.Collections.Generic.List`1<System.Int32>)+18]");
									if ((nint)obj54 >= 0)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									bool flag69 = System.Runtime.CompilerServices.Unsafe.As<UnityEngine.Object, UIntPtr>(ref obj41) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj53);
									num38 = 0;
									num39 = (nint)(&obj41);
									if (!flag69)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
										num38 = 0;
										obj53 = obj51;
										num39 = (nint)(&obj51);
									}
									obj52++;
								}
								object obj55 = obj53;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2762 @ rax_v151 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
								if ((nint)obj55 < 0)
								{
									mesh.SetTriangles(list45, (int)num36, calculateBounds: true);
									num36++;
									num35 = 1;
									list44 = list6;
									num = num36;
									num2 = (nint)list45;
									list2 = (List<TMP_Text>)(object)mesh;
									num37 = num36;
									continue;
								}
								object[] array2 = new object[4];
								bool flag70 = array2 == null;
								num = num39;
								UnityEngine.Object obj9 = (UnityEngine.Object)4;
								nint num40 = (nint)typeof(object[]);
								if (!flag70)
								{
									bool flag71 = "TMPTextBaker" == null;
									obj9 = (UnityEngine.Object)4;
									UnityEngine.Object obj56 = (UnityEngine.Object)(object)typeof(object[]);
									UnityEngine.Object obj57 = (UnityEngine.Object)(object)"TMPTextBaker";
									if (!flag71)
									{
										nint num41 = (nint)array2;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9770 @ rdx_v179 (Il2CppClass<System.Object[]>)+40]");
										obj9 = (UnityEngine.Object)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9770 @ rdx_v179 (Il2CppClass<System.Object[]>)+40]");
										List<int> list46 = ((List<List<int>>)(object)"TMPTextBaker").get_Item(0);
										bool flag72 = list46 == null;
										num = num39;
										UnityEngine.Object obj10 = (UnityEngine.Object)(object)"TMPTextBaker";
										if (flag72)
										{
											List<int> list47 = ((List<List<int>>)(object)obj10).get_Item((int)obj9);
											throw list47;
										}
										obj56 = (UnityEngine.Object)(object)"TMPTextBaker";
										obj57 = (UnityEngine.Object)(object)"TMPTextBaker";
									}
									bool flag73 = array2.Length <= 0;
									num = num39;
									num40 = (nint)obj56;
									if (!flag73)
									{
										array2[0] = obj57;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
										num40 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										bool flag74 = list48 == null;
										obj9 = (UnityEngine.Object)(&intPtr2);
										if (!flag74)
										{
											nint num42 = (nint)array2;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9802 @ rdx_v177 (Il2CppClass<System.Object[]>)+40]");
											int index = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9802 @ rdx_v177 (Il2CppClass<System.Object[]>)+40]");
											List<int> list49 = list48.get_Item(0);
											bool flag75 = list49 == null;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9802 @ rdx_v177 (Il2CppClass<System.Object[]>)+40]");
											obj9 = (UnityEngine.Object)0;
											num40 = (nint)list48;
											num = num39;
											List<List<int>> list50 = list48;
											if (flag75)
											{
												List<int> list51 = list50.get_Item(index);
												throw list51;
											}
										}
										bool flag76 = array2.Length <= 1;
										num = num39;
										if (!flag76)
										{
											array2[1] = list48;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
											num40 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
											bool flag77 = list52 == null;
											obj9 = (UnityEngine.Object)(&obj50);
											if (!flag77)
											{
												nint num43 = (nint)array2;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9888 @ rdx_v175 (Il2CppClass<System.Object[]>)+40]");
												int index2 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9888 @ rdx_v175 (Il2CppClass<System.Object[]>)+40]");
												List<int> list53 = list52.get_Item(0);
												bool flag78 = list53 == null;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9888 @ rdx_v175 (Il2CppClass<System.Object[]>)+40]");
												obj9 = (UnityEngine.Object)0;
												num40 = (nint)list52;
												num = num39;
												List<List<int>> list54 = list52;
												if (flag78)
												{
													List<int> list55 = list54.get_Item(index2);
													throw list55;
												}
											}
											bool flag79 = array2.Length <= 2;
											num = num39;
											if (!flag79)
											{
												array2[2] = list52;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
												num40 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
												bool flag80 = list56 == null;
												obj9 = (UnityEngine.Object)(&obj47);
												if (!flag80)
												{
													nint num44 = (nint)array2;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9936 @ rdx_v173 (Il2CppClass<System.Object[]>)+40]");
													int index3 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9936 @ rdx_v173 (Il2CppClass<System.Object[]>)+40]");
													List<int> list57 = list56.get_Item(0);
													bool flag81 = list57 == null;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9936 @ rdx_v173 (Il2CppClass<System.Object[]>)+40]");
													obj9 = (UnityEngine.Object)0;
													num40 = (nint)list56;
													num = num39;
													List<List<int>> list58 = list56;
													if (flag81)
													{
														List<int> list59 = list58.get_Item(index3);
														throw list59;
													}
												}
												bool flag82 = array2.Length <= 3;
												num = num39;
												if (!flag82)
												{
													array2[3] = list56;
													string message = string.Format("[{0}] Internal error: submesh {1} has max triangle index {2} >= vertexCount {3}. Skipping this submesh.", array2);
													Debug.LogError(message);
													int[] array3 = Array.Empty<int>();
													mesh.SetTriangles(array3, (int)num36, calculateBounds: true);
													num36++;
													num35 = 1;
													list44 = list6;
													num = num36;
													num2 = (nint)array3;
													list2 = (List<TMP_Text>)(object)mesh;
													num37 = num36;
													continue;
												}
											}
										}
									}
									throw new IndexOutOfRangeException();
								}
								Component component = (Component)num40;
								throw new NullReferenceException();
							}
							mesh.RecalculateBounds();
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
							bool flag83 = (nint)0 == 0;
							num2 = unchecked((nint)null);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
							list2 = (List<TMP_Text>)0;
							if (flag83)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
							MeshFilter meshFilter = ((GameObject)0).AddComponent<MeshFilter>();
							bool flag84 = (object)meshFilter == null;
							num2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
							list2 = (List<TMP_Text>)0;
							if (flag84)
							{
								break;
							}
							meshFilter.sharedMesh = mesh;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
							bool flag85 = (nint)0 == 0;
							num = unchecked((nint)null);
							num2 = (nint)mesh;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
							list2 = (List<TMP_Text>)0;
							if (flag85)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+40]");
							MeshRenderer meshRenderer = ((GameObject)0).AddComponent<MeshRenderer>();
							Material[] array4 = list44.ToArray();
							bool flag86 = (object)meshRenderer == null;
							num = unchecked((nint)null);
							num2 = 0;
							list2 = (List<TMP_Text>)(object)list44;
							if (flag86)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+38]");
							bool flag87 = (nint)0 == 0;
							num = unchecked((nint)null);
							num2 = (nint)array4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+38]");
							list2 = (List<TMP_Text>)0;
							if (flag87)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+38]");
							((List<GameObject>)0).Clear();
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
							num = 0;
							while (true)
							{
								if (enumerator15.MoveNext())
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
									bool flag88 = obj41 != null;
									bool flag89 = !flag88;
									num = unchecked((nint)null);
									if (flag89)
									{
										continue;
									}
									bool flag90 = (object)obj41 == null;
									num = unchecked((nint)null);
									UnityEngine.Object obj9 = null;
									Component component = (Component)obj41;
									if (!flag90)
									{
										GameObject gameObject5 = ((Component)obj41).gameObject;
										bool flag91 = (object)gameObject5 == null;
										num = unchecked((nint)null);
										obj9 = null;
										component = (Component)obj41;
										if (!flag91)
										{
											bool activeSelf = gameObject5.activeSelf;
											bool flag92 = !activeSelf;
											num = unchecked((nint)null);
											if (!flag92)
											{
												GameObject gameObject6 = ((Component)obj41).gameObject;
												bool flag93 = (object)gameObject6 == null;
												num = unchecked((nint)null);
												obj9 = null;
												component = (Component)obj41;
												if (flag93)
												{
													throw new NullReferenceException();
												}
												gameObject6.SetActive(value: false);
												GameObject item = ((Component)obj41).gameObject;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+38]");
												bool flag94 = (nint)0 == 0;
												num = unchecked((nint)null);
												obj9 = null;
												component = (Component)obj41;
												if (flag94)
												{
													break;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1504 @ stack_8 (System.Collections.Generic.List`1<UnityEngine.Color32>)+38]");
												((List<GameObject>)0).Add(item);
												num = 0;
											}
											continue;
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								enumerator15.Dispose();
								_ = 1;
								object[] array5 = new object[4];
								bool flag95 = "TMPTextBaker" == null;
								Component component2 = (Component)(object)"TMPTextBaker";
								if (!flag95)
								{
									nint num45 = (nint)array5;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9962 @ rdx_v146 (Il2CppClass<System.Object[]>)+40]");
									UnityEngine.Object obj9 = (UnityEngine.Object)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9962 @ rdx_v146 (Il2CppClass<System.Object[]>)+40]");
									List<int> list60 = ((List<List<int>>)(object)"TMPTextBaker").get_Item(0);
									bool flag96 = list60 == null;
									Component component = (Component)(object)"TMPTextBaker";
									if (flag96)
									{
										List<int> list61 = ((List<List<int>>)(object)component).get_Item((int)obj9);
										throw list61;
									}
									component2 = (Component)(object)"TMPTextBaker";
								}
								array5[0] = component2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								if (list62 != null)
								{
									nint num46 = (nint)array5;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10001 @ rdx_v144 (Il2CppClass<System.Object[]>)+40]");
									int index4 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10001 @ rdx_v144 (Il2CppClass<System.Object[]>)+40]");
									List<int> list63 = list62.get_Item(0);
									bool flag97 = list63 == null;
									List<List<int>> list64 = list62;
									if (flag97)
									{
										List<int> list65 = list64.get_Item(index4);
										throw list65;
									}
								}
								array5[1] = list62;
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								if (list66 != null)
								{
									nint num47 = (nint)array5;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10032 @ rdx_v142 (Il2CppClass<System.Object[]>)+40]");
									int index5 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10032 @ rdx_v142 (Il2CppClass<System.Object[]>)+40]");
									List<int> list67 = list66.get_Item(0);
									bool flag98 = list67 == null;
									List<List<int>> list68 = list66;
									if (flag98)
									{
										List<int> list69 = list68.get_Item(index5);
										throw list69;
									}
								}
								array5[2] = list66;
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								if (list70 != null)
								{
									nint num48 = (nint)array5;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10062 @ rdx_v140 (Il2CppClass<System.Object[]>)+40]");
									int index6 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10062 @ rdx_v140 (Il2CppClass<System.Object[]>)+40]");
									List<int> list71 = list70.get_Item(0);
									bool flag99 = list71 == null;
									List<List<int>> list72 = list70;
									if (flag99)
									{
										List<int> list73 = list72.get_Item(index6);
										throw list73;
									}
								}
								array5[3] = list70;
								string message2 = string.Format("[{0}] Bake complete. Combined {1} texts into 1 mesh. Materials: {2}. Skipped submeshes: {3}.", array5);
								Debug.Log(message2);
								return;
							}
							throw new NullReferenceException();
						}
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					string text13 = string.Format("[{0}] No vertex data produced. Skipped submeshes: {1}.", "TMPTextBaker", arg3);
					message3 = text13;
				}
				else
				{
					message3 = "[TMPTextBaker] No eligible TMP_Text components found.";
				}
				Debug.LogWarning(message3);
				return;
				IL_2c21:
				obj2++;
				obj += 8;
				obj3 = obj2;
				continue;
				IL_0442:
				list3 = list;
				goto IL_2c21;
				IL_0304:
				bool flag100 = !skipLocText;
				nint num49 = num4;
				if (!flag100)
				{
					GameObject gameObject7 = ((Component)obj).gameObject;
					bool flag101 = (object)gameObject7 == null;
					num = num3;
					num2 = unchecked((nint)null);
					list2 = (List<TMP_Text>)obj;
					if (flag101)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					bool flag102 = obj6 != null;
					num3 = unchecked((nint)null);
					num49 = unchecked((nint)null);
					list4 = (List<TMP_Text>)(object)obj6;
					num = unchecked((nint)null);
					num2 = unchecked((nint)null);
					list2 = (List<TMP_Text>)(object)obj6;
					if (flag102)
					{
						goto IL_0442;
					}
				}
				bool flag103 = list == null;
				num = num3;
				num2 = num49;
				list2 = list4;
				if (flag103)
				{
					break;
				}
				list.Add((TMP_Text)obj);
				obj2++;
				obj += 8;
				list3 = list;
				num = 0;
				num2 = (nint)obj;
				list2 = list;
				obj3 = obj2;
			}
		}
		throw new NullReferenceException();
	}

	public unsafe void Unbake()
	{
		if (baked)
		{
			if (bakedChild != null)
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
					if (obj != null)
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
				Debug.Log("[TMPTextBaker] Unbaked and restored original objects.");
				return;
			}
			throw new NullReferenceException();
		}
		Debug.LogWarning("[TMPTextBaker] Not baked.");
	}

	public TMPTextBaker()
	{
		List<GameObject> list = new List<GameObject>();
		disabledOriginals = list;
		base._002Ector();
	}
}
