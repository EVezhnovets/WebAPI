namespace my_books_tests
{
    public class PublisherServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbTest")
            .Options;

        AppDbContext context;
        PublisherService? publisherService; 


        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedAppDbContext.SeedDatabase(context);
            publisherService = new PublisherService(context);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        [Test, Order(1)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearching_WithNoPageNumber_Test()
        {
            var result = publisherService!.GetAllPublishers("", "", null);

            Assert.That(result.Count, Is.EqualTo(5));
        }

        [Test, Order(2)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearching_WithPageNumber_Test()
        {
            var result = publisherService?.GetAllPublishers("", "", 2);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test, Order(3)]
        public void GetAllPublishers_WithNoSortBy_WithSearching_WithNoPageNumber_Test()
        {
            var result = publisherService?.GetAllPublishers("", "3", null);

            Assert.That(result?.Count, Is.EqualTo(1));
            Assert.That(result?.FirstOrDefault()?.Name, Is.EqualTo("Publisher 3"));
        }

        [Test, Order(4)]
        public void GetAllPublishers_WithSortBy_WithNoSearching_WithNoPageNumber_Test()
        {
            var result = publisherService?.GetAllPublishers("name_desc", "", null);

            Assert.That(result?.Count, Is.EqualTo(5));
            Assert.That(result?.FirstOrDefault()?.Name, Is.EqualTo("Publisher 6"));
        }

        [Test, Order(5)]
        public void GetPublisherById_One_Test()
        {
            var result = publisherService?.GetPublisherById(1);

            Assert.That(result?.Id, Is.EqualTo(1));
        }

        [Test, Order(6)]
        public void GetPublisherById_NullZero_Test()
        {
            var result = publisherService?.GetPublisherById(0);

            Assert.That(result!.Id, Is.EqualTo(0));
            Assert.That(result!.Name, Is.EqualTo(null));
        }

        [Test, Order(8)]
        public void AddPublisher_WithException_Test()
        {
            var newPublisher = new PublisherVM()
            {
                Name = "123 With Exception"
            };

            Assert.That(() => publisherService!.AddPublisher(newPublisher),
                Throws.Exception.TypeOf<PublisherNameException>().With.Message.EqualTo("Name starts with number"));
        }

        [Test, Order(9)]
        public void AddPublisher_WithoutException_Test()
        {
            var newPublisher = new PublisherVM()
            {
                Name = "Without Excetion"
            };

            var result = publisherService!.AddPublisher(newPublisher);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Does.StartWith("Without"));
            Assert.That(result.Id, Is.Not.EqualTo(0));
        }

        [Test, Order(10)]
        public void GetPublisherData_Test()
        {
            var result = publisherService!.GetPublisherData(1);

            Assert.That(result.Name, Is.EqualTo("Publisher 1"));
            Assert.That(result.BookAuthors, Is.Not.Empty);
            Assert.That(result.BookAuthors.Count, Is.GreaterThan(0));

            var firstBookName = result.BookAuthors!.OrderBy(n => n.BookName).FirstOrDefault().BookName;
            Assert.That(firstBookName, Is.EqualTo("1st Book Title"));
        }
    }
}