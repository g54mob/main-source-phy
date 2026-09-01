using UnityEngine;

public class AlignIconAround : MonoBehaviour
{
	public Transform leftIcon;

	public Transform rightIcon;

	public DynamicText[] titles;

	public SlicedMesh upperLine;

	public SlicedMesh lowerLine;

	private Bounds combinedBounds;

	public float iconOffsetx = 0.5f;

	public float lineLengthOffset = 0.5f;

	private void Start()
	{
		Camera component = GameObject.Find("HUD Cam").GetComponent<Camera>();
		for (int i = 0; i < titles.Length; i++)
		{
			if (i == 0)
			{
				combinedBounds = titles[i].GetComponent<MeshRenderer>().bounds;
			}
			else
			{
				combinedBounds.Encapsulate(titles[i].GetComponent<MeshRenderer>().bounds);
			}
			if (component != null)
			{
				titles[i].cam = component;
			}
		}
		SetIcons();
	}

	protected void SetIcons()
	{
		float x = combinedBounds.max.x;
		float x2 = combinedBounds.min.x;
		leftIcon.position = new Vector3(x2 - iconOffsetx, combinedBounds.center.y, combinedBounds.center.z);
		rightIcon.position = new Vector3(x + iconOffsetx - 0.055f, combinedBounds.center.y, combinedBounds.center.z);
		upperLine.SetMeshSizeAspect(combinedBounds.size.x - lineLengthOffset);
		lowerLine.SetMeshSizeAspect(combinedBounds.size.x - lineLengthOffset);
	}
}
