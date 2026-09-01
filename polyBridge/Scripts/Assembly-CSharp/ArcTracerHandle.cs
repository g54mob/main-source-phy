using FluffyUnderware.Curvy;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vectrosity;

public class ArcTracerHandle : MonoBehaviour
{
	public GameObject handleSprite;

	public VectorObject2D handleLine;

	public Image m_Image;

	public bool handleIn;

	private ArcTracer arcTracer;

	private CurvySplineSegment controlPoint;

	private float abDistance;

	private Color m_OriginalColor;

	private readonly Color m_HoverColor = Color.white;

	private void Awake()
	{
		arcTracer = base.gameObject.GetComponentInParent<ArcTracer>();
		if (arcTracer == null)
		{
			Debug.LogError("ArcTracer component missing from parent");
		}
		controlPoint = base.gameObject.GetComponent<CurvySplineSegment>();
		if (controlPoint == null)
		{
			Debug.LogError("CurvySplineSegment component missing from parent");
		}
		m_OriginalColor = m_Image.color;
		if (handleIn)
		{
			handleSprite.GetComponent<RectTransform>().anchoredPosition = controlPoint.HandleIn;
		}
		else
		{
			handleSprite.GetComponent<RectTransform>().anchoredPosition = controlPoint.HandleOut;
		}
	}

	private void Start()
	{
		UpdateLine();
	}

	public void SetHandleVisible(bool value)
	{
		handleSprite.SetActive(value);
		handleLine.enabled = value;
	}

	public bool IsEnabled()
	{
		return handleLine.enabled;
	}

	public void UpdateLine()
	{
		handleLine.vectorLine.points2[0] = Vector2.zero;
		if (handleIn)
		{
			handleLine.vectorLine.points2[1] = controlPoint.HandleIn - controlPoint.HandleIn.normalized * 0.3f;
		}
		else
		{
			handleLine.vectorLine.points2[1] = controlPoint.HandleOut - controlPoint.HandleOut.normalized * 0.3f;
		}
		handleLine.vectorLine.Draw();
	}

	public void MoveHandleEvent(BaseEventData data)
	{
		if (!data.used)
		{
			data.Use();
			PointerEventData pointerEventData = (PointerEventData)data;
			Vector2 vector = Cameras.MainCamera().ScreenToWorldPoint(pointerEventData.position);
			if (handleIn)
			{
				arcTracer.HandleBeingMovedBy(Utils.V3toV2(controlPoint.HandleInPosition) - vector, this);
			}
			else
			{
				arcTracer.HandleBeingMovedBy(Utils.V3toV2(controlPoint.HandleOutPosition) - vector, this);
			}
		}
	}

	public void MoveHandleTo(Vector2 pos, bool worldPos = true)
	{
		if (BridgeTrace.m_SnapToGrid)
		{
			pos = GameGrid.SnapPosToGrid(pos);
		}
		if (handleIn)
		{
			if (worldPos)
			{
				controlPoint.HandleInPosition = Utils.V2toV3(pos);
			}
			else
			{
				controlPoint.HandleIn = Utils.V2toV3(pos);
			}
			handleSprite.GetComponent<RectTransform>().anchoredPosition = controlPoint.HandleIn;
		}
		else
		{
			if (worldPos)
			{
				controlPoint.HandleOutPosition = Utils.V2toV3(pos);
			}
			else
			{
				controlPoint.HandleOut = Utils.V2toV3(pos);
			}
			handleSprite.GetComponent<RectTransform>().anchoredPosition = controlPoint.HandleOut;
		}
		UpdateLine();
	}

	public Vector2 MoveHandleBy(Vector2 delta)
	{
		if (handleIn)
		{
			controlPoint.HandleIn -= Utils.V2toV3(delta);
			if (BridgeTrace.m_SnapToGrid)
			{
				controlPoint.HandleIn = GameGrid.SnapPosToGrid(controlPoint.HandleIn);
			}
			handleSprite.GetComponent<RectTransform>().anchoredPosition = controlPoint.HandleIn;
			UpdateLine();
			return delta;
		}
		controlPoint.HandleOut -= Utils.V2toV3(delta);
		if (BridgeTrace.m_SnapToGrid)
		{
			controlPoint.HandleOut = GameGrid.SnapPosToGrid(controlPoint.HandleOut);
		}
		handleSprite.GetComponent<RectTransform>().anchoredPosition = controlPoint.HandleOut;
		UpdateLine();
		return delta;
	}

	private void CapHandleLength()
	{
		if (handleIn)
		{
			if (controlPoint.HandleIn.magnitude > abDistance * 0.5f)
			{
				controlPoint.HandleIn = controlPoint.HandleIn.normalized * abDistance * 0.5f;
			}
			handleSprite.GetComponent<RectTransform>().anchoredPosition = controlPoint.HandleIn;
		}
		else
		{
			if (controlPoint.HandleOut.magnitude > abDistance * 0.5f)
			{
				controlPoint.HandleOut = controlPoint.HandleOut.normalized * abDistance * 0.5f;
			}
			handleSprite.GetComponent<RectTransform>().anchoredPosition = controlPoint.HandleOut;
		}
	}

	public Vector2 GetHandlePosition()
	{
		if (handleIn)
		{
			return controlPoint.HandleIn;
		}
		return controlPoint.HandleOut;
	}

	public Vector2 GetHandlePositionWorld()
	{
		if (handleIn)
		{
			return controlPoint.HandleInPosition;
		}
		return controlPoint.HandleOutPosition;
	}

	public void SetABDistance(float val)
	{
		abDistance = val;
	}

	public void UpdateColor(bool hover)
	{
		m_Image.color = (hover ? m_HoverColor : m_OriginalColor);
	}
}
