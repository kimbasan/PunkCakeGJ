using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class CloneMovement : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerMoves olderMoves;

    private Queue<Movements> movements;
    void Awake()
    {
        movements = olderMoves?.moves;
        playerMovement.PlayerMoved += PlayerMovement_PlayerMoved;
    }

    private void PlayerMovement_PlayerMoved(object sender, System.EventArgs e)
    {
        Movements result;
        if (movements != null && movements.TryDequeue(out result))
        {
            switch (result)
            {
                case Movements.Wait: break;
                case Movements.Up: transform.position += Vector3.forward; break;
                case Movements.Down: transform.position += Vector3.back; break;
                case Movements.Left: transform.position += Vector3.left; break;
                case Movements.Right: transform.position += Vector3.right; break;
            }
        }
    }
}
