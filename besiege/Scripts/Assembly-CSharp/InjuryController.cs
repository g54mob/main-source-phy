using UnityEngine;

public class InjuryController : MonoBehaviour, IExplosionEffect, IFireEffect
{
	public InjuryType activeType;

	public string[] bluntDeaths;

	public string[] fireDeaths;

	public bool causeOnScreenUpdate = true;

	private bool _isAlive;

	private void Start()
	{
		_isAlive = true;
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if ((mask & 4) != 0)
		{
			activeType = InjuryType.Fire;
			Kill();
			return true;
		}
		return false;
	}

	public bool OnIgnite(FireTag t, Collider c, bool pyroMode)
	{
		activeType = InjuryType.Fire;
		return true;
	}

	public void FireKill()
	{
		Kill();
	}

	public void Kill()
	{
		if (_isAlive)
		{
			if (!StatMaster.isMP)
			{
				AchievementHelper.Increment(8, 1);
			}
			CauseOfDeath(activeType);
			if (activeType == InjuryType.Fire && base.name.ToLower().Contains("chicken"))
			{
				AchievementHelper.Increment(16, 1);
			}
			if (causeOnScreenUpdate)
			{
				RealtimeUpdater.Instance.AddBox(string.Empty, string.Empty, activeType);
			}
			_isAlive = false;
		}
	}

	private void CauseOfDeath(InjuryType death)
	{
		switch (death)
		{
		case InjuryType.Blunt:
			RandomBlunt();
			break;
		case InjuryType.Sharp:
			break;
		default:
			RandomFire();
			break;
		}
	}

	private void RandomBlunt()
	{
	}

	private void RandomFire()
	{
	}
}
