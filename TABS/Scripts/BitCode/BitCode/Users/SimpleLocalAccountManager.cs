using System;
using System.Collections;
using System.Collections.Generic;

namespace BitCode.Users
{
	public class SimpleLocalAccountManager : IEnumerable<ILocalAccount>, IPlatformService, IEnumerable, ILocalAccountManager
	{
		private const string HAJMtJeukyqYnUbsolmgAOqLQfMu = "Default Account";

		private readonly SimpleLocalAccount[] GrLlUPtgKzueObloTNUijwvGrJRi;

		public int Count => GrLlUPtgKzueObloTNUijwvGrJRi.Length;

		public ILocalAccount this[int index] => GrLlUPtgKzueObloTNUijwvGrJRi[index];

		event Action<ILocalAccount> ILocalAccountManager.AccountAdded
		{
			add
			{
			}
			remove
			{
			}
		}

		event Action<ILocalAccount> ILocalAccountManager.AccountLeft
		{
			add
			{
			}
			remove
			{
			}
		}

		event Action<IPlatformService, Exception> IPlatformService.InternalErrorOccurred
		{
			add
			{
			}
			remove
			{
			}
		}

		event Action<Exception> ILocalAccountManager.AccountSignInFailed
		{
			add
			{
			}
			remove
			{
			}
		}

		public SimpleLocalAccountManager(string accountName = null)
		{
			GrLlUPtgKzueObloTNUijwvGrJRi = new SimpleLocalAccount[1]
			{
				new SimpleLocalAccount(0uL, accountName ?? "Default Account")
			};
		}

		public IEnumerator<ILocalAccount> GetEnumerator()
		{
			return ((IEnumerable<ILocalAccount>)GrLlUPtgKzueObloTNUijwvGrJRi).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GrLlUPtgKzueObloTNUijwvGrJRi.GetEnumerator();
		}

		public void PromptSignIn(SignInPromptOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
