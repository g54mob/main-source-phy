using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class VertexColorCycler : MonoBehaviour
{
	private sealed class _003CAnimateVertexColors_003Ed__3 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public VertexColorCycler _003C_003E4__this;

		private TMP_TextInfo _003CtextInfo_003E5__2;

		private int _003CcurrentCharacter_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAnimateVertexColors_003Ed__3(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0071: Expected I4, but got I8
			//IL_0015: Expected O, but got I4
			//IL_005d: Expected I4, but got I8
			//IL_00df: Expected O, but got Ref
			//IL_0426: Expected I4, but got O
			//IL_0170: Expected O, but got I4
			//IL_01b9: Expected O, but got I
			//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ce: Expected O, but got Unknown
			//IL_01eb: Expected O, but got I
			//IL_020d: Expected O, but got I4
			//IL_0239: Expected O, but got I4
			//IL_0436: Expected O, but got I4
			//IL_0443: Unknown result type (might be due to invalid IL or missing references)
			//IL_0448: Expected I4, but got Unknown
			//IL_02e3: Expected O, but got I
			//IL_0327: Expected O, but got I
			//IL_0366: Expected O, but got I
			VertexColorCycler vertexColorCycler = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag && (nint)obj != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
				vertexColorCycler.m_TextComponent.ForceMeshUpdate();
				TMP_TextInfo textInfo = vertexColorCycler.m_TextComponent.textInfo;
				_003CtextInfo_003E5__2 = textInfo;
				_003CcurrentCharacter_003E5__3 = 0;
				Color color = vertexColorCycler.m_TextComponent.color;
				object obj2 = default(object);
				Color32 color2 = (Color)(&obj2);
			}
			TMP_TextInfo tMP_TextInfo = _003CtextInfo_003E5__2;
			if (tMP_TextInfo.characterCount != 0)
			{
				TMP_CharacterInfo[] characterInfo = tMP_TextInfo.characterInfo;
				if (_003CcurrentCharacter_003E5__3 < characterInfo.Length)
				{
					TMP_MeshInfo[] meshInfo = tMP_TextInfo.meshInfo;
					object obj3 = _003CcurrentCharacter_003E5__3 * 376;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v366 @ rcx_v12+50+v117 @ rdx_v7 (TMPro.TMP_CharacterInfo[])]");
					if ((nint)0 < (nint)meshInfo.Length)
					{
						TMP_TextInfo tMP_TextInfo2 = _003CtextInfo_003E5__2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v366 @ rcx_v12+50+v117 @ rdx_v7 (TMPro.TMP_CharacterInfo[])]");
						object obj4 = (nint)0 * (nint)4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v366 @ rcx_v12+50+v117 @ rdx_v7 (TMPro.TMP_CharacterInfo[])]");
						object obj5 = 0 + obj4;
						object obj6 = obj5 + obj5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ rbx_v7 (TMPro.TMP_MeshInfo[])+58+v141 @ rcx_v14*8]");
						TMP_MeshInfo tMP_MeshInfo = (TMP_MeshInfo)0;
						TMP_CharacterInfo[] characterInfo2 = tMP_TextInfo2.characterInfo;
						object obj7 = _003CcurrentCharacter_003E5__3 * 376;
						TMP_TextInfo tMP_TextInfo3 = _003CtextInfo_003E5__2;
						TMP_CharacterInfo[] characterInfo3 = tMP_TextInfo3.characterInfo;
						object obj8 = _003CcurrentCharacter_003E5__3 * 376;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v517 @ rcx_v16+190+v516 @ rdx_v11 (TMPro.TMP_CharacterInfo[])]");
						if ((nint)0 == 0)
						{
							goto IL_0426;
						}
						int num = UnityEngine.Random.Range(0, 255);
						int num2 = UnityEngine.Random.Range(0, 255);
						int num3 = UnityEngine.Random.Range(0, 255);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v142 @ rcx_v15+64+v514 @ rdx_v9 (TMPro.TMP_CharacterInfo[])]");
						if (0 < (nint)tMP_MeshInfo.normals)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v142 @ rcx_v15+64+v514 @ rdx_v9 (TMPro.TMP_CharacterInfo[])]");
							object obj9 = (nint)0 + (nint)1;
							Vector3[] normals = tMP_MeshInfo.normals;
							if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9) < System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals))
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v142 @ rcx_v15+64+v514 @ rdx_v9 (TMPro.TMP_CharacterInfo[])]");
								object obj10 = (nint)0 + (nint)2;
								Vector3[] normals2 = tMP_MeshInfo.normals;
								if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10) < System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals2))
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v142 @ rcx_v15+64+v514 @ rdx_v9 (TMPro.TMP_CharacterInfo[])]");
									object obj11 = (nint)0 + (nint)3;
									Vector3[] normals3 = tMP_MeshInfo.normals;
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11) < System.Runtime.CompilerServices.Unsafe.As<Vector3[], UIntPtr>(ref normals3))
									{
										vertexColorCycler.m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
										goto IL_0426;
									}
								}
							}
						}
					}
				}
				IndexOutOfRangeException ex = new IndexOutOfRangeException();
				return (byte)(int)ex != 0;
			}
			WaitForSeconds waitForSeconds = new WaitForSeconds(0.25f);
			_003C_003E2__current = waitForSeconds;
			_003C_003E1__state = 1;
			goto IL_0470;
			IL_0470:
			return true;
			IL_0426:
			object obj12 = _003CcurrentCharacter_003E5__3 + 1;
			int num4 = obj12 % tMP_TextInfo.characterCount;
			_003CcurrentCharacter_003E5__3 = num4;
			WaitForSeconds waitForSeconds2 = new WaitForSeconds(0.05f);
			_003C_003E2__current = waitForSeconds2;
			_003C_003E1__state = 2;
			goto IL_0470;
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

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		TMP_Text textComponent = default(TMP_Text);
		m_TextComponent = textComponent;
	}

	private void Start()
	{
		_003CAnimateVertexColors_003Ed__3 obj = new _003CAnimateVertexColors_003Ed__3(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private IEnumerator AnimateVertexColors()
	{
		_003CAnimateVertexColors_003Ed__3 obj = new _003CAnimateVertexColors_003Ed__3(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
