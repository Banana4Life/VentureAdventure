using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using World;

namespace Model
{
    class PartyContainer : MonoBehaviour
    {
        public List<UnitVisualizer> Members = new List<UnitVisualizer>();
        public NodeController NodeController { get; set; }
        public bool IsHiddenParty { get; set; }
        public Node Node { get; set; }


        //TODO Should be Start()
        public void Update()
        {
            OrderParty();
        }

        public void OrderParty()
        {
            Vector3 extents = new Vector3();
            if (Members.Count != 0)
            {
                var spriteRenderer = Members[0].GetComponent<SpriteRenderer>();
                extents = spriteRenderer.bounds.extents; 
            }

            Vector3 centerPosition = NodeController.transform.localPosition;
            foreach (var member in Members)
            {
                member.transform.localPosition = centerPosition;
            }

            Vector3 orderingVector;
            switch (Members.Count)
            {
                case 1:
                    Members[0].transform.localPosition = centerPosition;
                    break;
                case 2:
                    orderingVector = new Vector3(extents.x * (-1.0F), 0.0F, 0.0F);
                    Members[0].transform.localPosition += orderingVector;
                    
                    orderingVector = new Vector3(extents.x, 0.0F, 0.0F);
                    Members[1].transform.localPosition += orderingVector;
                    break;
                case 3:
                    orderingVector = new Vector3(0.0F, extents.y, 0.0F);
                    Members[0].transform.localPosition += orderingVector;
                    
                    orderingVector = new Vector3(extents.x, extents.y * (-1.0F), 0.0F);
                    Members[1].transform.localPosition += orderingVector;
                    
                    orderingVector = new Vector3(extents.x * (-1.0F), extents.y * (-1.0F), 0.0F);
                    Members[2].transform.localPosition += orderingVector;
                    break;
                default:
                    break;
            }
        }
    }
}
