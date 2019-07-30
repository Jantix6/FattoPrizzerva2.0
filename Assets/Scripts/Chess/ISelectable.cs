using System;

public interface ISelectable<T>
{
    T SelectedObject { get; set; }

    void Select(T selectedObject);
    void Deselect();
}
