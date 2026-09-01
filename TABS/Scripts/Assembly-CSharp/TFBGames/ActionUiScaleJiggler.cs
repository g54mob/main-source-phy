using InControl;
using UnityEngine;

namespace TFBGames
{
	[RequireComponent(typeof(ActionGlyph))]
	public class ActionUiScaleJiggler : MonoBehaviour
	{
		[SerializeField]
		private UIScaleJiggle uiScaleJiggle;

		private ActionGlyph actionGlyph;

		private PlayerAction playerAction;

		private void Awake()
		{
			actionGlyph = GetComponent<ActionGlyph>();
			if (uiScaleJiggle == null)
			{
				uiScaleJiggle = GetComponent<UIScaleJiggle>();
			}
		}

		private void Start()
		{
			playerAction = actionGlyph.Action;
		}

		private void Update()
		{
			if (playerAction == null)
			{
				base.enabled = false;
			}
			else if (playerAction.WasPressed && uiScaleJiggle != null)
			{
				uiScaleJiggle.AddClickForce();
			}
		}
	}
}
