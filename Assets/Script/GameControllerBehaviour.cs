using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameControllerBehaviour : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;
    [Space]
    [SerializeField] private float timer;

    private bool isGame = true;
    private int currentLevel = 1;
    private int maxAvailableLevel = 1;
    private int maxLevel = 1;

	private string _levelNum = "p_ChoosenLevel";
	private string _levelMax = "p_MaxLevel";
	private string _levelMaxAvailable = "p_MaxAvailableLevel";
	private string _playerScore = "p_ScoreCount";

	public static UnityEvent onScoreUpEvent = new UnityEvent();
    public static UnityEvent onGameOverEvent = new UnityEvent();

    public static UnityEvent<int> onExactScoreUpEvent = new UnityEvent<int>();
    public static UnityEvent<int> onLevelLoadEvent = new UnityEvent<int>();

    // Start is called before the first frame update
    void Start()
    {
        onGameOverEvent.AddListener(GameOver);
        onScoreUpEvent.AddListener(ScoreUp);
		onExactScoreUpEvent.AddListener(ExactScoreUpEvent);

		currentLevel = PlayerPrefs.GetInt(_levelNum, 1);
		maxLevel = PlayerPrefs.GetInt(_levelMax, 1);
		maxAvailableLevel = PlayerPrefs.GetInt(_levelMaxAvailable, 1);

        onLevelLoadEvent.Invoke(currentLevel);
        timer = 20 * maxLevel / currentLevel;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        TimerUpdate();
	}

    void TimerUpdate()
    {
        if (!isGame)
            return;

        if(timer > 0)
            timer -= Time.deltaTime;

        if(timer <= 0)
			onGameOverEvent.Invoke();

        TimerTextUpdate();
	}

    void TimerTextUpdate()
    {
        int hours = (int)(timer / 3600f);
		int minutes = (int)((timer - hours * 3600f) / 60f);
		int seconds = (int)(timer - minutes * 60f - hours * 3600f);

        if (hours <= 0)
        {
            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        else
        {
			timerText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
		}
	}

    void ScoreUp()
    {
        score += 10;

        UpdateScoreText();
	}

    void ExactScoreUpEvent(int scoreCount)
    {
        score += scoreCount * 10 * (scoreCount - 1);

		UpdateScoreText();
	}

	void UpdateScoreText()
    {
		scoreText.text = score.ToString();
	}

    void GameWin()
    {
        if(maxAvailableLevel == currentLevel)
        {
			PlayerPrefs.SetInt(_levelMaxAvailable, maxAvailableLevel + 1);
		}

		PlayerPrefs.SetInt(_playerScore, score);

		SceneManager.LoadScene("WinScene");
	}

	void GameLose()
    {
        SceneManager.LoadScene("LoseScene");
	}

	void GameOver()
    {
        if(timer > 0)
        {
            GameWin();
		}
        else
        {
            GameLose();
        }

		Debug.Log("GameOver");
        isGame = false;
	}
}
