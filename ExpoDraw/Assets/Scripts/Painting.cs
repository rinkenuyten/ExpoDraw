using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class Painting
	{
		public string Name {get;set;}
		public List<Opdracht> Opdrachten {get; set;}

		public Painting (string name, List<Opdracht> opdrachten)
		{
			Name = name;
			Opdrachten = opdrachten;
		}
	}
}

