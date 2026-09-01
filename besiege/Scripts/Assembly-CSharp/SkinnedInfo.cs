using UnityEngine;

public class SkinnedInfo : BasicInfo
{
	public SkinnedMeshRenderer render;

	protected override void RetrieveBounds()
	{
		if (!gotBoundsThisFrame)
		{
			_updatedBounds = render.bounds;
			gotBoundsThisFrame = true;
		}
	}

	protected override void RetrieveDefaultBounds()
	{
		if (render == null)
		{
			Debug.LogError("Missing Meshrendere in basic info from: " + base.transform.name);
			_defaultBounds = new Bounds(new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f));
			_MaxAreaSize = 1f;
		}
		else
		{
			_defaultBounds = render.bounds;
		}
	}

	protected override void GetMeshRendererReference()
	{
	}
}
