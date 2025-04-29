using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace nzWalksApi.Controllers
{
    // https:localhost:port/api/Students 
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {


        // GET : // https:localhost:port/api/Students 
        [HttpGet]
        public IActionResult GetAllStudents()
        {

            string[] studentNames = new string[]
            {
                "john","jane","kelly"
            };

            return Ok(studentNames);

        }

    }
}
