using System;
using System.Collections.Generic;
using UnityEngine;

namespace Landfall
{
	public class BlobShadowService : ServicePrefab
	{
		[Serializable]
		public class BlobShadowSettings
		{
			public GameObject ShadowMesh;

			public Vector3 Scale = Vector3.one;
		}

		[Tooltip("Default blob shadow settings for all shadow casters that do not explicitly override settings.")]
		[SerializeField]
		private BlobShadowSettings defaultBlobShadowSettings;

		[Tooltip("Forces shadows to point in World Up direction")]
		[SerializeField]
		private bool forceAlignUp = true;

		[SerializeField]
		private float castDistance = 5f;

		[SerializeField]
		private float offset = 0.001f;

		[SerializeField]
		private LayerMask allowedLayers = (int)(LayerMask)1 << 9;

		[Tooltip("Enables/Disables all blob shadows")]
		[SerializeField]
		private bool showShadows = true;

		private Dictionary<Transform, GameObject> shadowCasters = new Dictionary<Transform, GameObject>();

		protected void Update()
		{
			Dictionary<Transform, GameObject>.Enumerator enumerator = shadowCasters.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Transform key = enumerator.Current.Key;
				GameObject value = enumerator.Current.Value;
				RaycastHit hitInfo;
				if (!showShadows)
				{
					value.SetActive(value: false);
				}
				else if (Physics.Raycast(key.position, Vector3.down, out hitInfo, castDistance, allowedLayers))
				{
					float y = hitInfo.point.y;
					Vector3 vector = -hitInfo.normal;
					value.SetActive(value: true);
					value.transform.position = new Vector3(key.position.x, y + offset, key.position.z);
					value.transform.up = (forceAlignUp ? Vector3.up : vector);
				}
				else
				{
					value.SetActive(value: false);
				}
			}
		}

		public void AddShadowCaster(Transform caster, BlobShadowSettings overrideSettings = null)
		{
			if (!shadowCasters.ContainsKey(caster))
			{
				GameObject value = InitShadow(overrideSettings);
				shadowCasters.Add(caster, value);
			}
		}

		public void RemoveShadowCaster(Transform casterToRemove)
		{
			if (shadowCasters.TryGetValue(casterToRemove, out var value))
			{
				UnityEngine.Object.Destroy(value);
				shadowCasters.Remove(casterToRemove);
			}
		}

		private GameObject InitShadow(BlobShadowSettings overrideSettings = null)
		{
			BlobShadowSettings blobShadowSettings = ((overrideSettings == null) ? defaultBlobShadowSettings : overrideSettings);
			GameObject obj = UnityEngine.Object.Instantiate((UnityEngine.Object)((blobShadowSettings.ShadowMesh == null) ? defaultBlobShadowSettings.ShadowMesh : blobShadowSettings.ShadowMesh), base.transform) as GameObject;
			obj.transform.localScale = blobShadowSettings.Scale;
			return obj;
		}
	}
}
