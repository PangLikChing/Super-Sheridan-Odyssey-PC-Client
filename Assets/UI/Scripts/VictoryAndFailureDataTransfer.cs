using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryAndFailureDataTransfer : MonoBehaviour
{

    public Text gameScore;

    // Start is called before the first frame update
    void Start()
    {
        gameScore.text = "Score: " + PlayerController.Instance.numOfCoinsHeld;
    }

}
