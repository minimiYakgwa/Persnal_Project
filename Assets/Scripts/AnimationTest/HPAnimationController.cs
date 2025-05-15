using TMPro;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;

public class HPAnimationController : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public Animator anim;
    public Slider hpBar;


    private void Update()
    {
        UPdateHpText();
        Anim();
        Move();
    }
    private void UPdateHpText()
    {
        hpText.text = ((int)hpBar.value * 100).ToString();
    }

    void Anim()
    {
        if (hpBar.value > 0.5f)
        {
            anim.SetLayerWeight(1, 0);
        }
        else
        {
            anim.SetLayerWeight(1, 1);
        }
    }

    void Move()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
            anim.SetFloat("Blend", 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (anim.GetFloat("Blend") == 1)
            {
                anim.SetFloat("Blend", 0);
            }
            else
            {
                anim.SetFloat("Blend", 1);
            }
        }
    }
}
