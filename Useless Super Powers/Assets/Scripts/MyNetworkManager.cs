using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;


public class MyNetworkManager : NetworkManager
{
    private string ipAddress;
    public int chosenCharacter = 0;
    public GameObject[] players;
    public GameObject Canvas;

    //subclass for sending network messages
    public class NetworkMessage : MessageBase
    {
        public int chosenClass;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        int selectedClass = message.chosenClass;
        Debug.Log("server add with message " + selectedClass);

        GameObject player = (GameObject)GameObject.Instantiate(players[selectedClass], new Vector3(0, 0, 0), Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        //if (selectedClass == 0)
        //{
        //    GameObject player = Instantiate(Resources.Load("Player")) as GameObject;
        //    NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        //}


        //if (selectedClass == 1)
        //{
        //    GameObject player = Instantiate(Resources.Load("Car")) as GameObject;
        //    NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        //}
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        NetworkMessage test = new NetworkMessage();
        test.chosenClass = chosenCharacter;

        ClientScene.AddPlayer(conn, 0, test);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //base.OnClientSceneChanged(conn);
    }

    public void btn1()
    {
        chosenCharacter = 0;
        Canvas.SetActive(false);
    }

    public void btn2()
    {
        chosenCharacter = 1;
        Canvas.SetActive(false);
    }
}