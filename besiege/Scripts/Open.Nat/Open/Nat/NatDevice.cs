using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Open.Nat
{
	public abstract class NatDevice
	{
		private readonly HashSet<Mapping> _openedMapping = new HashSet<Mapping>();

		protected DateTime LastSeen { get; private set; }

		internal void Touch()
		{
			LastSeen = DateTime.Now;
		}

		public abstract Task CreatePortMapAsync(Mapping mapping, float timeout = 4f);

		public abstract Task DeletePortMapAsync(Mapping mapping, float timeout = 4f);

		public abstract Task<IEnumerable<Mapping>> GetAllMappingsAsync(float timeout = 4f);

		public abstract Task<IPAddress> GetExternalIPAsync(float timeout = 4f);

		public abstract Task<Mapping> GetSpecificMappingAsync(Protocol protocol, int port, float timeout = 4f);

		protected void RegisterMapping(Mapping mapping)
		{
			_openedMapping.Remove(mapping);
			_openedMapping.Add(mapping);
		}

		protected void UnregisterMapping(Mapping mapping)
		{
			_openedMapping.RemoveWhere((Mapping x) => x.Equals(mapping));
		}

		internal void ReleaseMapping(IEnumerable<Mapping> mappings)
		{
			Mapping[] array = mappings.ToArray();
			int num = array.Length;
			NatDiscoverer.TraceSource.LogInfo("{0} ports to close", num);
			for (int i = 0; i < num; i = checked(i + 1))
			{
				Mapping mapping = _openedMapping.ElementAt(i);
				try
				{
					DeletePortMapAsync(mapping);
					NatDiscoverer.TraceSource.LogInfo(string.Concat(mapping, " port successfully closed"));
				}
				catch (Exception)
				{
					NatDiscoverer.TraceSource.LogError(string.Concat(mapping, " port couldn't be close"));
				}
			}
		}

		internal void ReleaseAll()
		{
			ReleaseMapping(_openedMapping);
		}

		internal void ReleaseSessionMappings()
		{
			IEnumerable<Mapping> mappings = _openedMapping.Where((Mapping m) => m.LifetimeType == MappingLifetime.Session);
			ReleaseMapping(mappings);
		}

		internal Task RenewMappings()
		{
			Task task = null;
			IEnumerable<Mapping> source = _openedMapping.Where((Mapping x) => x.ShoundRenew());
			Mapping[] array = source.ToArray();
			foreach (Mapping mapping in array)
			{
				Mapping m = mapping;
				task = ((task == null) ? RenewMapping(m) : task.ContinueWith((Task t) => RenewMapping(m)).Unwrap());
			}
			return task;
		}

		private Task RenewMapping(Mapping mapping)
		{
			Mapping renewMapping = new Mapping(mapping);
			renewMapping.Expiration = DateTime.UtcNow.AddSeconds(mapping.Lifetime);
			NatDiscoverer.TraceSource.LogInfo("Renewing mapping {0}", renewMapping);
			return CreatePortMapAsync(renewMapping).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					NatDiscoverer.TraceSource.LogWarn("Renew {0} failed", mapping);
				}
				else
				{
					NatDiscoverer.TraceSource.LogInfo("Next renew scheduled at: {0}", renewMapping.Expiration.ToLocalTime().TimeOfDay);
				}
			});
		}
	}
}
