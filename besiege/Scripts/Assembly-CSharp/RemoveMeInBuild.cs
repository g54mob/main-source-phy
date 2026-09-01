using UnityEngine;

public class RemoveMeInBuild : MonoBehaviour
{
	private void Awake()
	{
		if (SingleInstance<PrefabMaster>.hasInstance() && SingleInstance<PrefabMaster>.Instance.gameObject != base.gameObject)
		{
			Object.DestroyImmediate(base.gameObject);
		}
		else
		{
			Object.DestroyImmediate(base.gameObject);
		}
	}
}
