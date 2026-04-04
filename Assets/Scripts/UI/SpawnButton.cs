using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    [SerializeField]
    private GridFiller _gridFiller;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Spawn()
    {
        _gridFiller.StartFiller();
        gameObject.SetActive(false);
    }
}
