using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PadillionRadio.Business.Models;
using PadillionRadio.Business.Services;
using PadillionRadio.Helpers;
using PadillionRadio.Models;

namespace PadillionRadio.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Components.Route("[api/users]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> logger;
        private readonly IUserService userService;

        public ApiController(ILogger<ApiController> logger, IUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserModel>> Create(UserModel model)
        {
            var result = await userService.Create(await model.CodeGenerator(userService));

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserModel>> Delete(long id)
        {
            var result = await userService.Delete(id);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserModel>> Update(UserModel model)
        {
            var getModel = await userService.Get(model.Id);

            getModel.Code = string.IsNullOrWhiteSpace(model.Code)
                ? getModel.Code
                : model.Code;

            getModel.Email = string.IsNullOrWhiteSpace(model.Email)
                ? getModel.Email
                : model.Email;
            
            getModel.DeviceIdentifier = string.IsNullOrWhiteSpace(model.DeviceIdentifier)
                ? getModel.DeviceIdentifier
                : model.DeviceIdentifier;
            
            var result = await userService.Update(getModel);

            if (result == false)
            {
                return NoContent();
            }

            return Ok(getModel);
        }
        
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DeviceUpdateModel>> UpdateDeviceId (DeviceUpdateModel model)
        {
            var user = await userService.Get(model.Id);
            user.DeviceIdentifier = model.DeviceIdentifier;
            
            var result = await userService.Update(user);

            if (result == false)
            {
                return NoContent();
            }

            return Ok(model);
        }

        [HttpGet("[action]/{userId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserModel>> Get(long userId)
        {
            var result = await userService.Get(userId);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserModel[]>> GetList()
        {
            var result = await userService.GetList();

            if (result.Any() == false)
            {
                return NoContent();
            }

            return Ok(result);
        }
    }
}