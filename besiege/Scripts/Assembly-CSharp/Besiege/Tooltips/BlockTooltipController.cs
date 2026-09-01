using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Besiege.Tooltips
{
	public class BlockTooltipController : SingleInstance<BlockTooltipController>
	{
		public const int HIGH_FRICTION_LOC = 583;

		public const int LOW_FRICTION_LOC = 777;

		private static readonly Vector3 TOOLTIP_OFFSET = new Vector3(0.57f, 0.6f);

		public Transform tooltipRoot;

		public GameObject tooltipPrefab;

		public GameObject buildSurfacePrefab;

		public GameObject emulatePrefab;

		public GameObject draggedBlockPrefab;

		public Sprite floatSprite;

		public Sprite sinkSprite;

		public Sprite highFrictionSprite;

		public Sprite lowFrictionSprite;

		private Dictionary<BlockType, BlockTooltipHolder> tooltips = new Dictionary<BlockType, BlockTooltipHolder>();

		public override string Name
		{
			get
			{
				return "BlockTooltipController";
			}
		}

		public static event Action<BlockTooltipHolder> TooltipCreated;

		private void Awake()
		{
			BlockButtonControl.CreatedButton += SetupTooltip;
			foreach (int value in Enum.GetValues(typeof(BlockType)))
			{
				BlockPrefab prefab;
				if (!PrefabMaster.GetPrefab((BlockType)value, out prefab))
				{
					Debug.LogError("Tried to get nonexistent prefab " + (BlockType)value);
					break;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate(tooltipPrefab, tooltipRoot, false) as GameObject;
				gameObject.name = "BlockTooltip - " + (BlockType)value;
				BlockTooltipHolder component = gameObject.GetComponent<BlockTooltipHolder>();
				tooltips.AddOrReplace(prefab.Type, component);
				component.Setup(prefab);
			}
		}

		public BlockTooltipHolder GetTooltip(BlockType blockType)
		{
			return tooltips.GetValueOrDefault(blockType);
		}

		public void UpdatePosition(BlockTooltipHolder tooltipHolder, Transform button, [Optional] Vector3 extraOffset)
		{
			StartCoroutine(IEUpdatePosition(tooltipHolder, button.transform, extraOffset));
		}

		private IEnumerator IEUpdatePosition(BlockTooltipHolder tooltipHolder, Transform button, [Optional] Vector3 extraOffset)
		{
			if (!tooltipHolder.gameObject.activeSelf)
			{
				if (BlockTooltipController.TooltipCreated != null)
				{
					BlockTooltipController.TooltipCreated(tooltipHolder);
				}
				tooltipHolder.ConstructLayout();
				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();
			}
			tooltipHolder.transform.position = button.parent.TransformPoint(button.localPosition + TOOLTIP_OFFSET + extraOffset);
			tooltipHolder.tooltipCode.tooltipParentStartPos = tooltipHolder.transform.localPosition;
		}

		private void SetupTooltip(BlockButtonControl button)
		{
			BlockType myIndex = (BlockType)button.myIndex;
			BlockTooltipHolder tooltipHolder;
			if (!tooltips.TryGetValue(myIndex, out tooltipHolder))
			{
				Debug.LogError("Tried to get nonexistent tooltip " + myIndex);
				return;
			}
			DynamicText[] componentsInChildren = tooltipHolder.GetComponentsInChildren<DynamicText>(true);
			foreach (DynamicText dynamicText in componentsInChildren)
			{
				dynamicText.cam = SingleInstanceFindOnly<AddPiece>.Instance.hudCam;
			}
			Tooltip tooltipCode = tooltipHolder.tooltipCode;
			CursorHoverHook cursorHoverHook = button.gameObject.AddComponent<CursorHoverHook>();
			cursorHoverHook.onCursorEnter = (Action)Delegate.Combine(cursorHoverHook.onCursorEnter, (Action)delegate
			{
				UpdatePosition(tooltipHolder, button.transform);
			});
			cursorHoverHook.onCursorOver = (Action)Delegate.Combine(cursorHoverHook.onCursorOver, new Action(tooltipCode.OnCursorOver));
			cursorHoverHook.onCursorExit = (Action)Delegate.Combine(cursorHoverHook.onCursorExit, new Action(tooltipCode.OnMouseExit));
			MouseEventHook mouseEventHook = button.gameObject.AddComponent<MouseEventHook>();
			mouseEventHook.onMouseDown = (Action)Delegate.Combine(mouseEventHook.onMouseDown, new Action(tooltipCode.OnClicked));
			Tooltip component = button.GetComponent<Tooltip>();
			component.enabled = false;
			component.tooltipParent.gameObject.SetActive(false);
		}
	}
}
