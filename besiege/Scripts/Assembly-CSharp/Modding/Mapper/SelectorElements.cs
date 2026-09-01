using InternalModding.Mapper;
using UnityEngine;

namespace Modding.Mapper
{
	public class SelectorElements
	{
		private CustomSelectorReferences references;

		internal SelectorElements(CustomSelectorReferences refs)
		{
			references = refs;
		}

		public DynamicText MakeText(Vector3 position, string content, float fontSize = 0.175f)
		{
			Transform transform = Object.Instantiate(references.TextTemplate).transform;
			transform.parent = references.Content;
			transform.localPosition = position;
			DynamicText component = transform.GetComponent<DynamicText>();
			component.size = fontSize;
			component.SetText(content);
			transform.gameObject.SetActive(true);
			return component;
		}

		public Transform MakeBox(Vector3 position, Vector2 size, Material material)
		{
			Transform transform = Object.Instantiate(references.BoxTemplate).transform;
			transform.parent = references.Content;
			transform.localScale = new Vector3(size.x, size.y, 1f);
			transform.localPosition = position;
			MeshRenderer component = transform.GetComponent<MeshRenderer>();
			component.material = material;
			transform.gameObject.SetActive(true);
			return transform;
		}

		public MeshRenderer MakeTexture(Vector3 position, Vector2 size, Texture texture)
		{
			Transform transform = Object.Instantiate(references.TextureTemplate).transform;
			transform.parent = references.Content;
			transform.localScale = new Vector3(size.x, size.y, 1f);
			transform.localPosition = new Vector3(position.x, position.y, -0.1f);
			MeshRenderer component = transform.GetComponent<MeshRenderer>();
			component.material.mainTexture = texture;
			transform.gameObject.SetActive(true);
			return component;
		}

		public MeshRenderer MakeTexture(Vector3 position, Vector2 size, ModTexture texture)
		{
			MeshRenderer renderer = MakeTexture(position, size, (Texture)null);
			texture.OnLoad += delegate
			{
				renderer.material.mainTexture = (Texture2D)texture;
			};
			return renderer;
		}

		public UIButton AddButton(Transform t)
		{
			GameObject gameObject = t.gameObject;
			if (!gameObject.GetComponent<BoxCollider>())
			{
				gameObject.AddComponent<BoxCollider>();
			}
			UIButton uIButton = gameObject.AddComponent<UIButton>();
			uIButton.mask = 1;
			return uIButton;
		}

		public void ScaleOnMouse(Transform hover, Transform toScale)
		{
			hover.gameObject.SetActive(false);
			if (!hover.GetComponent<Collider>())
			{
				hover.gameObject.AddComponent<BoxCollider>();
			}
			ScaleOnMouseOver scaleOnMouseOver = hover.gameObject.AddComponent<ScaleOnMouseOver>();
			scaleOnMouseOver.objToScale = toScale;
			scaleOnMouseOver.sizeScaler = 1.1f;
			scaleOnMouseOver.mousePressedScale = 0.9f;
			scaleOnMouseOver.lerpSpeed = 0.1f;
			scaleOnMouseOver.mask = 1;
			hover.gameObject.SetActive(true);
		}
	}
}
