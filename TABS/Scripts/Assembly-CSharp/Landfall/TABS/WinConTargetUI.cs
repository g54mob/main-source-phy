using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class WinConTargetUI : MonoBehaviour
	{
		[SerializeField]
		private WinConTargetUIPreset m_preset;

		[SerializeField]
		private TextMeshProUGUI m_titleText;

		[SerializeField]
		private Image m_icon;

		[SerializeField]
		private Unit m_unit;

		[SerializeField]
		private Transform m_canvas;

		private bool m_isSetup;

		private Camera m_mainCamera;

		private Color m_highlightColor;

		public void Setup(WinConTargetUIPreset preset, Unit unit, Color color, Color UIcolor)
		{
			m_isSetup = true;
			m_mainCamera = Object.FindObjectOfType<MainCam>().m_camera;
			m_highlightColor = color;
			m_unit = unit;
			m_icon.sprite = preset.Icon;
			m_icon.color = UIcolor;
			m_titleText.text = preset.Title;
			m_titleText.color = UIcolor;
		}

		private void Update()
		{
			if (m_isSetup)
			{
				if (m_mainCamera == null)
				{
					m_mainCamera = Object.FindObjectOfType<MainCam>()?.m_camera;
				}
				if (!(m_mainCamera == null))
				{
					m_unit.SetHighlight(m_highlightColor);
					float time = Vector3.Distance(m_unit.Head.position, m_mainCamera.transform.position);
					float num = m_preset.DistanceScaleCurve.Evaluate(time);
					Vector3 position = m_unit.Head.position;
					position.y += m_preset.YOffset + num * 0.8f;
					base.transform.position = position;
					base.transform.localScale = Vector3.one * num;
					m_canvas.LookAt(m_mainCamera.transform.position);
				}
			}
		}
	}
}
