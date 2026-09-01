using UnityEngine;

public class FindParent : MonoBehaviour
{
	public string parent = "Main Camera";

	public Vector3 localPos = Vector3.zero;

	private void Start()
	{
		base.transform.parent = GameObject.Find(parent).transform;
		base.transform.localPosition = localPos;
	}
}
