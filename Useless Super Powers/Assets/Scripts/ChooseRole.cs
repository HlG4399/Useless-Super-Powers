using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

/// <summary>
/// 选择角色
/// </summary>
public class ChooseRole : NetworkBehaviour
{
    public static int RoleID = 1;//利用角色ID来d区分玩家选择的角色
    public GameObject Canvas;

    /// <summary>
    /// 玩家在选择角色时，与其交互的UI组件会调用这个函数
    /// </summary>
    /// <param name="id"></param>
    public void SetID(int id)
    {
        RoleID = id;
        CmdSetID(id);
        Canvas.SetActive(false);
    }

    [Command]
    public void CmdSetID(int id)
    {
        ChooseRole.RoleID = id;
    }
}
