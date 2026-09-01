using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class ScreenOrchestrator : MonoBehaviour
{
	public delegate void OnCompleteDelegate(Resolution? resolution, bool? fullScreen, FullScreenMode? fullScreenMode);

	private sealed class _003CapplyStaggered_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ScreenOrchestrator _003C_003E4__this;

		private bool? _003CtRequestedFullScreen_003E5__2;

		private FullScreenMode? _003CtRequestedFullScreenMode_003E5__3;

		private Resolution? _003CtRequestedResolution_003E5__4;

		private RefreshRate? _003CtRequestedRefreshRate_003E5__5;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CapplyStaggered_003Ed__16(int _003C_003E1__state)
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
			//IL_013e: Expected I4, but got I8
			//IL_06df: Expected I4, but got O
			//IL_001d: Expected O, but got I4
			//IL_01c1: Expected O, but got I4
			//IL_01cf: Expected O, but got I4
			//IL_01dd: Expected O, but got I4
			//IL_01f1: Expected O, but got I4
			//IL_0121: Expected I4, but got I8
			//IL_012a: Expected O, but got I4
			//IL_0202: Unknown result type (might be due to invalid IL or missing references)
			//IL_0207: Expected O, but got Unknown
			//IL_0232: Expected O, but got I4
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			//IL_066c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0671: Expected O, but got Unknown
			//IL_067f: Expected O, but got Ref
			//IL_024b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0250: Expected O, but got Unknown
			//IL_0104: Expected I4, but got I8
			//IL_010d: Expected O, but got I4
			//IL_061b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0620: Expected O, but got Unknown
			//IL_062e: Expected O, but got Ref
			//IL_028b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0290: Expected O, but got Unknown
			//IL_0472: Unknown result type (might be due to invalid IL or missing references)
			//IL_0477: Expected O, but got Unknown
			//IL_0485: Expected O, but got Ref
			//IL_0076: Expected I4, but got I8
			//IL_051d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0522: Expected O, but got Unknown
			//IL_0530: Expected O, but got Ref
			//IL_0548: Expected O, but got Ref
			//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d0: Expected O, but got Unknown
			//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e9: Expected O, but got Unknown
			//IL_04f7: Expected O, but got Ref
			//IL_0511: Expected O, but got I
			//IL_00d2: Expected O, but got Ref
			//IL_0367: Expected O, but got Ref
			//IL_038d: Expected O, but got Ref
			//IL_07db: Expected O, but got Ref
			//IL_07f3: Expected O, but got Ref
			//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ba: Expected O, but got Unknown
			//IL_05c8: Expected O, but got Ref
			//IL_03b3: Expected O, but got Ref
			//IL_0726: Unknown result type (might be due to invalid IL or missing references)
			//IL_072b: Expected O, but got Unknown
			//IL_03da: Expected O, but got Ref
			//IL_0764: Expected O, but got Ref
			//IL_077c: Expected O, but got Ref
			//IL_078c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0791: Expected O, but got Unknown
			//IL_079f: Expected O, but got Ref
			//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0404: Expected O, but got Unknown
			//IL_0412: Expected O, but got Ref
			//IL_0457: Expected O, but got I
			object obj2 = default(object);
			object obj = (object)(&obj2);
			ScreenOrchestrator screenOrchestrator = _003C_003E4__this;
			_ = 0;
			_ = 0;
			_ = 0;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj3 = _003C_003E1__state - 1;
				Resolution? resolution;
				if (!flag)
				{
					object obj4 = obj3 - 1;
					if (!flag)
					{
						if ((nint)obj4 == 1)
						{
							_003C_003E1__state = -1;
							if ((object)_003C_003E4__this == null)
							{
								goto IL_06d1;
							}
							OnCompleteDelegate onComplete = screenOrchestrator.OnComplete;
							if (screenOrchestrator.OnComplete != null)
							{
								object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 39));
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ScreenOrchestrator+<applyStaggered>d__16)+44]");
								_ = 0;
								_ = _003CtRequestedResolution_003E5__4;
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v184.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
							}
						}
						return false;
					}
					_003C_003E1__state = -1;
					resolution = (Resolution?)(object)0;
					goto IL_0285;
				}
				_003C_003E1__state = -1;
				resolution = (Resolution?)(object)0;
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_06d1;
				}
				_003CtRequestedFullScreen_003E5__2 = screenOrchestrator.requestedFullScreen;
				_003CtRequestedFullScreenMode_003E5__3 = screenOrchestrator.requestedFullScreenMode;
				_003CtRequestedResolution_003E5__4 = screenOrchestrator.requestedResolution;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdx_v1 (Kamgam.SettingsGenerator.ScreenOrchestrator)+38]");
				_ = 0;
				_003CtRequestedRefreshRate_003E5__5 = screenOrchestrator.requestedRefreshRate;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdx_v1 (Kamgam.SettingsGenerator.ScreenOrchestrator)+44]");
				_ = 0;
				screenOrchestrator.requestedFullScreen = (bool?)(object)0;
				screenOrchestrator.requestedFullScreenMode = (FullScreenMode?)(object)0;
				screenOrchestrator.requestedResolution = (Resolution?)(object)0;
				_ = 0;
				screenOrchestrator.requestedRefreshRate = (RefreshRate?)(object)0;
				_ = 0;
				object obj6 = this + 40;
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj7 = default(object);
				bool flag2 = obj7 != null;
				Resolution? resolution = (Resolution?)(object)0;
				if (flag2)
				{
					object obj8 = this + 40;
					object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
					bool flag3 = (nint)0 == 0;
					bool fullScreen = !flag3;
					Screen.fullScreen = fullScreen;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
			}
			object obj10 = this + 44;
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj11 = default(object);
			if (obj11 == null)
			{
				goto IL_0285;
			}
			object obj12 = this + 44;
			object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
			Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
			_003C_003E2__current = null;
			_003C_003E1__state = 2;
			return true;
			IL_06d1:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_05fd:
			_003C_003E2__current = null;
			_003C_003E1__state = 3;
			return true;
			IL_0285:
			object obj14 = this + 52;
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj15 = default(object);
			FullScreenMode fullscreenMode;
			RefreshRate preferredRefreshRate;
			int width3;
			int height2;
			if (obj15 == null)
			{
				object obj16 = this + 72;
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj17 = default(object);
				if (obj17 == null)
				{
					goto IL_05fd;
				}
				_ = 0;
				_ = 0;
				FullScreenMode fullScreenMode = Screen.fullScreenMode;
				if (fullScreenMode != FullScreenMode.Windowed)
				{
					int width = Screen.currentResolution.m_Width;
				}
				else
				{
					_ = 0;
					int width2 = Screen.width;
					object obj18 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805E11B0");
					int height = Screen.height;
					object obj19 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180623550");
					Resolution currentResolution = Screen.currentResolution;
					object obj20 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
					_ = currentResolution.m_Width;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
					object obj21 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180974FC0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-29]");
					int width = 0;
				}
				FullScreenMode? fullScreenMode2 = (FullScreenMode?)(object)(this + 44);
				if (((FullScreenMode?*)fullScreenMode2)->HasValue)
				{
					object obj22 = this + 44;
					object obj23 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
					fullscreenMode = FullScreenMode.ExclusiveFullScreen;
				}
				else
				{
					FullScreenMode fullScreenMode3 = Screen.fullScreenMode;
					fullscreenMode = fullScreenMode3;
				}
				object obj24 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
				object obj25 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
				object obj26 = this + 72;
				object obj27 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
				preferredRefreshRate = (RefreshRate)0;
				int num5 = default(int);
				width3 = num5;
				int num6 = default(int);
				height2 = num6;
			}
			else
			{
				object obj28 = this + 52;
				object obj29 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+7]");
				_ = 0;
				nint num7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v425 @ rdx_v9 (Il2CppClass<System.Nullable`1<UnityEngine.RefreshRate>>)+80]");
				RefreshRate refreshRate;
				if (((Resolution?*)null)->Value.m_Width != 0)
				{
					object obj30 = this + 72;
					object obj31 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
					refreshRate = (RefreshRate)0;
				}
				else
				{
					object obj32 = this + 52;
					object obj33 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
					object obj34 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+7]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
					RefreshRate refreshRate2 = default(RefreshRate);
					refreshRate = refreshRate2;
				}
				nint num8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v542 @ rdx_v12 (Il2CppClass<System.Nullable`1<UnityEngine.FullScreenMode>>)+80]");
				if (((Resolution?*)null)->Value.m_Width != 0)
				{
					object obj35 = this + 44;
					object obj36 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
					fullscreenMode = FullScreenMode.ExclusiveFullScreen;
				}
				else
				{
					FullScreenMode fullScreenMode4 = Screen.fullScreenMode;
					fullscreenMode = fullScreenMode4;
				}
				object obj37 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
				object obj38 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
				int num9 = default(int);
				width3 = num9;
				preferredRefreshRate = refreshRate;
				int num10 = default(int);
				height2 = num10;
			}
			Screen.SetResolution(width3, height2, fullscreenMode, preferredRefreshRate);
			goto IL_05fd;
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

	private static ScreenOrchestrator _instance;

	public OnCompleteDelegate OnComplete;

	protected Resolution? requestedResolution;

	protected RefreshRate? requestedRefreshRate;

	protected bool? requestedFullScreen;

	protected FullScreenMode? requestedFullScreenMode;

	protected Coroutine _applyCoroutine;

	public static ScreenOrchestrator Instance
	{
		get
		{
			if (!_instance)
			{
				GameObject gameObject = new GameObject();
				if ((object)gameObject != null)
				{
					ScreenOrchestrator instance = gameObject.AddComponent<ScreenOrchestrator>();
					_instance = instance;
					if ((object)_instance != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
						object obj = default(object);
						if (obj != null)
						{
							object obj2 = obj;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v194 @ rdx_v9+168] (should have been resolved before IL gen)");
							string text = default(string);
							_instance.name = text;
							if ((object)_instance != null)
							{
								GameObject target = _instance.gameObject;
								UnityEngine.Object.DontDestroyOnLoad(target);
								goto IL_010c;
							}
						}
					}
				}
				return (ScreenOrchestrator)(object)new NullReferenceException();
			}
			goto IL_010c;
			IL_010c:
			return _instance;
		}
	}

	public unsafe void RequestResolution(Resolution resolution)
	{
		//IL_0012: Expected O, but got Ref
		//IL_001d: Expected O, but got I4
		object obj = default(object);
		Resolution? resolution2 = (Resolution)(&obj);
		requestedResolution = (Resolution?)(object)0;
		_ = 0;
	}

	public unsafe void RequestRefreshRate(RefreshRate refreshRate)
	{
		//IL_0012: Expected O, but got Ref
		//IL_001d: Expected O, but got I4
		object obj = default(object);
		RefreshRate? refreshRate2 = (RefreshRate)(&obj);
		requestedRefreshRate = (RefreshRate?)(object)0;
		_ = 0;
	}

	public unsafe void RequestFullScreen(bool fullScreen)
	{
		//IL_001d: Expected O, but got I4
		object obj = default(object);
		bool? flag = (byte)(&obj) != 0;
		requestedFullScreen = (bool?)(object)0;
	}

	public unsafe void RequestFullScreenMode(FullScreenMode fullScreenMode)
	{
		//IL_001d: Expected O, but got I4
		object obj = default(object);
		FullScreenMode? fullScreenMode2 = (FullScreenMode)(int)(&obj);
		requestedFullScreenMode = (FullScreenMode?)(object)0;
	}

	public void LateUpdate()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		object obj = this + 40;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		if (obj2 == null)
		{
			object obj3 = this + 72;
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj4 = default(object);
			if (obj4 == null)
			{
				object obj5 = this + 76;
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj6 = default(object);
				if (obj6 == null)
				{
					return;
				}
			}
		}
		if (_applyCoroutine != null)
		{
			StopCoroutine(_applyCoroutine);
		}
		_003CapplyStaggered_003Ed__16 obj7 = new _003CapplyStaggered_003Ed__16(0);
		obj7._003C_003E1__state = 0;
		obj7._003C_003E4__this = this;
		Coroutine applyCoroutine = StartCoroutine(obj7);
		_applyCoroutine = applyCoroutine;
	}

	protected void apply()
	{
		if (_applyCoroutine != null)
		{
			StopCoroutine(_applyCoroutine);
		}
		_003CapplyStaggered_003Ed__16 obj = new _003CapplyStaggered_003Ed__16(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine applyCoroutine = StartCoroutine(obj);
		_applyCoroutine = applyCoroutine;
	}

	protected IEnumerator applyStaggered()
	{
		_003CapplyStaggered_003Ed__16 obj = new _003CapplyStaggered_003Ed__16(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public unsafe static Resolution GetCurrentResolution()
	{
		//IL_003c: Expected native int or pointer, but got O
		//IL_00a0: Expected native int or pointer, but got O
		FullScreenMode fullScreenMode = Screen.fullScreenMode;
		Resolution resolution = default(Resolution);
		if (fullScreenMode != FullScreenMode.Windowed)
		{
			((Resolution*)(nint)resolution)->m_Width = Screen.currentResolution.m_Width;
			return resolution;
		}
		int width = Screen.width;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805E11B0");
		int height = Screen.height;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180623550");
		Resolution currentResolution = Screen.currentResolution;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180974FC0");
		int width2 = default(int);
		((Resolution*)(nint)resolution)->m_Width = width2;
		return resolution;
	}

	public void Destroy()
	{
		_instance = null;
		if (this != null)
		{
			GameObject gameObject = base.gameObject;
			if (gameObject != null)
			{
				GameObject obj = base.gameObject;
				UnityEngine.Object.Destroy(obj);
			}
		}
	}
}
