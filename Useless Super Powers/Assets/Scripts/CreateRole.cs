using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CreateRole : NetworkManager
{
    public GameObject[] players;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = (GameObject)GameObject.Instantiate(players[ChooseRole.RoleID - 1], new Vector3(0, 0, 0), Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        ClientScene.AddPlayer(conn, (short)ChooseRole.RoleID);
        //玩家物体在网络HLAPI中有点特殊，NetworkManager对玩家物体的处理流程是：
        //将包含NetworkIdentity组件的预设体注册为派生预设体
        //客户端连接到服务器
        //客户端调用AddPlayer(), 发送MsgType.AddPlayer类型的消息给服务器
        //服务器接收到消息，调用NetworkManager.AddPlayer()
        //服务器从预设体中创建物体
        //服务器上为新创建的玩家物体调用NetworkServer.AddPlayerForConnection()
        //玩家物体被创建出来（你不需要再调用NetworkManager.Spawn()）
        //一个MsgType.Owner类型的消息发送给客户端（只发送给这个连接进来的客户端）
        //客户端接收到消息
        //客户端上调用OnStartLocalPlayer，并且将isLocalPlayer设置为True
        //要注意的是，OnStartLocalPlayer()会在OnStartClient()之后被调用，这发生在玩家物体被创建，并接收到Owner消息的时候。所以在OnStartClient函数中，isLocalPlayer属性还没有被设置。
    }
}
