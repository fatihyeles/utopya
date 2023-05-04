using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkVoice : MonoBehaviour
{
   
    AudioSource audioSource;
    Rigidbody2D rb2D;
    float x;
    float y;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
   

        if (rb2D.velocity.x != 0 || rb2D.velocity.y != 0)
        {

            if (!audioSource.isPlaying)
            {
                audioSource.Play();

            }
        }
        else
        {
            audioSource.Stop();
        }



    }
}


