using System.Collections.Generic;
using SleepyNodes;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
	private Dictionary<string, OperationState> operationStates;

	public static ProgressionManager Instance { get; private set; }

	public UserProgression UserProgression { get; private set; }

	public IReadOnlyDictionary<string, OperationState> OperationStates => null;

	public OperationState CurrentOperation { get; private set; }

	private string ProgressionSaveRoot => null;

	private string ProgressionPath => null;

	private string OperationFilePattern => null;

	private void Awake()
	{
	}

	public void StartOperation(OperationGraph operation)
	{
	}

	public OperationState GetOperation(string id)
	{
		return null;
	}

	public void SaveAll()
	{
	}

	public void SaveProgression()
	{
	}

	public void SaveOperation(string operationId)
	{
	}

	public void LoadAll()
	{
	}

	public bool IsCardUnlocked(string cardId)
	{
		return false;
	}

	public bool IsSceneObjectUnlocked(string objectId)
	{
		return false;
	}

	public bool UnlockSceneObject(string objectId)
	{
		return false;
	}

	public List<PunchcardDefinitionV2> BuildUnlockedPunchcards(Dictionary<string, PunchcardDefinitionV2> allDefinitions)
	{
		return null;
	}

	public List<string> UnlockPunchcards(IEnumerable<PunchcardDefinitionV2> punchcards)
	{
		return null;
	}

	public void SaveUnlockedCardStates(IEnumerable<PunchcardRuntime> cards)
	{
	}

	public void ResetAllUserProgress()
	{
	}

	public int ForceCompleteMissions(OperationGraph operation)
	{
		return 0;
	}

	private void LoadAllOperations()
	{
	}

	private void MigrateCompletedMissionPunchcardUnlocks()
	{
	}

	private HashSet<string> GetCompletedMissionIds()
	{
		return null;
	}

	private OperationState GetOrCreateOperation(OperationGraph operation)
	{
		return null;
	}

	private string GetOperationPath(string operationId)
	{
		return null;
	}

	private void NormalizeProgression()
	{
	}

	private int GetRemainingUses(string cardId, int fallback)
	{
		return 0;
	}

	private void SaveToFile<T>(T data, string path)
	{
	}

	private T LoadFromFile<T>(string path)
	{
		return default(T);
	}

	private byte[] Compress(byte[] data)
	{
		return null;
	}

	private byte[] Decompress(byte[] data)
	{
		return null;
	}

	private byte[] Encrypt(byte[] data)
	{
		return null;
	}

	private byte[] Decrypt(byte[] data)
	{
		return null;
	}
}
