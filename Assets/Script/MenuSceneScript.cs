using UnityEngine;

public class MenuSceneScript : MonoBehaviour
{

	private string _levelNum = "p_ChoosenLevel";
	private string _levelMaxAvailable = "p_MaxAvailableLevel";
	private string _levelMax = "p_MaxLevel";
	private string _playerScore = "p_ScoreCount";

	// Start is called before the first frame update
	void Start()
    {
        if (!PlayerPrefs.HasKey(_levelNum))
        {
			PlayerPrefs.SetInt(_levelNum, 0);
		}

		if (!PlayerPrefs.HasKey(_levelMaxAvailable))
		{
			PlayerPrefs.SetInt(_levelMaxAvailable, 1);
		}

		if (!PlayerPrefs.HasKey(_levelMax))
		{
			PlayerPrefs.SetInt(_levelMax, 1);
		}

		if (!PlayerPrefs.HasKey(_playerScore))
		{
			PlayerPrefs.SetInt(_playerScore, 0);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
