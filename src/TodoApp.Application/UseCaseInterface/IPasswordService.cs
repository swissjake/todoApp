using System;

namespace TodoApp.Application.UseCaseInterface;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}
