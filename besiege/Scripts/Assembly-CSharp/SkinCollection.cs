using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Besiege;
using Localisation;
using MultithreadCoroutines;

public class SkinCollection : AbstractObjectCollection
{
	public override string FilterExtension
	{
		get
		{
			return string.Empty;
		}
	}

	public override bool HideFileField
	{
		get
		{
			return true;
		}
	}

	private string FolderName
	{
		get
		{
			return "/Skins";
		}
	}

	public SkinCollection()
	{
		ObjectName = LocalisationManager.GetTranslation(955);
	}

	public override VirtualFolder GetRoot()
	{
		SingleInstance<AssetImporter>.Instance.StartCoroutineAsync(LoadSkins());
		string path = RootPath + FolderName + FileSystemPath.DirectorySeparator;
		DirectoryInfo directoryInfo = new DirectoryInfo(path);
		if (!directoryInfo.Exists)
		{
			return VirtualFolder.Empty;
		}
		base.CurrentFolder = new LocalFolder(directoryInfo);
		base.CurrentFolder.FolderChanged = base.OnFolderChanged;
		Generate(base.CurrentFolder);
		return base.CurrentFolder;
	}

	private IEnumerator LoadSkins()
	{
		yield return Ninja.JumpToUnity;
		BlockSkinLoader.LoadNewSkins();
	}

	private void Generate(VirtualFolder folder)
	{
		for (int i = 0; i < BlockSkinLoader.SkinPacks.Count; i++)
		{
			BlockSkinLoader.SkinPack skinPack = BlockSkinLoader.SkinPacks[i];
			if (skinPack.path == null)
			{
				LocalSkinFile localSkinFile = new LocalSkinFile(FileSystemPath.Root.AppendFile(skinPack.name), FileSystemPath.Root);
				localSkinFile.SkinPack = skinPack;
				folder.AddObject(localSkinFile);
				continue;
			}
			FileInfo fileInfo = new FileInfo(skinPack.path);
			ulong result;
			LocalSkinFile localSkinFile2;
			if (skinPack.type == PackType.Workshop && ulong.TryParse(skinPack.id, out result))
			{
				bool isOwner = false;
				WorkshopManager.WorkshopItem workshopItem = skinPack.workshopItem;
				uint dlcDependencyMask;
				bool areDlcRequirementsMet;
				if (workshopItem != null)
				{
					dlcDependencyMask = workshopItem.DlcDependencyMask;
					areDlcRequirementsMet = workshopItem.AreDlcRequirementsMet;
					isOwner = workshopItem.IsOwner;
				}
				else
				{
					dlcDependencyMask = 0u;
					areDlcRequirementsMet = true;
				}
				WorkshopSkinFile workshopSkinFile = LocalFile.FromFileInfo<WorkshopSkinFile>(fileInfo);
				workshopSkinFile.WorkshopItemId = result;
				workshopSkinFile.IsOwner = isOwner;
				workshopSkinFile.DlcDependencyMask = dlcDependencyMask;
				workshopSkinFile.AreDlcRequirementsMet = areDlcRequirementsMet;
				localSkinFile2 = workshopSkinFile;
			}
			else
			{
				localSkinFile2 = LocalFile.FromFileInfo<LocalSkinFile>(fileInfo);
			}
			localSkinFile2.SkinPack = skinPack;
			folder.AddObject(localSkinFile2);
		}
		InvokeCollectionChanged();
	}

	public override IEnumerable<IVirtualObject> OrderObjects(IEnumerable<IVirtualObject> objects)
	{
		return from x in base.OrderObjects(objects)
			orderby ((LocalSkinFile)x).SkinPack.type
			select x;
	}

	public override void ChangeFolder(VirtualFolder folder)
	{
	}

	public override void OpenParentFolder()
	{
	}

	public override void Dispose()
	{
	}
}
