using UnityEngine;

public class SetMaxDepthBlurFromRes : MonoBehaviour
{
	public DepthOfFieldScatter dof;

	private void Start()
	{
		float num = (float)Screen.height / 1080f;
		dof.maxBlurSize *= num;
	}
}
