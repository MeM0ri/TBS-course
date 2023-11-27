using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour, IDestructible
{
    public static event EventHandler OnAnyDestroyed;

    [SerializeField] private Transform wallDestoryedPrefab;

    private GridPosition gridPosition;

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
    }

    public GridPosition GetGridPosition() => gridPosition;

    public void Damage()
    {
        Transform wallDestroyedTransform = Instantiate(wallDestoryedPrefab, transform.position, transform.rotation);

        ApplyExplosionToChildren(wallDestroyedTransform, 250f, transform.position, 20f);

        Destroy(gameObject);

        OnAnyDestroyed?.Invoke(this, EventArgs.Empty);
    }

    public void Destruct()
    {
        Transform wallDestroyedTransform = Instantiate(wallDestoryedPrefab, transform.position, transform.rotation);

        ApplyExplosionToChildren(wallDestroyedTransform, 150f, transform.position, 10f);

        Destroy(gameObject);

        OnAnyDestroyed?.Invoke(this, EventArgs.Empty);
    }

    private void ApplyExplosionToChildren(Transform root, float explosionForce, Vector3 explosionPosition, float explosionRange)
    {
        foreach (Transform child in root)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
            {
                childRigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRange);
            }

            ApplyExplosionToChildren(child, explosionForce, explosionPosition, explosionRange);
        }
    }
}
