using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Club", menuName = "Club")]
public class Club : ScriptableObject
{

    public ClubType type = ClubType.pudder;


    public float minForce, maxForce;
    public float minVertical, maxVertical;

    public ClubBody clubArtObject; 

}


public enum ClubType {
    pudder, 
    iron, 
    wedge, 
    driver, 
    theDon
}