using System.Collections.Generic;
using UnityEngine;

public interface ITick
{
    void Tick();
}

public interface ITickService
{
    void Add(ITick tickable);
    void Remove(ITick tickable);
}

public class TickService : MonoBehaviour, ITickService
{
    private HashSet<ITick> _tickables = new HashSet<ITick>();
    private HashSet<ITick> _toDelete = new HashSet<ITick>();
    
    public void Add(ITick tickable)
    {
        _tickables.Add(tickable);
    }

    public void Remove(ITick tickable)
    {
        _toDelete.Add(tickable);
    }

    private void Awake()
    {
        gameObject.name = "TickService";
    }

    private void Update()
    {
        _tickables.RemoveWhere(x => _toDelete.Contains(x));
        _toDelete.Clear();

        foreach (var tickable in _tickables)
        {
            tickable.Tick();
        }
    }
}