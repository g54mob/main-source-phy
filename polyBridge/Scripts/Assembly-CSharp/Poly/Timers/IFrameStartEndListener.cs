namespace Poly.Timers
{
	public interface IFrameStartEndListener
	{
		void OnAwakeBegin();

		void OnAwakeEnd();

		void OnStartBegin();

		void OnStartEnd();

		void OnUpdateBegin();

		void OnUpdateEnd();

		void OnLateUpdateBegin();

		void OnLateUpdateEnd();

		void OnFixedUpdateBegin();

		void OnFixedUpdateEnd();
	}
}
