using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    private PlayerInput playerInput;

    private InputAction touchPositionAction;

    private InputAction touchPressAction;

    private Rigidbody rb;



    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();


        playerInput = GetComponent<PlayerInput>();



        touchPressAction = playerInput.actions["TouchPress"];


        touchPositionAction = playerInput.actions["TouchPosition"];
    }
    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed;

    }
    private void OnDisable()
    {
        touchPressAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector2 postition = touchPositionAction.ReadValue<Vector2>();
        Debug.Log(postition);

        rb.AddForce(new Vector3(0, 100, 0), ForceMode.Force);
    }
}
