using System;
using Microsoft.EntityFrameworkCore;

namespace _3_EntityFramework.Models
{
	[Keyless]
	public class CustOrderHistProcedureResponse
    {
		public string ProductName { get; set; }

		public int Total { get; set; }
	}
}

