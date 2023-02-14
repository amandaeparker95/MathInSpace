using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //we don't need any pre-made functions, so we use this instead of monobehavior
public class Problem
{
    public float firstNumber;           // first number in the problem
    public float secondNumber;          // second number in the problem
    public MathsOperation operation;    // operator 
    public float[] answers;             //array of possible answers
    [Range(0, 3)]
    public int correctTube;             
}

public enum MathsOperation
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}