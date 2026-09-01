using UnityEngine;

namespace LevelCreator
{
	public class UVScroller : MonoBehaviour
	{
		public Vector2 speed;

		public string texturePropertyName = "_MainTex";

		public string xOffsetName;

		public string yOffsetName;

		private Vector2 offset;

		private Material mat;

		private void Start()
		{
			mat = GetComponent<Renderer>().material;
		}

		private void Update()
		{
			offset += speed * Time.deltaTime;
			if (!string.IsNullOrEmpty(texturePropertyName))
			{
				mat.SetTextureOffset(texturePropertyName, offset);
			}
			if (!string.IsNullOrEmpty(xOffsetName))
			{
				mat.SetFloat(xOffsetName, offset.x);
			}
			if (!string.IsNullOrEmpty(yOffsetName))
			{
				mat.SetFloat(yOffsetName, offset.y);
			}
		}
	}
}
