using UnityEngine;

namespace CodeAnimo
{
	public class TextureDebugTest : MonoBehaviour
	{
		[TextureDebug]
		[SerializeField]
		protected Texture2D testTexture;

		private void Start()
		{
		}

		private void Update()
		{
		}
	}
}
