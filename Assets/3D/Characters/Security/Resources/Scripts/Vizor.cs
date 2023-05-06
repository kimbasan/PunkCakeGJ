using UnityEngine;

public class Vizor : MonoBehaviour
{
    [SerializeField] public bool DetectedPlayer { get; private set; }
    [SerializeField] public GameObject Vision;
    [SerializeField] private LayerMask _cloneLayer;
    private Collider[] _colliders;

    public void CheckClone()
    {
        _colliders = Physics.OverlapSphere(this.transform.position, 0.3f, _cloneLayer);
        if (_colliders.Length > 0)
        {
            DetectedPlayer = true;
        }
        else
        {
            DetectedPlayer = false;
        }
    }
}
