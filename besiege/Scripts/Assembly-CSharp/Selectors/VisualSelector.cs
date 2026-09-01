using System;
using System.Collections.Generic;
using UnityEngine;

namespace Selectors
{
	public class VisualSelector : Selector
	{
		[SerializeField]
		private Material activeMaterial;

		[SerializeField]
		private Transform content;

		[SerializeField]
		private DynamicText text;

		[SerializeField]
		private UIButton closeButton;

		[SerializeField]
		private UIButton previousButton;

		[SerializeField]
		private UIButton nextButton;

		[SerializeField]
		private FilterRendererPairT _icon;

		[SerializeField]
		private FilterRendererPairT _prevIcon;

		[SerializeField]
		private FilterRendererPairT _nextIcon;

		[SerializeField]
		private Renderer nameText;

		[SerializeField]
		private Renderer skinText;

		private bool updateCallback;

		private BlockSkinLoader.SkinPack.Skin[] lastSkin = new BlockSkinLoader.SkinPack.Skin[3];

		private List<BlockSkinLoader.SkinPack.Skin> register = new List<BlockSkinLoader.SkinPack.Skin>();

		private List<BlockSkinLoader.SkinPack.Skin> unregister = new List<BlockSkinLoader.SkinPack.Skin>();

		private List<BlockSkinLoader.SkinPack.Skin> registeredSkins = new List<BlockSkinLoader.SkinPack.Skin>();

		private UIButton uiButton;

		public override MapperType MapperType
		{
			get
			{
				return Visual;
			}
			set
			{
				MVisual visual = (MVisual)value;
				if (updateCallback)
				{
					BlockSkinLoader.SkinModified -= SkinModified;
					if (Visual != null)
					{
						Visual.ValueChanged -= OnSkinChanged;
					}
					updateCallback = false;
				}
				Visual = visual;
				BlockSkinLoader.SkinModified += SkinModified;
				Visual.ValueChanged += OnSkinChanged;
				updateCallback = true;
			}
		}

		public MVisual Visual { get; set; }

		protected void Awake()
		{
			BlockMapper.onMapperClose = (Action)Delegate.Combine(BlockMapper.onMapperClose, new Action(OnMapperClose));
			previousButton.Click += Previous;
			nextButton.Click += Next;
			closeButton.Click += Close;
		}

		private void ResetSkinChanged()
		{
			Visual.SetValue(Visual.Controller.Options.IndexOf(Visual.Controller.selectedSkin));
			UpdateBlock();
		}

		private void Close()
		{
			StatMaster.collapseSkinMapper = true;
			BlockMapper.CurrentInstance.IsDirty = true;
			UnregisterCurrentSkins();
		}

		private void OnMapperClose()
		{
			UnregisterCurrentSkins();
			Visual = null;
		}

		public void OnDestroy()
		{
			ResetToPool();
		}

		public override void ResetToPool()
		{
			if (updateCallback)
			{
				BlockSkinLoader.SkinModified -= SkinModified;
				if (Visual != null)
				{
					Visual.ValueChanged -= OnSkinChanged;
				}
				updateCallback = false;
			}
			UnregisterCurrentSkins();
			base.ResetToPool();
		}

		private void UnregisterCurrentSkins()
		{
			for (int i = 0; i < registeredSkins.Count; i++)
			{
				registeredSkins[i].Unregister(this);
			}
			ResetRegistration();
		}

		private void ResetRegistration()
		{
			lastSkin = new BlockSkinLoader.SkinPack.Skin[3];
			register.Clear();
			unregister.Clear();
			registeredSkins.Clear();
		}

		protected void SetIconsTo(int v, FauxTransform t)
		{
			SetIconsToMatch(t);
			SetIconsToVisual(v);
		}

		protected void SetIconsToMatch(FauxTransform trans)
		{
			SetIconToMatch(_icon.transform, trans);
			SetIconToMatch(_prevIcon.transform, trans);
			SetIconToMatch(_nextIcon.transform, trans);
		}

		protected void SetIconToMatch(Transform ico, FauxTransform trans)
		{
			Vector3 localPosition = trans.localPosition;
			localPosition.z = -1f;
			ico.localPosition = localPosition;
			ico.localRotation = trans.localRotation;
			ico.localScale = trans.localScale;
		}

