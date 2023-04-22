using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/grades")]
    [Authorize]
    public class GradesController : ControllerBase
    {
        private readonly GradeService gradeService;

        public GradesController(GradeService gradeService)
        {
            this.gradeService = gradeService;
        }

        [HttpPost("add")]
        public IActionResult Add(GradeAddDto payload)
        {
            var result = gradeService.Add(payload);

            if (result == null)
            {
                return BadRequest("Class cannot be added");
            }

            return Ok(result);
        }

        [HttpGet("teacher-get-all")]
        [Authorize(Roles = "Teacher")]
        public IActionResult TeacherGetAll()
        {
            var results = gradeService.GetAll();

            return Ok(results);
        }

        [HttpGet("student-get-all")]
        [Authorize(Roles = "Student")]
        public IActionResult StudentGetAll()
        {
            var studentId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "studentId").Value);
            var results = gradeService.GetGradesByStudentId(studentId);

            return Ok(results);
        }

        [HttpGet("{studentId}/student-grades")]
        public IActionResult GetStudentGrades([FromRoute] int studentId)
        {
            var results = gradeService.GetGradesByStudentId(studentId);

            return Ok(results);
        }

        [HttpGet("{studentId}/grouped-grades")]
        public IActionResult GetGroupedGrades([FromRoute] int studentId)
        {
            var results = gradeService.GetGroupedGradesByStudentId(studentId);

            return Ok(results);
        }

        [HttpGet("{studentId}/grouped-grades-average")]
        public IActionResult GetGroupedGradesAverage([FromRoute] int studentId)
        {
            var result = gradeService.GetGroupedGradesAverage(studentId);
            return Ok(result);
        }
    }
}
