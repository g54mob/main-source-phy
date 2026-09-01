using UnityEngine;

public class ChangeTextureOnTencent : MonoBehaviour
{
	public string propertyName = "_MainTex";

	public Texture tex;

	public Renderer render;

	public void Awake()
	{
		Object.Destroy(this);
	}
}
