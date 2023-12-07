using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_1 : MonoBehaviour 
{
    [Header ("Class")]
    private Rigidbody2D rigidbody;
    public  GameObject bullet;
    public GameObject Explosion;
    public ScoreManager scoreManager;
    public MyBullets playerBullets;

    [Header ("Shoot")]
    public Vector2 speed;
    public Vector3 Correction;
    public int damage;

    [Header ("Basic")]
    public int health;
    public int maxHealth;
    public int player_damage;
    public float lifetime;

    [Header ("Reward")]
    public int score;

    float last_atk_time = -100;
    float life_timer=0;

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
    }
    // Update is called once per frame
    void Update()
    {
        Shoot();
        Dead();
        DeadAuto();
    }

    void Shoot()//µ¥·¢µ¯Ä»
    {
        if (Time.time - last_atk_time >3f)
        {
            SoundEffectManager.PlayAudioEnemyShoot();
            last_atk_time = Time.time;
            Instantiate(bullet,gameObject .transform .position+Correction ,Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision .gameObject .tag == "Player_bullet")
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
            SoundEffectManager.PlayAudioExplosion();
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void DeadAuto()
    {
        life_timer += Time.deltaTime;
        if (life_timer>lifetime)
        {
            Destroy(gameObject);
        }
    }
}
