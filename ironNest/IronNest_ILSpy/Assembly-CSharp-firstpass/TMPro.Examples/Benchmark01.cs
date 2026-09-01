using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class Benchmark01 : MonoBehaviour
{
	private sealed class _003CStart_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Benchmark01 _003C_003E4__this;

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
			//IL_009c: Expected I4, but got I8
			//IL_08fd: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_0078: Expected I4, but got I8
			//IL_005b: Expected I4, but got I8
			//IL_03cf: Expected O, but got I
			//IL_041d: Expected O, but got I
			//IL_0162: Expected O, but got I
			//IL_0732: Expected O, but got I
			//IL_0756: Unknown result type (might be due to invalid IL or missing references)
			//IL_075b: Expected O, but got Unknown
			//IL_0786: Expected O, but got I
			//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c8: Expected O, but got Unknown
			//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_07d7: Expected O, but got Unknown
			//IL_04bc: Expected O, but got I
			//IL_0807: Expected O, but got I
			//IL_047c: Expected O, but got I
			//IL_047c: Expected O, but got I
			//IL_04f7: Expected O, but got I
			//IL_025b: Expected O, but got I
			//IL_06f0: Expected O, but got I
			//IL_0967: Expected O, but got I
			//IL_0858: Expected O, but got I
			//IL_086c: Expected O, but got I
			//IL_0532: Expected O, but got I
			//IL_08aa: Expected O, but got I
			//IL_0895: Expected O, but got I
			//IL_056d: Expected O, but got I
			//IL_057d: Expected O, but got I
			//IL_02a0: Expected O, but got I
			//IL_05b2: Expected O, but got I
			//IL_0604: Expected I, but got O
			//IL_0333: Expected O, but got I
			//IL_036e: Expected O, but got I
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
						result = false;
					}
					goto IL_0926;
				}
				_003C_003E1__state = -1;
				num = _003Ci_003E5__2 + 1;
				goto IL_092b;
			}
			_003C_003E1__state = -1;
			nint num2 = default(nint);
			if ((object)_003C_003E4__this != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+20]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+20]");
					if ((nint)0 != 1)
					{
						goto IL_0609;
					}
					GameObject gameObject = _003C_003E4__this.gameObject;
					if ((object)gameObject != null)
					{
						TextMesh textMesh = gameObject.AddComponent<TextMesh>();
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+30]");
						Font font;
						if ((UnityEngine.Object)0 == null)
						{
							Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Font));
							UnityEngine.Object obj2 = Resources.Load("Fonts/ARIAL", typeFromHandle);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+48]");
							if ((nint)0 == 0)
							{
								goto IL_08ef;
							}
							bool flag3 = (object)obj2 == null;
							font = null;
							if (!flag3)
							{
								bool flag4 = (object)obj2.GetType() != typeof(Font);
								font = null;
								if (!flag4)
								{
									font = (Font)obj2;
								}
							}
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+48]");
							if ((nint)0 == 0)
							{
								goto IL_08ef;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+30]");
							font = (Font)0;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+48]");
						((TextMesh)0).font = font;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+48]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+48]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+48]");
								Font font2 = ((TextMesh)0).font;
								if ((object)font2 != null)
								{
									Material material = font2.material;
									Renderer renderer = default(Renderer);
									if ((object)renderer != null)
									{
										renderer.SetMaterial(material);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+48]");
										if ((nint)0 != 0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+48]");
											((TextMesh)0).fontSize = 48;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+48]");
											if ((nint)0 != 0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+48]");
												((TextMesh)0).anchor = TextAnchor.MiddleCenter;
												num2 = 4;
												goto IL_0609;
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					GameObject gameObject2 = _003C_003E4__this.gameObject;
					if ((object)gameObject2 != null)
					{
						TextMeshPro textMeshPro = gameObject2.AddComponent<TextMeshPro>();
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
						object obj3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
						if ((nint)0 != 0)
						{
							object obj4 = obj3;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v708 @ r8_v16+5F8] (should have been resolved before IL gen)");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+28]");
							if ((UnityEngine.Object)0 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
								if ((nint)0 == 0)
								{
									goto IL_08ef;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
								nint num3 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+28]");
								((TMP_Text)num3).font = (TMP_FontAsset)0;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
								((TMP_Text)0).fontSize = 48f;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
									((TMP_Text)0).alignment = TextAlignmentOptions.Center;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
									if ((nint)0 != 0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
										((TMP_Text)0).extraPadding = true;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
										if ((nint)0 != 0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
											((TMP_Text)0).textWrappingMode = TextWrappingModes.NoWrap;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
											object obj5 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
											if ((nint)0 != 0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ rax_v49+100]");
												object obj6 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ rax_v49+100]");
												if ((nint)0 != 0)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ rdx_v33+88]");
													_ = 0;
													Material material2 = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - Drop Shadow");
													num2 = (nint)material2;
													goto IL_0609;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			goto IL_08ef;
			IL_092b:
			_003Ci_003E5__2 = num;
			if (num <= 1000000)
			{
				if ((object)_003C_003E4__this != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+20]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+20]");
						if ((nint)0 == 1)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ecx\"");
							int num4 = default(int);
							string text = num4.ToString();
							string text2 = "The <color=#0050FF>count is: </color>" + text;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+48]");
							if ((nint)0 == 0)
							{
								goto IL_08ef;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+48]");
							((TextMesh)0).text = text2;
						}
						goto IL_0991;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
						object obj7 = num2 >> 6;
						object obj8 = obj7 >> 31;
						object obj9 = obj7 + obj8;
						object obj10 = obj9 * 1000;
						float arg = (float)_003Ci_003E5__2 - (float)obj10;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
						((TMP_Text)0).SetText("The <#0050FF>count is: </color>{0}", arg);
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ecx\"");
						object obj11 = (object)"The <#0050FF>count is: </color>{0}" >> 6;
						object obj12 = obj11 >> 31;
						object obj13 = obj11 + obj12;
						object obj14 = obj13 * 1000;
						object obj15 = _003Ci_003E5__2 - obj14;
						if ((nint)obj15 != 999)
						{
							goto IL_0991;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
						object obj16 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
						if ((nint)0 != 0)
						{
							object obj17 = obj16;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v732 @ rdx_v11+568] (should have been resolved before IL gen)");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+50]");
							UnityEngine.Object obj18 = default(UnityEngine.Object);
							bool flag5 = obj18 == (UnityEngine.Object)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
							object obj19 = 0;
							if (flag5)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+58]");
								object obj20 = 0;
							}
							else
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+50]");
								object obj20 = 0;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (UnityEngine.Component)+38]");
							if ((nint)0 != 0)
							{
								object obj21 = obj19;
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v853 @ r8_v7+578] (should have been resolved before IL gen)");
								object obj22 = obj16;
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v858 @ r8_v9+578] (should have been resolved before IL gen)");
								goto IL_0991;
							}
						}
					}
				}
				goto IL_08ef;
			}
			_003C_003E2__current = null;
			_003C_003E1__state = 2;
			goto IL_09cd;
			IL_0926:
			return result;
			IL_09cd:
			result = true;
			goto IL_0926;
			IL_08ef:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0609:
			num = 0;
			goto IL_092b;
			IL_0991:
			_003C_003E2__current = null;
			_003C_003E1__state = 1;
			goto IL_09cd;
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

	public TMP_FontAsset TMProFont;

	public Font TextMeshFont;

	private TextMeshPro m_textMeshPro;

	private TextContainer m_textContainer;

	private TextMesh m_textMesh;

	private const string label01 = "The <#0050FF>count is: </color>{0}";

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
