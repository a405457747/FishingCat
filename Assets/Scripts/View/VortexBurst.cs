using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexBurst : MonoBehaviour {

    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        ps.Stop();
        ps.Play();
    }
}
