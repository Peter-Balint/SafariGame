using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Only needed if you want to reload scenes, optional

public class ExitScreenManager : MonoBehaviour
{
	public GameObject exitScreen; // Reference to the panel

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			// Toggle the exit screen
			exitScreen.SetActive(!exitScreen.activeSelf);

			
			
		}
	}

	// Called by "Yes" Button
	public void QuitGame()
	{
		Application.Quit();
		// In the editor, Application.Quit() won't do anything
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}

	// Called by "No" Button
	public void CancelExit()
	{
		exitScreen.SetActive(false);
		
	}
}
