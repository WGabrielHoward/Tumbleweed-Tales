# BioBall — Architecture Refactor

> A software architecture refactor exploring the transition from object-oriented design toward a more centralized, data-oriented, and system-focused architecture.

## Project Summary

BioBall is a 3D rolling survival game that began as a traditionally object-oriented application. The original architecture used principles such as encapsulation, abstraction, inheritance, and polymorphism to organize gameplay state and behavior.

As the project evolved, it became an opportunity to explore a different architectural approach.

Rather than organizing functionality primarily around individual objects that own both their data and behavior, BioBall was refactored toward a more centralized architecture in which:

- **Entities** provide identity and association.
- **Components** represent focused pieces of gameplay state.
- **Systems** centralize the processing of related data.
- **Component storage** is organized around system lookup and iteration requirements.

The refactor explores how separating data from behavior affects system organization, responsibility boundaries, data access, and the way gameplay state is located and processed.

BioBall serves as both a playable game and a practical exploration of architectural tradeoffs between traditional object-oriented design and a more data-oriented, system-focused approach.

---

## The Refactor at a Glance

| Original Architecture | Refactored Architecture |
|---|---|
| Objects encapsulate state and behavior | Gameplay state is separated from the systems that process it |
| Processing is distributed across individual objects | Related processing is centralized into focused systems |
| Data is primarily organized around object ownership | Data is organized around system access and processing requirements |
| Object relationships and references connect gameplay behavior | Entity IDs associate related component data |
| Locating relevant state may require navigating object relationships | Entity-to-component mappings support direct component lookup |
| Behavior is distributed across individual entities | Systems iterate over focused collections of relevant component data |

The goal of the refactor was not to declare one approach universally better than the other.

Instead, BioBall explores a different set of architectural tradeoffs by shifting the primary focus from:

> **What behavior does this object own?**

toward:

> **What data does this system need to process, and what responsibility should that system own?**

---

## Why Make This Change?

The original architecture provided clear abstraction, encapsulation, and ownership, and was effective during the initial development of the project.

As the application grew, however, related data and processing became increasingly distributed across individual objects. This created an opportunity to explore a different set of architectural tradeoffs: centralizing responsibilities, separating data from behavior, and organizing data around how it is accessed and processed.

The refactor was therefore motivated by more than code organization alone. It explores how a centralized, data-oriented architecture can improve both **system organization and data access**.

Rather than primarily asking:

> **What behavior does this object own?**

the refactored architecture places greater emphasis on questions such as:

> **What data does this system need to process?**  
> **How can that data be located efficiently?**  
> **What data is frequently accessed or processed together?**  
> **How can systems iterate over relevant entities without repeatedly searching through unrelated objects?**

This shift led to several architectural goals:

- **Separation of data and behavior** — Represent gameplay state independently from the systems responsible for processing it.
- **Centralized responsibilities** — Move related processing into focused systems rather than distributing it across individual objects.
- **Reduced coupling** — Reduce dependencies between processing logic and specific entity implementations.
- **Efficient data access** — Organize component storage around lookup and iteration requirements.
- **Targeted processing** — Allow systems to operate on relevant component collections rather than repeatedly searching through unrelated objects.
- **Search and lookup efficiency** — Associate entity IDs with component storage to support efficient component lookup.
- **Dense iteration** — Store active component data densely so systems can iterate directly over the data they process.
- **Clearer system ownership** — Define explicit responsibilities for processing movement, health, damage, behavior, and other gameplay state.
- **Scalability and performance exploration** — Establish an architecture that can be evaluated as the number of entities, components, and interactions increases.

The goal was not to treat data-oriented design as a universal replacement for object-oriented programming. Instead, the refactor explores where a more centralized, data-oriented approach provides clearer responsibility boundaries and more direct access to the data being processed.

By organizing component storage around entity lookup and dense iteration, the architecture also provides a foundation for future scalability and performance experimentation.

---

## Architectural Evolution

### Original Approach: Object-Oriented Design

The original version of BioBall followed a traditional object-oriented approach.

Gameplay entities were modeled as objects that encapsulated both state and behavior. Object-oriented principles such as **encapsulation, abstraction, inheritance, and polymorphism** were used to organize and extend gameplay functionality.

Conceptually:

```text
Entity / Object
│
├── State
│   ├── Health
│   ├── Movement
│   └── Other Entity Data
│
├── Behavior
│   ├── Movement Logic
│   ├── Damage Logic
│   └── Entity-Specific Logic
│
└── Relationships
    └── References and interactions with other objects
```

This approach provided clear ownership and encapsulation during the initial development of the project. As gameplay functionality expanded, however, related state and processing became distributed across multiple objects and responsibilities.

