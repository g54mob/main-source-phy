using System.Collections.Generic;

namespace Backtrace.Unity.Runtime.Native
{
	internal interface INativeClient
	{
		void HandleAnr(string gameObjectName, string callbackName);

		void GetAttributes(Dictionary<string, string> data);

		void SetAttribute(string key, string value);

		bool OnOOM();

		void UpdateClientTime(float time);

		void Disable();
	}
}
