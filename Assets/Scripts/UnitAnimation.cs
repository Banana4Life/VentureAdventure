using System;
using Model;
using UnityEngine;

[Serializable]
public struct UnitAnimation
{
    public UnitType UnitType;
    public RuntimeAnimatorController Controller;
    public float speed;
}