using UnityEngine;

public interface IFireEffect
{
	bool OnIgnite(FireTag t, Collider c, bool pyroMode);
}
