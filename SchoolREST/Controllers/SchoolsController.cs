using Microsoft.AspNetCore.Mvc;
using SchoolLibary;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.Eventing.Reader;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {


        private TeacherRepository _teacherRepository;

        public SchoolsController(TeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        // GET: api/<SchoolsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Teacher>> Get()
        {
            IEnumerable<Teacher> teacher = _teacherRepository.get();
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        // GET api/<SchoolsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Teacher> Get(int id)
        {
            Teacher teacher = _teacherRepository.GetById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);

        }

        // POST api/<SchoolsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost]


        public ActionResult<Teacher> Post([FromBody] Teacher newTeacher)
        {

            try
            {
                Teacher createdTeacher = _teacherRepository.AddTeacher(newTeacher);
                return Created("/" + createdTeacher.Id, createdTeacher);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<SchoolsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
       
        public ActionResult<Teacher> Put(int id, [FromBody] Teacher teacher)
        {
            try
            {
                Teacher updatedTeacher = _teacherRepository.Update(id, teacher);
                if (updatedTeacher == null)
                {
                    return NotFound();
                }
                
                else
                {
                    return Ok(updatedTeacher);
                }
                
                
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE api/<SchoolsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]

        public ActionResult<Teacher> Delete(int id)
        {
            Teacher deletedTeacher = _teacherRepository.Remove(id);
            if (deletedTeacher == null)
            {
                return NotFound();
            }
            return Ok(deletedTeacher);
        }
}
}
