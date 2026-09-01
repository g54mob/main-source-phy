using UnityEngine;

namespace InternalModding.Blocks
{
	public class ShootingNetworkProjectile : NetworkProjectile
	{
		public override void Despawn(byte[] despawnInfo)
		{
			base.Despawn(despawnInfo);
			if (StatMaster.isClient && despawnInfo != null && despawnInfo.Length > 0)
			{
				Vector3 vec = Vector3.zero;
				NetworkCompression.DecompressPosition(despawnInfo, 0, out vec);
				Quaternion rot = Quaternion.identity;
				NetworkCompression.DecompressRotation(despawnInfo, 6, out rot);
				projectileScript.Explode(vec, rot);
			}
		}
	}
}
