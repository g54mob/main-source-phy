using UnityEngine;

public class BlinkMove : Move
{
	private DataHandler data;

	private SpawnObject spawn;

	private void Start()
	{
		data = GetComponentInParent<DataHandler>();
		spawn = GetComponent<SpawnObject>();
	}

	public override void DoMove(Rigidbody enemyWeapon, Rigidbody enemyTorso, DataHandler targetData)
	{
		spawn.Spawn(data.hip.position, Random.onUnitSphere);
		ServiceLocator.GetService<ParticlePlayer>().PlayEffect(3, data.torso.position, Vector3.up);
		BlinkEffect.Blink(data, enemyTorso);
	}
}
