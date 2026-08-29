using System;
using System.Collections.Generic;

public class LuaLinks
{
	[NonSerialized]
	public List<Slot> linkedSlots = new List<Slot>();

	[NonSerialized]
	public List<Switch3D> linkedSwitches = new List<Switch3D>();

	[NonSerialized]
	public List<Lock> linkedLocks = new List<Lock>();

	[NonSerialized]
	public List<Trigger> linkedTriggers = new List<Trigger>();

	[NonSerialized]
	public List<Dial> linkedDials = new List<Dial>();

	[NonSerialized]
	public List<Turnable> linkedTurnables = new List<Turnable>();

	[NonSerialized]
	public List<Lookable> linkedLookables = new List<Lookable>();

	[NonSerialized]
	public List<Rotatable> linkedRotatables = new List<Rotatable>();

	[NonSerialized]
	public List<Sound> linkedSounds = new List<Sound>();

	[NonSerialized]
	public List<ActivatorComponent> linkedActivators = new List<ActivatorComponent>();

	[NonSerialized]
	public List<Slidable> linkedSlidables = new List<Slidable>();

	[NonSerialized]
	public List<Roulette> linkedRoulettes = new List<Roulette>();

	[NonSerialized]
	public List<LuaExecutor> linkedLuaExecutors = new List<LuaExecutor>();

	[NonSerialized]
	public List<EditorSkybox> linkedSkyboxes = new List<EditorSkybox>();

	[NonSerialized]
	public List<EditorOcean> linkedOceans = new List<EditorOcean>();

	[NonSerialized]
	public List<EditorClouds> linkedClouds = new List<EditorClouds>();

	[NonSerialized]
	public List<Fog> linkedFogs = new List<Fog>();

	[NonSerialized]
	public List<EditorPostProcessing> linkedPPs = new List<EditorPostProcessing>();

	[NonSerialized]
	public List<Interactive> linkedInteractives = new List<Interactive>();

	[NonSerialized]
	public List<OpenLink> linkedOpenLinks = new List<OpenLink>();

	[NonSerialized]
	public List<Teleport> linkedTeleports = new List<Teleport>();

	[NonSerialized]
	public List<EditorPuzzle> linkedPuzzles = new List<EditorPuzzle>();

	[NonSerialized]
	public List<Finish> linkedFinishes = new List<Finish>();

	[NonSerialized]
	public List<EditorDelay> linkedDelays = new List<EditorDelay>();
}
