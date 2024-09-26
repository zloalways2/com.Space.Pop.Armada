using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BallBehaviour : MonoBehaviour
{
	[SerializeField] private AudioClip _sound;
	private string _isSoundPlayingKey = "_isSoundPlaying";

	Vector2Int[] CheckDir = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

    public Vector2Int gridPos;
    public bool isBallMarked = false;

	private bool isCoroutineWork = false;

    private BoardBehaviour board;
    private SpriteRenderer _sr;

    public static UnityEvent<BallBehaviour> onBallDeath = new UnityEvent<BallBehaviour>();
    public static UnityEvent<BallBehaviour> onBallClicked = new UnityEvent<BallBehaviour>();

	// Start is called before the first frame update
	void Start()
    {
		board = transform.parent.GetComponent<BoardBehaviour>();
		_sr = GetComponent<SpriteRenderer>();

		onBallDeath.AddListener(PlaySound);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnMouseDown()
	{
        onBallClicked.Invoke(this);
		BoardBehaviour.onStep.Invoke();
	}

    public void MoveBall(Vector2Int pos)
    {
        //transform.position = board.GetCellWorldPos(new Vector3Int(pos.x, pos.y, 0));
        StartCoroutine(MoveCoroutine(board.GetCellWorldPos(new Vector3Int(pos.x, pos.y, 0))));
		gridPos = new Vector2Int(pos.y, pos.x);
	}

    IEnumerator MoveCoroutine(Vector3 pos)
    {   
        while(isCoroutineWork)
            yield return null;

        isCoroutineWork = true;

        var stepNum = (int)(Vector3.Distance(pos, transform.position) * 5f);
		var step = (pos - transform.position) / stepNum;
        for(int i = 0; i < stepNum; i++)
        {
            transform.position += step;
            yield return null;
		}
        isCoroutineWork = false;
	}

	public void Kill()
    {
        onBallDeath.Invoke(this);
		gameObject.SetActive(false);
	}

	public void PlaySound(BallBehaviour ball)
	{
		if (PlayerPrefs.GetInt(_isSoundPlayingKey) == 1)
		{
			ButtonBehaviour.onPlayAudioClipSound.Invoke(_sound);
		}
	}
}
