using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Neko : MonoBehaviour
{
    public int tempSpeed;
    public int moveSpeed; 
    public int runSpeed;
    public float hp;
    public float stamina;
    public float maxHP;
    public float maxStamina;
    public bool isStaminaLow = false;

    public float timeBtwAttack;
    public float startTimeBtwAttack;

    Animator animator;
    private string currentState;

    bool isDead;
    bool isAttackPressed;
    bool isAttacking = false;
    public Transform attackPos;
    public float attackRange;
    public LayerMask enemiesLayer;
    public int damage;

    //Animation State
    const string NEKO_IDLE = "idle";
    const string NEKO_WALK = "walk";
    const string NEKO_RUN = "run";
    const string NEKO_ATTACK = "attack";
    const string NEKO_DEAD = "dead";

    private SpriteRenderer spriteRenderer;

    public Rigidbody2D myBD;
    private Vector2 moveDirection;
    void Start()
    {
        myBD= GetComponent<Rigidbody2D>();  
        hp=maxHP; stamina=maxStamina;
        spriteRenderer=GetComponent<SpriteRenderer>();
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        NekoAttack();
        //CheckAttackAnim();
        CheckRotation();
        NekoDead();
        HealthConsume();
        StaminaCheck();
    }
    private void FixedUpdate()
    {
        Move();
        if (isStaminaLow==false)
        {
            Run();
        }
        else if(isStaminaLow)
        {
            tempSpeed = moveSpeed;
        }

        
    }
    void CheckInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        
       // Debug.Log("Input X la : " + moveX + " Input Y la" + moveY);
    }



    void Move()
    {
        if (isDead == false)
        {
            myBD.velocity = new Vector2(moveDirection.x * tempSpeed, moveDirection.y * tempSpeed);
            Debug.Log(myBD.velocity);
            if (myBD.velocity.x < 0 && myBD.velocity.x >= -moveSpeed)
            {
                ChangeAnimationState(NEKO_WALK);
                //this.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (myBD.velocity.x > 0 && myBD.velocity.x <= moveSpeed)
            {
                ChangeAnimationState(NEKO_WALK);
                //this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (myBD.velocity.y > 0 && myBD.velocity.y <= moveSpeed)
            {
                ChangeAnimationState(NEKO_WALK);
            }
            if (myBD.velocity.y < 0 && myBD.velocity.y >= -moveSpeed)
            {
                ChangeAnimationState(NEKO_WALK);
            }
            else if (myBD.velocity.x == 0 && myBD.velocity.y == 0)
            {
                ChangeAnimationState(NEKO_IDLE);
            }
            if (myBD.velocity.x > moveSpeed || myBD.velocity.x < -moveSpeed || myBD.velocity.y > moveSpeed || myBD.velocity.y < -moveSpeed)
            {
                ChangeAnimationState(NEKO_RUN);
            }
        }
    }
    void Run()
    {
        if (isStaminaLow == false)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                tempSpeed = runSpeed;
                //Debug.Log("Running");
                //  ChangeAnimationState(NEKO_RUN);
            }
            else
            {
                tempSpeed = moveSpeed;
                //  ChangeAnimationState(NEKO_WALK);
            }
        }
    }
    void HealthConsume()
    {
        if (hp >= 0)
        {
            hp -= 4f*Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            hp += 5;
            hp = Mathf.Clamp(hp, 0, maxHP);
        }
    }
    void ChangeAnimationState(string newState)
    {
        if(currentState == newState) return;//tránh Animation tự phá chính nó
        animator.Play(newState);
        currentState= newState;//thay newState vào 
    }
    void CheckRotation()
    {
        if(myBD.velocity.x>0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(myBD.velocity.x<0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }
    void NekoDead()
    {
        if(hp<=0)
        {
            myBD.bodyType = RigidbodyType2D.Static;
            isDead= true;
            ChangeAnimationState(NEKO_DEAD);
        }
    }

   void NekoAttack()
    {
        if(timeBtwAttack<=0)
        {
            timeBtwAttack = startTimeBtwAttack;
            if(Input.GetKey(KeyCode.Space))
            {
                ChangeAnimationState(NEKO_ATTACK);
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position,attackRange,enemiesLayer);
                for (int i = 0; i < enemies.LongLength; i++)
                {
                    //enemies[i].GetComponent<Enemy>().TakeDamage(damage);
                }      
               // yield return new WaitForSeconds(1f);

            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    //Hàm TakeDamge này cho vào script Enemy
    void TakeDamage(int damage)
    {       
        hp -= damage;
        Debug.Log("DamageTaken");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    void StaminaCheck()
    {
        if(currentState==NEKO_WALK || currentState == NEKO_IDLE)
        {
            stamina += 20 * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
        }
        if(currentState == NEKO_RUN)
        {
            stamina -= 40 * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
        }
        if (stamina <=10)
        {
            isStaminaLow = true;
        }
        else if(stamina>=30)
        {
            isStaminaLow = false;
        }
    }
}
