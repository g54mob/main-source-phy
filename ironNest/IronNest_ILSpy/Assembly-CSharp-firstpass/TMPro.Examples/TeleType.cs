using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class TeleType : MonoBehaviour
{
	private sealed class _003CStart_003Ed__4 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TeleType _003C_003E4__this;

		private int _003CtotalVisibleCharacters_003E5__2;

		private int _003Ccounter_003E5__3;

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

		private bool MoveNext()
		{
			//IL_0227: Expected I4, but got I8
			//IL_03d3: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_0191: Expected I4, but got I8
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_00fb: Expected I4, but got I8
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Expected O, but got Unknown
			//IL_00e7: Expected I4, but got I8
			//IL_02fe: Expected O, but got I4
			//IL_0308: Unknown result type (might be due to invalid IL or missing references)
			//IL_030d: Expected I4, but got Unknown
			//IL_008d: Expected I4, but got I8
			//IL_009d: Expected O, but got I4
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Expected I4, but got Unknown
			TeleType teleType = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			int num2;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						object obj3 = obj2 - 1;
						if (flag)
						{
							_003C_003E1__state = -1;
							goto IL_034d;
						}
						if ((nint)obj3 != 1)
						{
							return false;
						}
						_003C_003E1__state = -1;
						object obj4 = _003CtotalVisibleCharacters_003E5__2 + 1;
						int num = _003Ccounter_003E5__3 % obj4;
						if ((object)_003C_003E4__this != null)
						{
							num2 = num;
							goto IL_03fc;
						}
					}
					else
					{
						_003C_003E1__state = -1;
						if ((object)_003C_003E4__this != null && (object)teleType.m_textMeshPro != null)
						{
							teleType.m_textMeshPro.text = teleType.label01;
							WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
							_003C_003E2__current = waitForSeconds;
							_003C_003E1__state = 3;
							return true;
						}
					}
				}
				else
				{
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this != null && (object)teleType.m_textMeshPro != null)
					{
						teleType.m_textMeshPro.text = teleType.label02;
						WaitForSeconds waitForSeconds2 = new WaitForSeconds(1f);
						_003C_003E2__current = waitForSeconds2;
						_003C_003E1__state = 2;
						return true;
					}
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null && (object)teleType.m_textMeshPro != null)
				{
					teleType.m_textMeshPro.ForceMeshUpdate();
					if ((object)teleType.m_textMeshPro != null)
					{
						TMP_TextInfo textInfo = teleType.m_textMeshPro.textInfo;
						if (textInfo != null)
						{
							_003CtotalVisibleCharacters_003E5__2 = textInfo.characterCount;
							_003Ccounter_003E5__3 = 0;
							object obj5 = textInfo.characterCount + 1;
							int num3 = _003Ccounter_003E5__3 % obj5;
							num2 = num3;
							goto IL_03fc;
						}
					}
				}
			}
			goto IL_03c5;
			IL_034d:
			int num4 = _003Ccounter_003E5__3 + 1;
			_003Ccounter_003E5__3 = num4;
			WaitForSeconds waitForSeconds3 = new WaitForSeconds(0.05f);
			_003C_003E2__current = waitForSeconds3;
			_003C_003E1__state = 4;
			return true;
			IL_03fc:
			if ((object)teleType.m_textMeshPro != null)
			{
				teleType.m_textMeshPro.maxVisibleCharacters = num2;
				if (num2 < _003CtotalVisibleCharacters_003E5__2)
				{
					goto IL_034d;
				}
				WaitForSeconds waitForSeconds4 = new WaitForSeconds(1f);
				_003C_003E2__current = waitForSeconds4;
				_003C_003E1__state = 1;
				return true;
			}
			goto IL_03c5;
			IL_03c5:
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

	private string label01;

	private string label02;

	private TMP_Text m_textMeshPro;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		TMP_Text textMeshPro = default(TMP_Text);
		m_textMeshPro = textMeshPro;
		m_textMeshPro.text = label01;
		m_textMeshPro.textWrappingMode = TextWrappingModes.Normal;
		m_textMeshPro.alignment = TextAlignmentOptions.Top;
	}

	private IEnumerator Start()
	{
		_003CStart_003Ed__4 obj = new _003CStart_003Ed__4(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public TeleType()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39DBD]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		label01 = "Example <sprite=2> of using <sprite=7> <#ffa000>Graphics Inline</color> <sprite=5> with Text in <font=\"Bangers SDF\" material=\"Bangers SDF - Drop Shadow\">TextMesh<#40a0ff>Pro</color></font><sprite=0> and Unity<sprite=1>";
		label02 = "Example <sprite=2> of using <sprite=7> <#ffa000>Graphics Inline</color> <sprite=5> with Text in <font=\"Bangers SDF\" material=\"Bangers SDF - Drop Shadow\">TextMesh<#40a0ff>Pro</color></font><sprite=0> and Unity<sprite=2>";
		base._002Ector();
	}
}
