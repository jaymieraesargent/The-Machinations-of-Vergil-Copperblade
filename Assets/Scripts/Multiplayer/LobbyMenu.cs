using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMenu : MonoBehaviour
{

    [SerializeField] private NetworkManagerLobby networkManager = null;
    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;


    public void Start()
    {
        if(networkManager == null)
        {
            Debug.LogError("networkManager not attached to lobby menu");
        }

        if(landingPagePanel == null)
        {
            Debug.LogError("landingPagePanel not attached to lobby menu");
        }
    }

    public void HostLobby(GameObject gameMode)
    {
        networkManager.gameMode = gameMode;
        networkManager.StartHost();

        landingPagePanel.SetActive(false);
    }

}
