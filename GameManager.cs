using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject levelCompleteUI;
    public GameObject levelRestartUI;
    
    public void LevelComplete() {
        levelCompleteUI.SetActive(true);
    }

    public void RestartLevel()
    {
        levelRestartUI.SetActive(true);
    }

}
