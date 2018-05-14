using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CreateRole : NetworkManager
{
    public GameObject[] players;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        NetworkServer.AddPlayerForConnection(conn, players[ChooseRole.RoleID - 1], playerControllerId);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        ClientScene.AddPlayer(conn, (short)ChooseRole.RoleID);
    }
}
