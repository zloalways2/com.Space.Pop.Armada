using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text LevelText;
    [SerializeField] private TMP_Text LevelShadowText;

	private string _levelNum = "p_ChoosenLevel";
	private string _playerScore = "p_ScoreCount";
	private string _levelMax = "p_MaxLevel";

	private int currentLevel;
    private int score;
    private int maxLevel;

	// Start is called before the first frame update
	void Start()
    {
        currentLevel = PlayerPrefs.GetInt(_levelNum, 1);
		score = PlayerPrefs.GetInt(_playerScore, 0);
		maxLevel = PlayerPrefs.GetInt(_levelMax, 0);

		ScoreText.text = score.ToString();
        LevelText.text = "LEVEL " + currentLevel.ToString() + "\nCOMPLETED";
		LevelShadowText.text = "LEVEL " + currentLevel.ToString() + "\nCOMPLETED";
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        if(currentLevel < maxLevel)
            PlayerPrefs.SetInt(_levelNum, currentLevel + 1);
        SceneManager.LoadScene("GameScene");
	}
}
