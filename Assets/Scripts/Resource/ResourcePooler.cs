using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ResourcePooler : MonoBehaviour
{
    public static ResourcePooler Instance { get; private set; }
    
    public List<ResourceObject> resourceObjectHolder;

    [field: SerializeField] public int DefaultPoolSize { get; protected set; } = 10;
    [field: SerializeField] public int MaxPoolSize { get; protected set; } = 100;

    protected Dictionary<ItemType, IObjectPool<GameObject>> pools = new();
    public IObjectPool<GameObject> this[ItemType resourceType]
    {
        get { return pools[resourceType]; }
    }

    public T Get<T>(ItemType resourceType) => pools[resourceType].Get().GetComponent<T>();

    [SerializeField] protected bool collectionChecks = true; // Collection checks will throw errors if we try to release an item that is already in the pool.


    protected virtual void Awake()
    {
        Instance = this;

        foreach (var resource in resourceObjectHolder)
        {
            pools[resource.type] = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(resource.prefab);
            }, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, DefaultPoolSize, MaxPoolSize);
        }
    }

    protected void OnReturnedToPool(GameObject gameObject)
    {
        gameObject.transform.parent = null;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.localRotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    protected void OnTakeFromPool(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    protected void OnDestroyPoolObject(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
