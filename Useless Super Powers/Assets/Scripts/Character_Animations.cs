using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Character_Animations : NetworkBehaviour
{
    private Animator animator;//控制玩家动作的动画切换
    private float v, run;//玩家行走速度与冲刺速度
    public static float h;//控制玩家转动角度
    //public Material[] Character_Material;//存储玩家角色的材质，根据玩家选择角色的不同自动设置其材质球

    void Start()
    {
        CamPos.follow = this.transform;
        animator = this.GetComponent<Animator>();
    }

    /// <summary>
    /// 控制玩家行动
    /// </summary>
    void Update()
    {
        if (!isLocalPlayer) return;
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        Sprinting();
        //if (animator.GetFloat("Run") == 0.2)
        //{
        //    if (Input.GetKeyDown("space"))
        //    {
        //        animator.SetBool("Jump", true);
        //    }
        //}
    }

    /// <summary>
    /// 根据玩家状态的不同播放相应的动画
    /// </summary>
    void FixedUpdate()
    {
        animator.SetFloat("Walk", v);
        animator.SetFloat("Run", run);
        animator.SetFloat("Turn", h * 0.35f);
        if (h != 0 && v == 0)
        {
            this.gameObject.transform.Rotate(0, h * 0.9f, 0);
        }
    }

    /// <summary>
    /// 控制玩家冲刺
    /// </summary>
    void Sprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift))//同时按住左shift键可以进入冲刺状态
        {
            run = 0.2f;
        }
        else
        {
            run = 0.0f;
        }
    }

    /// <summary>
    /// 在客户端上调用该函数
    /// </summary>
    public override void OnStartLocalPlayer()//NetworkBehaviour自带的虚方法重写
    {
        ////根据RoleID自动设置角色的技能
        //switch (ChooseRole.RoleID)
        //{
        //    case 1:
        //        GetComponent<Plunder>().enabled = true;
        //        break;
        //    case 2:
        //        GetComponent<Stealth>().enabled = true;
        //        break;
        //    case 3:
        //        GetComponent<Teleport>().enabled = true;
        //        break;
        //}
        ////根据RoleID自动设置角色的材质球
        //foreach (Transform child in this.transform)
        //{
        //    child.gameObject.GetComponent<SkinnedMeshRenderer>().material = Character_Material[ChooseRole.RoleID - 1];
        //    break;
        //}
    }
}
