using UnityEngine;

namespace Landfall.TABS
{
	public abstract class TeamSingletons<T> : MonoBehaviour where T : MonoBehaviour
	{
		public static T RedInstance;

		public static T BlueInstance;

		public Team team;

		private void Awake()
		{
			if (team == Team.Red)
			{
				RedInstance = this as T;
			}
			else
			{
				BlueInstance = this as T;
			}
			OnAwake();
		}

		public virtual void OnAwake()
		{
		}

		public static T GetTeamInstance(Team team)
		{
			if (team == Team.Red)
			{
				return RedInstance;
			}
			return BlueInstance;
		}
	}
}
