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

    [Header("Buttons animators")]
    public Animator fightButtonAnim;
    public Animator defendButtonAnim;
    public Animator healButtonAnim;

    [Header("Stats")]
    [SerializeField] public int MaxHP = 100;
    public int currentHP;
    [SerializeField] private int baseDmg = 20;
    private bool ShieldUp = false;
    public int EnemiesDefeated = 0;

    [Header("Particles")]
    public ParticleSystem DefendParticles;
    public ParticleSystem HealParticles;

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
            Animator.Play("Death");
            GameVariables.GameEnded = true;
        }
    }

    private void ShieldPutDown()
    {
        ShieldUp = false;
        StopDefendParticles();
    }

    public void SetPlayerTurn()
    {
        GameVariables.PlayerTurn = true;
        captureManager.CanTakePhotos = true;
    }

    public void GainPoints()
    {
        EnemiesDefeated++;

        currentHP += 40;
        if (currentHP > MaxHP) currentHP = MaxHP;
    }

    public int Attack()
    {
        Animator.Play("Attack");
        ShieldPutDown();
        return baseDmg + UnityEngine.Random.Range(0,1) * UnityEngine.Random.Range(1,10);
    }

    public void Heal()
    {
        Animator.Play("Recover");
        ShieldPutDown();
        PlayHealParticles();

        currentHP += UnityEngine.Random.Range(0, 20);
        if(currentHP > MaxHP) currentHP = MaxHP;
    }

    public void Defend()
    {
        ShieldUp = true;
        PlayDefendParticles();
    }

    //Animation events
    public void SetAnimationToIdle()
    {
        Animator.Play("Idle");
    }

    public void PlayDefendParticles()
    {
        DefendParticles.Play();
    }

    public void StopDefendParticles()
    {
        DefendParticles.Stop();
    }

    public void PlayHealParticles()
    {
        HealParticles.Play();
    }

    void Update()
    {
        if (GameVariables.PlayerTurn && !GameVariables.GameEnded)
        {
            if(captureManager.palmFound || captureManager.fistFound || (captureManager.pointingLeftFound || captureManager.pointingRightFound))
            {
                if (captureManager.palmFound)
                {
                    Debug.Log("Attack!");
                    fightButtonAnim.Play("Click");
                    FindObjectOfType<EnemyController1>().TakeDmg(Attack());
                }
                else if (captureManager.fistFound)
                {
                    Debug.Log("Def!");
                    defendButtonAnim.Play("Click");
                    Defend();
                }
                else if ((captureManager.pointingLeftFound || captureManager.pointingRightFound))
                {
                    Debug.Log("Heal!");
                    healButtonAnim.Play("Click");
                    Heal();
                }

                captureManager.CanTakePhotos = false;
                captureManager.ResetGestureRec();
                GameVariables.PlayerTurn = false;
            }
        }
    }
}
