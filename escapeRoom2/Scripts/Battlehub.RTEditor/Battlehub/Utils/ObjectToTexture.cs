using System;
using UnityEngine;

namespace Battlehub.Utils
{
	public class ObjectToTexture : MonoBehaviour
	{
		public Camera Camera;

		[HideInInspector]
		public int objectImageLayer;

		public bool DestroyScripts = true;

		public int snapshotTextureWidth = 128;

		public int snapshotTextureHeight = 128;

		public Vector3 defaultPosition = new Vector3(0f, 0f, 0f);

		public Vector3 defaultRotation = new Vector3(26f, 135f, -24f);

		public Vector3 defaultScale = new Vector3(1f, 1f, 1f);

		private void Awake()
		{
			if (Camera == null)
			{
				Camera = GetComponent<Camera>();
			}
			Camera.enabled = false;
		}

		private void SetLayerRecursively(GameObject o, int layer)
		{
			Transform[] componentsInChildren = o.GetComponentsInChildren<Transform>(includeInactive: true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = layer;
			}
		}

		public Texture2D TakeObjectSnapshot(GameObject prefab, GameObject fallback)
		{
			return TakeObjectSnapshot(prefab, fallback, defaultPosition, Quaternion.Euler(defaultRotation), defaultScale, 1f);
		}

		public Texture2D TakeObjectSnapshot(GameObject prefab, GameObject fallback, Vector3 position)
		{
			return TakeObjectSnapshot(prefab, fallback, position, Quaternion.Euler(defaultRotation), defaultScale, 1f);
		}

		public Texture2D TakeObjectSnapshot(GameObject prefab, GameObject fallback, Vector3 position, Quaternion rotation, Vector3 scale)
		{
			return TakeObjectSnapshot(prefab, fallback, position, Quaternion.Euler(defaultRotation), scale, 1f);
		}

		public Texture2D TakeObjectSnapshot(GameObject prefab, GameObject fallback, Vector3 position, Quaternion rotation, Vector3 scale, float previewScale, bool instantiate = true)
		{
			if (Camera == null)
			{
				throw new InvalidOperationException("Object Image Camera must be set");
			}
			if (objectImageLayer < 0 || objectImageLayer > 31)
			{
				throw new InvalidOperationException("Object Image Layer must specify a valid layer between 0 and 31");
			}
			bool activeSelf = prefab.activeSelf;
			Vector3 position2 = prefab.transform.position;
			Vector3 eulerAngles = prefab.transform.eulerAngles;
			Vector3 localScale = prefab.transform.localScale;
			int layer = prefab.layer;
			Transform parent = null;
			GameObject gameObject;
			Renderer[] array;
			if (instantiate)
			{
				prefab.SetActive(value: false);
				gameObject = UnityEngine.Object.Instantiate(prefab, position, rotation * Quaternion.Inverse(prefab.transform.rotation));
				if (DestroyScripts)
				{
					MonoBehaviour[] componentsInChildren = gameObject.GetComponentsInChildren<MonoBehaviour>(includeInactive: true);
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						if (!(componentsInChildren[i] == null) && !componentsInChildren[i].GetType().FullName.StartsWith("UnityEngine"))
						{
							UnityEngine.Object.DestroyImmediate(componentsInChildren[i]);
						}
					}
				}
				prefab.SetActive(activeSelf);
				array = gameObject.GetComponentsInChildren<Renderer>(includeInactive: false);
				if (array.Length == 0 && fallback != null)
				{
					UnityEngine.Object.DestroyImmediate(gameObject);
					gameObject = UnityEngine.Object.Instantiate(fallback, position, rotation);
					array = new Renderer[1] { fallback.GetComponentInChildren<Renderer>(includeInactive: false) };
				}
			}
			else
			{
				gameObject = prefab;
				gameObject.SetActive(value: true);
				parent = gameObject.transform.parent;
				gameObject.transform.SetParent(null, worldPositionStays: false);
				gameObject.transform.position = position;
				gameObject.transform.rotation = rotation;
				array = gameObject.GetComponentsInChildren<Renderer>(includeInactive: false);
			}
			Texture2D texture2D = null;
			if (array.Length != 0)
			{
				gameObject.transform.localScale = scale;
				Bounds bounds = gameObject.CalculateBounds();
				float num = Camera.fieldOfView * (MathF.PI / 180f);
				float num2 = Mathf.Max(bounds.extents.y, bounds.extents.x, bounds.extents.z);
				float num3 = Mathf.Abs(num2 / Mathf.Sin(num / 2f));
				gameObject.transform.localScale = scale * previewScale;
				gameObject.SetActive(value: true);
				for (int j = 0; j < array.Length; j++)
				{
					array[j].gameObject.SetActive(value: true);
				}
				position += bounds.center;
				Camera.transform.position = position - num3 * Camera.transform.forward;
				Camera.orthographicSize = num2;
				SetLayerRecursively(gameObject, objectImageLayer);
				Camera.targetTexture = RenderTexture.GetTemporary(snapshotTextureWidth, snapshotTextureHeight, 24);
				Camera.enabled = true;
				Camera.Render();
				Camera.enabled = false;
				RenderTexture active = RenderTexture.active;
				RenderTexture.active = Camera.targetTexture;
				texture2D = new Texture2D(Camera.targetTexture.width, Camera.targetTexture.height);
				texture2D.ReadPixels(new Rect(0f, 0f, Camera.targetTexture.width, Camera.targetTexture.height), 0, 0);
				texture2D.Apply();
				RenderTexture.active = active;
				RenderTexture.ReleaseTemporary(Camera.targetTexture);
			}
			if (instantiate)
			{
				UnityEngine.Object.DestroyImmediate(gameObject);
			}
			else
			{
				gameObject.SetActive(activeSelf);
				gameObject.transform.SetParent(parent, worldPositionStays: false);
				gameObject.transform.position = position2;
				gameObject.transform.eulerAngles = eulerAngles;
				gameObject.transform.localScale = localScale;
				SetLayerRecursively(gameObject, layer);
			}
			return texture2D;
		}
	}
}
