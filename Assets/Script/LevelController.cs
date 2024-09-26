using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int levelNum;
    [SerializeField] private GameObject levelPrefub;

	private ScrollRect scrollRect; // —сылка на ScrollRect
	private GameObject content; // —сылка на Content (который имеет Grid Layout Group)
	private int maxLevel = 0;
	private int maxAvailableLevel = 0;

	private string _levelMaxAvailable = "p_MaxAvailableLevel";
	private string _levelMax = "p_MaxLevel";

	private List<GameObject> levels = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
		scrollRect = transform.parent.parent.GetComponent<ScrollRect>();
		content = gameObject;

		maxLevel = 0;

		PlayerPrefs.SetInt(_levelMax, levelNum);
		maxAvailableLevel = PlayerPrefs.GetInt(_levelMaxAvailable, 1);

		for (int i = 0; i < levelNum; i++)
        {
            AddLevel();
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddLevel()
    {
        maxLevel++;
        var level = Instantiate(levelPrefub, transform);
        level.GetComponent<LevelBehaviour>().LevelNum = maxLevel;
		levels.Add(level);

		if (maxLevel > maxAvailableLevel)
			level.GetComponent<Button>().interactable = false;

		LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());

		scrollRect.verticalNormalizedPosition = 1;
	}
}
