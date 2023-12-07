using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static AudioSource audioSource;
    public static AudioClip Player_Attack;
    public static AudioClip BeHitted;
    public static AudioClip Enemy_Shoot;
    public static AudioClip Explosion;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Player_Attack = Resources.Load<AudioClip>("Player_Shoot");
        BeHitted = Resources.Load<AudioClip>("BeHitted");
        Enemy_Shoot = Resources.Load<AudioClip>("Enemy_Shoot");
        Explosion = Resources.Load<AudioClip>("Explosion");
    }

    // Update is called once per frame
    public static void PlayAudioPlayer_Attack()
    {
        Debug.Log("Played");
        audioSource.PlayOneShot(Player_Attack);
    }

    public static void PlayAudioBeHitted()
    {
        audioSource.PlayOneShot(BeHitted);
    }

    public static void PlayAudioEnemyShoot()
    {
        audioSource.PlayOneShot(Enemy_Shoot);
    }

    public static void PlayAudioExplosion()
    {
        audioSource.PlayOneShot(Explosion);
    }
}
