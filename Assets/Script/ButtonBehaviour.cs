using UnityEngine;
using UnityEngine.Events;

public class ButtonBehaviour : MonoBehaviour
{
	[SerializeField] private AudioClip _sound;

	private string _isMusicPlayingKey = "_isMusicPlaying";
	private string _isSoundPlayingKey = "_isSoundPlaying";

	private bool _isMusicPlaying;
	private bool _isSoundPlaying;

	public static UnityEvent onSoundStateChange = new UnityEvent();
	public static UnityEvent onMusicStateChange = new UnityEvent();
	public static UnityEvent onPlayButtonSound = new UnityEvent();

	public static UnityEvent<AudioClip> onPlayAudioClipSound = new UnityEvent<AudioClip>();

	// Start is called before the first frame update
	void Start()
    {
		if (!PlayerPrefs.HasKey(_isSoundPlayingKey))
		{
			PlayerPrefs.SetInt(_isSoundPlayingKey, 1);
		}

		_isMusicPlaying = PlayerPrefs.GetInt(_isMusicPlayingKey) == 1;
		_isSoundPlaying = PlayerPrefs.GetInt(_isSoundPlayingKey) == 1;
	}

	public void ChangeMusicPlayState()
	{
		_isMusicPlaying = !_isMusicPlaying;

		if (_isMusicPlaying)
		{
			PlayerPrefs.SetInt(_isMusicPlayingKey, 1);
		}
		else
		{
			PlayerPrefs.SetInt(_isMusicPlayingKey, 0);
		}

		onMusicStateChange.Invoke();
	}

	public void ChangeSoundPlayState()
	{
		_isSoundPlaying = !_isSoundPlaying;

		if (_isSoundPlaying)
		{
			PlayerPrefs.SetInt(_isSoundPlayingKey, 1);
		}
		else
		{
			PlayerPrefs.SetInt(_isSoundPlayingKey, 0);
		}

		onSoundStateChange.Invoke();
	}

	public void PlayButtonSound()
	{
		if (PlayerPrefs.GetInt(_isSoundPlayingKey) == 1) {
			onPlayButtonSound.Invoke(); 
		}
	}

	public void PlaySound()
	{
		if (PlayerPrefs.GetInt(_isSoundPlayingKey) == 1)
		{
			onPlayAudioClipSound.Invoke(_sound);
		}
	}
}
