using UnityEngine;

public class MaterialShare : MonoBehaviour
{
	public Material materialToShare;

	private void Start()
	{
		GetComponent<Renderer>().sharedMaterial = materialToShare;
	}
}
