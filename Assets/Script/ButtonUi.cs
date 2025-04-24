using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUi : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public Animator fusionUiAnimator;
    public int click = 0;
    public bool isFusion;
    public bool isButton;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isButton == true)
        {
            fusionUiAnimator.SetBool("isFusion", isFusion);
        }
    }
    
    public void PlayAnimationInventory()
    {
        click++;
        if (click % 2 == 0)
        {
            animator.SetBool("isInventory", false);
           if (isFusion == true)
            {
                FusionManager.Instance.isFusion = false;
                isFusion = false;
                Debug.Log(FusionManager.Instance.isFusion);
            }
        }
        else
        {
            animator.SetBool("isInventory", true);
            if(FusionManager.Instance.IsCanFuse() == true)
            {
                FusionManager.Instance.isFusion = true;
                isFusion = true;
                Debug.Log(FusionManager.Instance.isFusion);
            }
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
