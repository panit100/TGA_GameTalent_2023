using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem Slowpoke;
    [SerializeField] private ParticleSystem BrokeAlarm;

    public void PlaySlowpokeVFX()
    {
        Slowpoke.Play();
    }

    public void PlayBrokeAlarm()
    {
        BrokeAlarm.Play();
    }
}