		public override void Init()
		{
			ReferenceMaster.VisualSelectorMapper = this;
			base.Init();
			ResetRegistration();
			UpdateVisual();
		}

		public void Previous()
		{
			if (Visual != null)
			{
				int num = Visual.Value - 1;
				if (num < 0)
				{
					num = Visual.Items.Count - 1;
				}
				Visual.Value = num;
			}
		}

		public void Next()
		{
			if (Visual != null)
			{
				int num = Visual.Value + 1;
				if (num >= Visual.Items.Count)
				{
					num = 0;
				}
				Visual.Value = num;
			}
		}

		private void OnSkinChanged(int index)
		{
			if (!isEditing)
			{
				return;
			}
			BlockBehaviour block = Visual.Controller.Block;
			Machine parentMachine = block.ParentMachine;
			if (parentMachine.isLoadingDifference)
			{
				return;
			}
			List<BlockBehaviour> machineSelection = AdvancedBlockEditor.Instance.selectionController.MachineSelection;
			if (StatMaster.isMP && machineSelection.Count > 0)
			{
				StatMaster.cachingTransformActions = true;
			}
			BlockSkinLoader.SkinPack.Skin skin = Visual.Controller.Options[Visual.Value];
			List<UndoAction> list = new List<UndoAction>();
			if (block.VisualController.selectedSkin != skin)
			{
				list.Add(new UndoActionSkin(parentMachine, block.Guid, skin, block.VisualController.selectedSkin));
				if (StatMaster.isMP)
				{
					NetworkAuxAddPiece.Instance.ChangeBlockSkin(block, skin);
				}
				UpdateBlock();
			}
			else
			{
				UpdateVisual();
			}
			for (int i = 0; i < machineSelection.Count; i++)
			{
				BlockBehaviour blockBehaviour = machineSelection[i];
				if (!(blockBehaviour == block) && blockBehaviour.VisualController.selectedSkin != skin)
				{
					list.Add(new UndoActionSkin(parentMachine, blockBehaviour.Guid, skin, blockBehaviour.VisualController.selectedSkin));
					if (StatMaster.isMP)
					{
						NetworkAuxAddPiece.Instance.ChangeBlockSkin(blockBehaviour, skin);
					}
					BlockVisualController visualController = blockBehaviour.VisualController;
					if (visualController != null)
					{
						visualController.UpdateVis(skin);
					}
				}
			}
			parentMachine.UndoSystem.AddActions(list);
			if (StatMaster.cachingTransformActions)
			{
				(parentMachine as ServerMachine).FlushBlockTransformActions();
			}
		}

		protected virtual void UpdateBlock()
		{
			BlockVisualController controller = Visual.Controller;
			if (controller != null)
			{
				controller.UpdateVis(controller.Options[Visual.Value]);
			}
			UpdateVisual();
		}

		protected override void UpdateVisual()
		{
			SetIconsTo(Visual.Value, Visual.Controller.Prefab.GetButtonIcon().Alignment);
			text.SetText(CleanedSkinName(Visual.Selection.pack.name).ToUpper());
			CenterText();
		}

		public void SkinModified(BlockSkinLoader.SModifier m)
		{
			if (isEditing)
			{
				BlockSkinLoader.SkinPack.Skin skin = null;
				if (m != null)
				{
					skin = m as BlockSkinLoader.SkinPack.Skin;
				}
				if (Visual.Selection.pack.deleted)
				{
					BlockMapper.CurrentInstance.Refresh();
				}
				else if (skin != null && Visual.Controller != null && Visual.Controller.ID == skin.prefab.ID)
				{
					SetIconsTo(Visual.Value, Visual.Controller.Prefab.GetButtonIcon().Alignment);
				}
			}
		}

		public void SetIconsToVisual(int selection)
		{
			int num = selection - 1;
			if (num < 0)
			{
				num = Visual.Controller.Options.Count - 1;
			}
			int num2 = selection + 1;
			if (num2 >= Visual.Controller.Options.Count)
			{
				num2 = 0;
			}
			SetIconToVisual(_icon, selection, ref lastSkin[0]);
			SetIconToVisual(_prevIcon, num, ref lastSkin[1]);
			SetIconToVisual(_nextIcon, num2, ref lastSkin[2]);
			ResolveSkinRegistration();
		}

