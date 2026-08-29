using UnityEngine;

[ExecuteInEditMode]
public class MeshRendererOrder : MonoBehaviour
{
	public int Order;

	private void OnEnable()
	{
		GetComponent<Renderer>().sortingOrder = Order;
	}

	private void OnValidate()
	{
		GetComponent<Renderer>().sortingOrder = Order;
	}
}
