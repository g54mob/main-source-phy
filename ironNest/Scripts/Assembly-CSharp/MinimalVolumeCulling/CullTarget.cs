using System.Collections.Generic;
using UnityEngine;

namespace MinimalVolumeCulling
{
	[DisallowMultipleComponent]
	public sealed class CullTarget : MonoBehaviour
	{
		public enum CullingAction
		{
			DisableGameObjects = 0
		}

		[Header("What gets toggled")]
		[SerializeField]
		[Tooltip("The GameObjects that will be enabled/disabled when this target is culled.\n\nDesigner workflow:\n- Drag the root visual GameObject(s) here (e.g., an LODGroup root, or a 'Visuals' child).\n- When culled, they will be SetActive(false).\n- When not culled, they will be SetActive(true).\n\nImportant safety notes:\n- Do NOT add THIS CullTarget GameObject to this list, otherwise it cannot un-cull itself.\n- Disabling a GameObject disables all components under it (Renderers, Colliders, Scripts, etc.).\n- If you only want to hide rendering, prefer renderer gating; but you explicitly requested GameObject toggling.\n\nSupported tokens/codes: none.\n\nSafe examples:\n- Drag 'Barbet_Details_Root' here.\n- Drag an LODGroup root GameObject here.")]
		private List<GameObject> toggleRoots;

		[SerializeField]
		[Tooltip("If enabled, the script will warn (in the Console) if you accidentally include this CullTarget's own GameObject in the Toggle Roots list.\n\nThis does not block play mode, it only warns.\n\nSafe default: enabled.\n\nSupported tokens/codes: none.")]
		private bool warnIfSelfIsInToggleRoots;

		[Header("Options")]
		[SerializeField]
		[Tooltip("If enabled, this target is never culled by the system.\n\nUse for critical objects you always want active.\n\nSafe default: disabled.\n\nSupported tokens/codes: none.")]
		private bool neverCull;

		[SerializeField]
		[Tooltip("If enabled, the script will attempt to restore the original active state of each Toggle Root when un-culled.\n\nBehavior:\n- On startup, the script records each root's initial activeSelf state.\n- When culled, it forces them inactive.\n- When un-culled:\n  - If this is enabled: sets each root back to its original activeSelf state.\n  - If this is disabled: forces them active (SetActive(true)).\n\nWhy:\n- This prevents the culling system from accidentally enabling something that was meant to stay disabled.\n\nSafe default: enabled.\n\nSupported tokens/codes: none.")]
		private bool restoreOriginalActiveStateOnUncull;

		[Header("Debug (read-only at runtime)")]
		[SerializeField]
		[Tooltip("DEBUG (read-only): True if this CullTarget is currently being culled by the system.\n\nMeaning:\n- True  => this component has applied the 'culled' state (typically SetActive(false) on Toggle Roots).\n- False => this component has applied the 'visible' state.\n\nDo not edit. This field is overwritten at runtime.\n\nSupported tokens/codes: none.")]
		private bool debugIsCulled;

		private bool _isCulled;

		private bool _capturedInitialStates;

		private readonly Dictionary<GameObject, bool> _initialActiveSelf;

		public bool IsCulled => false;

		private void Awake()
		{
		}

		private void OnValidate()
		{
		}

		private void CaptureInitialActiveStatesIfNeeded()
		{
		}

		public void ApplyCulled(bool culled)
		{
		}
	}
}
