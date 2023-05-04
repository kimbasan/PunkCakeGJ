using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    public UnityEvent eventToTrigger;
    public Collider leverTrigger;

    private bool pulled = false;

    private void Start()
    {
        leverTrigger.enabled = false;
        var levelControl = FindAnyObjectByType<LevelController>();
        levelControl._cloneEvent += Reset;
    }

    public void Switch()
    {
        if (!pulled)
        {
            pulled = true;
            leverTrigger.enabled = true;
        } else
        {
            pulled = false;
            leverTrigger.enabled = false;
        }
        eventToTrigger.Invoke();
                
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.CLONE_TAG))
        {
            Switch();
        }
    }

    private void Reset()
    {
        pulled = false;
        leverTrigger.enabled = false;
    }
}
