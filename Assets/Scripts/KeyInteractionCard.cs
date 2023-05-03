using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyInteractionCard : MonoBehaviour
{
    [SerializeField] private Image CardImage;
    [SerializeField] private AudioClip Clip;
    [SerializeField] private AudioSource Source;
    private PlayerInputActions PlayerInputActions;
    public bool CheckKeyCard, CheckDoor, AvailabilityKeyCard;

    private void Awake()
    {
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Player.Action.performed += context => InteractionWithTheEnvironment();
        CardImage.enabled = false;
    }
    private void OnEnable()
    {
        PlayerInputActions.Enable();
    }
    private void OnDisable()
    {
        PlayerInputActions.Disable();
    }
    public void KeyCard()
    {
        CheckKeyCard = true;
    }
    public void InteractionWithTheEnvironment()
    {
        if (CheckKeyCard)
        {
            Source.PlayOneShot(Clip);
            AvailabilityKeyCard = true;
            CheckKeyCard = false;
            CardImage.enabled = true;
            //GetComponent<FindObject>().ActionText.enabled = false;
            Destroy(gameObject);
            //GetComponent<FindObject>().ObjectFind = null;
        }
        else if(CheckDoor && AvailabilityKeyCard)
        {
            Debug.Log("Дверь открыта");
        }
        else if(CheckDoor && AvailabilityKeyCard == false)
        {
            Source.PlayOneShot(Clip);
            Debug.Log("Где карта");
        }
    }
}
