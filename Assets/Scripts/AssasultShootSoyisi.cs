using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssasultShootSoyisi : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gunTip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private Text remainingBulletText;
    [SerializeField] private Canvas gunInfoCanvas;

    private float bulletSpeed;
    private float fireDelayTime;
    private float fireDelayCounter;

    private int bulletCount;
    private int remainingBullet;
    private float reloadTime;
    private bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = 1f;
        fireDelayTime = 0.15f;
        fireDelayCounter = fireDelayTime;

        bulletCount = 30;
        remainingBullet = bulletCount;
        reloadTime = 2.5f;
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        fireDelayCounter += Time.deltaTime;
        shoot();
    }

    public void shoot()
    {
        if (fireDelayCounter >= fireDelayTime && remainingBullet > 0 && isShooting)
        {
            //gunshot audio
            GunShotAudio();

            //handle bullet
            DecreaseBullet();

            //create bullet, then fire and destroy it
            GameObject newBullet = Instantiate(bullet, gunTip.position, bullet.transform.rotation);
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();

            Vector3 direction = bulletRB.transform.TransformDirection(transform.forward);
            bulletRB.AddForce(transform.forward * bulletSpeed);
            Destroy(newBullet, 5f);

            //reset fireDelayCounter
            fireDelayCounter = 0f;
        }
    }

    private void GunShotAudio()
    {
        float pitchValue = UnityEngine.Random.Range(0.8f, 1.2f);
        audioSource.pitch = pitchValue;

        audioSource.Play();
    }

    private void DecreaseBullet()
    {
        remainingBullet--;
        remainingBulletText.text = remainingBullet.ToString();

        if (remainingBullet <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        audioSource2.Play();

        yield return new WaitForSeconds(reloadTime);

        remainingBullet = bulletCount;
        remainingBulletText.text = remainingBullet.ToString();
    }

    public void ActivateGunInfoCanvas()
    {
        gunInfoCanvas.gameObject.SetActive(true);
    }

    public void DeactivateGunInfoCanvas()
    {
        gunInfoCanvas.gameObject.SetActive(false);
    }

    public void ActivateIsShooting()
    {
        isShooting = true;
    }

    public void DeactivateIsShooting()
    {
        isShooting = false;
    }
}
