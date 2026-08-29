using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.VFX;

public class PirateDarkest4Logic : LevelLogic, ISaveable
{
	public sealed class WinAnimationTimer : Timer
	{
		public int step;

		public override byte getTypeId()
		{
			return 0;
		}

		public WinAnimationTimer()
		{
		}

		public WinAnimationTimer(int step)
		{
			this.step = step;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in step, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			step = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("step: " + $"{step}");
			return stringBuilder.ToString();
		}
	}

	[DontSave]
	public Switch3D obeliskSwitch;

	[DontSave]
	public MaterialState winObeliskGlow;

	[DontSave]
	public VisualEffect[] winVisualEffects;

	[DontSave]
	public GameObject winCutScene;

	[DontSave]
	public AnimationSampler winAnimationSampler;

	[DontSave]
	public float winTurnOnGlowPoint = 1f;

	[DontSave]
	public float winTurnOffGlowPoint = 3.6f;

	[DontSave]
	public Switch3D[] tailSwitches;

	[DontSave]
	public TweenState[] tailTSs;

	[DontSave]
	public TweenState[] tailTriangleSides;

	[DontSave]
	public TweenState smallTriangle1;

	[DontSave]
	public TweenState smallTriangle2;

	[DontSave]
	public MaterialState eye1;

	[DontSave]
	public MaterialState eye2;

	private readonly int[] tailNodesCurrent = new int[44];

	[DontSave]
	private (int, int)[] tailStarts = new(int, int)[4]
	{
		(2, 0),
		(0, 7),
		(2, 10),
		(4, 1)
	};

	[DontSave]
	private List<int[]> tailStartsValid = new List<int[]>
	{
		new int[2] { 0, 1 },
		new int[2] { 0, 1 },
		new int[2] { 0, 2 },
		new int[2] { 0, 1 }
	};

	[DontSave]
	private int[,] tailTriangles = new int[6, 11]
	{
		{
			-1, -1, 0, 1, 2, 3, 4, 5, -1, -1,
			-1
		},
		{
			-1, 6, 7, 8, 9, 10, -1, 11, 12, 13,
			-1
		},
		{
			14, 15, -1, 16, 17, 18, 19, 20, -1, 21,
			22
		},
		{
			-1, 23, 24, 25, -1, -1, 26, 27, 28, 29,
			-1
		},
		{
			-1, 30, 31, 32, 33, 34, 35, -1, 36, 37,
			-1
		},
		{
			-1, -1, 38, 39, 40, -1, 41, 42, 43, -1,
			-1
		}
	};

