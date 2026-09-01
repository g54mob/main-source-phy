using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class SkewTextExample : MonoBehaviour
{
	private sealed class _003CWarpText_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SkewTextExample _003C_003E4__this;

		private float _003Cold_CurveScale_003E5__2;

		private float _003Cold_ShearValue_003E5__3;

		private AnimationCurve _003Cold_curve_003E5__4;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CWarpText_003Ed__7(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			//IL_00da: Expected I4, but got I8
			//IL_007e: Expected O, but got I4
			//IL_00c6: Expected I4, but got I8
			//IL_0362: Expected I, but got O
			//IL_129c: Expected I4, but got O
			//IL_0240: Unknown result type (might be due to invalid IL or missing references)
			//IL_0245: Expected O, but got Unknown
			//IL_047f: Expected O, but got I4
			//IL_0488: Expected O, but got I4
			//IL_0492: Expected O, but got I4
			//IL_0296: Unknown result type (might be due to invalid IL or missing references)
			//IL_029b: Expected O, but got Unknown
			//IL_02b7: Invalid comparison between O and F4
			//IL_12b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_12bc: Expected O, but got Unknown
			//IL_12c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_12ca: Expected O, but got Unknown
			//IL_12d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_12d8: Expected O, but got Unknown
			//IL_052e: Expected O, but got I
			//IL_053e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0543: Expected O, but got Unknown
			//IL_0575: Expected O, but got I
			//IL_05ba: Expected O, but got I
			//IL_0604: Expected O, but got I
			//IL_0614: Unknown result type (might be due to invalid IL or missing references)
			//IL_0619: Expected O, but got Unknown
			//IL_062f: Expected O, but got I
			//IL_063f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0644: Expected O, but got Unknown
			//IL_0661: Expected O, but got I
			//IL_0677: Expected O, but got I
			//IL_0687: Unknown result type (might be due to invalid IL or missing references)
			//IL_068c: Expected O, but got Unknown
			//IL_06a2: Expected O, but got I
			//IL_0711: Expected O, but got I
			//IL_0721: Unknown result type (might be due to invalid IL or missing references)
			//IL_0726: Expected O, but got Unknown
			//IL_073c: Expected O, but got I
			//IL_079b: Expected O, but got I
			//IL_07ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_07b0: Expected O, but got Unknown
			//IL_07c6: Expected O, but got I
			//IL_0825: Expected O, but got I
			//IL_0835: Unknown result type (might be due to invalid IL or missing references)
			//IL_083a: Expected O, but got Unknown
			//IL_08d4: Expected O, but got I
			//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_08e9: Expected O, but got Unknown
			//IL_08ff: Expected O, but got I
			//IL_095e: Expected O, but got I
			//IL_096e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0973: Expected O, but got Unknown
			//IL_0989: Expected O, but got I
			//IL_09d8: Expected O, but got I
			//IL_09e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_09ed: Expected O, but got Unknown
			//IL_0a03: Expected O, but got I
			//IL_0a52: Expected O, but got I
			//IL_0a62: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a67: Expected O, but got Unknown
			//IL_1314: Invalid comparison between O and F4
			//IL_0bca: Expected I, but got O
			//IL_0bea: Expected F4, but got I
			//IL_1334: Unknown result type (might be due to invalid IL or missing references)
			//IL_1339: Expected O, but got Unknown
			//IL_13a7: Invalid comparison between F4 and I4
			//IL_13dc: Expected I, but got O
			//IL_13e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_13ea: Expected Ref, but got Unknown
			//IL_13f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_13fe: Expected Ref, but got Unknown
			//IL_0c26: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c2b: Expected O, but got Unknown
			//IL_0c41: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c46: Expected O, but got Unknown
			//IL_0c5c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c61: Expected O, but got Unknown
			//IL_0c8d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c92: Expected O, but got Unknown
			//IL_0cb9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cbe: Expected O, but got Unknown
			//IL_0d24: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d29: Expected O, but got Unknown
			//IL_0d3f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d44: Expected O, but got Unknown
			//IL_0d6e: Expected O, but got I
			//IL_0d7e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d83: Expected O, but got Unknown
			//IL_0dad: Expected O, but got I
			//IL_0df7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dfc: Expected O, but got Unknown
			//IL_0e12: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e17: Expected O, but got Unknown
			//IL_0e51: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e56: Expected O, but got Unknown
			//IL_0e6c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e71: Expected O, but got Unknown
			//IL_0e9b: Expected O, but got I
			//IL_0eab: Unknown result type (might be due to invalid IL or missing references)
			//IL_0eb0: Expected O, but got Unknown
			//IL_0eda: Expected O, but got I
			//IL_0f24: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f29: Expected O, but got Unknown
			//IL_0f3f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f44: Expected O, but got Unknown
			//IL_0f7e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f83: Expected O, but got Unknown
			//IL_0f99: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f9e: Expected O, but got Unknown
			//IL_0fc8: Expected O, but got I
			//IL_0fdc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fe1: Expected O, but got Unknown
			//IL_100c: Expected O, but got I
			//IL_1057: Unknown result type (might be due to invalid IL or missing references)
			//IL_105c: Expected O, but got Unknown
			//IL_10a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_10a8: Expected O, but got Unknown
			//IL_10be: Unknown result type (might be due to invalid IL or missing references)
			//IL_10c3: Expected O, but got Unknown
			//IL_1105: Unknown result type (might be due to invalid IL or missing references)
			//IL_110a: Expected O, but got Unknown
			//IL_1120: Unknown result type (might be due to invalid IL or missing references)
			//IL_1125: Expected O, but got Unknown
			//IL_1167: Unknown result type (might be due to invalid IL or missing references)
			//IL_116c: Expected O, but got Unknown
			//IL_1182: Unknown result type (might be due to invalid IL or missing references)
			//IL_1187: Expected O, but got Unknown
			//IL_11d1: Expected O, but got I
			//IL_11da: Unknown result type (might be due to invalid IL or missing references)
			//IL_11df: Expected O, but got Unknown
			//IL_11fd: Expected O, but got I
			//IL_120d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1212: Expected O, but got Unknown
			//IL_1234: Expected O, but got I
			//IL_123e: Expected O, but got I4
			object obj2 = default(object);
			object obj = obj2 - 408;
			SkewTextExample skewTextExample = _003C_003E4__this;
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
				skewTextExample.VertexCurve.preWrapMode = WrapMode.Once;
				skewTextExample.VertexCurve.postWrapMode = WrapMode.Once;
				skewTextExample.m_TextComponent.havePropertiesChanged = true;
				_003Cold_CurveScale_003E5__2 = (skewTextExample.CurveScale *= 10f);
				_003Cold_ShearValue_003E5__3 = skewTextExample.ShearAmount;
				AnimationCurve vertexCurve = skewTextExample.VertexCurve;
				AnimationCurve animationCurve = new AnimationCurve();
				Keyframe[] keys = skewTextExample.VertexCurve.keys;
				animationCurve.keys = keys;
				_003Cold_curve_003E5__4 = animationCurve;
			}
			object obj6 = default(object);
			Quaternion quaternion = default(Quaternion);
			Quaternion quaternion3 = default(Quaternion);
			object obj46 = default(object);
			Vector3 euler = default(Vector3);
			Quaternion q = default(Quaternion);
			while (true)
			{
				TMP_Text textComponent = skewTextExample.m_TextComponent;
				if (!textComponent.m_havePropertiesChanged)
				{
					float num = _003Cold_CurveScale_003E5__2;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp near ptr 0000000180399380h\"");
					if (_003Cold_CurveScale_003E5__2 == skewTextExample.CurveScale)
					{
						Keyframe[] keys2 = _003Cold_curve_003E5__4.keys;
						if (keys2.Length > 1)
						{
							object obj4 = keys2 + 60;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
							Keyframe[] keys3 = skewTextExample.VertexCurve.keys;
							if (keys3.Length > 1)
							{
								object obj5 = keys3 + 60;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180399380h\"");
								if (obj6 == (object)num)
								{
									num = _003Cold_ShearValue_003E5__3;
									bool flag2 = _003Cold_ShearValue_003E5__3 == skewTextExample.ShearAmount;
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180399380h\"");
									if (flag2)
									{
										_003C_003E2__current = null;
										_003C_003E1__state = 1;
										break;
									}
								}
								goto IL_0305;
							}
						}
						goto IL_128e;
					}
				}
				goto IL_0305;
				IL_0305:
				_003Cold_CurveScale_003E5__2 = skewTextExample.CurveScale;
				AnimationCurve animationCurve2 = skewTextExample.CopyAnimationCurve(skewTextExample.VertexCurve);
				_003Cold_curve_003E5__4 = animationCurve2;
				_003Cold_ShearValue_003E5__3 = skewTextExample.ShearAmount;
				TMP_Text textComponent2 = skewTextExample.m_TextComponent;
				nint num2 = (nint)textComponent2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1390 @ r9_v4 (Il2CppClass<TMPro.TMP_Text>)+7E0]");
				ref Vector3 reference = ref *(Vector3*)null;
				textComponent2.ForceMeshUpdate();
				TMP_TextInfo textInfo = skewTextExample.m_TextComponent.textInfo;
				int characterCount = textInfo.characterCount;
				if (textInfo.characterCount == 0)
				{
					continue;
				}
				Bounds bounds = skewTextExample.m_TextComponent.bounds;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v563 @ rax_v17 (UnityEngine.Bounds)+10]");
				_ = 0;
				object obj7 = (object)bounds.m_Center - (object)quaternion;
				Bounds bounds2 = skewTextExample.m_TextComponent.bounds;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1413 @ rax_v18 (UnityEngine.Bounds)+10]");
				_ = 0;
				object obj8 = (object)bounds2.m_Center + (object)quaternion;
				if (textInfo.characterCount <= 0)
				{
					goto IL_1255;
				}
				_ = 0;
				_ = textInfo.characterCount;
				object obj9 = 0;
				object obj10 = 0;
				object obj11 = 0;
				while (true)
				{
					TMP_CharacterInfo[] characterInfo = textInfo.characterInfo;
					if ((nint)obj10 >= characterInfo.Length)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+190+v170 @ r12_v5]");
					if ((nint)0 != 0)
					{
						TMP_MeshInfo[] meshInfo = textInfo.meshInfo;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+50+v170 @ r12_v5]");
						if ((nint)0 >= (nint)meshInfo.Length)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+50+v170 @ r12_v5]");
						object obj12 = (nint)0 * (nint)4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+50+v170 @ r12_v5]");
						object obj13 = 0 + obj12;
						object obj14 = obj13 + obj13;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v233 @ rbx_v9 (TMPro.TMP_MeshInfo[])+30+v546 @ rcx_v21*8]");
						characterCount = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						AnimationCurve vertexCurve = (AnimationCurve)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						nint num3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if (num3 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj15 = (nint)0 + (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj15 >= 0)
						{
							break;
						}
						TMP_CharacterInfo[] characterInfo2 = textInfo.characterInfo;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj16 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj17 = 0 + obj16;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj18 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj19 = 0 + obj18;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+38+v288 @ rdx_v18*4]");
						nint num4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+20+v1558 @ rcx_v22*4]");
						object obj20 = num4 + 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj21 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj22 = 0 + obj21;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj23 = (nint)0 + (nint)1;
						float num5 = (float)obj20 * 0.5f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+28+v1224 @ rcx_v23*4]");
						float num6 = 0f + -0f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj23 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj24 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj25 = 0 + obj24;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj26 = (nint)0 + (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+34+v1225 @ rcx_v24*4]");
						float num7 = 0f + -0f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj26 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj27 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj28 = 0 + obj27;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj29 = (nint)0 + (nint)3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+40+v1226 @ rcx_v25*4]");
						float num8 = 0f + -0f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj29 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj30 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj31 = 0 + obj30;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+4C+v1603 @ rcx_v26*4]");
						float num9 = 0f + -0f;
						TMP_CharacterInfo[] characterInfo3 = textInfo.characterInfo;
						if ((nint)obj10 >= characterInfo3.Length)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						nint num10 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if (num10 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj32 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj33 = 0 + obj32;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj34 = (nint)0 + (nint)1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+28+v1227 @ rcx_v28*4]");
						float num11 = 0f + -0f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj34 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj35 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj36 = 0 + obj35;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj37 = (nint)0 + (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+34+v1228 @ rcx_v29*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj37 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj38 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj39 = 0 + obj38;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj40 = (nint)0 + (nint)3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+40+v1229 @ rcx_v30*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj40 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj41 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rax_v24 (TMPro.TMP_CharacterInfo[])+64+v170 @ r12_v5]");
						object obj42 = 0 + obj41;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+4C+v1653 @ rcx_v31*4]");
						float num12 = 0f + -0f;
						object obj43 = obj8 - obj7;
						float num13 = num5 - (float)obj7;
						float num14 = num13 / (float)obj43;
						float num15 = num14 + 0.0001f;
						float num16 = skewTextExample.VertexCurve.Evaluate(num14);
						float num17 = num16 * skewTextExample.CurveScale;
						float num18 = skewTextExample.VertexCurve.Evaluate(num15);
						object obj44 = obj8 - obj7;
						float num19 = num18 * skewTextExample.CurveScale;
						float num20 = (float)obj44 * num15;
						float num21 = num19 - num17;
						float num22 = num20 + (float)obj7;
						float num23 = num22 - num5;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
						Vector3 vector;
						float num25;
						if (System.Runtime.CompilerServices.Unsafe.As<Quaternion, UIntPtr>(ref quaternion) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
						{
							num12 = 0f / (float)quaternion;
							vector = (Vector3)quaternion;
							Quaternion quaternion2 = quaternion;
							nint num24 = (nint)(&quaternion3);
							num25 = num12;
						}
						else
						{
							nint num26 = (nint)typeof(Vector3);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1734 @ rax_v56 (Il2CppClass<UnityEngine.Vector3>)+B8]");
							nint num24 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1745 @ rcx_v36 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
							num25 = 0f;
							vector = Vector3.zeroVector;
							Quaternion quaternion2 = quaternion;
						}
						object obj45 = obj46 * 0;
						float num27 = num25 * 0f;
						object obj47 = obj45 + (object)vector;
						float num28 = (float)obj47 + num27;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EF80");
						float num29 = num23 * 0f;
						float num30 = num28 * 57.29578f;
						float num31 = num21 - num29;
						if (!(num31 > 0f))
						{
							float num32 = 360f - num30;
							num30 = num32;
						}
						Quaternion quaternion4 = Quaternion.Internal_FromEulerRad(ref euler);
						nint num33 = (nint)typeof(Vector3);
						reference = ref *(Vector3*)(obj - 128);
						_ = 0;
						ref Vector3 pos = ref *(Vector3*)(obj - 112);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1796 @ rax_v42 (Il2CppClass<UnityEngine.Vector3>)+B8]");
						nint num34 = 0;
						_ = Vector3.oneVector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1797 @ rax_v43 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
						_ = 0;
						Matrix4x4 matrix4x = Matrix4x4.Internal_TRS(ref pos, ref q, ref reference);
						AnimationCurve animationCurve3 = vertexCurve;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)animationCurve3 >= 0)
						{
							break;
						}
						object obj48 = vertexCurve * 2;
						object obj49 = (object)vertexCurve + obj48;
						object obj50 = vertexCurve + 1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+20+v1804 @ rcx_v40*4]");
						_ = 0;
						object obj51 = vertexCurve * 2;
						object obj52 = (object)vertexCurve + obj51;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1+20]");
						object obj53 = 0 * quaternion;
						object obj54 = quaternion * quaternion;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+28+v1804 @ rcx_v40*4]");
						object obj55 = 0 * quaternion;
						object obj56 = obj53 + obj54;
						object obj57 = obj56 + obj55;
						object obj58 = obj57 + (object)quaternion;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj50 >= 0)
						{
							break;
						}
						object obj59 = vertexCurve * 2;
						object obj60 = (object)vertexCurve + obj59;
						object obj61 = vertexCurve + 1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+2C+v1232 @ rcx_v42*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1+30]");
						nint num35 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1-30]");
						object obj62 = num35 * 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1-20]");
						object obj63 = quaternion * 0;
						object obj64 = obj62 + obj63;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+34+v1232 @ rcx_v42*4]");
						nint num36 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1-10]");
						object obj65 = num36 * 0;
						object obj66 = obj64 + obj65;
						object obj67 = obj66 + (object)quaternion;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj61 >= 0)
						{
							break;
						}
						object obj68 = vertexCurve * 2;
						object obj69 = (object)vertexCurve + obj68;
						object obj70 = vertexCurve + 2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj70 >= 0)
						{
							break;
						}
						object obj71 = vertexCurve * 2;
						object obj72 = (object)vertexCurve + obj71;
						object obj73 = vertexCurve + 2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+38+v1234 @ rcx_v44*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1+40]");
						nint num37 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1-30]");
						object obj74 = num37 * 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1-20]");
						object obj75 = quaternion * 0;
						object obj76 = obj74 + obj75;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+40+v1234 @ rcx_v44*4]");
						nint num38 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1-10]");
						object obj77 = num38 * 0;
						object obj78 = obj76 + obj77;
						object obj79 = obj78 + (object)quaternion;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj73 >= 0)
						{
							break;
						}
						object obj80 = vertexCurve * 2;
						object obj81 = (object)vertexCurve + obj80;
						object obj82 = vertexCurve + 3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj82 >= 0)
						{
							break;
						}
						object obj83 = vertexCurve * 2;
						object obj84 = (object)vertexCurve + obj83;
						object obj85 = vertexCurve + 3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+44+v1236 @ rcx_v46*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1+50]");
						nint num39 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1-30]");
						object obj86 = num39 * 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1-20]");
						object obj87 = quaternion * 0;
						object obj88 = obj86 + obj87;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+4C+v1236 @ rcx_v46*4]");
						nint num40 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1-10]");
						object obj89 = num40 * 0;
						object obj90 = obj88 + obj89;
						object obj91 = obj90 + (object)quaternion;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj85 >= 0)
						{
							break;
						}
						object obj92 = vertexCurve * 2;
						object obj93 = (object)vertexCurve + obj92;
						AnimationCurve animationCurve4 = vertexCurve;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)animationCurve4 >= 0)
						{
							break;
						}
						object obj94 = vertexCurve * 2;
						object obj95 = (object)vertexCurve + obj94;
						object obj96 = vertexCurve + 1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+28+v1238 @ rcx_v48*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj96 >= 0)
						{
							break;
						}
						object obj97 = vertexCurve * 2;
						object obj98 = (object)vertexCurve + obj97;
						object obj99 = vertexCurve + 2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+34+v1239 @ rcx_v49*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj99 >= 0)
						{
							break;
						}
						object obj100 = vertexCurve * 2;
						object obj101 = (object)vertexCurve + obj100;
						object obj102 = vertexCurve + 3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+40+v1240 @ rcx_v50*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+18]");
						if ((nint)obj102 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1+60]");
						obj7 = 0;
						object obj103 = vertexCurve * 2;
						object obj104 = (object)vertexCurve + obj103;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1+70]");
						obj8 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v170 @ r12_v5+144+v1556 @ rax_v28 (TMPro.TMP_CharacterInfo[])]");
						object obj105 = 0 + quaternion;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1467 @ rbx_v8 (System.Int32)+4C+v1996 @ rcx_v51*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1+1A0]");
						obj11 = 0;
						euler = (Vector3)0;
						q = quaternion;
						quaternion3 = quaternion;
					}
					obj11++;
					obj10++;
					obj9 += 376;
					object obj106 = obj11;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbp_v1+1B0]");
					if ((nint)obj106 < 0)
					{
						continue;
					}
					goto IL_1255;
				}
				goto IL_128e;
				IL_1255:
				skewTextExample.m_TextComponent.UpdateVertexData();
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				break;
				IL_128e:
				IndexOutOfRangeException ex = new IndexOutOfRangeException();
				return (byte)(int)ex != 0;
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

	public float CurveScale;

	public float ShearAmount;

	private void Awake()
	{
		GameObject gameObject = base.gameObject;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		TMP_Text textComponent = default(TMP_Text);
		m_TextComponent = textComponent;
	}

	private void Start()
	{
		_003CWarpText_003Ed__7 obj = new _003CWarpText_003Ed__7(0);
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
		_003CWarpText_003Ed__7 obj = new _003CWarpText_003Ed__7(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public unsafe SkewTextExample()
	{
		//IL_0008: Expected O, but got Ref
		//IL_020b: Expected O, but got Ref
		//IL_022b: Expected native int or pointer, but got O
		//IL_001b: Expected O, but got Ref
		//IL_0062: Expected native int or pointer, but got O
		//IL_007a: Expected O, but got Ref
		//IL_00c1: Expected native int or pointer, but got O
		//IL_00d9: Expected O, but got Ref
		//IL_0120: Expected native int or pointer, but got O
		//IL_0138: Expected O, but got Ref
		//IL_017f: Expected native int or pointer, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Keyframe[] keys = new Keyframe[5];
		Keyframe keyframe = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
		_ = 0;
		_ = 0;
		_ = 0;
		*(Keyframe*)(nint)keyframe = new Keyframe(0f, 0f);
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+37]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+47]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+4F]");
		_ = 0;
		VertexCurve = new AnimationCurve(keys);
		CurveScale = 1f;
		ShearAmount = 1f;
		base._002Ector();
	}
}
