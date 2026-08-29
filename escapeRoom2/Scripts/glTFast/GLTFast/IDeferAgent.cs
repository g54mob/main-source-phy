using System.Threading.Tasks;

namespace GLTFast
{
	public interface IDeferAgent
	{
		bool ShouldDefer();

		bool ShouldDefer(float duration);

		Task BreakPoint();

		Task BreakPoint(float duration);
	}
}
