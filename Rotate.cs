using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float spinX = 0;
    public float spinY = 0;
    public float spinZ = 0;

    void Update()
    {
        transform.Rotate(spinX, spinY, spinZ);
    }
}
