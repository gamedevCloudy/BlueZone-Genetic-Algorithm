using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
    [SerializeField] private GameObject _agent;
    [SerializeField] private int _agentCount;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < _agentCount; i++)
        {
            float spawnPosX = Random.Range(75f, 170f);
            float spawnPosZ = Random.Range(70f, 270f);
            Vector3 spawnPos = new Vector3(spawnPosX, 2.5f, spawnPosZ);
            GameObject g = Instantiate(_agent, spawnPos, Quaternion.identity);
            g.transform.SetParent(GameObject.Find("Land").transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
