using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

class PartyContainer : MonoBehaviour
{
    private readonly Dictionary<Unit, UnitVisualizer> _members = new Dictionary<Unit, UnitVisualizer>();

    public GameObject UnitVisualizerPrefab;
    
    public Party Party { get; set; }

    public void Update()
    {
        if (Party == null) return;

        foreach (var unit in Party)
        {
            if (unit.IsAlive && !_members.ContainsKey(unit))
            {
                var gameObj = Instantiate(UnitVisualizerPrefab);
                var visualizer = gameObj.GetComponent<UnitVisualizer>();
                visualizer.Unit = unit;
                gameObj.transform.SetParent(transform);
                _members.Add(unit, visualizer);
            }
            else if (!unit.IsAlive && _members.ContainsKey(unit))
            {
                _members.Remove(unit);
            }
        }

        if (Party.IsHidden)
        {
            foreach (var visualizer in _members.Values)
            {
                visualizer.Hidden = true;
            }
        }

        OrderParty();
    }

    public void OrderParty()
    {
        if (!_members.Any())  return;

        var extents =  _members.Values.First().GetComponent<SpriteRenderer>().bounds.extents; 
        
        switch (_members.Count)
        {
            case 1:
                _members.Values.First().transform.localPosition = Vector3.zero;
                break;
            case 2:
                _members.Values.First().transform.localPosition = new Vector3(-extents.x, 0.0F, 0.0F); 
                _members.Values.Last().transform.localPosition = new Vector3(extents.x, 0.0F, 0.0F); 
                break;
            case 3:
                var visualizer = _members.Values.ToArray();
                visualizer[0].transform.localPosition = new Vector3(0.0F, extents.y, 0.0F);
                visualizer[1].transform.localPosition = new Vector3(extents.x, -extents.y, 0.0F);
                visualizer[2].transform.localPosition = new Vector3(-extents.x, -extents.y, 0.0F);
                break;
        }
    }
}