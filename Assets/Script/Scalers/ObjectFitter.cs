using UnityEngine;

public class ObjectFitter : MonoBehaviour
{
	private float width;
	private float height;

	private float refRatio = 0.5f;

	[SerializeField] private bool WideScale;

	// Start is called before the first frame update
	void Start()
	{
		width = transform.localScale.x;
		height = transform.localScale.y;

		refRatio = width / height;
		if (WideScale)
		{
			ScaleToFitWide();
		}
		else 
		{ 
			ScaleToFit();
		}
	}

#if UNITY_EDITOR
	// Update is called once per frame
	void Update()
	{
		if (WideScale)
		{
			ScaleToFitWide();
		}
		else
		{
			ScaleToFit();
		}
	}
#endif

	private void ScaleToFit()
	{
		float worldScreenHeight = Camera.main.orthographicSize * 2;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		float newHeight = worldScreenWidth / width * height;
		float newWidth = worldScreenHeight / height * width;

		if (newWidth / refRatio < newHeight)
		{
			transform.localScale = new Vector3(
			newWidth,
			worldScreenHeight,
			1
			);
		}
		else
		{
			transform.localScale = new Vector3(
			worldScreenWidth,
			newHeight,
			1
			);
		}
	}

	private void ScaleToFitWide()
	{
		float worldScreenHeight = Camera.main.orthographicSize * 2;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		transform.localScale = new Vector3(
		worldScreenWidth,
		transform.localScale.y,
		transform.localScale.z
		);
		
	}
}
