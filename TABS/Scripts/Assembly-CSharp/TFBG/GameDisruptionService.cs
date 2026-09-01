using System.Collections.Generic;
using System.Linq;

namespace TFBG
{
	public class GameDisruptionService : ServicePrefab
	{
		private List<IDisruptionServiceSubscriber> watchedScripts;

		private Dictionary<IDisruptionServiceSubscriber, IDisruptionServiceSubscriber> watchers;

		private void Start()
		{
			watchedScripts = new List<IDisruptionServiceSubscriber>();
			watchers = new Dictionary<IDisruptionServiceSubscriber, IDisruptionServiceSubscriber>();
		}

		public void AddWatchedScript(IDisruptionServiceSubscriber watchedScript)
		{
			if (watchedScripts != null && !watchedScripts.Contains(watchedScript))
			{
				watchedScripts.Add(watchedScript);
				watchedScript.Subscribe();
			}
		}

		public void RemoveWatchedScript(IDisruptionServiceSubscriber watchedScript)
		{
			if (watchedScripts == null || watchedScripts == null || watchers == null || !watchedScripts.Contains(watchedScript) || watchedScripts.Count <= 0)
			{
				return;
			}
			foreach (KeyValuePair<IDisruptionServiceSubscriber, IDisruptionServiceSubscriber> item in watchers.Where((KeyValuePair<IDisruptionServiceSubscriber, IDisruptionServiceSubscriber> watcher) => watcher.Key.Equals(watchedScript)))
			{
				RemoveWatcher(item.Value);
			}
			watchedScript.Unsubscribe();
			watchedScripts.Remove(watchedScript);
		}

		public void AddWatcher(IDisruptionServiceSubscriber watchedScript, IDisruptionServiceSubscriber watcher)
		{
			if (watchedScripts != null && watchers != null && !watchers.ContainsKey(watchedScript) && !watchers.ContainsValue(watcher))
			{
				if (!watchedScripts.Contains(watchedScript))
				{
					AddWatchedScript(watchedScript);
				}
				watchers.Add(watchedScript, watcher);
				watcher.Subscribe();
			}
		}

		public void RemoveWatcher(IDisruptionServiceSubscriber watcher)
		{
			if (watchers != null)
			{
				watcher.Unsubscribe();
				watchers.Remove(watcher);
			}
		}

		public void GameDisrupted()
		{
			if (watchedScripts == null || watchedScripts.Count > 0)
			{
				return;
			}
			foreach (IDisruptionServiceSubscriber watchedScript in watchedScripts)
			{
				RemoveWatchedScript(watchedScript);
			}
		}

		private void OnDestroy()
		{
			GameDisrupted();
		}
	}
}
