using BAL.DTOs;
using Core.Contstants;
using Core.Enums;
using Core.Models.Base;
using DAL.Entities;
using DAL.Entities.Base;
using DAL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace VNMAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class EmployeeController :ControllerBase
    {

        private readonly UserManager<Employee> _userManager;
        public EmployeeController(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }
            
        ////Add Employee
        //[HttpPost("AddEmployee")]
        //public async Task<Object> AddEmployee([FromBody] Employee employee)
        //{
        //    try
        //    {
        //        await _employeeService.AddEmployee(employee);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //Delete Employee
        [Authorize(Roles = AppRoles.Admin)]
        [HttpDelete("DeleteEmployee/{ID}")]
        public async Task<IActionResult> DeleteEmployee(string Id)
        {
            try
            {
                var userNameExist = await _userManager.FindByIdAsync(Id);
                if (userNameExist == null)
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "User doest not exists!" , StatusCode= StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });

                var result = await _userManager.DeleteAsync(userNameExist);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "User failed to delete!", StatusCode= StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow });
                return StatusCode(StatusCodes.Status200OK, new Response { Success = true, Message = "User Deleted Successfully!", StatusCode = StatusCodes.Status200OK, Timestamp = DateTime.UtcNow });
                
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = ex.Message, StatusCode= StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow }); }
        }

        //Update Employee
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(string Id, [FromBody] EmployeeViewModel employee)
        {
            try
            { 
                if(ModelState.IsValid)
                {
                    var user = await _userManager.FindByIdAsync(Id);
                    if (user == null)
                        return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "User doest not exists!" , StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });
                    CurrentUser currentUser=new CurrentUser();
                    var obj =await currentUser.Get(_userManager, HttpContext);
                    if(obj == null)
                        return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "Current login User does not found!" , StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });
                    user.UserName = employee.UserName;
                    user.Email = employee.Email;
                    user.PhoneNumber = employee.Phone;
                    user.Address = employee.Address;
                    user.DateOfJoing=employee.DateOfJoing;
                    user.DOB=employee.DOB;
                    user.UpdatedBy = obj.Id;
                    user.UpdatedOn=DateTime.Now;
                    var result = await _userManager.UpdateAsync(user);
                    if(!result.Succeeded)
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "User failed to update!", StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow });
                    return StatusCode(StatusCodes.Status200OK, new Response { Success = true, Message = "User Updated Successfully!" , StatusCode = StatusCodes.Status200OK, Timestamp = DateTime.UtcNow });
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow }); }
        }

        //Update to activate Employee
        [Authorize(Roles = AppRoles.Admin)]
        [HttpPut("ActivateEmployee/{Id}")]
        public async Task<IActionResult> ActivateEmployee(string Id)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(Id);
                if (user == null)
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "User doest not exists!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });
                CurrentUser currentUser = new CurrentUser();
                var obj = await currentUser.Get(_userManager, HttpContext);
                if (obj == null)
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "Current login User does not found!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });
                user.Status = true;
                user.UpdatedBy = obj.Id;
                user.UpdatedOn = DateTime.Now;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "User failed to activate!", StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow });
                return StatusCode(StatusCodes.Status200OK, new Response { Success = true, Message = "User Activated Successfully!" , StatusCode = StatusCodes.Status200OK, Timestamp = DateTime.UtcNow });
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow }); }
        }

        //Update to deactivate Employee
        [Authorize(Roles = AppRoles.Admin)]
        [HttpPut("DeactivateEmployee/{Id}")]
        public async Task<IActionResult> DeactivateEmployee(string Id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user == null)
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "User doest not exists!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });
                CurrentUser currentUser = new CurrentUser();
                var obj = await currentUser.Get(_userManager, HttpContext);
                if (obj == null)
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "Current login User does not found!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });
                user.Status = false;
                user.UpdatedBy = obj.Id;
                user.UpdatedOn = DateTime.Now;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "User failed to deactivate!", StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow });
                return StatusCode(StatusCodes.Status200OK, new Response { Success = true, Message = "User Deactivated Successfully!" , StatusCode = StatusCodes.Status201Created, Timestamp = DateTime.UtcNow });
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow }); }
        }

        //GET Employee by Id
        [HttpGet("GetEmployeeById/{Id}")]
        public async Task<IActionResult> GetEmployeeById(string Id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user == null)
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "User doest not exists!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });

                //var json = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings()
                //{
                //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                //});
                return StatusCode(StatusCodes.Status200OK, new Response { Success = true, Message = "User is fetched successfully!", Data = user, StatusCode = StatusCodes.Status200OK, Timestamp = DateTime.UtcNow });
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow }); }
        }


        //GET All Employee
        [HttpGet("GetAllEmployee")]
        public IActionResult GetAllEmployee()
        {
            try
            {
                var user =   _userManager.Users;
                if (user == null)
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "User doest not exists!",StatusCode= StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });

                //var json = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings()
                //{
                //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                //});
                return StatusCode(StatusCodes.Status200OK, new Response { Success = false, Message = "Users are fetched successfully!", Data = user });
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow }); }
        }
    }
}
