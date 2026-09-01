using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using Heathen.SteamworksIntegration.API;
using SteamTools;
using UnityEngine;

public class InitializationHandler : MonoBehaviour
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<bool> _003C_003E9__15_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003CStart_003Eb__15_0()
		{
			if (Interface._003CIsReady_003Ek__BackingField)
			{
				return true;
			}
			return App._003CHasInitialisationError_003Ek__BackingField;
		}
	}

	private sealed class _003CFadeOutCanvasGroup_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CanvasGroup canvasGroup;

		public float duration;

		private float _003CstartAlpha_003E5__2;

		private float _003Ctimer_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CFadeOutCanvasGroup_003Ed__16(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0031: Expected I4, but got I8
			//IL_00aa: Expected I4, but got I8
			//IL_01e5: Expected I4, but got O
			//IL_00ef: Invalid comparison between I4 and F4
			//IL_013a: Expected F4, but got I4
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)canvasGroup == null)
				{
					goto IL_01d7;
				}
				float alpha = canvasGroup.alpha;
				_003CstartAlpha_003E5__2 = alpha;
				_003Ctimer_003E5__3 = 0f;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_01d1;
				}
				_003C_003E1__state = -1;
				float unscaledDeltaTime = Time.unscaledDeltaTime;
				float num = unscaledDeltaTime + _003Ctimer_003E5__3;
				_003Ctimer_003E5__3 = num;
			}
			if (duration > _003Ctimer_003E5__3)
			{
				float num2 = _003Ctimer_003E5__3 / duration;
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
				if ((object)canvasGroup != null)
				{
					float num3 = 0f - _003CstartAlpha_003E5__2;
					float num4 = num3 * num2;
					float alpha2 = num4 + _003CstartAlpha_003E5__2;
					canvasGroup.alpha = alpha2;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
			}
			else if ((object)canvasGroup != null)
			{
				canvasGroup.alpha = 0f;
				goto IL_01d1;
			}
			goto IL_01d7;
			IL_01d1:
			return false;
			IL_01d7:
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

	private sealed class _003CHandleSplashDuration_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public InitializationHandler _003C_003E4__this;

		private float _003Ctimer_003E5__2;

		private float _003CcurrentScale_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CHandleSplashDuration_003Ed__17(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_03c0: Expected I4, but got I8
			//IL_0419: Expected I4, but got O
			//IL_0039: Expected O, but got I4
			//IL_0428: Expected I4, but got I8
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Expected O, but got Unknown
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Expected O, but got Unknown
			//IL_00e1: Expected I4, but got I8
			//IL_017c: Invalid comparison between I4 and F4
			//IL_01c7: Expected F4, but got I4
			//IL_00ae: Expected I4, but got I8
			//IL_0272: Unknown result type (might be due to invalid IL or missing references)
			//IL_0277: Expected O, but got Unknown
			//IL_027f: Invalid comparison between F4 and O
			//IL_033e: Invalid comparison between I4 and F4
			//IL_02bd: Invalid comparison between F4 and I4
			//IL_0389: Expected F4, but got I4
			InitializationHandler initializationHandler = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						object obj3 = obj2 - 1;
						if (!flag)
						{
							if ((nint)obj3 != 1)
							{
								goto IL_03a3;
							}
							_003C_003E1__state = -1;
							if ((object)_003C_003E4__this != null)
							{
								goto IL_0447;
							}
						}
						else
						{
							_003C_003E1__state = -1;
							if ((object)_003C_003E4__this != null)
							{
								goto IL_046d;
							}
						}
						goto IL_040b;
					}
				}
				else
				{
					_003Ctimer_003E5__2 = 0f;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if (initializationHandler._minSplashDisplayDuration > _003Ctimer_003E5__2)
					{
						float unscaledDeltaTime = Time.unscaledDeltaTime;
						float num = (_003Ctimer_003E5__2 = unscaledDeltaTime + _003Ctimer_003E5__2) / initializationHandler._minSplashDisplayDuration;
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
						float loadingBarXScale = initializationHandler._loadingBarMinDurationTarget * num;
						_003C_003E4__this.SetLoadingBarXScale(loadingBarXScale);
						_003C_003E2__current = null;
						_003C_003E1__state = 2;
						goto IL_0521;
					}
					initializationHandler._hasSplashMinDurationPassed = true;
					if ((object)initializationHandler._loadingBarRect != null)
					{
						_003CcurrentScale_003E5__3 = initializationHandler._loadingBarRect.localScale.x;
						goto IL_046d;
					}
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					_003C_003E4__this.SetLoadingBarXScale(0f);
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					goto IL_0521;
				}
			}
			goto IL_040b;
			IL_040b:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0521:
			return true;
			IL_046d:
			if (!initializationHandler._hasEverythingLoaded)
			{
				float num2 = initializationHandler._loadingBarWaitTarget;
				float unscaledDeltaTime2 = Time.unscaledDeltaTime;
				float num3 = initializationHandler._loadingBarWaitTarget - _003CcurrentScale_003E5__3;
				float num4 = unscaledDeltaTime2 * initializationHandler._loadingBarWaitMoveSpeed;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj4 = num3 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num4) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4))
				{
					float num5 = initializationHandler._loadingBarWaitTarget - _003CcurrentScale_003E5__3;
					float num6 = ((num5 < 0f) ? (-1f) : 1f);
					float num7 = num6 * num4;
					num2 = num7 + _003CcurrentScale_003E5__3;
				}
				_003CcurrentScale_003E5__3 = num2;
				_003C_003E4__this.SetLoadingBarXScale(num2);
				_003C_003E2__current = null;
				_003C_003E1__state = 3;
				goto IL_0521;
			}
			_003Ctimer_003E5__2 = 0f;
			goto IL_0447;
			IL_03a3:
			return false;
			IL_0447:
			if (initializationHandler._textFadeDuration > _003Ctimer_003E5__2)
			{
				float unscaledDeltaTime3 = Time.unscaledDeltaTime;
				float num8 = (_003Ctimer_003E5__2 = unscaledDeltaTime3 + _003Ctimer_003E5__2) / initializationHandler._textFadeDuration;
				if (!(0f > num8))
				{
					if (num8 > 1f)
					{
						num8 = 1f;
					}
				}
				else
				{
					num8 = 0f;
				}
				float num9 = 1f - _003CcurrentScale_003E5__3;
				float num10 = num9 * num8;
				float loadingBarXScale2 = num10 + _003CcurrentScale_003E5__3;
				_003C_003E4__this.SetLoadingBarXScale(loadingBarXScale2);
				_003C_003E2__current = null;
				_003C_003E1__state = 4;
				goto IL_0521;
			}
			_003C_003E4__this.SetLoadingBarXScale(1f);
			goto IL_03a3;
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

	private sealed class _003CStart_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public InitializationHandler _003C_003E4__this;

		private List<GameObject>.Enumerator _003C_003E7__wrap1;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CStart_003Ed__15(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		unsafe void IDisposable.Dispose()
		{
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			if (_003C_003E1__state == -3 || _003C_003E1__state == 2)
			{
				_ = 4294967295L;
				object obj = default(object);
				List<GameObject>.Enumerator enumerator = (List<GameObject>.Enumerator)(obj + 40);
				((List<GameObject>.Enumerator*)enumerator)->Dispose();
			}
		}

		private bool MoveNext()
		{
			//IL_004b: Expected O, but got I
			//IL_0012: Expected O, but got I8
			//IL_002c: Expected O, but got I8
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ stack_8_v2+10]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ stack_8_v2+10]");
			if ((nint)0 <= (nint)7)
			{
				object obj2 = 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ r8_v1+58199C+v40 @ rax_v3*4]");
				object obj3 = 0 + 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v59 @ rcx_v3 (should have been resolved before IL gen)");
			}
			return false;
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
			List<GameObject>.Enumerator enumerator = (List<GameObject>.Enumerator)(this + 40);
			((List<GameObject>.Enumerator*)enumerator)->Dispose();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private List<GameObject> _handledObjects;

	private GameObject _introObject;

	private bool _shouldWaitForSteam = true;

	private bool _shouldWaitForFmodBanks;

	private float _minSplashDisplayDuration = 3f;

	private CanvasGroup _splashCanvasGroup;

	private float _splashFadeDuration = 0.1f;

	private CanvasGroup _textCanvasGroup;

	private float _textFadeDuration = 1f;

	private RectTransform _loadingBarRect;

	private float _loadingBarMinDurationTarget = 0.75f;

	private float _loadingBarWaitTarget = 0.9f;

	private float _loadingBarWaitMoveSpeed = 0.01f;

	private bool _hasSplashMinDurationPassed;

	private bool _hasEverythingLoaded;

	private IEnumerator Start()
	{
		_003CStart_003Ed__15 obj = new _003CStart_003Ed__15(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator FadeOutCanvasGroup(CanvasGroup canvasGroup, float duration)
	{
		_003CFadeOutCanvasGroup_003Ed__16 obj = new _003CFadeOutCanvasGroup_003Ed__16(0);
		obj._003C_003E1__state = 0;
		obj.canvasGroup = canvasGroup;
		obj.duration = duration;
		return obj;
	}

	private IEnumerator HandleSplashDuration()
	{
		_003CHandleSplashDuration_003Ed__17 obj = new _003CHandleSplashDuration_003Ed__17(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private unsafe void SetLoadingBarXScale(float xScale)
	{
		//IL_0023: Expected O, but got Ref
		Vector3 localScale = _loadingBarRect.localScale;
		float num = default(float);
		_loadingBarRect.localScale = (Vector3)(&num);
	}

	private bool _003CStart_003Eb__15_1()
	{
		return _hasSplashMinDurationPassed;
	}
}
