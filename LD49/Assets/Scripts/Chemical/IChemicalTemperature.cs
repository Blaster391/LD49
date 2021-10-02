using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChemicalTemperature
{
    float Temperature { get; }
    float AlterTemperature(float i_change);
}
