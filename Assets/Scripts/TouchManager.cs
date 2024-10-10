using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class TouchManager : MonoBehaviour
{
    public Transform upperLaneAttackPos;
    public Transform bottomLaneAttackPos;
    public float attackRange;
    public LayerMask enemyLayer;

    private bool touch0Pressed = false;
    private string touch0PressedSide;


    private PlayerInput playerInput;
    private InputAction touch0PositionAction;
    private InputAction touch1PositionAction;
    private InputAction touch0PressAction;
    private InputAction touch1PressAction;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();


        touch0PositionAction = playerInput.actions["Touch0Position"];
        touch1PositionAction = playerInput.actions["Touch1Position"];

        touch0PressAction = playerInput.actions["Touch0Press"];
        touch1PressAction = playerInput.actions["Touch1Press"];
    }

    private void Update()
    {
        var activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;

        //if (activeTouches.Count == 2 && activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Began && activeTouches[1].phase == UnityEngine.InputSystem.TouchPhase.Began)
        //{
        //   if ((activeTouches[0].screenPosition.x < Screen.width/2 && activeTouches[1].screenPosition.x > Screen.width / 2) || activeTouches[1].screenPosition.x < Screen.width / 2 && activeTouches[0].screenPosition.x > Screen.width / 2)
        //    {
        //        Defend();
        //    }
        //}
        if (activeTouches.Count == 5)
        {
            GameManager.instance.invincible = true;
            GameManager.instance.health = 100;
            GameManager.instance.UpdateHealthBar();
        }

        //Debug.Log(touch0Pressed);
    }

    private void OnEnable()
    {
        touch0PressAction.performed += Touch0Pressed;
        touch0PressAction.canceled += Touch0Canceled;
        touch1PressAction.performed += Touch1Pressed;
        EnhancedTouchSupport.Enable();

    }
    private void OnDisable()
    {
        touch0PressAction.performed -= Touch0Pressed;
        touch0PressAction.canceled -= Touch0Canceled;
        touch1PressAction.performed -= Touch1Pressed;
        EnhancedTouchSupport.Disable();
    }

    private void Touch0Pressed(InputAction.CallbackContext context)
    {
        Vector2 position = touch0PositionAction.ReadValue<Vector2>();
        touch0Pressed = true;


        // If the player touches the left side of the screen, attack at upper lane. Else, attack at bottom lane
        if (position.x < Screen.width / 2)
        {
            Attack(upperLaneAttackPos);
            touch0PressedSide = "left";
        }
        else
        {
            Attack(bottomLaneAttackPos);
            touch0PressedSide = "right";
        }
    }

    private void Touch0Canceled(InputAction.CallbackContext context)
    {
        touch0Pressed = false;
    }

    private void Touch1Pressed(InputAction.CallbackContext context)
    {
        Debug.Log("Touch1");
        Vector2 position = touch1PositionAction.ReadValue<Vector2>();
        Debug.Log(position);

        if ((touch0PressedSide == "left" && position.x > Screen.width / 2) || (touch0PressedSide == "right" && position.x < Screen.width / 2))
        {
           Defend();
        }
    }


    public void Attack(Transform attackPos)
    {
        Debug.Log("Attack");
        
        Collider[] enemiesToDamage = Physics.OverlapSphere(attackPos.position, attackRange, enemyLayer); // Gets enemies in player's attack area
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyBehaviour>().Damage();
        }
    }

    public void Defend()
    {
        Debug.Log("Defend");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(upperLaneAttackPos.position, attackRange);

        Gizmos.DrawWireSphere(bottomLaneAttackPos.position, attackRange);
    }
}
