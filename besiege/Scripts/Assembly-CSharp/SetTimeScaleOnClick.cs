public class SetTimeScaleOnClick : ClickBehaviour
{
	public float myPercentage = 0.5f;

	public override void OnClicked()
	{
		if (!StatMaster.isClient || !StatMaster.Mode.LevelEditor.clientGlobalSim || StatMaster.InLocalPlayMode)
		{
			TimeSliderView.Instance.SetAuto(false);
			TimeSlider instance = TimeSlider.Instance;
			instance.SetPercentage(myPercentage);
			instance.SendTimeScale(false);
		}
	}
}
