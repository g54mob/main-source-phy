using System;
using UnityEngine;

namespace BitCode.Profiles
{
	public interface IProfileSelectionStateProvider
	{
		event Action<IProfileSelectionState> StateChanged;

		IProfileSelectionState GetState(RuntimePlatform platform);
	}
	public interface IProfileSelectionStateProvider<out TProfileState> : IProfileSelectionStateProvider where TProfileState : class, IProfileSelectionState
	{
		new event Action<TProfileState> StateChanged;

		new TProfileState GetState(RuntimePlatform platform);
	}
}
