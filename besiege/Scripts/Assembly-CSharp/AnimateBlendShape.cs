using System.Collections;
using UnityEngine;

public class AnimateBlendShape : MonoBehaviour
{
	public float duration = 1f;

	public SkinnedMeshRenderer mesh;

	private void Start()
	{
		StartCoroutine(Animate());
	}

	private IEnumerator Animate()
	{
		float time = 0f;
		while (time <= duration)
		{
			time += Time.deltaTime;
			mesh.SetBlendShapeWeight(0, time / duration * 100f);
			yield return null;
		}
		mesh.SetBlendShapeWeight(0, 100f);
		base.enabled = false;
	}
}
