using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioSource audioExplosion;
    void Start()
    {
        audioExplosion.Play();
    }
}