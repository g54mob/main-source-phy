using UnityEngine;

public class DrawLineRender : SimBehaviour
{
	public Transform targetObj;

	public Transform myTransform;

	public LineRenderer lineRendy;

	public bool destroyIfTargetNull;

	public bool destroyOnSimulate;

	public bool distanceBasedTiling;

	private Machine machine;

	protected override void Awake()
	{
		base.Awake();
		lineRendy.useWorldSpace = true;
	}

	private void Update()
	{
		if (!base.isSimulating)
		{
			if (distanceBasedTiling)
			{
				float num = Vector3.Distance(myTransform.position, targetObj.position);
				lineRendy.material.mainTextureScale = new Vector2(num * 3f, 1f);
			}
		}
		else
		{
			if (destroyOnSimulate)
			{
				DestroyAll();
				return;
			}
			if (destroyIfTargetNull && targetObj == null)
			{
				DestroyAll();
				return;
			}
		}
		lineRendy.SetPosition(0, myTransform.position);
		lineRendy.SetPosition(1, targetObj.position);
	}

	private void DestroyAll()
	{
		Object.Destroy(lineRendy);
		Object.Destroy(this);
	}
}
