using System;
using System.Collections;
using Besiege;
using InternalModding.Misc;
using UnityEngine;

namespace Modding
{
	public class ModTexture : ModResource
	{
		private bool _hasError;

		private string _error = string.Empty;

		private bool _loaded;

		public override bool HasError
		{
			get
			{
				return _hasError;
			}
		}

		public override string Error
		{
			get
			{
				return _error;
			}
		}

		public override bool Loaded
		{
			get
			{
				return _loaded;
			}
		}

		public Texture2D Texture { get; private set; }

		internal ModTexture()
		{
		}

		internal override IEnumerator Load()
		{
			AssetImporter.LoadingObject lObj = AssetImporter.StartImport.Texture(base.Info.Path, null, base.Info.UseMipMaps, !base.Info.Readable);
			yield return lObj.routine;
			Texture = lObj.tex;
			_loaded = true;
			_hasError = !string.IsNullOrEmpty(lObj.texError);
			_error = lObj.texError;
			TriggerOnLoad();
		}

		public void SetOnObject(GameObject go, Material mat, Action<GameObject> postSetAction = null, Action prefabPostSetAction = null)
		{
			SetOnObject(go, delegate(GameObject obj)
			{
				Renderer component = obj.GetComponent<Renderer>();
				if (!(component == null))
				{
					component.material = mat;
					component.material.mainTexture = Texture;
					if (postSetAction != null)
					{
						postSetAction(obj);
					}
				}
			}, prefabPostSetAction);
		}

		internal override void ApplyToObject(GameObject go)
		{
			Renderer component = go.GetComponent<Renderer>();
			if (component == null)
			{
				MLog.Warn("ModTexture.SetOnObject used with an object that has no Renderer!");
			}
			else
			{
				component.material.mainTexture = Texture;
			}
		}

		public static implicit operator Texture2D(ModTexture texture)
		{
			if (texture == null)
			{
				return null;
			}
			return texture.Texture;
		}

		public static explicit operator ModTexture(Texture2D texture)
		{
			if (texture == null)
			{
				return null;
			}
			ModTexture modTexture = new ModTexture();
			modTexture.Texture = texture;
			modTexture._loaded = true;
			modTexture._hasError = false;
			modTexture._error = string.Empty;
			return modTexture;
		}
	}
}
