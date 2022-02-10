using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    [SerializeField] private GameObject[] monster;
    [SerializeField] private float TIME_INTERVAL;

    private GameObject currentMonster;
    private float tempTimeInterval;
    private int tempmonsterIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMonster == null&& !cheatMenu.instance.noMonsterToggle.isOn)
        {
            tempTimeInterval += Time.deltaTime;
            if (tempTimeInterval >= TIME_INTERVAL)
            {
                tempTimeInterval = 0;
                tempmonsterIndex = Random.Range(0, monster.Length);
                currentMonster=Instantiate(monster[tempmonsterIndex], transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (cheatMenu.instance.noMonsterToggle.isOn)
            {
                Destroy(currentMonster);
            }
        }
    }
}
