using UnityEngine;
using System.Collections;

public class CannonTower : UpdateManager
{
    [SerializeField] private float m_shootInterval = 0.5f;
    [SerializeField] private float m_range = 4f;
    [SerializeField] private GameObject m_projectilePrefab;
    [SerializeField] private Transform m_shootPoint;
    [SerializeField] private float m_turnSpeed = 5f;
    [SerializeField] private float m_lastShotTime = -0.5f;

    public override void GameUpdate()
    {
        if (m_projectilePrefab != null && m_shootPoint != null)
            SearchNearEnemy();
    }

    private void Shot(Monster monster)
    {
        var projectile = Instantiate(m_projectilePrefab, m_shootPoint.position, m_shootPoint.rotation)
            .GetComponent<CannonProjectile>();
        projectile.m_target = monster.gameObject;
    }
    private void LockOnTarget(Transform target)
    {
        var dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        var rot = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * m_turnSpeed);

        transform.rotation = rot;
    }
    private void SearchNearEnemy()
    { 
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (var monster in Spawner.monsters)
        {
            if (monster != null)
            {
                float distanceToEnemy = Vector3.Distance(m_shootPoint.transform.position, monster.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = monster.transform;
                }
            }
            if (nearestEnemy != null && shortestDistance < m_range)
            {
                LockOnTarget(nearestEnemy);

                if (m_lastShotTime + m_shootInterval > Time.time)
                    continue;
                if (Quaternion.Dot(transform.rotation, nearestEnemy.transform.rotation) >= 0.5f)
                    Shot(monster);
            }
            m_lastShotTime = Time.time;
        }
    }
}
