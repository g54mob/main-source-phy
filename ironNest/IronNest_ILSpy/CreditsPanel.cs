using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CreditsPanel : MonoBehaviour
{
	private sealed class _003CCheckVisibility_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CreditsPanel _003C_003E4__this;

		private WaitForSecondsRealtime _003Cdelay_003E5__2;

		private Vector3[] _003CviewportCorners_003E5__3;

		private Vector3[] _003CchunkCorners_003E5__4;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCheckVisibility_003Ed__31(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_0126: Expected I4, but got I8
			//IL_04b2: Expected I4, but got O
			//IL_0197: Expected O, but got Ref
			//IL_01b5: Expected O, but got I
			//IL_020b: Expected O, but got Ref
			//IL_02d2: Invalid comparison between F4 and I
			//IL_03a7: Invalid comparison between I and F4
			CreditsPanel creditsPanel = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(0.16f);
				_003Cdelay_003E5__2 = waitForSecondsRealtime;
				Vector3[] array = new Vector3[4];
				_003CviewportCorners_003E5__3 = array;
				Vector3[] array2 = new Vector3[4];
				_003CchunkCorners_003E5__4 = array2;
				if ((object)_003C_003E4__this != null)
				{
					ScrollRect scrollRect = creditsPanel._scrollRect;
					if ((object)creditsPanel._scrollRect != null && (object)scrollRect.m_Viewport != null)
					{
						scrollRect.m_Viewport.GetWorldCorners(_003CviewportCorners_003E5__3);
						goto IL_0145;
					}
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_0145;
				}
			}
			goto IL_04a4;
			IL_0145:
			if (creditsPanel._sections != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<CreditsSection>.Enumerator enumerator = default(List<CreditsSection>.Enumerator);
				object obj = default(object);
				List<RectTransform>.Enumerator enumerator2 = default(List<RectTransform>.Enumerator);
				RectTransform rectTransform2 = default(RectTransform);
				while (true)
				{
					if (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						bool flag = obj == null;
						RectTransform rectTransform = (RectTransform)(&enumerator);
						if (flag)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ stack_18_v4+40]");
						rectTransform = (RectTransform)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ stack_18_v4+40]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
							while (enumerator2.MoveNext())
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
								bool flag2 = (object)rectTransform2 == null;
								rectTransform = (RectTransform)(&enumerator2);
								if (!flag2)
								{
									rectTransform2.GetWorldCorners(_003CchunkCorners_003E5__4);
									Vector3[] array3 = _003CchunkCorners_003E5__4;
									bool flag3 = _003CchunkCorners_003E5__4 == null;
									rectTransform = rectTransform2;
									if (!flag3)
									{
										rectTransform = (RectTransform)(object)_003CviewportCorners_003E5__3;
										if (_003CviewportCorners_003E5__3 != null)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v771 @ rcx_v24 (UnityEngine.RectTransform)+18]");
											if ((nint)0 > (nint)1)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v771 @ rcx_v24 (UnityEngine.RectTransform)+30]");
												float num = 0f + 250f;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v561 @ rax_v31 (UnityEngine.Vector3[])+24]");
												bool active;
												if (num < 0f)
												{
													active = false;
												}
												else
												{
													if (_003CchunkCorners_003E5__4 == null)
													{
														throw new NullReferenceException();
													}
													if (array3.Length <= 1)
													{
														throw new IndexOutOfRangeException();
													}
													if (_003CviewportCorners_003E5__3 == null)
													{
														throw new NullReferenceException();
													}
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v771 @ rcx_v24 (UnityEngine.RectTransform)+18]");
													if ((nint)0 <= (nint)0)
													{
														throw new IndexOutOfRangeException();
													}
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v771 @ rcx_v24 (UnityEngine.RectTransform)+24]");
													float num2 = 0f - 250f;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v561 @ rax_v31 (UnityEngine.Vector3[])+30]");
													bool flag4 = 0f < num2;
													active = !flag4;
												}
												GameObject gameObject = rectTransform2.gameObject;
												bool flag5 = (object)gameObject == null;
												rectTransform = rectTransform2;
												if (!flag5)
												{
													gameObject.SetActive(active);
													continue;
												}
												throw new NullReferenceException();
											}
											throw new IndexOutOfRangeException();
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							enumerator2.Dispose();
							continue;
						}
						throw new NullReferenceException();
					}
					enumerator.Dispose();
					_003C_003E2__current = _003Cdelay_003E5__2;
					_003C_003E1__state = 1;
					return true;
				}
				throw new NullReferenceException();
			}
			goto IL_04a4;
			IL_04a4:
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

	private sealed class _003CLoadCredits_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CreditsPanel _003C_003E4__this;

		private float _003CaccumulatedHeight_003E5__2;

		private List<CreditsSectionConfig>.Enumerator _003C_003E7__wrap2;

		private CreditsSection _003Csection_003E5__4;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CLoadCredits_003Ed__29(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		unsafe void IDisposable.Dispose()
		{
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Expected O, but got Unknown
			//IL_002f: Expected O, but got I4
			if (_003C_003E1__state != -3)
			{
				object obj = _003C_003E1__state - 1;
				if ((nint)obj > 1)
				{
					return;
				}
			}
			_ = 4294967295L;
			object obj2 = default(object);
			List<CreditsSectionConfig>.Enumerator enumerator = (List<CreditsSectionConfig>.Enumerator)(obj2 + 48);
			((List<CreditsSectionConfig>.Enumerator*)enumerator)->Dispose();
		}

		private unsafe bool MoveNext()
		{
			//IL_0762: Expected O, but got I
			//IL_001b: Expected O, but got I
			//IL_0083: Expected O, but got I
			//IL_00d7: Expected F4, but got I
			//IL_00e0: Expected O, but got I4
			//IL_079a: Expected O, but got I4
			//IL_0215: Expected O, but got I
			//IL_0110: Expected F4, but got I
			//IL_0110: Expected O, but got I
			//IL_0153: Expected O, but got I
			//IL_0832: Expected O, but got I4
			//IL_0857: Expected F4, but got I
			//IL_0877: Expected O, but got I4
			//IL_0298: Expected O, but got I4
			//IL_02b2: Expected O, but got I
			//IL_056a: Expected O, but got I4
			//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fa: Expected O, but got Unknown
			//IL_0321: Expected O, but got I
			//IL_0321: Expected O, but got I
			//IL_0598: Expected O, but got I
			//IL_05c0: Expected O, but got I4
			//IL_05c9: Expected O, but got I4
			//IL_0354: Expected I4, but got O
			//IL_05f6: Expected O, but got I
			//IL_0612: Expected I, but got O
			//IL_061b: Expected O, but got I4
			//IL_0624: Expected O, but got I4
			//IL_037f: Expected O, but got I
			//IL_037f: Expected O, but got I
			//IL_038f: Expected O, but got I
			//IL_0651: Expected O, but got I
			//IL_03e7: Expected O, but got I
			//IL_0437: Expected O, but got I
			//IL_044e: Expected O, but got I
			//IL_0486: Expected O, but got I4
			//IL_0803: Expected O, but got I4
			//IL_04be: Expected O, but got I
			//IL_0507: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+20]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+10]");
			bool flag = (nint)0 == 0;
			RectTransform.Axis axis = default(RectTransform.Axis);
			float num2;
			Vector2 vector;
			nint num5 = default(nint);
			RectTransform.Axis axis2;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+10]");
				object obj2 = -1;
				if (flag)
				{
					_ = 4294967293L;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+48]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+48]");
					bool flag2 = (nint)0 == 0;
					int num = (int)axis;
					if (!flag2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rcx_v55+38]");
						bool flag3 = (nint)0 == 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rcx_v55+48]");
						num2 = 0f;
						object obj4 = 0;
						if (!flag3)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rcx_v55+38]");
							nint num3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rcx_v55+48]");
							((RectTransform)num3).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rcx_v55+48]");
							float num4 = 0f;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+28]");
							float size = num4 + 0f;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+20]");
							((RectTransform)0).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
							_ = 0;
							_ = 2;
							return true;
						}
						num = 1;
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				if ((nint)obj2 != 1)
				{
					return false;
				}
				_ = 4294967293L;
				_ = 0;
			}
			else
			{
				_ = 4294967295L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+20]");
				bool flag4 = (nint)0 == 0;
				axis2 = axis;
				if (flag4)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+A4]");
				if ((nint)0 != 0)
				{
					return false;
				}
				_ = 1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+20]");
				bool flag5 = (nint)0 == 0;
				axis2 = axis;
				if (flag5)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+20]");
				Vector2 sizeDelta = ((RectTransform)0).sizeDelta;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+80]");
				bool flag6 = (nint)0 == 0;
				Vector2 vector2 = default(Vector2);
				vector = vector2;
				axis2 = RectTransform.Axis.Horizontal;
				if (flag6)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				_ = 4294967293L;
				object obj6 = default(object);
				object obj5 = obj6;
				num5 = 0;
			}
			List<CreditsSectionConfig>.Enumerator enumerator = (List<CreditsSectionConfig>.Enumerator)(axis + 48);
			if (((List<CreditsSectionConfig>.Enumerator*)enumerator)->MoveNext())
			{
				object obj7 = axis + 48;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+28]");
				vector = (Vector2)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+20]");
				bool flag7 = (nint)0 == 0;
				nint num6 = 0;
				CreditsSectionConfig config = default(CreditsSectionConfig);
				bool flag8 = (byte)(&config) != 0;
				if (!flag7)
				{
					Vector2 vector3 = vector;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+7C]");
					vector = vector3 + 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+70]");
					nint num7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+20]");
					CreditsSection creditsSection = UnityEngine.Object.Instantiate((CreditsSection)num7, (Transform)0);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+98]");
					bool flag9 = (nint)0 == 0;
					num6 = 0;
					int num = (int)creditsSection;
					if (!flag9)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+98]");
						nint num8 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+48]");
						((List<CreditsSection>)num8).Add((CreditsSection)0);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+48]");
						object obj8 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+48]");
						bool flag10 = (nint)0 == 0;
						num5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+48]");
						num = 0;
						if (!flag10)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+28]");
							nint num9 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
							object obj5 = num9 ^ 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rcx_v20+38]");
							bool flag11 = (nint)0 == 0;
							num5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+48]");
							num = 0;
							if (!flag11)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v702 @ rcx_v20+38]");
								Vector2 vector4 = default(Vector2);
								((RectTransform)0).anchoredPosition = vector4;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+20]");
								Action onFirstChunkInitialized = ((CreditsPanel)0).RevealCredits;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+48]");
								bool flag12 = (nint)0 == 0;
								num5 = 0;
								vector = vector4;
								object obj4 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+20]");
								num = 0;
								if (flag12)
								{
									throw new NullReferenceException();
								}
								CreditsSection._003CInitialize_003Ed__10 obj9 = new CreditsSection._003CInitialize_003Ed__10(0);
								obj9._003C_003E1__state = 0;
								bool flag13 = obj9 == null;
								num5 = 0;
								vector = vector4;
								obj4 = 0;
								num = 0;
								if (!flag13)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+48]");
									obj9._003C_003E4__this = (CreditsSection)0;
									obj9.config = config;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+78]");
									obj9.maxLinesPerChunk = 0;
									obj9.onFirstChunkInitialized = onFirstChunkInitialized;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+20]");
									Coroutine coroutine = ((MonoBehaviour)0).StartCoroutine(obj9);
									_ = 1;
									return true;
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						num6 = num5;
						throw new NullReferenceException();
					}
					flag8 = (byte)num != 0;
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			_ = 4294967295L;
			List<CreditsSectionConfig>.Enumerator enumerator2 = (List<CreditsSectionConfig>.Enumerator)(axis + 48);
			((List<CreditsSectionConfig>.Enumerator*)enumerator2)->Dispose();
			_ = 0;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+28]");
			num2 = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_8_v2 (UnityEngine.RectTransform+Axis)+20]");
			bool flag14 = (nint)0 == 0;
			vector = (Vector2)0;
			axis2 = RectTransform.Axis.Horizontal;
			if (!flag14)
			{
				float num10 = num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+7C]");
				num2 = num10 + 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+20]");
				bool flag15 = (nint)0 == 0;
				vector = (Vector2)0;
				axis2 = RectTransform.Axis.Horizontal;
				if (!flag15)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+20]");
					((RectTransform)0).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num2);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+38]");
					bool flag16 = (nint)0 == 0;
					nint num6 = num5;
					vector = (Vector2)0;
					object obj4 = 0;
					bool flag8 = true;
					if (!flag16)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+38]");
						((GameObject)0).SetActive(value: true);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+48]");
						bool flag17 = (nint)0 == 0;
						num6 = unchecked((nint)null);
						vector = (Vector2)0;
						obj4 = 0;
						flag8 = true;
						if (!flag17)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Object)+48]");
							((GameObject)0).SetActive(value: true);
							_ = 1;
							return false;
						}
						throw new NullReferenceException();
					}
					num5 = num6;
					axis2 = (flag8 ? RectTransform.Axis.Vertical : RectTransform.Axis.Horizontal);
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private unsafe void _003C_003Em__Finally1()
		{
			//IL_0014: Expected I4, but got I8
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected O, but got Unknown
			_003C_003E1__state = -1;
			List<CreditsSectionConfig>.Enumerator enumerator = (List<CreditsSectionConfig>.Enumerator)(this + 48);
			((List<CreditsSectionConfig>.Enumerator*)enumerator)->Dispose();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private sealed class _003CRevealCreditsCoroutine_003Ed__32 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CreditsPanel _003C_003E4__this;

		private float _003Ctimer_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CRevealCreditsCoroutine_003Ed__32(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_003b: Expected I4, but got I8
			//IL_00ec: Expected I4, but got I8
			//IL_0238: Expected I4, but got O
			//IL_0097: Invalid comparison between F4 and I4
			//IL_014c: Invalid comparison between I4 and F4
			//IL_0197: Expected F4, but got I4
			CreditsPanel creditsPanel = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null || (object)creditsPanel._canvasGroup == null)
				{
					goto IL_022a;
				}
				float alpha = creditsPanel._canvasGroup.alpha;
				if (alpha > 0f)
				{
					goto IL_0224;
				}
				_003Ctimer_003E5__2 = 0f;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_0224;
				}
				_003C_003E1__state = -1;
			}
			if (0.5f > _003Ctimer_003E5__2)
			{
				float unscaledDeltaTime = Time.unscaledDeltaTime;
				float num = (_003Ctimer_003E5__2 = unscaledDeltaTime + _003Ctimer_003E5__2);
				if ((object)_003C_003E4__this != null)
				{
					float num2 = num + num;
					if (!(0f > num2))
					{
						if (num2 > 1f)
						{
							num2 = 1f;
						}
					}
					else
					{
						num2 = 0f;
					}
					if ((object)creditsPanel._canvasGroup != null)
					{
						creditsPanel._canvasGroup.alpha = num2;
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
				}
			}
			else if ((object)_003C_003E4__this != null && (object)creditsPanel._canvasGroup != null)
			{
				creditsPanel._canvasGroup.alpha = 1f;
				goto IL_0224;
			}
			goto IL_022a;
			IL_0224:
			return false;
			IL_022a:
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

	private RectTransform _contentParent;

	private ScrollRect _scrollRect;

	private CanvasGroup _canvasGroup;

	private GameObject _scrollbarObject;

	private GameObject _mainMenu;

	private GameObject _exitHandler;

	private float _autoScrollSpeed = 100f;

	private float _delayBeforeAutoScroll = 1f;

	private float _manualScrollSpeed = 1000f;

	private InputActionReference _manualScrollAction;

	private InputActionReference _sectionSkipAction;

	private CreditsSection _sectionPrefab;

	private int _maxLinesPerSectionChunk = 250;

	private float _spaceBetweenSections = 100f;

	private List<CreditsSectionConfig> _sectionConfigs;

	private UnityEvent _onCreditsDisplayed;

	private UnityEvent _onCreditsHidden;

	private readonly List<CreditsSection> _sections;

	private float _lastInputTime;

	private bool _hasStartedLoadingCredits;

	private bool _hasLoadedCredits;

	private bool _isDisplayedFromMainMenu;

	private const float VISIBILITY_CHECK_INTERVAL = 0.16f;

	private const float VISIBILITY_MARGIN = 250f;

	private void Awake()
	{
		_canvasGroup.alpha = 0f;
		_scrollbarObject.SetActive(value: false);
	}

	private void OnEnable()
	{
		Vector2 anchoredPosition = default(Vector2);
		_contentParent.anchoredPosition = anchoredPosition;
		float time = Time.time;
		_lastInputTime = time;
		_003CLoadCredits_003Ed__29 obj = new _003CLoadCredits_003Ed__29(0);
		obj._003C_003E4__this = this;
		obj._003C_003E1__state = 0;
		Coroutine coroutine = StartCoroutine(obj);
		_003CCheckVisibility_003Ed__31 obj2 = new _003CCheckVisibility_003Ed__31(0);
		obj2._003C_003E1__state = 0;
		obj2._003C_003E4__this = this;
		Coroutine coroutine2 = StartCoroutine(obj2);
		InputAction action = _sectionSkipAction.action;
		Action<InputAction.CallbackContext> value = SectionSkipAction_performed;
		action.performed += value;
	}

	private void OnDisable()
	{
		_003CCheckVisibility_003Ed__31 obj = new _003CCheckVisibility_003Ed__31(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		StopCoroutine(obj);
		if (_isDisplayedFromMainMenu)
		{
			_mainMenu.SetActive(value: true);
		}
		if (_onCreditsHidden != null)
		{
			_onCreditsHidden.Invoke();
		}
		InputAction action = _sectionSkipAction.action;
		Action<InputAction.CallbackContext> value = SectionSkipAction_performed;
		action.performed -= value;
	}

	private void Update()
	{
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_00a0: Invalid comparison between O and F4
		float alpha = _canvasGroup.alpha;
		if (1f > alpha)
		{
			return;
		}
		Vector2 anchoredPosition = _contentParent.anchoredPosition;
		InputAction action = _manualScrollAction.action;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807015E0");
		if (_hasLoadedCredits)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj2 = default(object);
			object obj = obj2 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.5f))
			{
				float unscaledDeltaTime = Time.unscaledDeltaTime;
				float time = Time.time;
				_lastInputTime = time;
				goto IL_0137;
			}
		}
		float time2 = Time.time;
		float num = time2 - _lastInputTime;
		if (!(num < _delayBeforeAutoScroll))
		{
			float unscaledDeltaTime2 = Time.unscaledDeltaTime;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm0\"");
		}
		goto IL_0137;
		IL_0137:
		Vector2 anchoredPosition2 = default(Vector2);
		_contentParent.anchoredPosition = anchoredPosition2;
	}

	public void Show(bool isFromMainMenu)
	{
		_isDisplayedFromMainMenu = isFromMainMenu;
		GameObject gameObject = base.gameObject;
		gameObject.SetActive(value: true);
		if (_onCreditsDisplayed != null)
		{
			_onCreditsDisplayed.Invoke();
		}
	}

	private IEnumerator LoadCredits()
	{
		_003CLoadCredits_003Ed__29 obj = new _003CLoadCredits_003Ed__29(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void RevealCredits()
	{
		_003CRevealCreditsCoroutine_003Ed__32 obj = new _003CRevealCreditsCoroutine_003Ed__32(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private IEnumerator CheckVisibility()
	{
		_003CCheckVisibility_003Ed__31 obj = new _003CCheckVisibility_003Ed__31(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator RevealCreditsCoroutine()
	{
		_003CRevealCreditsCoroutine_003Ed__32 obj = new _003CRevealCreditsCoroutine_003Ed__32(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private unsafe void SectionSkipAction_performed(InputAction.CallbackContext ctx)
	{
		//IL_00c8: Expected O, but got I
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_02ab: Expected O, but got I8
		//IL_02b3: Expected O, but got Ref
		//IL_0134: Expected O, but got I4
		//IL_0154: Expected O, but got I
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Expected O, but got Unknown
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected O, but got Unknown
		//IL_02f5: Expected O, but got I4
		//IL_0291: Expected O, but got I4
		//IL_0299: Expected O, but got Ref
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Expected O, but got Unknown
		//IL_01c5: Expected O, but got Ref
		//IL_01ce: Expected O, but got I4
		//IL_01d7: Expected O, but got I4
		//IL_041b: Expected O, but got I8
		//IL_0376: Expected O, but got I
		//IL_01fc: Expected O, but got I
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Expected O, but got Unknown
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Expected O, but got Unknown
		//IL_0279: Expected O, but got Ref
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Expected O, but got Unknown
		//IL_0256: Expected O, but got Ref
		if (!_hasLoadedCredits)
		{
			return;
		}
		List<CreditsSection> sections = _sections;
		if (sections._size <= 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807546E0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		if (obj != null)
		{
			return;
		}
		object obj2 = default(object);
		if ((nint)obj2 >= 0)
		{
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si ebp,xmm0\"");
		Vector2 anchoredPosition = _contentParent.anchoredPosition;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ stack_8_v3+38]");
		Vector2 anchoredPosition2 = ((RectTransform)0).anchoredPosition;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj4 = default(object);
		object obj3 = obj4 & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj6 = default(object);
		object obj5 = obj6 & 0;
		object obj13;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
		{
			List<CreditsSection> sections2 = _sections;
			object obj7 = sections2._size - 1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ stack_8_v3+38]");
			Vector2 anchoredPosition3 = ((RectTransform)0).anchoredPosition;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			obj3 = obj4 & 0;
			List<CreditsSection> sections3 = _sections;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj8 = obj6 & 0;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj9 = obj4 & 0;
				object obj10 = (object)(&obj2);
				object obj11 = 0;
				object obj12 = 0;
				while (true)
				{
					bool flag = (nint)obj11 >= sections3._size;
					obj13 = 2147483648L;
					if (flag)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ stack_8_v3+38]");
					Vector2 anchoredPosition4 = ((RectTransform)0).anchoredPosition;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					object obj14 = obj4 & 0;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj14) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9))
					{
						sections3 = _sections;
						obj12++;
						obj10 = (object)(&obj2);
						obj11 = obj12;
						continue;
					}
					obj13 = obj12 - 1;
					obj10 = (object)(&obj2);
					break;
				}
			}
			else
			{
				obj13 = sections3._size - 1;
				object obj10 = (object)(&obj2);
			}
		}
		else
		{
			obj13 = 4294967295L;
			object obj10 = (object)(&obj2);
		}
		List<CreditsSection> sections4 = _sections;
		object obj16 = default(object);
		object obj15 = obj13 + obj16;
		if ((nint)obj15 >= -1)
		{
			object obj17 = sections4._size - 1;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj15) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj17))
			{
				bool flag2 = (nint)obj17 < -1;
				obj15 = obj17;
				if (flag2)
				{
					return;
				}
			}
			if ((nint)obj15 != -1)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ stack_8_v3+38]");
				Vector2 anchoredPosition5 = ((RectTransform)0).anchoredPosition;
				goto IL_038e;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371660");
		goto IL_038e;
		IL_038e:
		Vector2 anchoredPosition6 = default(Vector2);
		_contentParent.anchoredPosition = anchoredPosition6;
		float time = Time.time;
		_lastInputTime = time;
	}

	public CreditsPanel()
	{
		List<CreditsSection> sections = new List<CreditsSection>();
		_sections = sections;
		base._002Ector();
	}
}
