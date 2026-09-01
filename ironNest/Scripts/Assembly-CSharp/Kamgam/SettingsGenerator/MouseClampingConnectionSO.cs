using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "MouseClampingConnection", menuName = "SettingsGenerator/Connection/Gameplay/MouseClamping", order = 51)]
	public class MouseClampingConnectionSO : BoolConnectionSO
	{
		[Tooltip("Target Tag\n- Unity Tag used to find the GameObject that has DynamicCursorManager.\nFormat rules:\n- Must be a tag defined in Unity's Tag Manager.\n- Must match exactly (case-sensitive).\nSafe examples:\n- \"Clipboard\"\n- \"PlayerClipboard\"\nNotes:\n- This connection expects EXACTLY ONE matching object in the scene.\n- If none or multiple exist, it will safely no-op (optional warnings).")]
		public string TargetTag;

		[Tooltip("Find Behavior\n- How aggressively to search for the fader.\nBehavior:\n- If TRUE: resolves the fader every time Set() is called (more robust, slightly more overhead).\n- If FALSE: resolves once and caches (faster, but if the clipboard is spawned later you must re-apply settings or reload).\nRecommended:\n- TRUE if the clipboard object can be spawned/despawned.\n- FALSE if the clipboard exists from scene start and never changes.")]
		public bool ResolveEverySet;

		[Tooltip("Diagnostics\n- If TRUE, logs warnings when:\n  - TargetTag is empty\n  - No object with the tag exists\n  - Object exists but has no DynamicCursorManager\n  - Multiple objects exist with the tag (Unity's FindGameObjectWithTag won't detect multiples, but misconfiguration is still possible)\nRecommended:\n- TRUE during integration, FALSE for production silence.")]
		public bool LogWarnings;

		private MouseClampingConnection _connection;

		public override IConnection<bool> GetConnection()
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
