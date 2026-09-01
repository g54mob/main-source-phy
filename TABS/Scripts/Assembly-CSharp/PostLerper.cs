using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostLerper : MonoBehaviour
{
	public float dofAmount;

	public float speed = 5f;

	private DepthOfField dof;

	private PostProcessProfile profile;

	private void Start()
	{
		profile = GetComponent<PostProcessVolume>().sharedProfile;
	}

	private void Update()
	{
		profile.TryGetSettings<DepthOfField>(out dof);
		dof.aperture.value = Mathf.Lerp(dof.aperture.value, Mathf.Lerp(0f, 32f, 1f - dofAmount), Time.unscaledDeltaTime * speed);
	}
}
