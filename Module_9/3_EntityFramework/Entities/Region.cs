using System;
namespace _3_EntityFramework.Entities
{
	public class Region
	{
		public int RegionID { get; set; }

		public string RegionDescription { get; set; }

		public IEnumerable<Territory> Territories { get; set; }
	}
}

