using Landfall.TABS_Input;
using UnityEngine;

namespace LevelCreator
{
	public class SeedMeteor : MonoBehaviour
	{
		public GameObject actionInitial;

		private GameObject action;

		public Transform seedOriginTransformInitial;

		private Transform seedOriginTransform;

		public GameObject seedInitial;

		private GameObject seed;

		private DMEditor dmEditor;

		private GameObject targetVisualObject;

		private InputState inputState = new InputState("SeedMeteorInputState");

		private void OnDestroy()
		{
			targetVisualObject.GetComponentInChildren<MeshRenderer>().enabled = false;
			InputManager.RemoveState(inputState);
		}

		private void Start()
		{
			action = actionInitial;
			seedOriginTransform = seedOriginTransformInitial;
			seed = seedInitial;
			dmEditor = DMEditor.Instance;
			dmEditor.HideCursor();
			targetVisualObject = GameObject.Find("SeedMeteorVisualObject");
			targetVisualObject.GetComponentInChildren<MeshRenderer>().enabled = true;
			AssignInputStates();
		}

		private void AssignInputStates()
		{
			PlayerActions instance = PlayerActions.Instance;
			inputState.ClearAllEvents();
			inputState.AddOnKeyDownListener(instance.m_toolPrimary, delegate
			{
				if (Physics.Raycast(dmEditor.playerCamera.transform.position + dmEditor.playerCamera.transform.forward, dmEditor.playerCamera.transform.forward, out var hitInfo, dmEditor.rayDistance, LayerMask.GetMask("Map")))
				{
					dmEditor.ScheduleTakeLevelSnapshot();
					GameObject gameObject = Object.Instantiate(action, dmEditor.Actions.transform);
					GameObject obj = Object.Instantiate(seed, hitInfo.point + new Vector3(0f, Random.Range(15, 25), 0f), seedOriginTransform.rotation, gameObject.GetComponent<Action>().actionTasks.transform);
					Seed component = obj.GetComponent<Seed>();
					component.SpawnChildrenAtStart = true;
					component.dmEditor = dmEditor;
					component.seeds = dmEditor.seedTable.GetRowValue("1df3f4b5-aaf7-451f-9fb5-af67e36dc269").seeds;
					Rigidbody component2 = obj.GetComponent<Rigidbody>();
					if ((bool)component2)
					{
						component2.AddForce(new Vector3(Random.Range(-10, 10), Random.Range(-20, -15), Random.Range(-5, 5)), ForceMode.Impulse);
					}
				}
			});
			InputManager.PushState(inputState);
		}

		private void Update()
		{
			if (Physics.Raycast(dmEditor.playerCamera.transform.position + dmEditor.playerCamera.transform.forward, dmEditor.playerCamera.transform.forward, out var hitInfo, dmEditor.rayDistance, LayerMask.GetMask("Map")))
			{
				targetVisualObject.transform.position = hitInfo.point;
			}
			else
			{
				targetVisualObject.transform.position = dmEditor.playerCamera.transform.position + dmEditor.playerCamera.transform.forward * dmEditor.rayDistance;
			}
		}
	}
}
