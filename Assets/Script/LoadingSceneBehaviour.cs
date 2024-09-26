using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneBehaviour : MonoBehaviour
{
	[SerializeField] private GameObject loadingBar;

	[Range(0f, 1f), SerializeField] private float progressBarDebugger;

	private RectTransform loadingBarRectTransform;
	private float loadingBarRectTransformWidth;
	private float loadingBarRectTransformRight;

	// Start is called before the first frame update
	void Start()
    {
		loadingBarRectTransform = loadingBar.GetComponent<RectTransform>();
		loadingBarRectTransformWidth = -loadingBarRectTransform.rect.width;
		loadingBarRectTransformRight = loadingBarRectTransform.sizeDelta.x;

		LoadGameScene();
	}

	private void LoadGameScene()
    {
		StartCoroutine(AsyncLoadSceneCoroutune("MenuScene"));
    }

	IEnumerator AsyncLoadSceneCoroutune(string sceneName)
	{
		var async_operation = SceneManager.LoadSceneAsync(sceneName);

		while (!async_operation.isDone)
		{
			loadingBarRectTransform.sizeDelta = new Vector2(loadingBarRectTransformRight + (1 - async_operation.progress) * loadingBarRectTransformWidth, loadingBarRectTransform.sizeDelta.y);
			yield return null;
		}

		SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
	}
}
