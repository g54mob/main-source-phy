using System;

namespace Kamgam.UGUIComponentsForSettings
{
	public interface IInputBindingForGUI
	{
		string GetBindingPath();

		void SetBindingPath(string path);

		void StartListening();

		void AddOnCompleteCallback(Action callback);

		void RemoveOnCompleteCallback(Action callback);

		void AddOnCanceledCallback(Action callback);

		void RemoveOnCanceledCallback(Action callback);

		void OnEnable();

		void OnDisable();
	}
}
