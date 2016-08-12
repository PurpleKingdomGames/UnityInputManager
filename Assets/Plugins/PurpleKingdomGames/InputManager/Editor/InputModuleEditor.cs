using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PurpleKingdomGames.Unity.InputManager
{
    public static class InputModuleEditor
    {
        [MenuItem("GameObject/UI/Purple Kingdom Event System")]
        private static void CreateInputModule()
        {
            if (Object.FindObjectOfType<EventSystem>() == null) {
                GameObject obj = new GameObject();
                obj.name = "EventSystem";
                obj.AddComponent<EventSystem>();
                obj.AddComponent<InputModule>();
            }
        }
    }
}