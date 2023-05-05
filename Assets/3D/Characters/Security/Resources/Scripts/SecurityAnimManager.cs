using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityAnimManager : MonoBehaviour
{
    [SerializeField] public Animator MyAnimator;

    public void PlayWalk()
    {
        MyAnimator.SetBool("Walking", true);
        StartCoroutine(TimerToIdle());
    }

    private void PlayIdle()
    {
        MyAnimator.SetBool("Walking", false);
    }

    private IEnumerator TimerToIdle()
    {
        yield return new WaitForSeconds(0.65f);
        MyAnimator.SetBool("Walking", false);
    }
}
