using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowVictory : MonoBehaviour
{
    private void Start()
    {
        LevelManager.Instance.Victory.AddListener(OnVictory);
    }
    public void OnVictory()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("Game Won");
    }
}
