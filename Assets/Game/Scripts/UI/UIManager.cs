using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Slider health;
    public Text gameScore;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject quitMenu;
    // Start is called before the first frame update
    void Start()
    {
        gameScore.text = "Score: 0";
        health.maxValue = PlayerController.Instance.playerData.maxHealth;
        health.value = health.maxValue;
        PlayerController.Instance.UpdateScore.AddListener(SetScore);
        PlayerController.Instance.UpdateLives.AddListener(SetLife);
        PlayerController.Instance.playerData.UpdateHealth.AddListener(SetHealth);

    }

    public void SetScore(int score)
    {
        gameScore.text = "Score: " + score;
    }

    public void SetHealth(float currenthealth)
    {
        health.value = currenthealth;
    }

    public void SetLife(int lives)
    {
        if (lives < 3)
        {
            heart3.SetActive(false);
        }

        if (lives < 2)
        {
            heart2.SetActive(false);
        }

        if (lives < 1)
        {
            heart1.SetActive(false);
        }
    }

    public void ShowQuitMenu()
    {
        quitMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideQuitMenu()
    {
        quitMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
