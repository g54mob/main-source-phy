using System.Threading.Tasks;

namespace BitCode
{
	internal interface IInitializablePlatformService : IPlatformService
	{
		Task Initialize();
	}
}
