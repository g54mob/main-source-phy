using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace BitCode.Debug.TokenResolvers
{
	[UsedImplicitly]
	public interface ITokenResolver
	{
		bool NeedsUserToken { get; }

		Type ResolverType { get; }

		void Register(DebugConsole debugConsole);

		bool TryResolve(IReadOnlyList<string> tokens, ref int lastConsumedTokenIndex, out object resolvedToken);
	}
}
