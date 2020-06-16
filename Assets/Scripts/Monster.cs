using UnityEngine;
using System.Collections;
using System;

public class Monster : UpdateManager
{
    public GameObject m_moveTarget;
    [SerializeField] private float m_speed = 0.1f;
    [SerializeField] private int m_maxHP = 30;
    [SerializeField] private const float m_reachDistance = 0.3f;
    public int m_hp;
 
    void Start()
    {
        m_hp = m_maxHP;
    }
    private void OnDestroy()=>Spawner.monsters.Remove(this);
    private void Move()
    {
        if (m_moveTarget != null)
        {
            if (Vector3.Distance(transform.position, m_moveTarget.transform.position) <= m_reachDistance)
            {
                Destroy(gameObject);
                return;
            }
            var translation = m_moveTarget.transform.position - transform.position;
            if (translation.magnitude > m_speed)
                translation = translation.normalized * m_speed * Time.deltaTime;

            transform.Translate(translation);
        }
    }

    public override void GameUpdate()
    {
         Move();
    }
}
