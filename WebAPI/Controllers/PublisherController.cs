using Microsoft.AspNetCore.Mvc;
using WebAPI.ActionResults;
using WebAPI.Data.Services;
using WebAPI.Data.ViewModels;
using WebAPI.Exceptions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private PublisherService _publisherService;

        public PublisherController(PublisherService publisherService)
        {
            _publisherService = publisherService;
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