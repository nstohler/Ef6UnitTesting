using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLib
{
	public class PersonDbService
	{
		private readonly PersonDbContext _personDbContext;

		public PersonDbService(PersonDbContext personDbContext)
		{
			_personDbContext = personDbContext;
		}

		public Person AddPerson(Person p)
		{
			var r = _personDbContext.PersonSet.Add(p);
			_personDbContext.SaveChanges();

			return r;
		}

		public List<Person> GetAllPersons()
		{
			var query = _personDbContext.PersonSet.AsNoTracking()
						.OrderBy(x => x.LastName);

			return query.ToList();
		}
	}
}
