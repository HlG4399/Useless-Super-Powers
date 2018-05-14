using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum CharacterState
{
    lobby,
    idle,
    gaming,
    lose
}

public class HallManager : MonoBehaviour
{
    private CharacterState state = CharacterState.lobby;
    private string IP = "10.0.0.39";
    private int Port = 1000;
    private Rigidbody myChar;

    void OnGUI()
    {
        switch (state)
        {
            case CharacterState.lobby:
                OnLobby();
                break;
            case CharacterState.idle:
                OnIdle();
                break;
            case CharacterState.gaming:
                OnGaming();
                break;
            case CharacterState.lose:
                OnLose();
                break;
        }
    }

    //大厅逻辑状态
    void OnLobby()
    {
        if (GUILayout.Button("建立服务器"))
        {
            NetworkConnectionError error = Network.InitializeServer(10, Port, false);
            if (error == NetworkConnectionError.NoError)
            {
                state = CharacterState.idle;
            }
        }
        GUILayout.Label("=============================================================");
        GUILayout.BeginHorizontal();
        GUILayout.Label("服务器IP地址：");
        IP = GUILayout.TextField(IP);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("连接服务器"))
        {
            NetworkConnectionError error = Network.Connect(IP, Port);
            if (error == NetworkConnectionError.NoError)
            {
                state = CharacterState.idle;
            }
        }
    }

    //空闲状态逻辑
    void OnIdle()
    {
        if (GUILayout.Button("Start!"))
        {
            StartGaming();
        }
    }

    //游戏状态逻辑
    void OnGaming()
    {
        //控制自己的球体
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        myChar.AddForce(new Vector3(x, 0, y));

        //出界则失败
        if (myChar.transform.position.y < -5)
        {
            Network.Destroy(myChar.GetComponent<NetworkView>().viewID);
            
            state = CharacterState.lose;
        }
    }

    //失败状态逻辑
    void OnLose()
    {
        //此处需要一个失败界面图
        GUILayout.Label("You Lose!");
        if (GUILayout.Button("Start Again!"))
        {
            StartGaming();
        }
    }

    //开始游戏
    void StartGaming()
    {
        state = CharacterState.gaming;
        SceneManager.LoadScene(1);
    }
}
