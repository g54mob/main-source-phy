using System.Text.RegularExpressions;
using UnityEngine;

public static class GridCodeConverter
{
	private static readonly Regex codeRegex;

	public static readonly float[] digitThresholds;

	public static string LocalToCode(Vector2 localPos, float cellWidth, float cellHeight, bool yIncreasesUp, int unusedRowDecimals)
	{
		return null;
	}

	public static string LocalToCodeRegion(Vector2 localPos, float cellWidth, float cellHeight, bool yIncreasesUp, int unusedRowDecimals)
	{
		return null;
	}

	public static Vector2 CodeToLocal(string code, float cellWidth, float cellHeight, bool yIncreasesUp)
	{
		return default(Vector2);
	}

	private static int MapFracToDigit(float frac)
	{
		return 0;
	}

	private static string IndexToLetters(int index)
	{
		return null;
	}

	private static int LettersToIndex(string letters)
	{
		return 0;
	}
}
