using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.Scripting;

public class LuaExecutor : MonoBehaviour
{
	public string luaCodeLocation;

	public bool canBeTriggered;

	public string functionToCall = "";

	[NonSerialized]
	public List<string> callFilter = new List<string>();

	[NonSerialized]
	public LuaLinks luaLinks = new LuaLinks();

	[NonSerialized]
	public Script lua = new Script();

	[NonSerialized]
	public HashSet<LuaVariable> initialGlobalVariables = new HashSet<LuaVariable>();

	[NonSerialized]
	public Game game;

	[Preserve]
	public DynValue call(string function, params object[] args)
	{
		return game.callLuaFunction(lua, function, args);
	}
}
