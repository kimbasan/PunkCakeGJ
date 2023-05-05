using UnityEngine;

public class DoorControls : MonoBehaviour
{
    [SerializeField] private bool closed;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject doorColliderTile;
    [SerializeField] private int walkableLayer;
    [SerializeField] private int wallLayer;

    private static readonly string closedAnimParam = "Closed";
    private static readonly string reloadAnimParam = "Reload";
    private void Start()
    {
        var levelController = FindAnyObjectByType<LevelController>();
        if (levelController != null)
        {
            levelController._cloneEvent += Reset;
        }
        if (!closed)
        {
            Reset();
        }
    }

    public void TriggerDoor()
    {
        closed = !closed;
        if (closed)
        {
            doorColliderTile.layer = wallLayer;
        } else
        {
            doorColliderTile.layer = walkableLayer;
        }
        doorAnimator.SetBool(closedAnimParam, closed);
    }

    private void Reset()
    {
        closed = true;
        doorAnimator.SetTrigger(reloadAnimParam);
        doorAnimator.SetBool(closedAnimParam, closed);
        doorColliderTile.layer = wallLayer;
    }
}
