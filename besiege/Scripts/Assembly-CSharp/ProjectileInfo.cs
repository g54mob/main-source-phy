public class ProjectileInfo : BasicInfo
{
	public NetworkProjectileType projectileType;

	public ProjectileScript projectile;

	public bool hasProjectile;

	public NetworkProjectile netProjectile;

	protected override void Awake()
	{
		if (netProjectile == null)
		{
			netProjectile = GetComponent<NetworkProjectile>();
		}
		NetBlock = netProjectile;
		base.Awake();
	}

	public override void UpdateParentMachine()
	{
	}
}
