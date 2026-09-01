using UnityEngine;

public class ChangeRenderQueue : MonoBehaviour
{
	[SerializeField]
	private int newRenderQueueValue;

	private void Start()
	{
		GetComponent<MeshRenderer>().material.renderQueue = newRenderQueueValue;
	}
}
