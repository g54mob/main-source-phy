using System;
using BitCode.IO;
using BitCode.Users;
using JetBrains.Annotations;

namespace BitCode
{
	public static class PlatformServiceFactoryExtensions
	{
		private sealed class MmMddbElbNMpnvblkPLjGsZGYmgvB<_0001> where _0001 : IFactoryRegisteringPlatformServicesBuilder
		{
			public IIOWrapper nmbOKVziVWTdRjMSGwUgJZHrVFmi;

			public string feHqeyQhMnSGzVlZDCJSZzQIFzQf;

			internal SimpleSaveDataManager dLuEIIIgipPtNZGBcPWFqARSLKvLA()
			{
				nmbOKVziVWTdRjMSGwUgJZHrVFmi = nmbOKVziVWTdRjMSGwUgJZHrVFmi ?? new DotNetIO();
				return new SimpleSaveDataManager(nmbOKVziVWTdRjMSGwUgJZHrVFmi, feHqeyQhMnSGzVlZDCJSZzQIFzQf);
			}
		}

		private sealed class ZXjJJXdfjUCYdxswWWjOSJImoRgO<_0001> where _0001 : IFactoryRegisteringPlatformServicesBuilder
		{
			public string JCueHKcxCMDeAQCseIbWjKyhXFDFA;

			internal SimpleLocalAccountManager bxhcTHIbtUTGasIzqOBONxOacYmd()
			{
				return new SimpleLocalAccountManager(JCueHKcxCMDeAQCseIbWjKyhXFDFA);
			}
		}

		internal static _0001 OHyEPelkUDDIkjiFZiHpcMsjfyoxB<_0001, _0002>(this _0001 P_0, [NotNull] Func<_0002> P_1) where _0001 : IFactoryRegisteringPlatformServicesBuilder where _0002 : class, IPlatformService
		{
			P_0.RegisterFactory(P_1);
			return P_0;
		}

		public static TPlatformServiceBuilder WithSimpleSave<TPlatformServiceBuilder>(this TPlatformServiceBuilder builder, [NotNull] string basePath, IIOWrapper ioWrapper = null) where TPlatformServiceBuilder : IFactoryRegisteringPlatformServicesBuilder
		{
			MmMddbElbNMpnvblkPLjGsZGYmgvB<TPlatformServiceBuilder> mmMddbElbNMpnvblkPLjGsZGYmgvB = new MmMddbElbNMpnvblkPLjGsZGYmgvB<TPlatformServiceBuilder>();
			mmMddbElbNMpnvblkPLjGsZGYmgvB.nmbOKVziVWTdRjMSGwUgJZHrVFmi = ioWrapper;
			mmMddbElbNMpnvblkPLjGsZGYmgvB.feHqeyQhMnSGzVlZDCJSZzQIFzQf = basePath;
			builder.OHyEPelkUDDIkjiFZiHpcMsjfyoxB(mmMddbElbNMpnvblkPLjGsZGYmgvB.dLuEIIIgipPtNZGBcPWFqARSLKvLA);
			return builder;
		}

		public static TPlatformServiceBuilder WithSimpleAccounts<TPlatformServiceBuilder>(this TPlatformServiceBuilder builder, string accountName = null) where TPlatformServiceBuilder : IFactoryRegisteringPlatformServicesBuilder
		{
			ZXjJJXdfjUCYdxswWWjOSJImoRgO<TPlatformServiceBuilder> zXjJJXdfjUCYdxswWWjOSJImoRgO = new ZXjJJXdfjUCYdxswWWjOSJImoRgO<TPlatformServiceBuilder>();
			while (true)
			{
				int num = 986064082;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x1CE3AE93)) % 3)
					{
					case 0u:
						break;
					case 2u:
						goto IL_0028;
					default:
						return builder;
					}
					break;
					IL_0028:
					zXjJJXdfjUCYdxswWWjOSJImoRgO.JCueHKcxCMDeAQCseIbWjKyhXFDFA = accountName;
					builder.OHyEPelkUDDIkjiFZiHpcMsjfyoxB(zXjJJXdfjUCYdxswWWjOSJImoRgO.bxhcTHIbtUTGasIzqOBONxOacYmd);
					num = ((int)num2 * -1529350318) ^ 0x49FE498F;
				}
			}
		}
	}
}