The refactor explored an alternative approach centered on **entity identity, focused data, and centralized processing**.

### Refactored Approach: Data-Oriented, System-Focused Design

The refactored architecture separates **entity identity, gameplay data, and processing responsibilities**.

```text
                    Entity IDs
                        │
        ┌───────────────┼───────────────┐
        │               │               │
        ▼               ▼               ▼
    Movement          Health          Damage
      Data             Data            Data
        │               │               │
        └───────────────┼───────────────┘
                        │
                        ▼
               Centralized Systems
        ┌───────────────┼───────────────┐
        │               │               │
        ▼               ▼               ▼
   MovementSystem   HealthSystem    DamageSystem
```

Instead of individual objects owning most of the logic associated with their state, focused systems are responsible for processing related categories of data.

This shifts responsibility from being organized primarily around:

> **Which object owns this behavior?**

toward:

> **Which system is responsible for processing this data?**

---

## Core Architecture & Implementation

### Entities

Entities provide identity and associate related pieces of gameplay data.

Rather than requiring systems to operate through a complete object representation, systems use entity IDs to locate the component data relevant to their processing.

Entity identity acts as the connection between related pieces of state while allowing data and processing responsibilities to remain more independent.

### Components

Components represent focused pieces of gameplay state.

The current architecture includes components related to:

- Movement
- Health
- Damage
- Behavior
- Player input
- Death and entity lifecycle
- Elemental state and effects

Components are designed around the information required by the systems that process them.

Rather than placing all state and behavior inside a single entity object, gameplay state can be represented through focused component types and accessed according to the requirements of individual systems.

### Systems

Systems centralize the logic responsible for processing related gameplay data.

The architecture includes systems responsible for areas such as:

- **MovementSystem** — Processes movement-related component data.
- **HealthSystem** — Manages health-related state.
- **DamageSystem** — Applies and processes damage.
- **CombatSystem** — Coordinates combat interactions.
- **BehaviorSystem** — Processes entity behavior.
- **DeathSystem** — Handles death-related processing and entity lifecycle behavior.
- **ElementSystem** — Processes elemental state and effects.
- **GameStateSystem** — Coordinates higher-level game state and progression.
- **ScoreSystem** — Manages score-related state and processing.

By assigning focused responsibilities to systems, related processing can be centralized rather than distributed across individual entity implementations.

Each system has a clearer ownership boundary:

> **This system owns this category of processing and operates on the data required to perform it.**

---

## Data Access & Component Storage

### `SparseSet<T>`

A key part of the refactor was redesigning how dynamic component data is stored and accessed.

The project uses a custom sparse-set-style component store that associates entity IDs with densely stored component data.

Conceptually:

```text
Entity ID
    │
    ▼
Entity-to-Index Mapping
    │
    ▼
Dense Index
    │
    ├── Entity IDs
    │
    └── Component Data
```

The current implementation maintains an entity-to-dense-index mapping alongside dense collections of entity IDs and component data.

This allows the structure to support both:

- **Efficient entity lookup** — Locate the component associated with a specific entity without scanning the full component collection.
- **Dense iteration** — Process active component data by iterating directly through densely stored collections.

Component removal uses a swap-with-last approach to keep the dense collections compact while updating the entity-to-index mapping.

This structure is particularly useful for systems that need to answer two different questions:

> **Does this entity have the component I need?**

and:

> **What are all of the active components this system needs to process?**

By supporting both entity lookup and dense iteration, `SparseSet<T>` organizes component storage around the access patterns of the systems using it.

> The current implementation uses a dictionary-backed entity-to-index mapping. Future iterations may explore alternative sparse indexing strategies and benchmark their tradeoffs for lookup performance, memory usage, and scalability.

---

## Design Decisions & Tradeoffs

### A Practical Hybrid Architecture

The purpose of the refactor was not to eliminate object-oriented programming or Unity abstractions.

The project instead explores where centralized systems and focused component data provide advantages while retaining engine-facing objects where they remain practical.

The resulting architecture combines:

- **Centralized systems** for processing related categories of gameplay data.
- **Focused component data** for representing gameplay state independently from processing logic.
- **Entity IDs and component mappings** for associating related data.
- **Unity-facing bridge and integration classes** for responsibilities involving engine interaction, physics, collisions, and scene objects.

The goal is not architectural purity. The goal is to choose an organizational model based on the problem being solved and the data access patterns involved.

### Not a Full ECS Framework

BioBall is not intended to be a complete Entity Component System framework or a replacement for Unity's DOTS architecture.

