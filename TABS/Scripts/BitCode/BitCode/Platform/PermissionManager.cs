using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitCode.Users;

namespace BitCode.Platform
{
	public static class PermissionManager
	{
		internal enum fJinPQBWEgNUUVdtjSzVuNNhgKtq
		{

		}

		private sealed class SrkecxGhynPZQccmFfoswftaaIOxb : IPlatformService, IPermissionManager<fJinPQBWEgNUUVdtjSzVuNNhgKtq>
		{
			public event Action<IPlatformService, Exception> InternalErrorOccurred
			{
				add
				{
				}
				remove
				{
				}
			}

			public Task<PermissionResult<fJinPQBWEgNUUVdtjSzVuNNhgKtq>> HasPermissionAsync(ILocalAccount localAccount, fJinPQBWEgNUUVdtjSzVuNNhgKtq permission)
			{
				return Task.FromResult(new PermissionResult<fJinPQBWEgNUUVdtjSzVuNNhgKtq>(permission, PermissionState.Granted, localAccount));
			}

			public Task<PermissionResult<fJinPQBWEgNUUVdtjSzVuNNhgKtq>> HasPermissionWithTargetUserAsync(ILocalAccount localAccount, fJinPQBWEgNUUVdtjSzVuNNhgKtq permission, IRemoteAccount target)
			{
				return Task.FromResult(new PermissionResult<fJinPQBWEgNUUVdtjSzVuNNhgKtq>(permission, PermissionState.Granted, localAccount, target));
			}

			public Task<PermissionResult<fJinPQBWEgNUUVdtjSzVuNNhgKtq>> ResolvePermission(ILocalAccount localAccount, fJinPQBWEgNUUVdtjSzVuNNhgKtq permission)
			{
				throw new NotSupportedException();
			}

			public Task NotifyWithUI(ILocalAccount localAccount, fJinPQBWEgNUUVdtjSzVuNNhgKtq permission)
			{
				throw new NotSupportedException();
			}

			public PermissionRuleManager<TGameFeature, fJinPQBWEgNUUVdtjSzVuNNhgKtq> GetPermissionRulesManager<TGameFeature>(PermissionRules<TGameFeature, fJinPQBWEgNUUVdtjSzVuNNhgKtq> rules)
			{
				return new PermissionRuleManager<TGameFeature, fJinPQBWEgNUUVdtjSzVuNNhgKtq>(this, new PermissionRules<TGameFeature, fJinPQBWEgNUUVdtjSzVuNNhgKtq>());
			}
		}

		public static IPermissionRuleManager<TGameFeature> GetDefaultPermissionRulesManager<TGameFeature>(IEqualityComparer<TGameFeature> comparer = null)
		{
			return new SrkecxGhynPZQccmFfoswftaaIOxb().GetPermissionRulesManager(new PermissionRules<TGameFeature, fJinPQBWEgNUUVdtjSzVuNNhgKtq>(comparer));
		}
	}
}
