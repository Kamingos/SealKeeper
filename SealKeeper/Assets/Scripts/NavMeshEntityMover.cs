using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public sealed class NavMeshEntityMover : IEntityMover
{
    private NavMeshAgent _agent;
    private readonly NavMeshPath _path = new NavMeshPath();

    public void Init(NavMeshAgent agent)
    {
        _agent = agent;
    }

    public bool IsMoving =>
        _agent.pathPending ||
        (_agent.hasPath && _agent.remainingDistance > _agent.stoppingDistance);

    public bool Move(float3 pos)
    {
        // привязываем точку к NavMesh, на случай если она чуть мимо
        if (!NavMesh.SamplePosition(pos, out var hit, 1f, NavMesh.AllAreas))
            return false;

        // проверяем, что дойти реально можно
        if (!_agent.CalculatePath(hit.position, _path) ||
            _path.status != NavMeshPathStatus.PathComplete)
            return false;

        return _agent.SetPath(_path);
    }
}
