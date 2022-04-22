using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHud : MonoBehaviour
{
    private void Start()
    {
        LevelManager.Instance.Victory.AddListener(OnGameEnd);
        LevelManager.Instance.GameOver.AddListener(OnGameEnd);
    }
    public void OnGameEnd()
    {
        gameObject.SetActive(false);
    }
}
