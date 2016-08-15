using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;

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

		[MenuItem("Edit/Create Purple Kingdom Input File", false, 0)]
		private static void CreateInputConfig()
		{
			if (EditorUtility.DisplayDialog ("Replace Input Config?", "This action will overwrite your Input Config. Are you sure?", "Yes", "No")) {
				string fromPath = Application.dataPath + "/Plugins/PurpleKingdomGames/InputManager/Editor";
				string fromFilename = "InputManager.asset.config";
				string toPath = Application.dataPath + "/../ProjectSettings";
				string toFilename = "InputManager.asset";

				EditorApplication.LockReloadAssemblies ();
				File.Copy (toPath + "/" + toFilename, toPath + "/" + toFilename + ".pkbkup", true);
				File.Copy (fromPath + "/" + fromFilename, toPath + "/" + toFilename, true);
				EditorApplication.UnlockReloadAssemblies ();
			}
		}

		[MenuItem("Edit/Restore old Input File", false, 0)]
		private static void CreateInputConfig()
		{
			if (EditorUtility.DisplayDialog ("Replace Input Config?", "This action will overwrite your Input Config. Are you sure?", "Yes", "No")) {
				string fromPath = Application.dataPath + "/Plugins/PurpleKingdomGames/InputManager/Editor";
				string fromFilename = "InputManager.asset.config";
				string toPath = Application.dataPath + "/../ProjectSettings";
				string toFilename = "InputManager.asset";

				EditorApplication.LockReloadAssemblies ();
				File.Copy (toPath + "/" + toFilename, toPath + "/" + toFilename + ".pkbkup", true);
				File.Copy (fromPath + "/" + fromFilename, toPath + "/" + toFilename, true);
				EditorApplication.UnlockReloadAssemblies ();
			}
		}
    }
}