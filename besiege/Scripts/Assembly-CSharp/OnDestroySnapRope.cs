using UnityEngine;
using UnityEngine.Serialization;

public class OnDestroySnapRope : MonoBehaviour
{
	[FormerlySerializedAs("springCode")]
	public BlockBehaviour block;

	private bool isApplicationQuitting;

	protected void Start()
	{
		if (object.ReferenceEquals(block, null))
		{
			Object.Destroy(this);
		}
	}

	protected void OnApplicationQuit()
	{
		isApplicationQuitting = true;
	}

	private void OnDisable()
	{
		if (!isApplicationQuitting && block != null && block.isSimulating && block.ParentMachine.isSimulating)
		{
			ISnapable snapable = block as ISnapable;
			snapable.Snap();
			Object.Destroy(this);
		}
	}
}
