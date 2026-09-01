using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class VertexShakeB : MonoBehaviour
{
	private sealed class _003CAnimateVertexColors_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public VertexShakeB _003C_003E4__this;

		private TMP_TextInfo _003CtextInfo_003E5__2;

		private Vector3[][] _003CcopyOfVertices_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAnimateVertexColors_003Ed__10(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0008: Expected O, but got Ref
			//IL_007e: Expected I4, but got I8
			//IL_001d: Expected O, but got I4
			//IL_0065: Expected I4, but got I8
			//IL_26a9: Expected O, but got I4
			//IL_019f: Expected O, but got I4
			//IL_26c5: Expected I4, but got O
			//IL_02fb: Expected O, but got I4
			//IL_03c2: Expected O, but got I4
			//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_03cf: Expected O, but got Unknown
			//IL_0415: Expected O, but got I4
			//IL_041d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0422: Expected O, but got Unknown
			//IL_0440: Expected O, but got I
			//IL_0232: Expected O, but got I
			//IL_04d5: Expected O, but got I
			//IL_04f8: Expected O, but got I
			//IL_0522: Expected O, but got I
			//IL_252c: Expected O, but got I
			//IL_252c: Expected O, but got I
			//IL_060a: Expected O, but got I
			//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a7: Expected O, but got Unknown
			//IL_244e: Expected O, but got I
			//IL_2457: Expected O, but got I4
			//IL_258c: Expected O, but got I
			//IL_25ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_25b2: Expected O, but got Unknown
			//IL_0661: Unknown result type (might be due to invalid IL or missing references)
			//IL_0666: Expected O, but got Unknown
			//IL_26ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_26f4: Expected O, but got Unknown
			//IL_26fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_2702: Expected O, but got Unknown
			//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0704: Expected O, but got Unknown
			//IL_0714: Expected O, but got I
			//IL_0753: Expected O, but got I
			//IL_0763: Unknown result type (might be due to invalid IL or missing references)
			//IL_0768: Expected O, but got Unknown
			//IL_0785: Expected O, but got I
			//IL_0793: Unknown result type (might be due to invalid IL or missing references)
			//IL_0798: Expected O, but got Unknown
			//IL_07db: Expected O, but got I
			//IL_07f1: Expected O, but got I
			//IL_0801: Unknown result type (might be due to invalid IL or missing references)
			//IL_0806: Expected O, but got Unknown
			//IL_0840: Expected O, but got I
			//IL_0850: Unknown result type (might be due to invalid IL or missing references)
			//IL_0855: Expected O, but got Unknown
			//IL_0872: Expected O, but got I
			//IL_090c: Expected O, but got I
			//IL_091c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0921: Expected O, but got Unknown
			//IL_097c: Expected O, but got I
			//IL_098c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0991: Expected O, but got Unknown
			//IL_09fa: Expected O, but got I
			//IL_0a34: Expected O, but got I
			//IL_0a44: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a49: Expected O, but got Unknown
			//IL_0a7b: Expected O, but got I
			//IL_0ab2: Expected O, but got I
			//IL_0ac2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac7: Expected O, but got Unknown
			//IL_0b30: Expected O, but got I
			//IL_0b6a: Expected O, but got I
			//IL_0b7a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b7f: Expected O, but got Unknown
			//IL_0bb1: Expected O, but got I
			//IL_0be8: Expected O, but got I
			//IL_0bf8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bfd: Expected O, but got Unknown
			//IL_0c66: Expected O, but got I
			//IL_0ca0: Expected O, but got I
			//IL_0cb0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cb5: Expected O, but got Unknown
			//IL_0ce7: Expected O, but got I
			//IL_0d1e: Expected O, but got I
			//IL_0d2e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d33: Expected O, but got Unknown
			//IL_274f: Expected I, but got O
			//IL_2772: Expected I, but got O
			//IL_0de4: Expected O, but got I
			//IL_0df4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0df9: Expected O, but got Unknown
			//IL_0e1b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e20: Expected O, but got Unknown
			//IL_0e52: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e57: Expected O, but got Unknown
			//IL_0eb0: Expected O, but got I
			//IL_0ec0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ec5: Expected O, but got Unknown
			//IL_0f44: Expected O, but got I
			//IL_0f7b: Expected O, but got I
			//IL_0f8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f90: Expected O, but got Unknown
			//IL_0fa6: Expected O, but got I
			//IL_0fc3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fc8: Expected O, but got Unknown
			//IL_0ff2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ff7: Expected O, but got Unknown
			//IL_1048: Expected O, but got I
			//IL_1058: Unknown result type (might be due to invalid IL or missing references)
			//IL_105d: Expected O, but got Unknown
			//IL_10dc: Expected O, but got I
			//IL_1113: Expected O, but got I
			//IL_1123: Unknown result type (might be due to invalid IL or missing references)
			//IL_1128: Expected O, but got Unknown
			//IL_113e: Expected O, but got I
			//IL_115b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1160: Expected O, but got Unknown
			//IL_118a: Unknown result type (might be due to invalid IL or missing references)
			//IL_118f: Expected O, but got Unknown
			//IL_11e0: Expected O, but got I
			//IL_11f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_11f5: Expected O, but got Unknown
			//IL_1274: Expected O, but got I
			//IL_12ab: Expected O, but got I
			//IL_12bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_12c0: Expected O, but got Unknown
			//IL_12d6: Expected O, but got I
			//IL_12f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_12f8: Expected O, but got Unknown
			//IL_1322: Unknown result type (might be due to invalid IL or missing references)
			//IL_1327: Expected O, but got Unknown
			//IL_1378: Expected O, but got I
			//IL_1388: Unknown result type (might be due to invalid IL or missing references)
			//IL_138d: Expected O, but got Unknown
			//IL_1424: Expected O, but got I
			//IL_1434: Unknown result type (might be due to invalid IL or missing references)
			//IL_1439: Expected O, but got Unknown
			//IL_14be: Expected O, but got I
			//IL_14f5: Expected O, but got I
			//IL_1505: Unknown result type (might be due to invalid IL or missing references)
			//IL_150a: Expected O, but got Unknown
			//IL_158f: Expected O, but got I
			//IL_15c6: Expected O, but got I
			//IL_15d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_15db: Expected O, but got Unknown
			//IL_1660: Expected O, but got I
			//IL_1697: Expected O, but got I
			//IL_16a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_16ac: Expected O, but got Unknown
			//IL_175a: Expected O, but got I
			//IL_176a: Unknown result type (might be due to invalid IL or missing references)
			//IL_176f: Expected O, but got Unknown
			//IL_178c: Expected O, but got I
			//IL_17fa: Expected O, but got I
			//IL_1831: Expected O, but got I
			//IL_1841: Unknown result type (might be due to invalid IL or missing references)
			//IL_1846: Expected O, but got Unknown
			//IL_1863: Expected O, but got I
			//IL_18d1: Expected O, but got I
			//IL_1908: Expected O, but got I
			//IL_1918: Unknown result type (might be due to invalid IL or missing references)
			//IL_191d: Expected O, but got Unknown
			//IL_193a: Expected O, but got I
			//IL_19a8: Expected O, but got I
			//IL_19df: Expected O, but got I
			//IL_19ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_19f4: Expected O, but got Unknown
			//IL_1a11: Expected O, but got I
			//IL_2804: Expected I, but got O
			//IL_2832: Expected I, but got O
			//IL_1aaf: Expected O, but got I
			//IL_1abf: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ac4: Expected O, but got Unknown
			//IL_1ae1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ae6: Expected O, but got Unknown
			//IL_1b10: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b15: Expected O, but got Unknown
			//IL_1b6e: Expected O, but got I
			//IL_1b7e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b83: Expected O, but got Unknown
			//IL_1c02: Expected O, but got I
			//IL_1c39: Expected O, but got I
			//IL_1c49: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c4e: Expected O, but got Unknown
			//IL_1c64: Expected O, but got I
			//IL_1c8e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c93: Expected O, but got Unknown
			//IL_1ca3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ca8: Expected O, but got Unknown
			//IL_1d06: Expected O, but got I
			//IL_1d16: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d1b: Expected O, but got Unknown
			//IL_1d9a: Expected O, but got I
			//IL_1dd1: Expected O, but got I
			//IL_1de1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1de6: Expected O, but got Unknown
			//IL_1dfc: Expected O, but got I
			//IL_1e26: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e2b: Expected O, but got Unknown
			//IL_1e3b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e40: Expected O, but got Unknown
			//IL_1e9e: Expected O, but got I
			//IL_1eae: Unknown result type (might be due to invalid IL or missing references)
			//IL_1eb3: Expected O, but got Unknown
			//IL_1f32: Expected O, but got I
			//IL_1f69: Expected O, but got I
			//IL_1f79: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f7e: Expected O, but got Unknown
			//IL_1f94: Expected O, but got I
			//IL_1fc2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fc7: Expected O, but got Unknown
			//IL_1fd8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fdd: Expected O, but got Unknown
			//IL_203c: Expected O, but got I
			//IL_204c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2051: Expected O, but got Unknown
			//IL_20e2: Expected F4, but got I
			//IL_20f8: Expected O, but got I
			//IL_2108: Unknown result type (might be due to invalid IL or missing references)
			//IL_210d: Expected O, but got Unknown
			//IL_211d: Expected F4, but got I
			//IL_21aa: Expected O, but got I
			//IL_21e1: Expected O, but got I
			//IL_21f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_21f6: Expected O, but got Unknown
			//IL_2283: Expected O, but got I
			//IL_22ba: Expected O, but got I
			//IL_22ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_22cf: Expected O, but got Unknown
			//IL_235c: Expected O, but got I
			//IL_2399: Expected O, but got I
			//IL_23a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_23ae: Expected O, but got Unknown
			object obj2 = default(object);
			object obj = (object)(&obj2);
			VertexShakeB vertexShakeB = _003C_003E4__this;
			_ = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj3 = _003C_003E1__state - 1;
				if (!flag && (nint)obj3 != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
				TMP_Text textComponent = vertexShakeB.m_TextComponent;
				textComponent.ForceMeshUpdate();
				TMP_TextInfo textInfo = vertexShakeB.m_TextComponent.textInfo;
				_003CtextInfo_003E5__2 = textInfo;
				Vector3[][] array = new Vector3[0][];
				_003CcopyOfVertices_003E5__3 = array;
				vertexShakeB.hasTextChanged = true;
			}
			bool flag2 = !vertexShakeB.hasTextChanged;
			object obj4 = 32;
			bool flag3 = default(bool);
			if (!flag2)
			{
				Vector3[][] array2 = _003CcopyOfVertices_003E5__3;
				TMP_TextInfo tMP_TextInfo = _003CtextInfo_003E5__2;
				TMP_MeshInfo[] meshInfo = tMP_TextInfo.meshInfo;
				if (array2.Length < meshInfo.Length)
				{
					TMP_TextInfo tMP_TextInfo2 = _003CtextInfo_003E5__2;
					TMP_MeshInfo[] meshInfo2 = tMP_TextInfo2.meshInfo;
					Vector3[][] array3 = new Vector3[meshInfo2.Length][];
					_003CcopyOfVertices_003E5__3 = array3;
				}
				TMP_TextInfo tMP_TextInfo3 = _003CtextInfo_003E5__2;
				object obj5 = 32;
				flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				while (true)
				{
					TMP_MeshInfo[] meshInfo3 = tMP_TextInfo3.meshInfo;
					if ((flag5 ? 1 : 0) >= meshInfo3.Length)
					{
						break;
					}
					TMP_TextInfo tMP_TextInfo4 = _003CtextInfo_003E5__2;
					TMP_MeshInfo[] meshInfo4 = tMP_TextInfo4.meshInfo;
					if ((flag4 ? 1 : 0) < meshInfo4.Length)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2836 @ r14_v10 (System.Boolean)+30+v693 @ rax_v103 (TMPro.TMP_MeshInfo[])]");
						object obj6 = 0;
						Vector3[][] array4 = _003CcopyOfVertices_003E5__3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v598 @ rdx_v71+18]");
						Vector3[] array5 = new Vector3[0];
						if ((flag4 ? 1 : 0) < array4.Length)
						{
							tMP_TextInfo3 = _003CtextInfo_003E5__2;
							flag4 = (byte)((flag4 ? 1u : 0u) + 1u) != 0;
							obj5 += 8;
							flag3 = (byte)((flag3 ? 1u : 0u) + 80u) != 0;
							bool flag6 = _003CtextInfo_003E5__2 != null;
							flag5 = flag4;
							if (flag6)
							{
								continue;
							}
							goto IL_264b;
						}
					}
					goto IL_26b7;
				}
				vertexShakeB.hasTextChanged = false;
				obj4 = 32;
			}
			TMP_TextInfo tMP_TextInfo5 = _003CtextInfo_003E5__2;
			if (tMP_TextInfo5.characterCount != 0)
			{
				_ = 0;
				if (tMP_TextInfo5.lineCount > 0)
				{
					_ = tMP_TextInfo5.lineCount;
					float num = 0.5f;
					bool flag7 = false;
					bool flag8 = false;
					object obj17 = default(object);
					float num9 = default(float);
					Quaternion q = default(Quaternion);
					Quaternion quaternion2 = default(Quaternion);
					while (true)
					{
						TMP_TextInfo tMP_TextInfo6 = _003CtextInfo_003E5__2;
						TMP_LineInfo[] lineInfo = tMP_TextInfo6.lineInfo;
						if ((flag8 ? 1 : 0) >= lineInfo.Length)
						{
							break;
						}
						TMP_TextInfo tMP_TextInfo7 = _003CtextInfo_003E5__2;
						object obj7 = (flag7 ? 1 : 0) * 2;
						object obj8 = flag7 + obj7;
						object obj9 = obj8 << 5;
						TMP_LineInfo[] lineInfo2 = tMP_TextInfo7.lineInfo;
						TMP_TextInfo tMP_TextInfo8 = _003CtextInfo_003E5__2;
						TMP_CharacterInfo[] characterInfo = tMP_TextInfo8.characterInfo;
						object obj10 = (flag7 ? 1 : 0) * 2;
						object obj11 = flag7 + obj10;
						object obj12 = obj11 << 5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1878 @ rax_v30+38+v603 @ rdx_v18 (TMPro.TMP_LineInfo[])]");
						object obj13 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1878 @ rax_v30+38+v603 @ rdx_v18 (TMPro.TMP_LineInfo[])]");
						if ((nint)0 >= (nint)characterInfo.Length)
						{
							break;
						}
						TMP_TextInfo tMP_TextInfo9 = _003CtextInfo_003E5__2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1878 @ rax_v30+38+v603 @ rdx_v18 (TMPro.TMP_LineInfo[])]");
						ref Vector3 reference = ref *(Vector3*)((nint)0 * (nint)376);
						TMP_CharacterInfo[] characterInfo2 = tMP_TextInfo9.characterInfo;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2940 @ rax_v27 (TMPro.TMP_LineInfo[])+40+v747 @ rcx_v21]");
						if ((nint)0 >= (nint)characterInfo2.Length)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2940 @ rax_v27 (TMPro.TMP_LineInfo[])+40+v747 @ rcx_v21]");
						object obj14 = (nint)0 * (nint)376;
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2947 @ rcx_v22+128+v1872 @ rdx_v19 (TMPro.TMP_CharacterInfo[])]");
						nint num2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3029 @ r9_v12 (UnityEngine.Vector3&)+11C+v651 @ r8_v11 (TMPro.TMP_CharacterInfo[])]");
						object obj15 = num2 + 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2947 @ rcx_v22+120+v1872 @ rdx_v19 (TMPro.TMP_CharacterInfo[])]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2947 @ rcx_v22+120+v1872 @ rdx_v19 (TMPro.TMP_CharacterInfo[])]");
						nint num3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3029 @ r9_v12 (UnityEngine.Vector3&)+114+v651 @ r8_v11 (TMPro.TMP_CharacterInfo[])]");
						object obj16 = num3 + 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3029 @ r9_v12 (UnityEngine.Vector3&)+114+v651 @ r8_v11 (TMPro.TMP_CharacterInfo[])]");
						_ = 0;
						float num4 = (float)obj15 * num;
						float num5 = (float)obj16 * num;
						float num6 = UnityEngine.Random.Range(-0.25f, 0.25f);
						float num7 = num6 * ((float)Math.PI / 180f);
						Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 112)));
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2940 @ rax_v27 (TMPro.TMP_LineInfo[])+40+v747 @ rcx_v21]");
						_ = 0;
						_ = quaternion.x;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1878 @ rax_v30+38+v603 @ rdx_v18 (TMPro.TMP_LineInfo[])]");
						nint num8 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2940 @ rax_v27 (TMPro.TMP_LineInfo[])+40+v747 @ rcx_v21]");
						bool flag9 = num8 > 0;
						obj17 = obj17;
						num9 = num9;
						float num10 = num;
						object obj18 = obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1878 @ rax_v30+38+v603 @ rdx_v18 (TMPro.TMP_LineInfo[])]");
						object obj19 = 0;
						bool flag10 = flag3;
						Vector3[] array6 = null;
						if (!flag9)
						{
							while (true)
							{
								TMP_TextInfo tMP_TextInfo10 = _003CtextInfo_003E5__2;
								TMP_CharacterInfo[] characterInfo3 = tMP_TextInfo10.characterInfo;
								if ((nint)obj13 >= characterInfo3.Length)
								{
									break;
								}
								object obj20 = obj19 * 376;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v700 @ rax_v34+190+v749 @ rcx_v26 (TMPro.TMP_CharacterInfo[])]");
								if ((nint)0 != 0)
								{
									TMP_TextInfo tMP_TextInfo11 = _003CtextInfo_003E5__2;
									TMP_CharacterInfo[] characterInfo4 = tMP_TextInfo11.characterInfo;
									if ((nint)obj13 >= characterInfo4.Length)
									{
										break;
									}
									TMP_TextInfo tMP_TextInfo12 = _003CtextInfo_003E5__2;
									TMP_MeshInfo[] meshInfo5 = tMP_TextInfo12.meshInfo;
									TMP_CharacterInfo[] characterInfo5 = tMP_TextInfo12.characterInfo;
									object obj21 = obj19 * 376;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									obj18 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)meshInfo5.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									object obj22 = (nint)0 * (nint)4;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									object obj23 = 0 + obj22;
									object obj24 = obj23 + obj23;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v605 @ rdx_v23 (TMPro.TMP_MeshInfo[])+30+v750 @ rcx_v29*8]");
									TMP_MeshInfo tMP_MeshInfo = (TMP_MeshInfo)0;
									object obj25 = obj19 * 376;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									if (0 >= (nint)tMP_MeshInfo.normals)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj26 = (nint)0 + (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj27 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj28 = 0 + obj27;
									Vector3[] normals = tMP_MeshInfo.normals;
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj26) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals))
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj29 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj30 = 0 + obj29;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v606 @ rdx_v24 (TMPro.TMP_MeshInfo)+40+v751 @ rcx_v30*4]");
									nint num11 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v606 @ rdx_v24 (TMPro.TMP_MeshInfo)+28+v1874 @ r8_v16*4]");
									object obj31 = num11 + 0;
									Vector3[][] array7 = _003CcopyOfVertices_003E5__3;
									float num12 = (float)obj31 * num10;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array7.Length)
									{
										break;
									}
									Vector3[] array8 = array7[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									if (0 >= (nint)tMP_MeshInfo.normals)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj32 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj33 = 0 + obj32;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v606 @ rdx_v24 (TMPro.TMP_MeshInfo)+28+v752 @ rcx_v31*4]");
									float num13 = 0f - num12;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array8.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj34 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj35 = 0 + obj34;
									Vector3[][] array9 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array9.Length)
									{
										break;
									}
									Vector3[] array10 = array9[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj36 = (nint)0 + (nint)1;
									Vector3[] normals2 = tMP_MeshInfo.normals;
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj36) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals2))
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj37 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj38 = 0 + obj37;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v606 @ rdx_v24 (TMPro.TMP_MeshInfo)+34+v754 @ rcx_v33*4]");
									float num14 = 0f - num12;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj39 = (nint)0 + (nint)1;
									if ((nint)obj39 >= array10.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj40 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj41 = 0 + obj40;
									Vector3[][] array11 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array11.Length)
									{
										break;
									}
									Vector3[] array12 = array11[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj42 = (nint)0 + (nint)2;
									Vector3[] normals3 = tMP_MeshInfo.normals;
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj42) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals3))
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj43 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj44 = 0 + obj43;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v606 @ rdx_v24 (TMPro.TMP_MeshInfo)+40+v756 @ rcx_v35*4]");
									float num15 = 0f - num12;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj45 = (nint)0 + (nint)2;
									if ((nint)obj45 >= array12.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj46 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj47 = 0 + obj46;
									Vector3[][] array13 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array13.Length)
									{
										break;
									}
									Vector3[] array14 = array13[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj48 = (nint)0 + (nint)3;
									Vector3[] normals4 = tMP_MeshInfo.normals;
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj48) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals4))
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj49 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj50 = 0 + obj49;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v606 @ rdx_v24 (TMPro.TMP_MeshInfo)+4C+v758 @ rcx_v37*4]");
									float num16 = 0f - num12;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj51 = (nint)0 + (nint)3;
									if ((nint)obj51 >= array14.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj52 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj53 = 0 + obj52;
									float num17 = UnityEngine.Random.Range(0.95f, 1.05f);
									nint num18 = (nint)typeof(Vector3);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3192 @ rax_v47 (Il2CppClass<UnityEngine.Vector3>)+B8]");
									nint num19 = 0;
									nint num20 = (nint)typeof(Vector3);
									ref Vector3 s = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 80));
									ref Vector3 pos = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 64));
									_ = Vector3.oneVector;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3193 @ rcx_v40 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3219 @ rax_v51 (Il2CppClass<UnityEngine.Vector3>)+B8]");
									nint num21 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3220 @ rcx_v44 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
									float num22 = 0f * num17;
									Matrix4x4 matrix4x = Matrix4x4.Internal_TRS(ref pos, ref q, ref s);
									Vector3[][] array15 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array15.Length)
									{
										break;
									}
									Vector3[] array16 = array15[obj18];
									Vector3[] array17 = array15[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array16.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj54 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj55 = 0 + obj54;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v608 @ rdx_v29 (UnityEngine.Vector3[])+20+v1891 @ rcx_v47*4]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+10]");
									object obj56 = 0 * quaternion2;
									obj = quaternion2;
									object obj57 = quaternion2 * quaternion2;
									object obj58 = obj56 + obj57;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v608 @ rdx_v29 (UnityEngine.Vector3[])+28+v1891 @ rcx_v47*4]");
									object obj59 = 0 * quaternion2;
									object obj60 = obj58 + obj59;
									object obj61 = obj60 + (object)quaternion2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array17.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj62 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj63 = 0 + obj62;
									Vector3[][] array18 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array18.Length)
									{
										break;
									}
									Vector3[] array19 = array18[obj18];
									Vector3[] array20 = array18[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj64 = (nint)0 + (nint)1;
									if ((nint)obj64 >= array19.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj65 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj66 = 0 + obj65;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj67 = (nint)0 + (nint)1;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v609 @ rdx_v30 (UnityEngine.Vector3[])+2C+v1892 @ rcx_v50*4]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+20]");
									object obj68 = 0 * obj;
									object obj69 = quaternion2 * quaternion2;
									object obj70 = obj68 + obj69;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v609 @ rdx_v30 (UnityEngine.Vector3[])+34+v1892 @ rcx_v50*4]");
									object obj71 = 0 * quaternion2;
									object obj72 = obj70 + obj71;
									object obj73 = obj72 + (object)quaternion2;
									if ((nint)obj67 >= array20.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj74 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj75 = 0 + obj74;
									Vector3[][] array21 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array21.Length)
									{
										break;
									}
									Vector3[] array22 = array21[obj18];
									Vector3[] array23 = array21[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj76 = (nint)0 + (nint)2;
									if ((nint)obj76 >= array22.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj77 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj78 = 0 + obj77;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj79 = (nint)0 + (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v610 @ rdx_v31 (UnityEngine.Vector3[])+38+v1893 @ rcx_v53*4]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+30]");
									object obj80 = 0 * obj;
									object obj81 = quaternion2 * quaternion2;
									object obj82 = obj80 + obj81;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v610 @ rdx_v31 (UnityEngine.Vector3[])+40+v1893 @ rcx_v53*4]");
									object obj83 = 0 * quaternion2;
									object obj84 = obj82 + obj83;
									object obj85 = obj84 + (object)quaternion2;
									if ((nint)obj79 >= array23.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj86 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj87 = 0 + obj86;
									Vector3[][] array24 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array24.Length)
									{
										break;
									}
									Vector3[] array25 = array24[obj18];
									Vector3[] array26 = array24[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj88 = (nint)0 + (nint)3;
									if ((nint)obj88 >= array25.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj89 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj90 = 0 + obj89;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj91 = (nint)0 + (nint)3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v611 @ rdx_v32 (UnityEngine.Vector3[])+44+v1894 @ rcx_v56*4]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+40]");
									object obj92 = 0 * obj;
									object obj93 = quaternion2 * quaternion2;
									object obj94 = obj92 + obj93;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v611 @ rdx_v32 (UnityEngine.Vector3[])+4C+v1894 @ rcx_v56*4]");
									object obj95 = 0 * quaternion2;
									object obj96 = obj94 + obj95;
									object obj97 = obj96 + (object)quaternion2;
									if ((nint)obj91 >= array26.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj98 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj99 = 0 + obj98;
									Vector3[][] array27 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array27.Length)
									{
										break;
									}
									Vector3[] array28 = array27[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array28.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj100 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj101 = 0 + obj100;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v613 @ rdx_v34 (UnityEngine.Vector3[])+28+v764 @ rcx_v58*4]");
									float num23 = 0f + num12;
									Vector3[][] array29 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array29.Length)
									{
										break;
									}
									Vector3[] array30 = array29[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj102 = (nint)0 + (nint)1;
									if ((nint)obj102 >= array30.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj103 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj104 = 0 + obj103;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v615 @ rdx_v36 (UnityEngine.Vector3[])+34+v765 @ rcx_v59*4]");
									float num24 = 0f + num12;
									Vector3[][] array31 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array31.Length)
									{
										break;
									}
									Vector3[] array32 = array31[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj105 = (nint)0 + (nint)2;
									if ((nint)obj105 >= array32.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj106 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj107 = 0 + obj106;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v617 @ rdx_v38 (UnityEngine.Vector3[])+40+v766 @ rcx_v60*4]");
									float num25 = 0f + num12;
									Vector3[][] array33 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array33.Length)
									{
										break;
									}
									Vector3[] array34 = array33[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj108 = (nint)0 + (nint)3;
									if ((nint)obj108 >= array34.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj109 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj110 = 0 + obj109;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v619 @ rdx_v40 (UnityEngine.Vector3[])+4C+v767 @ rcx_v61*4]");
									float num26 = 0f + num12;
									Vector3[][] array35 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array35.Length)
									{
										break;
									}
									Vector3[] array36 = array35[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array36.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj111 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj112 = 0 + obj111;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v621 @ rdx_v42 (UnityEngine.Vector3[])+28+v768 @ rcx_v62*4]");
									nint num27 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1E0]");
									object obj113 = num27 - 0;
									Vector3[][] array37 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array37.Length)
									{
										break;
									}
									Vector3[] array38 = array37[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj114 = (nint)0 + (nint)1;
									if ((nint)obj114 >= array38.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj115 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj116 = 0 + obj115;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v623 @ rdx_v44 (UnityEngine.Vector3[])+34+v769 @ rcx_v63*4]");
									nint num28 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1E0]");
									object obj117 = num28 - 0;
									Vector3[][] array39 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array39.Length)
									{
										break;
									}
									Vector3[] array40 = array39[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj118 = (nint)0 + (nint)2;
									if ((nint)obj118 >= array40.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj119 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj120 = 0 + obj119;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v625 @ rdx_v46 (UnityEngine.Vector3[])+40+v770 @ rcx_v64*4]");
									nint num29 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1E0]");
									object obj121 = num29 - 0;
									Vector3[][] array41 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array41.Length)
									{
										break;
									}
									Vector3[] array42 = array41[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj122 = (nint)0 + (nint)3;
									if ((nint)obj122 >= array42.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj123 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj124 = 0 + obj123;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v627 @ rdx_v48 (UnityEngine.Vector3[])+4C+v3456 @ rcx_v65*4]");
									nint num30 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1E0]");
									object obj125 = num30 - 0;
									nint num31 = (nint)typeof(Vector3);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3484 @ rdx_v49 (Il2CppClass<UnityEngine.Vector3>)+B8]");
									nint num32 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3485 @ rax_v70 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
									flag10 = false;
									nint num33 = (nint)typeof(Vector3);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3498 @ rdx_v50 (Il2CppClass<UnityEngine.Vector3>)+B8]");
									nint num34 = 0;
									reference = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 48));
									_ = Vector3.oneVector;
									ref Vector3 pos2 = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 32));
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3485 @ rax_v70 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
									_ = 0;
									_ = Vector3.oneVector;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3502 @ rax_v72 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
									_ = 0;
									Matrix4x4 matrix4x2 = Matrix4x4.Internal_TRS(ref pos2, ref q, ref reference);
									Vector3[][] array43 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array43.Length)
									{
										break;
									}
									Vector3[] array44 = array43[obj18];
									Vector3[] array45 = array43[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array44.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj126 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj127 = 0 + obj126;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v629 @ rdx_v52 (UnityEngine.Vector3[])+20+v1895 @ rcx_v71*4]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+50]");
									object obj128 = 0 * quaternion2;
									object obj129 = quaternion2 * quaternion2;
									object obj130 = obj128 + obj129;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v629 @ rdx_v52 (UnityEngine.Vector3[])+28+v1895 @ rcx_v71*4]");
									object obj131 = 0 * quaternion2;
									object obj132 = obj130 + obj131;
									object obj133 = obj132 + (object)quaternion2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array45.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj134 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj135 = 0 + obj134;
									Vector3[][] array46 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array46.Length)
									{
										break;
									}
									Vector3[] array47 = array46[obj18];
									Vector3[] array48 = array46[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj136 = (nint)0 + (nint)1;
									if ((nint)obj136 >= array47.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj137 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj138 = 0 + obj137;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj139 = (nint)0 + (nint)1;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v630 @ rdx_v53 (UnityEngine.Vector3[])+2C+v1896 @ rcx_v74*4]");
									_ = 0;
									object obj140 = quaternion2 * quaternion2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+60]");
									object obj141 = 0 * quaternion2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v630 @ rdx_v53 (UnityEngine.Vector3[])+34+v1896 @ rcx_v74*4]");
									object obj142 = 0 * quaternion2;
									object obj143 = obj141 + obj140;
									object obj144 = obj143 + obj142;
									object obj145 = obj144 + (object)quaternion2;
									if ((nint)obj139 >= array48.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj146 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj147 = 0 + obj146;
									Vector3[][] array49 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array49.Length)
									{
										break;
									}
									Vector3[] array50 = array49[obj18];
									Vector3[] array51 = array49[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj148 = (nint)0 + (nint)2;
									if ((nint)obj148 >= array50.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj149 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj150 = 0 + obj149;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj151 = (nint)0 + (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v631 @ rdx_v54 (UnityEngine.Vector3[])+38+v1897 @ rcx_v77*4]");
									_ = 0;
									object obj152 = quaternion2 * quaternion2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+70]");
									object obj153 = 0 * quaternion2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v631 @ rdx_v54 (UnityEngine.Vector3[])+40+v1897 @ rcx_v77*4]");
									object obj154 = 0 * quaternion2;
									object obj155 = obj153 + obj152;
									object obj156 = obj155 + obj154;
									object obj157 = obj156 + (object)quaternion2;
									if ((nint)obj151 >= array51.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj158 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj159 = 0 + obj158;
									Vector3[][] array52 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array52.Length)
									{
										break;
									}
									Vector3[] array53 = array52[obj18];
									array6 = array52[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj160 = (nint)0 + (nint)3;
									if ((nint)obj160 >= array53.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj161 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj162 = 0 + obj161;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj163 = (nint)0 + (nint)3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v632 @ rdx_v55 (UnityEngine.Vector3[])+44+v1898 @ rcx_v80*4]");
									_ = 0;
									object obj164 = quaternion2 * quaternion2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+80]");
									object obj165 = 0 * quaternion2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v632 @ rdx_v55 (UnityEngine.Vector3[])+4C+v1898 @ rcx_v80*4]");
									object obj166 = 0 * quaternion2;
									object obj167 = obj165 + obj164;
									object obj168 = obj167 + obj166;
									obj17 = obj168 + (object)quaternion2;
									if ((nint)obj163 >= array6.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj169 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj170 = 0 + obj169;
									Vector3[][] array54 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array54.Length)
									{
										break;
									}
									Vector3[] array55 = array54[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array55.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1D0]");
									num5 = 0f;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj171 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj172 = 0 + obj171;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1E0]");
									num4 = 0f;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v634 @ rdx_v57 (UnityEngine.Vector3[])+28+v776 @ rcx_v82*4]");
									float num35 = 0f;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1E0]");
									float num36 = num35 + 0f;
									Vector3[][] array56 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array56.Length)
									{
										break;
									}
									Vector3[] array57 = array56[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj173 = (nint)0 + (nint)1;
									if ((nint)obj173 >= array57.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj174 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj175 = 0 + obj174;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v636 @ rdx_v59 (UnityEngine.Vector3[])+34+v777 @ rcx_v83*4]");
									float num37 = 0f;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1E0]");
									float num38 = num37 + 0f;
									Vector3[][] array58 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array58.Length)
									{
										break;
									}
									Vector3[] array59 = array58[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj176 = (nint)0 + (nint)2;
									if ((nint)obj176 >= array59.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj177 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj178 = 0 + obj177;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v638 @ rdx_v61 (UnityEngine.Vector3[])+40+v778 @ rcx_v84*4]");
									float num39 = 0f;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1E0]");
									float num40 = num39 + 0f;
									Vector3[][] array60 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rax_v37+50+v559 @ rdi_v12 (TMPro.TMP_CharacterInfo[])]");
									if ((nint)0 >= (nint)array60.Length)
									{
										break;
									}
									ref Vector3 reference2 = ref *(Vector3*)array60[obj18];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj179 = (nint)0 + (nint)3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3026 @ rdx_v22 (UnityEngine.Vector3&)+18]");
									if ((nint)obj179 >= 0)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj180 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1880 @ rax_v38+64+v588 @ rbx_v14 (TMPro.TMP_CharacterInfo[])]");
									object obj181 = 0 + obj180;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3026 @ rdx_v22 (UnityEngine.Vector3&)+4C+v3094 @ rcx_v85*4]");
									float num41 = 0f;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1E0]");
									num9 = num41 + 0f;
									q = quaternion2;
									num10 = 0.5f;
								}
								obj13++;
								obj19++;
								object obj182 = obj19;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-80]");
								bool flag11 = (nint)obj182 <= 0;
								num = num10;
								obj4 = obj18;
								flag3 = flag10;
								if (flag11)
								{
									continue;
								}
								goto IL_23f0;
							}
							break;
						}
						goto IL_23f0;
						IL_23f0:
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1E8]");
						flag8 = (byte)((nuint)0u + (nuint)1u) != 0;
						flag7 = true;
						bool num42 = flag7;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-60]");
						if ((nint)(num42 ? 1 : 0) < (nint)0)
						{
							continue;
						}
						goto IL_243e;
					}
					goto IL_26b7;
				}
				goto IL_26d7;
			}
			WaitForSeconds waitForSeconds = new WaitForSeconds(0.25f);
			_003C_003E2__current = waitForSeconds;
			_003C_003E1__state = 1;
			goto IL_2858;
			IL_26b7:
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
			return (byte)(int)ex != 0;
			IL_2858:
			return true;
			IL_243e:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-58]");
			vertexShakeB = (VertexShakeB)0;
			obj4 = 32;
			goto IL_26d7;
			IL_264b:
			throw new NullReferenceException();
			IL_26d7:
			TMP_TextInfo tMP_TextInfo13 = _003CtextInfo_003E5__2;
			bool flag12 = false;
			bool flag13 = false;
			bool flag14 = false;
			while (true)
			{
				TMP_MeshInfo[] meshInfo6 = tMP_TextInfo13.meshInfo;
				if ((flag14 ? 1 : 0) >= meshInfo6.Length)
				{
					break;
				}
				TMP_TextInfo tMP_TextInfo14 = _003CtextInfo_003E5__2;
				TMP_MeshInfo[] meshInfo7 = tMP_TextInfo14.meshInfo;
				if ((flag12 ? 1 : 0) < meshInfo7.Length)
				{
					Vector3[][] array61 = _003CcopyOfVertices_003E5__3;
					if ((flag12 ? 1 : 0) < array61.Length)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v734 @ rax_v20 (TMPro.TMP_MeshInfo[])+20+v590 @ rbx_v7 (System.Boolean)]");
						nint num43 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v642 @ rdx_v10 (UnityEngine.Vector3[][])+v2881 @ rdi_v14]");
						((Mesh)num43).vertices = (Vector3[])0;
						TMP_TextInfo tMP_TextInfo15 = _003CtextInfo_003E5__2;
						TMP_MeshInfo[] meshInfo8 = tMP_TextInfo15.meshInfo;
						if ((flag12 ? 1 : 0) < meshInfo8.Length)
						{
							TMP_Text textComponent2 = vertexShakeB.m_TextComponent;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v644 @ rdx_v13 (TMPro.TMP_MeshInfo[])+20+v590 @ rbx_v7 (System.Boolean)]");
							textComponent2.UpdateGeometry((Mesh)0, flag12 ? 1 : 0);
							tMP_TextInfo13 = _003CtextInfo_003E5__2;
							flag12 = (byte)((flag12 ? 1u : 0u) + 1u) != 0;
							obj4 += 8;
							flag13 = (byte)((flag13 ? 1u : 0u) + 80u) != 0;
							bool flag15 = _003CtextInfo_003E5__2 != null;
							flag14 = flag12;
							if (flag15)
							{
								continue;
							}
							goto IL_264b;
						}
					}
				}
				goto IL_26b7;
			}
			WaitForSeconds waitForSeconds2 = new WaitForSeconds(0.1f);
			_003C_003E2__current = waitForSeconds2;
			_003C_003E1__state = 2;
			goto IL_2858;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	public float AngleMultiplier = 1f;

	public float SpeedMultiplier = 1f;

	public float CurveScale = 1f;

	private TMP_Text m_TextComponent;

	private bool hasTextChanged;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		TMP_Text textComponent = default(TMP_Text);
		m_TextComponent = textComponent;
	}

	private void OnEnable()
	{
		Action<UnityEngine.Object> rhs = ON_TEXT_CHANGED;
		TMPro_EventManager.TEXT_CHANGED_EVENT.Add(rhs);
	}

	private void OnDisable()
	{
		Action<UnityEngine.Object> rhs = ON_TEXT_CHANGED;
		TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(rhs);
	}

	private void Start()
	{
		_003CAnimateVertexColors_003Ed__10 obj = new _003CAnimateVertexColors_003Ed__10(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private void ON_TEXT_CHANGED(UnityEngine.Object obj)
	{
		if ((bool)m_TextComponent)
		{
			hasTextChanged = true;
		}
	}

	private IEnumerator AnimateVertexColors()
	{
		_003CAnimateVertexColors_003Ed__10 obj = new _003CAnimateVertexColors_003Ed__10(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
