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

        public void Start()
        {
            OrderParty();
        }

        public void OrderParty()
        {
            Vector3 extents = new Vector3();
            if (Members.Count != null)
            {
                extents = Members.First().GetComponent<SpriteRenderer>().bounds.extents;
            }
            Vector3 position = NodeController.transform.localPosition;
            Vector3 position1;
            Vector3 position2;

            switch (Members.Count)
            {
                case 1:
                    Members.First().transform.localPosition = position;
                    break;
                case 2:
                    position1 = Members[0].transform.localPosition;
                    position1.x = position.x - extents.x;
                    Members[0].transform.localPosition.Set(position1.x, position.y, position.z);

                    position2 = Members[1].transform.localPosition;
                    position2.x = position.x + extents.x;
                    Members[1].transform.localPosition.Set(position2.x, position.y, position.z);
                    break;
                case 3:
                    position1 = Members[0].transform.localPosition;
                    position1.y = position.y + extents.y;
                    Members[0].transform.localPosition.Set(position.x, position1.y, position.z);

                    position2 = Members[1].transform.localPosition;
                    position2.y = position.y - extents.y;
                    position2.x = position.x + extents.x;
                    Members[1].transform.localPosition.Set(position2.x, position2.y, position.z);

                    var position3 = Members[2].transform.localPosition;
                    position3.y = position.y - extents.y;
                    position3.x = position.x - extents.x;
                    Members[2].transform.localPosition.Set(position3.x, position3.y, position.z);

                    break;
                default:
                    break;
            }
        }
    }
}
