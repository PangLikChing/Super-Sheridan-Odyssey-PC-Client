using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLost : MonoBehaviour
{
    private void Start()
    {
        LevelManager.Instance.GameOver.AddListener(OnGameOver);
    }
    public void OnGameOver()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("Game Lost");
    }
}
