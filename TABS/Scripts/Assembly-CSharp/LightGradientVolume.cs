using TFBGames;
using UnityEngine;

public class LightGradientVolume : MonoBehaviour
{
	[SerializeField]
	private Color m_colorSky;

	[SerializeField]
	private Color m_colorEquator;

	[SerializeField]
	private Color m_colorGround;

	private Color m_colorSkyDefault;

	private Color m_colorEquatorDefault;

	private Color m_colorGroundDefault;

	private Color m_colorSkyCurrent;

	private Color m_colorEquatorCurrent;

	private Color m_colorGroundCurrent;

	private Color m_colorSkyTarget;

	private Color m_colorEquatorTarget;

	private Color m_colorGroundTarget;

	private MainCam m_mainCam;

	private Collider m_colliderVolume;

	private void Start()
	{
		m_colorSkyDefault = RenderSettings.ambientSkyColor;
		m_colorEquatorDefault = RenderSettings.ambientEquatorColor;
		m_colorGroundDefault = RenderSettings.ambientGroundColor;
		m_colorSkyCurrent = m_colorSkyDefault;
		m_colorEquatorCurrent = m_colorEquatorDefault;
		m_colorGroundCurrent = m_colorGroundDefault;
		AssignMainCamera();
		m_colliderVolume = GetComponent<Collider>();
		if (m_mainCam == null || m_colliderVolume == null)
		{
			base.enabled = false;
		}
	}

	private void Update()
	{
		if (m_colliderVolume == null)
		{
			m_colliderVolume = GetComponent<Collider>();
		}
		if (!(m_mainCam == null) || AssignMainCamera())
		{
			if (m_colliderVolume.bounds.Contains(m_mainCam.transform.position))
			{
				m_colorSkyTarget = m_colorSky;
				m_colorEquatorTarget = m_colorEquator;
				m_colorGroundTarget = m_colorGround;
			}
			else
			{
				m_colorSkyTarget = m_colorSkyDefault;
				m_colorEquatorTarget = m_colorEquatorDefault;
				m_colorGroundTarget = m_colorGroundDefault;
			}
			m_colorSkyCurrent = Color.Lerp(m_colorSkyCurrent, m_colorSkyTarget, Time.deltaTime * 10f);
			m_colorEquatorCurrent = Color.Lerp(m_colorEquatorCurrent, m_colorEquatorTarget, Time.deltaTime * 10f);
			m_colorGroundCurrent = Color.Lerp(m_colorGroundCurrent, m_colorGroundTarget, Time.deltaTime * 10f);
			RenderSettings.ambientSkyColor = m_colorSkyCurrent;
			RenderSettings.ambientEquatorColor = m_colorEquatorCurrent;
			RenderSettings.ambientGroundColor = m_colorGroundCurrent;
		}
	}

	private bool AssignMainCamera()
	{
		m_mainCam = ServiceLocator.GetService<PlayerCamerasManager>()?.GetMainCam(TFBGames.Player.One);
		if (m_mainCam == null)
		{
			Debug.Log("MainCam is null");
			return false;
		}
		return true;
	}
}
