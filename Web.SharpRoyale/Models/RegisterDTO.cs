using System.ComponentModel.DataAnnotations;

namespace Web.SharpRoyale.Models;

public record RegisterDTO(
    [MinLength(3), MaxLength(10)]
    string Username,

    [MinLength(6), MaxLength(20)]
    string Password
);
