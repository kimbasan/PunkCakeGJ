using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    public UnityEvent eventToTrigger;
    public LayerMask characterLayer;
    public Lever otherLever;

    [SerializeField] private bool pulled;
    
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
            Debug.Log("moved event");
            StartCoroutine(CheckPlayerStaying());
        }
    }

    public bool IsHolding()
    {
        var rayStart = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        var direction = transform.rotation * Vector3.back;
        Debug.DrawRay(rayStart, direction * 2f, Color.yellow, 2, true);
        return Physics.Raycast(rayStart, direction, 2f, characterLayer);
    }

    private IEnumerator CheckPlayerStaying()
    {
        yield return new WaitForSeconds(0.2f);
        var rayStart = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        var direction = transform.rotation * Vector3.back;
        Debug.DrawRay(rayStart, direction * 2f, Color.yellow, 2, true);
        
        if (!Physics.Raycast(rayStart, direction, 2f, characterLayer))
        {
            Debug.Log("this lever not holding");
            if (otherLever != null && !otherLever.IsHolding())
            {
                Debug.Log("Other lever not holding");
                Switch();
                Debug.Log("Player hit");
                pulled = false;
            }
        };
    }


    public void Switch()
    {
        Debug.Log("Switch");
        pulled= !pulled;
        eventToTrigger.Invoke();
    }

    private void Reset()
    {
        pulled = false;
    }
}
