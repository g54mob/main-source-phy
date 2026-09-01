using UnityEngine;

public class TriggerEnterHook : MonoBehaviour
{
	public bool ignoreTriggers = true;

	public event TriggerEntered TriggerEntered;

	public virtual void OnTriggerEnter(Collider col)
	{
		if ((!col.isTrigger || !ignoreTriggers) && this.TriggerEntered != null)
		{
			this.TriggerEntered(col);
		}
	}

	public virtual void OnTriggerExit(Collider col)
	{
		if ((!col.isTrigger || !ignoreTriggers) && this.TriggerEntered != null)
		{
			this.TriggerEntered(null);
		}
	}
}
