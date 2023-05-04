using UnityEngine;

public class DoorControls : MonoBehaviour
{
    [SerializeField] private bool closed;
    [SerializeField] private Animator doorAnimator;

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
        doorAnimator.SetBool("Closed", closed);
    }

    private void Reset()
    {
        closed = true;
        doorAnimator.SetBool("Closed", closed);
    }
}
