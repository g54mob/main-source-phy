using System.Threading.Tasks;
using BitCode.Users;

namespace BitCode.Platform
{
	public interface IPermissionManager<TPlatformPermission> : IPlatformService
	{
		Task<PermissionResult<TPlatformPermission>> HasPermissionAsync(ILocalAccount localAccount, TPlatformPermission permission);

		Task<PermissionResult<TPlatformPermission>> HasPermissionWithTargetUserAsync(ILocalAccount localAccount, TPlatformPermission permission, IRemoteAccount target);

		Task<PermissionResult<TPlatformPermission>> ResolvePermission(ILocalAccount localAccount, TPlatformPermission permission);

		Task NotifyWithUI(ILocalAccount localAccount, TPlatformPermission permission);

		PermissionRuleManager<TGameFeature, TPlatformPermission> GetPermissionRulesManager<TGameFeature>(PermissionRules<TGameFeature, TPlatformPermission> rules);
	}
}
