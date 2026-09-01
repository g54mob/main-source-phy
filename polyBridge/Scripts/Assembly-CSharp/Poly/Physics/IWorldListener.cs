namespace Poly.Physics
{
	public interface IWorldListener
	{
		void BeforeStep();

		void AfterWorldCleared();

		void AfterWorldFrameUpdate();

		void AfterWorldFixedUpdate();
	}
}
