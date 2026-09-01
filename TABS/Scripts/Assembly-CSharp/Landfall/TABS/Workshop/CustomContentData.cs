using System;

namespace Landfall.TABS.Workshop
{
	public class CustomContentData
	{
		private static double[] m_PreviousVersions = new double[4] { 0.0, 1.0, 2.0, 3.0 };

		public static double Version_Number => m_PreviousVersions[m_PreviousVersions.Length - 1];

		public static int GetCurrentVersionIndex()
		{
			return m_PreviousVersions.Length - 1;
		}

		public static int GetIndexOfVersion(double version)
		{
			int num = m_PreviousVersions.Length;
			for (int i = 0; i < num; i++)
			{
				if (version == m_PreviousVersions[i])
				{
					return i;
				}
			}
			throw new Exception("Halp, Cannot find version: " + version + " In the version list, should not happen!");
		}
	}
}
