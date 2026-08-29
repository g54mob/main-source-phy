using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace Battlehub.RTCommon.HDRP
{
	public class RenderGraphics : CustomPass
	{
		protected Dictionary<Camera, List<IRTECamera>> m_cameras;

		[SerializeField]
		private bool m_afterImageEffects;

		[SerializeField]
		private bool m_clearDepth = true;

		protected RTECommandBuffer s_commandBufferWrapper;

		protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			base.Setup(renderContext, cmd);
			s_commandBufferWrapper = new RTECommandBuffer();
			m_cameras = new Dictionary<Camera, List<IRTECamera>>();
			RTECamera[] array = UnityObjectExt.FindObjectsByType<RTECamera>();
			foreach (RTECamera camera in array)
			{
				AddCamera(camera);
			}
			RTECamera.Created += OnRTECameraCreated;
			RTECamera.Destroyed += OnRTECameraDestroyed;
		}

		protected override void Cleanup()
		{
			base.Cleanup();
			RTECamera.Created -= OnRTECameraCreated;
			RTECamera.Destroyed -= OnRTECameraDestroyed;
			s_commandBufferWrapper = null;
			m_cameras.Clear();
		}

		private void OnRTECameraCreated(IRTECamera camera)
		{
			AddCamera(camera);
		}

		private void OnRTECameraDestroyed(IRTECamera camera)
		{
			RemoveCamera(camera);
		}

		protected virtual void AddCamera(IRTECamera camera)
		{
			switch (base.injectionPoint)
			{
			case CustomPassInjectionPoint.BeforeTransparent:
				if (camera.Event == CameraEvent.BeforeForwardAlpha || camera.Event == CameraEvent.AfterForwardAlpha)
				{
					GetList(camera).Add(camera);
				}
				break;
			case CustomPassInjectionPoint.BeforePostProcess:
				if (!m_afterImageEffects)
				{
					if (camera.Event == CameraEvent.BeforeImageEffects || camera.Event == CameraEvent.BeforeImageEffectsOpaque)
					{
						GetList(camera).Add(camera);
					}
				}
				else if (camera.Event == CameraEvent.AfterImageEffects || camera.Event == CameraEvent.AfterImageEffectsOpaque)
				{
					GetList(camera).Add(camera);
				}
				break;
			}
		}

		protected virtual void RemoveCamera(IRTECamera camera)
		{
			if (m_cameras.TryGetValue(camera.Camera, out var value))
			{
				value.Remove(camera);
				if (value.Count == 0)
				{
					m_cameras.Remove(camera.Camera);
				}
			}
		}

		protected List<IRTECamera> GetList(IRTECamera camera)
		{
			if (!m_cameras.TryGetValue(camera.Camera, out var value))
			{
				value = new List<IRTECamera>();
				m_cameras.Add(camera.Camera, value);
			}
			return value;
		}

		protected override void Execute(CustomPassContext ctx)
		{
			Execute(ctx.cmd, ctx.hdCamera);
		}

		private void Execute(CommandBuffer cmd, HDCamera hdCamera)
		{
			if (m_cameras.TryGetValue(hdCamera.camera, out var value))
			{
				cmd.ClearRenderTarget(m_clearDepth, clearColor: false, Color.black);
				for (int i = 0; i < value.Count; i++)
				{
					IRTECamera iRTECamera = value[i];
					s_commandBufferWrapper.WrappedCommandBuffer = cmd;
					iRTECamera.RTECommandBufferOverride = s_commandBufferWrapper;
					iRTECamera.RefreshCommandBuffer();
				}
			}
		}
	}
}
