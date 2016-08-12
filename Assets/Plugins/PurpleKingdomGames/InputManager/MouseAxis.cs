using System;
using UnityEngine;

[Serializable]
public class MouseAxis : IButton
{
    public enum DirectionType
    {
        X, Y
    }

    [SerializeField]
    public bool Invert { get; set; }

    [SerializeField]
    public string Name { get; protected set; }

    [SerializeField]
    public readonly DirectionType Direction;

    [SerializeField]
    private string _virtualKeyName;

    public MouseAxis(DirectionType direction)
    {
        Direction = direction;
        Name = _virtualKeyName = "Mouse Axis " + Direction.ToString();
    }

    public float GetCurrentValue()
    {
        if (Invert) {
            return -Input.GetAxis(_virtualKeyName);
        } else {
            return Input.GetAxis(_virtualKeyName);
        }
    }

    public float GetCurrentRawValue()
    {
        if (Invert) {
            return -Input.GetAxisRaw(_virtualKeyName);
        } else {
            return Input.GetAxisRaw(_virtualKeyName);
        }
    }

    public bool IsDown()
    {
        return GetCurrentRawValue() == 0;
    }

    public bool IsUp()
    {
        return GetCurrentRawValue() != 0;
    }

    public bool IsHeld()
    {
        return IsDown();
    }
}