using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Blocks/Ghost/GhostTrigger")]
public class GhostTrigger : MonoBehaviour
{
	public static bool isTouching;

	[HideInInspector]
	public int[] HUDLayers = new int[5] { 9, 13, 19, 21, 23 };

	public int touchingCount;

	public GhostMaterialController materialCode;

	[HideInInspector]
	public bool hasMaterialCode;

	public List<int> layersToIgnore = new List<int> { 16 };

	public int overlayBlockLayer = 27;

	private Vector3 pos = Vector3.zero;

	protected virtual void Awake()
	{
		hasMaterialCode = materialCode != null;
	}

	private void Update()
	{
		if (base.transform.position != pos)
		{
			Check();
			pos = base.transform.position;
		}
		else if (!isTouching && touchingCount > 0)
		{
			Check();
		}
		if (!hasMaterialCode)
		{
			return;
		}
		if (isTouching || (!StatMaster.Mode.allowIntersection && SingleInstanceFindOnly<AddPiece>.Instance.OutOfBounds))
		{
			materialCode.SetRed();
			if (InputManager.LeftMouseButton())
			{
				IntersectWarning.WarningFromWorldPos(base.transform.position);
			}
		}
		else
		{
			materialCode.SetNormal();
		}
	}

	private void OnDisable()
	{
		touchingCount = 0;
		Check();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!ColliderIsIgnored(other))
		{
			touchingCount++;
			Check();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (touchingCount <= 0 && !ColliderIsIgnored(other))
		{
			touchingCount++;
			Check();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!ColliderIsIgnored(other))
		{
			if (touchingCount > 0)
			{
				touchingCount--;
			}
			Check();
		}
	}

	protected bool ColliderIsIgnored(Collider col)
	{
		if (col == null)
		{
			return true;
		}
		if (col.CompareTag("DoubleBlock"))
		{
			return true;
		}
		if (col.gameObject.layer == overlayBlockLayer)
		{
			return true;
		}
		if (col.gameObject.name == "Adding Point" && col is BoxCollider && (col as BoxCollider).size.y != 0f)
		{
			return true;
		}
		if (col.gameObject.layer == 12 && col.isTrigger)
		{
			return true;
		}
		for (int i = 0; i < HUDLayers.Length; i++)
		{
			if (col.gameObject.layer == HUDLayers[i])
			{
				return true;
			}
		}
		for (int j = 0; j < layersToIgnore.Count; j++)
		{
			if (col.gameObject.layer == layersToIgnore[j])
			{
				return true;
			}
		}
		return false;
	}

	public virtual void Check()
	{
		isTouching = !StatMaster.Mode.allowIntersection && touchingCount > 0;
	}
}
