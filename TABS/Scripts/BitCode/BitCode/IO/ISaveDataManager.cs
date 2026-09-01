using System.Threading.Tasks;
using BitCode.Users;

namespace BitCode.IO
{
	public interface ISaveDataManager : IPlatformService
	{
		void SaveData(ILocalAccount userAccount, string path, byte[] data);

		Task SaveDataAsync(ILocalAccount userAccount, string path, byte[] data);

		byte[] LoadData(ILocalAccount userAccount, string path);

		Task<byte[]> LoadDataAsync(ILocalAccount userAccount, string path);

		bool DataExists(ILocalAccount userAccount, string path);

		Task<bool> DataExistsAsync(ILocalAccount userAccount, string path);

		void DeleteData(ILocalAccount userAccount, string path);

		Task DeleteDataAsync(ILocalAccount userAccount, string path);
	}
	public interface ISaveDataManager<in TAccount> : IPlatformService, ISaveDataManager where TAccount : class, ILocalAccount
	{
		void SaveData(TAccount userAccount, string path, byte[] data);

		Task SaveDataAsync(TAccount userAccount, string path, byte[] data);

		byte[] LoadData(TAccount userAccount, string path);

		Task<byte[]> LoadDataAsync(TAccount userAccount, string path);

		bool DataExists(TAccount userAccount, string path);

		Task<bool> DataExistsAsync(TAccount userAccount, string path);

		void DeleteData(TAccount userAccount, string path);

		Task DeleteDataAsync(TAccount userAccount, string path);
	}
}
