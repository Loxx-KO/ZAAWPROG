using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public CaptureManager captureManager;
    public Animator Animator;

    [Header("Turn Variables")]
    private bool PlayersTurn = false;

    [Header("Buttons")]
    public Button fightButton;
    public Button defendButton;
    public Button healButton;

    [Header("Stats")]
    [SerializeField] public int MaxHP = 100;
    public int currentHP;
    [SerializeField] private int baseDmg = 10;
    private bool ShieldUp = false;
    public int EnemiesDefeated = 0;

    void Awake()
    {
        MaxHP = 100;
        currentHP = MaxHP;
    }

    public void TakeDmg(int dmg)
    {
        Animator.Play("Hurt");
        if (ShieldUp)
        {
            currentHP -= dmg / 2;
        }
        else currentHP -= dmg;

        ShieldPutDown();

        if (currentHP <= 0) 
        {
            currentHP = 0;
            Debug.Log("Player died");
        }
    }

    private void ShieldPutDown()
    {
        ShieldUp = false;
    }

    public void SetPlayerTurn()
    {
        PlayersTurn = true;
        captureManager.ResetGestureRec();
        captureManager.CanTakePhotos = true;
    }

    public void GainPoints()
    {
        EnemiesDefeated++;

        currentHP += 40;
        if (currentHP > MaxHP) currentHP = MaxHP;
    }

    //Animation events
    public void SetAnimationToIdle()
    {
        Animator.Play("Idle");
        PlayersTurn = false;
    }

    public int Attack()
    {
        Animator.Play("Attack");
        ShieldPutDown();
        return baseDmg + UnityEngine.Random.Range(0,1) * UnityEngine.Random.Range(0,10);
    }

    public void Heal()
    {
        Animator.Play("Recover");
        ShieldPutDown();

        currentHP += UnityEngine.Random.Range(0, 20);
        if(currentHP > MaxHP) currentHP = MaxHP;
    }

    public void Defend()
    {
        ShieldUp = true;
    }

    void Update()
    {
        if (PlayersTurn)
        {
            if(captureManager.palmFound || captureManager.fistFound || (captureManager.pointingLeftFound || captureManager.pointingRightFound))
            {
                captureManager.CanTakePhotos = false;
            }

            if (captureManager.palmFound)
            {
                Attack();
                fightButton.gameObject.GetComponent<Animation>().Play("Click_Attack");
            }
            else if (captureManager.fistFound)
            {
                Defend();
                defendButton.gameObject.GetComponent<Animation>().Play("Click_Defence");

            }
            else if ((captureManager.pointingLeftFound || captureManager.pointingRightFound))
            {
                Heal();
                healButton.gameObject.GetComponent<Animation>().Play("Click_Heal");
            }
        }
        else
        {
            FindObjectOfType<EnemyController1>().SetEnemyTurn();
        }
    }
}
