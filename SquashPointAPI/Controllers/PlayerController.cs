using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Interfaces;

namespace SquashPointAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController(IPlayerRepository playerRepository) : Controller
{  

}