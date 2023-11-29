using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    public static AudioSource audioSource;
    public static AudioClip Attack;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Attack = Resources.Load<AudioClip>("shoot_2");
    }

    // Update is called once per frame
    public static void PlayAudioAttack()
    {
        audioSource.PlayOneShot(Attack);
    }
}
