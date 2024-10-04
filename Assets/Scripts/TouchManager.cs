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


    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;


    private void Awake()
    {

        playerInput = GetComponent<PlayerInput>();



        touchPressAction = playerInput.actions["TouchPress"];


        touchPositionAction = playerInput.actions["TouchPosition"];
    }

    private void Update()
    {
        var activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;

        if (activeTouches.Count == 2 && activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Began && activeTouches[1].phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
           if ((activeTouches[0].screenPosition.x < Screen.width/2 && activeTouches[1].screenPosition.x > Screen.width / 2) || activeTouches[1].screenPosition.x < Screen.width / 2 && activeTouches[0].screenPosition.x > Screen.width / 2)
            {
                Defend();
            }
        }
        else if (activeTouches.Count == 5)
        {
            GameManager.instance.invincible = true;
            GameManager.instance.health = 100;
            GameManager.instance.UpdateHealthBar();
        }
    }

    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed;
        EnhancedTouchSupport.Enable();

    }
    private void OnDisable()
    {
        touchPressAction.performed -= TouchPressed;
        EnhancedTouchSupport.Disable();
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector2 position = touchPositionAction.ReadValue<Vector2>();

        // If the player touches the left side of the screen, attack at upper lane. Else, attack at bottom lane
        if (position.x < Screen.width / 2)
        {
            Attack(upperLaneAttackPos);
        }
        else
        {
            Attack(bottomLaneAttackPos);
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
