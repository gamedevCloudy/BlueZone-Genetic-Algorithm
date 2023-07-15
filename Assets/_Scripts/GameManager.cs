using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using TMPro;

public class GameManager : MonoBehaviour
{
    public List<AgentBehaviour> agents; // Reference to all the agents in the scene

    public int selectedAgentsCount = 100; // Number of agents to select for the next generation

    private int currentGeneration = 1;

    [SerializeField] TMP_Text _genText;
    [SerializeField] TMP_Text _bestGenome;

    public GameObject agentPrefab;

    public float scale = 1f;

    // private float reproductionDelay = 5f; // Delay in seconds before triggering the next generation

    private void Start()
    {
        Application.targetFrameRate = 30;
        // Get all the AgentBehaviour components in the scene
        agents = FindObjectsOfType<AgentBehaviour>().ToList();

        // InvokeRepeating("SelectFittestAgents", 5f, reproductionDelayf);
    }

    public void SelectFittestAgents()
    {
        // Sort the agents based on their fitness scores in descending order
        agents.Sort((a, b) => b.fitness.CompareTo(a.fitness));

        // Select the top agents with the highest fitness scores
        for (int i = selectedAgentsCount; i < agents.Count; i++)
        {

            // Destroy(agents[i].gameObject);
            agents[i].gameObject.SetActive(false); // Disable or destroy the least fit agents
            GameObject g = agents[i].gameObject;
            agents.Remove(agents[i]);
            Destroy(g);
        }

        // Trigger reproduction and mutation process
        ReproduceAndMutate();

        // Increment the generation number
        currentGeneration++;
        _genText.text = "Gen: " + currentGeneration;

        _bestGenome.text = "Best Genome: " + agents[0].genome[0] + " " + agents[0].genome[1];

        BlueZone bz = GameObject.FindObjectOfType<BlueZone>();
        bz.ResetBlueZone();
        // Invoke(nameof(TriggerNextGeneration), reproductionDelay);
    }

    private void TriggerNextGeneration()
    {
        SelectFittestAgents();
    }

    private void Update()
    {
        Time.timeScale = scale;
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




            offspring.transform.position = new Vector3(Random.Range(-150f, 150f), 2f, Random.Range(-150f, 150f));
            AgentBehaviour offspringController = offspring.GetComponent<AgentBehaviour>();

            agents.Add(offspringController);

            // Combine the genomes of the parents and assign it to the offspring
            // You can implement different crossover methods based on your preference
            // For example, you can randomly select genes from the parents or average their values
            // Here, we'll simply clone the genome of one of the parents
            offspringController.genome = parentA.genome.ToArray();

            // Destroy(parentA.gameObject);
            // Destroy(parentB.gameObject);

            // Apply mutation to the offspring's genome to introduce variation
            // You can implement different mutation methods based on your preference
            // For example, you can randomly modify certain genes or add random values to them
            // Here, we'll simply mutate a single gene by adding a small random value
            int geneIndex = Random.Range(0, offspringController.genome.Length);
            offspringController.genome[geneIndex] += Random.Range(-0.1f, 0.1f);
        }
    }

}
