using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class VRRaycaster : BaseRaycaster
{
	private struct RaycastHitData
	{
		public Graphic graphic { get; }

		public Vector3 worldPosition { get; }

		public Vector3 worldNormal { get; }

		public Vector2 screenPosition { get; }

		public float distance { get; }

		public RaycastHitData(Graphic graphic, Vector3 worldPosition, Vector3 worldNormal, Vector2 screenPosition, float distance)
		{
			this.graphic = graphic;
			this.worldPosition = worldPosition;
			this.worldNormal = worldNormal;
			this.screenPosition = screenPosition;
			this.distance = distance;
		}
	}

	public static bool isOcclusionEnabled = true;

	public static List<Canvas> focusedCanvases;

	private const bool IGNORE_REVERSED_GRAPHICS = true;

	private const float MAX_OCCLUSION_RAYCAST_DISTANCE = 1000f;

	private static readonly Vector3[] RECT_TRANSFORM_CORNERS = new Vector3[4];

	private static readonly NonAllocSortBuffer<RaycastHit> HIT_BUFFER = new NonAllocSortBuffer<RaycastHit>(64, (RaycastHit hit1, RaycastHit hit2) => hit1.distance.CompareTo(hit2.distance));

	[NonSerialized]
	public List<Collider> colliderWhitelist = new List<Collider>();

	private readonly List<RaycastHitData> graphicsHitByRay = new List<RaycastHitData>();

	private Canvas canvasReference;

	public override int sortOrderPriority => canvas.sortingOrder;

	public override int renderOrderPriority => canvas.renderOrder;

	public override Camera eventCamera
	{
		get
		{
			Canvas canvas = this.canvas;
			if (!(canvas != null))
			{
				return null;
			}
			return canvas.worldCamera;
		}
	}

	private Canvas canvas
	{
		get
		{
			if (canvasReference != null)
			{
				return canvasReference;
			}
			canvasReference = GetComponent<Canvas>();
			return canvasReference;
		}
	}

	public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
	{
		if (eventData is ExtendedPointerEventData extendedPointerEventData && !(canvas == null) && !(eventCamera == null))
		{
			Ray ray = new Ray(extendedPointerEventData.trackedDevicePosition, extendedPointerEventData.trackedDeviceOrientation * Vector3.forward);
			calculateGraphicsHitByRay(ray);
			appendValidGraphicHits(ray, resultAppendList);
		}
	}

	private void calculateGraphicsHitByRay(Ray ray)
	{
		graphicsHitByRay.Clear();
		if (focusedCanvases != null && !focusedCanvases.Contains(canvas))
		{
			bool flag = false;
			foreach (Canvas focusedCanvase in focusedCanvases)
			{
				if (canvas.transform.IsChildOf(focusedCanvase.transform))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return;
			}
		}
		IList<Graphic> graphicsForCanvas = GraphicRegistry.GetGraphicsForCanvas(canvas);
		for (int i = 0; i < graphicsForCanvas.Count; i++)
		{
			Graphic graphic = graphicsForCanvas[i];
			if (graphic.depth != -1 && graphic.raycastTarget && rayIntersectsRectTransform(graphic.rectTransform, ray, out var worldPosition, out var distance))
			{
				Vector2 vector = eventCamera.WorldToScreenPoint(worldPosition);
				if (graphic.Raycast(vector, eventCamera))
				{
					Vector3 worldNormal = -graphic.transform.forward;
					graphicsHitByRay.Add(new RaycastHitData(graphic, worldPosition, worldNormal, vector, distance));
				}
			}
		}
	}

	private static bool rayIntersectsRectTransform(RectTransform transform, Ray ray, out Vector3 worldPosition, out float distance)
	{
		Vector3[] rECT_TRANSFORM_CORNERS = RECT_TRANSFORM_CORNERS;
		transform.GetWorldCorners(rECT_TRANSFORM_CORNERS);
		if (new Plane(rECT_TRANSFORM_CORNERS[0], rECT_TRANSFORM_CORNERS[1], rECT_TRANSFORM_CORNERS[2]).Raycast(ray, out var enter))
		{
			Vector3 point = ray.GetPoint(enter);
			Vector3 rhs = rECT_TRANSFORM_CORNERS[3] - rECT_TRANSFORM_CORNERS[0];
			Vector3 rhs2 = rECT_TRANSFORM_CORNERS[1] - rECT_TRANSFORM_CORNERS[0];
			float num = Vector3.Dot(point - rECT_TRANSFORM_CORNERS[0], rhs);
			if (Vector3.Dot(point - rECT_TRANSFORM_CORNERS[0], rhs2) >= 0f && num >= 0f)
			{
				Vector3 rhs3 = rECT_TRANSFORM_CORNERS[1] - rECT_TRANSFORM_CORNERS[2];
				Vector3 rhs4 = rECT_TRANSFORM_CORNERS[3] - rECT_TRANSFORM_CORNERS[2];
				float num2 = Vector3.Dot(point - rECT_TRANSFORM_CORNERS[2], rhs3);
				float num3 = Vector3.Dot(point - rECT_TRANSFORM_CORNERS[2], rhs4);
				if (num2 >= 0f && num3 >= 0f)
				{
					worldPosition = point;
					distance = enter;
					return true;
				}
			}
		}
		worldPosition = Vector3.zero;
		distance = 0f;
		return false;
	}

	private void appendValidGraphicHits(Ray ray, List<RaycastResult> resultAppendList)
	{
		if (graphicsHitByRay.Count == 0)
		{
			return;
		}
		float num = 1000f;
		if (isOcclusionEnabled && colliderWhitelist != null)
		{
			int num2 = Physics.RaycastNonAlloc(ray, HIT_BUFFER.elements);
			HIT_BUFFER.sort(num2);
			for (int i = 0; i < num2; i++)
			{
				if (!colliderWhitelist.Contains(HIT_BUFFER[i].collider))
				{
					num = HIT_BUFFER[i].distance;
					break;
				}
			}
		}
		foreach (RaycastHitData item in graphicsHitByRay)
		{
			bool flag = item.distance < num;
			GameObject gameObject = item.graphic.gameObject;
			if (flag & (Vector3.Dot(ray.direction, -item.worldNormal) > 0f))
			{
				resultAppendList.Add(new RaycastResult
				{
					gameObject = gameObject,
					module = this,
					distance = item.distance,
					index = resultAppendList.Count,
					depth = item.graphic.depth,
					worldPosition = item.worldPosition,
					worldNormal = item.worldNormal,
					screenPosition = item.screenPosition
				});
			}
		}
	}
}
