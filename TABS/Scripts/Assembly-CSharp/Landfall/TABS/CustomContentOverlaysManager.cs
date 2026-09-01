using System.Collections;
using Landfall.TABS.Workshop;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class CustomContentOverlaysManager : MonoBehaviour
	{
		public enum Page
		{
			None = 0,
			Save = 1,
			Load = 2
		}

		private enum State
		{
			Closed = 0,
			Open = 1
		}

		public BlurColorChanger m_BlurColorChanger;

		public Image m_FadeImage;

		public GameObject m_SavePage;

		public GameObject m_LoadPage;

		private State m_state;

		private Page m_page;

		private float targetAlpha;

		private bool doneExpanding;

		public void ShowWindow(Page newPage)
		{
			StartCoroutine(Window(newPage));
		}

		private void Start()
		{
			IBattleCreatorMenu[] componentsInChildren = base.transform.GetComponentsInChildren<IBattleCreatorMenu>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Init(this);
			}
		}

		public IEnumerator Window(Page newPage)
		{
			Debug.Log(string.Concat("State: ", m_state, ", newPage: ", newPage));
			if (m_state == State.Closed && newPage != Page.None)
			{
				doneExpanding = false;
				while (!doneExpanding)
				{
					yield return null;
				}
				m_FadeImage.enabled = true;
				m_FadeImage.color = m_BlurColorChanger.m_NewColor;
				targetAlpha = 0f;
				GameObject page = GetPage(m_page);
				if ((bool)page)
				{
					page.SetActive(value: false);
				}
				GameObject page2 = GetPage(newPage);
				if ((bool)page2)
				{
					page2.SetActive(value: true);
				}
				m_state = State.Open;
				yield return new WaitForSeconds(0.15f);
				m_FadeImage.enabled = false;
			}
			else if (m_state != State.Closed && newPage == Page.None)
			{
				Debug.Log("Closing Bar!");
				m_FadeImage.enabled = true;
				targetAlpha = 1f;
				yield return new WaitForSeconds(0.15f);
				GameObject page3 = GetPage(m_page);
				if ((bool)page3)
				{
					page3.SetActive(value: false);
				}
				m_FadeImage.enabled = false;
				m_state = State.Closed;
			}
			else if (m_state != State.Closed && newPage != Page.None && newPage != m_page)
			{
				m_FadeImage.enabled = true;
				targetAlpha = 1f;
				yield return new WaitForSeconds(0.15f);
				GameObject page4 = GetPage(m_page);
				if ((bool)page4)
				{
					page4.SetActive(value: false);
				}
				GameObject page5 = GetPage(newPage);
				if ((bool)page5)
				{
					page5.SetActive(value: true);
				}
				targetAlpha = 0f;
				yield return new WaitForSeconds(0.15f);
				m_FadeImage.enabled = false;
			}
			m_page = newPage;
		}

		public void DoneExpanding()
		{
			doneExpanding = true;
		}

		private GameObject GetPage(Page page)
		{
			switch (page)
			{
			case Page.Save:
				return m_SavePage;
			case Page.Load:
				return m_LoadPage;
			default:
				return null;
			}
		}

		private void Update()
		{
			Color color = m_FadeImage.color;
			color.a = targetAlpha;
			m_FadeImage.color = Color.Lerp(m_FadeImage.color, color, Time.deltaTime * 40f);
		}
	}
}
