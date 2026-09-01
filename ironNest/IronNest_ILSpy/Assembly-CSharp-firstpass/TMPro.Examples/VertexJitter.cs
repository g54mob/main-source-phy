using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class VertexJitter : MonoBehaviour
{
	private struct VertexAnim
	{
		public float angleRange;

		public float angle;

		public float speed;
	}

	private sealed class _003CAnimateVertexColors_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public VertexJitter _003C_003E4__this;

		private TMP_TextInfo _003CtextInfo_003E5__2;

		private int _003CloopCount_003E5__3;

		private VertexAnim[] _003CvertexAnim_003E5__4;

		private TMP_MeshInfo[] _003CcachedMeshInfo_003E5__5;

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
			//IL_0096: Expected I4, but got I8
			//IL_10b5: Expected I4, but got O
			//IL_001d: Expected O, but got I4
			//IL_0065: Expected I4, but got I8
			//IL_00ec: Expected I, but got O
			//IL_00fc: Expected O, but got I
			//IL_0196: Expected O, but got I4
			//IL_019f: Expected O, but got I4
			//IL_0376: Expected O, but got I4
			//IL_037e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0383: Expected O, but got Unknown
			//IL_03aa: Expected O, but got I4
			//IL_03bc: Expected O, but got I4
			//IL_03ce: Expected O, but got I4
			//IL_0e26: Expected O, but got I4
			//IL_0e2f: Expected O, but got I4
			//IL_0e38: Expected O, but got I4
			//IL_0206: Unknown result type (might be due to invalid IL or missing references)
			//IL_020b: Expected O, but got Unknown
			//IL_0214: Unknown result type (might be due to invalid IL or missing references)
			//IL_0219: Expected O, but got Unknown
			//IL_0403: Expected O, but got Ref
			//IL_112f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1134: Expected O, but got Unknown
			//IL_114a: Expected O, but got I
			//IL_1153: Unknown result type (might be due to invalid IL or missing references)
			//IL_1158: Expected O, but got Unknown
			//IL_1161: Unknown result type (might be due to invalid IL or missing references)
			//IL_1166: Expected O, but got Unknown
			//IL_0e18: Expected O, but got I
			//IL_0f0f: Expected O, but got I
			//IL_0f0f: Expected O, but got I
			//IL_0523: Expected O, but got I
			//IL_0533: Unknown result type (might be due to invalid IL or missing references)
			//IL_0538: Expected O, but got Unknown
			//IL_0555: Expected O, but got I
			//IL_058a: Expected O, but got I
			//IL_0fa7: Expected I4, but got O
			//IL_0fa7: Expected O, but got I
			//IL_0fba: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fbf: Expected O, but got Unknown
			//IL_0fc8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fcd: Expected O, but got Unknown
			//IL_061b: Expected O, but got I
			//IL_062b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0630: Expected O, but got Unknown
			//IL_064d: Expected O, but got I
			//IL_0668: Expected O, but got I
			//IL_0678: Unknown result type (might be due to invalid IL or missing references)
			//IL_067d: Expected O, but got Unknown
			//IL_06b8: Expected O, but got I
			//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_06cd: Expected O, but got Unknown
			//IL_06fa: Expected O, but got I
			//IL_070a: Unknown result type (might be due to invalid IL or missing references)
			//IL_070f: Expected O, but got Unknown
			//IL_072a: Expected O, but got I
			//IL_073a: Unknown result type (might be due to invalid IL or missing references)
			//IL_073f: Expected O, but got Unknown
			//IL_076c: Expected O, but got I
			//IL_077c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0781: Expected O, but got Unknown
			//IL_079c: Expected O, but got I
			//IL_07ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_07b1: Expected O, but got Unknown
			//IL_07de: Expected O, but got I
			//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_07f3: Expected O, but got Unknown
			//IL_080e: Expected O, but got I
			//IL_081e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0823: Expected O, but got Unknown
			//IL_08a9: Invalid comparison between I4 and F4
			//IL_08f2: Expected F4, but got I4
			//IL_11ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_11b1: Expected O, but got Unknown
			//IL_11ca: Invalid comparison between I4 and F4
			//IL_092e: Expected F4, but got I4
			//IL_121f: Expected O, but got I
			//IL_0951: Unknown result type (might be due to invalid IL or missing references)
			//IL_0956: Expected O, but got Unknown
			//IL_0973: Expected O, but got I
			//IL_097c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0981: Expected O, but got Unknown
			//IL_0a26: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a2b: Expected O, but got Unknown
			//IL_0a62: Expected O, but got I
			//IL_0aa5: Expected O, but got I
			//IL_0ad1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ad6: Expected O, but got Unknown
			//IL_0afb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b00: Expected O, but got Unknown
			//IL_0b37: Expected O, but got I
			//IL_0b7a: Expected O, but got I
			//IL_0ba6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bab: Expected O, but got Unknown
			//IL_0bd0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd5: Expected O, but got Unknown
			//IL_0c0d: Expected O, but got I
			//IL_0c58: Expected O, but got I
			//IL_0c87: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c8c: Expected O, but got Unknown
			//IL_0cb1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cb6: Expected O, but got Unknown
			//IL_0ce3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ce8: Expected O, but got Unknown
			//IL_0d15: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d1a: Expected O, but got Unknown
			//IL_0d56: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d5b: Expected O, but got Unknown
			//IL_0d78: Expected F4, but got I
			//IL_0dce: Expected O, but got F4
			//IL_0de1: Expected O, but got I4
			//IL_0e03: Expected O, but got Ref
			object obj2 = default(object);
			object obj = (object)(&obj2);
			VertexJitter vertexJitter = _003C_003E4__this;
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
				if ((object)vertexJitter != null)
				{
					goto IL_027d;
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					TMP_Text textComponent = vertexJitter.m_TextComponent;
					if ((object)vertexJitter.m_TextComponent != null)
					{
						nint num = (nint)textComponent;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v730 @ r9_v14 (Il2CppClass<TMPro.TMP_Text>)+7E0]");
						object obj4 = 0;
						vertexJitter.m_TextComponent.ForceMeshUpdate();
						if ((object)vertexJitter.m_TextComponent != null)
						{
							TMP_TextInfo textInfo = vertexJitter.m_TextComponent.textInfo;
							_003CtextInfo_003E5__2 = textInfo;
							_003CloopCount_003E5__3 = 0;
							vertexJitter.hasTextChanged = true;
							VertexAnim[] array = new VertexAnim[1024];
							_003CvertexAnim_003E5__4 = array;
							Vector3 vector = (Vector3)0;
							Vector3 vector2 = (Vector3)0;
							while (_003CvertexAnim_003E5__4 != null)
							{
								float num2 = UnityEngine.Random.Range(10f, 25f);
								if (_003CvertexAnim_003E5__4 == null)
								{
									break;
								}
								float num3 = UnityEngine.Random.Range(1f, 3f);
								vector2++;
								vector += 12;
								if ((nint)vector < 12288)
								{
									continue;
								}
								goto IL_0236;
							}
						}
					}
				}
			}
			goto IL_10a7;
			IL_0236:
			if (_003CtextInfo_003E5__2 != null)
			{
				TMP_MeshInfo[] array2 = _003CtextInfo_003E5__2.CopyMeshInfoVertexData();
				_003CcachedMeshInfo_003E5__5 = array2;
				float num4 = 3f;
				goto IL_027d;
			}
			goto IL_10a7;
			IL_027d:
			if (vertexJitter.hasTextChanged)
			{
				if (_003CtextInfo_003E5__2 == null)
				{
					goto IL_10a7;
				}
				TMP_MeshInfo[] array3 = _003CtextInfo_003E5__2.CopyMeshInfoVertexData();
				_003CcachedMeshInfo_003E5__5 = array3;
				vertexJitter.hasTextChanged = false;
			}
			TMP_TextInfo tMP_TextInfo = _003CtextInfo_003E5__2;
			if (_003CtextInfo_003E5__2 != null)
			{
				int num5 = tMP_TextInfo.characterCount ^ tMP_TextInfo.characterCount;
				int num6 = tMP_TextInfo.characterCount & num5;
				bool flag2 = num6 < 0;
				bool flag3 = tMP_TextInfo.characterCount < 0;
				bool flag4 = tMP_TextInfo.characterCount == 0;
				if (flag4)
				{
					WaitForSeconds waitForSeconds = new WaitForSeconds(0.25f);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					goto IL_12b3;
				}
				bool flag5 = flag3 == flag2;
				object obj5 = !flag5;
				object obj6 = obj5 | flag4;
				if (obj6 != null)
				{
					goto IL_10fd;
				}
				_ = 0;
				float num7 = 25f;
				Vector3 vector3 = (Vector3)0;
				float num8 = 2f;
				Vector3 vector4 = (Vector3)0;
				float num9 = 0.5f;
				Vector3 vector5 = (Vector3)0;
				float num11 = default(float);
				Vector3 euler = default(Vector3);
				Quaternion q = default(Quaternion);
				Vector3 s = default(Vector3);
				while (true)
				{
					TMP_TextInfo tMP_TextInfo2 = _003CtextInfo_003E5__2;
					if (_003CtextInfo_003E5__2 == null || tMP_TextInfo2.characterInfo == null)
					{
						break;
					}
					object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 144));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800039A0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+200]");
					if ((nint)0 != 0)
					{
						VertexAnim[] array4 = _003CvertexAnim_003E5__4;
						if (_003CvertexAnim_003E5__4 == null)
						{
							break;
						}
						TMP_TextInfo tMP_TextInfo3 = _003CtextInfo_003E5__2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v285 @ r12_v8 (UnityEngine.Vector3)+20+v400 @ rax_v31 (VertexAnim[])]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v285 @ r12_v8 (UnityEngine.Vector3)+28+v400 @ rax_v31 (VertexAnim[])]");
						_ = 0;
						if (_003CtextInfo_003E5__2 == null)
						{
							break;
						}
						TMP_CharacterInfo[] characterInfo = tMP_TextInfo3.characterInfo;
						if (tMP_TextInfo3.characterInfo == null)
						{
							break;
						}
						TMP_TextInfo tMP_TextInfo4 = _003CtextInfo_003E5__2;
						TMP_MeshInfo[] array5 = _003CcachedMeshInfo_003E5__5;
						TMP_CharacterInfo[] characterInfo2 = tMP_TextInfo4.characterInfo;
						if (_003CcachedMeshInfo_003E5__5 == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v377 @ r8_v16 (TMPro.TMP_CharacterInfo[])+50+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj8 = (nint)0 * (nint)4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v377 @ r8_v16 (TMPro.TMP_CharacterInfo[])+50+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj9 = 0 + obj8;
						object obj10 = obj9 + obj9;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v357 @ rdx_v21 (TMPro.TMP_MeshInfo[])+30+v418 @ rcx_v28*8]");
						TMP_MeshInfo tMP_MeshInfo = (TMP_MeshInfo)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v357 @ rdx_v21 (TMPro.TMP_MeshInfo[])+30+v418 @ rcx_v28*8]");
						if ((nint)0 == 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						Vector3 vector2 = (Vector3)0;
						TMP_TextInfo tMP_TextInfo5 = _003CtextInfo_003E5__2;
						float num10 = num11 + num11;
						float num12 = num10 * num9;
						if (_003CtextInfo_003E5__2 == null)
						{
							break;
						}
						TMP_MeshInfo[] meshInfo = tMP_TextInfo5.meshInfo;
						if (tMP_TextInfo5.meshInfo == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v377 @ r8_v16 (TMPro.TMP_CharacterInfo[])+50+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj11 = (nint)0 * (nint)4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v377 @ r8_v16 (TMPro.TMP_CharacterInfo[])+50+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj12 = 0 + obj11;
						object obj13 = obj12 + obj12;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v329 @ rbx_v16 (TMPro.TMP_MeshInfo[])+30+v1276 @ rcx_v31*8]");
						Vector3 vector = (Vector3)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj14 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj15 = 0 + obj14;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v329 @ rbx_v16 (TMPro.TMP_MeshInfo[])+30+v1276 @ rcx_v31*8]");
						if ((nint)0 == 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj16 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj17 = 0 + obj16;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v358 @ rdx_v22 (TMPro.TMP_MeshInfo)+28+v420 @ rcx_v32*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj18 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj19 = 0 + obj18;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj20 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj21 = 0 + obj20;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v358 @ rdx_v22 (TMPro.TMP_MeshInfo)+34+v1278 @ rcx_v34*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj22 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj23 = 0 + obj22;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj24 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj25 = 0 + obj24;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v358 @ rdx_v22 (TMPro.TMP_MeshInfo)+40+v1280 @ rcx_v36*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj26 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj27 = 0 + obj26;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj28 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rdi_v10 (TMPro.TMP_CharacterInfo[])+64+v276 @ r13_v9 (UnityEngine.Vector3)]");
						object obj29 = 0 + obj28;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v358 @ rdx_v22 (TMPro.TMP_MeshInfo)+4C+v1282 @ rcx_v38*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm6,dword ptr [rsi+30h]\"");
						float num13 = 0f / num7;
						float num14 = num13;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v285 @ r12_v8 (UnityEngine.Vector3)+28+v400 @ rax_v31 (VertexAnim[])]");
						float num15 = num14 * 0f;
						float x = num15 * num9;
						float num16 = MathF.Floor(x);
						float num17 = num16 + num16;
						float num18 = num15 - num17;
						if (!(0f > num18))
						{
							if (num18 > num8)
							{
								num18 = num8;
							}
						}
						else
						{
							num18 = 0f;
						}
						float num19 = num18 - 1f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
						object obj30 = num19 & 0;
						float num20 = 1f - (float)obj30;
						if (!(0f > num20))
						{
							if (num20 > 1f)
							{
								num20 = 1f;
							}
						}
						else
						{
							num20 = 0f;
						}
						float num21 = UnityEngine.Random.Range(-0.25f, 0.25f);
						float num22 = UnityEngine.Random.Range(-0.25f, 0.25f);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+300]");
						object obj31 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1817 @ rax_v41+28]");
						float num23 = 0f * 0f;
						float num24 = UnityEngine.Random.Range(-5f, 5f);
						Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
						Matrix4x4 matrix4x = Matrix4x4.Internal_TRS(ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128)), ref q, ref s);
						object obj32 = vector2 * 2;
						object obj33 = (object)vector2 + obj32;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v995 @ rbx_v14 (UnityEngine.Vector3)+20+v1858 @ rcx_v43*4]");
						obj = 0;
						object obj34 = vector2 * 2;
						object obj35 = (object)vector2 + obj34;
						float num25 = (float)obj * num11;
						float num26 = num11 * num11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v995 @ rbx_v14 (UnityEngine.Vector3)+28+v1858 @ rcx_v43*4]");
						float num27 = 0f * num11;
						float num28 = num25 + num26;
						float num29 = num28 + num27;
						float num30 = num29 + num11;
						object obj36 = vector2 * 2;
						object obj37 = (object)vector2 + obj36;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v995 @ rbx_v14 (UnityEngine.Vector3)+2C+v1285 @ rcx_v45*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+10]");
						nint num31 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-30]");
						object obj38 = num31 * 0;
						float num32 = num11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-20]");
						float num33 = num32 * 0f;
						float num34 = (float)obj38 + num33;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v995 @ rbx_v14 (UnityEngine.Vector3)+34+v1285 @ rcx_v45*4]");
						nint num35 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-10]");
						object obj39 = num35 * 0;
						float num36 = num34 + (float)obj39;
						float num37 = num36 + num11;
						object obj40 = vector2 * 2;
						object obj41 = (object)vector2 + obj40;
						object obj42 = vector2 * 2;
						object obj43 = (object)vector2 + obj42;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v995 @ rbx_v14 (UnityEngine.Vector3)+38+v1287 @ rcx_v47*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+20]");
						nint num38 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-30]");
						object obj44 = num38 * 0;
						float num39 = num11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-20]");
						float num40 = num39 * 0f;
						float num41 = (float)obj44 + num40;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v995 @ rbx_v14 (UnityEngine.Vector3)+40+v1287 @ rcx_v47*4]");
						nint num42 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-10]");
						object obj45 = num42 * 0;
						float num43 = num41 + (float)obj45;
						float num44 = num43 + num11;
						object obj46 = vector2 * 2;
						object obj47 = (object)vector2 + obj46;
						object obj48 = vector2 * 2;
						object obj49 = (object)vector2 + obj48;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v995 @ rbx_v14 (UnityEngine.Vector3)+44+v1289 @ rcx_v49*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+30]");
						nint num45 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-30]");
						object obj50 = num45 * 0;
						float num46 = num11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-20]");
						float num47 = num46 * 0f;
						float num48 = (float)obj50 + num47;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v995 @ rbx_v14 (UnityEngine.Vector3)+4C+v1289 @ rcx_v49*4]");
						nint num49 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-10]");
						object obj51 = num49 * 0;
						float num50 = num48 + (float)obj51;
						float num51 = num50 + num11;
						object obj52 = vector2 * 2;
						object obj53 = (object)vector2 + obj52;
						object obj54 = vector2 * 2;
						object obj55 = (object)vector2 + obj54;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v995 @ rbx_v14 (UnityEngine.Vector3)+28+v1291 @ rcx_v51*4]");
						_ = 0;
						object obj56 = vector2 * 2;
						object obj57 = (object)vector2 + obj56;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v995 @ rbx_v14 (UnityEngine.Vector3)+34+v1292 @ rcx_v52*4]");
						_ = 0;
						object obj58 = vector2 * 2;
						object obj59 = (object)vector2 + obj58;
						float num52 = num12 + num11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v995 @ rbx_v14 (UnityEngine.Vector3)+40+v1293 @ rcx_v53*4]");
						_ = 0;
						object obj60 = vector2 * 2;
						object obj61 = (object)vector2 + obj60;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v995 @ rbx_v14 (UnityEngine.Vector3)+44+v421 @ rcx_v54*4]");
						float num4 = 0f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v995 @ rbx_v14 (UnityEngine.Vector3)+4C+v421 @ rcx_v54*4]");
						_ = 0;
						if (_003CvertexAnim_003E5__4 == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+2F0]");
						_ = 0;
						s = Vector3.oneVector;
						q = (Quaternion)num11;
						num7 = 25f;
						euler = (Vector3)0;
						num8 = 2f;
						num9 = 0.5f;
						float num3 = num11;
						object obj4 = (object)(&s);
					}
					vector5++;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+308]");
					object obj62 = (nint)0 + (nint)1;
					vector3 += 376;
					vector4 += 12;
					if ((nint)obj62 < tMP_TextInfo.characterCount)
					{
						continue;
					}
					goto IL_0e08;
				}
			}
			goto IL_10a7;
			IL_10a7:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0ffc:
			int num53 = _003CloopCount_003E5__3 + 1;
			_003CloopCount_003E5__3 = num53;
			WaitForSeconds waitForSeconds2 = new WaitForSeconds(0.1f);
			_003C_003E2__current = waitForSeconds2;
			_003C_003E1__state = 2;
			goto IL_12b3;
			IL_12b3:
			return true;
			IL_0e08:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+300]");
			vertexJitter = (VertexJitter)0;
			goto IL_10fd;
			IL_10fd:
			TMP_TextInfo tMP_TextInfo6 = _003CtextInfo_003E5__2;
			if (_003CtextInfo_003E5__2 != null)
			{
				Vector3 vector6 = (Vector3)0;
				Vector3 vector7 = (Vector3)0;
				Vector3 vector8 = (Vector3)0;
				while (true)
				{
					TMP_MeshInfo[] meshInfo2 = tMP_TextInfo6.meshInfo;
					if (tMP_TextInfo6.meshInfo == null)
					{
						break;
					}
					if ((nint)vector8 < meshInfo2.Length)
					{
						TMP_TextInfo tMP_TextInfo7 = _003CtextInfo_003E5__2;
						if (_003CtextInfo_003E5__2 == null)
						{
							break;
						}
						TMP_MeshInfo[] meshInfo3 = tMP_TextInfo7.meshInfo;
						if (tMP_TextInfo7.meshInfo == null)
						{
							break;
						}
						TMP_TextInfo tMP_TextInfo8 = _003CtextInfo_003E5__2;
						TMP_MeshInfo[] meshInfo4 = tMP_TextInfo8.meshInfo;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v331 @ rbx_v10 (UnityEngine.Vector3)+20+v424 @ rcx_v19 (TMPro.TMP_MeshInfo[])]");
						if ((nint)0 == 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v331 @ rbx_v10 (UnityEngine.Vector3)+20+v424 @ rcx_v19 (TMPro.TMP_MeshInfo[])]");
						nint num54 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v331 @ rbx_v10 (UnityEngine.Vector3)+30+v361 @ rdx_v12 (TMPro.TMP_MeshInfo[])]");
						((Mesh)num54).vertices = (Vector3[])0;
						TMP_TextInfo tMP_TextInfo9 = _003CtextInfo_003E5__2;
						if (_003CtextInfo_003E5__2 == null)
						{
							break;
						}
						TMP_MeshInfo[] meshInfo5 = tMP_TextInfo9.meshInfo;
						if (tMP_TextInfo9.meshInfo == null || (object)vertexJitter.m_TextComponent == null)
						{
							break;
						}
						TMP_Text textComponent2 = vertexJitter.m_TextComponent;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v331 @ rbx_v10 (UnityEngine.Vector3)+20+v363 @ rdx_v15 (TMPro.TMP_MeshInfo[])]");
						textComponent2.UpdateGeometry((Mesh)0, (int)vector6);
						tMP_TextInfo6 = _003CtextInfo_003E5__2;
						vector6++;
						vector7 += 80;
						bool flag6 = _003CtextInfo_003E5__2 != null;
						vector8 = vector6;
						if (!flag6)
						{
							break;
						}
						continue;
					}
					goto IL_0ffc;
				}
			}
			goto IL_10a7;
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
		_003CAnimateVertexColors_003Ed__11 obj = new _003CAnimateVertexColors_003Ed__11(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private void ON_TEXT_CHANGED(UnityEngine.Object obj)
	{
		if (obj == m_TextComponent)
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
