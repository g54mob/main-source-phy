using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("UI/UI Mask")]
public class UIMask : MonoBehaviour
{
	public static Dictionary<int, UIMask> Active = new Dictionary<int, UIMask>();

	public int maskIndex = -1;

	public Transform area;

	public bool isOverMask;

	private void Awake()
	{
		if (maskIndex != -1)
		{
			if (Active.ContainsKey(maskIndex))
			{
				Active[maskIndex] = this;
			}
			else
			{
				Active.Add(maskIndex, this);
			}
		}
	}

	private void OnDestroy()
	{
		if (Active.ContainsKey(maskIndex) && Active[maskIndex] == this)
		{
			Active.Remove(maskIndex);
		}
	}

	public static bool InsideMask(int index, Vector3 position)
	{
		if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
		{
			return false;
		}
		if (index == -1)
		{
			return true;
		}
		if (Active.ContainsKey(index) && Active[index] != null)
		{
			return Active[index].InsideMask(position);
		}
		return true;
	}

	public bool InsideMask(Vector3 position)
	{
		Vector3 vector = area.position + area.lossyScale / 2f;
		Vector3 vector2 = area.position - area.lossyScale / 2f;
		if (position.x > vector2.x && position.y > vector2.y && position.x < vector.x && position.y < vector.y)
		{
			return true;
		}
		return false;
	}
}
