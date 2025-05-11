using UnityEngine;

public class AutoReturnToPool : MonoBehaviour
{
    private PoolingSystem pool;
    private float lifetime = 5f;

    public void Init(PoolingSystem system)
    {
        pool = system;
        CancelInvoke();
        Invoke(nameof(Return), lifetime);
    }

    private void Return()
    {
        pool.ReturnToPool(gameObject);
    }
}