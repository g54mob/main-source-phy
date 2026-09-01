using System;
using System.Collections;
using System.Collections.Generic;

namespace BitCode.Users
{
	public interface ILocalAccountManager : IEnumerable<ILocalAccount>, IPlatformService, IEnumerable
	{
		int Count { get; }

		ILocalAccount this[int index] { get; }

		event Action<ILocalAccount> AccountAdded;

		event Action<ILocalAccount> AccountLeft;

		event Action<Exception> AccountSignInFailed;

		void PromptSignIn(SignInPromptOptions options);
	}
}
