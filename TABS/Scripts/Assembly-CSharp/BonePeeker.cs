using UnityEngine;

[ExecuteInEditMode]
public class BonePeeker : MonoBehaviour
{
	private void Awake()
	{
		SkinnedMeshRenderer component = GetComponent<SkinnedMeshRenderer>();
		Debug.Log("ITERATING");
		Transform[] bones = component.bones;
		foreach (Transform transform in bones)
		{
			Debug.LogFormat("Bone {0} exists", transform.name);
		}
	}
}
