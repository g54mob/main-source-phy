using UnityEngine;

public class GreyOutUnusableSprites : MonoBehaviour
{
	public enum GreyState
	{
		Default = 0,
		Spectator = 1,
		LimitedMachines = 2,
		OneMachine = 3
	}

	public SpriteRenderer[] spectatorHidden;

	public MonoBehaviour[] spectatorDeactivated;

	public SpriteRenderer[] limitedMachinesHidden;

	public MonoBehaviour[] limitedMachinesDeactivated;

	public SpriteRenderer[] oneMachineHidden;

	public MonoBehaviour[] oneMachineDeactivated;

	protected GreyState _state;

	public GreyState state
	{
		get
		{
			return _state;
		}
		set
		{
			_state = value;
			ChangedState(_state);
		}
	}

	private void LateUpdate()
	{
		if (PlayerData.hasLocalPlayer)
		{
			if (PlayerData.localPlayer.isSpectator)
			{
				if (state != GreyState.Spectator)
				{
					state = GreyState.Spectator;
				}
				return;
			}
			if (StatMaster.limitMachines)
			{
				if (LevelEditor.Instance.Settings.AllowedMachines.Count > 1)
				{
					if (state != GreyState.LimitedMachines)
					{
						state = GreyState.LimitedMachines;
					}
				}
				else if (state != GreyState.OneMachine)
				{
					state = GreyState.OneMachine;
				}
				return;
			}
		}
		if (state != GreyState.Default)
		{
			state = GreyState.Default;
		}
	}

	public void ChangedState(GreyState s)
	{
		switch (state)
		{
		case GreyState.Spectator:
		{
			for (int num4 = 0; num4 < limitedMachinesDeactivated.Length; num4++)
			{
				limitedMachinesDeactivated[num4].enabled = false;
			}
			for (int num5 = 0; num5 < oneMachineDeactivated.Length; num5++)
			{
				oneMachineDeactivated[num5].enabled = false;
			}
			for (int num6 = 0; num6 < spectatorDeactivated.Length; num6++)
			{
				spectatorDeactivated[num6].enabled = false;
			}
			SetSpritesToAlpha(oneMachineHidden, 0.5f);
			SetSpritesToAlpha(limitedMachinesHidden, 0.5f);
			SetSpritesToAlpha(spectatorHidden, 0.5f);
			break;
		}
		case GreyState.LimitedMachines:
		{
			for (int num = 0; num < oneMachineDeactivated.Length; num++)
			{
				oneMachineDeactivated[num].enabled = true;
			}
			for (int num2 = 0; num2 < spectatorDeactivated.Length; num2++)
			{
				spectatorDeactivated[num2].enabled = true;
			}
			for (int num3 = 0; num3 < limitedMachinesDeactivated.Length; num3++)
			{
				limitedMachinesDeactivated[num3].enabled = LevelEditor.Instance.Settings.AllowModMachines;
			}
			SetSpritesToAlpha(spectatorHidden, 1f);
			SetSpritesToAlpha(oneMachineHidden, 1f);
			SetSpritesToAlpha(limitedMachinesHidden, (!LevelEditor.Instance.Settings.AllowModMachines) ? 0.25f : 0.5f);
			break;
		}
		case GreyState.OneMachine:
		{
			for (int l = 0; l < spectatorDeactivated.Length; l++)
			{
				spectatorDeactivated[l].enabled = true;
			}
			for (int m = 0; m < limitedMachinesDeactivated.Length; m++)
			{
				limitedMachinesDeactivated[m].enabled = LevelEditor.Instance.Settings.AllowModMachines;
			}
			for (int n = 0; n < oneMachineDeactivated.Length; n++)
			{
				oneMachineDeactivated[n].enabled = LevelEditor.Instance.Settings.AllowModMachines;
			}
			SetSpritesToAlpha(spectatorHidden, 1f);
			SetSpritesToAlpha(limitedMachinesHidden, (!LevelEditor.Instance.Settings.AllowModMachines) ? 0.25f : 0.5f);
			SetSpritesToAlpha(oneMachineHidden, (!LevelEditor.Instance.Settings.AllowModMachines) ? 0.25f : 0.5f);
			break;
		}
		default:
		{
			for (int i = 0; i < spectatorDeactivated.Length; i++)
			{
				spectatorDeactivated[i].enabled = true;
			}
			for (int j = 0; j < limitedMachinesDeactivated.Length; j++)
			{
				limitedMachinesDeactivated[j].enabled = true;
			}
			for (int k = 0; k < oneMachineDeactivated.Length; k++)
			{
				oneMachineDeactivated[k].enabled = true;
			}
			SetSpritesToAlpha(limitedMachinesHidden, 1f);
			SetSpritesToAlpha(oneMachineHidden, 1f);
			SetSpritesToAlpha(spectatorHidden, 1f);
			break;
		}
		}
	}

	private void SetSpritesToAlpha(SpriteRenderer[] ren, float alpha)
	{
		for (int i = 0; i < ren.Length; i++)
		{
			Color color = ren[i].color;
			color.a = alpha;
			ren[i].color = color;
		}
	}
}
