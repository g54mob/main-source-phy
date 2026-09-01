using UnityEngine;

public interface IExplosionEffect
{
	bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater);
}
