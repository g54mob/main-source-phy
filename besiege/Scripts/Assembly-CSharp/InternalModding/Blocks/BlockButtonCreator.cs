using System.Collections.Generic;
using System.Linq;
using Modding;
using Modding.Serialization;
using UnityEngine;

namespace InternalModding.Blocks
{
	public static class BlockButtonCreator
	{
		public static void CreateBlockButton(ModdedBlock block, GameObject tab)
		{
			GameObject gameObject = Object.Instantiate(SingleInstanceFindOnly<BlockLoader>.Instance.BlockButtonTemplate);
			gameObject.name = "Modded: " + block.Name;
			gameObject.transform.parent = tab.transform;
			BlockButtonControl bbc = gameObject.GetComponent<BlockButtonControl>();
			bbc.myIndex = block.Id;
			bbc.blockMenuControllerCode = tab.GetComponent<BlockMenuControl>();
			bbc.visBoxCode = tab.transform.parent.FindChild("BlockViewer/BLOCK Viewer").GetComponent<BlockViewerController>();
			Transform icon = gameObject.transform.FindChild("IconPivot/Icon");
			MeshRenderer mr = icon.GetComponent<MeshRenderer>();
			MeshFilter mf = icon.GetComponent<MeshFilter>();
			Material origMaterial = mr.material;
			mr.material = SingleInstanceFindOnly<BlockLoader>.Instance.LoadingMaterial;
			mf.mesh = null;
			GameObject parent = null;
			if (!block.Mesh.Loaded)
			{
				parent = new GameObject("Placeholder Vis");
				parent.transform.SetParent(icon, false);
				foreach (ModCollider collider in block.Colliders)
				{
					collider.CreateVisual(parent.transform).gameObject.layer = icon.gameObject.layer;
				}
				parent.transform.localScale = new UnityEngine.Vector3(0.1f, 0.1f, 0.1f);
			}
			icon.localScale = new UnityEngine.Vector3(3f, 3f, 3f);
			block.BlockButton = gameObject;
			block.Mesh.OnLoad += delegate
			{
				mf.sharedMesh = block.Mesh;
				if (parent != null)
				{
					Object.DestroyImmediate(parent);
				}
				block.Icon.SetOnTransform(icon);
				bbc.InvalidateAlignment();
				bbc.SetMesh(block.Mesh);
				foreach (SelectSkinButton item in SingleInstanceFindOnly<PrefabVisualUI>.Instance.officialIcons.Concat(SingleInstanceFindOnly<PrefabVisualUI>.Instance.downloadedIcons))
				{
					if (item.ID == block.Id)
					{
						item.Setup(block.Id, item.mySkin);
					}
				}
			};
			block.Texture.OnLoad += delegate
			{
				mr.material = origMaterial;
				mr.material.mainTexture = (Texture2D)block.Texture;
				foreach (SelectSkinButton item2 in SingleInstanceFindOnly<PrefabVisualUI>.Instance.officialIcons.Concat(SingleInstanceFindOnly<PrefabVisualUI>.Instance.downloadedIcons))
				{
					if (item2.ID == block.Id)
					{
						item2.Setup(block.Id, item2.mySkin);
					}
				}
			};
			CreateExtraObjects(block, icon, origMaterial);
			TextMesh component = gameObject.transform.FindChild("Tooltip/TooltipText").GetComponent<TextMesh>();
			component.text = block.Name.ToUpper();
			gameObject.transform.FindChild("BG").GetChild(0).GetComponent<DynamicText>()
				.cam = SingleInstanceFindOnly<AddPiece>.Instance.hudCam;
			Transform transform = tab.transform.parent.FindChild("StartPosition");
			gameObject.transform.position = transform.position;
			gameObject.SetActive(true);
		}

		private static void CreateExtraObjects(ModdedBlock block, Transform icon, Material origMaterial)
		{
			foreach (Transform item in icon)
			{
				if (item.name == "ExtraObject")
				{
					Object.Destroy(item.gameObject);
				}
			}
			if (block.ExtraIconObjects != null)
			{
				MeshTexturePair[] extraIconObjects = block.ExtraIconObjects;
				foreach (MeshTexturePair meshTexturePair in extraIconObjects)
				{
					ModTexture texture = meshTexturePair.Texture;
					ModMesh mesh = meshTexturePair.Mesh;
					MeshReference meshReference = meshTexturePair.MeshReference;
					GameObject gameObject = new GameObject("ExtraObject");
					gameObject.transform.parent = icon;
					gameObject.layer = icon.gameObject.layer;
					MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
					MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
					meshRenderer.material = SingleInstanceFindOnly<BlockLoader>.Instance.LoadingMaterial;
					meshFilter.mesh = null;
					texture.SetOnObject(gameObject, origMaterial);
					mesh.SetOnObject(gameObject, meshReference);
				}
			}
		}

		public static void DestroyBlockButtons()
		{
			foreach (ModdedBlock loadedBlock in SingleInstanceFindOnly<BlockLoader>.Instance.LoadedBlocks)
			{
				if (loadedBlock.BlockButton != null)
				{
					loadedBlock.BlockButton.GetComponent<BlockButtonControl>().OnDestroyDisregardInactive();
					Object.DestroyImmediate(loadedBlock.BlockButton);
				}
			}
		}

		public static void UpdateIcon(ModdedBlock block)
		{
			if (!(block.BlockButton == null))
			{
				Transform transform = block.BlockButton.transform.FindChild("IconPivot/Icon");
				block.Icon.SetOnTransform(transform);
				CreateExtraObjects(block, transform, transform.GetComponentInChildren<MeshRenderer>().material);
			}
		}

		public static void PositionButtons()
		{
			GameObject blockButtonTemplate = SingleInstanceFindOnly<BlockLoader>.Instance.BlockButtonTemplate;
			float num = blockButtonTemplate.GetComponent<BoxCollider>().size.x * blockButtonTemplate.transform.localScale.x;
			List<ModdedBlock> visibleBlocks = SingleInstanceFindOnly<BlockLoader>.Instance.VisibleBlocks;
			for (int i = 0; i < visibleBlocks.Count; i++)
			{
				int num2 = i % TabCreator.MaxBlocksPerTab;
				UnityEngine.Vector3 localPosition = visibleBlocks[i].BlockButton.transform.localPosition;
				localPosition.x += (float)num2 * num;
				visibleBlocks[i].BlockButton.transform.localPosition = localPosition;
			}
		}
	}
}
