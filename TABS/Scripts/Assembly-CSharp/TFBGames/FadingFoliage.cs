using BitCode;
using UnityEngine;

namespace TFBGames
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Renderer))]
	[ExecuteInEditMode]
	public class FadingFoliage : MonoBehaviour
	{
		private Renderer foliageRenderer;

		private float maxFadeDistance;

		private SettingsProfileManager profileManager;

		private Texture stippleTexture;

		private Material sharedMat;

		public void RecalculateFadeDistance()
		{
			maxFadeDistance = sharedMat.GetFloat(FoliageManager.FadeOutDistance) + sharedMat.GetFloat(FoliageManager.FadeOutRange);
		}

		protected virtual void Start()
		{
			foliageRenderer = GetComponent<Renderer>();
			sharedMat = foliageRenderer.sharedMaterial;
			stippleTexture = sharedMat.GetTexture(FoliageManager.StippleNoise);
			FoliageManager instance = Singleton<FoliageManager>.Instance;
			if (instance != null)
			{
				instance.RegisterFoliageMaterial(this, sharedMat);
			}
			RecalculateFadeDistance();
		}

		protected virtual void Update()
		{
			if (!(MainCam.instance == null))
			{
				Vector3 position = MainCam.instance.m_camera.transform.position;
				Vector3 vector = foliageRenderer.bounds.ClosestPoint(position);
				Vector3 vector2 = position - vector;
				float num = Mathf.Abs(vector2.x) + Mathf.Abs(vector2.y) + Mathf.Abs(vector2.z);
				foliageRenderer.enabled = num < maxFadeDistance;
			}
		}

		protected void OnWillRenderObject()
		{
			Camera current = Camera.current;
			if (current != null && stippleTexture != null)
			{
				Vector2 vector = new Vector2((float)current.pixelWidth / (float)stippleTexture.width, (float)current.pixelHeight / (float)stippleTexture.height);
				sharedMat.SetVector(FoliageManager.ScreenToNoiseRatio, vector);
			}
		}
	}
}
