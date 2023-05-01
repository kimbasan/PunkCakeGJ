using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody RbPlayer;
    private PlayerInputActions PlayerInputActions;//скрипт на управление
    private Vector2 MoveDirection;// 1)вектор направления, 2)вектор движения
    public float Speed;

    private void Awake()
    {
        PlayerInputActions = new PlayerInputActions();
    }
    private void OnEnable()
    {
        PlayerInputActions.Enable();
    }
    private void OnDisable()
    {
        PlayerInputActions.Disable();
    }
    private void Start()
    {
        RbPlayer = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        MoveDirection = PlayerInputActions.Player.Move.ReadValue<Vector2>();
        Moved();
    }
    public void Moved()
    {
        Vector3 Move = new Vector3(MoveDirection.x, 0, MoveDirection.y);
        RbPlayer.velocity = new Vector3(Move.x * Speed, Move.y, Move.z * Speed);
    }
}
