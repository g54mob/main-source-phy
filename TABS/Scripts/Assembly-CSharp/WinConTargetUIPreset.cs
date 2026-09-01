using UnityEngine;

[CreateAssetMenu(fileName = "New TargetUIPreset", menuName = "Landfall/TargetUIPreset", order = 99999999)]
public class WinConTargetUIPreset : ScriptableObject
{
	[SerializeField]
	private Sprite m_icon;

	[SerializeField]
	private string m_title;

	[SerializeField]
	private AnimationCurve m_distanceScaleCurve;

	[SerializeField]
	private float m_yOffset;

	public Sprite Icon => m_icon;

	public string Title => m_title;

	public AnimationCurve DistanceScaleCurve => m_distanceScaleCurve;

	public float YOffset => m_yOffset;
}
