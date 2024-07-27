using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Text _deathMenuText;
    [SerializeField] private int _mainMenuSceneIndex = 0;


    private void OnEnable()
    {
        _deathMenuText.text = _timer.SurvivalTime().ToString("F2");
        _timer.TimerText().gameObject.SetActive(false);
    }

    public void Restart()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneIndex);
    }
}
