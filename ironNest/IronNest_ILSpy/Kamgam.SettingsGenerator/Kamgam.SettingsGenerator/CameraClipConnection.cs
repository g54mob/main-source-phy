using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class CameraClipConnection : Connection<float>
{
	public enum ClippingMode
	{
		Near,
		Far
	}

	public const float DefaultFallbackNear = 0.3f;

	public const float DefaultFallbackFar = 1000f;

	public bool UseMain;

	public bool UseMarkers;

	public ClippingMode Mode;

	public float ClipMin;

	public float ClipMax;

	[NonSerialized]
	protected float _clipValue;

	public unsafe CameraClipConnection(ClippingMode mode = ClippingMode.Far, float clipMin = 1f, float clipMax = 1000f, bool useMain = true, bool useMarkers = true)
	{
		//IL_01cf: Expected I, but got O
		//IL_006c: Expected I, but got O
		//IL_0119: Expected I, but got O
		//IL_0278: Expected I, but got I8
		//IL_0102: Expected I, but got I8
		//IL_00c8: Expected I, but got I8
		//IL_02cb: Expected I, but got O
		//IL_02f6: Expected I, but got O
		//IL_030c: Expected I, but got O
		base._002Ector();
		ClipMin = clipMin;
		ClipMax = clipMax;
		Mode = mode;
		_clipValue = ((mode == ClippingMode.Near) ? 0.3f : 1000f);
		bool useMain2 = default(bool);
		UseMain = useMain2;
		bool useMarkers2 = default(bool);
		UseMarkers = useMarkers2;
		CameraDetector instance = CameraDetector.Instance;
		CameraDetector.OnNewCameraFoundDelegate onNewCameraFoundDelegate;
		nint invoke_impl;
		if ((object)instance != null)
		{
			onNewCameraFoundDelegate = null;
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rbx_v4 (Il2CppMethodInfo)+8]");
			((Delegate)onNewCameraFoundDelegate).method_ptr = (IntPtr)0;
			((Delegate)onNewCameraFoundDelegate).method = (nint)__ldftn(CameraClipConnection.onNewCamera);
			((Delegate)onNewCameraFoundDelegate).m_target = this;
			((Delegate)onNewCameraFoundDelegate).method_code = (IntPtr)onNewCameraFoundDelegate;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66E0");
			object obj = default(object);
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rbx_v4 (Il2CppMethodInfo)+52]");
				if ((nint)0 != 1)
				{
					goto IL_0107;
				}
				invoke_impl = unchecked((nint)6442459232L);
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rbx_v4 (Il2CppMethodInfo)+52]");
				if ((nint)0 != 0)
				{
					goto IL_0107;
				}
				invoke_impl = unchecked((nint)6442459120L);
			}
			goto IL_0259;
		}
		NullReferenceException ex = new NullReferenceException();
		nint num2 = unchecked((nint)null);
		goto IL_0325;
		IL_031a:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		return;
		IL_0259:
		((Delegate)onNewCameraFoundDelegate).invoke_impl = invoke_impl;
		((Delegate)onNewCameraFoundDelegate).extra_arg = unchecked((nint)6442458752L);
		Delegate obj2 = Delegate.Combine(instance.OnNewCameraFound, onNewCameraFoundDelegate);
		if ((object)obj2 == null)
		{
			instance.OnNewCameraFound = null;
			return;
		}
		bool flag = (object)obj2.GetType() != typeof(CameraDetector.OnNewCameraFoundDelegate);
		Delegate obj3 = null;
		if (!flag)
		{
			obj3 = obj2;
		}
		bool flag2 = (object)obj3 == null;
		nint num3 = (nint)typeof(CameraDetector.OnNewCameraFoundDelegate);
		if (flag2)
		{
			goto IL_031a;
		}
		instance.OnNewCameraFound = (CameraDetector.OnNewCameraFoundDelegate)obj3;
		bool flag3 = (object)obj2.GetType() != typeof(CameraDetector.OnNewCameraFoundDelegate);
		Delegate obj4 = null;
		if (!flag3)
		{
			obj4 = obj2;
		}
		bool flag4 = (object)obj4 == null;
		num3 = (nint)typeof(CameraDetector.OnNewCameraFoundDelegate);
		ex = (NullReferenceException)(object)obj2;
		num2 = (nint)typeof(CameraDetector.OnNewCameraFoundDelegate);
		if (!flag4)
		{
			return;
		}
		goto IL_0325;
		IL_0325:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_031a;
		IL_0107:
		((Delegate)onNewCameraFoundDelegate).method_code = (IntPtr)((Delegate)onNewCameraFoundDelegate).m_target;
		invoke_impl = ((Delegate)onNewCameraFoundDelegate).method_ptr;
		goto IL_0259;
	}

	protected void onNewCamera(Camera cam)
	{
		//IL_0005: Expected I, but got O
		//IL_001f: Expected O, but got I
		//IL_002f: Expected O, but got I
		nint num = (nint)this;
		float clipValue = _clipValue;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.CameraClipConnection>)+248]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.CameraClipConnection>)+250]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v3 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public void Apply()
	{
		//IL_0005: Expected I, but got O
		//IL_001f: Expected O, but got I
		//IL_002f: Expected O, but got I
		nint num = (nint)this;
		float clipValue = _clipValue;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.CameraClipConnection>)+248]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.CameraClipConnection>)+250]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v3 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override float Get()
	{
		Camera camera;
		if (UseMain)
		{
			Camera main = Camera.main;
			if (main != null)
			{
				camera = Camera.main;
				goto IL_0048;
			}
		}
		if (UseMarkers)
		{
			CameraMarker<FieldOfViewMarker> firstValidMarker = CameraMarker<FieldOfViewMarker>.GetFirstValidMarker();
			if (firstValidMarker != null)
			{
				camera = firstValidMarker.Camera;
				goto IL_0048;
			}
		}
		return _clipValue;
		IL_0048:
		if (Mode != ClippingMode.Far)
		{
			return camera.nearClipPlane;
		}
		return camera.farClipPlane;
	}

	public unsafe override void Set(float value)
	{
		//IL_00ac: Expected O, but got Ref
		_clipValue = value;
		if (UseMain)
		{
			Camera main = Camera.main;
			if (main != null)
			{
				Camera main2 = Camera.main;
				setClipValue(main2, value);
				return;
			}
		}
		if (!UseMarkers)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<CameraMarker<CameraClipMarker>>.Enumerator enumerator = default(List<CameraMarker<CameraClipMarker>>.Enumerator);
		CameraMarker<CameraClipMarker> cameraMarker = default(CameraMarker<CameraClipMarker>);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = (object)cameraMarker == null;
				List<CameraMarker<CameraClipMarker>> list = (List<CameraMarker<CameraClipMarker>>)(&enumerator);
				if (flag)
				{
					break;
				}
				if (cameraMarker.IsValid())
				{
					Camera camera = cameraMarker.Camera;
					setClipValue(camera, value);
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public float getClipValue(Camera cam)
	{
		if (Mode != ClippingMode.Far)
		{
			return cam.nearClipPlane;
		}
		return cam.farClipPlane;
	}

	public void setClipValue(Camera cam, float value)
	{
		UnityEngine.Object context;
		string message;
		if (Mode != ClippingMode.Far)
		{
			float nearClipPlane = cam.nearClipPlane;
			float farClipPlane = cam.farClipPlane;
			if (farClipPlane > nearClipPlane)
			{
				cam.nearClipPlane = value;
				return;
			}
			context = null;
			message = "CameraCipConnection: You can not set the near clipping distance higher than the far clipping distance!";
		}
		else
		{
			float farClipPlane2 = cam.farClipPlane;
			float nearClipPlane2 = cam.nearClipPlane;
			if (farClipPlane2 > nearClipPlane2)
			{
				cam.farClipPlane = value;
				return;
			}
			context = null;
			message = "CameraCipConnection: You can not set the far clipping distance lower than the near clipping distance!";
		}
		Logger.LogWarning(message, context);
	}
}
