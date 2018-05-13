using UnityEngine;
using System.Collections;

/// <summary>
/// 高速移动超能力：只有当碰到障碍物时才会停下来
/// </summary>
public class Teleport : MonoBehaviour
{
    private float castTime;//当前施法时间，也就是玩家按住T键的时间
    private float coolDown;//冷却时间
    private RaycastHit hitInfo;//创建射线用以检测玩家是否碰到其它障碍物
    private Animator animator;//玩家动画机
    private bool isStart;//判断角色是否开始使用超能力

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //计算冷却时间，只有当冷却结束后玩家才能再次使用该超能力
        if (coolDown > Time.deltaTime)
        {
            coolDown -= Time.deltaTime;
            return;
        }
        if(isStart)
        {
            //角色开始向前高速移动
            this.transform.position += transform.forward * Time.deltaTime * 17;
            //或者使用this.transform.Translate(Vector3.forward);

            //高度移动过程当中自动播放冲刺动画
            animator.SetFloat("Walk", 1);
            animator.SetFloat("Run", 0.2f);
            //当角色碰到障碍物即停止
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 3, 1 << 9))
            {
                animator.SetFloat("Walk", 0);
                animator.SetFloat("Run", 0);
                //刷新冷却时间
                coolDown = 30;
                //结束超能力施放
                isStart = false;
            }
            return;
        }
        //按下T键开始使用超能力
        if (Input.GetKey(KeyCode.T))
        {
            castTime += Time.deltaTime;
            if (castTime >= 2)
            {
                isStart = true;
            }
        }
        else
        {
            castTime = 0;
        }
    }
}
