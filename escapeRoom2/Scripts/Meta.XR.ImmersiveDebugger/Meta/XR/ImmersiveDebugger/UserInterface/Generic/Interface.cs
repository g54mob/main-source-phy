using UnityEngine;
using UnityEngine.EventSystems;

namespace Meta.XR.ImmersiveDebugger.UserInterface.Generic
{
	public class Interface : Controller
	{
		private ProxyInputModule _proxyInputModule;

		private ProxyCameraRig _proxyCameraRig;

		internal Cursor Cursor { get; private set; }

		internal Camera Camera => _proxyCameraRig.Camera;

		protected virtual bool FollowOverride { get; set; }

		protected virtual bool RotateOverride { get; set; }

		internal virtual void Awake()
		{
			Setup(null);
			GameObject gameObject = new GameObject("cursor");
			gameObject.transform.SetParent(base.Transform);
			Cursor = gameObject.AddComponent<Cursor>();
			_proxyCameraRig = new ProxyCameraRig();
			_proxyInputModule = new ProxyInputModule(base.GameObject, Cursor);
		}

		private void UpdateTransform()
		{
			if (FollowOverride)
			{
				base.Transform.position = _proxyCameraRig.CameraTransform.position;
			}
			if (RotateOverride)
			{
				Vector3 eulerAngles = _proxyCameraRig.CameraTransform.eulerAngles;
				eulerAngles.x = 0f;
				eulerAngles.z = 0f;
				base.Transform.rotation = Quaternion.Euler(eulerAngles);
			}
		}

		private void UpdateController()
		{
			OVRInputModule inputModule = _proxyInputModule.InputModule;
			inputModule.rayTransform = OVRInput.GetActiveController() switch
			{
				OVRInput.Controller.LTouch => _proxyCameraRig.LeftControllerTransform, 
				OVRInput.Controller.RTouch => _proxyCameraRig.RightControllerTransform, 
				_ => _proxyCameraRig.RightControllerTransform, 
			};
		}

		private void UpdateCulling()
		{
			RuntimeSettings instance = RuntimeSettings.Instance;
			if (instance.AutomaticLayerCullingUpdate)
			{
				int cullingMask = Camera.cullingMask;
				int num = SetBits(cullingMask, instance.PanelLayer, instance.MeshRendererLayer, state: false);
				if (num != cullingMask)
				{
					Camera.cullingMask = num;
				}
			}
		}

		private static int SetBits(int cullingMask, int bitPosition1, int bitPosition2, bool state)
		{
			if (state)
			{
				cullingMask |= 1 << bitPosition1;
				cullingMask |= 1 << bitPosition2;
			}
			else
			{
				cullingMask &= ~(1 << bitPosition1);
				cullingMask &= ~(1 << bitPosition2);
			}
			return cullingMask;
		}

		internal virtual void LateUpdate()
		{
			UpdateRefreshLayout(force: false);
			if (_proxyCameraRig.Refresh())
			{
				UpdateTransform();
				UpdateCulling();
				if (_proxyInputModule.Refresh())
				{
					UpdateController();
				}
			}
		}

		protected override void RefreshLayoutPreChildren()
		{
		}
	}
}
