using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueZone : MonoBehaviour
{

    public float shrinkingRate = 0.5f; // Rate at which the blue zone shrinks per second
    // public float damagePerSecond = 10f; // Amount of damage inflicted per second

    private float currentSize = 150f; // Current size of the blue zone
                                      // Start is called before the first frame update

    public GameManager gm;

    // Update is called once per frame
    void Update()
    {
        if (currentSize > 5f)
        {
            currentSize -= shrinkingRate * Time.deltaTime;
            currentSize = Mathf.Max(currentSize, 0f);

            // Update the scale of the blue zone
            transform.localScale = new Vector3(currentSize, 1f, currentSize);

        }
        else gm.SelectFittestAgents();
    }

    public void ResetBlueZone()
    {
        currentSize = 150f;
    }
}
