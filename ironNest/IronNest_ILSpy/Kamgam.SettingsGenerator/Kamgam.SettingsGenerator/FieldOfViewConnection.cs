using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using Unity.Cinemachine;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class FieldOfViewConnection : Connection<float>
{
	public const float DefaultFallback = 60f;

	public bool UseMain;

	public bool UseMarkers;

	[NonSerialized]
	protected float _fieldOfView;

	public bool trySetCinemachineValue(Camera camera, float value)
	{
		//IL_002d: Expected O, but got I4
		//IL_004b: Expected I4, but got O
		bool flag = tryGetCinemachineCamera(camera, out var _);
		if (!flag)
		{
			return flag;
		}
		object obj = 0;
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public unsafe bool tryGetCinemachineValue(Camera camera, out float fieldOfView)
	{
		//IL_006b: Expected I4, but got O
		bool flag = tryGetCinemachineCamera(camera, out var cinemaCamera);
		if (!flag)
		{
			ref float reference = ref *(float*)1114636288;
			return flag;
		}
		if ((object)cinemaCamera != null)
		{
			ref float reference = ref *(float*)cinemaCamera.Lens;
			return true;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private unsafe static bool tryGetCinemachineCamera(Camera camera, out CinemachineCamera cinemaCamera)
	{
		//IL_0157: Expected I4, but got O
		if (!(camera != null))
		{
			goto IL_013a;
		}
		ref CinemachineCamera reference;
		if ((object)camera != null)
		{
			GameObject gameObject = camera.gameObject;
			if ((object)gameObject != null)
			{
				if (!gameObject.TryGetComponent<CinemachineBrain>(out var component))
				{
					goto IL_013a;
				}
				if ((object)component != null)
				{
					ICinemachineCamera activeVirtualCamera = component.ActiveVirtualCamera;
					if (activeVirtualCamera == null)
					{
						reference = ref *(CinemachineCamera*)null;
					}
					else
					{
						bool flag = (object)activeVirtualCamera.GetType() != typeof(CinemachineCamera);
						ICinemachineCamera cinemachineCamera = null;
						if (!flag)
						{
							cinemachineCamera = activeVirtualCamera;
						}
						reference = ref *(CinemachineCamera*)cinemachineCamera;
						if ((object)activeVirtualCamera.GetType() == typeof(CinemachineCamera))
						{
							/*Error: End of method reached without returning.*/;
						}
					}
					return cinemaCamera != null;
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_013a:
		reference = ref *(CinemachineCamera*)null;
		return false;
	}

	public unsafe FieldOfViewConnection(bool useMain = true, bool useMarkers = true)
	{
		//IL_01b3: Expected I, but got O
		//IL_0050: Expected I, but got O
		//IL_00fd: Expected I, but got O
		//IL_0226: Expected I, but got I8
		//IL_00e6: Expected I, but got I8
		//IL_00ac: Expected I, but got I8
		//IL_0279: Expected I4, but got O
		//IL_02a4: Expected I4, but got O
		//IL_02ba: Expected I, but got O
		_fieldOfView = 60f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037D8E0");
		UseMain = useMain;
		bool useMarkers2 = default(bool);
		UseMarkers = useMarkers2;
		CameraDetector instance = CameraDetector.Instance;
		CameraDetector.OnNewCameraFoundDelegate onNewCameraFoundDelegate;
		nint invoke_impl;
		if ((object)instance != null)
		{
			onNewCameraFoundDelegate = null;
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rbx_v4 (Il2CppMethodInfo)+8]");
			((Delegate)onNewCameraFoundDelegate).method_ptr = (IntPtr)0;
			((Delegate)onNewCameraFoundDelegate).method = (nint)__ldftn(FieldOfViewConnection.onNewCamera);
			((Delegate)onNewCameraFoundDelegate).m_target = this;
			((Delegate)onNewCameraFoundDelegate).method_code = (IntPtr)onNewCameraFoundDelegate;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66E0");
			object obj = default(object);
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rbx_v4 (Il2CppMethodInfo)+52]");
				if ((nint)0 != 1)
				{
					goto IL_00eb;
				}
				invoke_impl = unchecked((nint)6442459232L);
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rbx_v4 (Il2CppMethodInfo)+52]");
				if ((nint)0 != 0)
				{
					goto IL_00eb;
				}
				invoke_impl = unchecked((nint)6442459120L);
			}
			goto IL_0207;
		}
		NullReferenceException ex = new NullReferenceException();
		nint num2 = unchecked((nint)null);
		goto IL_02d3;
		IL_00eb:
		((Delegate)onNewCameraFoundDelegate).method_code = (IntPtr)((Delegate)onNewCameraFoundDelegate).m_target;
		invoke_impl = ((Delegate)onNewCameraFoundDelegate).method_ptr;
		goto IL_0207;
		IL_02d3:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_02c8;
		IL_0207:
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
		useMarkers2 = (byte)(int)typeof(CameraDetector.OnNewCameraFoundDelegate) != 0;
		if (flag2)
		{
			goto IL_02c8;
		}
		instance.OnNewCameraFound = (CameraDetector.OnNewCameraFoundDelegate)obj3;
		bool flag3 = (object)obj2.GetType() != typeof(CameraDetector.OnNewCameraFoundDelegate);
		Delegate obj4 = null;
		if (!flag3)
		{
			obj4 = obj2;
		}
		bool flag4 = (object)obj4 == null;
		useMarkers2 = (byte)(int)typeof(CameraDetector.OnNewCameraFoundDelegate) != 0;
		ex = (NullReferenceException)(object)obj2;
		num2 = (nint)typeof(CameraDetector.OnNewCameraFoundDelegate);
		if (!flag4)
		{
			return;
		}
		goto IL_02d3;
		IL_02c8:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	protected void onNewCamera(Camera cam)
	{
		//IL_0005: Expected I, but got O
		//IL_001f: Expected O, but got I
		//IL_002f: Expected O, but got I
		nint num = (nint)this;
		float fieldOfView = _fieldOfView;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.FieldOfViewConnection>)+248]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.FieldOfViewConnection>)+250]");
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
		float fieldOfView = _fieldOfView;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.FieldOfViewConnection>)+248]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.FieldOfViewConnection>)+250]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v3 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override float Get()
	{
		//IL_0089: Expected F4, but got O
		CinemachineCamera cinemaCamera;
		CinemachineCamera cinemachineCamera;
		if (UseMain)
		{
			Camera main = Camera.main;
			if (main != null)
			{
				Camera main2 = Camera.main;
				if (tryGetCinemachineCamera(main2, out cinemaCamera))
				{
					cinemachineCamera = cinemaCamera;
					goto IL_007f;
				}
				Camera main3 = Camera.main;
				return main3.fieldOfView;
			}
		}
		if (UseMarkers)
		{
			CameraMarker<FieldOfViewMarker> firstValidMarker = CameraMarker<FieldOfViewMarker>.GetFirstValidMarker();
			if (firstValidMarker != null)
			{
				Camera camera = firstValidMarker.Camera;
				if (tryGetCinemachineCamera(camera, out cinemaCamera))
				{
					cinemachineCamera = cinemaCamera;
					goto IL_007f;
				}
				Camera camera2 = firstValidMarker.Camera;
				return camera2.fieldOfView;
			}
		}
		return _fieldOfView;
		IL_007f:
		return (float)cinemachineCamera.Lens;
	}

	public unsafe override void Set(float fieldOfView)
	{
		//IL_0125: Expected O, but got Ref
		//IL_00cb: Expected O, but got F4
		_fieldOfView = fieldOfView;
		if (UseMain)
		{
			Camera main = Camera.main;
			if (main != null)
			{
				Camera main2 = Camera.main;
				if (!tryGetCinemachineCamera(main2, out var cinemaCamera))
				{
					Camera main3 = Camera.main;
					if ((object)main3 != null)
					{
						main3.fieldOfView = fieldOfView;
						return;
					}
					goto IL_01d8;
				}
				if ((object)cinemaCamera != null)
				{
					cinemaCamera.Lens = (LensSettings)fieldOfView;
					return;
				}
				throw new NullReferenceException();
			}
		}
		if (!UseMarkers)
		{
			return;
		}
		if (CameraMarker<FieldOfViewMarker>.Markers != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<CameraMarker<FieldOfViewMarker>>.Enumerator enumerator = default(List<CameraMarker<FieldOfViewMarker>>.Enumerator);
			CameraMarker<FieldOfViewMarker> cameraMarker = default(CameraMarker<FieldOfViewMarker>);
			while (true)
			{
				if (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = (object)cameraMarker == null;
					CameraMarker<FieldOfViewMarker> cameraMarker2 = (CameraMarker<FieldOfViewMarker>)(&enumerator);
					if (!flag)
					{
						if (!cameraMarker.IsValid())
						{
							continue;
						}
						Camera camera = cameraMarker.Camera;
						if (!trySetCinemachineValue(camera, fieldOfView))
						{
							Camera camera2 = cameraMarker.Camera;
							if ((object)camera2 == null)
							{
								break;
							}
							camera2.fieldOfView = fieldOfView;
						}
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				return;
			}
			throw new NullReferenceException();
		}
		goto IL_01d8;
		IL_01d8:
		throw new NullReferenceException();
	}
}
