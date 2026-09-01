using UnityEngine;

public class SpawnZoneEntity : GenericEntity
{
	public MeshRenderer[] vis;

	public Collider col;

	public SetIconVirtualTrigger setTriggerIcon;

	private bool lastEnabled = true;

	public override void Init()
	{
		if (!isInitialized)
		{
			base.Init();
			ToggleCollider(isSimulating);
			UpdateVisibility(isSimulating);
		}
	}

	public override void SetupDefault()
	{
		base.SetupDefault();
		EntityLogic entityLogic = new EntityLogic(TriggerType.Activate, this);
		TriggerTarget triggerTarget = new TriggerTarget(TriggerTargetType.Picker);
		triggerTarget.type = TriggerTargetObjectType.Entity;
		entityLogic.targets.Add(triggerTarget);
		triggerTarget.ApplyValue();
		EntityEvent entityEvent = new EntityEvent(EventContainer.EventType.SetRespawn);
		entityLogic.events.Add(entityEvent);
		entityEvent.ApplyValue();
		logicData.Add(entityLogic);
		entityLogic.ApplyValue();
	}

	protected void ToggleIcon(bool t)
	{
		UpdateVisibility(isSimulating);
	}

	protected void ToggleCollider(bool t)
	{
		col.enabled = !t;
	}

	protected override void Start()
	{
		base.Start();
		ToggleCollider(isSimulating);
		UpdateVisibility(isSimulating);
	}

	public void UpdateVisibility(bool sim)
	{
		bool flag = StatMaster.Mode.levelEdit && !sim;
		if (lastEnabled != flag)
		{
			setTriggerIcon.enabled = flag;
			for (int i = 0; i < vis.Length; i++)
			{
				vis[i].enabled = flag;
			}
			lastEnabled = flag;
		}
	}
}
