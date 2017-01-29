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

        public void Update()
        {
            Debug.Log("Order party!");
            OrderParty();
            Debug.Log("Party ordered?");
        }

        public void OrderParty()
        {
            Vector3 extents = new Vector3();
            Debug.Log("Members count: " + Members.Count);
            if (Members.Count != 0)
            {
                var spriteRenderer = Members[0].GetComponent<SpriteRenderer>();
                Sprite sprite = spriteRenderer.sprite;
                extents = spriteRenderer.bounds.extents; //spriteRenderer.bounds.extents;
                Debug.Log("extents: " + extents);
            }
            Vector3 position = NodeController.transform.localPosition;
            Vector3 position1;
            Vector3 position2;

            switch (Members.Count)
            {
                case 1:
                    Debug.Log("Yo 1");
                    Members[0].transform.localPosition = position;
                    break;
                case 2:
                    Debug.Log("Yo 2");
                    position1 = Members[0].transform.localPosition;
                    Members[0].transform.localPosition.Set(position.x - extents.x, position.y, position.z);

                    position2 = Members[1].transform.localPosition;
                    Members[1].transform.localPosition.Set(position.x + extents.x, position.y, position.z);
                    break;
                case 3:
                    Debug.Log("Yo 3");
                    position1 = Members[0].transform.localPosition;
                    Members[0].transform.localPosition.Set(position.x, position.y + extents.y, position.z);

                    position2 = Members[1].transform.localPosition;
                    Members[1].transform.localPosition.Set(position.x + extents.x, position.y - extents.y, position.z);

                    var position3 = Members[2].transform.localPosition;
                    Members[2].transform.localPosition.Set(position.x - extents.x, position.y - extents.y, position.z); 

                    break;
                default:
                    Debug.Log("Yo default");
                    break;
            }
        }
    }
}
