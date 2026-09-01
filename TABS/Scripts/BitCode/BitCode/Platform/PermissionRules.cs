using System.Collections.Generic;
using System.Linq;

namespace BitCode.Platform
{
	public sealed class PermissionRules<TGameFeature, TPlatformPermission>
	{
		private readonly IDictionary<TGameFeature, List<TPlatformPermission>> zSSjXHSoWWHpGSPLXPdTQjzqmuWN;

		public static PermissionRules<TGameFeature, TPlatformPermission> AllowAll => new PermissionRules<TGameFeature, TPlatformPermission>();

		public PermissionRules(IEqualityComparer<TGameFeature> comparer = null)
		{
			zSSjXHSoWWHpGSPLXPdTQjzqmuWN = new Dictionary<TGameFeature, List<TPlatformPermission>>(comparer ?? EqualityComparer<TGameFeature>.Default);
		}

		public PermissionRules<TGameFeature, TPlatformPermission> AddRule(TGameFeature feature, params TPlatformPermission[] platformPermissions)
		{
			if (zSSjXHSoWWHpGSPLXPdTQjzqmuWN.ContainsKey(feature))
			{
				goto IL_000e;
			}
			goto IL_0059;
			IL_000e:
			int num = 496472412;
			goto IL_0013;
			IL_0013:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x45E5E1F0)) % 5)
				{
				case 3u:
					break;
				case 1u:
					zSSjXHSoWWHpGSPLXPdTQjzqmuWN[feature].AddRange(platformPermissions);
					num = (int)((num2 * 153152025) ^ 0x29DAA6FD);
					continue;
				case 4u:
					goto IL_0059;
				case 0u:
					num = (int)((num2 * 1233276856) ^ 0x7208E3A0);
					continue;
				default:
					return this;
				}
				break;
			}
			goto IL_000e;
			IL_0059:
			zSSjXHSoWWHpGSPLXPdTQjzqmuWN[feature] = platformPermissions.ToList();
			num = 1827308056;
			goto IL_0013;
		}

		internal bool oWMHqILhVIdphjksFHygeMZEzmvxA(TGameFeature P_0)
		{
			return zSSjXHSoWWHpGSPLXPdTQjzqmuWN.ContainsKey(P_0);
		}

		internal IList<TPlatformPermission> RfSBXdiNhBODkVNLxjedXNtmrUVoA(TGameFeature P_0)
		{
			return zSSjXHSoWWHpGSPLXPdTQjzqmuWN[P_0];
		}
	}
}
