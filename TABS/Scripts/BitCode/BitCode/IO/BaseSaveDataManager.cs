using System;
using System.Threading.Tasks;
using BitCode.Users;

namespace BitCode.IO
{
	public abstract class BaseSaveDataManager<TAccount> : IPlatformService, ISaveDataManager<TAccount>, ISaveDataManager where TAccount : class, ILocalAccount
	{
		public abstract event Action<IPlatformService, Exception> InternalErrorOccurred;

		void ISaveDataManager.SaveData(ILocalAccount userAccount, string path, byte[] data)
		{
			SaveData((TAccount)userAccount, path, data);
		}

		Task ISaveDataManager.SaveDataAsync(ILocalAccount userAccount, string path, byte[] data)
		{
			return SaveDataAsync((TAccount)userAccount, path, data);
		}

		byte[] ISaveDataManager.LoadData(ILocalAccount userAccount, string path)
		{
			return LoadData((TAccount)userAccount, path);
		}

		Task<byte[]> ISaveDataManager.LoadDataAsync(ILocalAccount userAccount, string path)
		{
			return LoadDataAsync((TAccount)userAccount, path);
		}

		bool ISaveDataManager.DataExists(ILocalAccount userAccount, string path)
		{
			return DataExists((TAccount)userAccount, path);
		}

		Task<bool> ISaveDataManager.DataExistsAsync(ILocalAccount userAccount, string path)
		{
			return DataExistsAsync((TAccount)userAccount, path);
		}

		void ISaveDataManager.DeleteData(ILocalAccount userAccount, string path)
		{
			DeleteData((TAccount)userAccount, path);
		}

		Task ISaveDataManager.DeleteDataAsync(ILocalAccount userAccount, string path)
		{
			return DeleteDataAsync((TAccount)userAccount, path);
		}

		public abstract void SaveData(TAccount userAccount, string path, byte[] data);

		public abstract Task SaveDataAsync(TAccount userAccount, string path, byte[] data);

		public abstract byte[] LoadData(TAccount userAccount, string path);

		public abstract Task<byte[]> LoadDataAsync(TAccount userAccount, string path);

		public abstract bool DataExists(TAccount userAccount, string path);

		public abstract Task<bool> DataExistsAsync(TAccount userAccount, string path);

		public abstract void DeleteData(TAccount userAccount, string path);

		public abstract Task DeleteDataAsync(TAccount userAccount, string path);
	}
}
