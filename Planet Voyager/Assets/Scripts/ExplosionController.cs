using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip explode;

    void ExplodeSound()
    {
        audioSource.PlayOneShot(explode, 1.0f);
    }

    void ResetLevel()
    {
        gameObject.SetActive(false);
    }
}
