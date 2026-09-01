using UnityEngine;

public class CodeAnimationUV : MonoBehaviour
{
	private CodeAnimation anim;

	private Renderer rend;

	public Vector2 offset;

	private void Start()
	{
		anim = GetComponent<CodeAnimation>();
		rend = GetComponent<Renderer>();
		anim.AddAnimationChangeAction(UpdateAnim);
	}

	private void UpdateAnim()
	{
		rend.material.SetTextureOffset("_MainTex", offset * anim.animationValue);
	}
}
