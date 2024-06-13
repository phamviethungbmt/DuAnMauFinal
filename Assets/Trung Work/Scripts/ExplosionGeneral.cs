using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionGeneral : MonoBehaviour
{
    public AudioSource audioExplosion;
    GameObject playerMain;
    void Start()
    {
        playerMain = GameObject.FindGameObjectWithTag("Player");
        audioExplosion.Play();
    }
}
