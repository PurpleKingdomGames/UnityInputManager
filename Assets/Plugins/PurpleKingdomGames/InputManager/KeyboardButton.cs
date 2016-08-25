using System;
using UnityEngine;

namespace PurpleKingdomGames.Unity.InputManager
{
    [Serializable]
    public class KeyboardButton : IButton
    {
        [SerializeField]
        public bool Invert { get; set; }

        [SerializeField]
        public string Name { get; protected set; }

        [SerializeField]
        public readonly KeyCode Key;

        public KeyboardButton(KeyCode key)
        {
			Key = key;
			Name = "";

			foreach (char letter in key.ToString()) {
				if (Char.IsUpper (letter) && Name.Length > 0) {
					Name += " " + letter;
				} else {
					Name += letter;
				}
			}
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
            return Input.GetKeyDown(Key);
        }

        public bool IsUp()
        {
            return Input.GetKeyUp(Key);
        }

        public bool IsHeld()
        {
            return Input.GetKey(Key);
        }
    }
}