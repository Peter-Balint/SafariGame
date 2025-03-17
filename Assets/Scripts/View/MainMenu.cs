using Safari.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button[] difficultyButtons;

    private GameDifficulty gameDifficulty;

    public void OnEnable()
    {
        difficultyButtons[0].Select();
        gameDifficulty = GameDifficulty.Easy;

        difficultyButtons[0].onClick.AddListener(() =>
        {
            gameDifficulty = GameDifficulty.Easy;
            Debug.Log("diff: " + gameDifficulty.ToString());
        });
        difficultyButtons[1].onClick.AddListener(() =>
        {
            gameDifficulty = GameDifficulty.Medium;
            Debug.Log("diff: " + gameDifficulty.ToString());
        });
        difficultyButtons[2].onClick.AddListener(() =>
        {
            gameDifficulty = GameDifficulty.Hard;
            Debug.Log("diff: " +  gameDifficulty.ToString());
        });
    }

    public void PlayGame()
    {
        SafariGame.StartGame(gameDifficulty);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
