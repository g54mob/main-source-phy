using UnityEngine;

namespace LevelCreator
{
	public class Action : MonoBehaviour
	{
		public GameObject actionTasks;

		public GameObject actionLevel;

		private DMEditor dmEditor;

		private void Start()
		{
			dmEditor = DMEditor.Instance;
		}

		private void Update()
		{
			if (actionTasks.transform.childCount == 0)
			{
				int num = 0;
				while (actionLevel.transform.childCount > 0)
				{
					dmEditor.MoveToLevel(actionLevel.transform.GetChild(0).gameObject);
					num++;
				}
				Object.Destroy(base.gameObject);
				Debug.Log("Action finished, moved " + num + " objects");
				if (num > 0)
				{
					dmEditor.ScheduleTakeLevelSnapshot();
				}
			}
		}
	}
}
