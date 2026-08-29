using System;

namespace Battlehub
{
	public class KnownAssemblies
	{
		private static string[] m_assemblyNames = new string[1] { "Assembly-CSharp" };

		public static string[] Names => m_assemblyNames;

		public static void Add(string assemblyName)
		{
			if (Array.IndexOf(m_assemblyNames, assemblyName) < 0)
			{
				int num = m_assemblyNames.Length;
				Array.Resize(ref m_assemblyNames, num + 1);
				m_assemblyNames[num] = assemblyName;
			}
		}
	}
}
