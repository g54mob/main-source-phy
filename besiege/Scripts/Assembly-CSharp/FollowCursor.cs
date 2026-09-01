using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
	public enum State
	{
		Cursor = 0,
		Ghost = 1,
		Selection = 2,
		BottomRight = 3
	}

	public State follow;

	public Camera cam;

	private Renderer[] toolRenderers;

	private Transform bottomRight;

	private TriumphBarsLerpIn triumph;

	private void Start()
	{
		toolRenderers = AdvancedBlockEditor.Instance.ToolTransform.GetComponentsInChildren<Renderer>(true);
		bottomRight = GameObject.FindGameObjectWithTag("lowerRight").transform;
		triumph = Object.FindObjectOfType<TriumphBarsLerpIn>();
	}

	private void Update()
	{
		Vector3 vector = Vector3.one * -1000f;
		switch (follow)
		{
		case State.Cursor:
			vector = cam.ScreenToWorldPoint(Input.mousePosition);
			break;
		case State.Ghost:
			if ((bool)SingleInstanceFindOnly<AddPiece>.Instance.ActiveGhost)
			{
				vector = SingleInstanceFindOnly<AddPiece>.Instance.ActiveGhost.position;
				vector = Camera.main.WorldToScreenPoint(vector);
				vector = cam.ScreenToWorldPoint(vector);
			}
			break;
		case State.Selection:
			vector = CalculateSelectionPosition();
			break;
		case State.BottomRight:
		{
			float num = 0f;
			if (triumph.NextZoneShown)
			{
				num += 0.8f;
			}
			if (StatMaster.isMP && !StatMaster.IsLevelEditorOnly)
			{
				num += 0.6f;
			}
			vector = bottomRight.position + new Vector3(0.1f, 0.35f + num, 0f);
			break;
		}
		}
		base.transform.position = new Vector3(vector.x, vector.y, 23f);
	}

	private Vector3 CalculateSelectionPosition()
	{
		List<BlockBehaviour> selectedBlocks = AddPiece.SelectedBlocks;
		Vector2 rightTopMostScreenPos = -Vector2.one;
		Bounds bounds = default(Bounds);
		for (int i = 0; i < selectedBlocks.Count; i++)
		{
			Bounds bounds2 = selectedBlocks[i].MeshRenderer.bounds;
			Vector3 center = bounds2.center;
			Vector2 vector = Camera.main.WorldToScreenPoint(center);
			if (ShouldReplaceRightTopMost(rightTopMostScreenPos, vector))
			{
				bounds = bounds2;
				rightTopMostScreenPos = vector;
			}
		}
		for (int j = 0; j < toolRenderers.Length; j++)
		{
			Bounds bounds3 = toolRenderers[j].bounds;
			Vector3 center2 = bounds3.center;
			Vector2 vector2 = Camera.main.WorldToScreenPoint(center2);
			if (ShouldReplaceRightTopMost(rightTopMostScreenPos, vector2))
			{
				bounds = bounds3;
				rightTopMostScreenPos = vector2;
			}
		}
		Vector2 vector3 = Camera.main.WorldToScreenPoint(bounds.min);
		Vector2 vector4 = Camera.main.WorldToScreenPoint(bounds.max);
		Vector2 vector5 = new Vector2(Mathf.Max(vector3.x, vector4.x), Mathf.Max(vector3.y, vector4.y));
		return cam.ScreenToWorldPoint(vector5);
	}

	private bool ShouldReplaceRightTopMost(Vector2 rightTopMostScreenPos, Vector2 screenPos)
	{
		bool flag = Mathf.Abs(rightTopMostScreenPos.x - screenPos.x) < 20f;
		bool flag2 = Mathf.Abs(rightTopMostScreenPos.y - screenPos.y) < 20f;
		bool flag3 = screenPos.x > rightTopMostScreenPos.x;
		bool flag4 = screenPos.y > rightTopMostScreenPos.y;
		return (flag3 && (flag2 || flag4)) || (flag4 && flag);
	}
}
