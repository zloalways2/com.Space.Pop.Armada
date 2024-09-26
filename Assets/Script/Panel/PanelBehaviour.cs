using UnityEngine;

public class PanelBehaviour : MonoBehaviour
{
	[SerializeField] private GameObject m_Panel;

	[SerializeField] private GameObject[] m_Objects;
	[SerializeField] private MonoBehaviour[] m_Scripts;

	private void Awake()
	{
		//m_Panel = gameObject;
	}

	public void OpenPanel()
	{
		m_Panel.SetActive(true);

		foreach (var obj in m_Objects)
		{
			obj.SetActive(false);
		}

		foreach (var script in m_Scripts)
		{
			script.enabled = false;
		}
	}

	public void ClosePanel()
	{
		foreach (var obj in m_Objects)
		{
			obj.SetActive(true);
		}

		foreach (var script in m_Scripts)
		{
			script.enabled = true;
		}

		m_Panel.SetActive(false);
	}
}
