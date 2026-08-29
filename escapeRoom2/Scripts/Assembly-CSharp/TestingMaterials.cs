using UnityEngine;

public class TestingMaterials : MonoBehaviour
{
	public Wall wall1;

	public Wall wall2;

	public Wall wall3;

	public Material defaultMaterial;

	private Material defaultMaterialCopy;

	private void Awake()
	{
		defaultMaterialCopy = new Material(defaultMaterial);
		defaultMaterialCopy.name = "Copy!!!";
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			wall1.GetComponentInChildren<Renderer>().materials[0].color = Color.red;
			Debug.Log("Setting wall 1 front to red");
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			wall1.GetComponentInChildren<Renderer>().sharedMaterials[1].color = Color.green;
			Debug.Log("Setting all walls sides to green");
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			Material[] sharedMaterials = wall1.GetComponentInChildren<Renderer>().sharedMaterials;
			sharedMaterials[2] = defaultMaterialCopy;
			wall1.GetComponentInChildren<Renderer>().sharedMaterials = sharedMaterials;
			Debug.Log("Setting all walls backs to green");
		}
	}
}
