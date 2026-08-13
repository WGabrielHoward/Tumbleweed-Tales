using UnityEngine;
using Scripts.Systems;
using Scripts.Data;
using Scripts.NPC;
using System.Collections.Generic;
using System.Data;
using Scripts.Entities_Sets;

[RequireComponent(typeof(PersistentData))]
[RequireComponent(typeof(BehaviorSystem))]
[RequireComponent(typeof(BehaviorSystem ))]
[RequireComponent(typeof(DamageSystem))]
[RequireComponent(typeof(GameStateSystem))]
[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(MovementSystem))]
[RequireComponent(typeof(ScoreSystem))]
[RequireComponent(typeof(CombatSystem))]
[RequireComponent(typeof(DeathSystem))]
[RequireComponent(typeof(ElementSystem))]
[RequireComponent(typeof(EntityRegistry))]
[RequireComponent(typeof(LevelManager))]

public class Launcher : MonoBehaviour
{
    public static Launcher Instance { get; private set; }

    // Persistent Data
    public PersistentData persistentData;

    // Systems
    public BehaviorSystem behaviorSystem;
    public DamageSystem damageSystem;
    public GameStateSystem gameStateSystem;
    public HealthSystem healthSystem;
    public MovementSystem movementSystem;
    public ScoreSystem scoreSystem;
    public CombatSystem combatSystem;
    public DeathSystem deathSystem;
    public ElementSystem elementSystem;

    public EntityRegistry entityRegistry;
    public LevelManager levelManager;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //Launch Systems
        LaunchSystems();

    }

    private void LaunchSystems()
    {
        PersistentData  persistentData  = gameObject.GetComponent<PersistentData >();
        BehaviorSystem  behaviorSystem  = gameObject.GetComponent<BehaviorSystem >();
        DamageSystem    damageSystem    = gameObject.GetComponent<DamageSystem   >();
        GameStateSystem gameStateSystem = gameObject.GetComponent<GameStateSystem>();
        HealthSystem    healthSystem    = gameObject.GetComponent<HealthSystem   >();
        MovementSystem  movementSystem  = gameObject.GetComponent<MovementSystem >();
        ScoreSystem     scoreSystem     = gameObject.GetComponent<ScoreSystem    >();
        CombatSystem    combatSystem    = gameObject.GetComponent<CombatSystem   >();
        DeathSystem     deathSystem     = gameObject.GetComponent<DeathSystem    >();
        ElementSystem   elementSystem   = gameObject.GetComponent<ElementSystem  >();
        EntityRegistry  entityRegistry  = gameObject.GetComponent<EntityRegistry >();
        LevelManager    levelManager    = gameObject.GetComponent<LevelManager   >();

    }

    private void OnDestroy()
    {
        
    }
}
