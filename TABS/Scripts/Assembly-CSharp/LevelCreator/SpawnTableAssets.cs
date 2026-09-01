using UnityEngine;

namespace LevelCreator
{
	public class SpawnTableAssets : MonoBehaviour
	{
		private DMEditor dmEditor;

		private void Start()
		{
			dmEditor = Object.FindObjectOfType<DMEditor>();
		}

		private void Update()
		{
			if (!Input.GetKeyDown(KeyCode.H))
			{
				return;
			}
			string[] keys = dmEditor.editorObjectTable.GetKeys();
			Vector3 vector = new Vector3(30f, 30f, -320f);
			for (int i = 0; i < keys.Length; i++)
			{
				if (i % 10 == 0)
				{
					vector.z += 10f;
					vector.x = 30f;
				}
				vector.x += 10f;
			}
		}
	}
}
