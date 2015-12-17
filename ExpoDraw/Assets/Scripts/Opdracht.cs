using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Opdracht 
{
    public string Description {get; private set; }
    public List<int> Sizes {get; private set; }
    public List<Texture> Textures {get; private set; }
    public List<Color> Colors {get; private set; }

    public Opdracht( string description, List<int> sizes, List<Texture> textures, List<Color> colors)
    {
        Description = description;
        Sizes = sizes;
        Textures = textures;
        Colors = colors;
    }

}
