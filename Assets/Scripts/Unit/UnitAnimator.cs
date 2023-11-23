using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private Transform bulletProjectilePrefab;
    [SerializeField] private Transform shootPointTransform;
    [SerializeField] private Transform rifleTransform;
    [SerializeField] private Transform swordTransform;

    private void Awake()
    {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving += MoveAction_OnStopMoving;
        };

        if (TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.OnShoot+= MoveAction_OnShoot;
        };
        
        if (TryGetComponent<SwordAction>(out SwordAction swordAction))
        {
            swordAction.OnSwordActionStarted += SwordAction_OnSwordActionStarted;
            swordAction.OnSwordActionCompleted += SwordAction_OnSwordActionCompleted;
        };
    }

    private void Start()
    {
        EquipRifle();
    }

    private void SwordAction_OnSwordActionCompleted(object sender, EventArgs e)
    {
        EquipRifle();
    }

    private void SwordAction_OnSwordActionStarted(object sender, EventArgs e)
    {
        EquipSword();
        unitAnimator.SetTrigger("SwordSlash");
    }

    private void MoveAction_OnStartMoving(object sender, EventArgs e)
    {
        unitAnimator.SetBool("isWalking", true);
    }

    private void MoveAction_OnStopMoving(object sender, EventArgs e)
    {
        unitAnimator.SetBool("isWalking", false);
    }

    private void MoveAction_OnShoot(object sender, ShootAction.OnShootEventArgs e)
    {
        unitAnimator.SetTrigger("Shoot");

        Transform bulletProjectileTransform = Instantiate(bulletProjectilePrefab, shootPointTransform.position, Quaternion.identity);
        BulletProjectile bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();

        Vector3 targetUnitAtPointPosition = e.targetUnit.GetWorldPosition();
        targetUnitAtPointPosition.y = shootPointTransform.position.y;

        bulletProjectile.Setup(targetUnitAtPointPosition);
    }

    private void EquipSword()
    {
        swordTransform.gameObject.SetActive(true);
        rifleTransform.gameObject.SetActive(false);
    }    

    private void EquipRifle()
    {
        swordTransform.gameObject.SetActive(false);
        rifleTransform.gameObject.SetActive(true);
    }
}
