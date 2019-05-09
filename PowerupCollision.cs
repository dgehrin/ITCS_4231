using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerupCollision : MonoBehaviour
{
    public PlayerMovement movement;
    //jump powerup
    public CapsuleCollider powerup1Collider;
    public MeshRenderer powerup1Render;

    //Speed powerup:
    public CapsuleCollider powerup2Collider;
    public MeshRenderer powerup2Render;

    //Speed powerup2
    public CapsuleCollider speedPowerup2Collider;
    public MeshRenderer speedPowerup2Render;

    //slow powerup:
    public CapsuleCollider powerup3Collider;
    public MeshRenderer powerup3Render;

    //jumpTimer and speedTimer stuff
    public float duration = 5f;
    public Image jumpTimer;
    public GameObject obj;
    public Image speedTimer;
    public GameObject obj2;
    public GameObject obj3;

    IEnumerator OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "jumpPowerup")
        {
            //stuff for enabling jumpTimer image and script
            obj = GameObject.Find("jumpTimer");
            obj.GetComponent<circleTimer>().enabled = true;
            jumpTimer.enabled = true;

            movement.jumpForce *= 3;
            powerup1Collider.enabled = !powerup1Collider.enabled;
            powerup1Render.enabled = !powerup1Render.enabled;

            //start countdown timer
            yield return new WaitForSeconds(duration);
            //reverse powerup effect
            movement.jumpForce /= 3;

        }
        else if(collisionInfo.collider.tag == "speedPowerup")
        {
            //stuff for enabling speedTimer image and script
            obj2 = GameObject.Find("speedTimer");
            obj2.GetComponent<circleTimer>().enabled = true;
            speedTimer.enabled = true;

            movement.maxSpeed *= 2;
            powerup2Collider.enabled = !powerup2Collider.enabled;
            powerup2Render.enabled = !powerup2Render.enabled;

            //start countdown timer
            yield return new WaitForSeconds(duration);
            //reverse powerup effect
            movement.maxSpeed /= 2;
        }
        else if (collisionInfo.collider.tag == "speedPowerup2")
        {
            //stuff for enabling speedTimer image and script
            obj3 = GameObject.Find("speedTimer");
            obj3.GetComponent<circleTimer>().enabled = true;
            speedTimer.enabled = true;

            movement.maxSpeed *= 2;
            speedPowerup2Collider.enabled = !speedPowerup2Collider.enabled;
            speedPowerup2Render.enabled = !speedPowerup2Render.enabled;

            //start countdown timer
            yield return new WaitForSeconds(duration);
            //reverse powerup effect
            movement.maxSpeed /= 2;
        }
        else if (collisionInfo.collider.tag == "slowPowerup")
        {
            movement.maxSpeed /=4;
            powerup3Collider.enabled = !powerup3Collider.enabled;
            powerup3Render.enabled = !powerup3Render.enabled;

            //start countdown timer
            yield return new WaitForSeconds(duration);
            //reverse powerup effect
            movement.maxSpeed *= 2;
        }
    }
}
