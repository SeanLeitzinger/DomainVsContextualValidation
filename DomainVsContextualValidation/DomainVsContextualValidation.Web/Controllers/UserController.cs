using DomainVsContextualValidation.Web.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DomainVsContextualValidation.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [Route("AddUserForFinancialApp")]
        [HttpPost]
        public async Task<IActionResult> AddForFinancialApp([FromBody]AddUserForFinancialAppRequest request)
        {
            return await request.Handle();
        }

        [Route("AddUserForEveryoneApp")]
        [HttpPost]
        public async Task<IActionResult> AddForEveryoneApp([FromBody]AddUserForEveryoneAppRequest request)
        {
            return await request.Handle();
        }
    }
}
