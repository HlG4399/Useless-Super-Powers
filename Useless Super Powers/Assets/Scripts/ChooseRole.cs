using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 选择角色
/// </summary>
public class ChooseRole : MonoBehaviour
{
    public static int RoleID = 1;//利用角色ID来区分玩家选择的角色

    /// <summary>
    /// 玩家在选择角色时，与其交互的UI组件会调用这个函数
    /// </summary>
    /// <param name="id"></param>
    public void SetID(int id)
    {
        RoleID = id;
        SceneManager.LoadScene("Useless Super Powers");
    }
}
