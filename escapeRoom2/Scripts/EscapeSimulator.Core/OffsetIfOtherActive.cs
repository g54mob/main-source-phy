using UnityEngine;

public class OffsetIfOtherActive : MonoBehaviour
{
	public Vector2 offset;

	public GameObject other;

	private RectTransform rt;

	private Vector2 initialOffset;

	private void Start()
	{
		rt = GetComponent<RectTransform>();
		initialOffset = new Vector2(rt.offsetMin.x, rt.offsetMax.x);
	}

	private void Update()
	{
		SetRight(other.activeInHierarchy ? offset.x : initialOffset.x);
		SetLeft(other.activeInHierarchy ? offset.y : initialOffset.y);
	}

	public void SetLeft(float left)
	{
		rt.offsetMin = new Vector2(left, rt.offsetMin.y);
	}

	public void SetRight(float right)
	{
		rt.offsetMax = new Vector2(0f - right, rt.offsetMax.y);
	}
}
