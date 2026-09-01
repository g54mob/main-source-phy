using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

public class ImmediateModeShapeDrawer : MonoBehaviour
{
	public bool useCullingMasks;

	public virtual void DrawShapes(Camera cam)
	{
	}

	private void OnCameraPreRender(Camera cam)
	{
		CameraType cameraType = cam.cameraType;
		if (cameraType == CameraType.Preview || cameraType == CameraType.Reflection)
		{
			return;
		}
		if (useCullingMasks)
		{
			int cullingMask = cam.cullingMask;
			GameObject gameObject = base.gameObject;
			int layer = gameObject.layer;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"bt esi,eax\"");
			if ((nint)gameObject >= 0)
			{
				return;
			}
		}
		DrawShapes(cam);
	}

	public virtual void OnEnable()
	{
		Action<ScriptableRenderContext, Camera> value = DrawShapesSRP;
		RenderPipelineManager.beginCameraRendering += value;
	}

	public virtual void OnDisable()
	{
		Action<ScriptableRenderContext, Camera> value = DrawShapesSRP;
		RenderPipelineManager.beginCameraRendering -= value;
	}

	private void DrawShapesSRP(ScriptableRenderContext ctx, Camera cam)
	{
		CameraType cameraType = cam.cameraType;
		if (cameraType == CameraType.Preview || cameraType == CameraType.Reflection)
		{
			return;
		}
		if (useCullingMasks)
		{
			int cullingMask = cam.cullingMask;
			GameObject gameObject = base.gameObject;
			int layer = gameObject.layer;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"bt esi,eax\"");
			if ((nint)gameObject >= 0)
			{
				return;
			}
		}
		DrawShapes(cam);
	}
}
