using UnityEngine;

public class RainFollowCam : MonoBehaviour
{
	public Transform objToFollow;

	public Transform myTransform;

	private void Update()
	{
		myTransform.position = objToFollow.position;
	}
}
