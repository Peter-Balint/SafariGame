using UnityEngine;

public class InfoPanelToggle : MonoBehaviour
{
	public GameObject infoPanel;

	public void ToggleInfoPanel()
	{
		if (infoPanel != null)
			infoPanel.SetActive(!infoPanel.activeSelf);
	}
}
