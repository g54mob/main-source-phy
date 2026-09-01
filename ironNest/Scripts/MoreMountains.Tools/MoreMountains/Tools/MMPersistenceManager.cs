using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMPersistenceManager : MMPersistentSingleton<MMPersistenceManager>, MMEventListener<MMGameEvent>, MMEventListenerBase
	{
		[Header("Persistence")]
		[Tooltip("A persistence ID used to identify the data associated to this manager. Usually you'll want to leave this to its default value.")]
		public string PersistenceID;

		[Header("Events")]
		[Tooltip("whether or not this manager should listen for save events. If you set this to false, you'll have to call SaveToMemory or SaveFromMemoryToFile manually")]
		public bool ListenForSaveEvents;

		[Tooltip("whether or not this manager should listen for load events. If you set this to false, you'll have to call LoadFromMemory or LoadFromFileToMemory manually")]
		public bool ListenForLoadEvents;

		[Tooltip("whether or not this manager should listen for save to memory events. If you set this to false, you'll have to call SaveToMemory manually")]
		public bool ListenForSaveToMemoryEvents;

		[Tooltip("whether or not this manager should listen for load from memory events. If you set this to false, you'll have to call LoadFromMemory manually")]
		public bool ListenForLoadFromMemoryEvents;

		[Tooltip("whether or not this manager should listen for save to file events. If you set this to false, you'll have to call SaveFromMemoryToFile manually")]
		public bool ListenForSaveToFileEvents;

		[Tooltip("whether or not this manager should listen for load from file events. If you set this to false, you'll have to call LoadFromFileToMemory manually")]
		public bool ListenForLoadFromFileEvents;

		[Tooltip("whether or not this manager should save data to file on save events")]
		public bool SaveToFileOnSaveEvents;

		[Tooltip("whether or not this manager should load data from file on load events")]
		public bool LoadFromFileOnLoadEvents;

		[Header("Debug Buttons (Only at Runtime)")]
		[MMInspectorButton("SaveToMemory")]
		public bool SaveToMemoryButton;

		[MMInspectorButton("LoadFromMemory")]
		public bool LoadFromMemoryButton;

		[MMInspectorButton("SaveFromMemoryToFile")]
		public bool SaveToFileButton;

		[MMInspectorButton("LoadFromFileToMemory")]
		public bool LoadFromFileButton;

		[MMInspectorButton("DeletePersistenceFile")]
		public bool DeletePersistenceFileButton;

		public DictionaryStringSceneData SceneDatas;

		public static string _resourceItemPath;

		public static string _saveFolderName;

		public static string _saveFileExtension;

		protected string _currentSceneName;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		protected static void InitializeStatics()
		{
		}

		protected override void Awake()
		{
		}

		public virtual void SaveToMemory()
		{
		}

		public virtual void LoadFromMemory()
		{
		}

		public virtual void SaveFromMemoryToFile()
		{
		}

		public virtual void LoadFromFileToMemory()
		{
		}

		public virtual void Save()
		{
		}

		public virtual void Load()
		{
		}

		public virtual void DeletePersistencyMemoryForScene(string sceneName)
		{
		}

		public virtual void ResetPersistence()
		{
		}

		public virtual void DeletePersistenceMemory()
		{
		}

		public virtual void DeletePersistenceFile()
		{
		}

		protected virtual IMMPersistent[] FindAllPersistentObjects()
		{
			return null;
		}

		protected virtual void ComputeCurrentSceneName()
		{
		}

		protected virtual string DetermineSaveName()
		{
			return null;
		}

		public virtual void OnMMEvent(MMGameEvent gameEvent)
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}
	}
}
