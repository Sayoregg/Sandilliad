using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    //Public Variables
    public GameObject footLeft;
    public GameObject footRight;
    public GameObject eyeLeft;
    public GameObject eyeRight;
    public GameObject body;
    public GameObject bodyPivot;
    public GuzzlerColliderController guzzlerCollider;

    public float torque;

    //Private Variables
    private CharacterController controller;
    private Vector3 startingForward;

    //Input actions
    private InputAction moveAction;
    private InputAction jumpAction;

    //Movement variables
    public float moveSpeed = 10f;
    private float walkingTimer = 0f;


    void Start()
    {
        //Gameobjects
        controller = GetComponent<CharacterController>();

        //Inputs
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

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
        Walking();
        Turning();
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
            Debug.Log("STOOOOP");
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

        Debug.Log(body.GetComponent<Rigidbody>().angularVelocity);
    }

}
