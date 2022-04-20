using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadTerrain()
    {
        Debug.Log("Terrain scene loading");
        SceneManager.LoadScene("SampleScene");
    }
}
