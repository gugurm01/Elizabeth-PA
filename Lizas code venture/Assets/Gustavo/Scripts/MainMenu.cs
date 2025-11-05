using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SoundManager.Instance.PlaySoundUI();
    }

    public void Quit()
    {
        SoundManager.Instance.PlaySoundUI();
        Application.Quit();
    }
}
