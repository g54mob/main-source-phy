using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class VertexShakeA : MonoBehaviour
{
	private sealed class _003CAnimateVertexColors_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public VertexShakeA _003C_003E4__this;

		private TMP_TextInfo _003CtextInfo_003E5__2;

		private Vector3[][] _003CcopyOfVertices_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAnimateVertexColors_003Ed__11(int _003C_003E1__state)
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
			//IL_0357: Expected O, but got I4
			//IL_0384: Expected O, but got I4
			//IL_019f: Expected O, but got I4
			//IL_18a9: Expected I4, but got O
			//IL_02f4: Expected O, but got I
			//IL_03e4: Expected O, but got I4
			//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f1: Expected O, but got Unknown
			//IL_042c: Expected O, but got I4
			//IL_0434: Unknown result type (might be due to invalid IL or missing references)
			//IL_0439: Expected O, but got Unknown
			//IL_0457: Expected O, but got I
			//IL_0232: Expected O, but got I
			//IL_04ec: Expected O, but got I
			//IL_0509: Expected O, but got I
			//IL_053c: Expected O, but got Ref
			//IL_1710: Expected O, but got I
			//IL_1710: Expected O, but got I
			//IL_161d: Expected O, but got I4
			//IL_059c: Expected O, but got I
			//IL_05b6: Expected O, but got I
			//IL_05d8: Expected O, but got Ref
			//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a7: Expected O, but got Unknown
			//IL_163d: Expected O, but got I4
			//IL_1770: Expected O, but got I
			//IL_1791: Unknown result type (might be due to invalid IL or missing references)
			//IL_1796: Expected O, but got Unknown
			//IL_18d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_18d8: Expected O, but got Unknown
			//IL_18e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_18e6: Expected O, but got Unknown
			//IL_18ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_18f4: Expected O, but got Unknown
			//IL_06b5: Expected O, but got I
			//IL_073d: Expected O, but got I
			//IL_074d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0752: Expected O, but got Unknown
			//IL_076f: Expected O, but got I
			//IL_07b7: Expected O, but got I
			//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_07cc: Expected O, but got Unknown
			//IL_0827: Expected O, but got I
			//IL_0837: Unknown result type (might be due to invalid IL or missing references)
			//IL_083c: Expected O, but got Unknown
			//IL_08a5: Expected O, but got I
			//IL_08df: Expected O, but got I
			//IL_08ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_08f4: Expected O, but got Unknown
			//IL_0926: Expected O, but got I
			//IL_095d: Expected O, but got I
			//IL_096d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0972: Expected O, but got Unknown
			//IL_09db: Expected O, but got I
			//IL_0a15: Expected O, but got I
			//IL_0a25: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a2a: Expected O, but got Unknown
			//IL_0a5c: Expected O, but got I
			//IL_0a93: Expected O, but got I
			//IL_0aa3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa8: Expected O, but got Unknown
			//IL_0b11: Expected O, but got I
			//IL_0b4b: Expected O, but got I
			//IL_0b5b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b60: Expected O, but got Unknown
			//IL_0b92: Expected O, but got I
			//IL_0bc9: Expected O, but got I
			//IL_0bd9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bde: Expected O, but got Unknown
			//IL_1949: Expected I, but got O
			//IL_1967: Expected I, but got O
			//IL_0cc4: Expected O, but got I
			//IL_0cd4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cd9: Expected O, but got Unknown
			//IL_0cfc: Expected O, but got I
			//IL_0d0c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d11: Expected O, but got Unknown
			//IL_0d26: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d2b: Expected O, but got Unknown
			//IL_0d52: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d57: Expected O, but got Unknown
			//IL_0df1: Expected O, but got I
			//IL_0e28: Expected O, but got I
			//IL_0e38: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e3d: Expected O, but got Unknown
			//IL_0e53: Expected O, but got I
			//IL_0e7d: Expected O, but got I
			//IL_0e8d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e92: Expected O, but got Unknown
			//IL_0ebc: Expected O, but got I
			//IL_0f0d: Expected O, but got I
			//IL_0f1d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f22: Expected O, but got Unknown
			//IL_0f90: Expected O, but got I
			//IL_0fc7: Expected O, but got I
			//IL_0fd7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fdc: Expected O, but got Unknown
			//IL_0ff2: Expected O, but got I
			//IL_1002: Expected O, but got I
			//IL_1012: Unknown result type (might be due to invalid IL or missing references)
			//IL_1017: Expected O, but got Unknown
			//IL_1027: Unknown result type (might be due to invalid IL or missing references)
			//IL_102c: Expected O, but got Unknown
			//IL_1056: Expected O, but got I
			//IL_10a7: Expected O, but got I
			//IL_10b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_10bc: Expected O, but got Unknown
			//IL_112a: Expected O, but got I
			//IL_1161: Expected O, but got I
			//IL_1171: Unknown result type (might be due to invalid IL or missing references)
			//IL_1176: Expected O, but got Unknown
			//IL_118c: Expected O, but got I
			//IL_11b6: Expected O, but got I
			//IL_11ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_11cf: Expected O, but got Unknown
			//IL_11fa: Expected O, but got I
			//IL_124c: Expected O, but got I
			//IL_125c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1261: Expected O, but got Unknown
			//IL_12f8: Expected O, but got I
			//IL_1308: Unknown result type (might be due to invalid IL or missing references)
			//IL_130d: Expected O, but got Unknown
			//IL_1392: Expected O, but got I
			//IL_13c9: Expected O, but got I
			//IL_13d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_13de: Expected O, but got Unknown
			//IL_1463: Expected O, but got I
			//IL_149a: Expected O, but got I
			//IL_14aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_14af: Expected O, but got Unknown
			//IL_1534: Expected O, but got I
			//IL_156b: Expected O, but got I
			//IL_157b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1580: Expected O, but got Unknown
			//IL_1591: Expected O, but got I
			object obj2 = default(object);
			object obj = (object)(&obj2);
			VertexShakeA vertexShakeA = _003C_003E4__this;
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
				TMP_Text textComponent = vertexShakeA.m_TextComponent;
				textComponent.ForceMeshUpdate();
				TMP_TextInfo textInfo = vertexShakeA.m_TextComponent.textInfo;
				_003CtextInfo_003E5__2 = textInfo;
				Vector3[][] array = new Vector3[0][];
				_003CcopyOfVertices_003E5__3 = array;
				vertexShakeA.hasTextChanged = true;
			}
			bool flag2 = !vertexShakeA.hasTextChanged;
			int num = 0;
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
				object obj4 = 32;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				while (true)
				{
					TMP_MeshInfo[] meshInfo3 = tMP_TextInfo3.meshInfo;
					if (num4 >= meshInfo3.Length)
					{
						break;
					}
					TMP_TextInfo tMP_TextInfo4 = _003CtextInfo_003E5__2;
					TMP_MeshInfo[] meshInfo4 = tMP_TextInfo4.meshInfo;
					if (num3 < meshInfo4.Length)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v426 @ r14_v13 (System.Int32)+30+v515 @ rax_v68 (TMPro.TMP_MeshInfo[])]");
						object obj5 = 0;
						Vector3[][] array4 = _003CcopyOfVertices_003E5__3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v451 @ rdx_v41+18]");
						Vector3[] array5 = new Vector3[0];
						if (num3 < array4.Length)
						{
							tMP_TextInfo3 = _003CtextInfo_003E5__2;
							num3++;
							obj4 += 8;
							num2 += 80;
							bool flag3 = _003CtextInfo_003E5__2 != null;
							num4 = num3;
							if (flag3)
							{
								continue;
							}
							goto IL_182f;
						}
					}
					goto IL_189b;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+170]");
				vertexShakeA = (VertexShakeA)0;
				vertexShakeA.hasTextChanged = false;
				num = 0;
			}
			TMP_TextInfo tMP_TextInfo5 = _003CtextInfo_003E5__2;
			object obj6;
			if (tMP_TextInfo5.characterCount != 0)
			{
				bool flag4 = tMP_TextInfo5.lineCount <= 0;
				obj6 = 32;
				if (!flag4)
				{
					float num5 = 0.001f;
					float num6 = 0.5f;
					TMP_CharacterInfo tMP_CharacterInfo = (TMP_CharacterInfo)num;
					int num7 = num;
					int num8 = num;
					Vector3 euler = default(Vector3);
					object obj18 = default(object);
					Quaternion q = default(Quaternion);
					Quaternion quaternion = default(Quaternion);
					while (true)
					{
						TMP_TextInfo tMP_TextInfo6 = _003CtextInfo_003E5__2;
						TMP_LineInfo[] lineInfo = tMP_TextInfo6.lineInfo;
						if (num8 >= lineInfo.Length)
						{
							break;
						}
						TMP_TextInfo tMP_TextInfo7 = _003CtextInfo_003E5__2;
						object obj7 = num7 * 2;
						object obj8 = num7 + obj7;
						object obj9 = obj8 << 5;
						TMP_LineInfo[] lineInfo2 = tMP_TextInfo7.lineInfo;
						TMP_CharacterInfo[] characterInfo = tMP_TextInfo7.characterInfo;
						object obj10 = num7 * 2;
						object obj11 = num7 + obj10;
						object obj12 = obj11 << 5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1377 @ rax_v28+38+v479 @ r8_v12 (TMPro.TMP_LineInfo[])]");
						object obj13 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1377 @ rax_v28+38+v479 @ r8_v12 (TMPro.TMP_LineInfo[])]");
						if ((nint)0 >= (nint)characterInfo.Length)
						{
							break;
						}
						TMP_TextInfo tMP_TextInfo8 = _003CtextInfo_003E5__2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1377 @ rax_v28+38+v479 @ r8_v12 (TMPro.TMP_LineInfo[])]");
						ref Vector3 reference = ref *(Vector3*)((nint)0 * (nint)376);
						TMP_CharacterInfo[] characterInfo2 = tMP_TextInfo8.characterInfo;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2094 @ rdx_v18+40+v550 @ rcx_v20 (TMPro.TMP_LineInfo[])]");
						if ((nint)0 >= (nint)characterInfo2.Length)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2094 @ rdx_v18+40+v550 @ rcx_v20 (TMPro.TMP_LineInfo[])]");
						object obj14 = (nint)0 * (nint)376;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2101 @ rcx_v21+128+v1373 @ r8_v13 (TMPro.TMP_CharacterInfo[])]");
						nint num9 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2181 @ r9_v15 (UnityEngine.Vector3&)+11C+v456 @ rdx_v19 (TMPro.TMP_CharacterInfo[])]");
						object obj15 = num9 + 0;
						float num10 = (float)obj15 * num6;
						float num11 = UnityEngine.Random.Range(-0.25f, 0.25f);
						object obj16 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
						_ = Quaternion.Internal_FromEulerRad(ref euler).x;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1377 @ rax_v28+38+v479 @ r8_v12 (TMPro.TMP_LineInfo[])]");
						nint num12 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2094 @ rdx_v18+40+v550 @ rcx_v20 (TMPro.TMP_LineInfo[])]");
						if (num12 <= 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1377 @ rax_v28+38+v479 @ r8_v12 (TMPro.TMP_LineInfo[])]");
							object obj17 = (nint)0 * (nint)376;
							obj18 = obj18;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1377 @ rax_v28+38+v479 @ r8_v12 (TMPro.TMP_LineInfo[])]");
							object obj19 = 0;
							float num13 = num5;
							TMP_CharacterInfo tMP_CharacterInfo2 = tMP_CharacterInfo;
							VertexShakeA vertexShakeA2 = vertexShakeA;
							Vector3[] array6 = (Vector3[])(&euler);
							Vector3[] array7 = null;
							while (true)
							{
								TMP_TextInfo tMP_TextInfo9 = _003CtextInfo_003E5__2;
								TMP_CharacterInfo[] characterInfo3 = tMP_TextInfo9.characterInfo;
								if ((nint)obj13 >= characterInfo3.Length)
								{
									break;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v522 @ rax_v36 (TMPro.TMP_CharacterInfo[])+190+v423 @ r15_v8]");
								if ((nint)0 != 0)
								{
									TMP_TextInfo tMP_TextInfo10 = _003CtextInfo_003E5__2;
									TMP_CharacterInfo[] characterInfo4 = tMP_TextInfo10.characterInfo;
									if ((nint)obj13 >= characterInfo4.Length)
									{
										break;
									}
									TMP_TextInfo tMP_TextInfo11 = _003CtextInfo_003E5__2;
									TMP_MeshInfo[] meshInfo5 = tMP_TextInfo11.meshInfo;
									TMP_CharacterInfo[] characterInfo5 = tMP_TextInfo11.characterInfo;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									tMP_CharacterInfo2 = (TMP_CharacterInfo)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									if ((nint)0 >= (nint)meshInfo5.Length)
									{
										break;
									}
									Vector3[][] array8 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									if ((nint)0 >= (nint)array8.Length)
									{
										break;
									}
									Vector3[] array9 = array8[(object)tMP_CharacterInfo2];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									object obj20 = (nint)0 * (nint)4;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									object obj21 = 0 + obj20;
									object obj22 = obj21 + obj21;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v458 @ rdx_v24 (TMPro.TMP_MeshInfo[])+30+v552 @ rcx_v27*8]");
									TMP_MeshInfo tMP_MeshInfo = (TMP_MeshInfo)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									if (0 >= (nint)tMP_MeshInfo.normals)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj23 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj24 = 0 + obj23;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v459 @ rdx_v25 (TMPro.TMP_MeshInfo)+28+v553 @ rcx_v28*4]");
									float num14 = 0f - num10;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									if ((nint)0 >= (nint)array9.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj25 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj26 = 0 + obj25;
									Vector3[][] array10 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									if ((nint)0 >= (nint)array10.Length)
									{
										break;
									}
									Vector3[] array11 = array10[(object)tMP_CharacterInfo2];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj27 = (nint)0 + (nint)1;
									Vector3[] normals = tMP_MeshInfo.normals;
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj27) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals))
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj28 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj29 = 0 + obj28;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v459 @ rdx_v25 (TMPro.TMP_MeshInfo)+34+v555 @ rcx_v30*4]");
									float num15 = 0f - num10;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj30 = (nint)0 + (nint)1;
									if ((nint)obj30 >= array11.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj31 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj32 = 0 + obj31;
									Vector3[][] array12 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									if ((nint)0 >= (nint)array12.Length)
									{
										break;
									}
									Vector3[] array13 = array12[(object)tMP_CharacterInfo2];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj33 = (nint)0 + (nint)2;
									Vector3[] normals2 = tMP_MeshInfo.normals;
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj33) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals2))
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj34 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj35 = 0 + obj34;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v459 @ rdx_v25 (TMPro.TMP_MeshInfo)+40+v557 @ rcx_v32*4]");
									float num16 = 0f - num10;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj36 = (nint)0 + (nint)2;
									if ((nint)obj36 >= array13.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj37 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj38 = 0 + obj37;
									Vector3[][] array14 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									if ((nint)0 >= (nint)array14.Length)
									{
										break;
									}
									Vector3[] array15 = array14[(object)tMP_CharacterInfo2];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj39 = (nint)0 + (nint)3;
									Vector3[] normals3 = tMP_MeshInfo.normals;
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj39) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals3))
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj40 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj41 = 0 + obj40;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v459 @ rdx_v25 (TMPro.TMP_MeshInfo)+4C+v559 @ rcx_v34*4]");
									float num17 = 0f - num10;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj42 = (nint)0 + (nint)3;
									if ((nint)obj42 >= array15.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj43 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj44 = 0 + obj43;
									float num18 = vertexShakeA2.ScaleMultiplier * num13;
									float num19 = vertexShakeA2.ScaleMultiplier * num13;
									float maxInclusive = num18 + 1.005f;
									float minInclusive = 0.995f - num19;
									float num20 = UnityEngine.Random.Range(minInclusive, maxInclusive);
									nint num21 = (nint)typeof(Vector3);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2325 @ rdx_v26 (Il2CppClass<UnityEngine.Vector3>)+B8]");
									nint num22 = 0;
									nint num23 = (nint)typeof(Vector3);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2340 @ rdx_v27 (Il2CppClass<UnityEngine.Vector3>)+B8]");
									nint num24 = 0;
									reference = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
									ref Vector3 pos = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 112));
									_ = Vector3.oneVector;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2326 @ rax_v46 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2344 @ rax_v48 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
									float num25 = 0f * num20;
									Matrix4x4 matrix4x = Matrix4x4.Internal_TRS(ref pos, ref q, ref reference);
									Vector3[][] array16 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									if ((nint)0 >= (nint)array16.Length)
									{
										break;
									}
									Vector3[] array17 = array16[(object)tMP_CharacterInfo2];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									if ((nint)0 >= (nint)array17.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj45 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj46 = 0 + obj45;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v490 @ r8_v29 (UnityEngine.Vector3[])+20+v2359 @ rcx_v41*4]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj47 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj48 = 0 + obj47;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-20]");
									object obj49 = 0 * quaternion;
									object obj50 = quaternion * quaternion;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v490 @ r8_v29 (UnityEngine.Vector3[])+28+v2359 @ rcx_v41*4]");
									object obj51 = 0 * quaternion;
									object obj52 = obj49 + obj50;
									object obj53 = obj52 + obj51;
									object obj54 = obj53 + (object)quaternion;
									Vector3[][] array18 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									if ((nint)0 >= (nint)array18.Length)
									{
										break;
									}
									Vector3[] array19 = array18[(object)tMP_CharacterInfo2];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj55 = (nint)0 + (nint)1;
									if ((nint)obj55 >= array19.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj56 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj57 = 0 + obj56;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj58 = (nint)0 + (nint)1;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v491 @ r8_v30 (UnityEngine.Vector3[])+2C+v1385 @ rcx_v44*4]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-10]");
									nint num26 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-50]");
									object obj59 = num26 * 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-40]");
									object obj60 = quaternion * 0;
									object obj61 = obj59 + obj60;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v491 @ r8_v30 (UnityEngine.Vector3[])+34+v1385 @ rcx_v44*4]");
									nint num27 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-30]");
									object obj62 = num27 * 0;
									object obj63 = obj61 + obj62;
									object obj64 = obj63 + (object)quaternion;
									if ((nint)obj58 >= array19.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj65 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj66 = 0 + obj65;
									Vector3[][] array20 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									if ((nint)0 >= (nint)array20.Length)
									{
										break;
									}
									Vector3[] array21 = array20[(object)tMP_CharacterInfo2];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj67 = (nint)0 + (nint)2;
									if ((nint)obj67 >= array21.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj68 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj69 = 0 + obj68;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj70 = (nint)0 + (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v492 @ r8_v31 (UnityEngine.Vector3[])+38+v1386 @ rcx_v47*4]");
									obj = 0;
									object obj71 = obj;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-50]");
									object obj72 = obj71 * 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-40]");
									object obj73 = quaternion * 0;
									object obj74 = obj72 + obj73;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v492 @ r8_v31 (UnityEngine.Vector3[])+40+v1386 @ rcx_v47*4]");
									nint num28 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-30]");
									object obj75 = num28 * 0;
									object obj76 = obj74 + obj75;
									object obj77 = obj76 + (object)quaternion;
									if ((nint)obj70 >= array21.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj78 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj79 = 0 + obj78;
									Vector3[][] array22 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									if ((nint)0 >= (nint)array22.Length)
									{
										break;
									}
									array7 = array22[(object)tMP_CharacterInfo2];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj80 = (nint)0 + (nint)3;
									if ((nint)obj80 >= array7.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj81 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj82 = 0 + obj81;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj83 = (nint)0 + (nint)3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2179 @ r8_v18 (UnityEngine.Vector3[])+44+v1387 @ rcx_v50*4]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+10]");
									nint num29 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-50]");
									object obj84 = num29 * 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-40]");
									object obj85 = quaternion * 0;
									object obj86 = obj84 + obj85;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2179 @ r8_v18 (UnityEngine.Vector3[])+4C+v1387 @ rcx_v50*4]");
									nint num30 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-30]");
									object obj87 = num30 * 0;
									object obj88 = obj86 + obj87;
									obj18 = obj88 + (object)quaternion;
									if ((nint)obj83 >= array7.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj89 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj90 = 0 + obj89;
									Vector3[][] array23 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									if ((nint)0 >= (nint)array23.Length)
									{
										break;
									}
									Vector3[] array24 = array23[(object)tMP_CharacterInfo2];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									if ((nint)0 >= (nint)array24.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj91 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj92 = 0 + obj91;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v462 @ rdx_v30 (UnityEngine.Vector3[])+28+v565 @ rcx_v52*4]");
									float num31 = 0f + num10;
									Vector3[][] array25 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									if ((nint)0 >= (nint)array25.Length)
									{
										break;
									}
									Vector3[] array26 = array25[(object)tMP_CharacterInfo2];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj93 = (nint)0 + (nint)1;
									if ((nint)obj93 >= array26.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj94 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj95 = 0 + obj94;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v464 @ rdx_v32 (UnityEngine.Vector3[])+34+v566 @ rcx_v53*4]");
									float num32 = 0f + num10;
									Vector3[][] array27 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									if ((nint)0 >= (nint)array27.Length)
									{
										break;
									}
									Vector3[] array28 = array27[(object)tMP_CharacterInfo2];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj96 = (nint)0 + (nint)2;
									if ((nint)obj96 >= array28.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj97 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj98 = 0 + obj97;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v466 @ rdx_v34 (UnityEngine.Vector3[])+40+v567 @ rcx_v54*4]");
									float num33 = 0f + num10;
									Vector3[][] array29 = _003CcopyOfVertices_003E5__3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ rdi_v14 (TMPro.TMP_CharacterInfo[])+50+v423 @ r15_v8]");
									if ((nint)0 >= (nint)array29.Length)
									{
										break;
									}
									array6 = array29[(object)tMP_CharacterInfo2];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj99 = (nint)0 + (nint)3;
									if ((nint)obj99 >= array6.Length)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									object obj100 = (nint)0 * (nint)2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v423 @ r15_v8]");
									obj16 = 0 + obj100;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+170]");
									vertexShakeA2 = (VertexShakeA)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2178 @ rdx_v23 (UnityEngine.Vector3[])+4C+v2184 @ rcx_v25*4]");
									num6 = 0f + num10;
									q = quaternion;
									num13 = 0.001f;
								}
								obj13++;
								obj19++;
								object obj4 = obj17 + 376;
								object obj101 = obj19;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2094 @ rdx_v18+40+v550 @ rcx_v20 (TMPro.TMP_LineInfo[])]");
								bool flag5 = (nint)obj101 <= 0;
								num5 = num13;
								tMP_CharacterInfo = tMP_CharacterInfo2;
								vertexShakeA = vertexShakeA2;
								obj17 = obj4;
								if (flag5)
								{
									continue;
								}
								goto IL_15cb;
							}
							break;
						}
						goto IL_15cb;
						IL_15cb:
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+160]");
						num8 = (int)((nint)0 + (nint)1);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+178]");
						num7 = (int)((nint)0 + (nint)1);
						bool flag6 = num7 < tMP_TextInfo5.lineCount;
						euler = (Vector3)0;
						num6 = 0.5f;
						if (flag6)
						{
							continue;
						}
						goto IL_1634;
					}
					goto IL_189b;
				}
				goto IL_18bb;
			}
			WaitForSeconds waitForSeconds = new WaitForSeconds(0.25f);
			_003C_003E2__current = waitForSeconds;
			_003C_003E1__state = 1;
			goto IL_198d;
			IL_189b:
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
			return (byte)(int)ex != 0;
			IL_182f:
			throw new NullReferenceException();
			IL_1634:
			obj6 = 32;
			num = 0;
			goto IL_18bb;
			IL_198d:
			return true;
			IL_18bb:
			TMP_TextInfo tMP_TextInfo12 = _003CtextInfo_003E5__2;
			int num34 = num;
			int num35 = num;
			while (true)
			{
				TMP_MeshInfo[] meshInfo6 = tMP_TextInfo12.meshInfo;
				if (num35 >= meshInfo6.Length)
				{
					break;
				}
				TMP_TextInfo tMP_TextInfo13 = _003CtextInfo_003E5__2;
				TMP_MeshInfo[] meshInfo7 = tMP_TextInfo13.meshInfo;
				if (num < meshInfo7.Length)
				{
					Vector3[][] array30 = _003CcopyOfVertices_003E5__3;
					if (num < array30.Length)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v537 @ rax_v20 (TMPro.TMP_MeshInfo[])+20+v443 @ rbx_v7 (System.Int32)]");
						nint num36 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v470 @ rdx_v10 (UnityEngine.Vector3[][])+v373 @ r12_v5]");
						((Mesh)num36).vertices = (Vector3[])0;
						TMP_TextInfo tMP_TextInfo14 = _003CtextInfo_003E5__2;
						TMP_MeshInfo[] meshInfo8 = tMP_TextInfo14.meshInfo;
						if (num < meshInfo8.Length)
						{
							TMP_Text textComponent2 = vertexShakeA.m_TextComponent;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v472 @ rdx_v13 (TMPro.TMP_MeshInfo[])+20+v443 @ rbx_v7 (System.Int32)]");
							textComponent2.UpdateGeometry((Mesh)0, num);
							tMP_TextInfo12 = _003CtextInfo_003E5__2;
							num++;
							obj6 += 8;
							num34 += 80;
							bool flag7 = _003CtextInfo_003E5__2 != null;
							num35 = num;
							if (flag7)
							{
								continue;
							}
							goto IL_182f;
						}
					}
				}
				goto IL_189b;
			}
			WaitForSeconds waitForSeconds2 = new WaitForSeconds(0.1f);
			_003C_003E2__current = waitForSeconds2;
			_003C_003E1__state = 2;
			goto IL_198d;
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

	public float ScaleMultiplier = 1f;

	public float RotationMultiplier = 1f;

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
		_003CAnimateVertexColors_003Ed__11 obj = new _003CAnimateVertexColors_003Ed__11(0);
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
		_003CAnimateVertexColors_003Ed__11 obj = new _003CAnimateVertexColors_003Ed__11(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
