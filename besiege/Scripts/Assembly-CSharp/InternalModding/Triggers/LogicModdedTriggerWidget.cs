using InternalModding.Loading;

namespace InternalModding.Triggers
{
	public class LogicModdedTriggerWidget : LogicWidget
	{
		public DynamicText NameText;

		public UIButton NextButton;

		public UIButton PrevButton;

		protected void Awake()
		{
			NextButton.Click += OnNext;
			PrevButton.Click += OnPrev;
		}

		protected override void Init()
		{
			bool active = entityBehaviour.entity.behaviour.prefab.moddedEvents.Length > 1;
			NextButton.gameObject.SetActive(active);
			PrevButton.gameObject.SetActive(active);
			if (logic.moddedTriggerType == null)
			{
				logicSelector.OnRemove();
			}
		}

		private void OnNext()
		{
			int effectiveId = entityBehaviour.NextModdedTriggerType(logic.moddedTriggerType.Id);
			logic.moddedTriggerType = ModIds.GetTriggerByEffectiveId(effectiveId);
			UpdateVisual();
			if (hasHandler)
			{
				editLogicHandler.OnEditLogic(logic);
			}
		}

		private void OnPrev()
		{
			int effectiveId = entityBehaviour.PreviousModdedTriggerType(logic.moddedTriggerType.Id);
			logic.moddedTriggerType = ModIds.GetTriggerByEffectiveId(effectiveId);
			UpdateVisual();
			if (hasHandler)
			{
				editLogicHandler.OnEditLogic(logic);
			}
		}

		protected override void UpdateVisual()
		{
			if (logic.moddedTriggerType != null)
			{
				NameText.SetText(logic.moddedTriggerType.Name);
			}
		}
	}
}
