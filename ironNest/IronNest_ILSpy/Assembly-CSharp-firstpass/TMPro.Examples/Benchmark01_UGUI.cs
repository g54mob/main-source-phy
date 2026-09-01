using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro.Examples;

public class Benchmark01_UGUI : MonoBehaviour
{
	private sealed class _003CStart_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Benchmark01_UGUI _003C_003E4__this;

		private int _003Ci_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CStart_003Ed__10(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0094: Expected I4, but got I8
			//IL_0728: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_0070: Expected I4, but got I8
			//IL_005b: Expected I4, but got I8
			//IL_0294: Expected O, but got I
			//IL_0547: Expected O, but got I
			//IL_0333: Expected O, but got I
			//IL_015a: Expected O, but got I
			//IL_02f3: Expected O, but got I
			//IL_02f3: Expected O, but got I
			//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f2: Expected O, but got Unknown
			//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0601: Expected O, but got Unknown
			//IL_04c8: Expected O, but got I
			//IL_036e: Expected O, but got I
			//IL_01f9: Expected O, but got I
			//IL_0631: Expected O, but got I
			//IL_03a9: Expected O, but got I
			//IL_03b9: Expected O, but got I
			//IL_01b9: Expected O, but got I
			//IL_01b9: Expected O, but got I
			//IL_0234: Expected O, but got I
			//IL_03ee: Expected O, but got I
			//IL_0682: Expected O, but got I
			//IL_0696: Expected O, but got I
			//IL_06d4: Expected O, but got I
			//IL_06bf: Expected O, but got I
			Component component = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			bool result;
			int num;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					bool flag2 = (nint)obj != 1;
					result = false;
					if (!flag2)
					{
						_003C_003E1__state = -1;
						return false;
					}
					goto IL_0751;
				}
				_003C_003E1__state = -1;
				num = _003Ci_003E5__2 + 1;
				goto IL_0756;
			}
			_003C_003E1__state = -1;
			if ((object)_003C_003E4__this != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+20]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+20]");
					if ((nint)0 != 1)
					{
						goto IL_043e;
					}
					GameObject gameObject = _003C_003E4__this.gameObject;
					if ((object)gameObject != null)
					{
						Text text = gameObject.AddComponent<Text>();
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+38]");
						if ((UnityEngine.Object)0 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+48]");
							if ((nint)0 == 0)
							{
								goto IL_071a;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+48]");
							nint num2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+38]");
							((Text)num2).font = (Font)0;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+48]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+48]");
							((Text)0).fontSize = 48;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+48]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+48]");
								((Text)0).alignment = TextAnchor.MiddleCenter;
								goto IL_043e;
							}
						}
					}
				}
				else
				{
					GameObject gameObject2 = _003C_003E4__this.gameObject;
					if ((object)gameObject2 != null)
					{
						TextMeshProUGUI textMeshProUGUI = gameObject2.AddComponent<TextMeshProUGUI>();
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+30]");
						if ((UnityEngine.Object)0 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
							if ((nint)0 == 0)
							{
								goto IL_071a;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
							nint num3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+30]");
							((TMP_Text)num3).font = (TMP_FontAsset)0;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
							((TMP_Text)0).fontSize = 48f;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
								((TMP_Text)0).alignment = TextAlignmentOptions.Center;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
									((TMP_Text)0).extraPadding = true;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
									object obj2 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
									if ((nint)0 != 0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ rax_v48+100]");
										object obj3 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ rax_v48+100]");
										if ((nint)0 != 0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rdx_v32+88]");
											_ = 0;
											Material material = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - BEVEL");
											goto IL_043e;
										}
									}
								}
							}
						}
					}
				}
			}
			goto IL_071a;
			IL_0756:
			_003Ci_003E5__2 = num;
			if (num <= 1000000)
			{
				if ((object)_003C_003E4__this != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+20]");
					int num4 = default(int);
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+20]");
						if ((nint)0 == 1)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+48]");
							object obj4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ecx\"");
							string text2 = num4.ToString();
							string text3 = "The <color=#0050FF>count is: </color>" + text2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+48]");
							if ((nint)0 == 0)
							{
								goto IL_071a;
							}
							object obj5 = obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v625 @ r8_v12+5E8] (should have been resolved before IL gen)");
						}
						goto IL_077d;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
					object obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ecx\"");
					string text4 = num4.ToString();
					string text5 = "The <#0050FF>count is: </color>" + text4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
					if ((nint)0 != 0)
					{
						object obj7 = obj6;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v602 @ r8_v4+558] (should have been resolved before IL gen)");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ecx\"");
						object obj8 = (object)text5 >> 6;
						object obj9 = obj8 >> 31;
						object obj10 = obj8 + obj9;
						object obj11 = obj10 * 1000;
						object obj12 = _003Ci_003E5__2 - obj11;
						if ((nint)obj12 != 999)
						{
							goto IL_077d;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
						object obj13 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
						if ((nint)0 != 0)
						{
							object obj14 = obj13;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v650 @ rdx_v12+568] (should have been resolved before IL gen)");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+50]");
							UnityEngine.Object obj15 = default(UnityEngine.Object);
							bool flag3 = obj15 == (UnityEngine.Object)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
							object obj16 = 0;
							if (flag3)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+58]");
								object obj17 = 0;
							}
							else
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+50]");
								object obj17 = 0;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Component)+40]");
							if ((nint)0 != 0)
							{
								object obj18 = obj16;
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v696 @ r8_v7+578] (should have been resolved before IL gen)");
								object obj19 = obj13;
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v701 @ r8_v9+578] (should have been resolved before IL gen)");
								goto IL_077d;
							}
						}
					}
				}
				goto IL_071a;
			}
			_003C_003E2__current = null;
			_003C_003E1__state = 2;
			return true;
			IL_077d:
			_003C_003E2__current = null;
			_003C_003E1__state = 1;
			result = true;
			goto IL_0751;
			IL_0751:
			return result;
			IL_043e:
			num = 0;
			goto IL_0756;
			IL_071a:
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

	public int BenchmarkType;

	public Canvas canvas;

	public TMP_FontAsset TMProFont;

	public Font TextMeshFont;

	private TextMeshProUGUI m_textMeshPro;

	private Text m_textMesh;

	private const string label01 = "The <#0050FF>count is: </color>";

	private const string label02 = "The <color=#0050FF>count is: </color>";

	private Material m_material01;

	private Material m_material02;

	private IEnumerator Start()
	{
		_003CStart_003Ed__10 obj = new _003CStart_003Ed__10(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
