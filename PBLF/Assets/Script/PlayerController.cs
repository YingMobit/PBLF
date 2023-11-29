using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject Player_bullet;
    public Rigidbody2D myrigidbody;
    public Animator animator;
    [Header("Health")]
    public int health;
    public int maxhealth;

    public int attack_type;

    public bool isdead;
    [Header("Velocity")]
    public float move_direction_x;
    public float move_direction_y;
    public float velocity;
    
    private float timer = -100;
    public Vector3 correction;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myrigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    public void Move()
    {
        move_direction_x = Input.GetAxis("Horizontal");
        move_direction_y = Input.GetAxis("Vertical");
        if (!isdead)
        {
            myrigidbody.velocity = new Vector3(move_direction_x*velocity ,move_direction_y*velocity ,0);
            if (move_direction_x > 0) { animator.SetBool("TrunRight", true); animator.SetBool("TrunLeft", false); }
            else if (move_direction_x < 0) { animator.SetBool("TrunRight", false); animator.SetBool("TrunLeft", true);}
            else { animator.SetBool("TrunRight", false); animator.SetBool("TrunLeft", false); }
        }
    }

    public void Shoot()
    {
        if (Time.time - timer > 0.2f)
        {
            timer = Time.time;
            Instantiate(Player_bullet,transform .position+correction ,Quaternion.identity);
            SoundEffectController.PlayAudioAttack();
        }
    }
}
