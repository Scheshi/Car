using System;
using System.Collections.Generic;
using UnityEngine;


public class GameUpdater : MonoBehaviour
{
    public static GameUpdater Instance { get; private set; }

    private List<Action> _actions = new List<Action>();
    
    #region UnityMethods

    public GameUpdater()
    {
        Instance = this;
    }
    
    private void Start()
    {
        Instance ??= this;
    }

    private void Update()
    {
        for (int i = 0; i < _actions.Count; i++)
        {
            _actions[i]?.Invoke();
        }
    }

    #endregion


    #region Methods

    
    public void Add(Action controller)
    {
        _actions.Add(controller);
    }

    public void Remove(Action controller)
    { 
        _actions.Remove(controller);
    }
    
    
    #endregion

}
