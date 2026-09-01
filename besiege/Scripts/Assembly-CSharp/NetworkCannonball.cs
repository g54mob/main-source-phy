using UnityEngine;

public class NetworkCannonball : NetworkProjectile
{
	public CannonBallDamage ballDamage;

	private static Vector3 ballScale = Vector3.one * 0.6f;

	protected override void SetParentMachine(ushort playerId)
	{
		ServerMachine machine;
		if (NetworkScene.Instance.GetMachine(playerId, out machine))
		{
			projectileInfo.SetParentMachine(machine);
			ballDamage.SetParentMachine(machine);
		}
		else
		{
			projectileInfo.ResetParentMachine();
			ballDamage.ResetParentMachine();
		}
	}

	public override void ReturnToPool()
	{
		myTransform.localRotation = Quaternion.identity;
		myTransform.localScale = ballScale;
	}

	public override void Despawn(byte[] despawnInfo)
	{
		if (StatMaster.isClient && despawnInfo != null && despawnInfo.Length > 0)
		{
			Vector3 vec = Vector3.zero;
			NetworkCompression.DecompressPosition(despawnInfo, 0, out vec);
			Quaternion rot = Quaternion.identity;
			NetworkCompression.DecompressRotation(despawnInfo, 6, out rot);
			ballDamage.Explode(vec, rot);
		}
	}

	public override void Spawn(uint frame, ushort playerId, byte[] spawnInfo, bool explode = false)
	{
		base.Spawn(frame, playerId, spawnInfo);
		ballDamage.alwaysExplode = explode;
	}
}
