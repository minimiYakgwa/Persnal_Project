using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator anim;

    public float temp = 1;
    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.5f)
        {
            if (temp >= 0) temp -= Time.deltaTime;
            anim.SetLayerWeight(1, temp);
        }
    }
}
