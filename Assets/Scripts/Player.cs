using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimState
{
    Idle,
    Walk,
    WalkBack,
    Run
}

public class Player : MonoBehaviour
{
    public int currentHp;
    public int maxHp;
    public int currentMp;
    public int maxMp;
    public int level;
    public int exp; // TODO : leveller için exp sınırı belirlenecek
    public int attack;
    public int defence;

    public AnimState animState;
    public float walkSpeed;
    public float runSpeed;
    public float rotationSpeed;

    Vector3 moveDir;
    float currentRotate;
    Animator animator;

    private void Start()
    {
        animState = AnimState.Idle;
        moveDir = Vector3.zero;
        currentRotate = 0.0f;
        animator = GetComponent<Animator>();

        GameManager.instance.uiManager.uiStatusBar.UpdateHp(currentHp, maxHp);
        GameManager.instance.uiManager.uiStatusBar.UpdateMp(currentMp, maxMp);
        GameManager.instance.uiManager.uiStatusBar.UpdateExp(0, 100); // TODO
        GameManager.instance.uiManager.uiStatusBar.UpdateLevel(level);
    }

    private void Update()
    {
        animState = AnimState.Idle;
        moveDir = Vector3.zero;
        currentRotate = 0.0f;

        if (Input.GetKey(KeyCode.W))
        {
            float currentMovementSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
            moveDir = transform.forward * currentMovementSpeed * Time.deltaTime;
            animState = currentMovementSpeed == runSpeed ? AnimState.Run : AnimState.Walk;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDir = -transform.forward * walkSpeed * Time.deltaTime;
            animState = AnimState.WalkBack;
        }

        if(Input.GetKey(KeyCode.A))
            currentRotate = -rotationSpeed;

        if (Input.GetKey(KeyCode.D))
            currentRotate = rotationSpeed;

        animator.SetInteger("AnimState", (int)animState);
    }

    private void FixedUpdate()
    {
        if(currentRotate != 0.0f)
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot.y += currentRotate * Time.deltaTime;
            transform.rotation = Quaternion.Euler(rot);
        }

        transform.position += moveDir;
    }

    public void ApplyDamage(int pureDamage)
    {
        // TODO : defans ile ilgili hesaplamalar vs yapılacak
        currentHp -= pureDamage;
        GameManager.instance.uiManager.uiStatusBar.UpdateHp(currentHp, maxHp);
    }
}