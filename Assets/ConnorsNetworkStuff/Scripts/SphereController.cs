using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereController : MonoBehaviour
{
 
    void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene("SampleScene");
    }
};
