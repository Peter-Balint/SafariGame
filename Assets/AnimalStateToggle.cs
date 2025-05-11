using UnityEngine;

public class AnimalStateToggle : MonoBehaviour
{
	public GameObject infoPanel;

	public void ToggleInfoPanel()
	{
		if (infoPanel != null)
			infoPanel.SetActive(!infoPanel.activeSelf);
		Debug.Log("button pressed");
	}

}
