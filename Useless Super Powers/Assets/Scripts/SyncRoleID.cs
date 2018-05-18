﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncRoleID : NetworkBehaviour
{
    [Command]
    public void CmdSetID(int id)
    {
        Debug.Log("isServer:"+isServer);
        ChooseRole.RoleID = id;
        Debug.Log("服务器端" + ChooseRole.RoleID);
    }
}
