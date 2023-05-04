using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2d : MonoBehaviour
{
    Rigidbody2D rigidbody2d; //Bedeni tanımlama
    [SerializeField] float speed = 4f; //Karakterin hızı
    [SerializeField] float runSpeed = 5f;
    Vector2 motionVector;  //Hareket vektörü
     public Vector2 lastMotionVector; //Son hareket vektörü
    Animator animator; 
    public bool moving; //Karakterin hareket etmesi
    bool running;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            running = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");  //Yatay yönü tanımlama
        float vertical   = Input.GetAxisRaw("Vertical");  //Dikey yönü tanımlama
        motionVector.x = horizontal;
        motionVector.y = vertical;
        
        animator.SetFloat("horizontal", horizontal); //Yatay yönde ilerlerken karakterin animasyonu
        animator.SetFloat("vertical", vertical);     //Dikey yönde ilerlerken karakterin animasyonu


        moving = horizontal != 0 || vertical != 0;  //Karakterin yürümeye baþladýðýný gösterme
        animator.SetBool("moving", moving);


        if (horizontal != 0 || vertical !=0)
        {
            lastMotionVector = new Vector2(horizontal, vertical).normalized; //Son hareket gösterimi
            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);
        }


    }


    void FixedUpdate()
    {
        Move();

    }
    private void Move()
    {
        rigidbody2d.velocity = motionVector * (running == true ?  runSpeed : speed);//cismin hızına hareket vektörü atanma
    }

    private void OnDisable()
    {
        rigidbody2d.velocity = Vector2.zero;
    }
}
