using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "ClipboardOffsetConnection", menuName = "SettingsGenerator/Connection/Clipboard/Offset (Percent -> Units)", order = 50)]
	public class ClipboardOffsetConnectionSO : FloatConnectionSO
	{
		[Tooltip("Input Range (Setting/UI)\n- The range of the slider in UI space.\n- Most percent sliders use 0..100.\nBehavior:\n- Incoming values are normalized within this range and remapped into OutputOffsetUnitsRange.\nFormat rules:\n- X must be <= Y (if not, values will be swapped at runtime).\nSafe examples:\n- (0, 100) for a percent slider.")]
		public Vector2 InputRange;

		[Tooltip("Output Offset Range (Local Units)\n- The value written to ClipboardAspectRatioOffsetFader.SetAspectRatioOffsetAmount().\nUser experience mapping:\n- InputRange minimum maps to OutputOffsetUnitsRange.x\n- InputRange maximum maps to OutputOffsetUnitsRange.y\nFormat rules:\n- X must be <= Y (if not, values will be swapped at runtime).\nSafe examples (your desired behavior):\n- (-0.2, 0.2) so 0% => -0.2 and 100% => +0.2\nNotes:\n- The fader will still fade the applied offset to 0 while IsFocused is true.\n- This range is purely the configured offset when not focused.")]
		public Vector2 OutputOffsetUnitsRange;

		[Tooltip("Target Tag\n- Unity Tag used to find the GameObject that has ClipboardAspectRatioOffsetFader.\nFormat rules:\n- Must be a tag defined in Unity's Tag Manager.\n- Must match exactly (case-sensitive).\nSafe examples:\n- \"Clipboard\"\n- \"PlayerClipboard\"\nNotes:\n- This connection expects EXACTLY ONE matching object in the scene.\n- If none or multiple exist, it will safely no-op (optional warnings).")]
		public string TargetTag;

		[Tooltip("Find Behavior\n- How aggressively to search for the fader.\nBehavior:\n- If TRUE: resolves the fader every time Set() is called (more robust, slightly more overhead).\n- If FALSE: resolves once and caches (faster, but if the clipboard is spawned later you must re-apply settings or reload).\nRecommended:\n- TRUE if the clipboard object can be spawned/despawned.\n- FALSE if the clipboard exists from scene start and never changes.")]
		public bool ResolveEverySet;

		[Tooltip("Diagnostics\n- If TRUE, logs warnings when:\n  - TargetTag is empty\n  - No object with the tag exists\n  - Object exists but has no ClipboardAspectRatioOffsetFader\n  - Multiple objects exist with the tag (Unity's FindGameObjectWithTag won't detect multiples, but misconfiguration is still possible)\nRecommended:\n- TRUE during integration, FALSE for production silence.")]
		public bool LogWarnings;

		private ClipboardOffsetConnection _connection;

		public override IConnection<float> GetConnection()
		{
			return null;
		}

		public void Create()
		{
		}

		public override void DestroyConnection()
		{
		}
	}
}
