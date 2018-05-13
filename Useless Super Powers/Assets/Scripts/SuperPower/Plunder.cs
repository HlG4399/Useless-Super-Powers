using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// 掠夺超能力：可以暂时夺取一个其他玩家的超能力
/// </summary>
public class Plunder : NetworkBehaviour
{
    private float castTime;//当前施法时间，也就是玩家“盯”住其他玩家的时间
    private float duration;//当前持续时间
    private float coolDown;//冷却时间
    private RaycastHit hitInfo;//创建射线用以检测玩家是否盯住其他玩家
    public Character_Animations controller;//玩家动画机

    void Start()
    {
        castTime = 0;
        duration = 0;
        coolDown = 0;
    }

    void Update()
    {
        if (duration > Time.deltaTime)//如果现在控制成功且还在控制时间内，则计算完控制时间后直接返回
        {
            duration -= Time.deltaTime;
            return;
        }
        else
        {
            //控制时间结束，恢复原来角色的控制，并在冷却时间结束前无法再控制其他玩家
            controller.enabled = true;
            if(hitInfo.collider!=null)
            {
                hitInfo.collider.GetComponent<Character_Animations>().enabled = false;
            }
            CamPos.follow = this.transform;
        }
        //计算冷却时间，只有当冷却结束后玩家才能再次使用该超能力
        if (coolDown > Time.deltaTime)
        {
            coolDown -= Time.deltaTime;
            return;
        }
        //按下P键开始使用超能力
        if (Input.GetKey(KeyCode.P))
        {
            castTime += Time.deltaTime;
            if (castTime >= 2)
            {
                if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hitInfo, 3, 1 << 10))//盯住其他玩家并且同时按住P键一定时间，则达到超能力施放条件
                {
                    controller.enabled = false;//失去自身角色控制
                    //取得其他玩家控制权
                    hitInfo.collider.GetComponent<Character_Animations>().enabled = true;
                    CamPos.follow = hitInfo.collider.transform;
                    //刷新当前持续时间与冷却时间
                    duration = 5;
                    coolDown = 30;
                }
            }
        }
        else
        {
            castTime = 0;
        }
    }
}
