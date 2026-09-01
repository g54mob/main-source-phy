using I2.Loc;
using TMPro;
using UnityEngine;

public class Banner : MonoBehaviour
{
	public PanelResizeHorizontal m_BannerResizeHorizontal;

	public TextMeshProUGUI m_BannerTitle;

	public void Refresh()
	{
		I2.Loc.Localize component = m_BannerTitle.GetComponent<I2.Loc.Localize>();
		if (component != null)
		{
			component.OnLocalize();
		}
		m_BannerTitle.ForceMeshUpdate();
		m_BannerResizeHorizontal.ForceUpdate();
	}

	private void OnEnable()
	{
		Refresh();
	}
}
