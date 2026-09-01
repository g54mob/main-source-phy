using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class ImCanvasContext
{
	public Camera camera;

	public Canvas canvas;

	public Rect canvasRect;

	public Matrix4x4 worldToCanvas;

	public Matrix4x4 canvasToWorld;

	public Matrix4x4 canvasToWorldNet;

	internal void UpdateParams(Canvas canvas, Camera camera, RectTransform cnvTf, Matrix4x4 canvasToWorldNet)
	{
		//IL_0035: Expected O, but got F4
		//IL_0056: Expected O, but got F4
		//IL_0090: Expected O, but got F4
		this.camera = camera;
		this.canvas = canvas;
		canvasRect = (Rect)cnvTf.rect.m_XMin;
		Matrix4x4 worldToLocalMatrix = cnvTf.worldToLocalMatrix;
		worldToCanvas = (Matrix4x4)worldToLocalMatrix.m00;
		_ = worldToLocalMatrix.m01;
		_ = worldToLocalMatrix.m02;
		_ = worldToLocalMatrix.m03;
		Matrix4x4 localToWorldMatrix = cnvTf.localToWorldMatrix;
		canvasToWorld = (Matrix4x4)localToWorldMatrix.m00;
		_ = localToWorldMatrix.m01;
		object obj = default(object);
		this.canvasToWorldNet = (Matrix4x4)obj;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ stack_28+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ stack_28+20]");
		_ = 0;
		_ = localToWorldMatrix.m02;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ stack_28+30]");
		_ = 0;
		_ = localToWorldMatrix.m03;
	}
}
