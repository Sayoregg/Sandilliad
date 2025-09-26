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

    //Private Variables
    private CharacterController controller;

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
        Vector2 mousePos, guzzlerPos;
        mousePos = Mouse.current.position.ReadValue();
        guzzlerPos = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 relativeVector = mousePos - guzzlerPos;
        float guzzlerAngle = Mathf.Atan2(relativeVector.y, relativeVector.x) * (180 / Mathf.PI);

        body.transform.rotation = Quaternion.Euler(-guzzlerAngle, 90, 0);

        if (Mathf.Abs(guzzlerAngle) > 90)
        {
            bodyPivot.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        } else
        {
            bodyPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        Debug.Log(guzzlerAngle);
    }

}
