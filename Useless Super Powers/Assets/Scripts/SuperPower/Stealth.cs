using UnityEngine;
using System.Collections;

/// <summary>
/// 隐身超能力：只对目标角色有效  
/// </summary>
public class Stealth : MonoBehaviour
{
    private float castTime;//当前施法时间，也就是玩家“盯”住其他玩家的时间
    private float duration;//当前持续时间
    private float coolDown;//冷却时间
    private RaycastHit hitInfo;//创建射线用以检测玩家是否盯住其他玩家
    private Camera targetPerson;//目标角色的摄像机组件

    void Start()
    {
        castTime = 0;
        duration = 0;
        coolDown = 0;
    }

    void Update()
    {
        if (duration > Time.deltaTime)//如果现在隐身成功且还在隐身时间内，则计算完隐身时间后直接返回
        {
            duration -= Time.deltaTime;
            return;
        }
        else
        {
            //隐身时间结束，恢复原来角色的隐身，并在冷却时间结束前无法再隐身
            if (targetPerson == null) return;
            //使得该角色出现在目标角色的渲染层中
            targetPerson.cullingMask |= (1 << 8);
            targetPerson = null;
        }
        //计算冷却时间，只有当冷却结束后玩家才能再次使用该超能力
        if (coolDown > Time.deltaTime)
        {
            coolDown -= Time.deltaTime;
            return;
        }
        //按下S键开始使用超能力
        if (Input.GetKey(KeyCode.S))
        {
            castTime += Time.deltaTime;
            if (castTime >= 2)
            {
                if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hitInfo, 3, 1 << 10))//盯住其他玩家并且同时按住S键一定时间，则达到超能力施放条件
                {
                    targetPerson = hitInfo.collider.gameObject.GetComponent<Camera>();
                    if (targetPerson == null)
                    {
                        Debug.Log("获取目标人物的相机组件失败！");
                        return;
                    }
                    //将该角色放置在第8层，即专门设置的隐身层
                    this.gameObject.layer = 8;
                    //将该角色从目标角色的渲染层移除，使得只有目标角色无法看见该角色
                    targetPerson.cullingMask = ~(1 << 8);
                }
            }
        }
        else
        {
            castTime = 0;
        }
    }
}