Instead, the project incrementally applies several principles commonly associated with data-oriented and ECS-style architecture:

- Entity identifiers
- Focused component data
- Centralized systems
- Separation of data and behavior
- Dense component storage
- Entity-to-component lookup

These ideas were introduced through a refactor of an existing application rather than through the creation of a complete ECS engine.

---

## Engineering Concepts Demonstrated

Although BioBall is a game project, the refactor focuses on software engineering concepts that extend beyond game development:

- **Architectural Refactoring** — Transitioning an existing application toward a different architectural model without rewriting it from scratch.
- **Data-Oriented Design** — Organizing data around system processing and access patterns rather than exclusively around object ownership.
- **System Design** — Defining focused systems with clear responsibilities and ownership boundaries.
- **Data Structure Design** — Implementing component storage that supports entity lookup and dense iteration.
- **Performance-Oriented Thinking** — Exploring data access, search efficiency, targeted processing, and scalability.
- **Separation of Concerns** — Separating entity identity, gameplay state, and processing responsibilities.
- **Architectural Tradeoffs** — Evaluating where object-oriented and data-oriented approaches are most appropriate.

---

## Project Structure

The project is organized around focused component data, centralized processing systems, supporting infrastructure, and Unity-facing integration.

```text
Assets/
└── Scripts/
    ├── Components/
    │   ├── BehaviorComponent.cs
    │   ├── DamageComponent.cs
    │   ├── DeathComponent.cs
    │   ├── ElementComponent.cs
    │   ├── HealthComponent.cs
    │   ├── InputComponent.cs
    │   ├── MovementComponent.cs
    │   └── ...
    │
    ├── DataStructures/
    │   └── SparseSet.cs
    │
    ├── Entities/
    │   ├── Entity.cs
    │   ├── EntityFactory.cs
    │   ├── EntityIdGenerator.cs
    │   └── EntityRegistry.cs
    │
    ├── Systems/
    │   ├── BehaviorSystem.cs
    │   ├── CombatSystem.cs
    │   ├── DamageSystem.cs
    │   ├── DeathSystem.cs
    │   ├── ElementSystem.cs
    │   ├── GameStateSystem.cs
    │   ├── HealthSystem.cs
    │   ├── MovementSystem.cs
    │   └── ScoreSystem.cs
    │
    ├── Bridges/
    │   ├── EntityBridge.cs
    │   └── CollisionBridge.cs
    │
    └── ...
```

The project structure separates:

- Gameplay data
- System processing
- Entity management
- Component storage
- Unity-facing integration

---

## Gameplay

BioBall is a 3D rolling survival game in which the player controls a plant-like sphere.

The objective is to:

- Roll through each level.
- Collect gems for points.
- Avoid and survive enemies.
- Reach the goal to advance.

### Play the Game

**Play BioBall in your browser:**

- [Play on Unity Play](https://play.unity.com/en/games/3d1ab859-707c-44e6-90b9-8dddc777cf41/bioball)
- [Play on itch.io](https://mercurymerc8.itch.io/bioball)

### Gameplay

<img src="Assets/gameplay/Bioball_v1.gif" alt="BioBall gameplay" width="600"/>

### Controls

| Input | Action |
|---|---|
| **W / Up Arrow** | Move forward |
| **S / Down Arrow** | Move backward |
| **A / D** or **Left / Right Arrow** | Rotate / steer |

---

## Tech Stack

- **C#**
- **Unity 6**
- **Data-Oriented Design Principles**
- **System-Focused Architecture**
- **Custom Sparse-Set-Style Component Storage**
- **Physics-Based Movement**
- **JSON Data Persistence**
- **WebGL**

---

## How to Run

1. Clone the repository:

```bash
git clone https://github.com/WGabrielHoward/Bioball-Architecture-Refactor.git
```

2. Open the project using Unity Hub.

3. Open the project using the Unity version specified in the project configuration.

4. Load the main scene.

5. Press **Play**.

---

## Screenshots

<img src="Assets/gameplay/screenshot3.png" alt="BioBall gameplay" width="400"/>

<img src="Assets/gameplay/screenshot2.png" alt="BioBall pause screen" width="400"/>

<img src="Assets/gameplay/screenshot1.png" alt="BioBall gameplay UI" width="400"/>

---

## License

This project is open source and available under the [MIT License](LICENSE).

---

## Credits & Assets

- [Yughues Free Ground Materials](https://assetstore.unity.com/packages/2d/textures-materials/nature/yughues-free-ground-materials-13001) by Nobiax / Yughues  
  Licensed under the Standard Unity Asset Store EULA.

Built by William G. Howard while studying Game Programming at DePaul University.
