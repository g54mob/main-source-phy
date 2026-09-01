using UnityEngine;

namespace Landfall.TABS
{
	public class MainMenuUnitSpawner : MonoBehaviour
	{
		public float spacing = 1f;

		public float setDelay;

		public float setDistance;

		public MainMenuTimeHandler time;

		private float counter;

		public int number = 10;

		public bool sprint;

		public UnitBlueprint blueprint;

		private void Start()
		{
			time = Object.FindObjectOfType<MainMenuTimeHandler>();
			if ((bool)time)
			{
				if (setDelay != 0f)
				{
					time.delay = setDelay;
				}
				if (setDistance != 0f)
				{
					time.targetCamDistance = setDistance;
				}
			}
			SpawnUnits();
		}

		public void SpawnUnits()
		{
			for (int i = 0; i < number; i++)
			{
				Unit component = blueprint.Spawn(base.transform.position + ((float)i * spacing - 0.5f * (float)number * spacing) * Vector3.forward, base.transform.rotation, base.transform.root.name.ToUpper().Contains("B") ? Team.Blue : Team.Red)[0].GetComponent<Unit>();
				if (sprint)
				{
					component.GetComponentInChildren<GeneralInput>().shift = true;
				}
				if ((bool)time)
				{
					component.GetComponentInChildren<HealthHandler>().AssignDamageAction(time.Freeze);
				}
			}
		}
	}
}
