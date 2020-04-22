using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMovement : MonoBehaviour
{
    public float _moveSpeed = 8;
    public float _rotSpeed = 5.0f;
    private float rot = 0.0f;

    float jumpSpeed = 8.0f;
    float gravity = 25.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float vSpeed = 0;

    CharacterController controller;
    private GameObject Legs;
    private Animator LegsAni;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        foreach (Transform go in GetComponentsInChildren<Transform>())
        {
            if (go.name.ToLower() == "legs")
            {
                Legs = go.gameObject;
                LegsAni = Legs.GetComponent<Animator>();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController();
        Rotation();
    }
     
    void PlayerController()
    {
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        }
        else
        {
            moveDirection = Vector3.zero;
        }
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= _moveSpeed;
        //Debug.Log("Grounded: " + controller.isGrounded + " vSpeed: " + vSpeed);
        
        if (controller.isGrounded)
        {
            vSpeed = -1;
            if (Input.GetButtonDown("Jump"))
            {
                vSpeed = jumpSpeed;
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        moveDirection.y = vSpeed;
        controller.Move(moveDirection * Time.deltaTime);
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            LegsAni.SetBool("IsRunning", true);
        }
        else
        {
            LegsAni.SetBool("IsRunning", false);
        }
    }    

    void Rotation()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            rot += Input.GetAxis("Horizontal") * _rotSpeed;
            transform.eulerAngles = new Vector3(0.0f, rot, 0.0f);
        }
    }
}
