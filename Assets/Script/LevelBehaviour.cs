using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelBehaviour : MonoBehaviour
{
    private int levelNum;
    private TMP_Text levelNumText;
    private Button button;

    private string _levelNum = "p_ChoosenLevel";

    public int LevelNum { 
        get 
        { 
            return levelNum; 
        } 
        set
		{
			levelNum = value;
			levelNumText.text = levelNum.ToString();
		} 
    }

    // Start is called before the first frame update
    void Awake()
    {
		levelNumText = transform.GetChild(0).GetComponent<TMP_Text>();
		button = GetComponent<Button>();
        button.onClick.AddListener(ChooseLevel);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChooseLevel()
    {
        PlayerPrefs.SetInt(_levelNum, levelNum);
        SceneManager.LoadScene("GameScene");
        Debug.Log(levelNumText.text);
    }
}
