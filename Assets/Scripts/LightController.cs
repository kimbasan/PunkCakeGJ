using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light light;

    private void Awake()
    {
        light.color = Color.red;
    }
    public void SetLight(bool on)
    {
         light.enabled = on;
    }
}
