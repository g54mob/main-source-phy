using Landfall.TABS.Workshop;
using ModIO;
using UnityEngine;

namespace TFBGames
{
	public class ModIODebugGUI : MonoBehaviour
	{
		private class RedGreenText
		{
			public string text;

			private bool setGreen;

			public RedGreenText(string setText, bool isGreen)
			{
				text = setText;
				setGreen = isGreen;
			}

			public void SetDetails(string setText, bool isGreen)
			{
				text = setText;
				setGreen = isGreen;
			}

			public Color GetColor()
			{
				if (!setGreen)
				{
					return Color.red;
				}
				return Color.green;
			}
		}

		private AccountManager m_AccountManager;

		private CustomContentLoaderModIO customContentLoaderModIO;

		public static ModIODebugGUI instance { get; private set; }

		private void Awake()
		{
			m_AccountManager = ServiceLocator.GetService<AccountManager>();
			customContentLoaderModIO = ServiceLocator.GetService<CustomContentLoaderModIO>();
			Debug.LogError("WARNING: REMOVE OnGUI DEBUG MOD.IO PREFAB FROM SCENE!!!!");
			Object.DestroyImmediate(this);
		}

		private string GetModCount()
		{
			if (LocalUser.Profile == null)
			{
				return $"Subscribed Mods :{0}";
			}
			return $"Subscribed Mods :{LocalUser.SubscribedModIds.Count}";
		}

		private RedGreenText LocalUserIsLoaded()
		{
			if (LocalUser.Profile != null && LocalUser.isLoaded)
			{
				return new RedGreenText($"Local User :{LocalUser.Profile.id}", isGreen: true);
			}
			return new RedGreenText("No User is Loaded", isGreen: false);
		}

		private RedGreenText GetModIOLocalUser()
		{
			if (customContentLoaderModIO != null)
			{
				return new RedGreenText("LocalMod.IOUserId: " + customContentLoaderModIO.LocalModIOUserID, isGreen: true);
			}
			return new RedGreenText("LocalMod.IOUserId: Null", isGreen: false);
		}

		private void OnGUI()
		{
			float num = 5f;
			Vector2 vector = new Vector2(600f, 45f);
			Vector2 vector2 = new Vector2((float)Screen.width - vector.x - num, 80f + num);
			float num2 = 5f;
			Rect position = new Rect(vector2.x - num, vector2.y - num, vector.x + num * 2f, vector.y * num2 + num * 2f);
			GUIStyle gUIStyle = new GUIStyle
			{
				normal = 
				{
					textColor = Color.white
				},
				fontSize = 40
			};
			GUI.Box(position, "");
			GUI.Box(position, "");
			GUI.Label(new Rect(vector2.x, vector2.y, vector.x, vector.y), "MOD.IO USER DEBUG INFO", gUIStyle);
			vector2.y += vector.y;
			RedGreenText redGreenText = LocalUserIsLoaded();
			gUIStyle.normal.textColor = redGreenText.GetColor();
			string text = string.Format(redGreenText.text);
			GUI.Label(new Rect(vector2.x, vector2.y, vector.x, vector.y), text, gUIStyle);
			vector2.y += vector.y;
			gUIStyle.normal.textColor = Color.white;
			text = GetModCount();
			GUI.Label(new Rect(vector2.x, vector2.y, vector.x, vector.y), text, gUIStyle);
			vector2.y += vector.y;
			RedGreenText modIOLocalUser = GetModIOLocalUser();
			text = modIOLocalUser.text;
			gUIStyle.normal.textColor = modIOLocalUser.GetColor();
			GUI.Label(new Rect(vector2.x, vector2.y, vector.x, vector.y), text, gUIStyle);
			vector2.y += vector.y;
			gUIStyle.normal.textColor = Color.white;
		}
	}
}
