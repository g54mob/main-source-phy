using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class ImmediateModeCanvas : ImmediateModeShapeDrawer
{
	private static ImCanvasContext canvasContext;

	private Canvas canvas;

	private RectTransform canvasRectTf;

	private Camera camUI;

	private List<ImmediateModePanel> panels;

	private Canvas Canvas
	{
		get
		{
			Canvas result;
			if (this.canvas != null)
			{
				result = this.canvas;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				Canvas canvas = default(Canvas);
				result = canvas;
			}
			this.canvas = result;
			return result;
		}
	}

	private RectTransform CanvasRectTf
	{
		get
		{
			RectTransform result;
			if (canvasRectTf != null)
			{
				result = canvasRectTf;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				RectTransform rectTransform = default(RectTransform);
				result = rectTransform;
			}
			canvasRectTf = result;
			return result;
		}
	}

	private Camera CamUI
	{
		get
		{
			Camera result;
			if (camUI != null)
			{
				result = camUI;
			}
			else
			{
				Canvas canvas = Canvas;
				if ((object)canvas == null)
				{
					return (Camera)(object)new NullReferenceException();
				}
				Camera worldCamera = canvas.worldCamera;
				result = worldCamera;
			}
			camUI = result;
			return result;
		}
	}

	private bool IsCameraBasedUI
	{
		get
		{
			//IL_009c: Expected I4, but got O
			//IL_007a: Expected O, but got I4
			Canvas canvas = Canvas;
			if ((object)canvas != null)
			{
				Camera worldCamera = canvas.worldCamera;
				bool flag = worldCamera != null;
				if (!flag)
				{
					return flag;
				}
				Canvas canvas2 = Canvas;
				if ((object)canvas2 != null)
				{
					RenderMode renderMode = canvas2.renderMode;
					object obj = renderMode - 2;
					return obj == null;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public void Add(ImmediateModePanel panel)
	{
		panels.Add(panel);
	}

	public void Remove(ImmediateModePanel panel)
	{
		bool flag = panels.Remove(panel);
	}

	protected unsafe void DrawPanels()
	{
		//IL_0062: Expected I, but got O
		//IL_0082: Expected F4, but got I
		//IL_0097: Expected O, but got Ref
		//IL_0097: Expected O, but got Ref
		//IL_00ae: Expected I, but got O
		//IL_00cc: Expected O, but got F4
		//IL_00f3: Expected O, but got I4
		//IL_0103: Expected O, but got I
		//IL_010e: Expected F4, but got O
		//IL_022c: Expected I, but got O
		//IL_0255: Expected O, but got I
		//IL_02b0: Expected O, but got Ref
		//IL_02b0: Expected O, but got Ref
		//IL_02d5: Expected O, but got I4
		StateStack scope = Draw.Scope;
		Canvas canvas = Canvas;
		Matrix4x4 matrix4x3 = default(Matrix4x4);
		if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
		{
			nint num = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v464 @ rax_v63 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v465 @ rcx_v45 (Il2CppStaticFields<Shapes.Draw>)+98]");
			float num3 = 0f;
			Canvas canvas2 = (Canvas)(object)canvasContext;
			Matrix4x4 matrix4x2 = default(Matrix4x4);
			Matrix4x4 matrix4x = (Matrix4x4)(&matrix4x2) * (Matrix4x4)(&matrix4x3);
			nint num4 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v257 @ rax_v71 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num5 = 0;
			Draw.matrix = (Matrix4x4)matrix4x.m00;
			_ = matrix4x.m01;
			_ = matrix4x.m02;
			_ = matrix4x.m03;
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v550 @ rcx_v47 (UnityEngine.Canvas)+30]");
			matrix4x3 = (Matrix4x4)0;
			float num6 = (float)Draw.matrix;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<ImmediateModePanel>.Enumerator enumerator = default(List<ImmediateModePanel>.Enumerator);
		Component component = default(Component);
		float num9 = default(float);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				StateStack scope2 = Draw.Scope;
				Canvas canvas3 = Canvas;
				if ((object)canvas3 != null)
				{
					Component component2;
					if (canvas3.renderMode != RenderMode.ScreenSpaceOverlay)
					{
						bool flag = (object)component == null;
						Canvas canvas4 = canvas3;
						if (flag)
						{
							throw new NullReferenceException();
						}
						Transform transform = component.transform;
						bool flag2 = (object)transform == null;
						canvas4 = (Canvas)component;
						if (flag2)
						{
							break;
						}
						Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
						float num6 = localToWorldMatrix.m02;
						float num3 = localToWorldMatrix.m03;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9BEA0");
						component2 = component;
					}
					else
					{
						nint num7 = (nint)typeof(Draw);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v833 @ rax_v35 (Il2CppClass<Shapes.Draw>)+B8]");
						nint num8 = 0;
						bool flag3 = (object)component == null;
						ImmediateModeCanvas immediateModeCanvas = (ImmediateModeCanvas)num8;
						if (flag3)
						{
							throw new NullReferenceException();
						}
						Transform transform2 = component.transform;
						bool flag4 = (object)transform2 == null;
						Canvas canvas4 = (Canvas)component;
						if (flag4)
						{
							immediateModeCanvas = (ImmediateModeCanvas)(object)canvas4;
							throw new NullReferenceException();
						}
						Matrix4x4 localToWorldMatrix2 = transform2.localToWorldMatrix;
						Matrix4x4 matrix4x4 = ShapesMath.AffineMtxMul((Matrix4x4)(&matrix4x3), (Matrix4x4)(&num9));
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9BEA0");
						component2 = component;
						object obj = 0;
						matrix4x3 = Draw.matrix;
					}
					((ImmediateModePanel)component2).DrawPanel(canvasContext);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1810687A0");
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1810687A0");
			return;
		}
		throw new NullReferenceException();
	}

	private bool CameraShouldRenderUI(Camera cam)
	{
		//IL_017e: Expected I4, but got O
		//IL_015c: Expected O, but got I4
		if ((object)cam != null)
		{
			CameraType cameraType = cam.cameraType;
			if (cameraType != CameraType.Game)
			{
				return false;
			}
			if ((object)this.canvas != null)
			{
				if (this.canvas.renderMode != RenderMode.ScreenSpaceOverlay)
				{
					UnityEngine.Object obj;
					if (camUI != null)
					{
						obj = camUI;
					}
					else
					{
						Canvas canvas = Canvas;
						if ((object)canvas == null)
						{
							goto IL_0170;
						}
						Camera worldCamera = canvas.worldCamera;
						obj = worldCamera;
					}
					camUI = (Camera)obj;
					return cam == obj;
				}
				int targetDisplay = cam.targetDisplay;
				if ((object)this.canvas != null)
				{
					int targetDisplay2 = this.canvas.targetDisplay;
					object obj2 = targetDisplay - targetDisplay2;
					return obj2 == null;
				}
			}
		}
		goto IL_0170;
		IL_0170:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public unsafe override void DrawShapes(Camera cam)
	{
		//IL_0133: Expected O, but got I4
		//IL_017e: Expected I, but got O
		//IL_030f: Expected O, but got Ref
		//IL_04f2: Expected O, but got Ref
		//IL_038e: Expected O, but got F4
		//IL_03b2: Expected O, but got F4
		//IL_03ef: Expected O, but got F4
		//IL_041f: Expected O, but got F4
		//IL_045b: Expected I, but got O
		Canvas canvas = Canvas;
		if (!canvas.enabled)
		{
			return;
		}
		CameraType cameraType = cam.cameraType;
		if (cameraType != CameraType.Game)
		{
			return;
		}
		bool flag;
		if (this.canvas.renderMode != RenderMode.ScreenSpaceOverlay)
		{
			UnityEngine.Object obj;
			if (camUI != null)
			{
				obj = camUI;
			}
			else
			{
				Canvas canvas2 = Canvas;
				Camera worldCamera = canvas2.worldCamera;
				obj = worldCamera;
			}
			camUI = (Camera)obj;
			flag = cam == obj;
		}
		else
		{
			int targetDisplay = cam.targetDisplay;
			int targetDisplay2 = this.canvas.targetDisplay;
			object obj2 = targetDisplay - targetDisplay2;
			bool flag2 = obj2 == null;
			flag = flag2;
		}
		if (!flag)
		{
			return;
		}
		DrawCommand drawCommand = Draw.Command(cam);
		nint num = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v524 @ rax_v17 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num2 = 0;
		_ = 8;
		Transform transform;
		if (canvasRectTf != null)
		{
			transform = canvasRectTf;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Transform transform2 = default(Transform);
			transform = transform2;
		}
		canvasRectTf = (RectTransform)transform;
		ImCanvasContext imCanvasContext = canvasContext;
		Canvas canvas3 = Canvas;
		CameraType cameraType2 = cam.cameraType;
		if (cameraType2 == CameraType.SceneView)
		{
			goto IL_02dd;
		}
		Canvas canvas4 = Canvas;
		Matrix4x4 matrix4x;
		object obj4 = default(object);
		if ((object)canvas4 != null)
		{
			Camera worldCamera2 = canvas4.worldCamera;
			if (worldCamera2 != null)
			{
				Canvas canvas5 = Canvas;
				if ((object)canvas5 == null)
				{
					throw new NullReferenceException();
				}
				RenderMode renderMode = canvas5.renderMode;
				if (renderMode == RenderMode.WorldSpace)
				{
					Canvas canvas6 = Canvas;
					if ((object)canvas6 == null)
					{
						throw new NullReferenceException();
					}
					Camera worldCamera3 = canvas6.worldCamera;
					if (cam == worldCamera3)
					{
						goto IL_02dd;
					}
				}
			}
			matrix4x = GetOverlayToWorldMatrix(cam);
			UnityEngine.Object obj3 = (UnityEngine.Object)(&obj4);
			goto IL_0314;
		}
		throw new NullReferenceException();
		IL_02dd:
		if ((object)transform != null)
		{
			matrix4x = transform.localToWorldMatrix;
			UnityEngine.Object obj3 = (UnityEngine.Object)(&obj4);
			goto IL_0314;
		}
		throw new NullReferenceException();
		IL_0314:
		if (canvasContext != null)
		{
			imCanvasContext.camera = cam;
			imCanvasContext.canvas = canvas3;
			if ((object)transform != null)
			{
				imCanvasContext.canvasRect = (Rect)((RectTransform)transform).rect.m_XMin;
				Matrix4x4 worldToLocalMatrix = transform.worldToLocalMatrix;
				imCanvasContext.worldToCanvas = (Matrix4x4)worldToLocalMatrix.m00;
				_ = worldToLocalMatrix.m01;
				_ = worldToLocalMatrix.m02;
				_ = worldToLocalMatrix.m03;
				Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
				imCanvasContext.canvasToWorld = (Matrix4x4)localToWorldMatrix.m00;
				_ = localToWorldMatrix.m01;
				_ = localToWorldMatrix.m02;
				_ = localToWorldMatrix.m03;
				imCanvasContext.canvasToWorldNet = (Matrix4x4)matrix4x.m00;
				_ = matrix4x.m01;
				_ = matrix4x.m02;
				_ = matrix4x.m03;
				ImCanvasContext imCanvasContext2 = canvasContext;
				nint num3 = (nint)typeof(Draw);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1096 @ rax_v55 (Il2CppClass<Shapes.Draw>)+B8]");
				nint num4 = 0;
				Draw.matrix = imCanvasContext2.canvasToWorldNet;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1010 @ rdx_v26 (Shapes.ImCanvasContext)+C0]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1010 @ rdx_v26 (Shapes.ImCanvasContext)+D0]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1010 @ rdx_v26 (Shapes.ImCanvasContext)+E0]");
				_ = 0;
				DrawCanvasShapes(canvasContext);
				object obj5 = default(object);
				if (obj5 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	private bool DisplayAsWorldSpacePanel(Camera cam)
	{
		//IL_012d: Expected I4, but got O
		if ((object)cam != null)
		{
			CameraType cameraType = cam.cameraType;
			if (cameraType == CameraType.SceneView)
			{
				return true;
			}
			Canvas canvas = Canvas;
			if ((object)canvas != null)
			{
				Camera worldCamera = canvas.worldCamera;
				if (!(worldCamera != null))
				{
					goto IL_0119;
				}
				Canvas canvas2 = Canvas;
				if ((object)canvas2 != null)
				{
					RenderMode renderMode = canvas2.renderMode;
					if (renderMode != RenderMode.WorldSpace)
					{
						goto IL_0119;
					}
					Canvas canvas3 = Canvas;
					if ((object)canvas3 != null)
					{
						Camera worldCamera2 = canvas3.worldCamera;
						return cam == worldCamera2;
					}
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_0119:
		return false;
	}

	private unsafe Matrix4x4 GetOverlayToWorldMatrix(Camera cam)
	{
		//IL_0008: Expected O, but got Ref
		//IL_00d3: Expected F4, but got I4
		//IL_00dc: Expected F4, but got I4
		//IL_0272: Expected F4, but got I4
		//IL_027b: Expected F4, but got I4
		//IL_0308: Expected F4, but got I4
		//IL_0311: Expected F4, but got I4
		//IL_01c2: Expected F4, but got I4
		//IL_01cb: Expected F4, but got I4
		//IL_0362: Expected O, but got Ref
		//IL_038c: Expected native int or pointer, but got O
		//IL_03a6: Expected native int or pointer, but got O
		//IL_03ba: Expected native int or pointer, but got O
		//IL_03c8: Expected native int or pointer, but got O
		//IL_0416: Expected O, but got Ref
		//IL_0424: Expected O, but got Ref
		//IL_043d: Expected O, but got F4
		//IL_0438: Expected native int or pointer, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		bool flag = (object)cam == null;
		Camera camera = cam;
		ImmediateModeCanvas immediateModeCanvas = this;
		NullReferenceException ex;
		if (!flag)
		{
			float nearClipPlane = cam.nearClipPlane;
			float farClipPlane = cam.farClipPlane;
			Transform transform = cam.transform;
			bool flag2 = (object)transform == null;
			camera = cam;
			immediateModeCanvas = null;
			if (!flag2)
			{
				_ = transform.forward.x;
				float num = default(float);
				Vector3 vector = transform.TransformPoint(0f, 0f, num);
				farClipPlane = vector.x;
				_ = vector.x;
				Canvas canvas = Canvas;
				bool flag3 = (object)canvas == null;
				float num2 = 0f;
				float num3 = 0f;
				camera = null;
				immediateModeCanvas = null;
				if (!flag3)
				{
					Transform transform2 = canvas.transform;
					bool flag4 = (object)transform2 == null;
					RectTransform rectTransform = null;
					if (!flag4)
					{
						bool flag5 = (object)transform2.GetType() != typeof(RectTransform);
						Transform transform3 = null;
						if (!flag5)
						{
							transform3 = transform2;
						}
						bool flag6 = (object)transform3 == null;
						rectTransform = (RectTransform)transform3;
						num2 = 0f;
						num3 = 0f;
						camera = null;
						immediateModeCanvas = (ImmediateModeCanvas)(object)typeof(RectTransform);
						ex = (NullReferenceException)(object)transform2;
						if (flag6)
						{
							goto IL_0442;
						}
					}
					if (!cam.orthographic)
					{
						farClipPlane = cam.fieldOfView;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm6,xmm0\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,qword ptr [182206E70h]\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,qword ptr [182206D18h]\"");
						bool flag7 = (object)rectTransform == null;
						num2 = 0f;
						num3 = 0f;
						camera = null;
						immediateModeCanvas = null;
						if (flag7)
						{
							goto IL_02af;
						}
						Vector2 sizeDelta = rectTransform.sizeDelta;
						double num4 = Math.Tan(0.0);
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm7\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm1\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm2,xmm0\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm2\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm2,xmm0\"");
					}
					else
					{
						farClipPlane = cam.orthographicSize;
						bool flag8 = (object)rectTransform == null;
						num2 = 0f;
						num3 = 0f;
						camera = null;
						immediateModeCanvas = null;
						if (flag8)
						{
							goto IL_02af;
						}
						Vector2 sizeDelta2 = rectTransform.sizeDelta;
					}
					Vector3 right = transform.right;
					Vector3 up = transform.up;
					Vector4 column = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-65]");
					_ = 0;
					_ = right.x;
					_ = up.x;
					Matrix4x4 matrix4x = default(Matrix4x4);
					((Matrix4x4*)(nint)matrix4x)->m00 = 0f;
					_ = 0;
					_ = 0;
					((Matrix4x4*)(nint)matrix4x)->m01 = 0f;
					_ = 0;
					((Matrix4x4*)(nint)matrix4x)->m02 = 0f;
					((Matrix4x4*)(nint)matrix4x)->m03 = 0f;
					_ = vector.z;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-69]");
					_ = 0;
					_ = 1065353216;
					Vector4 column2 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
					Vector4 column3 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
					*(Matrix4x4*)(nint)matrix4x = new Matrix4x4(column3, column2, column, (Vector4)num);
					return matrix4x;
				}
			}
		}
		goto IL_02af;
		IL_02af:
		ex = new NullReferenceException();
		goto IL_0442;
		IL_0442:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		Matrix4x4 result = default(Matrix4x4);
		return result;
	}

	public virtual void DrawCanvasShapes(ImCanvasContext ctx)
	{
	}

	public ImmediateModeCanvas()
	{
		List<ImmediateModePanel> list = new List<ImmediateModePanel>();
		panels = list;
		((MonoBehaviour)this)._002Ector();
	}

	static ImmediateModeCanvas()
	{
		ImCanvasContext imCanvasContext = new ImCanvasContext();
		canvasContext = imCanvasContext;
	}
}
