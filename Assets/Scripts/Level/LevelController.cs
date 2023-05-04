using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using static LevelController;

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

    [SerializeField] public TextMeshProUGUI _numberOfStepsText;
    [SerializeField] private GameObject _deadPanel;
    [SerializeField] private List<SecurityController> _securities;
    [SerializeField] private List<CinemachineVirtualCamera> _cams;
    private int _currentCameraIndex = 0;

    private GameObject _player;
    private PlayerMovement _playerMovement;
    private Vector3 _startClonePosition;
    private Quaternion _startCloneRotation;
    public Action _cloneEvent;
    public Action BotStep;

    public enum CameraState
    {
        Front,
        Right,
        Back,
        Left
    }

    private void Awake()
    {
        //запоминаем позицию с которой клон начнет
        _player = Instantiate(_playerPrefab, _playerSpawner.position, _playerSpawner.rotation);
        _clonesList = new List<CloneMovement>();
        for (int i = 0; i < _cams.Count; i++)
        {
            _cams[i].Follow = _player.transform;
        }
    }

    private void Start()
    {
        _player.GetComponent<CollisionChecker>().PlayerCollision += Dead;
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _playerMovement._numberOfStepsText = _numberOfStepsText;
        _playerMovement.StepDistance = _stepDistance;
        _playerMovement.NumberOfSteps = _numberOfSteps;
        _playerMovement.NumberOfStepsLeft = _numberOfSteps;
        _playerMovement.PlayerStep += PlayerStep;
        SecuritiesInitialize();
    }

    private void CameraLeft()
    {
        if (_playerMovement.MyCameraState == CameraState.Left)
        {
            _playerMovement.MyCameraState = CameraState.Front;
        }
        else
        {
            _playerMovement.MyCameraState++;
        }

        if (_currentCameraIndex == _cams.Count - 1)
        {
            _currentCameraIndex = 0;
            _cams[_currentCameraIndex].gameObject.SetActive(true);
            _cams[_cams.Count - 1].gameObject.SetActive(false);
            return;
        }
        _currentCameraIndex++;
        _cams[_currentCameraIndex].gameObject.SetActive(true);
        _cams[_currentCameraIndex - 1].gameObject.SetActive(false);
    }

    private void CameraRight()
    {
        if (_playerMovement.MyCameraState == 0)
        {
            _playerMovement.MyCameraState = CameraState.Left;
        }
        else
        {
            _playerMovement.MyCameraState--;
        }


        if (_currentCameraIndex == 0)
        {
            _currentCameraIndex = _cams.Count - 1;
            _cams[_currentCameraIndex].gameObject.SetActive(true);
            _cams[0].gameObject.SetActive(false);
            return;
        }
        _currentCameraIndex--;
        _cams[_currentCameraIndex].gameObject.SetActive(true);
        _cams[_currentCameraIndex + 1].gameObject.SetActive(false);
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

    public void PlayerStep()
    {
        _playerMovement.IsPlayerStep = false;
        MStep(BotStep);
    }

    private IEnumerator TimerStep()
    {
        yield return new WaitForSeconds(1);
        _playerMovement.IsPlayerStep = true;
    }

    private void MStep(Action action)
    {
        action?.Invoke();
        StartCoroutine(TimerStep());
    }

    private void SecuritiesInitialize()
    {
        for (int i = 0; i < _securities.Count; i++)
        {
            BotStep += _securities[i].Step;
            _cloneEvent += _securities[i].RestartPosition;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CameraLeft();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            CameraRight();
        }
    }
}
