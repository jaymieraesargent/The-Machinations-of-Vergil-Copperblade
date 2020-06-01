﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class NetworkLobbyPlayer : NetworkBehaviour
{    
    [Header("UI")]
    [SerializeField] private GameObject lobbyUI = null;
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[12];
    [SerializeField] private TMP_Text[] playerReadyTexts = new TMP_Text[12];
    [SerializeField] private Button startGameButton = null;
    [SerializeField] private TMP_Text gameTitle = null;



    private string PlayerSkillKey = "PlayerSkill";

    [Header("Player")]
    [SyncVar(hook = nameof(HandleDisplayNameChanged))]
    public string DisplayName = "Loading...";
    [SyncVar(hook = nameof(HandleReadyStatusChanged))]
    public bool IsReady = false;

    public SoWeapon selectedWeapon;
    public Quirk selectedQuirk;
    public float skillLevel;
    [SerializeField] private bool isLeader = false;

    public bool IsLeader
    {
        set
        {
            isLeader = value;
            if (startGameButton != null)
            {
                startGameButton.gameObject.SetActive(value);
            }
            else
            {
                Debug.Log("Start Button has not been assigned to network room player");
            }
        }
    }
    private NetworkManagerLobby room;

    private NetworkManagerLobby Room
    {
        get
        {
            if (room != null)
            {
                return room;
            }
            room = NetworkManager.singleton as NetworkManagerLobby;
            return room;
        }
    }

    public override void OnStartAuthority()
    {
        CmdSetDisplayName(SelectionScreen.DisplayName);
        lobbyUI.SetActive(true);
        gameTitle.text = "Game Lobby : " + Room.gameMode;

        //Get skill if played before or set player at skill default level 1 (float)
        if (PlayerPrefs.HasKey(PlayerSkillKey))
            skillLevel = PlayerPrefs.GetFloat(PlayerSkillKey);
        else
            PlayerPrefs.SetFloat(PlayerSkillKey, 1f);

    }

    public override void OnStartClient()
    {
        Room.RoomPlayers.Add(this);
        UpdateDisplay();
    }

    public override void OnNetworkDestroy()
    {
        Room.RoomPlayers.Remove(this);
        UpdateDisplay();
    }

    public void HandleDisplayNameChanged(string oldValue, string newValue)
    {
        UpdateDisplay();
    }

    public void HandleReadyStatusChanged(bool oldValue, bool newValue)
    {
        UpdateDisplay();
    }

    public void HandleGameTypeChanged(bool oldValue, bool newValue)
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (!isLocalPlayer)
        {
            foreach (var player in Room.RoomPlayers)
            {
                if (player.hasAuthority)
                {
                    player.UpdateDisplay();
                    break;
                }
            }

            return;
        }

        for (int i = 0; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = "Waiting for Player...";
            playerReadyTexts[i].text = string.Empty;
        }

        for (int i = 0; i < Room.RoomPlayers.Count; i++)
        {
            playerNameTexts[i].text = Room.RoomPlayers[i].DisplayName + " (" + skillLevel + ")";
            playerReadyTexts[i].text = Room.RoomPlayers[i].IsReady ? "<color=green>Ready</color>" : "<color=red>Not Ready</color>";
        }
    }

    public void HandleReadyToStart(bool readyToStart)
    {
        if (!isLeader)
        {
            return;
        }

        startGameButton.interactable = readyToStart;
    }

    [Command]
    private void CmdSetDisplayName(string displayName)
    {
        DisplayName = displayName;
    }

    [Command]
    public void CmdReadyUp()
    {
        IsReady = !IsReady;
        Room.NotifyPlayersOfReadyState();
    }

    [Command]
    public void CmdStartGame()
    {
        if (connectionToClient != Room.RoomPlayers[0].connectionToClient)
        {
            return;
        }

        //Start Game
        Room.StartGame();
    }
}
