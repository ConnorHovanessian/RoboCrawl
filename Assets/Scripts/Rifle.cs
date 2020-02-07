using UnityEngine;

public class Rifle : MonoBehaviour
{
    public float damage = 30f;
    public float range = 200f;
    public float impactForce = 90f;
    public float fireRate = 10f;

    public bool automatic = true;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && automatic == true)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        else if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && automatic == false)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null) enemy.takeDamage(damage);

            if (hit.rigidbody != null) hit.rigidbody.AddForce(-hit.normal * impactForce);

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, .2f);
        }


    }
}
