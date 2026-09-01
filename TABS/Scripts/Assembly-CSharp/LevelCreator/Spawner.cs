using System.Collections.Generic;
using UnityEngine;

namespace LevelCreator
{
	public class Spawner : MonoBehaviour, ITriggerable
	{
		public GameObject objectToSpawnInitial;

		private GameObject objectToSpawn;

		public List<GameObject> editorOnlyObjects = new List<GameObject>();

		public float spawnRadius;

		public void Trigger()
		{
			Vector2 vector = Random.insideUnitCircle * spawnRadius;
			Vector3 position = base.transform.position + new Vector3(vector.x, 0f, vector.y);
			Object.Instantiate(objectToSpawn, position, Quaternion.identity);
		}

		private void Awake()
		{
			if (Utility.GetCurrentGameMode() == Utility.GameMode.PlayMode)
			{
				foreach (GameObject editorOnlyObject in editorOnlyObjects)
				{
					editorOnlyObject.SetActive(value: false);
				}
			}
			objectToSpawn = objectToSpawnInitial;
			if (DMEditor.Instance == null)
			{
				Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].enabled = false;
				}
			}
		}
	}
}
