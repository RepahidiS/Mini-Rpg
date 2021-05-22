using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAnimStates
{
    Idle,
    Walk,
    Attack1,
    Attack2
}

public abstract class Enemy : MonoBehaviour
{
    public Spawner spawner { get; set; }
    public string enemyName { get; set; }
    public int currentHp { get; set; }
    public int maxHp { get; set; }
    public int attack { get; set; }
    public float attackInterval { get; set; }
    public int defense { get; set; }
    public float movementSpeed { get; set; }
    public float rotationSpeed { get; set; }
    public Vector3 spawnCenter { get; set; }
    public float moveCooldown { get; set; }
    public float maxMoveInternal { get; set; }
    public Vector3 nextPos { get; set; }
    public float attackableRange { get; set; }
    public float maxTrackingRange { get; set; }
    public bool isTrackingPlayer { get; set; }

    public Animator animator;
    public EnemyAnimStates animState;

    public abstract void ApplyDamage(int pureDamage);
    public abstract void DealDamage();

    public int GetHpPercent()
    {
        return (currentHp * 100) / maxHp;
    }

    public bool MoveToNextPos()
    {
        if(Vector3.Distance(transform.position, nextPos) <= 2.0f)
            return true;

        Quaternion tr = Quaternion.LookRotation(nextPos - transform.position);
        Vector3 eulerRotation = tr.eulerAngles;
        eulerRotation.x = 0.0f;
        eulerRotation.z = 0.0f;
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(eulerRotation), Time.deltaTime * rotationSpeed);
        transform.rotation = targetRotation;

        transform.position += transform.forward * movementSpeed * Time.deltaTime;

        return false;
    }

    public bool MoveToPlayer(Vector3 playerPos)
    {
        Quaternion tr = Quaternion.LookRotation(playerPos - transform.position);
        Vector3 eulerRotation = tr.eulerAngles;
        eulerRotation.x = 0.0f;
        eulerRotation.z = 0.0f;
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(eulerRotation), Time.deltaTime * rotationSpeed);
        transform.rotation = targetRotation;

        if (Vector3.Distance(transform.position, playerPos) <= attackableRange)
            return true;

        transform.position += transform.forward * movementSpeed * Time.deltaTime;

        return false;
    }

    public bool IsOutOfTrackingRange()
    {
        return Vector3.Distance(transform.position, spawnCenter) >= maxTrackingRange;
    }
}