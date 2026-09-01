using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class CameraDetector : MonoBehaviour
{
	public delegate void OnNewCameraFoundDelegate(Camera cam);

	public OnNewCameraFoundDelegate OnNewCameraFound;

	private static CameraDetector _instance;

	protected Camera[] _previousCameras;

	protected Camera[] _cameras;

	public static CameraDetector Instance
	{
		get
		{
			if (!_instance)
			{
				GameObject gameObject = new GameObject();
				if ((object)gameObject != null)
				{
					CameraDetector instance = gameObject.AddComponent<CameraDetector>();
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
				return (CameraDetector)(object)new NullReferenceException();
			}
			goto IL_010c;
			IL_010c:
			return _instance;
		}
	}

	public Camera[] Cameras => _cameras;

	private CameraDetector()
	{
		Camera[] previousCameras = new Camera[10];
		_previousCameras = previousCameras;
		Camera[] cameras = new Camera[10];
		_cameras = cameras;
		base._002Ector();
	}

	private void Update()
	{
		//IL_000e: Expected O, but got I4
		//IL_0017: Expected O, but got I4
		//IL_0020: Expected O, but got I4
		//IL_00ca: Expected O, but got I4
		//IL_00d3: Expected O, but got I4
		//IL_00dc: Expected O, but got I4
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_0107: Expected O, but got I
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Expected O, but got Unknown
		//IL_013c: Expected O, but got I
		Camera[] cameras = _cameras;
		object obj = 0;
		object obj2 = 32;
		object obj3 = 0;
		while ((nint)obj3 < cameras.Length)
		{
			Camera[] cameras2 = _cameras;
			Camera[] previousCameras = _previousCameras;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ r14_v4+v79 @ rax_v21 (UnityEngine.Camera[])]");
			_ = 0;
			Camera[] cameras3 = _cameras;
			_ = 0;
			cameras = _cameras;
			obj++;
			obj2 += 8;
			obj3 = obj;
		}
		increaseCapacity();
		int allCameras = Camera.GetAllCameras(_cameras);
		Camera[] cameras4 = _cameras;
		object obj4 = 0;
		object obj5 = 32;
		object obj6 = 0;
		bool flag;
		do
		{
			if ((nint)obj6 >= cameras4.Length)
			{
				return;
			}
			Camera[] cameras5 = _cameras;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ r15_v4+v83 @ rax_v12 (UnityEngine.Camera[])]");
			if ((UnityEngine.Object)0 != null)
			{
				Camera[] previousCameras2 = _previousCameras;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ r15_v4+v83 @ rax_v12 (UnityEngine.Camera[])]");
				if (!contains(previousCameras2, (Camera)0))
				{
					OnNewCameraFoundDelegate onNewCameraFound = OnNewCameraFound;
					if (OnNewCameraFound != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v364.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
					}
				}
			}
			cameras4 = _cameras;
			obj4++;
			obj5 += 8;
			flag = _cameras != null;
			obj6 = obj4;
		}
		while (flag);
		throw new NullReferenceException();
	}

	protected void increaseCapacity()
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_0089: Expected O, but got I4
		//IL_0092: Expected O, but got I4
		//IL_00ec: Expected O, but got I4
		//IL_00f5: Expected O, but got I4
		//IL_00fe: Expected O, but got I4
		//IL_00ab: Expected O, but got I4
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected O, but got Unknown
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		Camera[] cameras = _cameras;
		int allCamerasCount = Camera.GetAllCamerasCount();
		if (cameras.Length < allCamerasCount)
		{
			int allCamerasCount2 = Camera.GetAllCamerasCount();
			Camera[] array = new Camera[allCamerasCount2];
			int allCamerasCount3 = Camera.GetAllCamerasCount();
			Camera[] array2 = new Camera[allCamerasCount3];
			object obj = array2 + 32;
			object obj2 = (object)array - (object)array2;
			object obj3 = 0;
			object obj4 = 0;
			while ((nint)obj4 < array.Length)
			{
				_ = 0;
				obj = 0;
				obj3++;
				obj += 8;
				obj4 = obj3;
			}
			Camera[] previousCameras = _previousCameras;
			object obj5 = 32;
			object obj6 = 0;
			object obj7 = 0;
			while ((nint)obj7 < previousCameras.Length)
			{
				Camera[] previousCameras2 = _previousCameras;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ rsi_v8+v79 @ rax_v18 (UnityEngine.Camera[])]");
				_ = 0;
				previousCameras = _previousCameras;
				obj6++;
				obj5 += 8;
				obj7 = obj6;
			}
			_cameras = array;
			_previousCameras = array2;
		}
	}

	protected bool contains(Camera[] cameras, Camera cam)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_001c: Expected O, but got I4
		//IL_0025: Expected O, but got I4
		//IL_00c2: Expected I4, but got O
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		object obj = cameras + 32;
		object obj2 = 0;
		object obj3 = 0;
		while (true)
		{
			if ((nint)obj3 < cameras.Length)
			{
				if ((nint)obj2 >= cameras.Length)
				{
					break;
				}
				if ((UnityEngine.Object)obj != cam)
				{
					obj2++;
					obj += 8;
					obj3 = obj2;
					continue;
				}
				return true;
			}
			return false;
		}
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		return (byte)(int)ex != 0;
	}
}
