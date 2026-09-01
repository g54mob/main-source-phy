using Selectors;

public class LogicKeyChangeWidget : LogicWidget
{
	public KeyChangeSelector keySelector;

	protected override void Init()
	{
		keySelector.Key = logic.mKey;
		keySelector.isLogic = true;
		keySelector.Init();
		keySelector.KeysChanged += OnKeyChanged;
	}

	private void OnKeyChanged()
	{
		logic.keyPressCode = logic.mKey.GetKey(0);
		if (hasHandler)
		{
			editLogicHandler.OnEditLogic(logic);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (keySelector != null)
		{
			keySelector.KeysChanged -= OnKeyChanged;
		}
	}
}
