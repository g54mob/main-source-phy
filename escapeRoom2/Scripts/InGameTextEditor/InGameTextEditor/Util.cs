using UnityEngine;

namespace InGameTextEditor
{
	public class Util
	{
		public static bool IsMacOS()
		{
			if (Application.platform != RuntimePlatform.OSXPlayer)
			{
				return Application.platform == RuntimePlatform.OSXEditor;
			}
			return true;
		}

		public static bool IsPrintableCharacter(char c)
		{
			switch (c)
			{
			case '\n':
				return true;
			case ' ':
			case '!':
			case '"':
			case '#':
			case '$':
			case '%':
			case '&':
			case '\'':
			case '(':
			case ')':
			case '*':
			case '+':
			case ',':
			case '-':
			case '.':
			case '/':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
			case ':':
			case ';':
			case '<':
			case '=':
			case '>':
			case '?':
			case '@':
			case 'A':
			case 'B':
			case 'C':
			case 'D':
			case 'E':
			case 'F':
			case 'G':
			case 'H':
			case 'I':
			case 'J':
			case 'K':
			case 'L':
			case 'M':
			case 'N':
			case 'O':
			case 'P':
			case 'Q':
			case 'R':
			case 'S':
			case 'T':
			case 'U':
			case 'V':
			case 'W':
			case 'X':
			case 'Y':
			case 'Z':
			case '[':
			case '\\':
			case ']':
			case '^':
			case '_':
			case '`':
			case 'a':
			case 'b':
			case 'c':
			case 'd':
			case 'e':
			case 'f':
			case 'g':
			case 'h':
			case 'i':
			case 'j':
			case 'k':
			case 'l':
			case 'm':
			case 'n':
			case 'o':
			case 'p':
			case 'q':
			case 'r':
			case 's':
			case 't':
			case 'u':
			case 'v':
			case 'w':
			case 'x':
			case 'y':
			case 'z':
			case '{':
			case '|':
			case '}':
			case '~':
				return true;
			default:
				if (c >= '\u00a0' && c <= 'ÿ')
				{
					return true;
				}
				if (c >= '‘' && c <= '‚')
				{
					return true;
				}
				if (c == '₤' || c == '€')
				{
					return true;
				}
				return false;
			}
		}

		public static string ReplaceTabsWithSpaces(string text, int tabStopWidth, int offset = 0)
		{
			string[] array = text.Split('\t');
			if (array.Length <= 1)
			{
				return text;
			}
			text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text += array[i];
				if (i < array.Length - 1)
				{
					string text2 = "";
					for (int j = 0; j < tabStopWidth - (text.Length + offset) % tabStopWidth; j++)
					{
						text2 += " ";
					}
					text += text2;
				}
			}
			return text;
		}
	}
}
