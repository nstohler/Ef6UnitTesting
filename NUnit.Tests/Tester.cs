using DbAccessLib;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.Tests
{
	[TestFixture]
	public class Tester
	{
		[Test]
		public void SimpleTest()
		{
			Assert.IsTrue(true);
		}

		[Test]
		public void TestAddUsingNSubstitute_TestingExtensions()
		{
			// https://github.com/scott-xu/EntityFramework.Testing
			var data = new List<Person>
			{
				new Person { FirstName = "Hallo", LastName = "Velo", BirthDate = DateTime.Today, ShoeSizeUS = 0.0 },
				new Person { FirstName = "3232", LastName = "Vel5554o", BirthDate = DateTime.Today, ShoeSizeUS = 10.0 },
				new Person { FirstName = "444", LastName = "Ve2234lo", BirthDate = DateTime.Today, ShoeSizeUS = 20.0 },
			};

			var newPerson = new Person { PersonId = 111, FirstName = "TEST", LastName = "REST", BirthDate = DateTime.Today, ShoeSizeUS = 22.0 };
			//var newPerson = Substitute.For<Person>();
			//newPerson.PersonId.Returns(111);
			//newPerson.FirstName.Returns("TESTMOCK");

			// arrange
			//var set1 = Substitute.For<DbSet<Person>, IQueryable<Person>, IDbAsyncEnumerable<Person>>();
			//var set = set1.SetupData(data);

			var set = Substitute.For<DbSet<Person>>().SetupDataX(data);

			var context = Substitute.For<PersonDbContext>();
			context.PersonSet.Returns(set);

			var personDbService = new PersonDbService(context);

			// act
			var addedPerson = personDbService.AddPerson(newPerson);

			// assert
			Assert.That(addedPerson.PersonId, Is.EqualTo(111));
			Assert.That(data.Count(), Is.EqualTo(4));
			CollectionAssert.Contains(data, newPerson);

			context.Received(1).SaveChanges();
		}
	}

	public static class ExtentionMethods
	{
		public static DbSet<TEntity> SetupDataX<TEntity>(this DbSet<TEntity> dbSet, ICollection<TEntity> data) where TEntity : class
		{
			return Substitute.For<DbSet<TEntity>, IQueryable<TEntity>, IDbAsyncEnumerable<TEntity>>().SetupData(data);
			// return Substitute.For<DbSet<TEntity>>().SetupData(data);
		}
	}
}
