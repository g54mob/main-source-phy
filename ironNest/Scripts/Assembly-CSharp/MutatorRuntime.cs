using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Missions/Mutators/Mutator Runtime")]
public class MutatorRuntime : MonoBehaviour
{
	[Tooltip("If true, logs mutator transitions to the Console for troubleshooting.")]
	public bool verbose;

	private readonly List<MutatorDefinition> _activeList;

	private readonly HashSet<MutatorDefinition> _activeSet;

	public static MutatorRuntime Instance { get; private set; }

	public IReadOnlyList<MutatorDefinition> ActiveMutators => null;

	public event Action<IReadOnlyList<MutatorDefinition>> MutatorsChanged
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	private void Awake()
	{
	}

	public void SetActiveMutators(IList<MutatorDefinition> mutators)
	{
	}

	public void ClearActiveMutators()
	{
	}

	public bool IsActive(MutatorDefinition mutator)
	{
		return false;
	}
}
