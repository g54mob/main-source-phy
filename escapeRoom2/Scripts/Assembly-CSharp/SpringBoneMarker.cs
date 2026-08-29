using UnityChan;
using UnityEngine;

public class SpringBoneMarker : MonoBehaviour
{
	public bool CheckHasChildren()
	{
		return base.transform.childCount != 0;
	}

	public void MarkChildren()
	{
		if (!CheckHasChildren())
		{
			return;
		}
		for (int i = 0; i < base.transform.childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject;
			if (gameObject.transform.childCount != 0 && gameObject.GetComponent<SpringBoneMarker>() == null)
			{
				gameObject.AddComponent<SpringBoneMarker>();
				gameObject.GetComponent<SpringBoneMarker>().MarkChildren();
			}
		}
	}

	public SpringBone AddSpringBone()
	{
		SpringBone springBone = base.gameObject.AddComponent<SpringBone>();
		springBone.child = base.gameObject.transform.GetChild(0);
		return springBone;
	}

	public void UnmarkSelf()
	{
		Object.DestroyImmediate(this);
	}
}
