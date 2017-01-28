using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Generic;
using UnityEngine;
using Util;

namespace World
{
    class Player : MonoBehaviour
    {
        Vector3 position;
        float movementSpeed = 1.0F;

        public void moveTo(float x, float y)
        {
            if (position.x < x)
            {
                position.x = position.x + movementSpeed;
            }
            else if (position.x > x)
            {
                position.x = position.x - movementSpeed;
            }

            if (position.y < y)
            {
                position.y = position.y + movementSpeed;
            }
            else if (position.y > y)
            {
                position.y = position.y - movementSpeed;
            }
        }

        public void moveToWithSpeed(float x, float y, float movementSpeed)
        {
            if (position.x < x)
            {
                position.x = position.x + movementSpeed;
            }
            else if (position.x > x)
            {
                position.x = position.x - movementSpeed;
            }

            if (position.y < y)
            {
                position.y = position.y + movementSpeed;
            }
            else if (position.y > y)
            {
                position.y = position.y - movementSpeed;
            }
        }

        public UnityEngine.Vector3 getPosition()
        {
            return position;
        }

        public void setPosition(UnityEngine.Vector3 position)
        {
            this.position = position;
        }

    }
}
