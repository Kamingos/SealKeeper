using Unity.Mathematics;

public interface IEntityMover
{
    // true, если до точки можно дойти и движение началось;
    // false, если дойти невозможно, тогда ничего не происходит
    bool Move(float3 pos);

    bool IsMoving { get; }
}
