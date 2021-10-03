using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITemperature
{
    float Temperature { get; }
    float HeatProp { get; }
    float CoolProp { get; }
    float AlterTemperature(float i_change);
}
