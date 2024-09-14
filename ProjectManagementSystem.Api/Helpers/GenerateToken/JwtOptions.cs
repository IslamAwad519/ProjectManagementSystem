﻿using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Api.Helpers.GenerateToken;
public class JwtOptions
{
    public static string SectionName { get; set; } = "Jwt";

    [Required]
    public string Issuer { get; init; } = string.Empty; 
    [Required]
    public string Audience { get; init; } = string.Empty;
    [Required]
    public string Key { get; init; } = string.Empty;
    [Range(1,int.MaxValue)]
    public int ExpirayMinutes { get; init; }
}
