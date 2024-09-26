using UnityEngine;

public class FitToScreen : MonoBehaviour
{
	private SpriteRenderer sr;

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		ScaleObjectOnScreenResize();
	}

	private void ScaleObjectOnScreenResize()
	{
		float worldScreenHeight = Camera.main.orthographicSize * 2;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		transform.localScale = new Vector3(
			worldScreenWidth / sr.sprite.bounds.size.x,
			worldScreenHeight / sr.sprite.bounds.size.y,
			1
			);
	}

	void Update()
	{
#if UNITY_EDITOR
		ScaleObjectOnScreenResize();
#endif
	}
}
