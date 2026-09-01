public class ResetTimeScale : ClickBehaviour
{
	public override void OnClicked()
	{
		TimeSlider.Instance.SetPercentage(0.5f);
	}
}
