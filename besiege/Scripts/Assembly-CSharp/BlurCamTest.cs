using UnityEngine;

public class BlurCamTest : MonoBehaviour
{
	public Transform _target;

	public Transform targetScroll;

	public Camera targetsCamera;

	public Transform upperMask;

	public Transform lowerMask;

	protected Camera myCamera;

	protected bool setup;

	protected GameObject scrollGO;

	protected float maxY;

	protected float minY;

	private Transform lowerRight;

	public bool clampToBottomBar;

	public Camera BlurCam
	{
		get
		{
			if (myCamera == null)
			{
				Start();
			}
			return myCamera;
		}
	}

	public virtual Object target
	{
		get
		{
			return _target;
		}
		set
		{
			_target = value as Transform;
			Start();
			bool flag = value != null;
			myCamera.enabled = !StatMaster.hudHidden && flag;
		}
	}

	protected virtual void Start()
	{
		if (!setup)
		{
			if (clampToBottomBar)
			{
				lowerRight = GameObject.FindWithTag("lowerRight").transform;
			}
			scrollGO = ((!targetScroll) ? null : targetScroll.gameObject);
			myCamera = GetComponent<Camera>();
			setup = true;
			if ((bool)upperMask)
			{
				maxY = upperMask.position.y - upperMask.lossyScale.y / 2f;
			}
			if ((bool)lowerMask)
			{
				minY = lowerMask.position.y + lowerMask.lossyScale.y / 2f;
			}
		}
	}

	protected virtual void LateUpdate()
	{
		if (_target == null)
		{
			if (myCamera.enabled)
			{
				myCamera.enabled = false;
			}
		}
		else if (myCamera.enabled && !StatMaster.hudHidden)
		{
			float num = _target.lossyScale.x / 2f;
			float num2 = _target.lossyScale.y / 2f;
			float x = _target.position.x - num;
			float x2 = ((!scrollGO || !scrollGO.activeInHierarchy) ? (_target.position.x + num) : (targetScroll.position.x + targetScroll.lossyScale.x / 2f));
			float num3 = _target.position.y + num2;
			float num4 = _target.position.y - num2;
			if (maxY != 0f && num3 > maxY)
			{
				num3 = maxY;
			}
			if (minY != 0f && num4 < minY)
			{
				num4 = minY;
			}
			if (clampToBottomBar)
			{
				num4 = Mathf.Max(num4, lowerRight.position.y);
			}
			Vector2 vector = targetsCamera.WorldToViewportPoint(new Vector3(x, num3));
			Vector2 vector2 = targetsCamera.WorldToViewportPoint(new Vector3(x2, num4));
			myCamera.rect = new Rect(vector.x, vector2.y, vector2.x - vector.x, vector.y - vector2.y);
		}
	}
}
