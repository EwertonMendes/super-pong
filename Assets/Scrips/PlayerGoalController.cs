using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoalController : MonoBehaviour
{
    private AudioSource goalSound;
    private void Start()
    {
        goalSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            if (!goalSound.isPlaying)
            {
                goalSound.Play();
            }
        }
    }
}
