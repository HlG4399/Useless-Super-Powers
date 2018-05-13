using UnityEngine;
using System.Collections;

/// <summary>
/// 隐身超能力：只对目标玩家有效  
/// </summary>
public class Stealth : MonoBehaviour
{
    //public Stealth stealth;
    private Camera targetPerson;

    void OnEnable()
    {
        if (targetPerson==null)
        {
            Debug.Log("获取目标人物的相机组件失败！");
            return;
        }
        //将该角色放置在第8层，即专门设置的隐身层
        this.gameObject.layer = 8;
        //将该角色从目标角色的渲染层移除，使得只有目标角色无法看见该角色
        targetPerson.cullingMask = ~(1<<8);
    }

    void OnDisable()
    {
        if (targetPerson == null)
        {
            Debug.Log("获取目标人物的相机组件失败！");
            return;
        }
        //使得该角色出现在目标角色的渲染层中
        targetPerson.cullingMask |= (1 << 8);
    }

    /// <summary>
    /// 获取目标角色的相机组件
    /// </summary>
    /// <param name="targetPerson"></param>
    void set_targetPerson(Camera targetPerson)
    {
        this.targetPerson = targetPerson;
    }
}
