using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Runtime")]
    [SerializeField] private List<AgentBehaviour> agents; // Reference to all the agents in the scene

    [SerializeField] private int selectedAgentsCount = 100; // Number of agents to select for the next generation

    [Tooltip("Adjust upto 100 - Speed up the simulation")]
    [Range(1f, 100f)]
    [SerializeField] private float _timeScale = 1f;

    [SerializeField] private float spawnRange = 40f;

    private int currentGeneration = 1;

    [Header("UI")]
    [SerializeField] TMP_Text _genText;
    [SerializeField] TMP_Text _genomeText;

    [Header("Vars")]
    [SerializeField] private GameObject agentPrefab;
    [SerializeField] private GameObject land;
    [SerializeField] private BlueZone bz;

    // [SerializeField] private GameObject _agent;
    [Tooltip("Inital number of agents.")]
    [SerializeField] private int _agentCount;

    // Start is called before the first frame update
    void Awake()
    {
        //create an initial population
        for (int i = 0; i < _agentCount; i++)
        {
            float spawnPosX = 0;
            float spawnPosZ = 0;
            // while (25f > spawnPosX && spawnPosX > -25f)
            spawnPosX = Random.Range(-spawnRange, spawnRange);
            // while (25f > spawnPosZ && spawnPosZ > -25f)
            spawnPosZ = Random.Range(-spawnRange, spawnRange);

            Vector3 spawnPos = new Vector3(spawnPosX, 2.5f, spawnPosZ);
            GameObject g = Instantiate(agentPrefab, spawnPos, Quaternion.identity);
            g.transform.SetParent(GameObject.Find("Land").transform);

            AgentBehaviour ag = g.GetComponent<AgentBehaviour>();

            for (int j = 0; j < ag.genome.Length; j++)
            {
                ag.genome[j] = Random.Range(-spawnRange, spawnRange);
            }
            agents.Add(ag);
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 30;
    }

    public void SelectFittestAgents()
    {
        // Sort the agents based on their fitness scores in descending order
        agents.Sort((a, b) => b.fitness.CompareTo(a.fitness));

        // Select the top agents with the highest fitness scores
        for (int i = selectedAgentsCount; i < agents.Count; i++)
        {
            agents[i].gameObject.SetActive(false); // Disable or destroy the least fit agents
            GameObject g = agents[i].gameObject;
            agents.Remove(agents[i]);
            Destroy(g);
        }

        // Trigger reproduction and mutation process
        ReproduceAndMutate();

        // Increment the generation number
        currentGeneration++;
        UpadteUI();


        bz.ResetBlueZone();
        // Invoke(nameof(TriggerNextGeneration), reproductionDelay);
    }


    void UpadteUI()
    {
        _genText.text = "Gen: " + currentGeneration;

        _genomeText.text = "Best Genome: " + agents[0].genome[0] + " " + agents[0].genome[1];
    }
    private void Update()
    {
        Time.timeScale = _timeScale;
    }

    public void ReproduceAndMutate()
    {
        // Create new offspring from the selected fittest agents
        // for (int i = selectedAgentsCount; i < agents.Count; i++)
        for (int i = 0; i < selectedAgentsCount; i++)
        {
            AgentBehaviour parentA = agents[Random.Range(0, selectedAgentsCount)];
            AgentBehaviour parentB = agents[Random.Range(0, selectedAgentsCount)];

            // Create a new agent object as the offspring
            GameObject offspring = Instantiate(agentPrefab);

            offspring.transform.SetParent(land.transform);


            offspring.transform.position = new Vector3(Random.Range(-spawnRange, spawnRange), 2f, Random.Range(-spawnRange, spawnRange));
            AgentBehaviour offspringController = offspring.GetComponent<AgentBehaviour>();

            agents.Add(offspringController);

            // Combine the genomes of the parents and assign it to the offspring
            // You can implement different crossover methods based on your preference
            // For example, you can randomly select genes from the parents or average their values
            // Here, we'll simply clone the genome of one of the parents
            // offspringController.genome = parentA.genome.ToArray();


            offspringController.genome[0] = (parentA.genome[0] + parentB.genome[0]) / 2;
            offspringController.genome[1] = parentA.genome[1];



            // Apply mutation to the offspring's genome to introduce variation
            // You can implement different mutation methods based on your preference
            // For example, you can randomly modify certain genes or add random values to them
            // Here, we'll simply mutate a single gene by adding a small random value
            int geneIndex = Random.Range(0, offspringController.genome.Length);
            offspringController.genome[geneIndex] += Random.Range(-2f, 2f);
        }

        for (int i = 0; i < (agents.Count - selectedAgentsCount); i++)
        {
            agents[i].gameObject.SetActive(false);
            agents.Remove(agents[i]);
        }
    }

}
