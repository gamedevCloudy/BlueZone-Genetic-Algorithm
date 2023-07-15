# Blue Zone - Genetic Algorithm | Survival Simulation

## Evolutionary AI Simulation

This project is an AI simulation built in Unity that explores the concept of evolutionary algorithms. In this experiment, a population of primitive shape agents must learn to survive within a shrinking blue zone. The agents evolve over multiple generations, gradually improving their survival strategies.

![Screenshot](https://github.com/gamedevCloudy/BlueZone-Genetic-Algorithm/blob/4c6af3e1f4642caf17766bd9a53870e0a997bb14/SS.png)

## How It Works

- Agents: The agents are represented by primitive shapes in the virtual world. They move around freely and must avoid leaving the blue zone to prevent taking damage.

- Genome: The current genes are target destination for the agent at start.

- Fitness Evaluation: The fitness of each agent is determined by their ability to stay inside the blue zone. The longer an agent survives without taking damage, the higher its fitness score.

- Selection: The fittest agents are selected for reproduction. These agents have the highest fitness scores and are considered the parents of the next generation.

- Reproduction and Mutation: The selected agents reproduce to create offspring. The offspring inherit the traits and behaviors of their parents, with slight random changes introduced through mutation to add variation.

- Generations: The experiment progresses through multiple generations. The fittest agents from each generation become the parents of the next generation, creating an iterative process of improvement.

## Observation

- Agents start moving to the center of the zone after generation 4. With significant behaviour changes.

- There are diminishing returns after generation 100

- Over time, Genome comes closer to 0, compared to starting value of +-150

## Getting Started

1. Clone the repository or download the source code.

2. Open the project in Unity.

3. Run the simulation by entering Play mode.

4. Observe how the agents learn and evolve over time, surviving within the blue zone.

## Customization

- Blue Zone: The size and rate of shrinkage of the blue zone can be adjusted in the BlueZone script.

- Agent Behavior: The movement and decision-making of the agents can be modified in the AgentController script.

- Genetic Algorithm Parameters: The population size, selection criteria, mutation rate, and other genetic algorithm parameters can be adjusted in the GameManager script to experiment with different evolutionary dynamics.

## Contributing

Contributions to the project are welcome! If you have any ideas, suggestions, or improvements, feel free to submit a pull request or open an issue.

## License

This project is licensed under the [MIT License](LICENSE).

## Acknowledgments

This project is inspired by the concept of evolutionary algorithms and aims to demonstrate the power of evolution in solving complex problems. It is a learning experiment and not intended for production use.

---

Feel free to customize and expand on this template to provide more details and instructions specific to your experiment.
