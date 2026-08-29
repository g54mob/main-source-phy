using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.VFX;

public class PirateDarkest3Logic : LevelLogic, ISaveable
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

	public sealed class CancelValueTimer : Timer
	{
		public override byte getTypeId()
		{
			return 1;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class FinishTimer : Timer
	{
		public override byte getTypeId()
		{
			return 2;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class GunpowderFlyInDarkestTransition : Transition
	{
		public override byte getTypeId()
		{
			return 3;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			base.writeData(writer);
		}

		public override void readData(FastBinaryReader reader)
		{
			base.readData(reader);
		}
	}

	public sealed class GunpowderFlyBackDarkestTimer : Timer
	{
		public Vector3 pos;

		public Quaternion rot;

		public Vector3 scale;

		public override byte getTypeId()
		{
			return 4;
		}

		public GunpowderFlyBackDarkestTimer()
		{
		}

		public GunpowderFlyBackDarkestTimer(Vector3 pos, Quaternion rot, Vector3 scale)
		{
			this.pos = pos;
			this.rot = rot;
			this.scale = scale;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteVector3(in pos);
			writer.WriteQuaternion(in rot);
			writer.WriteVector3(in scale);
		}

		public override void readData(FastBinaryReader reader)
		{
			pos = reader.ReadVector3();
			rot = reader.ReadQuaternion();
			scale = reader.ReadVector3();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("pos: " + $"{pos}");
			stringBuilder.AppendLine("rot: " + $"{rot}");
			stringBuilder.Append("scale: " + $"{scale}");
			return stringBuilder.ToString();
		}
	}

	public class TrailGunpowderDarkest
	{
		public Trackable trackable;

		public Renderer renderer;

		public MaterialState dissolve;

		public GameObject trail;

		public GameObject start;

		public GameObject end;

		public GameObject particles;

		public TweenState tween;

		public TweenState platformTween;

		public int platformTweenGoal;

		public bool state;

		[DontSave]
		public List<int> startTrails;

		[DontSave]
		public List<int> endTrails;

		public bool fireFromStart;

		public bool fire(Vector3 startingPosition)
		{
			if (dissolve.getTargetWeight("NewState") == 1f)
			{
				return false;
			}
			if (platformTween != null && platformTween.getWeight("Down") != (float)platformTweenGoal)
			{
				return false;
			}
			float num = Vector3.Distance(startingPosition, start.transform.position);
			float num2 = Vector3.Distance(startingPosition, end.transform.position);
			fireFromStart = num < num2;
			particles.transform.SetParent((fireFromStart ? start : end).transform);
			particles.transform.localPosition = Vector3.zero;
			tween.transitionTo(fireFromStart ? "ToEnd" : "ToStart", 2f);
			particles.SetActive(value: true);
			state = true;
			return true;
		}

		public void onTweenDone(List<TrailGunpowderDarkest> trailGunpowderOnFire, List<TrailGunpowderDarkest> trailGunpowders)
		{
			particles.SetActive(value: false);
			dissolve.setWeight("NewState");
			trackable.targetable = true;
			foreach (int item in fireFromStart ? endTrails : startTrails)
			{
				if (!trailGunpowderOnFire.Contains(trailGunpowders[item]) && trailGunpowders[item].fire((fireFromStart ? end : start).transform.position))
				{
					trailGunpowderOnFire.Add(trailGunpowders[item]);
				}
			}
		}
	}

	public sealed class OnStartGunpowderPacket : Packet
	{
		public List<bool> gunpowderStates;

		public override byte getTypeId()
		{
			return 0;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteList(gunpowderStates, delegate(FastBinaryWriter w, bool e)
			{
				w.Write(in e, default(FastBinaryWriter.ForPrimitives));
			});
		}

		public override void readData(FastBinaryReader reader)
		{
			gunpowderStates = reader.ReadList((FastBinaryReader r) => r.ReadBoolean());
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("gunpowderStates: " + ToStringHelper.Stringify(gunpowderStates, (bool e) => $"{e}"));
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
	public Switch3D[] platforms;

	[DontSave]
	public RefArray<Trackable, Renderer, TweenState, GameObject> gunpowderTrailPowders;

	[DontSave]
	public MaterialState[] gunpowderDissolve;

	[DontSave]
	public MaterialState[] gunpowderTrailHover;

	[DontSave]
	public ParticleSystem[] gunpowderBarrelExplosions;

	[DontSave]
	public GameObject gunpowderSpark;

	[DontSave]
	public Item gunpowderPouch;

	[DontSave]
	public Ref<TweenState, Transform, GameObject> gunpowderPouchImpostor;

	[DontSave]
	public Ref<Transform> gunpowderPouchImpostorRendTarget;

	[DontSave]
	public ParticleSystem gunpowderPouchParticle;

	[DontSave]
	public Switch3D gunpowderStartFlameSwitch;

	[DontSave]
	public TweenState[] explodeBarrels;

	private List<TrailGunpowderDarkest> trailGunpowders;

	private float gunpowderDropTimer;

	private int currentGunpowderDroplet;

	private int nextGunpowderFloorPowder;

	private Vector3 gunpowderStartingLocalPos;

	private List<TrailGunpowderDarkest> trailGunpowderOnFire = new List<TrailGunpowderDarkest>();

	private bool gunpowderAnimActive;

	private int currentGunpowderIndex;

	private float explodeValue;

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

	public override void onUpdate()
	{
		foreach (TrailGunpowderDarkest trailGunpowder in trailGunpowders)
		{
			if (trailGunpowder.platformTween != null)
			{
				float target = ((trailGunpowder.platformTween.findStateByName("Down").targetWeight == (float)trailGunpowder.platformTweenGoal) ? 0.91f : 0.7839323f);
				float y = Mathf.MoveTowards(trailGunpowder.trail.transform.localScale.y, target, Time.deltaTime * 0.3f);
				trailGunpowder.trail.transform.localScale = new Vector3(trailGunpowder.trail.transform.localScale.x, y, trailGunpowder.trail.transform.localScale.y);
			}
		}
		bool flag = game.isInAnyPlayerHand(gunpowderPouch);
		for (int i = 0; i < gunpowderTrailPowders.Length; i++)
		{
			gunpowderTrailPowders[i].Get<Trackable>(0).targetable = flag || !trailGunpowders[i].state;
		}
		bool flag2 = game.isSelectedInPCMode(gunpowderPouch.gameObject) && !game.isInTopZoom(gunpowderPouch.gameObject);
		MaterialState[] array = gunpowderTrailHover;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].transitionTo("Default", 3f);
		}
		int num = -1;
		float num2 = 99f;
		for (int k = 0; k < trailGunpowders.Count; k++)
		{
			if (trailGunpowders[k].trackable.GetComponentInChildren<Collider>().Raycast(game.playerViewRay, out var hitInfo, 10f) && Game.inReach(game.playerViewRay.origin, hitInfo.point, 2.5f) && hitInfo.distance < num2 && (flag2 || !trailGunpowders[k].state))
			{
				num = k;
				num2 = hitInfo.distance;
			}
		}
		if (num != -1)
		{
			gunpowderTrailHover[num].transitionTo("Hover", 3f);
		}
	}

	public override void onInit()
	{
		initGunpowder();
	}

	public override void onInitAfterLoad()
	{
		float num = 0.3f;
		foreach (TrailGunpowderDarkest trailGunpowder in trailGunpowders)
		{
			trailGunpowder.startTrails = new List<int>();
			trailGunpowder.endTrails = new List<int>();
			foreach (TrailGunpowderDarkest trailGunpowder2 in trailGunpowders)
			{
				if (trailGunpowder != trailGunpowder2)
				{
					if (trailGunpowder.start != null && trailGunpowder2.start != null && Vector3.Distance(trailGunpowder.start.transform.position, trailGunpowder2.start.transform.position) <= num)
					{
						trailGunpowder.startTrails.Add(trailGunpowders.IndexOf(trailGunpowder2));
					}
					if (trailGunpowder.start != null && trailGunpowder2.end != null && Vector3.Distance(trailGunpowder.start.transform.position, trailGunpowder2.end.transform.position) <= num)
					{
						trailGunpowder.startTrails.Add(trailGunpowders.IndexOf(trailGunpowder2));
					}
					if (trailGunpowder.end != null && trailGunpowder2.start != null && Vector3.Distance(trailGunpowder.end.transform.position, trailGunpowder2.start.transform.position) <= num)
					{
						trailGunpowder.endTrails.Add(trailGunpowders.IndexOf(trailGunpowder2));
					}
					if (trailGunpowder.end != null && trailGunpowder2.end != null && Vector3.Distance(trailGunpowder.end.transform.position, trailGunpowder2.end.transform.position) <= num)
					{
						trailGunpowder.endTrails.Add(trailGunpowders.IndexOf(trailGunpowder2));
					}
				}
			}
		}
	}

	public override void onTrackable(Trackable trackable, TrackableEvent trackableEvent)
	{
		if (trackableEvent.type != TrackableEventType.Start || trailGunpowderOnFire.Count != 0)
		{
			return;
		}
		foreach (TrailGunpowderDarkest trailGunpowder in trailGunpowders)
		{
			if (trailGunpowder.trackable == trackable)
			{
				if (!trailGunpowder.state)
				{
					trailGunpowder.state = true;
					trailGunpowder.dissolve.transitionTo("NewState");
				}
				break;
			}
		}
	}

	private void initGunpowder()
	{
		trailGunpowders = new List<TrailGunpowderDarkest>();
		foreach (Ref<Trackable, Renderer, TweenState, GameObject> gunpowderTrailPowder in gunpowderTrailPowders)
		{
			TrailGunpowderDarkest item = new TrailGunpowderDarkest
			{
				trail = gunpowderTrailPowder,
				trackable = gunpowderTrailPowder.Get<Trackable>(0),
				renderer = gunpowderTrailPowder.Get<Renderer>(0f),
				tween = gunpowderTrailPowder.Get<TweenState>(0u),
				start = gunpowderTrailPowder.Get<GameObject>((short)0).transform.Find("Start")?.gameObject,
				end = gunpowderTrailPowder.Get<GameObject>((short)0).transform.Find("End")?.gameObject,
				particles = gunpowderTrailPowder.Get<GameObject>((short)0).transform.Find("BruningVFX/pf_vfx-ult_xp-ckit_psys_oneshot_realisticFire-alpha")?.gameObject,
				startTrails = new List<int>(),
				endTrails = new List<int>()
			};
			trailGunpowders.Add(item);
		}
		for (int i = 0; i < trailGunpowders.Count; i++)
		{
			TrailGunpowderDarkest trailGunpowderDarkest = trailGunpowders[i];
			if (i == 11 || i == 49)
			{
				trailGunpowderDarkest.platformTween = platforms[7].tweenState;
				trailGunpowderDarkest.platformTweenGoal = 1;
			}
			if (i == 24 || i == 48)
			{
				trailGunpowderDarkest.platformTween = platforms[6].tweenState;
				trailGunpowderDarkest.platformTweenGoal = 0;
			}
			if (i == 27)
			{
				trailGunpowderDarkest.platformTween = platforms[5].tweenState;
				trailGunpowderDarkest.platformTweenGoal = 1;
			}
			if (i == 4)
			{
				trailGunpowderDarkest.platformTween = platforms[4].tweenState;
				trailGunpowderDarkest.platformTweenGoal = 0;
			}
			if (i == 39 || i == 40)
			{
				trailGunpowderDarkest.platformTween = platforms[3].tweenState;
				trailGunpowderDarkest.platformTweenGoal = 0;
			}
			if (i == 15 || i == 16)
			{
				trailGunpowderDarkest.platformTween = platforms[2].tweenState;
				trailGunpowderDarkest.platformTweenGoal = 1;
			}
			if (i == 7 || i == 53)
			{
				trailGunpowderDarkest.platformTween = platforms[1].tweenState;
				trailGunpowderDarkest.platformTweenGoal = 0;
			}
			if (i == 20 || i == 44)
			{
				trailGunpowderDarkest.platformTween = platforms[0].tweenState;
				trailGunpowderDarkest.platformTweenGoal = 1;
			}
			trailGunpowderDarkest.dissolve = gunpowderDissolve[i];
			if (!trailGunpowderDarkest.renderer.enabled)
			{
				trailGunpowderDarkest.state = true;
				trailGunpowderDarkest.dissolve.setWeight("NewState");
			}
			trailGunpowderDarkest.renderer.enabled = true;
		}
		float num = 0.3f;
		foreach (TrailGunpowderDarkest trailGunpowder in trailGunpowders)
		{
			foreach (TrailGunpowderDarkest trailGunpowder2 in trailGunpowders)
			{
				if (trailGunpowder != trailGunpowder2)
				{
					if (trailGunpowder.start != null && trailGunpowder2.start != null && Vector3.Distance(trailGunpowder.start.transform.position, trailGunpowder2.start.transform.position) <= num)
					{
						trailGunpowder.startTrails.Add(trailGunpowders.IndexOf(trailGunpowder2));
					}
					if (trailGunpowder.start != null && trailGunpowder2.end != null && Vector3.Distance(trailGunpowder.start.transform.position, trailGunpowder2.end.transform.position) <= num)
					{
						trailGunpowder.startTrails.Add(trailGunpowders.IndexOf(trailGunpowder2));
					}
					if (trailGunpowder.end != null && trailGunpowder2.start != null && Vector3.Distance(trailGunpowder.end.transform.position, trailGunpowder2.start.transform.position) <= num)
					{
						trailGunpowder.endTrails.Add(trailGunpowders.IndexOf(trailGunpowder2));
					}
					if (trailGunpowder.end != null && trailGunpowder2.end != null && Vector3.Distance(trailGunpowder.end.transform.position, trailGunpowder2.end.transform.position) <= num)
					{
						trailGunpowder.endTrails.Add(trailGunpowders.IndexOf(trailGunpowder2));
					}
				}
			}
		}
	}

	private void onGunpowderPouch(ToolContext context)
	{
		if (gunpowderAnimActive || context.state != ToolState.Start || !(context.currentTarget != null))
		{
			return;
		}
		int num = -1;
		for (int i = 0; i < trailGunpowders.Count; i++)
		{
			if (trailGunpowders[i].trackable.gameObject == context.currentTarget.gameObject)
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			return;
		}
		if (trailGunpowderOnFire.Count != 0)
		{
			gunpowderPouch.unlockItemInteractions = game.hasAuthority(gunpowderPouch.gameObject);
			gunpowderPouch.hideItemInHand = false;
		}
		else if (trailGunpowders[num].state)
		{
			gunpowderPouchImpostor.Get<GameObject>(0u).SetActive(value: true);
			gunpowderPouchImpostor.Get<Transform>(0f).position = context.currentTarget.transform.position;
			gunpowderPouchImpostor.Get<Transform>(0f).rotation = context.currentTarget.transform.rotation * Quaternion.Euler(0f, 0f, 85.425f);
			Vector3 position = gunpowderPouchImpostorRendTarget.Get<Transform>().position;
			Quaternion rotation = gunpowderPouchImpostorRendTarget.Get<Transform>().rotation;
			Vector3 one = Vector3.one;
			Transform transform = null;
			transform = ((!game.hasAuthority(gunpowderPouch)) ? game.getPlayerWithItemInInventory(gunpowderPouch.gameObject).inHandItemBubble.transform : game.pcSelectedItemImpostors[0].transform);
			gunpowderPouchImpostorRendTarget.Get<Transform>().position = transform.position;
			gunpowderPouchImpostorRendTarget.Get<Transform>().rotation = transform.rotation;
			gunpowderPouchImpostorRendTarget.Get<Transform>().localScale = transform.localScale;
			gunpowderPouch.hideItemInHand = true;
			game.startTransitionGlobal(new GunpowderFlyInDarkestTransition(), gunpowderPouchImpostorRendTarget, 0.5f, 0f, position, rotation, one);
			for (int j = 0; j < gunpowderTrailPowders.Length; j++)
			{
				if (gunpowderTrailPowders[j].Get<Trackable>(0) == context.currentTarget)
				{
					currentGunpowderIndex = j;
					break;
				}
			}
			gunpowderAnimActive = true;
		}
		else
		{
			trailGunpowders[num].state = true;
			trailGunpowders[num].dissolve.transitionTo("NewState");
			gunpowderPouch.unlockItemInteractions = game.hasAuthority(gunpowderPouch.gameObject);
			gunpowderPouch.hideItemInHand = false;
		}
	}

	public override void onPacket(Packet packet)
	{
		if (!(packet is OnStartGunpowderPacket onStartGunpowderPacket))
		{
			return;
		}
		Debug.Log(string.Format("[Gunpowder] Received {0} | Count {1}", "OnStartGunpowderPacket", onStartGunpowderPacket.gunpowderStates.Count));
		for (int i = 0; i < onStartGunpowderPacket.gunpowderStates.Count && i < trailGunpowders.Count; i++)
		{
			if (trailGunpowders[i].state != onStartGunpowderPacket.gunpowderStates[i])
			{
				Debug.LogWarning("[Gunpowder] State missmatch: " + i);
			}
			trailGunpowders[i].state = onStartGunpowderPacket.gunpowderStates[i];
			trailGunpowders[i].dissolve.transitionTo("NewState", 1f, trailGunpowders[i].state ? 1f : 0f);
		}
		startGunpowderFire();
	}

	private void startGunpowderFireSynced()
	{
		if (!game.isHost())
		{
			return;
		}
		List<bool> list = new List<bool>();
		foreach (TrailGunpowderDarkest trailGunpowder in trailGunpowders)
		{
			list.Add(trailGunpowder.state);
		}
		game.session.send(new OnStartGunpowderPacket
		{
			gunpowderStates = list
		});
		startGunpowderFire();
	}

	private void startGunpowderFire()
	{
		trailGunpowderOnFire.Clear();
		if (trailGunpowders[0].fire(gunpowderSpark.transform.position))
		{
			trailGunpowderOnFire.Add(trailGunpowders[0]);
			gunpowderStartFlameSwitch.targetable = false;
		}
		gunpowderStartFlameSwitch.tweenState.setState("Down", 0f);
		Switch3D[] array = platforms;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
	}

	private void completeGunpowderFire()
	{
		gunpowderStartFlameSwitch.targetable = true;
		foreach (TrailGunpowderDarkest trailGunpowder in trailGunpowders)
		{
			trailGunpowder.tween.setState("Default", 0f);
		}
		Switch3D[] array = platforms;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = true;
		}
	}

	private void explodeBarrel(int index)
	{
		gunpowderBarrelExplosions[index].Play();
		explodeBarrels[index].transitionTo("Shake", 3f);
		explodeValue += 0.25f;
		if (explodeValue == 1f)
		{
			game.startTimer(new FinishTimer(), 1.5f);
		}
		else
		{
			game.cancelTimers<CancelValueTimer>();
			game.startTimer(new CancelValueTimer(), 0.5f);
		}
		Debug.Log($"EXPLODE BARREL {index} " + Time.time);
	}

	private void onGunpowderPoleTweenDone(TweenState tween)
	{
		tween.transitionTo("Up", 1f, 0f);
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveGunpowder()
	{
		Debug.Log("SOLVED GUNPOWDER");
		winAnimation();
	}

	public override void onTimerUpdate(Timer timer)
	{
		if (!(timer is GunpowderFlyBackDarkestTimer gunpowderFlyBackDarkestTimer))
		{
			return;
		}
		Transform transform = null;
		if (game.hasAuthority(gunpowderPouch.gameObject))
		{
			if (game.pcSelectedItemImpostors != null && game.pcSelectedItemImpostors.Length != 0)
			{
				transform = game.pcSelectedItemImpostors[0].transform;
			}
		}
		else
		{
			Game.GamePlayerData playerWithItemInInventory = game.getPlayerWithItemInInventory(gunpowderPouch.gameObject);
			if (playerWithItemInInventory != null && playerWithItemInInventory.inHandItemBubble != null)
			{
				transform = playerWithItemInInventory.inHandItemBubble.transform;
			}
		}
		if (transform != null)
		{
			float t = gunpowderFlyBackDarkestTimer.time / gunpowderFlyBackDarkestTimer.duration;
			Vector3 b = new Vector3(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
			gunpowderPouchImpostorRendTarget.Get<Transform>().position = Vector3.Lerp(gunpowderFlyBackDarkestTimer.pos, transform.position, t);
			gunpowderPouchImpostorRendTarget.Get<Transform>().rotation = Quaternion.Lerp(gunpowderFlyBackDarkestTimer.rot, transform.rotation, t);
			gunpowderPouchImpostorRendTarget.Get<Transform>().localScale = Vector3.Lerp(gunpowderFlyBackDarkestTimer.scale, b, t);
		}
	}

	public override void onTweenTransitionDone(TweenState tweenState, string state)
	{
		if (tweenState == explodeBarrels[0] && state == "End")
		{
			winAnimation();
		}
		if (tweenState == gunpowderPouchImpostor.Get<TweenState>(0))
		{
			trailGunpowders[currentGunpowderIndex].state = false;
			gunpowderDissolve[currentGunpowderIndex].setWeight("NewState", 0f);
			game.startTimer(new GunpowderFlyBackDarkestTimer(gunpowderPouchImpostorRendTarget.Get<Transform>().position, gunpowderPouchImpostorRendTarget.Get<Transform>().rotation, gunpowderPouchImpostorRendTarget.Get<Transform>().localScale), 0.5f);
		}
		TweenState[] array = explodeBarrels;
		foreach (TweenState tweenState2 in array)
		{
			if (tweenState == tweenState2 && state == "Shake")
			{
				tweenState2.transitionTo("Shake", 2f, 0f);
			}
		}
		for (int j = 0; j < trailGunpowders.Count; j++)
		{
			TrailGunpowderDarkest trailGunpowderDarkest = trailGunpowders[j];
			if (trailGunpowderDarkest.tween == tweenState)
			{
				trailGunpowderDarkest.onTweenDone(trailGunpowderOnFire, trailGunpowders);
				trailGunpowderOnFire.Remove(trailGunpowderDarkest);
				if (trailGunpowderOnFire.Count == 0)
				{
					completeGunpowderFire();
				}
				if (j == 19)
				{
					explodeBarrel(0);
				}
				if (j == 14)
				{
					explodeBarrel(1);
				}
				if (j == 43)
				{
					explodeBarrel(2);
				}
				if (j == 38)
				{
					explodeBarrel(3);
				}
			}
		}
	}

	public override void onSwitch3D(Switch3D switch3D, Switch3DEvent switchEvent)
	{
		if (switchEvent == Switch3DEvent.Start)
		{
			for (int i = 0; i < platforms.Length; i++)
			{
				if (switch3D == platforms[i])
				{
					int num = i + 1;
					if (i % 2 == 1)
					{
						num = i - 1;
					}
					int num2 = ((platforms[num].tweenState.findStateByName("Down").targetWeight != 1f) ? 1 : 0);
					platforms[num].tweenState.transitionTo("Down", 1f, num2);
				}
			}
		}
		if (switchEvent == Switch3DEvent.On && switch3D == gunpowderStartFlameSwitch)
		{
			startGunpowderFireSynced();
		}
		if (switch3D == obeliskSwitch && game.hasAuthority(switch3D))
		{
			game.showExitLevelDialogue();
		}
	}

	public override void onTimerDone(Timer timer)
	{
		if (timer is GunpowderFlyInDarkestTransition)
		{
			gunpowderPouchImpostor.Get<TweenState>(0).transitionTo("NewState", 1.24f);
			gunpowderPouchParticle.Play();
		}
		if (timer is GunpowderFlyBackDarkestTimer)
		{
			gunpowderPouchImpostor.Get<GameObject>(0u).SetActive(value: false);
			gunpowderPouch.unlockItemInteractions = game.hasAuthority(gunpowderPouch.gameObject);
			gunpowderPouch.hideItemInHand = false;
			gunpowderPouchImpostor.Get<TweenState>(0).setState("NewState", 0f);
			gunpowderAnimActive = false;
		}
		if (timer is FinishTimer)
		{
			TweenState[] array = explodeBarrels;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].transitionTo("End");
			}
		}
		if (timer is CancelValueTimer && !timer.isCancelled)
		{
			explodeValue = 0f;
		}
		if (timer is WinAnimationTimer)
		{
			onWinAnimationTimerDone((WinAnimationTimer)timer);
		}
	}

	public override void onTool(Item tool, ToolContext context)
	{
		if (tool == gunpowderPouch)
		{
			onGunpowderPouch(context);
		}
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.WriteList(trailGunpowders, delegate(FastBinaryWriter w, TrailGunpowderDarkest e)
		{
			w.WriteTrailGunpowderDarkest(e);
		});
		writer.Write(in gunpowderDropTimer, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentGunpowderDroplet, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in nextGunpowderFloorPowder, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in gunpowderStartingLocalPos);
		writer.WriteList(trailGunpowderOnFire, delegate(FastBinaryWriter w, TrailGunpowderDarkest e)
		{
			w.WriteTrailGunpowderDarkest(e);
		});
		writer.Write(in gunpowderAnimActive, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentGunpowderIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in explodeValue, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		trailGunpowders = reader.ReadList((FastBinaryReader r) => r.ReadTrailGunpowderDarkest());
		gunpowderDropTimer = reader.ReadSingle();
		currentGunpowderDroplet = reader.ReadInt32();
		nextGunpowderFloorPowder = reader.ReadInt32();
		gunpowderStartingLocalPos = reader.ReadVector3();
		trailGunpowderOnFire = reader.ReadList((FastBinaryReader r) => r.ReadTrailGunpowderDarkest());
		gunpowderAnimActive = reader.ReadBoolean();
		currentGunpowderIndex = reader.ReadInt32();
		explodeValue = reader.ReadSingle();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		List<TrailGunpowderDarkest> list = reader.ReadList((FastBinaryReader r) => r.ReadTrailGunpowderDarkest());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "trailGunpowders[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gunpowderDropTimer",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num2 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentGunpowderDroplet",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num3 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "nextGunpowderFloorPowder",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gunpowderStartingLocalPos",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<TrailGunpowderDarkest> list2 = reader.ReadList((FastBinaryReader r) => r.ReadTrailGunpowderDarkest());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "trailGunpowderOnFire[" + ((list2 == null) ? string.Empty : list2.Count.ToString()) + "]",
			fieldValue = (((list2 == null) ? "null" : string.Join(", ", list2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gunpowderAnimActive",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num4 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentGunpowderIndex",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num5 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "explodeValue",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override int getPacketCount()
	{
		return PirateDarkest3.getPacketCount();
	}

	public override Packet getPacket(byte id)
	{
		return PirateDarkest3.getPacket(id);
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new WinAnimationTimer(), 
			1 => new CancelValueTimer(), 
			2 => new FinishTimer(), 
			3 => new GunpowderFlyInDarkestTransition(), 
			4 => new GunpowderFlyBackDarkestTimer(), 
			_ => null, 
		};
	}
}
