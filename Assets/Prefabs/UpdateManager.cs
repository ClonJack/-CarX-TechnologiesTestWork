using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public virtual void GameUpdate()
    {

    }

    private void Start() => StartUpdate();
    private void Update() => GameUpdate();

    public virtual void StartUpdate()
    {

    }

}
