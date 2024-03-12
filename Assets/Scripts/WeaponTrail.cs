using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrail : MonoBehaviour
{
    [Header("Weapon FX")]
    public ParticleSystem normalWeaponTrail;

    public void PlayWeaponFX()
    {
        normalWeaponTrail.Stop();

        if(normalWeaponTrail.isStopped)
        {
            normalWeaponTrail.Play();
        }
    }
}
