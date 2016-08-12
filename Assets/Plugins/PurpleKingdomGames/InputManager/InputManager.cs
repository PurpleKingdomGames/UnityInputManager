using System;
using UnityEngine;

namespace PurpleKingdomGames.Unity.InputManager
{
    public static class InputManager
    {
        private static string[] _keys = new string[500];
        private static IButton[] _buttons = new IButton[_keys.Length];

        public static void Clear()
        {
            for (int i = 0; i < _keys.Length; i++) {
                if (_keys[i] == null) {
                    break;
                }

                _keys[i] = null;
                _buttons[i] = null;
            }
        }

        public static void SetMap(string key, IButton button)
        {
            int i = getIndex(key);
            if (i < 0) {
                for (i = 0; i < _keys.Length; i++) {
                    if (_keys[i] == null) {
                        break;
                    }
                }

                if (i >= _keys.Length) {
                    throw new Exception("Exceeded maximum limit of " + _keys.Length + " mappings");
                }
            }

            _keys[i] = key;
            _buttons[i] = button;
        }

        public static bool ContainsMap(string key)
        {
            return getIndex(key) != -1;
        }

        public static void RemoveMap(string key)
        {
            int i = getIndex(key);
            if (i < 0) {
                throw new Exception("No mapping exists for key " + key);
            }

            // Remove the entry for this key from the arrays
            _keys[i] = null;
            _buttons[i] = null;

            // Move any items up, to fill the gap
            for (; i < _keys.Length; i++) {
                if (_keys[i] == null && i < _keys.Length - 2) {
                    int nextIndex = i + 1;
                    if (_keys[nextIndex] == null) {
                        break;
                    }

                    _keys[i] = _keys[nextIndex];
                    _buttons[i] = _buttons[nextIndex];

                    _keys[nextIndex] = null;
                    _buttons[nextIndex] = null;
                }
            }
        }

        public static IButton GetButton(string key)
        {
            int i = getIndex(key);
            if (i < 0) {
                throw new UnassignedReferenceException("No mapping exists for key " + key);
            }

            return _buttons[i];
        }

        public static float GetCurrentValue(string key)
        {
            int i = getIndex(key);
            if (i < 0) {
                throw new UnassignedReferenceException("No mapping exists for key " + key);
            }

            return _buttons[i].GetCurrentValue();
        }

        public static float GetCurrentRawValue(string key)
        {
            int i = getIndex(key);
            if (i < 0) {
                throw new UnassignedReferenceException("No mapping exists for key " + key);
            }

            return _buttons[i].GetCurrentRawValue();
        }

        public static bool IsDown(string key)
        {
            int i = getIndex(key);
            if (i < 0) {
                throw new UnassignedReferenceException("No mapping exists for key " + key);
            }

            return _buttons[i].IsDown();
        }

        public static bool IsUp(string key)
        {
            int i = getIndex(key);
            if (i < 0) {
                throw new UnassignedReferenceException("No mapping exists for key " + key);
            }

            return _buttons[i].IsUp();
        }

        public static bool IsHeld(string key)
        {
            int i = getIndex(key);
            if (i < 0) {
                throw new UnassignedReferenceException("No mapping exists for key " + key);
            }

            return _buttons[i].IsHeld();
        }

        private static int getIndex(string key)
        {
            for (int i = 0; i < _keys.Length; i++) {
                if (_keys[i] == null) {
                    break;
                }

                if (key == _keys[i]) {
                    return i;
                }
            }

            return -1;
        }
    }
}