	[DebugButton("Win Animation", Tint.Default, PostClickAction.ReturnToGame, 0, new object[] { })]
	private void winAnimation()
	{
		winCutScene.SetActive(value: true);
		VisualEffect[] array = winVisualEffects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Play();
		}
		winAnimationSampler.play();
		game.startTimer(new WinAnimationTimer(0), winTurnOnGlowPoint);
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Magic/Darkest_Portal_Activate", winCutScene);
	}

	private void tailCheckGlows()
	{
		TweenState[] array = tailTriangleSides;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].transitionTo("Up", 3f, 0f);
		}
		eye1.transitionTo("NewState", 3f, 0f);
		eye2.transitionTo("NewState", 3f, 0f);
		smallTriangle1.transitionTo("Up", 3f, 0f);
		smallTriangle2.transitionTo("Up", 3f, 0f);
		bool flag = false;
		bool flag2 = false;
		for (int j = 0; j < tailStarts.Length; j++)
		{
			(int, int) start = tailStarts[j];
			if (Array.TrueForAll(tailStartsValid[j], (int x) => tailNodesCurrent[tailTriangles[start.Item1, start.Item2]] != x))
			{
				continue;
			}
			Stack<(int, int)> stack = new Stack<(int, int)>();
			HashSet<(int, int)> hashSet = new HashSet<(int, int)>();
			stack.Push(start);
			hashSet.Add(start);
			while (stack.Count > 0 && stack.Count <= 1)
			{
				(int, int) tuple = stack.Pop();
				bool flag3 = (tuple.Item1 + tuple.Item2) % 2 == 1;
				List<(int, int)> list = getAdjacents(tuple, flag3);
				(int, int) tuple2 = tuple;
				if (tuple2.Item1 == 1 && tuple2.Item2 == 5 && tailNodesCurrent[tailTriangles[0, 6]] == 2)
				{
					list[2] = (0, 5);
					smallTriangle1.transitionTo("Up", 3f);
					smallTriangle2.transitionTo("Up", 3f);
				}
				tuple2 = tuple;
				if (tuple2.Item1 == 3 && tuple2.Item2 == 1 && tailNodesCurrent[tailTriangles[3, 1]] != 2)
				{
					list[2] = (-1, -1);
				}
				if (j == 3)
				{
					tuple2 = tuple;
					if (tuple2.Item1 == 4 && tuple2.Item2 == 1 && tailNodesCurrent[tailTriangles[3, 1]] != 2)
					{
						list[2] = (-1, -1);
					}
				}
				if (j == 2)
				{
					tuple2 = tuple;
					if (tuple2.Item1 == 3 && tuple2.Item2 == 2 && tailNodesCurrent[tailTriangles[3, 1]] == 1 && tailNodesCurrent[tailTriangles[4, 1]] != 1)
					{
						list[1] = (-1, -1);
					}
				}
				bool flag4 = false;
				for (int num = 0; num < list.Count; num++)
				{
					(int, int) tuple3 = list[num];
					tuple2 = tuple3;
					if ((tuple2.Item1 == -1 && tuple2.Item2 == -1) || hashSet.Contains(tuple3))
					{
						continue;
					}
					int num2 = tailNodesCurrent[tailTriangles[tuple.Item1, tuple.Item2]];
					int num3 = tailNodesCurrent[tailTriangles[tuple3.Item1, tuple3.Item2]];
					if (num3 == num2)
					{
						if ((tailNodesCurrent[tailTriangles[tuple3.Item1, tuple3.Item2]] == 0 && num == 2) || (tailNodesCurrent[tailTriangles[tuple3.Item1, tuple3.Item2]] == 1 && num == 1 && !flag3) || (tailNodesCurrent[tailTriangles[tuple3.Item1, tuple3.Item2]] == 2 && num == 0 && !flag3) || (tailNodesCurrent[tailTriangles[tuple3.Item1, tuple3.Item2]] == 1 && num == 0 && flag3) || (tailNodesCurrent[tailTriangles[tuple3.Item1, tuple3.Item2]] == 2 && num == 1 && flag3))
						{
							flag4 = true;
							break;
						}
						continue;
					}
					int num4 = (flag3 ? 1 : (-1));
					if (num == 0 || num == 1)
					{
						int num5 = ((num == 0) ? 1 : (-1));
						int num6 = (flag3 ? 1 : 2);
						if (num == 1 && num6 == 1)
						{
							num6 = 2;
						}
						else if (num == 1 && num6 == 2)
						{
							num6 = 1;
						}
						if (num2 == 1 && num3 == 0 && (spotExcludeValue((tuple3.Item1 + num4, tuple3.Item2), num6) || spotExcludeValue((tuple.Item1, tuple.Item2 - num5), num6)))
						{
							flag4 = true;
							break;
						}
						if (num2 == 1 && num3 == 2)
						{
							if (!flag3)
							{
								num5 *= -1;
							}
							if (spotExcludeValue((tuple.Item1 - num4, tuple.Item2), num6) || spotExcludeValue((tuple3.Item1, tuple3.Item2 + num5), num6))
							{
								flag4 = true;
								break;
							}
						}
						if ((num2 == 1 || num2 == 2) && num3 == 0 && (spotExcludeValue((tuple.Item1, tuple.Item2 - num5), num6) || spotExcludeValue((tuple.Item1 + num4, tuple.Item2 + num5), num6)))
						{
							flag4 = true;
							break;
						}
						if (num2 == 0 && (num3 == 1 || num3 == 2) && (spotExcludeValue((tuple.Item1, tuple3.Item2 + num5), num6) || spotExcludeValue((tuple.Item1 - num4, tuple.Item2), num6)))
						{
							flag4 = true;
							break;
						}
						tuple2 = tuple;
						if ((tuple2.Item1 != 0 || tuple2.Item2 != 6) && num2 == 2 && (num3 == 1 || num3 == 2) && (spotExcludeValue((tuple3.Item1 + num4, tuple3.Item2), num6) || spotExcludeValue((tuple.Item1, tuple.Item2 - num5), num6)))
						{
							flag4 = true;
							break;
						}
					}
					else
					{
						if (num2 == 0 && (num3 == 1 || num3 == 2))
						{
							int num7 = ((num3 == 1) ? 1 : (-1));
							if (flag3)
							{
								num7 *= -1;
							}
							if (spotExcludeValue((tuple.Item1, tuple.Item2 + num7), 0) || spotExcludeValue((tuple.Item1 - num4, tuple.Item2 + num7), 0))
							{
								flag4 = true;
								break;
							}
						}
						if (num2 == 1 && (num3 == 1 || num3 == 2))
						{
							int num8 = ((num3 == 1) ? 1 : (-1));
							if (flag3)
							{
								num8 *= -1;
							}
							if (spotExcludeValue((tuple.Item1, tuple.Item2 + num8), 0) || spotExcludeValue((tuple.Item1 - num4, tuple.Item2 + num8), 0))
							{
								flag4 = true;
								break;
							}
						}
						if (num2 == 2 && (num3 == 1 || num3 == 2))
						{
							int num9 = ((num3 == 1) ? 1 : (-1));
							if (flag3)
							{
								num9 *= -1;
							}
							if (spotExcludeValue((tuple.Item1, tuple.Item2 + num9), 0) || spotExcludeValue((tuple.Item1 - num4, tuple.Item2 + num9), 0))
							{
								flag4 = true;
								break;
							}
						}
					}
					stack.Push(tuple3);
					hashSet.Add(tuple3);
				}
				if (j == 0)
				{
					tuple2 = tuple;
					if (tuple2.Item1 == 0 && tuple2.Item2 == 7)
					{
						Debug.Log("first completed ye");
						eye1.transitionTo("NewState", 3f);
						flag = true;
					}
				}
				if (j == 2)
				{
					tuple2 = tuple;
					if (tuple2.Item1 == 4 && tuple2.Item2 == 1 && tailNodesCurrent[tailTriangles[3, 1]] == 2)
					{
						Debug.Log("second completed ye");
						eye2.transitionTo("NewState", 3f);
						flag2 = true;
					}
				}
				if (flag4)
				{
					break;
				}
				int num10 = tailTriangles[tuple.Item1, tuple.Item2];
				bool flag5 = true;
				if (j == 3)
				{
					tuple2 = tuple;
					if (tuple2.Item1 == 4 && tuple2.Item2 == 1 && tailNodesCurrent[tailTriangles[3, 1]] != 2)
					{
						flag5 = false;
					}
				}
				int num11 = tailNodesCurrent[tailTriangles[tuple.Item1, tuple.Item2]];
				if (flag5)
				{
					tailTriangleSides[num10 * 3 + num11].transitionTo("Up", 3f);
				}
				else
				{
					stack.Clear();
				}
			}
		}
		if (flag && flag2)
		{
			winAnimation();
		}
		List<(int, int)> getAdjacents((int, int) spot, bool upsideDown)
		{
			List<(int, int)> list2 = new List<(int, int)>();
			if (spotIsValid((spot.Item1, spot.Item2 + 1)))
			{
				list2.Add((spot.Item1, spot.Item2 + 1));
			}
			else
			{
				list2.Add((-1, -1));
			}
			if (spotIsValid((spot.Item1, spot.Item2 - 1)))
			{
				list2.Add((spot.Item1, spot.Item2 - 1));
			}
			else
			{
				list2.Add((-1, -1));
			}
			(int, int) tuple4 = (upsideDown ? (spot.Item1 - 1, spot.Item2) : (spot.Item1 + 1, spot.Item2));
			if (spotIsValid(tuple4))
			{
				list2.Add(tuple4);
			}
			else
			{
				list2.Add((-1, -1));
			}
			return list2;
		}
		bool spotExcludeValue((int, int) spot, int value)
		{
			if (!spotIsValid((spot.Item1, spot.Item2)))
			{
				return false;
			}
			return tailNodesCurrent[tailTriangles[spot.Item1, spot.Item2]] != value;
		}
		bool spotIsValid((int, int) spot)
		{
			if (spot.Item1 >= 0 && spot.Item1 <= 5 && spot.Item2 >= 0 && spot.Item2 <= 10)
			{
				return tailTriangles[spot.Item1, spot.Item2] != -1;
			}
			return false;
		}
	}

	public override void onSwitch3D(Switch3D switch3D, Switch3DEvent switchEvent)
	{
		int num = Array.IndexOf(tailSwitches, switch3D);
		if (num != -1 && switchEvent == Switch3DEvent.Start)
		{
			tailNodesCurrent[num]++;
			tailNodesCurrent[num] %= 3;
			tailTSs[num].transitionToDuration(tailNodesCurrent[num].ToString(), 0.1f);
			tailCheckGlows();
		}
		if (switch3D == obeliskSwitch && game.hasAuthority(switch3D))
		{
			game.showExitLevelDialogue();
		}
	}

	public override void onInit()
	{
		tailCheckGlows();
	}

	public override void onUpdate()
	{
	}

	public virtual void save(FastBinaryWriter writer)
	{
	}

	public virtual void load(FastBinaryReader reader)
	{
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
	}

	public override Timer getTimer(byte id)
	{
		if (id == 0)
		{
			return new WinAnimationTimer();
		}
		return null;
	}

	[CompilerGenerated]
	public override void onTimerUpdate(Timer timer)
	{
		_ = timer is WinAnimationTimer;
	}

	[CompilerGenerated]
	public override void onTimerDone(Timer timer)
	{
		if (timer is WinAnimationTimer timer2)
		{
			onWinAnimationTimerDone(timer2);
		}
	}

	private void onWinAnimationTimerDone(WinAnimationTimer timer)
	{
		if (timer.step == 0)
		{
			float duration = winTurnOffGlowPoint - winTurnOnGlowPoint;
			winObeliskGlow.transitionToDuration("Glowing", duration);
			game.startTimer(new WinAnimationTimer(1), duration);
			return;
		}
		if (timer.step == 1)
		{
			float duration2 = winAnimationSampler.clip.length - winTurnOffGlowPoint;
			winObeliskGlow.transitionToDuration("Glowing", duration2, 0f);
			game.startTimer(new WinAnimationTimer(2), duration2);
			return;
		}
		winCutScene.SetActive(value: false);
		VisualEffect[] array = winVisualEffects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Stop();
		}
		game.levelCompleted();
	}
}
