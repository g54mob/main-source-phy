using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.Events;

namespace LevelCreator
{
	public class SeedShooter : Tool
	{
		public GameObject initalActionPrefab;

		public Transform initialSeedOriginTransform;

		public GameObject intialSeedPrefab;

		private GameObject actionPrefab;

		private Transform seedOriginTransform;

		private GameObject seedPrefab;

		private DMEditor dmEditor;

		private CameraScript cameraScript;

		private CharacterController playerController;

		private UnityAction<string> OnRadialItemSelected;

		protected override void Start()
		{
			actionPrefab = initalActionPrefab;
			seedPrefab = intialSeedPrefab;
			seedOriginTransform = initialSeedOriginTransform;
			base.Start();
			dmEditor = DMEditor.Instance;
			dmEditor.HideCursor();
			cameraScript = dmEditor.playerCamera.GetComponent<CameraScript>();
			playerController = dmEditor.playerController.GetComponent<CharacterController>();
		}

		protected override void AssignInput(PlayerActions actions)
		{
			base.AssignInput(actions);
			m_inputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				GameObject gameObject = Object.Instantiate(actionPrefab, dmEditor.Actions.transform);
				GameObject obj = Object.Instantiate(seedPrefab, seedOriginTransform.position, Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)), gameObject.GetComponent<Action>().actionTasks.transform);
				Seed component = obj.GetComponent<Seed>();
				component.cameraScript = cameraScript;
				component.GetComponent<Seed>().dmEditor = dmEditor;
				component.seeds = dmEditor.seedTable.GetRowValue("1df3f4b5-aaf7-451f-9fb5-af67e36dc269").seeds;
				Rigidbody component2 = obj.GetComponent<Rigidbody>();
				if ((bool)component2)
				{
					component2.AddForce(seedOriginTransform.TransformVector(Vector3.forward * 10f) + playerController.velocity, ForceMode.Impulse);
					component2.AddTorque(Random.insideUnitSphere * 100f, ForceMode.Impulse);
				}
			}, m_contextIcons.m_primaryIcon);
			m_inputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				if (Physics.Raycast(dmEditor.playerCamera.transform.position + dmEditor.playerCamera.transform.forward, dmEditor.playerCamera.transform.forward, out var hitInfo, dmEditor.rayDistance, LayerMask.GetMask("Map")))
				{
					dmEditor.ScheduleTakeLevelSnapshot();
					GameObject gameObject = Object.Instantiate(actionPrefab, dmEditor.Actions.transform);
					GameObject obj = Object.Instantiate(seedPrefab, hitInfo.point + new Vector3(0f, Random.Range(15, 25), 0f), seedOriginTransform.rotation, gameObject.GetComponent<Action>().actionTasks.transform);
					Seed component = obj.GetComponent<Seed>();
					component.SpawnChildrenAtStart = true;
					component.dmEditor = dmEditor;
					component.seeds = dmEditor.seedTable.GetRowValue("1df3f4b5-aaf7-451f-9fb5-af67e36dc269").seeds;
					Rigidbody component2 = obj.GetComponent<Rigidbody>();
					if ((bool)component2)
					{
						component2.AddForce(new Vector3(Random.Range(-40, 40), Random.Range(-30, -25), Random.Range(-40, 40)), ForceMode.Impulse);
					}
				}
			}, m_contextIcons.m_secondaryIcon);
		}
	}
}
