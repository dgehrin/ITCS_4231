using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_Timer : MonoBehaviour
{

    public float startTime;
    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        startTime = 60;   
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime > 0)
        {
            if (startTime <= 10)
            {
                timerText.color = Color.red;
            }
            startTime -= Time.deltaTime;
        }
        if (startTime <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        timerText.text = "Time Left: " + Mathf.Round(startTime) + "sec";
    }
}
