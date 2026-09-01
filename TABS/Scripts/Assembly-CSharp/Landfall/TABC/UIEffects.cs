using UnityEngine;

namespace Landfall.TABC
{
	public class UIEffects : MonoBehaviour
	{
		public static UIEffects instance;

		public CodeAnimation cantAffordAnim;

		public CodeAnimation tooSlowAnim;

		public CodeAnimation notDuringBattleAnim;

		public CodeAnimation notThereAnim;

		public Camera uiCam;

		private float canvasScale;

		private void Awake()
		{
			instance = this;
			canvasScale = base.transform.root.GetComponentInChildren<Canvas>().transform.localScale.x;
		}

		private void Start()
		{
		}

		public void TooSlow()
		{
			tooSlowAnim.transform.parent.localEulerAngles = new Vector3(0f, 0f, Random.Range(-35f, 35f));
			tooSlowAnim.transform.parent.transform.position = mouseToCanvasPos();
			tooSlowAnim.PlayIn();
		}

		public void CantAfford()
		{
			cantAffordAnim.transform.parent.localEulerAngles = new Vector3(0f, 0f, Random.Range(-35f, 35f));
			cantAffordAnim.transform.parent.transform.position = mouseToCanvasPos();
			cantAffordAnim.PlayIn();
		}

		public void NotDuringBattle()
		{
			notDuringBattleAnim.transform.parent.localEulerAngles = new Vector3(0f, 0f, Random.Range(-35f, 35f));
			notDuringBattleAnim.transform.parent.transform.position = mouseToCanvasPos();
			notDuringBattleAnim.PlayIn();
		}

		public void NotThere()
		{
			notThereAnim.transform.parent.localEulerAngles = new Vector3(0f, 0f, Random.Range(-35f, 35f));
			notThereAnim.transform.parent.transform.position = mouseToCanvasPos();
			notThereAnim.PlayIn();
		}

		private Vector3 mouseToCanvasPos()
		{
			Ray ray = uiCam.ScreenPointToRay(Input.mousePosition);
			return uiCam.transform.position + ray.direction.normalized * 5f;
		}
	}
}
