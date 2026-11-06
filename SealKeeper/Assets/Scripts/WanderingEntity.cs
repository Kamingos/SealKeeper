using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public sealed class WanderingEntity : MonoBehaviour
{
    [SerializeField] private ConvexPolygonArea _area;

    private readonly MoveLogic _moveLogic = new MoveLogic();

    private void Awake()
    {
        var mover = new NavMeshEntityMover();
        mover.Init(GetComponent<NavMeshAgent>());

        _moveLogic.Init(mover, _area);
    }

    private void Update() => _moveLogic.Tick(Time.deltaTime);
}
