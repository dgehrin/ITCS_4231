using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour 
{
    public float moveAccel = 2f;
    public float curSpeed = 0f;
    public float minSpeed = 0f;
    public float maxSpeed = 10f;
    public float jumpForce = 50f;

    public Transform camPivot;
    float mouseXMove = 0;
    float mouseYMove = 0;
    public Transform cam;

    int jumpCount = 0;
    Collision theCollision;

    Vector3 camForward;
    Vector3 camRight;

    Vector2 input;
    Animator anim;

    //Camera collision stuff
    //First two values must be changed according to position of camera
    public float distanceAway;
    public float distanceUp;
    public Vector3 velocityCamSmooth = Vector3.zero;
    public float camSmoothDampTime = 0.3f;
    public Vector3 lookDir;
    public Vector3 targetPos;

    void Start()
    {
        anim = GetComponent<Animator>();
        lookDir = transform.forward;
    }
    
    void Update()
    {
        DoInput();
        CalculateCamera();
        DoMovement();

        if(Input.GetKeyDown("w") || Input.GetKeyDown("s"))
        {
            anim.Play("Run", -1, 0f);
            anim.SetFloat("Run",1);
        }
        else if(Input.GetKeyUp("w") || Input.GetKeyUp("s"))
        {
            anim.Play("Idle", -1, 0f);
            anim.SetFloat("Run", 0);
        }
        //Shortcut for reloading level
        if (Input.GetKeyDown("r")) {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    void DoInput()
    {
        mouseXMove += Input.GetAxis("Mouse X") * Time.deltaTime * 140;
        mouseYMove -= Input.GetAxis("Mouse Y") * Time.deltaTime * 140;
        mouseYMove = Mathf.Clamp(mouseYMove, -80, 60); //limit Y movement of camera
        camPivot.rotation = Quaternion.Euler(mouseYMove, mouseXMove, 0);
        transform.rotation = Quaternion.Euler(0, mouseXMove, 0); //make player face direction camera is looking, but only on x axis

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);
    }

    void CalculateCamera()
    {
        camForward = cam.forward;
        camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    void DoMovement()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0)
        {
            curSpeed += moveAccel * Time.deltaTime;
            if (curSpeed > maxSpeed)
            {
                curSpeed = maxSpeed;
            }
            transform.position += curSpeed * (camForward * input.y + camRight * input.x) * Time.deltaTime;
        }
        else { 
            curSpeed += -1 * moveAccel * Time.deltaTime;
            if (curSpeed < minSpeed)
            {
                curSpeed = minSpeed;
            }
        }
        if(GetComponent<Rigidbody>().velocity.y == 0)
        {
            jumpCount = 0;

        }
        /*else if (GetComponent<Rigidbody>().velocity.y != 0&& jumpCount==0)
        {
            jumpCount = 1;

        }*/
        if (Input.GetButtonDown("Jump") &&jumpCount<2) {
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
            anim.Play("HumanoidJumpUp", -1, 0f); //Play jump animation
            jumpCount++;
        }
        
    }

    //Camera collision
    void LateUpdate() {
        //Vector3 character = transform.position + new Vector3(0f, distanceUp, 0f);
        Vector3 character = camPivot.transform.position + new Vector3(0f, distanceUp, 0f);

        lookDir = character - cam.transform.position;
        lookDir.y = 0;
        lookDir.Normalize();
        Debug.DrawRay(cam.transform.position, lookDir, Color.green);
        
        targetPos = character + transform.up * distanceUp - lookDir * distanceAway;
        targetPos.y = cam.transform.position.y; //track y position of camera
        Debug.DrawLine(transform.position, targetPos, Color.magenta);

        CompensateForWalls(character, ref targetPos);
        smoothPosition(cam.transform.position, targetPos);
        cam.transform.LookAt(character);
    }

    void smoothPosition(Vector3 fromPos, Vector3 toPos) {
        //Make smooth transition of camera position
        cam.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
        Debug.DrawLine(transform.position, cam.transform.position, Color.black);
    }

    void CompensateForWalls(Vector3 from, ref Vector3 to) {
        Debug.DrawLine(from, to, Color.cyan);
        RaycastHit wallHit = new RaycastHit();
        if(Physics.Linecast(from, to, out wallHit)) {
            Debug.DrawRay(wallHit.point, Vector3.left, Color.red);
            to = new Vector3(wallHit.point.x, to.y, wallHit.point.z);
        }
    }
}
    
