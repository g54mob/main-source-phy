using System;
using BitCode.Users;

namespace BitCode.Purchasing
{
	public interface IPurchaseManager
	{
		bool SupportsRestorePurchases { get; }

		event Action<string> PurchasedProduct;

		void PurchaseProductAsync(ILocalAccount userAccount, string productId, Action<string, Exception> doneCallback);

		void GetPurchasesAsync(ILocalAccount userAccount, Action<string[], Exception> doneCallback);

		void RestorePurchasesAsync(ILocalAccount userAccount, Action<Exception> doneCallback);
	}
}