		protected void SetIconToVisual(FilterRendererPair ico, int selection, ref BlockSkinLoader.SkinPack.Skin skin)
		{
			if (skin != null)
			{
				Unregister(skin);
			}
			skin = Visual.Controller.Options[selection];
			Register(skin);
			Material material = ico.renderer.material;
			if (Visual.Controller.CanChangeTexture)
			{
				Material material2 = skin.material;
				if (material2 == null)
				{
					Debug.LogError("Missing material on skin " + skin.pack.name + "." + (BlockType)skin.ID);
					material2 = skin.prefab.DefaultSkin.material;
				}
				Color color = material2.color;
				material.color = new Color(color.r, color.g, color.b, material.color.a);
				material.mainTexture = skin.texture;
				if (material.HasProperty("_RimColor") && material2.HasProperty("_RimColor"))
				{
					material.SetColor("_RimColor", material2.GetColor("_RimColor"));
				}
				if (material.HasProperty("_RimPower") && material2.HasProperty("_RimPower"))
				{
					material.SetFloat("_RimPower", material2.GetFloat("_RimPower"));
				}
			}
			if (Visual.Controller.CanChangeMesh)
			{
				ico.filter.sharedMesh = skin.mesh;
				CorrectScaleForOutlierSkinSizes(ico.renderer);
			}
			else
			{
				ico.filter.sharedMesh = Visual.Controller.Prefab.GetButtonIcon().myMeshFilter.sharedMesh;
			}
			BlockType iD = (BlockType)skin.prefab.ID;
			if (iD == BlockType.SqrBalloon)
			{
				ico.renderer.materials = new Material[2] { material, material };
			}
			else
			{
				ico.renderer.materials = new Material[1] { material };
			}
		}

		protected void Register(BlockSkinLoader.SkinPack.Skin skin)
		{
			if (!register.Contains(skin))
			{
				if (unregister.Contains(skin))
				{
					unregister.Remove(skin);
				}
				else
				{
					register.Add(skin);
				}
			}
		}

		protected void Unregister(BlockSkinLoader.SkinPack.Skin skin)
		{
			if (!unregister.Contains(skin))
			{
				if (register.Contains(skin))
				{
					register.Remove(skin);
				}
				else
				{
					unregister.Add(skin);
				}
			}
		}

		protected void ResolveSkinRegistration()
		{
			for (int i = 0; i < unregister.Count; i++)
			{
				registeredSkins.Remove(unregister[i]);
				unregister[i].Unregister(this);
			}
			for (int j = 0; j < register.Count; j++)
			{
				registeredSkins.Add(register[j]);
				register[j].Register(this);
			}
			register.Clear();
			unregister.Clear();
		}

		protected void CorrectScaleForOutlierSkinSizes(Renderer target)
		{
			Vector3 size = target.bounds.size;
			float magnitude = new Vector3(size.x, size.y, 0f).magnitude;
			float targetMag = Visual.Controller.Prefab.GetButtonIcon().targetMag;
			if (magnitude != 0f && Mathf.Abs(targetMag - magnitude) > 0.6f * targetMag)
			{
				float num = targetMag / magnitude;
				target.transform.localScale *= num;
			}
		}

		public void CenterText()
		{
			float x = nameText.bounds.min.x;
			float x2 = skinText.bounds.max.x;
			float num = (x + x2) / 2f;
			float num2 = nameText.transform.parent.position.x - num;
			nameText.transform.position += Vector3.right * num2;
			skinText.transform.position += Vector3.right * num2;
		}

		private string CleanedSkinName(string objectName)
		{
			string text = objectName.ToUpper().TrimEnd();
			while (true)
			{
				if (text.EndsWith("SKIN"))
				{
					text = text.Replace("SKIN", string.Empty);
					text = text.TrimEnd();
					break;
				}
				if (text.EndsWith("PACK"))
				{
					text = text.Replace("PACK", string.Empty);
					text = text.TrimEnd();
					continue;
				}
				if (text.EndsWith("PACKAGE"))
				{
					text = text.Replace("PACKAGE", string.Empty);
					text = text.TrimEnd();
					continue;
				}
				if (text.EndsWith("包"))
				{
					text = ((!text.EndsWith("图像包")) ? text.Replace("包", string.Empty) : text.Replace("图像包", string.Empty));
				}
				break;
			}
			return text;
		}
	}
}
