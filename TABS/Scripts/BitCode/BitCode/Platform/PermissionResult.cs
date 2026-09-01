using System.Runtime.CompilerServices;
using BitCode.Users;
using FOCOMlbIVTBKccALlxtjLsuqUTti;
using JetBrains.Annotations;

namespace BitCode.Platform
{
	[LnudqmdEKuPWwKXxRggLRAQwDsZxA]
	public struct PermissionResult<TPlatformPermission> : IPermissionResult
	{
		public readonly TPlatformPermission PlatformPermission;

		[CompilerGenerated]
		private readonly PermissionState mBcihcpJKRCDOWPPEGyNlEKQWROh;

		[CompilerGenerated]
		private readonly PermissionDetail QbRhEgaeLJEOqPFBjNCENnZIIaqIA;

		[CompilerGenerated]
		private readonly ILocalAccount GXXkBNATopFrlLVMcomITmalPZXJ;

		[CompilerGenerated]
		private readonly IRemoteAccount fwwgSJHNfFpIAKHhbXceZaPrewDN;

		public PermissionState State
		{
			[CompilerGenerated]
			get
			{
				return mBcihcpJKRCDOWPPEGyNlEKQWROh;
			}
		}

		public PermissionDetail Detail
		{
			[CompilerGenerated]
			get
			{
				return QbRhEgaeLJEOqPFBjNCENnZIIaqIA;
			}
		}

		public ILocalAccount LocalUser
		{
			[CompilerGenerated]
			get
			{
				return GXXkBNATopFrlLVMcomITmalPZXJ;
			}
		}

		public IRemoteAccount TargetUser
		{
			[CompilerGenerated]
			get
			{
				return fwwgSJHNfFpIAKHhbXceZaPrewDN;
			}
		}

		public PermissionResult(TPlatformPermission permission, PermissionState state, PermissionDetail detail, ILocalAccount localUser, [CanBeNull] IRemoteAccount targetUser = null)
		{
			PlatformPermission = permission;
			mBcihcpJKRCDOWPPEGyNlEKQWROh = state;
			GXXkBNATopFrlLVMcomITmalPZXJ = localUser;
			fwwgSJHNfFpIAKHhbXceZaPrewDN = targetUser;
			QbRhEgaeLJEOqPFBjNCENnZIIaqIA = detail;
		}

		public PermissionResult(TPlatformPermission permission, PermissionState state, ILocalAccount localUser, [CanBeNull] IRemoteAccount targetUser = null)
			: this(permission, state, PermissionDetail.NoDetail, localUser, targetUser)
		{
		}
	}
}
