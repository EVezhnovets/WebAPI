using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json.Linq;
using WebAPI.Controllers;

namespace my_books_tests
{
    public class PublisherControllerTest
    {
        private AppDbContext _context;
        private PublisherController? _publisherController;
        private PublisherService? _publisherService;

        private static DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbTest")
            .Options;

        [OneTimeSetUp]
        public void SetUp()
        {
            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();

            SeedAppDbContext.SeedDatabase(_context);
            _publisherService = new(_context);
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [Test, Order(1)]
        public void HTTPGet_GetAllPublishers_WithNoSortBy_WithNoSearching_WithNoPageNumber_Test()
        {
            _publisherController = new(_publisherService!, new NullLogger<PublisherController>());
            var result = _publisherController!.GetAllPublishers(null, null, null);

            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var castResult = (result as ObjectResult).Value as List<Publisher>;
            Assert.That(castResult!.First().Name, Is.EqualTo("Publisher 1"));
            Assert.That(castResult!.First().Id, Is.EqualTo(1));
        }

        [Test, Order(2)]
        public void HTTPGet_GetAllPublishers_WithNoSortBy_WithNoSearching_WithPageNumber_Test()
        {
            _publisherController = new(_publisherService!, new NullLogger<PublisherController>());
            var result1 = _publisherController!.GetAllPublishers(null, null, 1);

            Assert.That(result1, Is.TypeOf<OkObjectResult>());

            var castResult1 = (result1 as ObjectResult).Value as List<Publisher>;
            Assert.That(castResult1!.First().Name, Is.EqualTo("Publisher 1"));
            Assert.That(castResult1!.Count, Is.EqualTo(5));


            var result2 = _publisherController!.GetAllPublishers(null, null, 2);

            Assert.That(result2, Is.TypeOf<OkObjectResult>());

            var castResult2 = (result2 as ObjectResult).Value as List<Publisher>;
            Assert.That(castResult2!.First().Name, Is.EqualTo("Publisher 6"));
            Assert.That(castResult2!.First().Id, Is.EqualTo(6));
        }

        [Test, Order(3)]
        public void HTTPGet_GetAllPublishers_WithSortBy_WithNoSearching_WithNoPageNumber_Test()
        {
            _publisherController = new(_publisherService!, new NullLogger<PublisherController>());
            var result = _publisherController!.GetAllPublishers("name_desc", null, null);

            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var castResult = (result as ObjectResult).Value as List<Publisher>;
            Assert.That(castResult!.First().Name, Is.EqualTo("Publisher 6"));
            Assert.That(castResult!.First().Id, Is.EqualTo(6));
        }

        [Test, Order(4)]
        public void HTTPGet_GetAllPublishers_WithNoSortBy_WithSearching_WithPageNumber_Test()
        {
            _publisherController = new(_publisherService!, new NullLogger<PublisherController>());
            var result = _publisherController!.GetAllPublishers(null, "Publisher 5", null);

            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var castResult = (result as ObjectResult).Value as List<Publisher>;
            Assert.That(castResult!.First().Name, Is.EqualTo("Publisher 5"));
            Assert.That(castResult!.First().Id, Is.EqualTo(5));
        }

        [Test, Order(5)]
        public void HTTPGet_GetPublisherById_OneId_Test()
        {
            _publisherController = new(_publisherService!, new NullLogger<PublisherController>());
            var result = _publisherController!.GetPublisherById(1);

            var castResult = (result as ObjectResult).Value as Publisher;
            Assert.That(castResult!.Name, Is.EqualTo("Publisher 1"));
            Assert.That(castResult!.Id, Is.EqualTo(1));
        }

        [Test, Order(6)]
        public void HTTPGet_GetPublisherById_ReturnNull_Test()
        {
            _publisherController = new(_publisherService!, new NullLogger<PublisherController>());
            var result = _publisherController!.GetPublisherById(77);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test, Order(7)]
        public void HTTPPost_AddPublisher_ReturnCreated_Test()
        {
            _publisherController = new(_publisherService!, new NullLogger<PublisherController>());

            var newPubisher = new PublisherVM() { Name = "Publisher 7" };

            var result = _publisherController!.AddPublisher(newPubisher);

            var newResult = (result as ObjectResult).Value as Publisher;
            Assert.That(result, Is.TypeOf<CreatedResult>());
            Assert.That(newResult.Id, Is.EqualTo(7));
        }

        [Test, Order(8)]
        public void HTTPPost_AddPublisher_BadRequest_Test()
        {
            _publisherController = new(_publisherService!, new NullLogger<PublisherController>());

            var newPubisher = new PublisherVM() { Name = "8Publisher" };

            var result = _publisherController!.AddPublisher(newPubisher);

            var newResult = (result as ObjectResult).Value as Publisher;
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test, Order(9)]
        public void HTTPDelete_DeletePublisherById_One_ReturnsOk_Test()
        {
            _publisherController = new(_publisherService!, new NullLogger<PublisherController>());
            var result = _publisherController!.DeletePublisherById(1);
           
            Assert.That(result, Is.TypeOf<OkResult>());

            var checkCollection = _publisherController!.GetAllPublishers(null, null, null);


            Assert.That(checkCollection, Is.TypeOf<OkObjectResult>());

            var castResult = (checkCollection! as ObjectResult).Value as List<Publisher>;
            Assert.That(castResult!.First().Name, Is.EqualTo("Publisher 2"));
        }

        [Test, Order(10)]
        public void HTTPDelete_DeletePublisherById_One_ReturnsBadRequest_Test()
        {
            _publisherController = new(_publisherService!, new NullLogger<PublisherController>());
            var result = _publisherController!.DeletePublisherById(77);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }
    }
}