using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    public UnityEvent eventToTrigger;
    public LayerMask characterLayer;

    private bool pulled = false;

    private void Start()
    {
        var levelControl = FindAnyObjectByType<LevelController>();
        levelControl._cloneEvent += Reset;
        var playerMovement = FindAnyObjectByType<PlayerMovement>();
        playerMovement.PlayerMoved += PlayerMovement_PlayerMoved;
    }

    private void PlayerMovement_PlayerMoved(object sender, System.EventArgs e)
    {
        if (pulled)
        {
            StartCoroutine(CheckPlayerStaying());
        }
    }

    private IEnumerator CheckPlayerStaying()
    {
        yield return new WaitForSeconds(0.8f);
        var rayStart = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        //Debug.DrawRay(rayStart, Vector3.back * 2f, Color.yellow, 2, true);
        if (!Physics.Raycast(rayStart, Vector3.back, 2f, characterLayer))
        {
            Switch();
            pulled = false;
        };
    }


    public void Switch()
    {
        pulled= !pulled;
        eventToTrigger.Invoke();
    }

    private void Reset()
    {
        pulled = false;
    }
}
