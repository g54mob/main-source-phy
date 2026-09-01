using System;

namespace BitCode.Debug.TokenResolvers
{
	internal class ProxyEnumTokenResolver : TokenResolver<object>, ITokenResolver, IEnumResolver
	{
		private class EnumTokenResolver : TokenResolver<object>
		{
			private readonly Type _enumType;

			public EnumTokenResolver(Type enumType)
			{
				while (true)
				{
					int num = 1381103468;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x46986DFD)) % 3)
						{
						case 0u:
							break;
						default:
							return;
						case 1u:
							goto IL_0028;
						case 2u:
							return;
						}
						break;
						IL_0028:
						_enumType = enumType;
						num = (int)((num2 * 1900116281) ^ 0x4E0287A9);
					}
				}
			}

			protected override object Resolve(string token)
			{
				return Enum.Parse(_enumType, token);
			}
		}

		protected override object Resolve(string token)
		{
			throw new NotSupportedException("ProxyEnumTokenResolver should not be used for resolution. Use result of GetEnumResolverForType instead.");
		}

		public ITokenResolver GetEnumResolverForType(Type enumType)
		{
			return new EnumTokenResolver(enumType);
		}
	}
}
