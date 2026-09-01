using UnityEngine;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	public class SelectIfNull : MonoBehaviour
	{
		public enum Trigger
		{
			Awake = 0,
			OnEnable = 1,
			Start = 2,
			Update = 3,
			LateUpdate = 4,
			OnDirectionInput = 5
		}

		[Tooltip("Defines when the select if null logic should be executed. You can pick one or more times.")]
		public Trigger[] Triggers;

		[Tooltip("Optional: If candidates are set then these are the preferred target for the selection. A candidate will only be selected if it is enable, active and interactable.")]
		public Selectable[] Candidates;

		[Tooltip("Search for any interactable selectable if none of the Candidates is valid?")]
		public bool SearchForSelectables;

		[Tooltip("If currently selected object is not active it will be treated as null")]
		public bool TreatDisabledObjectsAsNull;

		public void Awake()
		{
		}

		public void Start()
		{
		}

		public void OnEnable()
		{
		}

		public void Update()
		{
		}

		public void LateUpdate()
		{
		}

		private bool containsTrigger(Trigger trigger)
		{
			return false;
		}

		private void selectIfNull()
		{
		}
	}
}
