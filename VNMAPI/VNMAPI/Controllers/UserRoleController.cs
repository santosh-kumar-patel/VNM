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
using Microsoft.OpenApi.Extensions;

namespace VNMAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserRoleController : ControllerBase
    {
        
        private readonly RoleManager<UserRole> _roleManager;
        private readonly UserManager<Employee> _userManager;
        public UserRoleController(RoleManager<UserRole> roleManager, UserManager<Employee> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        //Add UserRole
        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost("CreateUserRole")]
        public async Task<IActionResult> CreateUserRole([FromBody] RoleViewModel roleViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if the role already exists
                    bool roleExists = await _roleManager.RoleExistsAsync(roleViewModel.RoleName);
                    if (roleExists)
                        return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "Role already exists!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });

                    CurrentUser currentUser = new CurrentUser();
                    var obj = await currentUser.Get(_userManager, HttpContext);
                    if (obj == null)
                        return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "Current user doest not exists!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });
                    // Create the role
                    // We just need to specify a unique role name to create a new role
                    UserRole userRole = new UserRole
                    {
                        Name = roleViewModel.RoleName,
                        NormalizedName = roleViewModel.RoleName.ToUpperInvariant(),
                        CreatedBy = obj.Id
                    };

                    // Saves the role in the underlying AspNetRoles table
                    var result = await _roleManager.CreateAsync(userRole);

                    if (result.Succeeded)
                        return StatusCode(StatusCodes.Status201Created, new Response { Success = true, Message = "Role Created Successfully!", StatusCode = StatusCodes.Status201Created, Timestamp = DateTime.UtcNow });
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "Role failed to create!", StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow });
                }
                return BadRequest(ModelState);

                //await _userRoleService.AddUserRole(userRole);
                //return true;
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow }); }
        }

        //Delete UserRole
        [Authorize(Roles = AppRoles.Admin)]
        [HttpDelete("DeleteUserRole/{Id}")]
        public async Task<IActionResult> DeleteUserRole(string ID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if the role already exists
                    var getRole = await _roleManager.FindByIdAsync(ID);
                    if (getRole==null)
                        return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "Role does not exists!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });

                    IdentityResult result = await _roleManager.DeleteAsync(getRole);
                    if (result.Succeeded)
                        return StatusCode(StatusCodes.Status200OK, new Response { Success = true, Message = "Role Delated Successfully!", StatusCode = StatusCodes.Status200OK, Timestamp = DateTime.UtcNow });
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "Role failed to create!", StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow });
                }
                return BadRequest(ModelState);
                //    await _userRoleService.DeleteUserRole(Id);
                //return true;
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow }); }
        }

       //update User Role
        [Authorize(Roles = AppRoles.Admin)]
        [HttpPut("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole(string Id,[FromBody] RoleViewModel roleViewModel)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(Id);
                if (role == null)
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "Role doest not exists!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });
                CurrentUser currentUser = new CurrentUser();
                var obj = await currentUser.Get(_userManager, HttpContext);
                if (obj == null)
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "Current login User does not found!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });
                role.Name = roleViewModel.RoleName;
                role.NormalizedName = roleViewModel.RoleDescription;
                role.UpdatedBy = obj.Id;
                role.UpdatedOn = DateTime.Now;
                var result = await _roleManager.UpdateAsync(role);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "Role Name failed to update!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });
                var result1 = _roleManager.UpdateNormalizedRoleNameAsync(role);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "Role Description failed to update!", StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow });
                return StatusCode(StatusCodes.Status200OK, new Response { Success = true, Message = "Role Update Successfully!", StatusCode = StatusCodes.Status200OK, Timestamp = DateTime.UtcNow });
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow }); }
        }

        //Update to activate User Role
        [Authorize(Roles = AppRoles.Admin)]
        [HttpPut("ActivateUserRole/{Id}")]
        public async Task<IActionResult> ActivateUserRole(string Id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(Id);
                if (role == null)
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "Role doest not exists!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });
                CurrentUser currentUser = new CurrentUser();
                var obj = await currentUser.Get(_userManager, HttpContext);
                if (obj == null)
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "Current login User does not found!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });
                role.Status = true;
                role.UpdatedBy = obj.Id;
                role.UpdatedOn = DateTime.Now;
                var result = await _roleManager.UpdateAsync(role);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "Role failed to activate!", StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow });
                return StatusCode(StatusCodes.Status200OK, new Response { Success = true, Message = "Role Activated Successfully!", StatusCode = StatusCodes.Status200OK, Timestamp = DateTime.UtcNow });
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow }); }

        }

        //Update to deactivate User Role
        [Authorize(Roles = AppRoles.Admin)]
        [HttpPut("DeactivateroleRole/{Id}")]
        public async Task<IActionResult> DeactivateroleRole(string Id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(Id);
                if (role == null)
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "Role doest not exists!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });
                CurrentUser currentUser = new CurrentUser();
                var obj = await currentUser.Get(_userManager, HttpContext);
                if (obj == null)
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "Current login User does not found!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });
                role.Status = false;
                role.UpdatedBy = obj.Id;
                role.UpdatedOn = DateTime.Now;
                var result = await _roleManager.UpdateAsync(role);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = "Role failed to deactivate!", StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow });
                return StatusCode(StatusCodes.Status200OK, new Response { Success = true, Message = "Role Dectivated Successfully!", StatusCode = StatusCodes.Status200OK, Timestamp = DateTime.UtcNow });
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow }); }
        }

        //GET UserRole by Id
        [HttpGet("GetUserRoleById/{Id}")]
        public async Task<IActionResult> GetUserRoleById(string Id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(Id);
                if (role == null)
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "Role doest not exists!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });

                //var json = JsonConvert.SerializeObject(role, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings()
                //{
                //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                //});
                return StatusCode(StatusCodes.Status200OK, new Response { Success = true, Message = "User Role is fetched successfully!", Data = role, StatusCode = StatusCodes.Status200OK, Timestamp = DateTime.UtcNow });
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow }); }
        }


        //GET All UserRole
        [HttpGet("GetAllUserRole")]
        public IActionResult GetAllUserRole()
        {
            try
            {
                var role = _roleManager.Roles;
                if (role == null)
                    return StatusCode(StatusCodes.Status403Forbidden, new Response { Success = false, Message = "Role doest not exists!", StatusCode = StatusCodes.Status403Forbidden, Timestamp = DateTime.UtcNow });

                //var json = JsonConvert.SerializeObject(role, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings()
                //{
                //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                //});
                return StatusCode(StatusCodes.Status200OK, new Response { Success = true, Message = "User Roles are fetched successfully!", Data = role, StatusCode = StatusCodes.Status200OK, Timestamp = DateTime.UtcNow });
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, new Response { Success = false, Message = ex.Message, StatusCode = StatusCodes.Status500InternalServerError, Timestamp = DateTime.UtcNow }); }
        }
    }
}
