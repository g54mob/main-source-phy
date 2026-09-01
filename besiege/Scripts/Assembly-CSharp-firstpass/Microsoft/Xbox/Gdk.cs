using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.GameCore.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Microsoft.Xbox
{
	public class Gdk : MonoBehaviour
	{
		public delegate void OnGameSaveLoadedHandler(object sender, GameSaveLoadedArgs e);

		public delegate void OnErrorHandler(object sender, ErrorEventArgs e);

		private const int _100PercentAchievementProgress = 100;

		private const string _GameSaveContainerName = "x_game_save_default_container";

		private const string _GameSaveBlobName = "x_game_save_default_blob";

		private const int _MaxAssociatedProductsToRetrieve = 25;

		[Header("Changing the SCID here will also change the value in your MicrosoftGame.config")]
		[Tooltip("Service Configuration GUID in the form: 12345678-1234-1234-1234-123456789abc")]
		[Delayed]
		public string scid;

		[Tooltip("Will automatically sign the user in after XGameRuntime initialization if checked")]
		public bool signInOnStart = true;

		public Text gamertagLabel;

		private static Gdk _xboxHelpers;

		private static bool _initialized;

		private static Dictionary<int, string> _hresultToFriendlyErrorLookup;

		private string _lastScid = string.Empty;

		public static Gdk Helpers
		{
			get
			{
				if (_xboxHelpers == null)
				{
					Gdk[] array = Object.FindObjectsOfType<Gdk>();
					if (array.Length > 0)
					{
						_xboxHelpers = array[0];
						_xboxHelpers._Initialize();
					}
					else
					{
						Debug.LogError("Error: Could not find Xbox prefab. Make sure you have added the Xbox prefab to your scene.");
					}
				}
				return _xboxHelpers;
			}
		}

		public event OnGameSaveLoadedHandler OnGameSaveLoaded;

		public event OnErrorHandler OnError;

		private bool ValidateGuid(string guid)
		{
			string[] array = guid.Split('-');
			if (array.Length != 5)
			{
				return false;
			}
			if (!array.Select((string str) => str.Length).SequenceEqual(new int[5] { 8, 4, 4, 4, 12 }))
			{
				return false;
			}
			if (!guid.All((char c) => "1234567890abcdef-".Contains(c)))
			{
				return false;
			}
			return true;
		}

		private void OnValidate()
		{
			if (scid == _lastScid)
			{
				return;
			}
			if (scid.Length != 36 || !ValidateGuid(scid))
			{
				Debug.LogError("Invalid SCID given");
				scid = _lastScid;
				return;
			}
			_lastScid = scid;
			XDocument xDocument = XDocument.Load(GdkUtilities.GameConfigPath);
			try
			{
				XElement xElement = (from node in xDocument.Descendants("ExtendedAttribute")
					where node.Attribute("Name").Value == "Scid"
					select node).First();
				xElement.Attribute("Value").Value = scid;
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				xmlWriterSettings.Indent = true;
				xmlWriterSettings.NewLineOnAttributes = true;
				XmlWriterSettings settings = xmlWriterSettings;
				using (XmlWriter w = XmlWriter.Create(GdkUtilities.GameConfigPath, settings))
				{
					xDocument.WriteTo(w);
				}
			}
			catch
			{
				Debug.LogError("Malformed MicrosoftGame.Config. Try associating with the Micosoft Store again or re-import the plugin.");
			}
		}

		private void Start()
		{
			_Initialize();
		}

		private void _Initialize()
		{
			if (!_initialized)
			{
				_initialized = true;
				Object.DontDestroyOnLoad(base.gameObject);
				_hresultToFriendlyErrorLookup = new Dictionary<int, string>();
				InitializeHresultToFriendlyErrorLookup();
			}
		}

		private void InitializeHresultToFriendlyErrorLookup()
		{
			if (_hresultToFriendlyErrorLookup != null)
			{
				_hresultToFriendlyErrorLookup.Add(-2143330041, "IAP_UNEXPECTED: Does the player you are signed in as have a license for the game? You can get one by downloading your game from the store and purchasing it first. If you can't find your game in the store, have you published it in Partner Center?");
				_hresultToFriendlyErrorLookup.Add(-1994108656, "E_GAMEUSER_NO_PACKAGE_IDENTITY: Are you trying to call GDK APIs from the Unity editor? To call GDK APIs, you must use the GDK > Build and Run menu. You can debug your code by attaching the Unity debugger once yourgame is launched.");
				_hresultToFriendlyErrorLookup.Add(-1994129152, "E_GAMERUNTIME_NOT_INITIALIZED: Are you trying to call GDK APIs from the Unity editor? To call GDK APIs, you must use the GDK > Build and Run menu. You can debug your code by attaching the Unity debugger once yourgame is launched.");
				_hresultToFriendlyErrorLookup.Add(-2015559675, "AM_E_XAST_UNEXPECTED: Have you added the Windows 10 PC platform on the Xbox Settings page in Partner Center? Learn more: aka.ms/sandboxtroubleshootingguide");
			}
		}

		public void SignIn()
		{
		}

		public void Save(byte[] data)
		{
		}

		public void LoadSaveData()
		{
		}

		public void UnlockAchievement(string achievementId)
		{
		}

		protected static bool Succeeded(int hresult, string operationFriendlyName)
		{
			bool result = false;
			if (HR.SUCCEEDED(hresult))
			{
				result = true;
			}
			else
			{
				string text = hresult.ToString("X8");
				string empty = string.Empty;
				empty = ((!_hresultToFriendlyErrorLookup.ContainsKey(hresult)) ? (operationFriendlyName + " failed.") : _hresultToFriendlyErrorLookup[hresult]);
				string message = string.Format("{0} Error code: hr=0x{1}", empty, text);
				Debug.LogError(message);
				if (Helpers.OnError != null)
				{
					Helpers.OnError(Helpers, new ErrorEventArgs(text, empty));
				}
			}
			return result;
		}
	}
}
