using System;

namespace BitCode.Debug.TokenResolvers
{
	internal interface IEnumResolver : ITokenResolver
	{
		ITokenResolver GetEnumResolverForType(Type enumType);
	}
}
