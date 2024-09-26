using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAliveScript : MonoBehaviour
{
	[SerializeField] private List<string> m_Scenes = new List<string>();
	public static SceneAliveScript instance;
	private string sceneName;

	void Awake()
	{
		sceneName = SceneManager.GetActiveScene().name;

		if (instance == null || !m_Scenes.Contains(instance.sceneName))
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (!m_Scenes.Contains(instance.sceneName))
		{
			Destroy(gameObject);
		}
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
