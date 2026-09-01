using UnityEngine;

public class SetTextureOnStart : MonoBehaviour
{
	public Texture2D myTex;

	private void Start()
	{
		GetComponent<Renderer>().material.mainTexture = myTex;
	}
}
