using Landfall.TABS;
using TFBGames;
using UnityEngine;
using UnityEngine.Events;

public class KillAfterSeconds : MonoBehaviour
{
	public float seconds = 30f;

	public bool destroyRoot;

	public float eventTimer;

	public UnityEvent killEvent = new UnityEvent();

	[HideInInspector]
	public bool skinnedShape;

	private bool eventInvoked;

	private HealthHandler health;

	private DataHandler data;

	private bool isAllowedToKillUnit = true;

	private float counter;

	private Unit unit;

	private TeamHolder rootTeamHolder;

	[HideInInspector]
	public GameObject objectToSpawn;

	[HideInInspector]
	public bool spawnObjectOnMainRig;

	private void Start()
	{
		health = base.transform.root.GetComponentInChildren<HealthHandler>();
		data = base.transform.root.GetComponentInChildren<DataHandler>();
		TeamHolder.GetTeamRelevantComponents(base.transform, ref unit, ref rootTeamHolder);
		INetworkService service = ServiceLocator.GetService<INetworkService>();
		if (health != null && service != null && service.IsClient)
		{
			isAllowedToKillUnit = false;
			health.UnitDied += OnUnitDied;
		}
	}

	private void OnDestroy()
	{
		if (health != null)
		{
			health.UnitDied -= OnUnitDied;
		}
	}

	private void Update()
	{
		counter += Time.deltaTime;
		if (counter > seconds && isAllowedToKillUnit && (bool)health && !data.Dead)
		{
			Spawn();
			if (health.Die() && destroyRoot)
			{
				Object.Destroy(base.transform.root.gameObject);
			}
		}
		if (eventTimer > 0f && counter > eventTimer && !eventInvoked)
		{
			killEvent.Invoke();
			eventInvoked = true;
		}
	}

	private void Spawn()
	{
		if (!objectToSpawn)
		{
			return;
		}
		if (spawnObjectOnMainRig)
		{
			Transform transform = base.transform.root.Find("Mesh");
			GameObject gameObject = Object.Instantiate(objectToSpawn, base.transform.GetComponentInChildren<DataHandler>().mainRig.position, Quaternion.identity);
			TeamHolder.AddTeamHolder(gameObject, unit, rootTeamHolder);
			if (skinnedShape && (bool)transform)
			{
				SkinnedMeshRenderer componentInChildren = transform.GetComponentInChildren<SkinnedMeshRenderer>();
				ParticleSystem componentInChildren2 = gameObject.GetComponentInChildren<ParticleSystem>();
				if ((bool)componentInChildren2 && (bool)componentInChildren)
				{
					ParticleSystem.ShapeModule shape = componentInChildren2.shape;
					shape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
					shape.skinnedMeshRenderer = componentInChildren;
					componentInChildren2.Emit(50);
				}
			}
		}
		else
		{
			Object.Instantiate(objectToSpawn, base.transform.position, Quaternion.identity);
		}
	}

	private void OnUnitDied(Unit unit)
	{
		Spawn();
	}
}
