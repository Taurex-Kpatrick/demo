using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

public class RegisterDTO
{


[Required]

public required string Username {get;set;}

[Required]
public required string Password {get;set;}

}