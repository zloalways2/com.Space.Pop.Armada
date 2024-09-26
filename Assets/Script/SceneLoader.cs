using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public void LoadScene(int sceneId)
	{
		SceneManager.LoadScene(sceneId);
	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void DelayLoadScene(string sceneName)
	{
		float delay = 0.5f;
		StartCoroutine(LoadSceneCoroutine(sceneName, delay));
	}

	IEnumerator LoadSceneCoroutine(string sceneName, float delay)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(sceneName);
		yield return null;
	}

	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void GameExit()
	{
		Application.Quit();
	}
}
