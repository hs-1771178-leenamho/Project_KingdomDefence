using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float towerRange = 15f;
    [SerializeField] ParticleSystem towerParticle;
    [SerializeField] AudioSource attackSound;
    Transform target;

    // Update is called once per frame
    void Update()
    {
        FindClosesetTarget();
        AimWeapon();
    }

    void FindClosesetTarget(){
        Enemy[] eneimes = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in eneimes){
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if(maxDistance > targetDistance){
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }

        }

        target = closestTarget;

    }
    void AimWeapon(){

        float targetDistance = Vector3.Distance(transform.position, target.position);

        weapon.LookAt(target);

        if(targetDistance <= towerRange) Attack(true);
        else Attack(false);
    }

    void Attack(bool isActive){
        var emissionModule = towerParticle.emission;
        if(Time.timeScale == 0){
            emissionModule.enabled = false;
            attackSound.enabled = false;
        }
        emissionModule.enabled = isActive;
        attackSound.enabled = isActive;

    }
}
