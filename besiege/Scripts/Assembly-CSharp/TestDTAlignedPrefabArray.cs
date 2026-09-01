using UnityEngine;

public class TestDTAlignedPrefabArray : MonoBehaviour
{
	public DynamicText[] dynamicTextPrefabs;

	public Vector3 startPos = Vector3.zero;

	public Vector3 paddingBetweenTexts = Vector3.zero;

	private void Start()
	{
		if (dynamicTextPrefabs == null || dynamicTextPrefabs.Length == 0)
		{
			Debug.LogError("Must fill Dynamic Text Prefabs array in editor");
			return;
		}
		Vector3 position = startPos;
		for (int i = 0; i < dynamicTextPrefabs.Length; i++)
		{
			DynamicText dynamicText = dynamicTextPrefabs[i];
			Transform transform = Object.Instantiate(dynamicText.transform);
			transform.parent = base.transform;
			DynamicText component = transform.GetComponent<DynamicText>();
			component.transform.position = position;
			position += new Vector3(component.bounds.size.x, 0f, 0f);
			position += paddingBetweenTexts;
		}
	}
}
