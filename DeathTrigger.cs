using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public GameManager gameManager;
    public Transform sophia;

    void OnTriggerEnter()
    {
        gameManager.RestartLevel();
        //sophia.position = new Vector3(sophia.position.x, 0, sophia.position.z);
    }
}
