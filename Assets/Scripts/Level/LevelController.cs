using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController: MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private CloneMovement cloneMovement;
    [SerializeField] private GameObject clone;
    
    [SerializeField] private LevelState currentState;

    private Vector3 startingPosition;

    private void Awake()
    {
        //запоминаем позицию с которой клон начнет
        startingPosition = playerMovement.transform.position;

        LevelState previousState = SavingController.Load();
        if (previousState != null)
        {
            //enable clone
            cloneMovement.SetClone(previousState.cloneStates[0]); // переписать на много клонов
            clone.SetActive(true);

            // move player to previous position
            playerMovement.transform.position = previousState.playerPosition.ToVector3();
        }
        
    }
    public void ReloadLevel()
    {
        if (currentState == null)
        {
            currentState = new LevelState();
        }
        // сохраняем ходы и начальную позицию
        CloneState clone = new CloneState(playerMovement.GetMovements(), startingPosition);
        currentState.cloneStates.Add(clone);
        currentState.playerPosition = new SerializableVector3(playerMovement.transform.position);

        SavingController.Save(currentState);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FailLevel()
    {
        // clean up old save and reload
    }
}
