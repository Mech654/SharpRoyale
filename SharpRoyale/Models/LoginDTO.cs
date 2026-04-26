using System.ComponentModel.DataAnnotations;

namespace SharpRoyale.Models;

public record LoginDTO(
    [MinLength(3), MaxLength(10)]
    string Username, 
    
    [MinLength(6), MaxLength(20)]
    string Password
);