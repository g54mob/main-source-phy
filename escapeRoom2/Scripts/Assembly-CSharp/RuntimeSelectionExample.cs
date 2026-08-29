using Battlehub.RTHandles;
using UnityEngine;

public class RuntimeSelectionExample : MonoBehaviour
{
	private void Start()
	{
		Transform transform = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
		Transform transform2 = GameObject.CreatePrimitive(PrimitiveType.Capsule).transform;
		Transform transform3 = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
		transform.position = new Vector3(2f, 0f, 0f);
		transform2.position = new Vector3(0f, 0f, 0f);
		transform3.position = new Vector3(-2f, 0f, 0f);
		transform.gameObject.AddComponent<PositionHandle>();
		transform2.gameObject.AddComponent<PositionHandle>();
		transform3.gameObject.AddComponent<PositionHandle>();
	}
}
