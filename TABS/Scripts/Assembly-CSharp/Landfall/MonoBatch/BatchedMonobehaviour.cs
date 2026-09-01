using UnityEngine;

namespace Landfall.MonoBatch
{
	public class BatchedMonobehaviour : MonoBehaviour
	{
		private MonoBatchService m_manager;

		protected virtual void Start()
		{
			m_manager = ServiceLocator.GetService<MonoBatchService>();
			m_manager.AddBatchedBehaviour(this);
		}

		protected virtual void OnDestroy()
		{
			if (m_manager != null)
			{
				m_manager.RemoveBatchedBehaviour(this);
			}
		}

		public virtual void BatchedUpdate()
		{
		}

		public virtual void BatchedFixedUpdate()
		{
		}
	}
}
