using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardBehaviour : MonoBehaviour
{
	[SerializeField] private int rows = 10;
	[SerializeField] private int columns = 8;

	[SerializeField] private List<GameObject> objects = new List<GameObject>();
	
    private List<GameObject> spawnedBalls = new List<GameObject>();
    private List<GameObject> markedBalls = new List<GameObject>();
    private GameObject[][] ballsOnBoard;
	private Grid grid;

	private Queue<GameObject> queue = new Queue<GameObject>();

	private string _levelMax = "p_MaxLevel";
	private int maxLevelNum;

	Vector2Int[] CheckDir = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

	public static UnityEvent onStep = new UnityEvent();

	// Start is called before the first frame update
	void Awake()
    {
		onStep.AddListener(CheckStepsExist);
		//BallBehaviour.onBallDeath.AddListener(RemoveBallFromBoard);
		BallBehaviour.onBallClicked.AddListener(CheckNearBalls);
		GameControllerBehaviour.onLevelLoadEvent.AddListener(SpawnObjects);

		maxLevelNum = PlayerPrefs.GetInt(_levelMax, 1);

		grid = GetComponent<Grid>();

		//SpawnObjects();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObjects(int level)
    {
		spawnedBalls.Clear();

        ballsOnBoard = new GameObject[rows][];

		for (int i = 0; i < rows; i++)
        {
            ballsOnBoard[i] = new GameObject[columns];

			for (int j = 0; j < columns; j++)
            {
                var obj = Instantiate(objects[Random.Range(0, Mathf.FloorToInt(3 + (objects.Count - 3) * level / maxLevelNum))], transform);

				Vector3Int gridPosition = new Vector3Int(j, i, 0);

				Vector3 worldPosition = grid.GetCellCenterWorld(gridPosition);

				obj.transform.position = worldPosition;

                obj.GetComponent<BallBehaviour>().gridPos = new Vector2Int(i, j);

				spawnedBalls.Add(obj);

                ballsOnBoard[i][j] = obj;
			}
		}
    }

    public GameObject TryGetBallAtPos(Vector2Int pos)
    {
        if (pos.x >= 0 && pos.x < rows &&
			pos.y >= 0 && pos.y < columns)
        {
            return ballsOnBoard[pos.x][pos.y];
        }

        return null;
	}

	public Vector3 GetCellWorldPos(Vector3Int pos)
	{
		return grid.GetCellCenterWorld(pos);
	}

	private void RemoveBallFromBoard(BallBehaviour ball)
	{
		ballsOnBoard[ball.gridPos.x][ball.gridPos.y] = null;
	}

	private void RearrangeBalls()
	{
		

		// downward move
		for (int j = 0; j < columns; j++)
		{
			int nullElemPtr = -1;
			for (int i = 0; i < rows; i++)
			{
				if (ballsOnBoard[i][j] == null && nullElemPtr == -1)
				{
					nullElemPtr = i;
					//MoveColumn(new Vector2Int(i, j));
				}
				if (ballsOnBoard[i][j] != null && nullElemPtr != -1)
				{
					ballsOnBoard[i][j].GetComponent<BallBehaviour>().MoveBall(new Vector2Int(j, nullElemPtr));
					ballsOnBoard[nullElemPtr][j] = ballsOnBoard[i][j];
					ballsOnBoard[i][j] = null;
					nullElemPtr++;
				}
			}
		}

		// side move
		int sideNullElemPtr = -1;
		for(int j = columns - 1; j >= 0; j--)
		{
			if (ballsOnBoard[0][j] == null && sideNullElemPtr == -1)
			{
				sideNullElemPtr = j;
			}

			if(ballsOnBoard[0][j] != null && sideNullElemPtr != -1)
			{
				for(int i = 0; i < rows; i++)
				{
					if (ballsOnBoard[i][j] == null)
						continue;

					ballsOnBoard[i][j].GetComponent<BallBehaviour>().MoveBall(new Vector2Int(sideNullElemPtr, i));
					ballsOnBoard[i][sideNullElemPtr] = ballsOnBoard[i][j];
					ballsOnBoard[i][j] = null;
				}
				sideNullElemPtr--;
			}
		}

	}

	private void MoveColumn(Vector2Int pos)
	{
		var start = pos.x;
		var current = pos.x;

		while(ballsOnBoard[current][pos.y] == null)
		{
			current++;
		}

		for (int i = 0; i < rows - current; ++i)
		{
			if (ballsOnBoard[i + current][pos.y] != null)
				ballsOnBoard[i + current][pos.y].GetComponent<BallBehaviour>().MoveBall(new Vector2Int(pos.y, i + start));
		}
	}

	private void CheckStepsExist()
	{
		bool isStepExist = false;

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				if(CheckNearBallForIdentical(new Vector2Int(i, j)))
				{
					isStepExist = true; break;
				}
			}
		}

		if (!isStepExist)
		{
			GameControllerBehaviour.onGameOverEvent.Invoke();
		}
	}

	public void CheckNearBalls(BallBehaviour ball)
	{
		if (!CheckNearBallForIdentical(ball.gridPos))
		{
			return;
		}

		queue.Clear();
		queue.Enqueue(ball.gameObject);

		ball.isBallMarked = true;
		markedBalls.Add(ball.gameObject);

		while (queue.Count > 0)
		{
			var ball1 = queue.Dequeue();
			var ball1Script = ball1.GetComponent<BallBehaviour>();

			foreach (var dir in CheckDir)
			{
				var otherBall = TryGetBallAtPos(ball1Script.gridPos + dir);

				if (otherBall != null)
				{
					if (IsBallIdentical(ball1, otherBall))
					{
						var otherBallScript = otherBall.GetComponent<BallBehaviour>();

						if (!otherBallScript.isBallMarked)
						{
							queue.Enqueue(otherBall);
							otherBallScript.isBallMarked = true;
							markedBalls.Add(otherBall);
						}
					}
				}
			}
		}

		GameControllerBehaviour.onExactScoreUpEvent.Invoke(markedBalls.Count);

		RemoveMarkedBalls();
	}

	private void RemoveMarkedBalls()
	{
		foreach (var ball in markedBalls)
		{
			ball.GetComponent<BallBehaviour>().Kill();
			RemoveBallFromBoard(ball.GetComponent<BallBehaviour>());
		}

		markedBalls.Clear();
		RearrangeBalls();
	}

	private bool CheckNearBallForIdentical(Vector2Int gridPos)
	{
		bool isIdenticalBallNear = false;

		var ball1 = TryGetBallAtPos(gridPos);

		if (ball1 == null)
			return false;

		foreach (var dir in CheckDir)
		{
			var ball2 = TryGetBallAtPos(gridPos + dir);

			if (ball2 != null)
			{
				if (IsBallIdentical(ball1, ball2))
				{
					isIdenticalBallNear = true;
					break;
				}
			}
		}

		return isIdenticalBallNear;
	}

	private bool IsBallIdentical(GameObject ball1, GameObject ball2)
	{
		return ball1.gameObject.GetComponent<SpriteRenderer>().sprite.Equals(ball2.gameObject.GetComponent<SpriteRenderer>().sprite);
	}
}
