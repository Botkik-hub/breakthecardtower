using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
// Only used "using" left, other is deleted

/*
 * Use multiple lines comments for long comments/documents
 *
 * This is INTERNAL file not needed in the build, or any other logic!!!
 * Here is explained base naming conventions for this project
 */

public enum EMyEnum
{
    ItemOne,
    ItemTwo
}

public interface IStandards
{
    // Try not to use shorten names
    void DoSomething();
}

// Class name is an UpperCase 
public class Standards : MonoBehaviour, IStandards
{
    // Base Inspector exposed field 
    [SerializeField] private int field1;
    // Use to split different section of the Inspector view  
    [Space] 
    // Use specific type of the prefab, instead of generic GameObject (if possible)
    [SerializeField] private Transform standardPrefab;
    
    // Base constant 
    private const float Float1 = 1.0f;

    // base private field
    private float _float1;
    
    // Try not to use shorten field names (not "pos")
    private Position _position;
    
    // Add readonly or const if possible
    private  readonly List<IStandards> _standards = new List<IStandards>();
    
    // Default comment deleted, added "private" 
    private void Start()
    {
        DoSomething();
        IsDoingSomething();
    }

    // Removed unused method Update()
    private bool IsDoingSomething()
    {
        return true;
    }
    
    
    // Try to write documentation/comments for public functions widely used outside of the class
    
    /*
     * This function does...
     */
    public void DoSomething()
    {
        // logic happens here
    }

    // use descriptive names for methods and passed arguments
    public void ArgumentMethod(int index)
    {
        // If there is a restriction  for the function arguments, throw error instead of simple return
        if (index < 0) throw new ArgumentException("index cannot be less then 0");

        DoSomething();
    }

    /*
     * Write a comment why this part of the code is commented
     * If it is old code that would never be used again, delete it, (it can be restored from the github if needed)
     */
    
    // private int BrokenMethod()
    // {
    //     var smells = 0;
    //     smells++;
    //     return smells;
    // }
}
