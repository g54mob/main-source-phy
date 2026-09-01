using System;
using System.Collections.Generic;
using System.Reflection;
using BitCode.Debug.TokenResolvers;
using JetBrains.Annotations;

namespace BitCode.Debug
{
	public interface IParameterResolver
	{
		object ResolveParameter([NotNull] ParameterInfo parameter, [NotNull] IReadOnlyList<string> tokens, ref int lastUsedTokenIndex);

		bool HasResolverForType([NotNull] Type type);

		bool HasResolverForType<T>();

		ITokenResolver GetResolverForType([NotNull] Type type);
	}
}
