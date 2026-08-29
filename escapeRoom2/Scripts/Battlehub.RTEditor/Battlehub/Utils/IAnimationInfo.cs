namespace Battlehub.Utils
{
	public interface IAnimationInfo
	{
		float Duration { get; }

		float T { get; set; }

		float Delay { get; set; }

		bool InProgress { get; }

		void Abort();
	}
}
