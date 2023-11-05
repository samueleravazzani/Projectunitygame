using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Medicine Card", menuName = "Medicine_Card")]
public class Medicine_Card : ScriptableObject
{
    public new string name;
    public string drug_class;
    public string drug_indication;
    public string drug_warnings;
    public Sprite drug_image;
    
}
