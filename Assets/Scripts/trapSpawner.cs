using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private float X_LEFT_BORDER;
    [SerializeField] private float X_RIGHT_BORDER;
    [SerializeField] private float INTERVAL_TIME;
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private float X_SPACE_BETWEEN;

    private float tempTime;
    private Vector3 position;
    private float x;
    private float y;
    // Start is called before the first frame update
    void Start()
    {
        tempTime = 0;
        position = transform.position;
    }

    private void _SpawnTrap(GameObject obj,Vector3 position)
    {
        Instantiate(obj, position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        tempTime += Time.deltaTime;
        if (tempTime >= INTERVAL_TIME)
        {
            GameObject obj = objects[Random.Range(0,objects.Length)];
            _SetPosition();
            _SpawnTrap(obj, position);
            tempTime = 0;
        }
    }

    private void _SetPosition()
    {
        position.x = player.transform.position.x + Random.Range(-X_SPACE_BETWEEN,X_SPACE_BETWEEN);
        if (position.x > X_RIGHT_BORDER) position.x = X_RIGHT_BORDER;
        if (position.x < X_LEFT_BORDER) position.x = X_LEFT_BORDER;
    }
}
