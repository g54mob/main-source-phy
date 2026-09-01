public class CannonBoltTransform : PrefabTransform
{
	public CanonBlock canonBlock;

	public override void Apply(BlockPrefab prefab)
	{
		base.Apply(prefab);
		canonBlock.boltSpawnPos = base.transform.localPosition;
		canonBlock.boltSpawnRot = base.transform.localRotation;
	}
}
