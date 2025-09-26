using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    //Private Variables
    private CharacterController controller;

    //Input actions
    private InputAction moveAction;
    private InputAction jumpAction;

    //Movement variables
    public float moveSpeed = 10f;


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

    }
    private void Walking()
    {
        //Movement
        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        controller.Move(new Vector3(movementInput.x * moveSpeed * Time.deltaTime, 0));
    }

}
