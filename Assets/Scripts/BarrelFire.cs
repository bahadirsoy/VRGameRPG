using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelFire : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject destroyParticleEffect;
    [SerializeField] private GameObject fireEffect;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Destroy()
    {
        BarrelExplosionAudio();
        
        Destroy(Instantiate(destroyParticleEffect, transform.position, transform.rotation), 1.5f);
        Destroy(Instantiate(fireEffect, transform.position, transform.rotation), 7f);
        Destroy(gameObject);
    }

    private void BarrelExplosionAudio()
    {
        float pitchValue = Random.Range(0.5f, 1.2f);
        audioSource.pitch = pitchValue;

        //audioSource.Play();
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
    }
}
