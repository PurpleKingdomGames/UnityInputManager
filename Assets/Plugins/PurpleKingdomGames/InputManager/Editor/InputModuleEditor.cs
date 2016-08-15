using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;

namespace PurpleKingdomGames.Unity.InputManager
{
    public static class InputModuleEditor
    {
		private static string _inputPath = Application.dataPath + "/../ProjectSettings";
		private static string _inputFile = "InputManager.asset";

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

		[MenuItem("Tools/Purple Kingdom Games/Create Input File", false, 0)]
		private static void CreateInputConfig()
		{
			if (EditorUtility.DisplayDialog ("Replace Input Config?", "This action will overwrite your Input Config. Are you sure?", "Yes", "No")) {
				string fromPath = Application.dataPath + "/Plugins/PurpleKingdomGames/InputManager/Editor";
				string fromFilename = "InputManager.asset.config";

				EditorApplication.LockReloadAssemblies ();
				File.Copy (_inputPath + "/" + _inputFile, _inputPath + "/" + _inputFile + ".pkbkup", true);
				File.Copy (fromPath + "/" + fromFilename, _inputPath + "/" + _inputFile, true);
				EditorApplication.UnlockReloadAssemblies ();
			}
		}

		[MenuItem("Tools/Purple Kingdom Games/Restore old Input File", false, 0)]
		private static void RestoreInputConfig()
		{
			if (EditorUtility.DisplayDialog ("Restore backup?", "This action restore your config to it's. Are you sure?", "Yes", "No")) {
				EditorApplication.LockReloadAssemblies ();
				File.Copy (_inputPath + "/" + _inputFile, _inputPath + "/" + _inputFile, true);
				File.Delete (_inputPath + "/" + _inputFile);
				EditorApplication.UnlockReloadAssemblies ();
			}
		}

		[MenuItem("Tools/Purple Kingdom Games/Restore old Input File", true)]
		private static bool ValidateRestoreConfig()
		{
			return File.Exists(_inputPath + "/" + _inputFile + ".pkbkup");
		}

		[MenuItem("Tools/Purple Kingdom Games/Create Input File", true)]
		private static bool ValidateCreateConfig()
		{
			return !ValidateRestoreConfig ();
		}
    }
}