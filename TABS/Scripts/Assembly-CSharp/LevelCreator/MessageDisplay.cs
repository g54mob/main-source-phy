using UnityEngine;

namespace LevelCreator
{
	public class MessageDisplay : MonoBehaviour
	{
		public GameObject messagePrefabInitial;

		[SerializeField]
		private GameObject messagePrefab;

		public static MessageDisplay instance;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			messagePrefab = messagePrefabInitial;
		}

		public static void DisplayMessage(string message)
		{
			GameObject obj = Object.Instantiate(instance.messagePrefab, instance.transform);
			obj.transform.SetAsFirstSibling();
			obj.GetComponentInChildren<LocalizeText>().LocaleID = message;
			Utility.PlaySound("UI/Wrong2", 0.3f, DMEditor.Instance.playerCamera.transform);
		}

		public void ShowMessage(string message)
		{
			DisplayMessage(message);
		}
	}
}
