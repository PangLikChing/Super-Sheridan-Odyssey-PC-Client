using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class SceneSetupController : MonoBehaviour
{
    GameObject i = null;
   // GameObject j = null;

    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = false;
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        //  if (PhotonNetwork.IsMasterClient)
        //  {
        Debug.Log("Creating Player");
        i = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"),
                                  Vector3.zero, Quaternion.identity);
        //j = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerNickName"),
        //                          Vector3.zero, Quaternion.identity);
        //j.transform.position = new Vector3(0.0f, 5.0f, 0.0f);

        // }
    }

    private void Update()
    {
        //  if (i != null)
        //  {
        //i.transform.position = new Vector3(35.78f, 209.3f, -143.3f);
        i.transform.Translate(new Vector3(0.0f, 0.0f, 0.0f));
       // j.transform.Translate(new Vector3(0.0f, 0.0f, 0.0f));
        // }
    }
}
