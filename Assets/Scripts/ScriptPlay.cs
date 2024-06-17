using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptPllay : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Игра началась");
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }
}