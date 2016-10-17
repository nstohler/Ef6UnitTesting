using DbAccessLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess_con
{
	class Program
	{
		static void Main(string[] args)
		{
			var name = "nicolas";
			Console.WriteLine($"hello {name}");

			using (var context = new PersonDbContext())
			{
				var dbAccess = new PersonDbService(context);
				var personSet = dbAccess.GetAllPersons();

			}
		}
	}
}
