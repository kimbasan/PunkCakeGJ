using System.Collections.Generic;
using UnityEngine;

using static PlayerMovement;

public class CloneMovement : MonoBehaviour
{
    public PlayerMovement playerMovement;

    private Queue<Movements> movements;

    public void SetClone(CloneState cloneState)
    {
        this.movements = cloneState.moves;
        transform.position = cloneState.startingPosition.ToVector3();
    }

    void Awake()
    {        
        if (playerMovement != null)
        {
            playerMovement.PlayerMoved += PlayerMovement_PlayerMoved;
        }
    }
    
    private void PlayerMovement_PlayerMoved(object sender, System.EventArgs e)
    {
        Movements result;
        if (movements != null && movements.TryDequeue(out result))
        {
            switch (result)
            {
                case Movements.Wait: break;
                case Movements.Up: Move(Vector3.forward); break;
                case Movements.Down: Move(Vector3.back); break;
                case Movements.Left: Move(Vector3.left); break;
                case Movements.Right: Move(Vector3.right); break;
            }
        }
    }

    private void Move(Vector3 direction)
    {
        transform.position+= direction;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
