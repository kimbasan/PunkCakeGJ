using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController: MonoBehaviour
{
    [Header("дебаг")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _clonePrefab;
    [Header("дебаг")]
    [SerializeField] private List<CloneMovement> _clonesList;
    [SerializeField] public Transform _playerSpawner;

    [Header("Ќастройки игрока")]
    [SerializeField] private float _stepDistance; //ƒальность передвижени€ за один шаг
    [SerializeField] private int _numberOfSteps;
    [SerializeField] private LayerMask LayerMask;//маска, которую ищет луч

    [SerializeField] public Text _numberOfStepsText;
    [SerializeField] private GameObject _deadPanel;


    private GameObject _player;
    private PlayerMovement _playerMovement;
    private Vector3 _startClonePosition;
    private Quaternion _startCloneRotation;
    public Action _cloneEvent;

    private void Awake()
    {
        //запоминаем позицию с которой клон начнет
        _player = Instantiate(_playerPrefab, _playerSpawner.position, _playerSpawner.rotation);
        _clonesList = new List<CloneMovement>();
    }

    private void Start()
    {
        _player.GetComponent<CollisionChecker>().PlayerCollision += Dead;
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _playerMovement._numberOfStepsText = _numberOfStepsText;
        _playerMovement.StepDistance = _stepDistance;
        _playerMovement.NumberOfSteps = _numberOfSteps;
        _playerMovement.NumberOfStepsLeft = _numberOfSteps;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartNextClone()
    {
        _cloneEvent?.Invoke();
        CloneMovement currentClone;
        if (_clonesList.Count == 0)
        {
            currentClone = Instantiate(_clonePrefab, _playerSpawner.position, _playerSpawner.rotation).GetComponent<CloneMovement>();
        }
        else
        {
            currentClone = Instantiate(_clonePrefab, _startClonePosition, _startCloneRotation).GetComponent<CloneMovement>();

        }
        foreach (var item in _clonesList)
        {
            item.gameObject.SetActive(true);
        }
        currentClone.Initialization(_playerMovement, _playerMovement.GetMovements(), _stepDistance);
        _cloneEvent += currentClone.RestartPosition;
        currentClone.GetComponent<CollisionChecker>().PlayerCollision += Dead;
        _startClonePosition = _playerMovement.transform.position;
        _startCloneRotation = _playerMovement.transform.rotation;
        _clonesList.Add(currentClone);
        currentClone.name = $"Clone {_clonesList.Count}";
        _playerMovement.ClearMovements();
        _playerMovement.NumberOfStepsLeft = _numberOfSteps;
        _numberOfStepsText.text = _playerMovement.NumberOfStepsLeft.ToString();
    }

    public void Dead()
    {
        _deadPanel.SetActive(true);
    }
}
