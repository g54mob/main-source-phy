using System;
using System.Text;

namespace MagicaCloth2
{
	public interface IManager : IDisposable
	{
		void Initialize();

		void EnterdEditMode();

		void InformationLog(StringBuilder allsb);
	}
}
