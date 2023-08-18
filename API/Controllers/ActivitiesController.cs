using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Persistence;
using MediatR;
using Application.Activities;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet] //api/activities
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")] //api/activities/wpmvoseml
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            // Specifying curly brackets initializes an object during class instantiation 
            return await Mediator.Send(new Details.Query{Id = id});
        }
        
        /// <summary>
        ///     Since this controller class inherietes from BaseApiController, which has the [ApiController] attribute,
        ///     when an object is sent through the request body, the code is smart enough 
        ///     to look in the method's parameter to get the needed object, and if the properties available match the expected object. 
        ///     
        ///     Note: Could also add attribute to be more explicit. E.g. CreateActivity([FromBody]Activity activity)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody]Activity activity)
        {
            return Ok(await Mediator.Send(new Create.Command{Activity = activity}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(Guid id, Activity activity)
        {
            activity.Id = id;
            return Ok(await Mediator.Send(new Edit.Command{Activity = activity}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command{Id = id}));
        }
    }
}