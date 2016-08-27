<Query Kind="Program">
  <NuGetReference>EntityFramework</NuGetReference>
  <NuGetReference>NBuilder</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations.Schema</Namespace>
  <Namespace>System.Data.Entity</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration.Conventions</Namespace>
  <Namespace>System.Data.EntityModel.SchemaObjectModel</Namespace>
</Query>

public interface IKarsEntity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	Guid Id { get; set; }
}
public interface IScientificEvent : IKarsEntity
{
	bool IsEditable { get; set; }

	DateTime CreatedDate { get; set; }

	DateTime UpdateDate { get; set; }
}
public class User : IKarsEntity
{
	public User()
	{
		//Books=new HashSet<Book>();
		//Articles=new HashSet<Article>();
	}
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }
	//public ICollection<Book> Books { get; set; }
	//public ICollection<Article> Articles { get; set; }
}
public abstract class Event : IKarsEntity
{
	public Event()
	{
	//	Authors=new HashSet<ScientificEventAuthor>();
	}
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }

	public Guid UserId { get; set; }
	[ForeignKey("UserId")]
	public virtual User User { get; set; }
	
	//public ICollection<ScientificEventAuthor> Authors { get; set; }
}

public class ScientificEventAuthor : IKarsEntity
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }

	public Guid ScientificEventId { get; set; }

	//[ForeignKey("ScientificEventId")]
	//public virtual Event Event { get; set; }

	public Guid UserId { get; set; }
	[ForeignKey("UserId")]
	public virtual User User { get; set; }
}
/*
public abstract class ScientificEvent : Event, IScientificEvent
{
	public ScientificEvent()
	{
		ScientificEventAuthors = new HashSet<ScientificEventAuthor>();
	}

	public bool IsEditable { get; set; }

	public DateTime CreatedDate { get; set; }

	public DateTime UpdateDate { get; set; }

	public ICollection<ScientificEventAuthor> ScientificEventAuthors { get; set; }
}
*/

public class Book : Event//ScientificEvent
{
	[Required]
	[StringLength(2000)]
	public string BookType { get; set; }
	[Required]
	public DateTime PublishingDate { get; set; }
}

public partial class Article : Event//ScientificEvent
{
	[Required]
	[StringLength(2000)]
	public string JournalName { get; set; }


	public Guid T2Id { get; set; }
	[ForeignKey("T2Id")]
	public virtual T2 T2 { get; set; }

}
public partial class T2 : IKarsEntity//ScientificEvent
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }
	[Required]
	[StringLength(2000)]
	public string TestName { get; set; }


}
string conStrTest = @"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=C:\Users\ufuko\Desktop\del\Test.mdf;integrated security=True;multipleactiveresultsets=True;application name=EntityFramework";
void Main()
{
	var db = new TestContext(conStrTest);
	
	//db.Database.Log=(o)=>Debug.Write(o);
	db.Database.Delete();
	if (db.Database.CreateIfNotExists())
	{
		"Created".Dump();
		while (true)
		{
			db.T2s.AddRange(FizzWare.NBuilder.Builder<T2>.CreateListOfSize(10).Build().ToList());
			db.Users.AddRange(FizzWare.NBuilder.Builder<User>.CreateListOfSize(10).Build().ToList());
			db.Events.AddRange(FizzWare.NBuilder.Builder<Book>.CreateListOfSize(10).Build().ToList());
			db.Events.AddRange(FizzWare.NBuilder.Builder<Article>.CreateListOfSize(10).Build().ToList());

			db.EventAuthors.AddRange(FizzWare.NBuilder.Builder<ScientificEventAuthor>.CreateListOfSize(10).Build().ToList());
			db.SaveChanges();
			var list=db.Users.ToList();
			db.ChangeTracker.Entries().Count().Dump();
			db.ChangeTracker.Entries().Any(o=>o.Entity==list.First()).Dump();
			db.ChangeTracker.Entries().ToList().ForEach(o=>{o.State=System.Data.Entity.EntityState.Detached;});
			Thread.Sleep(1000);
		}
    }
	
	//db.Books.ToList().Dump();
}

public class TestContext : DbContext
{
	public TestContext(string conStr) : base(conStr)
	{

	}

	public DbSet<T2> T2s { get; set; }
	public DbSet<Event> Events { get; set; }
	public DbSet<ScientificEventAuthor> EventAuthors { get; set; }
	//public DbSet<Book> Books { get; set; }
	//public DbSet<Article> Articles { get; set; }
	
	public DbSet<User> Users { get; set; }
	
	protected override void OnModelCreating(DbModelBuilder modelBuilder)
	{
		modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
		modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

		/*modelBuilder.Entity<Event>()
		.Map<Book>(m => m.Requires("BillingDetailType").HasValue("BA"))
		.Map<Article>(m => m.Requires("BillingDetailType").HasValue("CC"));
*/

		//modelBuilder.Entity<User>().HasMany(o=>o.Books).WithRequired().HasForeignKey(o=>o.UserId);
		//modelBuilder.Entity<User>().HasMany(o=>o.Articles).WithRequired().HasForeignKey(o=>o.UserId);
		
//		modelBuilder.Entity<Book>().Map(m => 
//		{
//		m.MapInheritedProperties();
//		m.ToTable("Book");
//        });
//		
//		modelBuilder.Entity<Article>().Map(m =>
//		{			
//			m.MapInheritedProperties();
//			m.ToTable("Article");
//		});
	//	modelBuilder.Entity<Book>().Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//		modelBuilder.Entity<Article>().Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
		
		//		modelBuilder.Entity<Book>()
		//			  .HasRequired(a => a.User)
		//			  .WithMany()
		//			  .HasForeignKey(u => u.UserId);
//
//		modelBuilder.Entity<Article>()
//			  .HasRequired(a => a.User)
//			  .WithMany()
//			  .HasForeignKey(u => u.UserId);
//		
	

		base.OnModelCreating(modelBuilder);
	}
}