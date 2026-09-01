using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class FireMissionTokenProcessor
{
	public class LineEvalContext
	{
		public string Raw;

		public int LineIndex;

		public Dictionary<string, MapEntity> SelectedIds;

		public Dictionary<string, GridReference> LocationContextKeys;

		public bool noActiveMatchFound;

		public HashSet<EntityRoles> implicatedRoles;

		public Vector2? FromPos;
	}

	public class Command
	{
		public class Parameter
		{
			public List<string> Ids;

			public int Index;

			public List<Option> Options;

			public bool RandomFlagSeen;
		}

		public class Option
		{
			public string Symbol;

			public string Value;
		}

		public static class OptionSynbols
		{
			public static string Index;

			public static string Add;

			public static string Subtract;
		}

		public string Raw;

		public string Cmd;

		public List<string> Modifiers;

		public List<Parameter> Parameters;
	}

	private static readonly Regex tokenRegex;

	private static readonly Regex parametereRegex;

	private static readonly Regex parameterePartRegex;

	public static Command ParseCommand(string text)
	{
		return null;
	}

	private static Command.Parameter ParseParameter(string text)
	{
		return null;
	}

	public static List<string> ProcessBlock(string text, Dictionary<string, GridReference> locationContextKeys = null)
	{
		return null;
	}

	private static string ProcessLine(string line, int lineIndex, LineEvalContext eval = null)
	{
		return null;
	}

	public static string ProcessToken(Command command, LineEvalContext context)
	{
		return null;
	}

	private static string FormatBearing(Command command, LineEvalContext context)
	{
		return null;
	}

	private static string FormatDistance(Command command, LineEvalContext context)
	{
		return null;
	}

	private static string FormatGrid(Command command, LineEvalContext context)
	{
		return null;
	}

	private static string FormatRegion(Command command, LineEvalContext context)
	{
		return null;
	}

	private static string FormatRemaining(Command command, LineEvalContext context)
	{
		return null;
	}

	private static string FormatTimer(Command command, LineEvalContext context)
	{
		return null;
	}

	private static string FormatDirection(Command command, LineEvalContext context)
	{
		return null;
	}

	private static string BearingToCompass(float deg, int level)
	{
		return null;
	}

	private static string MapDirection(float deg, string[] names)
	{
		return null;
	}

	private static string MapDirectionExpanded16(float deg)
	{
		return null;
	}

	private static string Expand16(string abbr)
	{
		return null;
	}

	private static bool TryResolveRelativePosition(string Id, Vector2? reference, out MapEntity entity)
	{
		entity = null;
		return false;
	}

	private static bool TryResolveSpecialPosition(string Id, out Vector2 pos)
	{
		pos = default(Vector2);
		return false;
	}

	private static bool TryResolveLocationContextKey(string Id, LineEvalContext context, out Vector2 pos)
	{
		pos = default(Vector2);
		return false;
	}

	private static bool TryResolvePointPositionOnly(Command.Parameter parameter, LineEvalContext context, out Vector2 pos)
	{
		pos = default(Vector2);
		return false;
	}

	private static bool TryResolveParameterToEntity(Command.Parameter parameter, LineEvalContext context, out MapEntity entity)
	{
		entity = null;
		return false;
	}

	private static bool TryResolvePosition(Command.Parameter parameter, LineEvalContext context, out Vector2 pos)
	{
		pos = default(Vector2);
		return false;
	}

	private static string GetDisplayName(Command command, LineEvalContext context)
	{
		return null;
	}

	private static bool TryResolveRoleToRandomEntity(string roleString, int index, LineEvalContext context, out MapEntity entity)
	{
		entity = null;
		return false;
	}

	private static int StableHash(string s)
	{
		return 0;
	}
}
