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

    private string touch0PressedSide;


    private PlayerInput playerInput;
    private InputAction touch0PositionAction;
    private InputAction touch1PositionAction;
    private InputAction touch0PressAction;
    private InputAction touch1PressAction;

    public bool attacking = false;
    private Collider attackedEnemy;


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

        if (activeTouches.Count == 5)
        {
            GameManager.instance.invincible = true;
            GameManager.instance.health = 100;
            GameManager.instance.UpdateHealthBar();
        }
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


        // If the player touches the left side of the screen, attack at upper lane. Else, attack at bottom lane
        if (position.x < Screen.width / 2)
        {
            touch0PressedSide = "left";
            Attack(upperLaneAttackPos);
        }
        else
        {
            touch0PressedSide = "right";
            Attack(bottomLaneAttackPos);
        }
    }

    private void Touch0Canceled(InputAction.CallbackContext context)
    {
        if (GameManager.instance.playerAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attacking"))
        {
            GameManager.instance.playerAnim.SetTrigger("Idling");
            GameManager.instance.swordAnim.SetTrigger("Idling");
        }

        // Change hold enemy state when the player releases the screen
        if (attackedEnemy != null)
        {
            attackedEnemy.GetComponent<EnemyBehaviour>().NextState(PlayerInputs.Release, 4);
            attackedEnemy = null;
        }

    }

    private void Touch1Pressed(InputAction.CallbackContext context)
    {
        Vector2 position = touch1PositionAction.ReadValue<Vector2>();

        if ((touch0PressedSide == "left" && position.x > Screen.width / 2) || (touch0PressedSide == "right" && position.x < Screen.width / 2))
        {
           Defend();
        }
    }


    public void Attack(Transform attackPos)
    {
        if (!GameManager.instance.gamePaused)
        {
                AnimatorClipInfo animatorinfo = GameManager.instance.playerAnim.GetCurrentAnimatorClipInfo(0)[0];
                if (touch0PressedSide == "left")
                {
                    if (animatorinfo.clip.name != "PlayerRig_PlayerAttack Top")
                    {
                        GameManager.instance.playerAnim.SetTrigger("AttackedTop");
                        GameManager.instance.swordAnim.SetTrigger("AttackedTop");
                    }
                }
                else
                {
                    if (animatorinfo.clip.name != "PlayerRig_PlayerAttack Base")
                    {
                        GameManager.instance.playerAnim.SetTrigger("AttackedBase");
                        GameManager.instance.swordAnim.SetTrigger("AttackedBase");
                    }
                }
            

            Collider[] enemyToDamage = Physics.OverlapSphere(attackPos.position, attackRange, enemyLayer); // Gets the firts enemy in player's attack area
            if (enemyToDamage.Length != 0)
            {
                attackedEnemy = enemyToDamage[0];
                float precision = Mathf.Abs(attackPos.position.x - enemyToDamage[0].transform.position.x);
                attackedEnemy.GetComponent<EnemyBehaviour>().NextState(PlayerInputs.Attack, precision);
            }
        }
    }

    public void Defend()
    {
        Collider[] enemiesToDamage = new Collider[2];
        Collider[] enemiesInArea = Physics.OverlapSphere(bottomLaneAttackPos.position, attackRange, enemyLayer); // Gets the enemies in player's bottom attack area 

        if (enemiesInArea.Length != 0)
        {
            enemiesToDamage[0] = enemiesInArea[0]; // Gets the firts enemy in player's bottom attack area 
        }

        enemiesInArea = Physics.OverlapSphere(upperLaneAttackPos.position, attackRange, enemyLayer); // Gets the enemies in player's bottom attack area 

        if (enemiesInArea.Length != 0)
        {
            enemiesToDamage[1] = enemiesInArea[0];  // Gets the firts enemy in player's bottom attack area 
        }

        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i] != null)
            {
                float precision = Mathf.Abs(bottomLaneAttackPos.position.x - enemiesToDamage[i].transform.position.x);
                enemiesToDamage[i].GetComponent<EnemyBehaviour>().NextState(PlayerInputs.Defend, precision);
            }
        }

        GameManager.instance.playerAnim.SetTrigger("Defended");
        GameManager.instance.swordAnim.SetTrigger("Defended");
    }

    public void SetAttacking(bool input)
    {
        attacking = input;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(upperLaneAttackPos.position, attackRange);

        Gizmos.DrawWireSphere(bottomLaneAttackPos.position, attackRange);
    }
}
