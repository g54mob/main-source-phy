using System;
using BitCode.Threading;
using BitCode.Users;

namespace BitCode.IO
{
	public static class SaveDataManagerExtensions
	{
		public static void SaveDataAsync(this ISaveDataManager saveDataManager, ILocalAccount userAccount, string path, byte[] data, Action<Exception> onCompleted)
		{
			saveDataManager.SaveDataAsync(userAccount, path, data).ContinueWithAsync(onCompleted);
		}

		public static void LoadDataAsync(this ISaveDataManager saveDataManager, ILocalAccount userAccount, string path, Action<byte[], Exception> onCompleted)
		{
			saveDataManager.LoadDataAsync(userAccount, path).ContinueWithAsync(onCompleted);
		}

		public static void DataExistsAsync(this ISaveDataManager saveDataManager, ILocalAccount userAccount, string path, Action<bool, Exception> onCompleted)
		{
			saveDataManager.DataExistsAsync(userAccount, path).ContinueWithAsync(onCompleted);
		}

		public static void DeleteDataAsync(this ISaveDataManager saveDataManager, ILocalAccount userAccount, string path, Action<Exception> onCompleted)
		{
			saveDataManager.DeleteDataAsync(userAccount, path).ContinueWithAsync(onCompleted);
		}

		public static void SaveDataAsync<TAccount>(this ISaveDataManager<TAccount> saveDataManager, TAccount userAccount, string path, byte[] data, Action<Exception> onCompleted) where TAccount : class, ILocalAccount
		{
			saveDataManager.SaveDataAsync(userAccount, path, data).ContinueWithAsync(onCompleted);
		}

		public static void LoadDataAsync<TAccount>(this ISaveDataManager<TAccount> saveDataManager, TAccount userAccount, string path, Action<byte[], Exception> onCompleted) where TAccount : class, ILocalAccount
		{
			saveDataManager.LoadDataAsync(userAccount, path).ContinueWithAsync(onCompleted);
		}

		public static void DataExistsAsync<TAccount>(this ISaveDataManager<TAccount> saveDataManager, TAccount userAccount, string path, Action<bool, Exception> onCompleted) where TAccount : class, ILocalAccount
		{
			saveDataManager.DataExistsAsync(userAccount, path).ContinueWithAsync(onCompleted);
		}

		public static void DeleteDataAsync<TAccount>(this ISaveDataManager<TAccount> saveDataManager, TAccount userAccount, string path, Action<Exception> onCompleted) where TAccount : class, ILocalAccount
		{
			saveDataManager.DeleteDataAsync(userAccount, path).ContinueWithAsync(onCompleted);
		}
	}
}
