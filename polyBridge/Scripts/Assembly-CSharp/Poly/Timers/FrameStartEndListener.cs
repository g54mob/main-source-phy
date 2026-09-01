using UnityEngine;

namespace Poly.Timers
{
	public class FrameStartEndListener : MonoBehaviour, IFrameStartEndListener
	{
		public virtual void OnAwakeBegin()
		{
		}

		public virtual void OnAwakeEnd()
		{
		}

		public virtual void OnStartBegin()
		{
		}

		public virtual void OnStartEnd()
		{
		}

		public virtual void OnUpdateBegin()
		{
		}

		public virtual void OnUpdateEnd()
		{
		}

		public virtual void OnLateUpdateBegin()
		{
		}

		public virtual void OnLateUpdateEnd()
		{
		}

		public virtual void OnFixedUpdateBegin()
		{
		}

		public virtual void OnFixedUpdateEnd()
		{
		}
	}
}
