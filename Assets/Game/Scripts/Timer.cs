using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    private float _survivalTime;

    private void Awake()
    {
        _survivalTime = 0;
        _timerText.gameObject.SetActive(true);
        _timerText.text = _survivalTime.ToString("F2");
    }

    private void Update()
    {
        _survivalTime += Time.deltaTime;
        Display();
    }

    public float SurvivalTime() => _survivalTime;

    public Text TimerText() => _timerText;

    private void Display()
    {
        _timerText.text = _survivalTime.ToString("F2");
    }
}
