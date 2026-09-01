using System.Threading.Tasks;
using BitCode.Users;

namespace BitCode.Platform
{
	public interface IPermissionRuleManager<in TGameFeature> : IPlatformService
	{
		Task<IPermissionResult> HasPermission(ILocalAccount localAccount, TGameFeature feature);

		Task<IPermissionResult> HasPermissionWithTargetUser(ILocalAccount localAccount, TGameFeature permission, IRemoteAccount targetUser);

		Task NotifyWithUI(ILocalAccount localAccount, IPermissionResult result);

		Task<IPermissionResult> ResolvePermission(ILocalAccount localAccount, IPermissionResult result);
	}
}
