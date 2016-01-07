using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Opdracht 
{
	public string Name {get; private set; }
    public string Description {get; private set; }
    public List<int> Sizes {get; private set; }
    public List<Texture> Brush {get; private set; }
    public List<Color> Colors {get; private set; }

	public Opdracht(string name, string description, List<int> sizes, List<Texture> brush, List<Color> colors)
    {
		Name = name;
        Description = description;
        Sizes = sizes;
        Brush = brush;
        Colors = colors;
    }

}
