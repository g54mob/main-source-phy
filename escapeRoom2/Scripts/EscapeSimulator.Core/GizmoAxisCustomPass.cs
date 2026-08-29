using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

[Serializable]
public class GizmoAxisCustomPass : CustomPass
{
	public List<Renderer> gizmoRenderers = new List<Renderer>();

	public Camera gizmoCamera;

	public RawImage gizmoImage;

	private readonly Vector3[] gizmoImageCorners = new Vector3[4];

	protected override void Execute(CustomPassContext context)
	{
		if (gizmoCamera == null || gizmoImage == null || gizmoRenderers.Count == 0)
		{
			return;
		}
		context.cmd.SetViewport(getGizmoImageScreenRect());
		context.cmd.SetViewProjectionMatrices(gizmoCamera.worldToCameraMatrix, gizmoCamera.projectionMatrix);
		foreach (Renderer gizmoRenderer in gizmoRenderers)
		{
			context.cmd.DrawRenderer(gizmoRenderer, gizmoRenderer.sharedMaterial);
		}
		context.cmd.SetViewProjectionMatrices(context.hdCamera.camera.worldToCameraMatrix, context.hdCamera.camera.projectionMatrix);
	}

	public Rect getGizmoImageScreenRect()
	{
		RectTransform rectTransform = gizmoImage.rectTransform;
		Vector3[] array = gizmoImageCorners;
		rectTransform.GetWorldCorners(array);
		for (int i = 0; i < 4; i++)
		{
			array[i] = RectTransformUtility.WorldToScreenPoint(null, array[i]);
		}
		float x = array[0].x;
		float y = array[0].y;
		float width = array[2].x - array[0].x;
		float height = array[2].y - array[0].y;
		return new Rect(x, y, width, height);
	}
}
