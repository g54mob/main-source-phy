using UnityEngine;

[AddComponentMenu("Levels/Delete World Bounds")]
public class DeleteWorldBounds : MonoBehaviour
{
	private void Start()
	{
		Object.Destroy(GameObject.Find("WORLD BOUNDARIES"));
	}
}
