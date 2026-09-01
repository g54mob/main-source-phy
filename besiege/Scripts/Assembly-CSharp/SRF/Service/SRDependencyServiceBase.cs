using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRF.Service
{
	public abstract class SRDependencyServiceBase<T> : SRServiceBase<T>, IAsyncService where T : class
	{
		private bool _isLoaded;

		protected abstract Type[] Dependencies { get; }

		public bool IsLoaded
		{
			get
			{
				return _isLoaded;
			}
		}

		[Conditional("ENABLE_LOGGING")]
		private void Log(string msg, UnityEngine.Object target)
		{
			UnityEngine.Debug.Log(msg, target);
		}

		protected override void Start()
		{
			base.Start();
			StartCoroutine(LoadDependencies());
		}

		protected virtual void OnLoaded()
		{
		}

		private IEnumerator LoadDependencies()
		{
			SRServiceManager.LoadingCount++;
			Type[] dependencies = Dependencies;
			foreach (Type d in dependencies)
			{
				if (SRServiceManager.HasService(d))
				{
					continue;
				}
				object service = SRServiceManager.GetService(d);
				if (service == null)
				{
					UnityEngine.Debug.LogError("[Service] Could not resolve dependency ({0})".Fmt(d.Name));
					base.enabled = false;
					yield break;
				}
				IAsyncService a = service as IAsyncService;
				if (a != null)
				{
					while (!a.IsLoaded)
					{
						yield return new WaitForEndOfFrame();
					}
				}
			}
			_isLoaded = true;
			SRServiceManager.LoadingCount--;
			OnLoaded();
		}
	}
}
