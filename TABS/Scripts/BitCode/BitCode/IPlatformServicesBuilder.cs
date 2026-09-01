using System.Threading.Tasks;

namespace BitCode
{
	public interface IPlatformServicesBuilder
	{
		Task<IPlatformServices> Build();
	}
}
