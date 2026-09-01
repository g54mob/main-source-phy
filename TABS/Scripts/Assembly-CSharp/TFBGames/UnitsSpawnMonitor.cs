using System;
using Landfall.TABS;

namespace TFBGames
{
	public class UnitsSpawnMonitor : IService
	{
		public event Action<Unit> SpawnedUnitBeforeInitialization;

		public event Action<Unit> SpawnedUnit;

		public event Action<Unit> DestroyedUnit;

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
		}

		public void OnStart()
		{
		}

		public void OnUpdate()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void UnRegister()
		{
		}

		public void OnUnitAwake(Unit unit)
		{
			this.SpawnedUnitBeforeInitialization?.Invoke(unit);
		}

		public void OnUnitStart(Unit unit)
		{
			this.SpawnedUnit?.Invoke(unit);
		}

		public void OnUnitDestroyed(Unit unit)
		{
			this.DestroyedUnit?.Invoke(unit);
		}
	}
}
