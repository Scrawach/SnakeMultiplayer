using UnityEngine;

public class InputService
{
    private readonly Plane _ground;

    public InputService()
    {
        _ground = new Plane(Vector3.up, Vector3.zero);
    }
}
