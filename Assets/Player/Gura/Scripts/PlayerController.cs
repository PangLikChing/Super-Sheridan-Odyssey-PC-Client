using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;
using Cinemachine;
using Photon.Pun;

public class PlayerController : Singleton<PlayerController>
{
    public InputHandler inputHandler;
    public PlayableDirector playableDirector;

    public int numOfCoinsHeld;
    public int livesNum;

    [HideInInspector]
    public GameObject playerAvatar;
    [HideInInspector]
    public CharacterLocomotion characterLocomotion;
    [HideInInspector]
    public CharacterAttack characterAttack;
    [HideInInspector]
    public Animator characterAnimation;
    [HideInInspector]
    public PlayerData playerData;
    [HideInInspector]
    public PlayerCameraControl cameraControl;


    [HideInInspector]
    public UnityEvent Spawn;
    [HideInInspector]
    public UnityEvent<int> UpdateScore;
    [HideInInspector]
    public UnityEvent<int> UpdateLives;

    [HideInInspector]
    public float respawnDelay = 5f;
    [HideInInspector]
    public float respawnTimer;
    [HideInInspector]
    public Transform localSpawnPoint;
    [HideInInspector]
    public CinemachineStateDrivenCamera stateCamera;


    private bool respawnCountDown;
    private void Awake()
    {
        GetComponent<FSM_Q>().enabled = false;
        GetComponent<Animator>().enabled = false;
    }

    private void OnEnable()
    {
        //avatar configuration
        characterLocomotion = playerAvatar.GetComponent<CharacterLocomotion>();
        characterAttack = playerAvatar.GetComponent<CharacterAttack>();
        characterAnimation = playerAvatar.GetComponent<Animator>();
        playerData = playerAvatar.GetComponent<PlayerData>();
        cameraControl = playerAvatar.transform.GetChild(0).GetComponent<PlayerCameraControl>();
        stateCamera = GetComponent<CinemachineStateDrivenCamera>();
        GetComponent<Animator>().enabled = true;
        GetComponent<FSM_Q>().enabled = true;

        //camera configuration
        Transform cameraRoot = playerAvatar.transform.GetChild(0);
        stateCamera.m_Instructions[0].m_VirtualCamera = stateCamera.ChildCameras[LevelManager.Instance.playerIndex];
        stateCamera.LookAt = cameraRoot;
        stateCamera.ChildCameras[2].Follow = cameraRoot;
        stateCamera.ChildCameras[3].Follow = cameraRoot;

        respawnTimer = 0;

        playerData.PlayerDefeat.AddListener(OnPlayerDefeat);
    }

    private void Update()
    {
        Respawn();
    }

    private void Respawn()
    {
        if (respawnCountDown)
        {
            respawnTimer -= Time.deltaTime;
        }

        if (respawnTimer <= 0)
        {
            Spawn.Invoke();
            characterLocomotion.transform.position = localSpawnPoint.position + Vector3.up * 0.05f;
            characterLocomotion.transform.rotation = localSpawnPoint.rotation;
            respawnTimer = respawnDelay;
            respawnCountDown = false;
        }
    }

    public void OnPlayerDefeat()
    {
        livesNum--;
        UpdateLives.Invoke(livesNum);
        if (livesNum > 0)
        {
            respawnCountDown = true;
        }
        else
        {
            playerData.PV.RPC("playerExhausted",RpcTarget.All);
            //playerData.PlayerExhausted.Invoke();
        }
    }

    public void KeepScore(int newCoinToBeAdded)
    {
        numOfCoinsHeld = numOfCoinsHeld + newCoinToBeAdded;
        UpdateScore.Invoke(numOfCoinsHeld);
    }

}
