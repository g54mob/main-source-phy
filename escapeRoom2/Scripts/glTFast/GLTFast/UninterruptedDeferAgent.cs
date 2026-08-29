using System.Threading.Tasks;

namespace GLTFast
{
	public class UninterruptedDeferAgent : IDeferAgent
	{
		public bool ShouldDefer()
		{
			return false;
		}

		public bool ShouldDefer(float duration)
		{
			return false;
		}

		public async Task BreakPoint()
		{
		}

		public async Task BreakPoint(float duration)
		{
		}
	}
}
