﻿namespace Profex.Persistance.Dtos.Auth;

public class VerifyRegisterDto
{
    public string PhoneNumber { get; set; } = String.Empty;
    public int Code { get; set; }
}
