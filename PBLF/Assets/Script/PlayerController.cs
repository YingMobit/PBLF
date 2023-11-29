using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D myrigidbody;
    [Header("Health")]
    public int health;
    public int maxhealth;

    public int attack_type;

    public bool isdead;
    [Header("Velocity")]
    public float move_direction_x;
    public float move_direction_y;
    public Vector3 LeftMove;
    public Vector3 RightMove;
    public Vector3 UpMove;
    public Vector3 DownMove;
    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        move_direction_x = Input.GetAxis("Horizontal");
        move_direction_y = Input.GetAxis("Vertical");
        if (!isdead && Input.GetKeyDown(KeyCode.A))
        {
            myrigidbody.velocity = LeftMove;
        }
        if (!isdead && Input.GetKeyDown(KeyCode.D))
        {
            myrigidbody.velocity = RightMove;
        }
        if (!isdead && Input.GetKeyDown(KeyCode.W))
        {
            myrigidbody.velocity = UpMove;
        }
        if (!isdead && Input.GetKeyDown(KeyCode.S))
        {
            myrigidbody.velocity = DownMove;
        }
    }
}
