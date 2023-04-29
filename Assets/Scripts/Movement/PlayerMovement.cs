using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Queue<Movements> movementsQueue;

    public event EventHandler PlayerMoved;

    private void Awake()
    {
        PlayerInputActions playerInputActions = new PlayerInputActions();

        playerInputActions.Enable();
        playerInputActions.Player.W.performed += W_performed;
        playerInputActions.Player.S.performed += S_performed;
        playerInputActions.Player.A.performed += A_performed;
        playerInputActions.Player.D.performed += D_performed;
        playerInputActions.Player.Wait.performed += Wait_performed;

        movementsQueue = new Queue<Movements>();
    }

    private void Wait_performed(InputAction.CallbackContext obj)
    {
        movementsQueue.Enqueue(Movements.Wait);
        PlayerMoved?.Invoke(this, EventArgs.Empty);
    }

    private void W_performed(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        transform.position += Vector3.forward;
        movementsQueue.Enqueue(Movements.Up);
        PlayerMoved?.Invoke(this, EventArgs.Empty);
    }

    private void S_performed(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        transform.position += Vector3.back;
        movementsQueue.Enqueue(Movements.Down);
        PlayerMoved?.Invoke(this, EventArgs.Empty);
    }
    private void A_performed(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        transform.position += Vector3.left;
        movementsQueue.Enqueue(Movements.Left);
        PlayerMoved?.Invoke(this, EventArgs.Empty);
    }
    private void D_performed(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        transform.position += Vector3.right;
        movementsQueue.Enqueue(Movements.Right);
        PlayerMoved?.Invoke(this, EventArgs.Empty);
    }

    public enum Movements
    {
        Up, Down, Left, Right, Wait
    }

    public void SaveData()
    {
        SavingManager.Save(new PlayerMoves(movementsQueue));
    }

    public void LoadData()
    {
        PlayerMoves moves = SavingManager.Load();
        Debug.Log("Loaded Moves");
        foreach(Movements move in moves.moves)
        {
            Debug.Log(move);
        }
    }
}
