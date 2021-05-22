using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spider : Enemy
{
    Player player;

    private void Start()
    {
        enemyName = "Spider";
        currentHp = 500;
        maxHp = 500;
        attack = 10;
        attackInterval = 2.0f;
        defense = 20;
        movementSpeed = 3.0f;
        rotationSpeed = 15.0f;
        maxMoveInternal = 4.0f;
        moveCooldown = Time.time + Random.Range(1.0f, maxMoveInternal);
        nextPos = spawner.getRandomPosInRange();
        attackableRange = 3.0f;
        maxTrackingRange = 20.0f;
        isTrackingPlayer = false;
        animState = EnemyAnimStates.Idle;

        Canvas canvas = GetComponentInChildren<Canvas>();
        if(canvas == null)
        {
            Debug.LogError(name + " need to have name canvas.");
            Debug.Break();
        }

        TextMeshProUGUI txtName = canvas.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        if(txtName == null)
        {
            Debug.LogError(name + " need to have name text component.");
            Debug.Break();
        }

        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogError(name + " need to have animator.");
            Debug.Break();
        }

        txtName.text = enemyName;
    }

    private void FixedUpdate()
    {
        animState = EnemyAnimStates.Idle;

        if(isTrackingPlayer)
        {
            if(MoveToPlayer(player.transform.position)) // in attackable range
            {
                // TODO : attack
                animState = EnemyAnimStates.Attack1;
            }else animState = EnemyAnimStates.Walk;

            if(IsOutOfTrackingRange())
            {
                isTrackingPlayer = false;
                nextPos = spawner.getRandomPosInRange();
                moveCooldown = 0;
            }
        }
        else if(Time.time >= moveCooldown)
        {
            if(MoveToNextPos()) // movement is done
            {
                moveCooldown = Time.time + Random.Range(1.0f, maxMoveInternal);
                nextPos = spawner.getRandomPosInRange();
            }else animState = EnemyAnimStates.Walk;
        }

        animator.SetInteger("AnimState", (int)animState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isTrackingPlayer = true;
            player = other.GetComponent<Player>();
        }
    }

    public override void ApplyDamage(int pureDamage)
    {
        throw new System.NotImplementedException();
    }

    public override void DealDamage()
    {
        GameManager.instance.player.ApplyDamage(attack);
    }
}