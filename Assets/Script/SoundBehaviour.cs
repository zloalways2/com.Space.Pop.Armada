using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundBehaviour : MonoBehaviour
{
	[SerializeField] private AudioSource _music;
	[SerializeField] private AudioSource _btnSound;
	[SerializeField] private Slider _musicSlider;
	[SerializeField] private Slider _soundSlider;
	private AudioSource _soundSource;

	private bool _isMusicPlaying;
	private bool _isSoundPlaying;

	private string _musicVolumeKey = "MusicVolumeKey";
	private string _soundVolumeKey = "SoundVolumeKey";

	// Start is called before the first frame update
	void Start()
	{
		_soundSource = gameObject.AddComponent<AudioSource>();

		ButtonBehaviour.onPlayAudioClipSound.AddListener(PlaySound);


		if (!PlayerPrefs.HasKey(_musicVolumeKey))
		{
			PlayerPrefs.SetFloat(_musicVolumeKey, 0.2f);
		}

		if (!PlayerPrefs.HasKey(_soundVolumeKey))
		{
			PlayerPrefs.SetFloat(_soundVolumeKey, 0.2f);
		}

		if (_musicSlider != null)
		{
			_musicSlider.onValueChanged.AddListener(delegate { MusicChangeVolume(); });

			_musicSlider.value = PlayerPrefs.GetFloat(_musicVolumeKey);
		}

		if (_soundSlider != null)
		{
			_soundSlider.onValueChanged.AddListener(delegate { SoundChangeVolume(); });

			_soundSlider.value = PlayerPrefs.GetFloat(_soundVolumeKey);
		}

		_music.volume = PlayerPrefs.GetFloat(_musicVolumeKey);
		_soundSource.volume = PlayerPrefs.GetFloat(_soundVolumeKey);

		if (_music != null && _isMusicPlaying)
		{
			_music.Play();
		}
	}

	void OnSceneLoad(Scene scene, LoadSceneMode mode)
	{
		if (_musicSlider == null)
		{
			var Sliders = Resources.FindObjectsOfTypeAll(typeof(Slider)) as Slider[];
			if(Sliders.Length > 0)
			{
				_musicSlider = Sliders.Where(x => x.name == "MusicSlider").First();
			}
		}

		if (_soundSlider == null)
		{
			var Sliders = Resources.FindObjectsOfTypeAll(typeof(Slider)) as Slider[];
			if (Sliders.Length > 0)
			{
				_soundSlider = Sliders.Where(x => x.name == "SoundSlider").First();
			}
		}

		if (_musicSlider != null)
		{
			_musicSlider.onValueChanged.AddListener(delegate { MusicChangeVolume(); });
			_musicSlider.value = PlayerPrefs.GetFloat(_musicVolumeKey);
		}
		if (_soundSlider != null)
		{
			_soundSlider.onValueChanged.AddListener(delegate { SoundChangeVolume(); });
			_soundSlider.value = PlayerPrefs.GetFloat(_soundVolumeKey);
		}
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoad;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoad;
	}

	void MusicChangeVolume()
	{
		PlayerPrefs.SetFloat(_musicVolumeKey, _musicSlider.value);
		_music.volume = PlayerPrefs.GetFloat(_musicVolumeKey);
		if (_musicSlider != null)
		{
			_musicSlider.value = PlayerPrefs.GetFloat(_musicVolumeKey);
		}
	}

	void SoundChangeVolume()
	{
		PlayerPrefs.SetFloat(_soundVolumeKey, _soundSlider.value);
		if (_soundSlider != null)
		{
			_soundSlider.value = PlayerPrefs.GetFloat(_soundVolumeKey);
			_soundSource.volume = PlayerPrefs.GetFloat(_soundVolumeKey);
		}
	}

	public void PlayButtonSound()
	{
		_btnSound.volume = PlayerPrefs.GetFloat(_musicVolumeKey);
		_btnSound.Play();
	}

	public void PlaySound(AudioClip sound)
	{
		_soundSource.clip = sound;
		_soundSource.Play();
	}
}
