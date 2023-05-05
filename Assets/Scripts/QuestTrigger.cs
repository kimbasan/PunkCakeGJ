using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public Quests quests;
    private PlayerInputActions PlayerInputActions;

    public void CheckQuests(int IndexQuest)
    {
        if (quests.IndexEducation == IndexQuest)
        {
            quests.CheckEducationQuest();
        }
    }
    private void Awake()
    {
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Player.Move.performed += context => CheckQuests(0);
        PlayerInputActions.Player.QE.performed += context => CheckQuests(1);
        PlayerInputActions.Player.NextClone.performed += context => CheckQuests(3);
        PlayerInputActions.Player.Stay.performed += context => CheckQuests(4);
    }
    private void OnEnable()
    {
        PlayerInputActions.Enable();
    }
    private void OnDisable()
    {
        PlayerInputActions.Disable();
    }
}
