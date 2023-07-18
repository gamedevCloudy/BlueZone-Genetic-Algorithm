using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueZone : MonoBehaviour
{

    public float shrinkingRate = 0.5f;
    // Rate at which the blue zone shrinks per second

    [SerializeField] private float startSize = 100f;
    [SerializeField] private float currentSize = 100f;
    // Current size of the blue zone

    public GameManager _gameManager;

    // Update is called once per frame
    void Update()
    {
        if (currentSize > 1f)
        {
            currentSize -= shrinkingRate * Time.deltaTime;
            currentSize = Mathf.Max(currentSize, 0f);

            // Update the scale of the blue zone
            transform.localScale = new Vector3(currentSize, 1f, currentSize);

        }
        else _gameManager.SelectFittestAgents();
    }

    public void ResetBlueZone()
    {
        currentSize = startSize;
    }
}
