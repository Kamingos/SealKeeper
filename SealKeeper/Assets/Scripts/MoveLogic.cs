using UnityEngine;

public sealed class MoveLogic
{
    private IEntityMover _mover;
    private ConvexPolygonArea _area;
    private float _minPause, _maxPause;
    private float _pauseTimer;

    public void Init(IEntityMover mover, ConvexPolygonArea area,
                     float minPause = 0.5f, float maxPause = 2f)
    {
        _mover = mover;
        _area = area;
        _minPause = minPause;
        _maxPause = maxPause;
    }

    public void Tick(float deltaTime)
    {
        if (_mover.IsMoving) return;

        _pauseTimer -= deltaTime;
        if (_pauseTimer > 0f) return;

        // несколько попыток: точка могла оказаться недостижимой
        for (int i = 0; i < 5; i++)
        {
            if (_mover.Move(_area.GetRandomPoint()))
            {
                _pauseTimer = Random.Range(_minPause, _maxPause);
                return;
            }
        }
    }
}
