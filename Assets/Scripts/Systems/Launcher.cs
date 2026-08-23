using UnityEngine;
using Scripts.Systems;
using Scripts.Entities_Sets;

[RequireComponent(typeof(PersistentData))]
[RequireComponent(typeof(BehaviorSystem))]
public class Launcher : MonoBehaviour
{
    public static Launcher Instance { get; private set; }

    public PersistentData Persistent { get; private set; }
    // Systems
    public BehaviorSystem BehaviorSystem { get; private set; }
    public DamageSystem DamageSystem { get; private set; }
    public GameStateSystem GameStateSystem { get; private set; }
    public HealthSystem HealthSystem { get; private set; }
    public MovementSystem MovementSystem { get; private set; }
    public ScoreSystem ScoreSystem { get; private set; }
    public CombatSystem CombatSystem { get; private set; }
    public DeathSystem DeathSystem { get; private set; }
    public ElementSystem ElementSystem { get; private set; }

    public EntityRegistry EntityRegistry { get; private set; }
    public LevelManager LevelManager { get; private set; }

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

        GameStateSystem = new GameStateSystem();
        ScoreSystem = new ScoreSystem();
         
        Persistent = gameObject.GetComponent<PersistentData>();
         
        LevelManager    = new LevelManager   ();    // Req ScoreS, GameSS
        DamageSystem    = new DamageSystem   ();    // Req levMan 
        BehaviorSystem  = gameObject.GetComponent<BehaviorSystem>();
        HealthSystem    = new HealthSystem   ();
        MovementSystem  = new MovementSystem (); 
        CombatSystem    = new CombatSystem   ();
        DeathSystem     = new DeathSystem    ();
        ElementSystem   = new ElementSystem  ();
        EntityRegistry  = new EntityRegistry ();
        

    }

    public void Update()
    {
        LevelManager.Update();

    }

    public void FixedUpdate()
    {
        MovementSystem.FixedUpdate();
        DeathSystem.FixedUpdate();
    }

    private void OnDestroy()
    {
       
    }
}
