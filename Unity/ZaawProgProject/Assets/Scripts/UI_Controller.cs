using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [Header("Components")]
    public PlayerController playerController = null;
    public EnemyController1 enemyController = null;
    public CaptureManager CaptureManager;
    public Slider sliderPlayer;
    public Slider sliderEnemy;
    public TMP_Text EnemiesKilledText;
    public TMP_Text Timer;
    public TMP_Text GameOverText;

    [Header("Variables")]
    float currentSliderValPlayer;
    float newSliderValuePlayer;

    float currentSliderValEnemy;
    float newSliderValueEnemy;
    [SerializeField] bool valuesInitialized = false;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        enemyController = FindObjectOfType<EnemyController1>();

        currentSliderValPlayer = playerController.MaxHP;
        currentSliderValEnemy = enemyController.MaxHP;

        newSliderValuePlayer = currentSliderValPlayer;
        newSliderValueEnemy = currentSliderValEnemy;

        sliderPlayer.maxValue = currentSliderValPlayer;
        sliderPlayer.value = currentSliderValPlayer;

        sliderEnemy.maxValue = currentSliderValEnemy;
        sliderEnemy.value = currentSliderValEnemy;

        valuesInitialized = true;
    }

    private void CheckHPs()
    {
        if (playerController.currentHP != newSliderValuePlayer)
        {
            newSliderValuePlayer = playerController.currentHP;
        }

        if (enemyController.currentHP != newSliderValueEnemy)
        {
            newSliderValueEnemy = enemyController.currentHP;
        }
    }

    private void UpdatePlayerSlider()
    {
        if (currentSliderValPlayer > newSliderValuePlayer)
        {
            currentSliderValPlayer -= Time.deltaTime * newSliderValuePlayer;
            if (currentSliderValPlayer - newSliderValuePlayer < 0.5)
            {
                currentSliderValPlayer = newSliderValuePlayer;
            }
        }
        else if (currentSliderValPlayer < newSliderValuePlayer)
        {
            currentSliderValPlayer += Time.deltaTime * newSliderValuePlayer;
            if (newSliderValuePlayer - currentSliderValPlayer < 0.5)
            {
                currentSliderValPlayer = newSliderValuePlayer;
            }
        }

        sliderPlayer.value = currentSliderValPlayer;
    }

    private void UpdateEnemySlider()
    {
        if (currentSliderValEnemy > newSliderValueEnemy)
        {
            currentSliderValEnemy -= Time.deltaTime * newSliderValuePlayer;
            if (currentSliderValEnemy - newSliderValueEnemy < 0.5)
            {
                currentSliderValEnemy = newSliderValueEnemy;
            }
        }
        else if (currentSliderValEnemy < newSliderValueEnemy)
        {
            currentSliderValEnemy += Time.deltaTime * newSliderValuePlayer;
            if (newSliderValueEnemy - currentSliderValEnemy < 0.5)
            {
                currentSliderValEnemy = newSliderValueEnemy;
            }
        }

        if (sliderEnemy.maxValue != enemyController.MaxHP) { sliderEnemy.maxValue = enemyController.MaxHP; }

        sliderEnemy.value = currentSliderValEnemy;
    }

    private void SetKillCount()
    {
        EnemiesKilledText.text = "Killed: " + playerController.EnemiesDefeated.ToString();
    }

    private void Update()
    {
        if (!GameVariables.GameEnded)
        {
            if (valuesInitialized)
            {
                CheckHPs();
                UpdatePlayerSlider();
                UpdateEnemySlider();
                SetKillCount();
            }

            Timer.text = "Timer: " + CaptureManager.CurrentTime.ToString();
        }

        if(GameVariables.GameEnded && !GameOverText.enabled)
        {
            GameOverText.enabled = true;
        }
    }
}
