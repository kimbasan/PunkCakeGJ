using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimManager : MonoBehaviour
{
    public Animator _animator;

    public void PlayWalk()
    {
        _animator.SetBool("Walking", true);
        StartCoroutine(TimerToIdle());
    }

    private IEnumerator TimerToIdle()
    {
        yield return new WaitForSeconds(0.4f);
        _animator.SetBool("Walking", false);
    }
}
