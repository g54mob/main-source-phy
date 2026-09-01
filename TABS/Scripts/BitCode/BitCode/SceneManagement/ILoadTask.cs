namespace BitCode.SceneManagement
{
	public interface ILoadTask
	{
		float TaskProgress { get; }

		bool IsDone { get; }

		void Start(bool async);

		void Complete();
	}
}
