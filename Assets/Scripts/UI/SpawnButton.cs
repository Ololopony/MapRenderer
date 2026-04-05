using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    [SerializeField]
    private GridFiller _gridFiller;
    [SerializeField]
    private GameObject _playerCapsule;
    [SerializeField]
    private GameObject _playerCamera;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Spawn()
    {
        _gridFiller.StartFiller();
        _playerCapsule.SetActive(true);
        _playerCamera.SetActive(true);
        gameObject.SetActive(false);
    }
}
