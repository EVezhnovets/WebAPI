using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebAPI.ActionResults;
using WebAPI.Data.Services;
using WebAPI.Data.ViewModels;
using WebAPI.Exceptions;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private PublisherService _publisherService;
        private readonly ILogger<PublisherController> _logger;
        public PublisherController(PublisherService publisherService, ILogger<PublisherController> logger)
        {
            _publisherService = publisherService;
            _logger = logger;
        }

        [HttpGet("get-all-publishers")]
        public ActionResult GetAllPublishers(string? sortBy, string? searchString, int? pageNumber) 
        {
            try
            {
                //Log.Information("This is just a log in GetAllPublishers()");
                _logger.LogInformation("This is just a log in GetAllPublishers()");
                var _result = _publisherService.GetAllPublishers(sortBy!, searchString!, pageNumber);
                return Ok(_result);
            }
            catch (Exception)
            {

                return BadRequest("Sorry, we could not load publishers");
            }
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publisherService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch (PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _response = _publisherService.GetPublisherById(id);

            if(_response != null)
            {
                return Ok(_response);
            }
            else
            {
                return NotFound();
            }
        }

        //CustomActionResult
        //[HttpGet("get-publisher-by-id/{id}")]
        //public IActionResult GetPublisherById(int id)
        //{
        //    var _response = _publisherService.GetPublisherById(id);

        //    if (_response != null)
        //    {
        //        var _responseObj = new CustomActionResultVM()
        //        {
        //            Publisher = _response
        //        };

        //        return new CustomActionResult(_responseObj);
        //    }
        //    else
        //    {
        //        var _responseObj = new CustomActionResultVM()
        //        {
        //            Exception = new Exception("This is coming from publishers controller")
        //        };

        //        return new CustomActionResult(_responseObj);
        //    }
        //}

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var response = _publisherService.GetPublisherData(id);
            return Ok(response);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publisherService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}