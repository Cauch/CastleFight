using Prototype.NetworkLobby;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook {
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayerGO, GameObject gamePlayerGO)
    {
        LobbyPlayer lobby = lobbyPlayerGO.GetComponent<LobbyPlayer>();
        PlayerConnection playerConnection = gamePlayerGO.GetComponent<PlayerConnection>();

        playerConnection.BuilderId = lobby.playerBuilderId;
        playerConnection.Allegiance = lobby.playerAllegiance;
        playerConnection.Name = lobby.playerName;
        playerConnection.Seed = lobby.Seed;
    }

}
