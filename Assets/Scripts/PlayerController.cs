using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    //Public Variables
    public GameObject footLeft;
    public GameObject footRight;
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
        startingForward = body.transform.forward;

        Vector2 mousePos, guzzlerPos;
        mousePos = Mouse.current.position.ReadValue();
        guzzlerPos = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 relativeVector = mousePos - guzzlerPos;
        relativeVector.Normalize();

        body.GetComponent<Rigidbody>().AddTorque(Vector3.forward * Vector3.Cross(relativeVector, body.transform.forward).z * -torque);

        Debug.Log(Vector3.forward * Vector3.Cross(relativeVector, body.transform.forward).z * -1);

        //body.GetComponent<Rigidbody>().rotation = Quaternion.RotateTowards(body.GetComponent<Rigidbody>().rotation, Quaternion.LookRotation(relativeVector), 600 * Time.deltaTime);
        //body.transform.forward = relativeVector;
        //if (guzzlerCollider.isColliding)
        //{
        //    body.transform.forward = startingForward;
        //}
    }

}
