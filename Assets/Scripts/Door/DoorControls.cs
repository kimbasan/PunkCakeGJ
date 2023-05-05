using UnityEngine;

public class DoorControls : MonoBehaviour
{
    [SerializeField] private bool closed;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject doorColliderTile;
    [SerializeField] private int walkableLayer;
    [SerializeField] private int wallLayer;
    private void Start()
    {
        var levelController = FindAnyObjectByType<LevelController>();
        if (levelController != null)
        {
            levelController._cloneEvent += Reset;
        }
        Reset();
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
        doorAnimator.SetBool("Closed", closed);
    }

    private void Reset()
    {
        closed = true;
        doorAnimator.SetBool("Closed", closed);
        doorColliderTile.layer = wallLayer;
    }
}
