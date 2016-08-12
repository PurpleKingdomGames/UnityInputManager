using System;
using UnityEngine;

namespace PurpleKingdomGames.Unity.InputManager
{
    [Serializable]
    public class MouseButton : IButton
    {
        [SerializeField]
        public bool Invert { get; set; }

        [SerializeField]
        public string Name { get; protected set; }

        [SerializeField]
        public readonly int ButtonNumber;

        public MouseButton(int buttonNumber)
        {
            ButtonNumber = buttonNumber;
            Name = "Mouse Button " + buttonNumber;
        }

        public float GetCurrentValue()
        {
            return IsHeld() ? Invert ? -1 : 1 : 0;
        }

        public float GetCurrentRawValue()
        {
            return GetCurrentValue();
        }

        public bool IsDown()
        {
            return Input.GetMouseButtonDown(ButtonNumber);
        }

        public bool IsUp()
        {
            return Input.GetMouseButtonUp(ButtonNumber);
        }

        public bool IsHeld()
        {
            return Input.GetMouseButton(ButtonNumber);
        }
    }
}