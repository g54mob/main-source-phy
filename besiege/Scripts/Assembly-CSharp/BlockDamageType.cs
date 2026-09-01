public class BlockDamageType : SimComponent
{
	public DamageType damageType;

	protected bool hasOldAI;

	public DamageType DamageType
	{
		get
		{
			return damageType;
		}
	}

	protected void Start()
	{
		if (StatMaster.levelSimulating)
		{
			hasOldAI = FactionsController.SimpleAiArray().Length > 0;
		}
	}

	public override void Init(Machine machine, BlockBehaviour block)
	{
		base.Init(machine, block);
		damageType = block.Prefab.myDamageType;
	}
}
