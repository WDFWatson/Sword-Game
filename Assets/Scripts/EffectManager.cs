using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    public AudioSource audioSource;
    public Animator swordAnimator;
    public Animator redFlash;
    public List<AudioClip> clashSounds;
    public List<AudioClip> damageSounds;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }


    }

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (swordAnimator == null)
        {
            swordAnimator = InputController.instance.sword.GetComponent<Animator>();
        }
    }

    /*public void Parry()
    {
        swordAnimator.SetTrigger("Parry");
    }*/
    
    public void Clash()
    {
        swordAnimator.SetTrigger("Parry");
        int i = UnityEngine.Random.Range(0, clashSounds.Count);
        audioSource.PlayOneShot(clashSounds[i]);
    }

    public void Damage()
    {
        redFlash.SetTrigger("TakeDamage");
        int i = UnityEngine.Random.Range(0, damageSounds.Count);
        audioSource.PlayOneShot(damageSounds[i]);
    }
    
}
