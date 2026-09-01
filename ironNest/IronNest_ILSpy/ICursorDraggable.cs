using System;

public interface ICursorDraggable
{
	bool IsDragging { get; }

	event Action DragStarted;

	event Action DragEnded;
}
