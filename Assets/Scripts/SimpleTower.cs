using UnityEngine;
using System.Collections;

public class SimpleTower : UpdateManager
{
    /// <summary>
    /// FindObjectOfType очень ресурсоемкий да и еще в каждом Update 
    /// Очень сильно FPS будет садиться 
    /// Логику такую как Shot по мне так лучше выносить в отдельный метод
    /// </summary>
    [SerializeField] private float m_shootInterval = 2;
    [SerializeField] private float m_range = 4f;
    [SerializeField] private GameObject m_projectilePrefab;
    [SerializeField] private float m_lastShotTime = -0.5f;


    public override void GameUpdate()
    {
        if (m_projectilePrefab != null)
            SearchNearEnemy();
    }
    private void Shot(Monster monster)
    {
        var projectile =
                 Instantiate(m_projectilePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity).GetComponent<GuidedProjectile>();
        projectile.m_target = monster.gameObject;

        m_lastShotTime = Time.time;
    }
    private void SearchNearEnemy()
    {
        foreach (var monster in Spawner.monsters)
        {
            if (monster != null)
            {
                if (Vector3.Distance(transform.position, monster.transform.position) > m_range)
                    continue;
                if (m_lastShotTime + m_shootInterval > Time.time)
                    continue;

                Shot(monster);
            }
        }

    }
    
}
