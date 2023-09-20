using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Aid
{
	public class SceneLoader : MonoBehaviour
	{
		public event Action<Scene> Loaded;

		public static bool IsSceneLoaded(string sceneName, out int sceneIndex)
		{
			sceneIndex = -1;
			var scenesCount = SceneManager.sceneCount;

			for (int i = 0; i < scenesCount; i++)
				if (SceneManager.GetSceneAt(i).name == sceneName)
				{
					sceneIndex = i;
					return true;
				}

			return false;
		}

		public static Scene GetLoadedScene(int sceneIndex)
		{
			return SceneManager.GetSceneAt(sceneIndex);
		}

		public void Load(string sceneName)
		{
			enabled = true;
			var asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
			asyncOperation.completed += operation => OnLoaded(sceneName);
		}

		private void OnLoaded(string sceneName)
		{

			Loaded?.Invoke(SceneManager.GetSceneByName(sceneName));
		}

		public void Unload(string sceneName)
		{
			SceneManager.UnloadSceneAsync(sceneName);
		}
	}
}