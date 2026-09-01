using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

public class EnvMapAnimator : MonoBehaviour
{
	private sealed class _003CStart_003Ed__4 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public EnvMapAnimator _003C_003E4__this;

		private Matrix4x4 _003Cmatrix_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CStart_003Ed__4(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0042: Expected I4, but got I8
			//IL_004d: Expected O, but got I4
			//IL_0095: Expected I4, but got I8
			//IL_013f: Expected I4, but got O
			//IL_0145: Unknown result type (might be due to invalid IL or missing references)
			//IL_014a: Expected Ref, but got Unknown
			//IL_00dd: Expected O, but got Ref
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39C44]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			EnvMapAnimator envMapAnimator = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003Cmatrix_003E5__2 = (Matrix4x4)0;
				_ = 0;
				_ = 0;
				_ = 0;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
			}
			float time = Time.time;
			if ((object)_003C_003E4__this != null)
			{
				float time2 = Time.time;
				float time3 = Time.time;
				Vector3 euler = default(Vector3);
				Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
				Vector3 pos = default(Vector3);
				Quaternion q = default(Quaternion);
				Matrix4x4.Internal_SetTRS(ref *(Matrix4x4*)(this + 40), ref pos, ref q, ref euler);
				object obj = default(object);
				envMapAnimator.m_material.SetMatrix("_EnvMatrix", (Matrix4x4)(&obj));
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			NullReferenceException ex = new NullReferenceException();
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

	public Vector3 RotationSpeeds;

	private TMP_Text m_textMeshPro;

	private Material m_material;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		TMP_Text textMeshPro = default(TMP_Text);
		m_textMeshPro = textMeshPro;
		Material fontSharedMaterial = m_textMeshPro.fontSharedMaterial;
		m_material = fontSharedMaterial;
	}

	private IEnumerator Start()
	{
		_003CStart_003Ed__4 obj = new _003CStart_003Ed__4(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
