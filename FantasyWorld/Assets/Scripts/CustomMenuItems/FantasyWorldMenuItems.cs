#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomMenuItems
{
    public class FantasyWorldMenuItems : MonoBehaviour
    {
        [MenuItem("Fantasy World/Run", priority = 1)]
        private static void Run()
        {
            GoToInitializationScene();
            EditorApplication.EnterPlaymode();
        }

        [MenuItem("Fantasy World/Run", isValidateFunction: true, priority = 1)]
        private static bool RunValidation()
        {
            return !Application.isPlaying;
        }

        [MenuItem("Fantasy World/Go to Initialization Scene", priority = 12)]
        private static void GoToInitializationScene()
        {
            var currentScene = SceneManager.GetActiveScene();
            EditorSceneManager.SaveScene(currentScene);
            EditorSceneManager.OpenScene(FantasyWorldMenuItemsConst.InitializationSceneNamePath);
        }

        [MenuItem("Fantasy World/Go to Initialization Scene", isValidateFunction: true, priority = 12)]
        private static bool GoToInitializationSceneValidation()
        {
            if (Application.isPlaying)
            {
                return false;
            }

            var currentScene = SceneManager.GetActiveScene();
            return currentScene.name != FantasyWorldMenuItemsConst.InitializationSceneName;
        }
        
        [MenuItem("Fantasy World/Go to Home Scene", priority = 13)]
        private static void GoToHomeScene()
        {
            var currentScene = SceneManager.GetActiveScene();
            EditorSceneManager.SaveScene(currentScene);
            EditorSceneManager.OpenScene(FantasyWorldMenuItemsConst.HomeSceneNamePath);
        }

        [MenuItem("Fantasy World/Go to Home Scene", isValidateFunction: true, priority = 13)]
        private static bool GoToHomeSceneValidation()
        {
            if (Application.isPlaying)
            {
                return false;
            }

            var currentScene = SceneManager.GetActiveScene();
            return currentScene.name != FantasyWorldMenuItemsConst.HomeSceneName;
        }

        [MenuItem("Fantasy World/Go to Game Scene", priority = 14)]
        private static void GoToGame()
        {
            var currentScene = SceneManager.GetActiveScene();
            EditorSceneManager.SaveScene(currentScene);
            EditorSceneManager.OpenScene(FantasyWorldMenuItemsConst.GameSceneNamePath);
        }

        [MenuItem("Fantasy World/Go to Game Scene", isValidateFunction: true, priority = 14)]
        private static bool GoToGameValidation()
        {
            if (Application.isPlaying)
            {
                return false;
            }

            var currentScene = SceneManager.GetActiveScene();
            return currentScene.name != FantasyWorldMenuItemsConst.GameSceneName;
        }
    }
}
#endif