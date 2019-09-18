using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class MachineParticleRunner : MonoBehaviour
{
    Animator grinder;
    ParticleSystem pSystem;

    // Start is called before the first frame update
    void Start()
    {
        grinder = transform.GetComponentInChildren<Animator>();
        pSystem = transform.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grinder.GetCurrentAnimatorStateInfo(0).IsName("Process"))
        {
            pSystem.gameObject.SetActive(true);
        }else pSystem.gameObject.SetActive(false);
    }
}
