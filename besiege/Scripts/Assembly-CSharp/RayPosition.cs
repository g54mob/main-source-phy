public class RayPosition : PrefabTransform
{
	public override void Apply(BlockPrefab prefab)
	{
		base.Apply(prefab);
		prefab.rayPosition = base.transform.position;
	}
}
