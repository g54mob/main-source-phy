using UnityEngine;

public class ObjectFade : MonoBehaviour
{
	[SerializeField]
	protected float fadePerSecond = 2.5f;

	private Material objectMaterial;

	protected void Awake()
	{
		objectMaterial = GetComponent<Renderer>().material;
	}

	protected void Update()
	{
		Color color = objectMaterial.color;
		if (color.a <= 0f)
		{
			Object.Destroy(base.gameObject.transform.parent.gameObject);
		}
		else
		{
			objectMaterial.color = new Color(color.r, color.g, color.b, Mathf.Clamp01(color.a - fadePerSecond * Time.deltaTime));
		}
	}
}
