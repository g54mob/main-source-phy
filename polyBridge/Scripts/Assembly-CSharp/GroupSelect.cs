using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class GroupSelect
{
	private static GameObject m_SelectionBox;

	private static SpriteRenderer m_SelectionBoxSpriteRenderer;

	private static VectorLine m_SelectionBoxOutline;

	public static void OnLayoutLoaded()
	{
		if (m_SelectionBoxOutline != null)
		{
			VectorLine.Destroy(ref m_SelectionBoxOutline);
			m_SelectionBoxOutline = null;
		}
	}

	public static void Start(Vector2 cursorScreenPos)
	{
		if (!m_SelectionBox)
		{
			m_SelectionBox = Object.Instantiate(Prefabs.m_Instance.m_SelectionBox);
			m_SelectionBoxSpriteRenderer = m_SelectionBox.GetComponentInChildren<SpriteRenderer>();
			Object.DontDestroyOnLoad(m_SelectionBox);
		}
		m_SelectionBox.transform.position = Utils.GetWorldPointFromScreenPos(cursorScreenPos);
		m_SelectionBox.transform.localScale = Vector3.zero;
		m_SelectionBoxSpriteRenderer.color = GameUI.GroupSelectionBoxColor();
		m_SelectionBox.gameObject.SetActive(value: true);
		if (m_SelectionBoxOutline == null)
		{
			m_SelectionBoxOutline = new VectorLine("SelectionBoxOutline", new List<Vector2>(8), 1f, LineType.Discrete, Joins.Weld);
			m_SelectionBoxOutline.layer = Utils.DEFAULT_LAYER;
			m_SelectionBoxOutline.color = GameUI.GroupSelectionBoxOutlineColor();
		}
		m_SelectionBoxOutline.active = true;
	}

	public static void UpdateXY(Vector2 cursorScreenPos)
	{
		if ((bool)m_SelectionBox && m_SelectionBox.activeInHierarchy && m_SelectionBoxOutline != null)
		{
			Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(cursorScreenPos);
			worldPointFromScreenPos.z = 0f;
			Vector3 vector = worldPointFromScreenPos - m_SelectionBox.transform.position;
			m_SelectionBox.transform.localScale = new Vector3(vector.x, 0f - vector.y, 1f);
			m_SelectionBoxSpriteRenderer.transform.SetParent(m_SelectionBox.transform);
			m_SelectionBoxSpriteRenderer.transform.rotation = Quaternion.identity;
			m_SelectionBoxSpriteRenderer.transform.localPosition = new Vector3(0.5f, -0.5f, 0f);
			m_SelectionBoxSpriteRenderer.transform.localScale = Vector3.one;
			Vector3 v = Cameras.MainCamera().WorldToScreenPoint(m_SelectionBox.transform.position);
			m_SelectionBoxOutline.MakeRect(Utils.V3toV2(v), cursorScreenPos);
			m_SelectionBoxOutline.Draw();
		}
	}

	public static void UpdateXZ(Vector2 cursorScreenPos)
	{
		if ((bool)m_SelectionBox && m_SelectionBox.activeInHierarchy && m_SelectionBoxOutline != null)
		{
			Vector3 vector = Cameras.MainCamera().ScreenToWorldPoint(cursorScreenPos) - m_SelectionBox.transform.position;
			m_SelectionBox.transform.localScale = new Vector3(vector.x, 1f, 0f - vector.z);
			m_SelectionBoxSpriteRenderer.transform.SetParent(null);
			m_SelectionBoxSpriteRenderer.transform.eulerAngles = new Vector3(90f, 0f, 0f);
			m_SelectionBoxSpriteRenderer.transform.position = m_SelectionBox.transform.position + new Vector3(vector.x / 2f, 0f, vector.z / 2f);
			m_SelectionBoxSpriteRenderer.transform.localScale = new Vector3(vector.x, 0f - vector.z, 1f);
			Vector3 v = Cameras.MainCamera().WorldToScreenPoint(m_SelectionBox.transform.position);
			m_SelectionBoxOutline.MakeRect(Utils.V3toV2(v), cursorScreenPos);
			m_SelectionBoxOutline.Draw();
		}
	}

	public static bool IsActive()
	{
		if ((bool)m_SelectionBox && m_SelectionBox.gameObject.activeInHierarchy)
		{
			if (!(Mathf.Abs(m_SelectionBox.transform.localScale.x) > 0.05f))
			{
				return Mathf.Abs(m_SelectionBox.transform.localScale.y) > 0.05f;
			}
			return true;
		}
		return false;
	}

	public static void Cancel()
	{
		if ((bool)m_SelectionBox)
		{
			m_SelectionBoxSpriteRenderer.transform.SetParent(m_SelectionBox.transform);
			m_SelectionBox.gameObject.SetActive(value: false);
			m_SelectionBox.transform.localScale = Vector3.zero;
		}
		if (m_SelectionBoxOutline != null)
		{
			m_SelectionBoxOutline.active = false;
		}
	}

	public static Rect GetRect()
	{
		Vector3 position = m_SelectionBox.transform.position;
		Vector3 vector = position + new Vector3(m_SelectionBox.transform.localScale.x, 0f - m_SelectionBox.transform.localScale.y, 0f);
		Vector2 vector2 = (position + vector) / 2f;
		Vector2 vector3 = new Vector3(Mathf.Abs(position.x - vector.x), Mathf.Abs(position.y - vector.y));
		return new Rect(vector2 - vector3 / 2f, vector3);
	}

	public static Rect GetRectXZ()
	{
		Vector3 position = m_SelectionBox.transform.position;
		Vector3 vector = position + new Vector3(m_SelectionBox.transform.localScale.x, 0f, 0f - m_SelectionBox.transform.localScale.z);
		Vector3 vector2 = (position + vector) / 2f;
		Vector3 vector3 = new Vector3(Mathf.Abs(position.x - vector.x), Mathf.Abs(position.y - vector.y), Mathf.Abs(position.z - vector.z));
		Vector2 vector4 = new Vector2(vector2.x, vector2.z);
		Vector2 vector5 = new Vector2(vector3.x, vector3.z);
		return new Rect(vector4 - vector5, vector5);
	}

	public static bool OverlapsSelectionRect(Bounds b)
	{
		Rect other = new Rect(b.min, b.size);
		if (GetRect().Overlaps(other))
		{
			return true;
		}
		return false;
	}

	public static bool OverlapsSelectionRectXZ(Bounds b)
	{
		Vector3 center = m_SelectionBox.transform.position + new Vector3(m_SelectionBox.transform.localScale.x / 2f, 0f, (0f - m_SelectionBox.transform.localScale.z) / 2f);
		Bounds bounds = new Bounds(center, new Vector3(Mathf.Abs(m_SelectionBox.transform.localScale.x), float.MaxValue, Mathf.Abs(m_SelectionBox.transform.localScale.z)));
		if (b.Intersects(bounds))
		{
			return true;
		}
		return false;
	}
}
