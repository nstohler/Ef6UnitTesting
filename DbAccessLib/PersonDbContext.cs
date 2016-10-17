using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLib
{
	public class PersonDbContext : DbContext
	{
		public virtual DbSet<Person> PersonSet { get; set; }
	}
}
