using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class VertexZoom : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass10_0
	{
		public List<float> modifiedCharScale;

		public Comparison<int> _003C_003E9__0;

		internal int _003CAnimateVertexColors_003Eb__0(int a, int b)
		{
			//IL_0061: Expected I4, but got O
			if (modifiedCharScale != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (modifiedCharScale != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					float num = default(float);
					float value = default(float);
					return num.CompareTo(value);
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	private sealed class _003CAnimateVertexColors_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public VertexZoom _003C_003E4__this;

		private _003C_003Ec__DisplayClass10_0 _003C_003E8__1;

		private TMP_TextInfo _003CtextInfo_003E5__2;

		private TMP_MeshInfo[] _003CcachedMeshInfoVertexData_003E5__3;

		private List<int> _003CscaleSortingOrder_003E5__4;

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
			//IL_00b6: Expected I, but got O
			//IL_00c6: Expected O, but got I
			//IL_01b0: Expected I, but got O
			//IL_0177: Expected I, but got O
			//IL_029d: Expected O, but got I
			//IL_033b: Expected O, but got I
			//IL_039e: Expected O, but got Ref
			//IL_1c87: Expected O, but got I
			//IL_1ccc: Expected I4, but got O
			//IL_193b: Expected O, but got I4
			//IL_1943: Unknown result type (might be due to invalid IL or missing references)
			//IL_1948: Expected O, but got Unknown
			//IL_195f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1964: Expected O, but got Unknown
			//IL_0474: Expected O, but got I
			//IL_0484: Unknown result type (might be due to invalid IL or missing references)
			//IL_0489: Expected O, but got Unknown
			//IL_04a6: Expected O, but got I
			//IL_04bb: Expected O, but got I
			//IL_04fd: Expected O, but got I
			//IL_19fb: Expected O, but got I
			//IL_19fb: Expected O, but got I
			//IL_05a3: Expected O, but got I
			//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b8: Expected O, but got Unknown
			//IL_1a7b: Expected O, but got I
			//IL_1a7b: Expected O, but got I
			//IL_0617: Expected O, but got I
			//IL_0627: Unknown result type (might be due to invalid IL or missing references)
			//IL_062c: Expected O, but got Unknown
			//IL_0676: Expected O, but got I
			//IL_0686: Unknown result type (might be due to invalid IL or missing references)
			//IL_068b: Expected O, but got Unknown
			//IL_06a1: Expected O, but got I
			//IL_1af6: Expected O, but got I
			//IL_1af6: Expected O, but got I
			//IL_06ed: Expected O, but got I
			//IL_06fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0702: Expected O, but got Unknown
			//IL_0718: Expected O, but got I
			//IL_0755: Expected O, but got I
			//IL_0765: Unknown result type (might be due to invalid IL or missing references)
			//IL_076a: Expected O, but got Unknown
			//IL_0780: Expected O, but got I
			//IL_1b56: Expected O, but got I
			//IL_07cc: Expected O, but got I
			//IL_07dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e1: Expected O, but got Unknown
			//IL_07f7: Expected O, but got I
			//IL_0834: Expected O, but got I
			//IL_0844: Unknown result type (might be due to invalid IL or missing references)
			//IL_0849: Expected O, but got Unknown
			//IL_085f: Expected O, but got I
			//IL_08ab: Expected O, but got I
			//IL_08bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c0: Expected O, but got Unknown
			//IL_08d6: Expected O, but got I
			//IL_0913: Expected O, but got I
			//IL_0923: Unknown result type (might be due to invalid IL or missing references)
			//IL_0928: Expected O, but got Unknown
			//IL_09c8: Expected O, but got I
			//IL_09f7: Expected O, but got I
			//IL_0a07: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a0c: Expected O, but got Unknown
			//IL_0a22: Expected O, but got I
			//IL_0a45: Expected O, but got I
			//IL_0a55: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a5a: Expected O, but got Unknown
			//IL_0a6f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a74: Expected O, but got Unknown
			//IL_0a9b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa0: Expected O, but got Unknown
			//IL_0b13: Expected O, but got I
			//IL_0b23: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b28: Expected O, but got Unknown
			//IL_0b3e: Expected O, but got I
			//IL_0b68: Expected O, but got I
			//IL_0b78: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b7d: Expected O, but got Unknown
			//IL_0ba7: Expected O, but got I
			//IL_0bfe: Expected O, but got I
			//IL_0c0e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c13: Expected O, but got Unknown
			//IL_0c29: Expected O, but got I
			//IL_0c70: Expected O, but got I
			//IL_0c80: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c85: Expected O, but got Unknown
			//IL_0c9b: Expected O, but got I
			//IL_0cc5: Expected O, but got I
			//IL_0cd5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cda: Expected O, but got Unknown
			//IL_0d04: Expected O, but got I
			//IL_0d5b: Expected O, but got I
			//IL_0d6b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d70: Expected O, but got Unknown
			//IL_0d86: Expected O, but got I
			//IL_0dcd: Expected O, but got I
			//IL_0ddd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0de2: Expected O, but got Unknown
			//IL_0df8: Expected O, but got I
			//IL_0e22: Expected O, but got I
			//IL_0e36: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e3b: Expected O, but got Unknown
			//IL_0e66: Expected O, but got I
			//IL_0ebe: Expected O, but got I
			//IL_0ece: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ed3: Expected O, but got Unknown
			//IL_0f22: Expected O, but got I
			//IL_0f32: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f37: Expected O, but got Unknown
			//IL_0f4d: Expected O, but got I
			//IL_0f9c: Expected O, but got I
			//IL_0fac: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fb1: Expected O, but got Unknown
			//IL_0fc7: Expected O, but got I
			//IL_1016: Expected O, but got I
			//IL_1026: Unknown result type (might be due to invalid IL or missing references)
			//IL_102b: Expected O, but got Unknown
			//IL_1041: Expected O, but got I
			//IL_1090: Expected O, but got I
			//IL_10a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_10a5: Expected O, but got Unknown
			//IL_111f: Expected O, but got I
			//IL_112f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1134: Expected O, but got Unknown
			//IL_1191: Expected O, but got I
			//IL_11d8: Expected O, but got I
			//IL_11e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ed: Expected O, but got Unknown
			//IL_1210: Expected O, but got I
			//IL_122d: Expected O, but got I
			//IL_1274: Expected O, but got I
			//IL_12a4: Expected O, but got I
			//IL_12de: Expected O, but got I
			//IL_1301: Expected O, but got I
			//IL_133b: Expected O, but got I
			//IL_136b: Expected O, but got I
			//IL_13a5: Expected O, but got I
			//IL_13c8: Expected O, but got I
			//IL_1402: Expected O, but got I
			//IL_1432: Expected O, but got I
			//IL_146c: Expected O, but got I
			//IL_148f: Expected O, but got I
			//IL_14c9: Expected O, but got I
			//IL_153b: Expected O, but got I
			//IL_154b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1550: Expected O, but got Unknown
			//IL_15ad: Expected O, but got I
			//IL_15c3: Expected O, but got I
			//IL_15d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_15d8: Expected O, but got Unknown
			//IL_1626: Expected O, but got I
			//IL_167a: Expected O, but got I
			//IL_16b4: Expected O, but got I
			//IL_16fb: Expected O, but got I
			//IL_1735: Expected O, but got I
			//IL_177c: Expected O, but got I
			//IL_17b6: Expected O, but got I
			object obj2 = default(object);
			object obj = (object)(&obj2);
			VertexZoom vertexZoom = _003C_003E4__this;
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
				_003C_003Ec__DisplayClass10_0 obj4 = new _003C_003Ec__DisplayClass10_0();
				_003C_003E8__1 = obj4;
				TMP_Text textComponent = vertexZoom.m_TextComponent;
				nint num = (nint)textComponent;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2014 @ r9_v23 (Il2CppClass<TMPro.TMP_Text>)+7E0]");
				TMP_MeshInfo[] array = (TMP_MeshInfo[])0;
				textComponent.ForceMeshUpdate();
				TMP_TextInfo textInfo = vertexZoom.m_TextComponent.textInfo;
				_003CtextInfo_003E5__2 = textInfo;
				TMP_MeshInfo[] array2 = _003CtextInfo_003E5__2.CopyMeshInfoVertexData();
				_003CcachedMeshInfoVertexData_003E5__3 = array2;
				_003C_003Ec__DisplayClass10_0 obj5 = _003C_003E8__1;
				List<float> modifiedCharScale = new List<float>();
				obj5.modifiedCharScale = modifiedCharScale;
				List<int> list = (_003CscaleSortingOrder_003E5__4 = new List<int>());
				vertexZoom.hasTextChanged = true;
				nint num2 = (nint)list;
			}
			if (vertexZoom.hasTextChanged)
			{
				TMP_MeshInfo[] array3 = (_003CcachedMeshInfoVertexData_003E5__3 = _003CtextInfo_003E5__2.CopyMeshInfoVertexData());
				vertexZoom.hasTextChanged = false;
				nint num2 = (nint)array3;
			}
			TMP_TextInfo tMP_TextInfo = _003CtextInfo_003E5__2;
			int characterCount = tMP_TextInfo.characterCount;
			if (tMP_TextInfo.characterCount != 0)
			{
				_003C_003Ec__DisplayClass10_0 obj6 = _003C_003E8__1;
				List<float> modifiedCharScale2 = obj6.modifiedCharScale;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v351 @ rbx_v6 (System.Collections.Generic.List`1<System.Single>)+1C]");
				_ = (nint)0 + (nint)1;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
				object obj7 = default(object);
				if (obj7 == null)
				{
					_ = 0;
				}
				else
				{
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v351 @ rbx_v6 (System.Collections.Generic.List`1<System.Single>)+18]");
					if ((nint)0 > (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v351 @ rbx_v6 (System.Collections.Generic.List`1<System.Single>)+10]");
						nint num3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v351 @ rbx_v6 (System.Collections.Generic.List`1<System.Single>)+18]");
						Array.Clear((Array)num3, 0, 0);
						TMP_MeshInfo[] array = null;
					}
				}
				List<int> list2 = _003CscaleSortingOrder_003E5__4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2211 @ rbx_v14 (System.Collections.Generic.List`1<System.Int32>)+1C]");
				_ = (nint)0 + (nint)1;
				if (!RuntimeHelpers.IsReferenceOrContainsReferences<int>())
				{
					_ = 0;
				}
				else
				{
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2211 @ rbx_v14 (System.Collections.Generic.List`1<System.Int32>)+18]");
					if ((nint)0 > (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2211 @ rbx_v14 (System.Collections.Generic.List`1<System.Int32>)+10]");
						nint num4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2211 @ rbx_v14 (System.Collections.Generic.List`1<System.Int32>)+18]");
						Array.Clear((Array)num4, 0, 0);
						TMP_MeshInfo[] array = null;
					}
				}
				if (characterCount <= 0)
				{
					goto IL_180f;
				}
				_ = 0;
				int num5 = 0;
				float num6 = 0.5f;
				int num7 = 0;
				Vector3 vector = default(Vector3);
				Vector3 pos = default(Vector3);
				Quaternion q = default(Quaternion);
				Vector3 s = default(Vector3);
				while (true)
				{
					TMP_TextInfo tMP_TextInfo2 = _003CtextInfo_003E5__2;
					object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 64));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800039A0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+1B0]");
					if ((nint)0 != 0)
					{
						TMP_TextInfo tMP_TextInfo3 = _003CtextInfo_003E5__2;
						TMP_CharacterInfo[] characterInfo = tMP_TextInfo3.characterInfo;
						if (num7 >= characterInfo.Length)
						{
							break;
						}
						TMP_TextInfo tMP_TextInfo4 = _003CtextInfo_003E5__2;
						TMP_MeshInfo[] array4 = _003CcachedMeshInfoVertexData_003E5__3;
						TMP_CharacterInfo[] characterInfo2 = tMP_TextInfo4.characterInfo;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						if ((nint)0 >= (nint)array4.Length)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						object obj9 = (nint)0 * (nint)4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						object obj10 = 0 + obj9;
						object obj11 = obj10 + obj10;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v370 @ rdx_v29 (TMPro.TMP_MeshInfo[])+30+v433 @ rcx_v50*8]");
						TMP_MeshInfo tMP_MeshInfo = (TMP_MeshInfo)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						list2 = (List<int>)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						if (0 >= (nint)tMP_MeshInfo.normals)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj12 = (nint)0 + (nint)2;
						Vector3[] normals = tMP_MeshInfo.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals))
						{
							break;
						}
						TMP_TextInfo tMP_TextInfo5 = _003CtextInfo_003E5__2;
						object obj13 = vector + vector;
						float num8 = (float)obj13 * num6;
						TMP_MeshInfo[] meshInfo = tMP_TextInfo5.meshInfo;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						if ((nint)0 >= (nint)meshInfo.Length)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						object obj14 = (nint)0 * (nint)4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						object obj15 = 0 + obj14;
						object obj16 = obj15 + obj15;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v305 @ rdi_v14 (TMPro.TMP_MeshInfo[])+30+v1448 @ rcx_v53*8]");
						characterCount = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						if (0 >= (nint)tMP_MeshInfo.normals)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj17 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj18 = 0 + obj17;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						nint num9 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+18]");
						if (num9 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj19 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj20 = 0 + obj19;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj21 = (nint)0 + (nint)1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ rdx_v30 (TMPro.TMP_MeshInfo)+28+v435 @ rcx_v54*4]");
						_ = 0;
						Vector3[] normals2 = tMP_MeshInfo.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj21) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals2))
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj22 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj23 = 0 + obj22;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj24 = (nint)0 + (nint)1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+18]");
						if ((nint)obj24 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj25 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj26 = 0 + obj25;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj27 = (nint)0 + (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ rdx_v30 (TMPro.TMP_MeshInfo)+34+v1450 @ rcx_v56*4]");
						_ = 0;
						Vector3[] normals3 = tMP_MeshInfo.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj27) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals3))
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj28 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj29 = 0 + obj28;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj30 = (nint)0 + (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+18]");
						if ((nint)obj30 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj31 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj32 = 0 + obj31;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj33 = (nint)0 + (nint)3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ rdx_v30 (TMPro.TMP_MeshInfo)+40+v1452 @ rcx_v58*4]");
						_ = 0;
						Vector3[] normals4 = tMP_MeshInfo.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj33) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals4))
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj34 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj35 = 0 + obj34;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj36 = (nint)0 + (nint)3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+18]");
						if ((nint)obj36 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj37 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj38 = 0 + obj37;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ rdx_v30 (TMPro.TMP_MeshInfo)+4C+v1454 @ rcx_v60*4]");
						_ = 0;
						float num10 = UnityEngine.Random.Range(1f, 1.5f);
						_003C_003Ec__DisplayClass10_0 obj39 = _003C_003E8__1;
						float item = (float)(ref obj2) + 672f;
						obj39.modifiedCharScale.Add(item);
						_003C_003Ec__DisplayClass10_0 obj40 = _003C_003E8__1;
						List<float> modifiedCharScale3 = obj40.modifiedCharScale;
						int item2 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 672));
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v406 @ rax_v57 (System.Collections.Generic.List`1<System.Single>)+18]");
						object obj41 = -1;
						_003CscaleSortingOrder_003E5__4.Add(item2);
						Matrix4x4 matrix4x = Matrix4x4.Internal_TRS(ref pos, ref q, ref s);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						nint num11 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+18]");
						if (num11 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj42 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj43 = 0 + obj42;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj44 = (nint)0 + (nint)1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+20+v2447 @ rcx_v70*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj45 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj46 = 0 + obj45;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-40]");
						object obj47 = 0 * vector;
						object obj48 = (object)vector * (object)vector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+28+v2447 @ rcx_v70*4]");
						object obj49 = 0 * vector;
						object obj50 = obj47 + obj48;
						object obj51 = obj50 + obj49;
						object obj52 = obj51 + (object)vector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+18]");
						if ((nint)obj44 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj53 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj54 = 0 + obj53;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj55 = (nint)0 + (nint)1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+2C+v1457 @ rcx_v72*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-30]");
						nint num12 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-70]");
						object obj56 = num12 * 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-60]");
						object obj57 = vector * 0;
						object obj58 = obj56 + obj57;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+34+v1457 @ rcx_v72*4]");
						nint num13 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-50]");
						object obj59 = num13 * 0;
						object obj60 = obj58 + obj59;
						object obj61 = obj60 + (object)vector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+18]");
						if ((nint)obj55 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj62 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj63 = 0 + obj62;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj64 = (nint)0 + (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+18]");
						if ((nint)obj64 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj65 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj66 = 0 + obj65;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj67 = (nint)0 + (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+38+v1459 @ rcx_v74*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-20]");
						nint num14 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-70]");
						object obj68 = num14 * 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-60]");
						object obj69 = vector * 0;
						object obj70 = obj68 + obj69;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+40+v1459 @ rcx_v74*4]");
						nint num15 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-50]");
						object obj71 = num15 * 0;
						object obj72 = obj70 + obj71;
						object obj73 = obj72 + (object)vector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+18]");
						if ((nint)obj67 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj74 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj75 = 0 + obj74;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj76 = (nint)0 + (nint)3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+18]");
						if ((nint)obj76 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj77 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj78 = 0 + obj77;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj79 = (nint)0 + (nint)3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+44+v1461 @ rcx_v76*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-10]");
						nint num16 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-70]");
						object obj80 = num16 * 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-60]");
						object obj81 = vector * 0;
						object obj82 = obj80 + obj81;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+4C+v1461 @ rcx_v76*4]");
						nint num17 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-50]");
						object obj83 = num17 * 0;
						object obj84 = obj82 + obj83;
						object obj85 = obj84 + (object)vector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+18]");
						if ((nint)obj79 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj86 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj87 = 0 + obj86;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						nint num18 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+18]");
						if (num18 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj88 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj89 = 0 + obj88;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj90 = (nint)0 + (nint)1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+28+v1463 @ rcx_v78*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+18]");
						if ((nint)obj90 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj91 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj92 = 0 + obj91;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj93 = (nint)0 + (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+34+v1464 @ rcx_v79*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+18]");
						if ((nint)obj93 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj94 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj95 = 0 + obj94;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj96 = (nint)0 + (nint)3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+40+v1465 @ rcx_v80*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+18]");
						if ((nint)obj96 >= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj97 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj98 = 0 + obj97;
						float num19 = (float)vector + num8;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2208 @ rdi_v12 (System.Int32)+4C+v2641 @ rcx_v81*4]");
						_ = 0;
						TMP_MeshInfo[] array5 = _003CcachedMeshInfoVertexData_003E5__3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						if ((nint)0 >= (nint)array5.Length)
						{
							break;
						}
						TMP_TextInfo tMP_TextInfo6 = _003CtextInfo_003E5__2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						object obj99 = (nint)0 * (nint)4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						object obj100 = 0 + obj99;
						object obj101 = obj100 + obj100;
						TMP_MeshInfo[] meshInfo2 = tMP_TextInfo6.meshInfo;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						if ((nint)0 >= (nint)meshInfo2.Length)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v439 @ rcx_v82 (TMPro.TMP_MeshInfo[])+48+v320 @ r8_v27*8]");
						TMP_MeshInfo tMP_MeshInfo2 = (TMP_MeshInfo)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						if (0 >= (nint)tMP_MeshInfo2.normals)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						object obj102 = (nint)0 * (nint)4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						object obj103 = 0 + obj102;
						object obj104 = obj103 + obj103;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj105 = (nint)0 + (nint)2;
						object obj106 = obj105 + obj105;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v375 @ rdx_v35 (TMPro.TMP_MeshInfo[])+48+v440 @ rcx_v84*8]");
						TMP_MeshInfo tMP_MeshInfo3 = (TMP_MeshInfo)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						if (0 >= (nint)tMP_MeshInfo3.normals)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj107 = (nint)0 + (nint)2;
						object obj108 = obj107 + obj107;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v321 @ r8_v28 (TMPro.TMP_MeshInfo)+v340 @ r9_v17*8]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj109 = (nint)0 + (nint)1;
						Vector3[] normals5 = tMP_MeshInfo2.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj109) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals5))
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj110 = (nint)0 + (nint)3;
						object obj111 = obj110 + obj110;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj112 = (nint)0 + (nint)1;
						Vector3[] normals6 = tMP_MeshInfo3.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj112) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals6))
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj113 = (nint)0 + (nint)3;
						object obj114 = obj113 + obj113;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v321 @ r8_v28 (TMPro.TMP_MeshInfo)+v1466 @ rcx_v86*8]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj115 = (nint)0 + (nint)2;
						Vector3[] normals7 = tMP_MeshInfo2.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj115) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals7))
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj116 = (nint)0 + (nint)4;
						object obj117 = obj116 + obj116;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj118 = (nint)0 + (nint)2;
						Vector3[] normals8 = tMP_MeshInfo3.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj118) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals8))
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj119 = (nint)0 + (nint)4;
						object obj120 = obj119 + obj119;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v321 @ r8_v28 (TMPro.TMP_MeshInfo)+v1467 @ rcx_v88*8]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj121 = (nint)0 + (nint)3;
						Vector3[] normals9 = tMP_MeshInfo2.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj121) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals9))
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj122 = (nint)0 + (nint)5;
						object obj123 = obj122 + obj122;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj124 = (nint)0 + (nint)3;
						Vector3[] normals10 = tMP_MeshInfo3.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj124) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals10))
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj125 = (nint)0 + (nint)5;
						object obj126 = obj125 + obj125;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v321 @ r8_v28 (TMPro.TMP_MeshInfo)+v441 @ rcx_v90*8]");
						_ = 0;
						TMP_MeshInfo[] array = _003CcachedMeshInfoVertexData_003E5__3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						if ((nint)0 >= (nint)array.Length)
						{
							break;
						}
						TMP_TextInfo tMP_TextInfo7 = _003CtextInfo_003E5__2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						object obj127 = (nint)0 * (nint)4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						object obj128 = 0 + obj127;
						object obj129 = obj128 + obj128;
						TMP_MeshInfo[] meshInfo3 = tMP_TextInfo7.meshInfo;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						if ((nint)0 >= (nint)meshInfo3.Length)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2210 @ r9_v14 (TMPro.TMP_MeshInfo[])+58+v322 @ r8_v30*8]");
						TMP_MeshInfo tMP_MeshInfo4 = (TMP_MeshInfo)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						object obj130 = (nint)0 * (nint)4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v270 @ r15_v10 (TMPro.TMP_CharacterInfo[])+50+v279 @ r13_v7 (System.Int32)]");
						object obj131 = 0 + obj130;
						object obj132 = obj131 + obj131;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						if (0 >= (nint)tMP_MeshInfo4.normals)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v378 @ rdx_v38 (TMPro.TMP_MeshInfo[])+58+v442 @ rcx_v92*8]");
						TMP_MeshInfo tMP_MeshInfo5 = (TMP_MeshInfo)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						if (0 >= (nint)tMP_MeshInfo5.normals)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ r8_v31 (TMPro.TMP_MeshInfo)+20+v2211 @ rbx_v14 (System.Collections.Generic.List`1<System.Int32>)*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj133 = (nint)0 + (nint)1;
						Vector3[] normals11 = tMP_MeshInfo4.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj133) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals11))
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj134 = (nint)0 + (nint)1;
						Vector3[] normals12 = tMP_MeshInfo5.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj134) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals12))
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ r8_v31 (TMPro.TMP_MeshInfo)+24+v2211 @ rbx_v14 (System.Collections.Generic.List`1<System.Int32>)*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj135 = (nint)0 + (nint)2;
						Vector3[] normals13 = tMP_MeshInfo4.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj135) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals13))
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj136 = (nint)0 + (nint)2;
						Vector3[] normals14 = tMP_MeshInfo5.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj136) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals14))
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ r8_v31 (TMPro.TMP_MeshInfo)+28+v2211 @ rbx_v14 (System.Collections.Generic.List`1<System.Int32>)*4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj137 = (nint)0 + (nint)3;
						Vector3[] normals15 = tMP_MeshInfo4.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj137) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals15))
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rbx_v15 (TMPro.TMP_CharacterInfo[])+64+v279 @ r13_v7 (System.Int32)]");
						object obj138 = (nint)0 + (nint)3;
						Vector3[] normals16 = tMP_MeshInfo5.normals;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj138) >= System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals16))
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ r8_v31 (TMPro.TMP_MeshInfo)+2C+v2211 @ rbx_v14 (System.Collections.Generic.List`1<System.Int32>)*4]");
						_ = 0;
						pos = vector;
						s = vector;
						q = (Quaternion)vector;
						num6 = 0.5f;
					}
					num7++;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+2B0]");
					object obj139 = (nint)0 + (nint)1;
					num5 += 376;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+2B8]");
					if ((nint)obj139 < 0)
					{
						continue;
					}
					goto IL_180f;
				}
				goto IL_1cbe;
			}
			WaitForSeconds waitForSeconds = new WaitForSeconds(0.25f);
			_003C_003E2__current = waitForSeconds;
			_003C_003E1__state = 1;
			goto IL_1ce3;
			IL_1ce3:
			return true;
			IL_180f:
			TMP_TextInfo tMP_TextInfo8 = _003CtextInfo_003E5__2;
			int num20 = 0;
			int num21 = 0;
			int num22 = 0;
			while (true)
			{
				TMP_MeshInfo[] meshInfo4 = tMP_TextInfo8.meshInfo;
				if (num22 >= meshInfo4.Length)
				{
					break;
				}
				_003C_003Ec__DisplayClass10_0 obj140 = _003C_003E8__1;
				Comparison<int> comparison = obj140._003C_003E9__0;
				if (obj140._003C_003E9__0 == null)
				{
					object obj141 = _003C_003E8__1;
					Comparison<int> comparison2 = delegate
					{
						//IL_0061: Expected I4, but got O
						if (_003C_003E8__1.modifiedCharScale != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							if (_003C_003E8__1.modifiedCharScale != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								float num26 = default(float);
								float value = default(float);
								return num26.CompareTo(value);
							}
						}
						NullReferenceException ex2 = new NullReferenceException();
						return (int)ex2;
					};
					comparison = comparison2;
				}
				_003CscaleSortingOrder_003E5__4.Sort(comparison);
				TMP_TextInfo tMP_TextInfo9 = _003CtextInfo_003E5__2;
				TMP_MeshInfo[] meshInfo5 = tMP_TextInfo9.meshInfo;
				if (num21 < meshInfo5.Length)
				{
					object obj142 = num21 * 4;
					object obj143 = num21 + obj142;
					object obj144 = obj143 << 4;
					object obj145 = obj144 + 32;
					TMP_MeshInfo tMP_MeshInfo6 = (TMP_MeshInfo)(obj145 + (object)meshInfo5);
					((TMP_MeshInfo*)tMP_MeshInfo6)->SortGeometry(_003CscaleSortingOrder_003E5__4);
					TMP_TextInfo tMP_TextInfo10 = _003CtextInfo_003E5__2;
					TMP_MeshInfo[] meshInfo6 = tMP_TextInfo10.meshInfo;
					if (num21 < meshInfo6.Length)
					{
						TMP_TextInfo tMP_TextInfo11 = _003CtextInfo_003E5__2;
						TMP_MeshInfo[] meshInfo7 = tMP_TextInfo11.meshInfo;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ r15_v5 (System.Int32)+20+v448 @ rcx_v32 (TMPro.TMP_MeshInfo[])]");
						nint num23 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ r15_v5 (System.Int32)+30+v385 @ rdx_v15 (TMPro.TMP_MeshInfo[])]");
						((Mesh)num23).vertices = (Vector3[])0;
						TMP_TextInfo tMP_TextInfo12 = _003CtextInfo_003E5__2;
						TMP_MeshInfo[] meshInfo8 = tMP_TextInfo12.meshInfo;
						if (num21 < meshInfo8.Length)
						{
							TMP_TextInfo tMP_TextInfo13 = _003CtextInfo_003E5__2;
							TMP_MeshInfo[] meshInfo9 = tMP_TextInfo13.meshInfo;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ r15_v5 (System.Int32)+20+v451 @ rcx_v35 (TMPro.TMP_MeshInfo[])]");
							nint num24 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ r15_v5 (System.Int32)+48+v330 @ r8_v14 (TMPro.TMP_MeshInfo[])]");
							((Mesh)num24).SetUVs(0, (Vector4[])0);
							TMP_TextInfo tMP_TextInfo14 = _003CtextInfo_003E5__2;
							TMP_MeshInfo[] meshInfo10 = tMP_TextInfo14.meshInfo;
							if (num21 < meshInfo10.Length)
							{
								TMP_TextInfo tMP_TextInfo15 = _003CtextInfo_003E5__2;
								TMP_MeshInfo[] meshInfo11 = tMP_TextInfo15.meshInfo;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ r15_v5 (System.Int32)+20+v454 @ rcx_v38 (TMPro.TMP_MeshInfo[])]");
								nint num25 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ r15_v5 (System.Int32)+58+v388 @ rdx_v18 (TMPro.TMP_MeshInfo[])]");
								((Mesh)num25).colors32 = (Color32[])0;
								TMP_TextInfo tMP_TextInfo16 = _003CtextInfo_003E5__2;
								TMP_MeshInfo[] meshInfo12 = tMP_TextInfo16.meshInfo;
								if (num21 < meshInfo12.Length)
								{
									TMP_Text textComponent2 = vertexZoom.m_TextComponent;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v390 @ rdx_v21 (TMPro.TMP_MeshInfo[])+20+v273 @ r15_v5 (System.Int32)]");
									textComponent2.UpdateGeometry((Mesh)0, num21);
									tMP_TextInfo8 = _003CtextInfo_003E5__2;
									num21++;
									num20 += 80;
									bool flag2 = _003CtextInfo_003E5__2 != null;
									num22 = num21;
									if (!flag2)
									{
										throw new NullReferenceException();
									}
									continue;
								}
							}
						}
					}
				}
				goto IL_1cbe;
			}
			WaitForSeconds waitForSeconds2 = new WaitForSeconds(0.1f);
			_003C_003E2__current = waitForSeconds2;
			_003C_003E1__state = 2;
			goto IL_1ce3;
			IL_1cbe:
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
			return (byte)(int)ex != 0;
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
		if (obj == m_TextComponent)
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
