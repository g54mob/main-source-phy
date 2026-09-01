using System;
using System.Collections.Generic;

namespace BitCode.Debug.TokenResolvers
{
	public abstract class TokenResolverBase<T> : ITokenResolver
	{
		protected DebugConsole owningConsole;

		public Type ResolverType => typeof(T);

		public virtual bool NeedsUserToken => true;

		public virtual void Register(DebugConsole debugConsole)
		{
			owningConsole = debugConsole;
		}

		public abstract bool TryResolve(IReadOnlyList<string> tokens, ref int lastConsumedTokenIndex, out object resolvedToken);
	}
}
