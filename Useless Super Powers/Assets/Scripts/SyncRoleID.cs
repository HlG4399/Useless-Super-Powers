using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncRoleID : NetworkManager
{
    public GameObject[] players;

    void Start()
    {
        ClientScene.AddPlayer(CreateRole.connection, (short)ChooseRole.RoleID);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = (GameObject)GameObject.Instantiate(players[ChooseRole.RoleID - 1], new Vector3(0, 0, 0), Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
