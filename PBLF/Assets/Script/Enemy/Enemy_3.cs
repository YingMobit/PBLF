using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : MonoBehaviour
{
    [Header("Class")]
    private Rigidbody2D rigidbody;
    public GameObject bullet;
    public GameObject Explosion;
    public ScoreManager scoreManager;
    public MyBullets playerBullets;

    [Header("Shoot")]
    public float bullet_speed;
    public Vector3 Correction;
    public float StartAngle;//�Ƕ���
    public float EndAngle;//�Ƕ���
    private float AngleRange;
    private float AnglePerBullet;
    public float bullet_num;
    public int damage;

    [Header("Basic")]
    public int health;
    public int maxHealth;
    public int player_damage;
    public float lifetime;

    [Header("Reward")]
    public int score;

    float last_atk_time = -100;
    float life_timer;
    public Vector2 speed;
    // Start is called before the first frame update
    void Start()
    {
        ClassInitial();
        DataInitial();
    }

    void ClassInitial()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        rigidbody = GetComponent<Rigidbody2D>();
        //playerBullets = FindObjectOfType<MyBullets>();
    }

    void DataInitial()
    {
        health = maxHealth;
        player_damage = playerBullets.Damage;
        rigidbody.velocity = speed;
        EndAngle = -StartAngle;
        AngleRange = Mathf.Abs(EndAngle-StartAngle);
        AnglePerBullet = AngleRange / bullet_num;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        DeadAuto();
        Dead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_bullet")
        {
            health -= player_damage;
            Destroy(collision.gameObject);
        }
    }

    private void Dead()
    {
        if (health <= 0)
        {
            scoreManager.ChangeScore(score);
            SoundEffectManager.PlayAudioBeHitted();
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void DeadAuto()
    {
        life_timer += Time.deltaTime;
        if (life_timer > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void Shoot()//���ε�Ļ8���ӵ�
    {
        if (Time.time - last_atk_time > 2.5)
        {
            last_atk_time = Time.time;
            for (int i = 0; i < bullet_num; i++)
            {
                GameObject newbullet = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, StartAngle + i * AnglePerBullet));
                Rigidbody2D rb = newbullet.GetComponent<Rigidbody2D>();
                float tan_a = 0;
                float angle = newbullet.transform.rotation.eulerAngles.z;
                if (angle > 180f) angle -= 360f;
                tan_a = Mathf.Tan(Mathf.Deg2Rad * (90f - Mathf.Abs(angle)));
                if (angle > 0f) rb.velocity = bullet_speed * new Vector2(tan_a, -1f);
                else rb.velocity = bullet_speed * new Vector2(tan_a, 1);
            }
        }
    }

}
