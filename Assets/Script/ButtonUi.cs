using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUi : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public int click = 0;
    public bool isFusion;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isFusion", isFusion);
    }
    
    public void PlayAnimationInventory()
    {
        click++;
        if (click % 2 == 0)
        {
            animator.SetBool("isInventory", false);
        //    if (isFusion == true)
        //     {
        //         FusionManager.Instance.isFusion = false;
        //         Debug.Log(FusionManager.Instance.isFusion);
        //     }
        }
        else
        {
            animator.SetBool("isInventory", true);
        }
    }

    public void PlayAnimationBook()
    {
        click++;
        if (click % 2 == 0)
        {
            animator.SetBool("isBook", false);
        }
        else
        {
            animator.SetBool("isBook", true);
        }
    }

    public void PlayAnimationClock()
    {
        click++;
        if (click % 2 == 0)
        {
            animator.SetBool("isClock", false);
        }
        else
        {
            animator.SetBool("isClock", true);
        }
    }

    public void PlayAnimationFusion()
    {
        click++;
        if (click % 2 == 0)
        {
            isFusion = false;
        }
        else
        {
            isFusion = true;
        }
    }
}
