using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSounds : MonoBehaviour
{

    public AudioSource source;
    public AudioClip step;

    void WalkStep()
    {
        source.PlayOneShot(step);
    }

}