using System.Collections;
using System.Collections.Generic;
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

    private SpriteRenderer spriteRenderer;

    public Rigidbody2D myBD;
    private Vector2 moveDirection;
    void Start()
    {
        myBD= GetComponent<Rigidbody2D>();  
        hp=maxHP; stamina=maxStamina;
        spriteRenderer=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }
    private void FixedUpdate()
    {
        Move();
        Run();
        HealthConsume();
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
        myBD.velocity = new Vector2(moveDirection.x * tempSpeed, moveDirection.y * tempSpeed);
        Debug.Log(myBD.velocity);
        if (myBD.velocity.x < 0)
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
           // anim.Play("Walk");
        }
        else if (myBD.velocity.x > 0)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
         //   anim.Play("Walk");
        }
    }
    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            tempSpeed = runSpeed;
            //Debug.Log("Running");
        }
        else
        {
            tempSpeed = moveSpeed;
        }
    }
    void HealthConsume()
    {
        if (hp >= 0)
        {
            hp -= 0.05f;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            hp += 5;
            hp = Mathf.Clamp(hp, 0, maxHP);
        }
    }

}
