using System;
using BitCode.Purchasing;

namespace TFBGames
{
	public interface IPurchaseManagerService : IPurchaseManager, IService
	{
		string AprilFoolsBugsProductId { get; }

		void BuyProductAsync(string productId, Action<string, Exception> doneCallback);
	}
}
