using System.Collections;
using TMPro;
using UnityEngine;

namespace Landfall.TABC
{
	public class GoldManagerUI : MonoBehaviour
	{
		public static GoldManagerUI instance;

		public CodeAnimation bgAnim;

		public CodeAnimation rimAnim;

		public CodeAnimation baseAnim;

		public CodeAnimation interestAnim;

		public CodeAnimation streakAnim;

		public CodeAnimation winAnim;

		public CodeAnimation totalAnim;

		private TextMeshProUGUI baseText;

		private TextMeshProUGUI interestText;

		private TextMeshProUGUI streakText;

		private TextMeshProUGUI winText;

		private TextMeshProUGUI totalText;

		private void Awake()
		{
			instance = this;
			baseText = baseAnim.GetComponent<TextMeshProUGUI>();
			interestText = interestAnim.GetComponent<TextMeshProUGUI>();
			streakText = streakAnim.GetComponent<TextMeshProUGUI>();
			totalText = totalAnim.GetComponent<TextMeshProUGUI>();
			winText = winAnim.GetComponent<TextMeshProUGUI>();
		}

		public void ShowGold(int baseGold, int winGold, int interestGold, int streakGold)
		{
			StartCoroutine(DoShowGold(baseGold, winGold, interestGold, streakGold));
		}

		private IEnumerator DoShowGold(int baseGold, int winGold, int interestGold, int streakGold)
		{
			yield return new WaitForSeconds(1f);
			if (baseGold > 0)
			{
				baseText.transform.parent.gameObject.SetActive(value: true);
			}
			else
			{
				baseText.transform.parent.gameObject.SetActive(value: false);
			}
			if (winGold > 0)
			{
				winText.transform.parent.gameObject.SetActive(value: true);
			}
			else
			{
				winText.transform.parent.gameObject.SetActive(value: false);
			}
			if (interestGold > 0)
			{
				interestText.transform.parent.gameObject.SetActive(value: true);
			}
			else
			{
				interestText.transform.parent.gameObject.SetActive(value: false);
			}
			if (streakGold > 0)
			{
				streakText.transform.parent.gameObject.SetActive(value: true);
			}
			else
			{
				streakText.transform.parent.gameObject.SetActive(value: false);
			}
			bgAnim.PlayIn();
			rimAnim.PlayIn();
			yield return new WaitForSeconds(0.3f);
			if (baseGold > 0)
			{
				baseText.text = "+" + baseGold + " Base";
				baseAnim.PlayIn();
				yield return new WaitForSeconds(0.15f);
			}
			if (winGold > 0)
			{
				winText.text = "+" + winGold + " Victory";
				winAnim.PlayIn();
				yield return new WaitForSeconds(0.15f);
			}
			if (interestGold > 0)
			{
				interestText.text = "+" + interestGold + " Interest";
				interestAnim.PlayIn();
				yield return new WaitForSeconds(0.15f);
			}
			if (streakGold > 0)
			{
				streakText.text = "+" + streakGold + " Win streak";
				streakAnim.PlayIn();
				yield return new WaitForSeconds(0.15f);
			}
			yield return new WaitForSeconds(2f);
			baseAnim.PlayOut();
			streakAnim.PlayOut();
			yield return new WaitForSeconds(0.2f);
			interestAnim.PlayOut();
			winAnim.PlayOut();
			yield return new WaitForSeconds(0.3f);
			totalAnim.PlayIn();
			totalText.text = "Total " + (baseGold + winGold + interestGold + streakGold);
			yield return new WaitForSeconds(1f);
			totalAnim.PlayOut();
			yield return new WaitForSeconds(0.1f);
			bgAnim.PlayOut();
			rimAnim.PlayOut();
		}
	}
}
