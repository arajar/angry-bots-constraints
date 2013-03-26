using System.Collections.Generic;
using System.Linq;
using ConstraintThingy;
using UnityEngine;

public class LevelPopulator : MonoBehaviour
{
    // finite domain that represents all possible types of content
    public static readonly ConstraintThingy.FiniteDomain<ContentType> ContentTypes = new ConstraintThingy.FiniteDomain<ContentType>(
        ContentType.Spider,
        ContentType.EnemyMech,
        ContentType.ConfusedMech,
        ContentType.Buzzers,
        ContentType.LargeBarrel,
        ContentType.SmallBarrel,
        ContentType.CrateA,
        ContentType.CrateATall,
        ContentType.CrateSmall,
        ContentType.SmallHealth,
        ContentType.MediumHealth,
        ContentType.LargeHealth, 
        ContentType.None);

    private readonly Dictionary<Room, SpawnPoint[]> _roomToSpawnPoints = new Dictionary<Room, SpawnPoint[]>();

    private readonly Dictionary<Room, GUIConstraint[]> _roomsToConstraints = new Dictionary<Room, GUIConstraint[]>();

    private GUIConstraint[] _globalConstraints;

    public GameObject Root;

    void Start()
    {
        _globalConstraints = GetComponents<GUIConstraint>();

        var rooms = Root.GetComponentsInChildren<Room>();

        foreach (var room in rooms)
        {
            _roomToSpawnPoints[room] = room.GetComponentsInChildren<SpawnPoint>();
            _roomsToConstraints[room] = room.GetComponents<GUIConstraint>();
        }
    }  


    private void Solve()
    {
        ConstraintThingySolver solver = new ConstraintThingySolver()
            {
                ExpansionOrder = ExpansionOrder.Random
            };

        Dictionary<SpawnPoint, FiniteDomainVariable<ContentType>> spawnPointToVariable = new Dictionary<SpawnPoint, FiniteDomainVariable<ContentType>>();

        List<FiniteDomainVariable<ContentType>> allVariables = new List<FiniteDomainVariable<ContentType>>();

        foreach (var roomToSpawnPoint in _roomToSpawnPoints)
        {
            var room = roomToSpawnPoint.Key;

            var spawnPoints = roomToSpawnPoint.Value;

            FiniteDomainVariable<ContentType>[] variables = new FiniteDomainVariable<ContentType>[spawnPoints.Length];

            for (int i = 0; i < variables.Length; i++)
            {
                variables[i] = solver.CreateFiniteDomainVariable(ContentTypes);
                spawnPointToVariable[spawnPoints[i]] = variables[i];
            }

            foreach (var constraint in _roomsToConstraints[room])
            {
                constraint.Apply(variables);
            }

            allVariables.AddRange(variables);
        }

        foreach (var constraint in _globalConstraints)
        {
            constraint.Apply(allVariables.ToArray());
        }

        if (solver.Solutions.Any())
        {
            foreach (var kvp in spawnPointToVariable)
            {
                kvp.Key.Instantiate(kvp.Value.UniqueValue);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Solve();
        }
    }
}