using System;
using UnityEngine;

namespace Utils
{
    public class PeristentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab = null;

        static bool hasSpawned = false;

        private void Awake() {
            if (hasSpawned) return;

            SpawnPersistentObjects();

            hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}