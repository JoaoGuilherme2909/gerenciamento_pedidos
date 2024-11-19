using AutoMapper;
using gerenciamento_pedidos.api.Data;
using gerenciamento_pedidos.api.Dtos.User;
using gerenciamento_pedidos.api.Models;
using gerenciamento_pedidos.api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gerenciamento_pedidos.api.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserController: ControllerBase
{
    //   private readonly UserService _service;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly TokenService _tokenService;

    public UserController(TokenService tokenService ,IMapper mapper, UserManager<User> userManager, AppDbContext context, SignInManager<User> signInManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _context = context;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDTO userDTO)
    {
        User user = _mapper.Map<User>(userDTO);
        

        IdentityResult result = await _userManager.CreateAsync(user, userDTO.password);

        if (result.Succeeded) 
        {
            return Ok("Usuario cadastrado com sucesso!");
        }

        return BadRequest(result.Errors);
    }

    [HttpPut]
    public async Task<IActionResult> Login(UserLoginDTO loginDTO) 
    {
        var result = await _signInManager.PasswordSignInAsync(loginDTO.Username, loginDTO.Password, false, false);

        if (!result.Succeeded) 
        {
            return Unauthorized("Usuario nao autenticado. Parar imediatamente!");
        }
        var user = _signInManager.UserManager.Users.FirstOrDefault(u => u.NormalizedUserName == loginDTO.Username.ToUpper());
        
        return Ok(_tokenService.GenerateToken(user));
    }
}
