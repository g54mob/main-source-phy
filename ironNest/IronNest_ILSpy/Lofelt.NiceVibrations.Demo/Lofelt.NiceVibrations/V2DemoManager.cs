using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Lofelt.NiceVibrations;

public class V2DemoManager : MonoBehaviour
{
	private sealed class _003CTransitionCoroutine_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public V2DemoManager _003C_003E4__this;

		public int previous;

		public int next;

		public bool goingRight;

		private float _003CtimeSpent_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CTransitionCoroutine_003Ed__16(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0025: Expected I4, but got I8
			//IL_0232: Expected I4, but got I8
			//IL_047a: Invalid comparison between I4 and F4
			//IL_04c5: Expected F4, but got I4
			//IL_02f2: Invalid comparison between I4 and F4
			//IL_0800: Expected O, but got F4
			//IL_033d: Expected F4, but got I4
			//IL_076c: Expected O, but got F4
			//IL_0500: Expected O, but got Ref
			//IL_0378: Expected O, but got Ref
			//IL_0152: Expected O, but got I4
			//IL_0558: Invalid comparison between I4 and F4
			//IL_05a3: Expected F4, but got I4
			//IL_03d0: Invalid comparison between I4 and F4
			//IL_017c: Expected O, but got Ref
			//IL_0433: Expected F4, but got I4
			//IL_07c1: Expected O, but got F4
			//IL_05dd: Expected O, but got Ref
			V2DemoManager v2DemoManager = _003C_003E4__this;
			Vector3 vector = default(Vector3);
			Component component = default(Component);
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null && v2DemoManager.Pages != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Transform transform = default(Transform);
					if ((object)transform != null)
					{
						_ = transform.localPosition.y;
						if (v2DemoManager.Pages != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							if ((object)transform != null)
							{
								_ = transform.localPosition.z;
								if (v2DemoManager.Pages != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
									List<RectTransform>.Enumerator enumerator = default(List<RectTransform>.Enumerator);
									while (enumerator.MoveNext())
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
										v2DemoManager._position = (Vector3)1150681088;
										if ((object)transform != null)
										{
											transform.localPosition = (Vector3)(&vector);
											continue;
										}
										throw new NullReferenceException();
									}
									enumerator.Dispose();
									if (v2DemoManager.Pages != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
										if ((object)component != null)
										{
											GameObject gameObject = component.gameObject;
											if ((object)gameObject != null)
											{
												gameObject.SetActive(value: true);
												_003CtimeSpent_003E5__2 = 0f;
												goto IL_0252;
											}
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
				if (_003C_003E1__state != 1)
				{
					goto IL_06ac;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_0252;
				}
			}
			goto IL_06ba;
			IL_0252:
			float num5;
			if (v2DemoManager.PageTransitionDuration > _003CtimeSpent_003E5__2)
			{
				float time = _003CtimeSpent_003E5__2 / v2DemoManager.PageTransitionDuration;
				if (!goingRight)
				{
					if (v2DemoManager.TransitionCurve != null)
					{
						float num = v2DemoManager.TransitionCurve.Evaluate(time);
						if (!(0f > num))
						{
							if (num > 1f)
							{
								num = 1f;
							}
						}
						else
						{
							num = 0f;
						}
						float num2 = num * 1200f;
						v2DemoManager._position = (Vector3)num2;
						if (v2DemoManager.Pages != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							Transform transform2 = default(Transform);
							if ((object)transform2 != null)
							{
								transform2.localPosition = (Vector3)(&vector);
								float time2 = _003CtimeSpent_003E5__2 / v2DemoManager.PageTransitionDuration;
								if (v2DemoManager.TransitionCurve != null)
								{
									float num3 = v2DemoManager.TransitionCurve.Evaluate(time2);
									if (!(0f > num3))
									{
										if (num3 > 1f)
										{
											float num4 = 1f * 1200f;
											num5 = num4 - 1200f;
											goto IL_07b4;
										}
									}
									else
									{
										num3 = 0f;
									}
									float num6 = num3 * 1200f;
									num5 = num6 - 1200f;
									goto IL_07b4;
								}
							}
						}
					}
				}
				else if (v2DemoManager.TransitionCurve != null)
				{
					float num7 = v2DemoManager.TransitionCurve.Evaluate(time);
					if (!(0f > num7))
					{
						if (num7 > 1f)
						{
							num7 = 1f;
						}
					}
					else
					{
						num7 = 0f;
					}
					float num8 = num7 * -1200f;
					v2DemoManager._position = (Vector3)num8;
					if (v2DemoManager.Pages != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						Transform transform3 = default(Transform);
						if ((object)transform3 != null)
						{
							transform3.localPosition = (Vector3)(&vector);
							float time3 = _003CtimeSpent_003E5__2 / v2DemoManager.PageTransitionDuration;
							if (v2DemoManager.TransitionCurve != null)
							{
								float num9 = v2DemoManager.TransitionCurve.Evaluate(time3);
								if (!(0f > num9))
								{
									if (num9 > 1f)
									{
										num9 = 1f;
									}
								}
								else
								{
									num9 = 0f;
								}
								float num10 = num9 * -1200f;
								num5 = num10 + 1200f;
								goto IL_07b4;
							}
						}
					}
				}
			}
			else if (v2DemoManager.Pages != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if ((object)component != null)
				{
					GameObject gameObject2 = component.gameObject;
					if ((object)gameObject2 != null)
					{
						gameObject2.SetActive(value: false);
						goto IL_06ac;
					}
				}
			}
			goto IL_06ba;
			IL_06ba:
			throw new NullReferenceException();
			IL_07b4:
			v2DemoManager._position = (Vector3)num5;
			if (v2DemoManager.Pages != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Transform transform4 = default(Transform);
				if ((object)transform4 != null)
				{
					transform4.localPosition = (Vector3)(&vector);
					float deltaTime = Time.deltaTime;
					float num11 = deltaTime + _003CtimeSpent_003E5__2;
					_003CtimeSpent_003E5__2 = num11;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
			}
			goto IL_06ba;
			IL_06ac:
			return false;
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

	public List<RectTransform> Pages;

	public int CurrentPage;

	public float PageTransitionDuration = 1f;

	public AnimationCurve TransitionCurve;

	public Color ActiveColor;

	public Color InactiveColor;

	public bool SoundActive = true;

	protected Vector3 _position;

	protected List<Pagination> _paginations;

	protected Coroutine _transitionCoroutine;

	protected virtual void Start()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.V2DemoManager>)+188]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Lofelt.NiceVibrations.V2DemoManager>)+190]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected unsafe virtual void Initialization()
	{
		//IL_0087: Expected O, but got Ref
		//IL_015b: Expected O, but got Ref
		//IL_0180: Expected O, but got Ref
		//IL_0196: Expected I, but got O
		//IL_01df: Expected I, but got O
		Application.targetFrameRate = 60;
		List<Pagination> paginations = new List<Pagination>();
		_paginations = paginations;
		Component pages = (Component)(object)Pages;
		if (Pages != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<RectTransform>.Enumerator enumerator = default(List<RectTransform>.Enumerator);
			Component component = default(Component);
			Pagination item = default(Pagination);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = (object)component == null;
				pages = (Component)(&enumerator);
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
					if (_paginations != null)
					{
						_paginations.Add(item);
						GameObject gameObject = component.gameObject;
						if ((object)gameObject != null)
						{
							gameObject.SetActive(value: false);
							continue;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			pages = (Component)(object)_paginations;
			if (_paginations != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<Pagination>.Enumerator enumerator2 = default(List<Pagination>.Enumerator);
				while (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					List<RectTransform> pages2 = Pages;
					bool flag2 = Pages == null;
					pages = (Component)(&enumerator2);
					if (!flag2)
					{
						bool flag3 = (object)component == null;
						pages = (Component)(&enumerator2);
						if (!flag3)
						{
							nint num = (nint)component;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v624 @ r8_v15 (Il2CppClass<UnityEngine.Component>)+178] (should have been resolved before IL gen)");
							_ = ActiveColor;
							_ = InactiveColor;
							List<RectTransform> pages3 = Pages;
							if (Pages != null)
							{
								nint num2 = (nint)component;
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v626 @ r9_v10 (Il2CppClass<UnityEngine.Component>)+188] (should have been resolved before IL gen)");
								continue;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				enumerator2.Dispose();
				if (Pages != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Component component2 = default(Component);
					if ((object)component2 != null)
					{
						GameObject gameObject2 = component2.gameObject;
						if ((object)gameObject2 != null)
						{
							gameObject2.SetActive(value: true);
							if (!SoundActive)
							{
								AudioListener.volume = 0f;
								SoundActive = false;
							}
							else
							{
								AudioListener.volume = 1f;
								SoundActive = true;
							}
							return;
						}
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	public virtual void PreviousPage()
	{
		//IL_0066: Expected I, but got O
		//IL_0076: Expected O, but got I
		//IL_0086: Expected O, but got I
		if (CurrentPage > 0)
		{
			int currentPage = CurrentPage - 1;
			CurrentPage = currentPage;
			int next = CurrentPage - 1;
			Transition(CurrentPage, next, goingRight: false);
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rdx_v2 (Il2CppClass<Lofelt.NiceVibrations.V2DemoManager>)+1B8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rdx_v2 (Il2CppClass<Lofelt.NiceVibrations.V2DemoManager>)+1C0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v32 @ rax_v5 (should have been resolved before IL gen)");
		}
	}

	public virtual void NextPage()
	{
		//IL_0018: Expected O, but got I4
		//IL_0079: Expected I, but got O
		//IL_0089: Expected O, but got I
		//IL_0099: Expected O, but got I
		List<RectTransform> pages = Pages;
		object obj = pages._size - 1;
		if (CurrentPage < (nint)obj)
		{
			int num = ++CurrentPage;
			int previous = num - 1;
			Transition(previous, num, goingRight: true);
			nint num2 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rdx_v2 (Il2CppClass<Lofelt.NiceVibrations.V2DemoManager>)+1B8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rdx_v2 (Il2CppClass<Lofelt.NiceVibrations.V2DemoManager>)+1C0]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v64 @ rax_v9 (should have been resolved before IL gen)");
		}
	}

	protected unsafe virtual void SetCurrentPage()
	{
		//IL_0042: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<Pagination>.Enumerator enumerator = default(List<Pagination>.Enumerator);
		object obj3 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				List<RectTransform> pages = Pages;
				bool flag = Pages == null;
				object obj = (object)(&enumerator);
				if (flag)
				{
					break;
				}
				object obj2 = obj3;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v205 @ r9_v3+188] (should have been resolved before IL gen)");
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	protected virtual void Transition(int previous, int next, bool goingRight)
	{
		HapticController.Reset();
		if (_transitionCoroutine != null)
		{
			StopCoroutine(_transitionCoroutine);
		}
		IEnumerator routine = TransitionCoroutine(previous, next, goingRight);
		Coroutine transitionCoroutine = StartCoroutine(routine);
		_transitionCoroutine = transitionCoroutine;
	}

	protected virtual IEnumerator TransitionCoroutine(int previous, int next, bool goingRight)
	{
		_003CTransitionCoroutine_003Ed__16 obj = new _003CTransitionCoroutine_003Ed__16(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.next = next;
		obj.goingRight = goingRight;
		obj.previous = previous;
		return obj;
	}

	public virtual void TurnHapticsOn()
	{
		HapticPatterns.PlayPreset(HapticPatterns.PresetType.Success);
	}

	public virtual void TurnHapticsOff()
	{
		HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
	}

	public virtual void TurnSoundsOn()
	{
		AudioListener.volume = 1f;
		SoundActive = true;
		HapticPatterns.PlayPreset(HapticPatterns.PresetType.Success);
	}

	public virtual void TurnSoundsOff()
	{
		AudioListener.volume = 0f;
		SoundActive = false;
		HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
	}
}
