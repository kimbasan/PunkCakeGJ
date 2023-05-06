using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyInteractionCard : MonoBehaviour
{
    [SerializeField] private Image CardImage;
    [SerializeField] private AudioClip Clip;
    [SerializeField] private AudioSource Source;
    //private PlayerInputActions PlayerInputActions;
    public bool CheckKeyCard, CheckDoor, AvailabilityKeyCard;
    public Quests quests;

    private void Awake()
    {
        //PlayerInputActions = new PlayerInputActions();
        //PlayerInputActions.Player.Action.performed += context => InteractionWithTheEnvironment();
        CardImage.enabled = false;
    }
    //private void OnEnable()
    //{
    //    PlayerInputActions.Enable();
    //}
    //private void OnDisable()
    //{
    //    PlayerInputActions.Disable();
    //}
    public void KeyCard()
    {
        CheckKeyCard = !CheckKeyCard;
    }
    public void InteractionWithTheEnvironment()
    {
        if (CheckKeyCard)
        {
            Source.PlayOneShot(Clip);
            AvailabilityKeyCard = true;
            CheckKeyCard = false;
            CardImage.enabled = true;
            CheckQuest();
            quests.CompletingTheQuest[0] = true;
            //quests.ProgressOfTheCompletedTask[0].SetActive(true);
            quests.CheckProgressAdditionalQuest(0);
            Destroy(gameObject);
        }
    }
    void CheckQuest()
    {
        bool quest = false;
        if (quests.AdditionalQuests[0].activeSelf)
        {
            quest = false;            
        }
        if(quest == false)
        {
            quests.AdditionalQuests[0].SetActive(true);
        }
    }
}
