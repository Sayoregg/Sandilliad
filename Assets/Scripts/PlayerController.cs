using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Public Variables
    public GameObject footLeft;
    public GameObject footRight;
    public GameObject eyeLeft;
    public GameObject eyeRight;
    public GameObject body;
    public GameObject bodyPivot;
    public GameObject hole;
    public GuzzlerColliderController guzzlerCollider;
    public float sandAmount;

    public float torque;

    //Private Variables
    private CharacterController controller;
    private Vector3 startingForward;
    private GameObject sandMeter;

    private float sandMax;
    private float sandOverflow;
    private float sandTimer;

    private bool isBroken;


    //Input actions
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction repairAction;

    //Movement variables
    public float moveSpeed = 10f;
    private float walkingTimer = 0f;
    public Vector3 velocity;
    public bool isGrounded;
    public float gravitySpecial = -25f;
    //Jump variables
    private bool isJumping;
    public float jumpHeight = 2.5f;
    public float maxJumpTime = 0.3f;
    public float jumpHoldForce = 25f;
    private float jumpTimeCounter;


    void Start()
    {
        //Gameobjects
        controller = GetComponent<CharacterController>();
        sandMeter = GameObject.FindGameObjectWithTag("SandMeter");

        //Inputs
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        repairAction = InputSystem.actions.FindAction("Interact");

        //Variables
        sandMeter.GetComponent<Image>().fillAmount = 0f;
        sandMax = 140f;
        sandOverflow = 60f;
        sandAmount = 20f;
        sandTimer = 0f;
    }
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        isGrounded = GetComponent<CharacterController>().isGrounded;
        Walking();
        Turning();
        Jumping();
        UpdateSandMeter();

        // Apply gravity
        velocity.y += gravitySpecial * Time.deltaTime;
        if (isGrounded)
        {
            velocity.x = 0f;
            velocity.z = 0f;
        }
        controller.Move(velocity * Time.deltaTime);
    }
    private void Walking()
    {
        //Movement
        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        controller.Move(new Vector3(movementInput.x * moveSpeed * Time.deltaTime, 0));
        body.transform.position = bodyPivot.transform.position;

        //Animation
        if (movementInput.magnitude > 0)
        {
            walkingTimer += Time.deltaTime;
        }
        else
        {
            walkingTimer = 0f;
        }
        footLeft.transform.position = new Vector3(transform.position.x + (1 * Mathf.Sin(2 * Mathf.PI * 1.5f * walkingTimer)), footLeft.transform.position.y, footLeft.transform.position.z);
        footRight.transform.position = new Vector3(transform.position.x - (1 * Mathf.Sin(2 * Mathf.PI * 1.5f * walkingTimer)), footRight.transform.position.y, footRight.transform.position.z);
    }

    private void Turning()
    {
        //Variables
        Vector2 mousePos, guzzlerPos;
        mousePos = Mouse.current.position.ReadValue();
        guzzlerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 relativeVector = mousePos - guzzlerPos;
        relativeVector.Normalize();
        float cross = Vector3.Cross(relativeVector, body.transform.forward).z;

        //Turning
        body.GetComponent<Rigidbody>().AddTorque(Vector3.forward * cross * -torque);

        //Rotation Dampening
        body.GetComponent<Rigidbody>().angularVelocity = Vector3.ClampMagnitude(body.GetComponent<Rigidbody>().angularVelocity, 10f);
        if (Mathf.Abs(cross) < 0.1f)
        {
            body.GetComponent<Rigidbody>().angularVelocity = Vector3.MoveTowards(body.GetComponent<Rigidbody>().angularVelocity, Vector3.zero, 30f * Time.deltaTime);
        }

        //Eye Position Switching
        if (body.transform.forward.x < 0)
        {
            eyeLeft.transform.localPosition = new Vector3(-0.735f, 0.375f, 0.325f);
            eyeRight.transform.localPosition = new Vector3(-0.735f, 0.375f, 0.325f);
        } 
        if (body.transform.forward.x > 0)
        {
            eyeLeft.transform.localPosition = new Vector3(-0.735f, -0.375f, 0.325f);
            eyeRight.transform.localPosition = new Vector3(-0.735f, -0.375f, 0.325f);
        }
    }




    private void Jumping()
    {
        // Jump start
        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravitySpecial);
            isJumping = true;
            jumpTimeCounter = 0f;
        }
        //Jump hold
        if (jumpAction.IsPressed() && isJumping)
        {
            if (jumpTimeCounter < maxJumpTime)
            {
                velocity.y += jumpHoldForce * Time.deltaTime;
                jumpTimeCounter += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        // Jump end
        if (jumpAction.WasReleasedThisFrame())
        {
            isJumping = false;
        }
    }
    private void UpdateSandMeter()
    {
        sandTimer += Time.deltaTime;
        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        Image sandMeterImage = sandMeter.GetComponent<Image>();

        //Meter Filling
 

        float maxPlusOverflow = 0.277777f + 0.277777f * (sandOverflow/sandMax);
        Debug.Log("Max+Oveflow: " +maxPlusOverflow);
        sandMeterImage.fillAmount = Mathf.MoveTowards(sandMeterImage.fillAmount, maxPlusOverflow * sandAmount / (sandMax + sandOverflow), Time.deltaTime * 0.4f);
        sandMeterImage.fillAmount = Mathf.Clamp(sandMeterImage.fillAmount, 0, maxPlusOverflow);
        sandAmount = Mathf.Clamp(sandAmount, 0, sandMax + sandOverflow);

        //Overflow
        if (sandAmount > sandMax && sandTimer > 2f)
        {
            sandAmount = Mathf.MoveTowards(sandAmount, sandMax, 20f * Time.deltaTime);
        }

        //Oveflow Explosion
        if (sandAmount >= sandMax + sandOverflow)
        {
            isBroken = true;
        }
        if (isBroken)
        {
            hole.SetActive(true); 
            sandAmount = Mathf.MoveTowards(sandAmount, 0, 10f * Time.deltaTime);

            if (repairAction.WasPressedThisFrame() && sandAmount > 60f)
            {
                sandAmount -= 60f;
                isBroken = false;
            }
        } else
        {
            hole.SetActive(false);
        }

        //Meter Turning
        sandMeter.GetComponent<RectTransform>().localEulerAngles = Vector3.forward * (Vector3.SignedAngle(Vector3.right, body.transform.forward, Vector3.forward) - 40);


    }
    public void AddSand(float amount)
    {
        sandAmount += amount;
        sandTimer = 0;
    }
    public void RemoveSand(float amount)
    {
        sandAmount -= amount;
    }

}
