using UnityEngine;
using UnityEngine.UI;
public class TimeController : MonoBehaviour
{
    [SerializeField] private Text timeText;
    private float time;
    private string minute, second;
    void Start()
    {
        time = 90;
    }
    void Update()
    {
        ShowTime();
    }
    private void ShowTime()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        minute = ((time / 60) < 10) ? "0" + ((int)time / 60).ToString() : ((int)time / 60).ToString();
        second = ((time % 60) < 10) ? "0" + ((int)time % 60).ToString() : ((int)time % 60).ToString();
        timeText.text = minute + ":" + second;
    }
}
