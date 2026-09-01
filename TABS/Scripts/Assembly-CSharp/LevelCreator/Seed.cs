using UnityEngine;

namespace LevelCreator
{
	public class Seed : MonoBehaviour
	{
		public bool SpawnChildrenAtStart;

		public CameraScript cameraScript;

		public SeedCollectionData[] seeds;

		[HideInInspector]
		private float scaleMultiplier;

		[HideInInspector]
		private float rotation;

		[HideInInspector]
		private float downOffset;

		[HideInInspector]
		private string prefabKey;

		private Action action;

		private Rigidbody thisRigidBody;

		private bool spawnChildren = true;

		private float timeLeftToLive = 6f;

		public DMEditor dmEditor;

		private void Start()
		{
			thisRigidBody = GetComponent<Rigidbody>();
			action = GetComponentInParent<Action>();
			if (SpawnChildrenAtStart)
			{
				timeLeftToLive *= 2f;
				SpawnChildren();
				Object.Destroy(base.gameObject);
			}
		}

		private void Update()
		{
			timeLeftToLive -= Time.deltaTime;
			if (timeLeftToLive < 0f)
			{
				Object.Destroy(base.gameObject);
			}
			else if (thisRigidBody.IsSleeping())
			{
				if (spawnChildren)
				{
					SpawnChildren();
				}
				else
				{
					DMEditorComponent editorObject = dmEditor.InstantiateEditorObject(prefabKey, base.transform.position + Vector3.down * downOffset, Quaternion.identity, Quaternion.Euler(0f, rotation, 0f), Vector3.one * scaleMultiplier, action.actionLevel, animatedSpawn: true);
					dmEditor.MarkObjectForSnapping(editorObject);
				}
				Object.Destroy(base.gameObject);
			}
		}

		private void SpawnChildren()
		{
			for (int i = 0; i < seeds.Length; i++)
			{
				for (int j = 0; j < seeds[i].countMultiplier; j++)
				{
					Vector3 vector = new Vector3(Random.Range(-1f, 1f), Random.Range(0.1f, 1f), Random.Range(-1f, 1f));
					GameObject obj = Object.Instantiate(base.gameObject, base.transform.position + vector * 0.5f, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f), action.actionTasks.transform);
					Seed component = obj.GetComponent<Seed>();
					component.spawnChildren = false;
					component.SpawnChildrenAtStart = false;
					component.timeLeftToLive = 6f;
					SeedCollectionData seedCollectionData = seeds[i];
					component.prefabKey = seedCollectionData.editorObjectId;
					component.scaleMultiplier = Random.Range(seedCollectionData.scaleMultiplierMinMax.x, seedCollectionData.scaleMultiplierMinMax.y);
					component.rotation = Random.Range(0f, 360f);
					component.downOffset = Random.Range(seedCollectionData.downOffsetMinMax.x, seedCollectionData.downOffsetMinMax.y);
					Rigidbody component2 = obj.GetComponent<Rigidbody>();
					if ((bool)component2)
					{
						component2.AddForce(vector * Random.Range(seedCollectionData.splitForceMinMax.x, seedCollectionData.splitForceMinMax.y) / ((!SpawnChildrenAtStart) ? 1 : 5), ForceMode.Impulse);
					}
				}
			}
		}
	}
}
