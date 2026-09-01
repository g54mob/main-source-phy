using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/VFX/PanningTexture")]
	public class MMPanningTexture : MonoBehaviour
	{
		[MMInformation("This script will let you pan a texture on an attached Renderer.", MMInformationAttribute.InformationType.Info, false)]
		public bool TextureShouldPan;

		public Vector2 Speed;

		public string SortingLayerName;

		[Tooltip("the property name, for example _MainTex")]
		public string MaterialPropertyName;

		[Tooltip("the index of the material")]
		public int MaterialIndex;

		protected RawImage _rawImage;

		protected Renderer _renderer;

		protected Vector4 _position;

		protected Vector4 _speed;

		protected MaterialPropertyBlock _propertyBlock;

		protected virtual void Start()
		{
		}

		protected virtual void Update()
		{
		}
	}
}
