using UnityEngine;

public class LocImageChanger : MonoBehaviour
{
	public Renderer myRenderer;

	public Texture2D english;

	private void Start()
	{
		myRenderer.material.mainTexture = english;
	}
}
