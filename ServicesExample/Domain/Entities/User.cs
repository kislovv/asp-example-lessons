﻿namespace ServicesExample.Domain.Entities;

public abstract class User
{
    public long? Id { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public required UserRole Role { get; set; }

    public bool IsDeleted { get; set; }
}