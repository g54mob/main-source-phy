using System.IO;
using System.Linq;
using InternalModding.Misc;
using InternalModding.Mods;
using Modding;
using UnityEngine;

namespace InternalModding.Loading
{
	public class ModLoader : SingleInstanceFindOnly<ModLoader>
	{
		private bool initialModsLoaded;

		public override string Name
		{
			get
			{
				return "ModMaster";
			}
		}

		protected override void Awake()
		{
			base.Awake();
			ModManager.OnInitialModsLoaded += delegate
			{
				initialModsLoaded = true;
			};
		}

		public ModContainer LoadModInfoFrom(DirectoryInfo dir)
		{
			FileInfo fileInfo = new FileInfo(Path.Combine(dir.FullName, "Mod.xml"));
			if (!fileInfo.Exists)
			{
				return null;
			}
			ModInfo info = ModInfo.LoadFromFile(fileInfo.FullName, false);
			if (info == null)
			{
				MLog.Warn("Not loading " + dir.Name);
				return null;
			}
			ModContainer modContainer;
			if ((modContainer = ModManager.Mods.FirstOrDefault((ModContainer m) => m.Info.Id == info.Id)) != null)
			{
				if (modContainer.Info.Directory != info.Directory)
				{
					MLog.Warn("Tried to load mod with same ID as already loaded mod: " + info.Name + ", ID used by " + modContainer.Info.Name);
				}
				return null;
			}
			ModContainer modContainer2 = new ModContainer(info);
			if (OptionsMaster.firstRunAfterUpdate && !OptionsMaster.BesiegeConfig.FirstTimePlaying && !initialModsLoaded)
			{
				Debug.Log("[ModLoader] Game version changed detected, turning off " + modContainer2.Info.Name);
				ModStatus.DisableMod(modContainer2);
				modContainer2.IsEnabled = false;
			}
			else
			{
				modContainer2.IsEnabled = !ModStatus.IsModDisabled(info);
			}
			LoadResourcesAsync(modContainer2);
			return modContainer2;
		}

		public void PostLoadMod(ModContainer mod)
		{
			Modding.Mods.InvokeModLoaded(mod);
		}

		private void LoadResourcesAsync(ModContainer mod)
		{
			string empty = string.Empty;
			if (mod.Info.IconReference != null)
			{
				empty = mod.Info.IconReference.Name;
			}
			ModInfo.ResourceInfo resourceInfo;
			foreach (ModInfo.ResourceInfo resource in mod.Info.Resources)
			{
				resourceInfo = resource;
				resourceInfo.Mod = mod;
				if (mod.Resources.Any((ModResource r) => r.Name == resourceInfo.Name))
				{
					MLog.Error("Multiple resources with the same name: " + resourceInfo.Name);
					continue;
				}
				if (resourceInfo.Name == empty)
				{
					resourceInfo.Readable = true;
				}
				ModResource item = ModResource.Load(resourceInfo);
				mod.Resources.Add(item);
			}
			if (mod.Info.IconReference != null)
			{
				mod.Info.Icon = ModResource.Get(mod.Info.IconReference, mod) as ModTexture;
				if (mod.Info.Icon == null)
				{
					MLog.Error("Could not find mod icon resource: " + mod.Info.IconReference.Name);
					mod.Info.Icon = (ModTexture)SingleInstanceFindOnly<ModManager>.Instance.NoThumbnailTexture;
				}
				else
				{
					mod.Info.Icon.OnLoad += delegate
					{
						Texture2D texture = mod.Info.Icon.Texture;
						if (texture != null && texture.width != texture.height)
						{
							MLog.Error("Mod Icon must be a square image!");
							mod.Info.Icon = (ModTexture)SingleInstanceFindOnly<ModManager>.Instance.NoThumbnailTexture;
						}
						else if (texture == null)
						{
							mod.Info.Icon = (ModTexture)SingleInstanceFindOnly<ModManager>.Instance.NoThumbnailTexture;
						}
					};
				}
			}
			else
			{
				mod.Info.Icon = (ModTexture)SingleInstanceFindOnly<ModManager>.Instance.NoThumbnailTexture;
			}
			if (mod.Info.WorkshopThumbnailReference != null)
			{
				mod.Info.WorkshopThumbnail = ModResource.Get(mod.Info.WorkshopThumbnailReference, mod) as ModTexture;
				if (mod.Info.WorkshopThumbnail == null)
				{
					MLog.Error("Could not find workshop thumbnail resource: " + mod.Info.WorkshopThumbnailReference.Name);
				}
			}
		}
	}
}
