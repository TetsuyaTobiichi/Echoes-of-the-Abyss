using Components;
using Components.Container;
using Leopotam.Ecs;
using Systems;
using UnityEngine;

public class ESCStartUp : MonoBehaviour
{
    private EcsWorld World;
    private EcsSystems UpdateSystems;
    private EcsSystems FixedUpdateSystems;

    [SerializeField]
    private PlayerData configuration;
    [SerializeField]
    private SceneData sceneData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private IObjectsContainer container;
    void Awake()
    {
        World = new();

        UpdateSystems = new(World);
        FixedUpdateSystems = new(World);
        InitPlayerUpdateSystems();
        InitPlayerFixedUpdateSystems();
        container = new ObjectsContainer();

        UpdateSystems.Inject(container);
        FixedUpdateSystems.Inject(container);

        UpdateSystems.Init();
        FixedUpdateSystems.Init();
    }

    private void InitPlayerUpdateSystems()
    {
        UpdateSystems
            .Add(new PlayerInitSystem())
            .Add(new PlayerInputSystem())
            .Add(new PlayerAttackSystem())
            .Add(new PlayerBlockAttackSystem())
            .Inject(configuration)
            .Inject(sceneData);
    }

    private void InitPlayerFixedUpdateSystems()
    {
        FixedUpdateSystems
            .Add(new PlayerMoveSystem())
            .Add(new PlayerJumpSystem());
    }
    // Update is called once per frame
    void Update()
    {
        UpdateSystems?.Run();
    }

    void FixedUpdate()
    {
        FixedUpdateSystems?.Run();
    }

    private void OnDestroy()
    {
        UpdateSystems?.Destroy();
        UpdateSystems = null;
        FixedUpdateSystems?.Destroy();
        FixedUpdateSystems = null;
        World?.Destroy();
        World = null;
    }
}
