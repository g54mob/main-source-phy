using UnityEngine;
using UnityEngine.UI;

namespace SRF.UI
{
	[AddComponentMenu("SRF/UI/Retina Scaler")]
	[RequireComponent(typeof(CanvasScaler))]
	public class SRRetinaScaler : SRMonoBehaviour
	{
		[SerializeField]
		private float _retinaScale = 2f;

		[SerializeField]
		private int _thresholdDpi = 250;

		[SerializeField]
		private bool _disablePixelPerfect;

		public int ThresholdDpi
		{
			get
			{
				return _thresholdDpi;
			}
		}

		public float RetinaScale
		{
			get
			{
				return _retinaScale;
			}
		}

		private void Start()
		{
			float dpi = Screen.dpi;
			if (!(dpi <= 0f) && dpi > (float)ThresholdDpi)
			{
				CanvasScaler component = GetComponent<CanvasScaler>();
				component.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
				component.scaleFactor = RetinaScale;
				if (_disablePixelPerfect)
				{
					GetComponent<Canvas>().pixelPerfect = false;
				}
			}
		}
	}
}
