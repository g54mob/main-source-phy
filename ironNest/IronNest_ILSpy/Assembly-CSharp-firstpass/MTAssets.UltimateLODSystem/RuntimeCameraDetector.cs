using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace MTAssets.UltimateLODSystem;

public class RuntimeCameraDetector : MonoBehaviour
{
	private sealed class _003CArrayOfCamerasDelayedUpdater_003Ed__4 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RuntimeCameraDetector _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CArrayOfCamerasDelayedUpdater_003Ed__4(int _003C_003E1__state)
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
			//IL_0090: Expected I4, but got I8
			//IL_00fe: Expected I4, but got O
			RuntimeCameraDetector runtimeCameraDetector = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_00ca;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
				Camera[] allCameras = Camera.allCameras;
				if ((object)_003C_003E4__this != null)
				{
					runtimeCameraDetector.currentArrayOfCameras = allCameras;
					goto IL_00ca;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00ca:
			_003C_003E2__current = runtimeCameraDetector.DELAY_BETWEEN_ARRAY_OF_CAMERAS_UPDATE;
			_003C_003E1__state = 1;
			return true;
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

	private sealed class _003CCurrentCameraOnScreenDetector_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RuntimeCameraDetector _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCurrentCameraOnScreenDetector_003Ed__5(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0039: Expected I4, but got I8
			//IL_0047: Expected O, but got I4
			//IL_0066: Expected I4, but got I8
			//IL_046d: Expected I4, but got O
			//IL_00cd: Expected O, but got I
			//IL_0484: Unknown result type (might be due to invalid IL or missing references)
			//IL_0489: Expected O, but got Unknown
			//IL_0123: Expected O, but got I
			//IL_018c: Expected O, but got I
			//IL_01e2: Expected O, but got I
			//IL_01fe: Invalid comparison between O and F4
			//IL_0259: Expected O, but got I
			//IL_0275: Invalid comparison between O and F4
			//IL_02d0: Expected O, but got I
			//IL_0355: Expected O, but got I
			//IL_03bc: Expected O, but got I
			RuntimeCameraDetector runtimeCameraDetector = _003C_003E4__this;
			if (_003C_003E1__state != 0 && _003C_003E1__state != 1)
			{
				return false;
			}
			_003C_003E1__state = -1;
			object obj = 32;
			float num = -1000f;
			int num2 = 0;
			int num3 = -1;
			int num4 = 0;
			object obj2 = default(object);
			Camera currentCameraThatIsOnTopOfScreenInThisScene;
			while (true)
			{
				Camera[] currentArrayOfCameras = runtimeCameraDetector.currentArrayOfCameras;
				if (num4 < currentArrayOfCameras.Length)
				{
					if (num2 < currentArrayOfCameras.Length)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rsi_v4+v161 @ rax_v8 (UnityEngine.Camera[])]");
						if (!((UnityEngine.Object)0 != null))
						{
							goto IL_046d;
						}
						Camera[] currentArrayOfCameras2 = runtimeCameraDetector.currentArrayOfCameras;
						if (num2 < currentArrayOfCameras2.Length)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rsi_v4+v162 @ rax_v24 (UnityEngine.Camera[])]");
							RenderTexture targetTexture = ((Camera)0).targetTexture;
							if (!(targetTexture == null))
							{
								goto IL_046d;
							}
							Camera[] currentArrayOfCameras3 = runtimeCameraDetector.currentArrayOfCameras;
							if (num2 < currentArrayOfCameras3.Length)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rsi_v4+v163 @ rax_v28 (UnityEngine.Camera[])]");
								if (((Camera)0).orthographic)
								{
									goto IL_046d;
								}
								Camera[] currentArrayOfCameras4 = runtimeCameraDetector.currentArrayOfCameras;
								if (num2 < currentArrayOfCameras4.Length)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rsi_v4+v164 @ rax_v30 (UnityEngine.Camera[])]");
									Rect rect = ((Camera)0).rect;
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp near ptr 00000001803AF5A1h\"");
									if (obj2 != (object)1f)
									{
										goto IL_046d;
									}
									Camera[] currentArrayOfCameras5 = runtimeCameraDetector.currentArrayOfCameras;
									if (num2 < currentArrayOfCameras5.Length)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rsi_v4+v165 @ rax_v33 (UnityEngine.Camera[])]");
										Rect rect2 = ((Camera)0).rect;
										Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp near ptr 00000001803AF5A1h\"");
										if (obj2 != (object)1f)
										{
											goto IL_046d;
										}
										Camera[] currentArrayOfCameras6 = runtimeCameraDetector.currentArrayOfCameras;
										if (num2 < currentArrayOfCameras6.Length)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rsi_v4+v166 @ rax_v36 (UnityEngine.Camera[])]");
											float depth = ((Camera)0).depth;
											Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803AF542h\"");
											if (num == depth)
											{
												Debug.LogError("Ultimate LOD System: There are one or more cameras active in this scene, which have the same depth level. This causes the camera detection algorithm that is currently appearing on the screen to not work. Please set different depth values for each camera that is active at the same time in your scene, or disable cameras that are not being used and leave only the cameras that are being used, enabled.");
											}
											Camera[] currentArrayOfCameras7 = runtimeCameraDetector.currentArrayOfCameras;
											if (num2 < currentArrayOfCameras7.Length)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rsi_v4+v167 @ rax_v38 (UnityEngine.Camera[])]");
												float depth2 = ((Camera)0).depth;
												if (!(depth2 > num))
												{
													goto IL_046d;
												}
												Camera[] currentArrayOfCameras8 = runtimeCameraDetector.currentArrayOfCameras;
												if (num2 < currentArrayOfCameras8.Length)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rsi_v4+v168 @ rax_v39 (UnityEngine.Camera[])]");
													depth2 = ((Camera)0).depth;
													num = depth2;
													num3 = num2;
													goto IL_046d;
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
					if (num3 == -1)
					{
						currentCameraThatIsOnTopOfScreenInThisScene = null;
						break;
					}
					if (num3 < currentArrayOfCameras.Length)
					{
						currentCameraThatIsOnTopOfScreenInThisScene = currentArrayOfCameras[num3];
						break;
					}
				}
				IndexOutOfRangeException ex = new IndexOutOfRangeException();
				return (byte)(int)ex != 0;
				IL_046d:
				num2++;
				obj += 8;
				num4 = num2;
			}
			UltimateLevelOfDetailGlobal.currentCameraThatIsOnTopOfScreenInThisScene = currentCameraThatIsOnTopOfScreenInThisScene;
			_003C_003E2__current = runtimeCameraDetector.DELAY_BETWEEN_CURRENT_CAMERA_DETECTOR;
			_003C_003E1__state = 1;
			return true;
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

	private WaitForSecondsRealtime DELAY_BETWEEN_ARRAY_OF_CAMERAS_UPDATE;

	private WaitForSecondsRealtime DELAY_BETWEEN_CURRENT_CAMERA_DETECTOR;

	private Camera[] currentArrayOfCameras;

	public void Awake()
	{
		Camera[] allCameras = Camera.allCameras;
		currentArrayOfCameras = allCameras;
		_003CArrayOfCamerasDelayedUpdater_003Ed__4 obj = new _003CArrayOfCamerasDelayedUpdater_003Ed__4(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
		_003CCurrentCameraOnScreenDetector_003Ed__5 obj2 = new _003CCurrentCameraOnScreenDetector_003Ed__5(0);
		obj2._003C_003E1__state = 0;
		obj2._003C_003E4__this = this;
		Coroutine coroutine2 = StartCoroutine(obj2);
	}

	private IEnumerator ArrayOfCamerasDelayedUpdater()
	{
		_003CArrayOfCamerasDelayedUpdater_003Ed__4 obj = new _003CArrayOfCamerasDelayedUpdater_003Ed__4(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator CurrentCameraOnScreenDetector()
	{
		_003CCurrentCameraOnScreenDetector_003Ed__5 obj = new _003CCurrentCameraOnScreenDetector_003Ed__5(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public RuntimeCameraDetector()
	{
		WaitForSecondsRealtime dELAY_BETWEEN_ARRAY_OF_CAMERAS_UPDATE = new WaitForSecondsRealtime(0.5f);
		DELAY_BETWEEN_ARRAY_OF_CAMERAS_UPDATE = dELAY_BETWEEN_ARRAY_OF_CAMERAS_UPDATE;
		DELAY_BETWEEN_CURRENT_CAMERA_DETECTOR = new WaitForSecondsRealtime(0.09f);
		currentArrayOfCameras = new Camera[0];
		base._002Ector();
	}
}
