using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteractionCard : MonoBehaviour
{
    private PlayerInputActions PlayerInputActions;
    public bool CheckKeyCard, CheckDoor, AvailabilityKeyCard;

    private void Awake()
    {
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Player.Action.performed += context => InteractionWithTheEnvironment();
    }
    private void OnEnable()
    {
        PlayerInputActions.Enable();
    }
    private void OnDisable()
    {
        PlayerInputActions.Disable();
    }
    public void InteractionWithTheEnvironment()
    {
        if (CheckKeyCard)
        {
            AvailabilityKeyCard = true;
            CheckKeyCard = false;
            Destroy(GetComponent<FindObject>().ObjectFind.GetComponent<ParentSearch>().Parent);
        }
        else if(CheckDoor && AvailabilityKeyCard)
        {
            Debug.Log("Дверь открыта");
        }
        else if(CheckDoor && AvailabilityKeyCard == false)
        {
            Debug.Log("Где карта");
        }
    }
}
