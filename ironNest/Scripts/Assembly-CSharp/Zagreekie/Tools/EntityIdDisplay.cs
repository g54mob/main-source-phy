using TMPro;
using UnityEngine;

namespace Zagreekie.Tools
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(EntityLocation))]
	public class EntityIdDisplay : MonoBehaviour
	{
		[Header("References")]
		[SerializeField]
		private EntityLocation _entityLocation;

		[SerializeField]
		private TMP_Text _targetText;

		[Header("Formatting")]
		[Tooltip("Controls how the Entity ID is displayed in the text field. Supported tokens:\n\n{ID} — replaced with the assigned Entity's full ID string (e.g. \"Spotter_3\").\n\n{NUM} — replaced with just the trailing number at the end of the Entity's ID, if one exists (e.g. \"3\" for ID \"Spotter_3\", \"13\" for ID \"Artillery_13\"). The match looks only at the very end of the ID string — any run of digit characters immediately before the end, regardless of what (if anything) precedes them (an underscore, a '#', a space, or nothing at all).\n\nIMPORTANT — missing-number rule: if the template contains {NUM} and the current Entity's ID does NOT end in a digit (e.g. \"King\"), the ENTIRE text field is cleared to an empty string for that entity — not just the {NUM} portion. Any other literal text or the {ID} token elsewhere in the same template will NOT be shown either in that case. This only applies while {NUM} is present in the template; a template using only {ID} is unaffected and always shows the full ID.\n\nThese are the only two supported tokens. Any other characters in the template are shown as-is (plain text/formatting) when a number IS found. Token matching is case-sensitive and each token must be written exactly as shown, including the braces. Either token may be used zero, one, or multiple times. The format string is re-applied every time a new (or changed) Entity ID is detected.\n\nExamples:\n  \"{ID}\" -> \"Spotter_3\" (always shows the ID, regardless of trailing digits)\n  \"{NUM}\" -> \"3\" for ID \"Spotter_3\"; entire field blank for ID \"King\"\n  \"Kills: {NUM}\" -> \"Kills: 3\" for ID \"Spotter_3\"; entire field blank (not \"Kills: \") for ID \"King\"")]
		[SerializeField]
		private string _format;

		[Header("Behaviour")]
		[Tooltip("If enabled, the displayed text is cleared (set to an empty string) whenever no Entity is currently assigned to the referenced EntityLocation (e.g. before Init() has run, or after this object has been despawned/pooled and its Entity reference cleared elsewhere). If disabled, the last known text is left on screen until a new Entity is assigned. This is separate from, and does not affect, the {NUM} missing-number rule described above (which always clears the text regardless of this setting, once an Entity IS assigned but its ID has no trailing digits).")]
		[SerializeField]
		private bool _clearTextWhenNoEntity;

		[Tooltip("If enabled, this component keeps checking every frame (in Update) for a change in the assigned Entity or its ID, and refreshes the text automatically. This safely supports prefab pooling/reuse where the same EntityLocation may be re-initialised with a different MapEntity later. If disabled, the ID is only captured once a real (non-empty) ID is first detected, after which this component stops ticking — cheaper, but will not reflect any later re-initialisation.")]
		[SerializeField]
		private bool _watchForChanges;

		private string _lastAppliedId;

		private void Awake()
		{
		}

		private void OnEnable()
		{
		}

		private void Update()
		{
		}

		private void RefreshText()
		{
		}

		private void ApplyToText()
		{
		}
	}
}
