using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class WarpTextExample : MonoBehaviour
{
	private sealed class _003CWarpText_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WarpTextExample _003C_003E4__this;

		private float _003Cold_CurveScale_003E5__2;

		private AnimationCurve _003Cold_curve_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CWarpText_003Ed__8(int _003C_003E1__state)
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
			//IL_0079: Expected I4, but got I8
			//IL_001d: Expected O, but got I4
			//IL_0065: Expected I4, but got I8
			//IL_02a9: Expected I, but got O
			//IL_02b9: Expected O, but got I
			//IL_101a: Expected I4, but got O
			//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d5: Expected O, but got Unknown
			//IL_03c6: Expected O, but got I4
			//IL_03d8: Expected O, but got I4
			//IL_03e2: Expected O, but got I4
			//IL_0226: Unknown result type (might be due to invalid IL or missing references)
			//IL_022b: Expected O, but got Unknown
			//IL_023d: Invalid comparison between O and F4
			//IL_1035: Unknown result type (might be due to invalid IL or missing references)
			//IL_103a: Expected O, but got Unknown
			//IL_1043: Unknown result type (might be due to invalid IL or missing references)
			//IL_1048: Expected O, but got Unknown
			//IL_1051: Unknown result type (might be due to invalid IL or missing references)
			//IL_1056: Expected O, but got Unknown
			//IL_047e: Expected O, but got I
			//IL_048e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0493: Expected O, but got Unknown
			//IL_04c5: Expected O, but got I
			//IL_050a: Expected O, but got I
			//IL_0554: Expected O, but got I
			//IL_0564: Unknown result type (might be due to invalid IL or missing references)
			//IL_0569: Expected O, but got Unknown
			//IL_057f: Expected O, but got I
			//IL_058f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0594: Expected O, but got Unknown
			//IL_05b1: Expected O, but got I
			//IL_05c7: Expected O, but got I
			//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05dc: Expected O, but got Unknown
			//IL_05f2: Expected O, but got I
			//IL_0660: Expected O, but got I
			//IL_0670: Unknown result type (might be due to invalid IL or missing references)
			//IL_0675: Expected O, but got Unknown
			//IL_068b: Expected O, but got I
			//IL_06e9: Expected O, but got I
			//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_06fe: Expected O, but got Unknown
			//IL_0714: Expected O, but got I
			//IL_0772: Expected O, but got I
			//IL_0782: Unknown result type (might be due to invalid IL or missing references)
			//IL_0787: Expected O, but got Unknown
			//IL_07be: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c3: Expected O, but got Unknown
			//IL_0872: Expected O, but got Ref
			//IL_089c: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a1: Expected O, but got Unknown
			//IL_08fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0901: Expected O, but got Unknown
			//IL_0951: Unknown result type (might be due to invalid IL or missing references)
			//IL_0956: Expected O, but got Unknown
			//IL_096c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0971: Expected O, but got Unknown
			//IL_0981: Expected O, but got I
			//IL_098a: Unknown result type (might be due to invalid IL or missing references)
			//IL_098f: Expected O, but got Unknown
			//IL_09da: Unknown result type (might be due to invalid IL or missing references)
			//IL_09df: Expected O, but got Unknown
			//IL_0a45: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a4a: Expected O, but got Unknown
			//IL_0a60: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a65: Expected O, but got Unknown
			//IL_0a8f: Expected O, but got I
			//IL_0a9f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa4: Expected O, but got Unknown
			//IL_0ace: Expected O, but got I
			//IL_0b18: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b1d: Expected O, but got Unknown
			//IL_0b33: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b38: Expected O, but got Unknown
			//IL_0b72: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b77: Expected O, but got Unknown
			//IL_0b8d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b92: Expected O, but got Unknown
			//IL_0bbc: Expected O, but got I
			//IL_0bcc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd1: Expected O, but got Unknown
			//IL_0bfb: Expected O, but got I
			//IL_0c45: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c4a: Expected O, but got Unknown
			//IL_0c60: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c65: Expected O, but got Unknown
			//IL_0c9f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ca4: Expected O, but got Unknown
			//IL_0cba: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cbf: Expected O, but got Unknown
			//IL_0ce9: Expected O, but got I
			//IL_0cfa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cff: Expected O, but got Unknown
			//IL_0d2c: Expected O, but got I
			//IL_0d78: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d7d: Expected O, but got Unknown
			//IL_0dc4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dc9: Expected O, but got Unknown
			//IL_0ddf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0de4: Expected O, but got Unknown
			//IL_0e26: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e2b: Expected O, but got Unknown
			//IL_0e41: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e46: Expected O, but got Unknown
			//IL_0e88: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e8d: Expected O, but got Unknown
			//IL_0ea3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ea8: Expected O, but got Unknown
			//IL_0ef2: Expected O, but got I
			//IL_0efb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f00: Expected O, but got Unknown
			//IL_0f1e: Expected O, but got I
			//IL_0f41: Expected O, but got I
			//IL_0f54: Expected O, but got I4
			//IL_0f76: Expected O, but got Ref
			object obj2 = default(object);
			object obj = (object)(&obj2);
			WarpTextExample warpTextExample = _003C_003E4__this;
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
				warpTextExample.VertexCurve.preWrapMode = WrapMode.Once;
				warpTextExample.VertexCurve.postWrapMode = WrapMode.Once;
				warpTextExample.m_TextComponent.havePropertiesChanged = true;
				_003Cold_CurveScale_003E5__2 = (warpTextExample.CurveScale *= 10f);
				AnimationCurve vertexCurve = warpTextExample.VertexCurve;
				AnimationCurve animationCurve = new AnimationCurve();
				Keyframe[] keys = warpTextExample.VertexCurve.keys;
				animationCurve.keys = keys;
				_003Cold_curve_003E5__3 = animationCurve;
			}
			Vector3 vector = default(Vector3);
			Vector3 vector2 = default(Vector3);
			Vector3 vector3 = default(Vector3);
			object obj39 = default(object);
			Vector3 euler = default(Vector3);
			Quaternion q = default(Quaternion);
			Vector3 s = default(Vector3);
			while (true)
			{
				TMP_Text textComponent = warpTextExample.m_TextComponent;
				if (!textComponent.m_havePropertiesChanged)
				{
					float num = _003Cold_CurveScale_003E5__2;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803B2C6Ah\"");
					if (_003Cold_CurveScale_003E5__2 == warpTextExample.CurveScale)
					{
						Keyframe[] keys2 = _003Cold_curve_003E5__3.keys;
						if (keys2.Length > 1)
						{
							object obj4 = keys2 + 60;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
							Keyframe[] keys3 = warpTextExample.VertexCurve.keys;
							if (keys3.Length > 1)
							{
								object obj5 = keys3 + 60;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
								bool flag2 = (object)vector == (object)num;
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803B2C6Ah\"");
								if (!flag2)
								{
									goto IL_025b;
								}
								_003C_003E2__current = null;
								_003C_003E1__state = 1;
								break;
							}
						}
						goto IL_100c;
					}
				}
				goto IL_025b;
				IL_0f7c:
				warpTextExample.m_TextComponent.UpdateVertexData();
				WaitForSeconds waitForSeconds = new WaitForSeconds(0.025f);
				_003C_003E2__current = waitForSeconds;
				_003C_003E1__state = 2;
				break;
				IL_100c:
				IndexOutOfRangeException ex = new IndexOutOfRangeException();
				return (byte)(int)ex != 0;
				IL_025b:
				_003Cold_CurveScale_003E5__2 = warpTextExample.CurveScale;
				AnimationCurve animationCurve2 = warpTextExample.CopyAnimationCurve(warpTextExample.VertexCurve);
				_003Cold_curve_003E5__3 = animationCurve2;
				TMP_Text textComponent2 = warpTextExample.m_TextComponent;
				nint num2 = (nint)textComponent2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1289 @ r9_v6 (Il2CppClass<TMPro.TMP_Text>)+7E0]");
				object obj6 = 0;
				textComponent2.ForceMeshUpdate();
				TMP_TextInfo textInfo = warpTextExample.m_TextComponent.textInfo;
				int characterCount = textInfo.characterCount;
				if (textInfo.characterCount == 0)
				{
					continue;
				}
				Bounds bounds = warpTextExample.m_TextComponent.bounds;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v321 @ rax_v19 (UnityEngine.Bounds)+10]");
				_ = 0;
				object obj7 = bounds.m_Center - vector2;
				Bounds bounds2 = warpTextExample.m_TextComponent.bounds;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1313 @ rax_v20 (UnityEngine.Bounds)+10]");
				_ = 0;
				object obj8 = bounds2.m_Center + vector2;
				if (textInfo.characterCount <= 0)
				{
					goto IL_0f7c;
				}
				_ = 0;
				_ = textInfo.characterCount;
				object obj9 = 0;
				float num3 = -0f;
				object obj10 = 0;
				object obj11 = 0;
				while (true)
				{
					TMP_CharacterInfo[] characterInfo = textInfo.characterInfo;
					if ((nint)obj10 >= characterInfo.Length)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+190+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
					if ((nint)0 != 0)
					{
						TMP_MeshInfo[] meshInfo = textInfo.meshInfo;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+50+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						if ((nint)0 >= (nint)meshInfo.Length)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+50+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj12 = (nint)0 * (nint)4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+50+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj13 = 0 + obj12;
						object obj14 = obj13 + obj13;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v257 @ rbx_v13 (TMPro.TMP_MeshInfo[])+30+v339 @ rcx_v26*8]");
						characterCount = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						AnimationCurve vertexCurve = (AnimationCurve)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						nint num4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if (num4 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj15 = (nint)0 + (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if ((nint)obj15 >= 0)
						{
							break;
						}
						TMP_CharacterInfo[] characterInfo2 = textInfo.characterInfo;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj16 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj17 = 0 + obj16;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj18 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj19 = 0 + obj18;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+38+v295 @ rdx_v20*4]");
						nint num5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+20+v1468 @ rcx_v27*4]");
						object obj20 = num5 + 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj21 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj22 = 0 + obj21;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj23 = (nint)0 + (nint)1;
						float num6 = (float)obj20 * 0.5f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+28+v928 @ rcx_v28*4]");
						float num7 = 0f + num3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if ((nint)obj23 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj24 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj25 = 0 + obj24;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj26 = (nint)0 + (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+34+v929 @ rcx_v29*4]");
						float num8 = 0f + num3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if ((nint)obj26 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj27 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj28 = 0 + obj27;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj29 = (nint)0 + (nint)3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+40+v930 @ rcx_v30*4]");
						float num9 = 0f + num3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if ((nint)obj29 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj30 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+64+v322 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj31 = 0 + obj30;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+4C+v1513 @ rcx_v31*4]");
						float num10 = 0f + num3;
						float num11 = num6 - (float)obj7;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r12_v8+144+v1466 @ rax_v32 (TMPro.TMP_CharacterInfo[])]");
						object obj32 = 0 ^ -0f;
						object obj33 = obj32 + (object)vector2;
						object obj34 = obj8 - obj7;
						float num12 = num11 / (float)obj34;
						float time = num12 + 0.0001f;
						float num13 = warpTextExample.VertexCurve.Evaluate(num12);
						float num14 = num13 * warpTextExample.CurveScale;
						float num15 = warpTextExample.VertexCurve.Evaluate(time);
						float num16 = num15 * warpTextExample.CurveScale;
						object obj35 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 96));
						float num17 = num16 - num14;
						Vector3 normalized = vector3.normalized;
						object obj36 = vector2 * 0;
						float num18 = (float)obj36 + normalized.x;
						float num19 = normalized.z * 0f;
						float num20 = num18 + num19;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EF80");
						float num21 = num20 * 57.29578f;
						object obj37 = vector2 * 0;
						object obj38 = obj39 - obj37;
						if ((nint)obj38 <= 0)
						{
							float num22 = 360f - num21;
							num21 = num22;
						}
						Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
						_ = 0;
						Matrix4x4 matrix4x = Matrix4x4.Internal_TRS(ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128)), ref q, ref s);
						AnimationCurve animationCurve3 = vertexCurve;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if ((nint)animationCurve3 >= 0)
						{
							break;
						}
						object obj40 = vertexCurve * 2;
						object obj41 = (object)vertexCurve + obj40;
						object obj42 = vertexCurve + 1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+20+v1622 @ rcx_v38*4]");
						obj = 0;
						object obj43 = vertexCurve * 2;
						object obj44 = (object)vertexCurve + obj43;
						object obj45 = obj * (object)vector2;
						object obj46 = (object)vector2 * (object)vector2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+28+v1622 @ rcx_v38*4]");
						object obj47 = 0 * vector2;
						object obj48 = obj45 + obj46;
						object obj49 = obj48 + obj47;
						object obj50 = obj49 + (object)vector2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if ((nint)obj42 >= 0)
						{
							break;
						}
						object obj51 = vertexCurve * 2;
						object obj52 = (object)vertexCurve + obj51;
						object obj53 = vertexCurve + 1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+2C+v933 @ rcx_v40*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+10]");
						nint num23 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-50]");
						object obj54 = num23 * 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-40]");
						object obj55 = vector2 * 0;
						object obj56 = obj54 + obj55;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+34+v933 @ rcx_v40*4]");
						nint num24 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-30]");
						object obj57 = num24 * 0;
						object obj58 = obj56 + obj57;
						object obj59 = obj58 + (object)vector2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if ((nint)obj53 >= 0)
						{
							break;
						}
						object obj60 = vertexCurve * 2;
						object obj61 = (object)vertexCurve + obj60;
						object obj62 = vertexCurve + 2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if ((nint)obj62 >= 0)
						{
							break;
						}
						object obj63 = vertexCurve * 2;
						object obj64 = (object)vertexCurve + obj63;
						object obj65 = vertexCurve + 2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+38+v935 @ rcx_v42*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+20]");
						nint num25 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-50]");
						object obj66 = num25 * 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-40]");
						object obj67 = vector2 * 0;
						object obj68 = obj66 + obj67;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+40+v935 @ rcx_v42*4]");
						nint num26 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-30]");
						object obj69 = num26 * 0;
						object obj70 = obj68 + obj69;
						object obj71 = obj70 + (object)vector2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if ((nint)obj65 >= 0)
						{
							break;
						}
						object obj72 = vertexCurve * 2;
						object obj73 = (object)vertexCurve + obj72;
						object obj74 = vertexCurve + 3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if ((nint)obj74 >= 0)
						{
							break;
						}
						object obj75 = vertexCurve * 2;
						object obj76 = (object)vertexCurve + obj75;
						object obj77 = vertexCurve + 3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+44+v937 @ rcx_v44*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+30]");
						nint num27 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-50]");
						object obj78 = num27 * 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-40]");
						object obj79 = vector2 * 0;
						object obj80 = obj78 + obj79;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+4C+v937 @ rcx_v44*4]");
						nint num28 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-30]");
						object obj81 = num28 * 0;
						object obj82 = obj80 + obj81;
						object obj83 = obj82 + (object)vector2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if ((nint)obj77 >= 0)
						{
							break;
						}
						object obj84 = vertexCurve * 2;
						object obj85 = (object)vertexCurve + obj84;
						AnimationCurve animationCurve4 = vertexCurve;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if ((nint)animationCurve4 >= 0)
						{
							break;
						}
						object obj86 = vertexCurve * 2;
						object obj87 = (object)vertexCurve + obj86;
						object obj88 = vertexCurve + 1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+28+v939 @ rcx_v46*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if ((nint)obj88 >= 0)
						{
							break;
						}
						object obj89 = vertexCurve * 2;
						object obj90 = (object)vertexCurve + obj89;
						object obj91 = vertexCurve + 2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+34+v940 @ rcx_v47*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if ((nint)obj91 >= 0)
						{
							break;
						}
						object obj92 = vertexCurve * 2;
						object obj93 = (object)vertexCurve + obj92;
						object obj94 = vertexCurve + 3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+40+v941 @ rcx_v48*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+18]");
						if ((nint)obj94 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+40]");
						obj7 = 0;
						object obj95 = vertexCurve * 2;
						object obj96 = (object)vertexCurve + obj95;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+50]");
						obj8 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1372 @ rbx_v12 (System.Int32)+4C+v1814 @ rcx_v49*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+190]");
						obj11 = 0;
						s = Vector3.oneVector;
						euler = (Vector3)0;
						q = (Quaternion)vector2;
						vector3 = vector2;
						num3 = -0f;
						obj6 = (object)(&s);
					}
					obj11++;
					obj10++;
					obj9 += 376;
					object obj97 = obj11;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1A0]");
					if ((nint)obj97 < 0)
					{
						continue;
					}
					goto IL_0f7c;
				}
				goto IL_100c;
			}
			return true;
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

	private TMP_Text m_TextComponent;

	public AnimationCurve VertexCurve;

	public float AngleMultiplier;

	public float SpeedMultiplier;

	public float CurveScale;

	private void Awake()
	{
		GameObject gameObject = base.gameObject;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		TMP_Text textComponent = default(TMP_Text);
		m_TextComponent = textComponent;
	}

	private void Start()
	{
		_003CWarpText_003Ed__8 obj = new _003CWarpText_003Ed__8(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private AnimationCurve CopyAnimationCurve(AnimationCurve curve)
	{
		AnimationCurve animationCurve = new AnimationCurve();
		if (curve != null)
		{
			Keyframe[] keys = curve.keys;
			if (animationCurve != null)
			{
				animationCurve.keys = keys;
				return animationCurve;
			}
		}
		return (AnimationCurve)(object)new NullReferenceException();
	}

	private IEnumerator WarpText()
	{
		_003CWarpText_003Ed__8 obj = new _003CWarpText_003Ed__8(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public unsafe WarpTextExample()
	{
		//IL_0008: Expected O, but got Ref
		//IL_02c4: Expected O, but got Ref
		//IL_02e4: Expected native int or pointer, but got O
		//IL_003d: Expected O, but got Ref
		//IL_0084: Expected native int or pointer, but got O
		//IL_00b9: Expected O, but got Ref
		//IL_0100: Expected native int or pointer, but got O
		//IL_013d: Expected O, but got Ref
		//IL_0184: Expected native int or pointer, but got O
		//IL_01c1: Expected O, but got Ref
		//IL_0208: Expected native int or pointer, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Keyframe[] array = new Keyframe[5];
		Keyframe keyframe = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe = new Keyframe(0f, 0f);
		if (array.Length > 0)
		{
			Keyframe keyframe2 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-39]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-31]");
			_ = 0;
			_ = 0;
			_ = 0;
			_ = 0;
			*(Keyframe*)(nint)keyframe2 = new Keyframe(0.25f, 2f);
			if (array.Length > 1)
			{
				Keyframe keyframe3 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-19]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-11]");
				_ = 0;
				_ = 0;
				_ = 0;
				_ = 0;
				*(Keyframe*)(nint)keyframe3 = new Keyframe(0.5f, 0f);
				bool flag = array.Length <= 2;
				keyframe = keyframe2;
				if (!flag)
				{
					Keyframe keyframe4 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-9]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+7]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+F]");
					_ = 0;
					_ = 0;
					_ = 0;
					_ = 0;
					*(Keyframe*)(nint)keyframe4 = new Keyframe(0.75f, 2f);
					bool flag2 = array.Length <= 3;
					keyframe = keyframe3;
					if (!flag2)
					{
						Keyframe keyframe5 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 55));
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+17]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+27]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+2F]");
						_ = 0;
						_ = 0;
						_ = 0;
						_ = 0;
						*(Keyframe*)(nint)keyframe5 = new Keyframe(1f, 0f);
						bool flag3 = array.Length <= 4;
						keyframe = keyframe4;
						if (!flag3)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+37]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+47]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+4F]");
							_ = 0;
							VertexCurve = new AnimationCurve(array);
							AngleMultiplier = 1f;
							SpeedMultiplier = 1f;
							CurveScale = 1f;
							base._002Ector();
							return;
						}
					}
				}
			}
		}
		throw new IndexOutOfRangeException();
	}
}
