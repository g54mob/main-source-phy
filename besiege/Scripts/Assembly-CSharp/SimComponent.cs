using UnityEngine;

public abstract class SimComponent : MonoBehaviour
{
	protected bool isInitialized;

	public virtual void Init(Machine machine, BlockBehaviour block)
	{
		isInitialized = true;
	}
}
