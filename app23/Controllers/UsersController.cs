using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using app23.Data;
using app23.Models;
using app23.DTOs;
using app23.Interfaces;
using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace app23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //wyswietalnie uzytkownikow
        [HttpGet]
        public async Task<ActionResult<List<UserShowDTO>>> GetUsers()
        {
            return await _userService.GetUsers();
        }


        //edytowanie uzytkownikow
        [HttpPut]
        public async Task<IActionResult> PutUser(int id, UserEditDTO user)
        {
            if (await _userService.UpdateUser(id, user))
            {
                var connectionString = "Endpoint=sb://pspraktyki.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccesKey;SharedAccessKey=z60u2t2Z3Hz1GZrItbTAKtqdZ/56HCLs9zLkmMeWuDc=;EntityPath=add-user-data";
                var client = new ServiceBusClient(connectionString);
                var sender = client.CreateSender("add-user-data");
                var body = JsonSerializer.Serialize(user);
                var message = new ServiceBusMessage(body);
                await sender.SendMessageAsync(message);
                return Ok();
            }
            else return BadRequest();
        }

        //dodawanie użytkowników
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserAddDTO user)
        {            
            if(await _userService.AddUser(user))
            {
                var connectionString = "Endpoint=sb://pspraktyki.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccesKey;SharedAccessKey=z60u2t2Z3Hz1GZrItbTAKtqdZ/56HCLs9zLkmMeWuDc=;EntityPath=add-user-data";
                var client = new ServiceBusClient(connectionString);
                var sender = client.CreateSender("add-user-data");
                var body = JsonSerializer.Serialize(user);
                var message = new ServiceBusMessage(body);
                await sender.SendMessageAsync(message);
                return Ok();
            } else
            {
                return BadRequest();
            }
        }

    }
}
