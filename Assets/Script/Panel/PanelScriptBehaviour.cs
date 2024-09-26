using UnityEngine;

public class PanelScriptBehaviour : MonoBehaviour
{
	[SerializeField] private GameObject[] OnOpenPanelEnable;
	[SerializeField] private GameObject[] OnOpenPanelDisable;

	public void OpenPanel()
	{
		foreach (var obj in OnOpenPanelEnable)
		{
			obj.SetActive(true);
		}

		foreach (var obj in OnOpenPanelDisable)
		{
			obj.SetActive(false);
		}
	}

	public void ClosePanel()
	{
		foreach (var obj in OnOpenPanelDisable)
		{
			obj.SetActive(true);
		}


		foreach (var obj in OnOpenPanelEnable)
		{
			obj.SetActive(false);
		}
	}
}
