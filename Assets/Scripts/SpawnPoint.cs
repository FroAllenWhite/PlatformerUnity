using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;

    private void Start()
    {
        SpawnCharacter();
    }

    private void SpawnCharacter()
    {
        Instantiate(_characterPrefab, transform.position, Quaternion.identity);
    }
}
