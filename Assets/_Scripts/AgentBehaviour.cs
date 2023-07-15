using UnityEngine;

public class AgentBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float health = 100f;
    [SerializeField] private GameManager gm;
    public float fitness;
    private bool _dealDamange = true;

    public float[] genome;

    private Vector3 target;

    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();

        genome = new float[2]; // Assuming a single gene in this example
        for (int i = 0; i < genome.Length; i++)
        {
            genome[i] = Random.Range(-150f, 150f);
        }

        Vector3 target = new Vector3(genome[0], 1f, genome[1]);
    }
    private void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (_dealDamange) DealDamage();

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }

        CalculateFitness();
    }

    void OnTriggerExit(Collider col)
    {
        //not safe
        if (col.gameObject.tag == "BlueZone") _dealDamange = true;

    }

    void OnTriggerEnter(Collider col)
    {
        //is safe
        if (col.gameObject.tag == "BlueZone") _dealDamange = false;
    }

    void DealDamage()
    {
        // take damage
        health -= 65f * Time.deltaTime;
    }

    private void CalculateFitness()
    {
        fitness = Mathf.Max(0f, health) + Mathf.Max(0f, Time.timeSinceLevelLoad - 10f) * 10f; // Fitness based on remaining health and survival time

        if (!_dealDamange)
        {
            fitness += 1000f; // Bonus fitness for being inside the blue zone
        }
    }

}
