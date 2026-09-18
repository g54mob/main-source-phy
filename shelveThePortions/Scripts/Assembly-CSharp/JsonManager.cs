using System.Collections.Generic;
using System.IO;

public class JsonManager
{
	public static List<string> LoadAllJsonFileFromFile(string folderName)
	{
		return LoadAllJsonFile(SaveSystem.GetSaveDir(folderName) + "/");
	}

	public static List<string> LoadAllJsonFile()
	{
		return LoadAllJsonFile(SaveSystem.GetSaveDir() + "/");
	}

	private static List<string> LoadAllJsonFile(string path)
	{
		List<string> list = new List<string>();
		FileInfo[] files = new DirectoryInfo(path).GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			StreamReader streamReader = new StreamReader(path + fileInfo.Name);
			list.Add(streamReader.ReadToEnd());
			streamReader.Close();
		}
		return list;
	}

	public static void DeleteJsonFile(string SavefolderName, string fileSaveName)
	{
		File.Delete(SaveSystem.GetSaveDir(SavefolderName) + "/" + fileSaveName + SaveSystem.filePrefex);
	}

	public static void DeleteJsonFile(string fileSaveName)
	{
		File.Delete(SaveSystem.GetSaveDir() + "/" + fileSaveName + SaveSystem.filePrefex);
	}

	public static string LoadJsonFile(string SavefolderName, string fileSaveName)
	{
		StreamReader streamReader = new StreamReader(SaveSystem.GetSaveDir(SavefolderName) + "/" + fileSaveName + SaveSystem.filePrefex);
		string result = streamReader.ReadToEnd();
		streamReader.Close();
		return result;
	}

	public static string LoadJsonFile(string fileSaveName)
	{
		StreamReader streamReader = new StreamReader(SaveSystem.GetSaveDir() + "/" + fileSaveName + SaveSystem.filePrefex);
		string result = streamReader.ReadToEnd();
		streamReader.Close();
		return result;
	}

	public static void SaveJsonFile(string fileSaveName, string js)
	{
		StreamWriter streamWriter = new StreamWriter(SaveSystem.GetSaveDir() + "/" + fileSaveName + SaveSystem.filePrefex);
		streamWriter.Write(js);
		streamWriter.Close();
	}

	public static void SaveJsonFile(string SavefolderName, string fileSaveName, string js)
	{
		StreamWriter streamWriter = new StreamWriter(SaveSystem.GetSaveDir(SavefolderName) + "/" + fileSaveName + SaveSystem.filePrefex);
		streamWriter.Write(js);
		streamWriter.Close();
	}

	public static string GetFormatedJson(string s)
	{
		char[] array = s.ToCharArray();
		int num = 0;
		bool flag = false;
		string text = "";
		char[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			char c = array2[i];
			string text2 = "";
			if (object.Equals('"', c))
			{
				flag = !flag;
			}
			switch (c)
			{
			case '{':
				num++;
				text2 = text2 + c + "\n" + GetSpaces(num);
				break;
			case '}':
				num--;
				text2 = text2 + "\n" + GetSpaces(num) + c;
				break;
			case '[':
				num++;
				text2 = text2 + "\n" + GetSpaces(num - 1) + c + "\n" + GetSpaces(num);
				break;
			case ']':
				num--;
				text2 = text2 + "\n" + GetSpaces(num) + c;
				break;
			case ',':
				text2 = ((!flag) ? (text2 + c + "\n" + GetSpaces(num)) : c.ToString());
				break;
			default:
				text2 = c.ToString();
				break;
			}
			text += text2;
		}
		return text;
	}

	private static string GetSpaces(int num)
	{
		string text = "";
		for (int i = 0; i < num; i++)
		{
			text += "\t";
		}
		return text;
	}
}
