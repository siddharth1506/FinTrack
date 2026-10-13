using System.Net.Mail;
using FinTrack.Domain.Common;

namespace FinTrack.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;

    private User(){}

    public User(string fullname, string email, string passwrdHash)
    {
        if(string.IsNullOrWhiteSpace(fullname))
        throw new ArgumentException("name can not be null", nameof(fullname));
             if (!MailAddress.TryCreate(email, out _))
            throw new ArgumentException("A valid email is required.", nameof(email));
         if(string.IsNullOrWhiteSpace(passwrdHash))
        throw new ArgumentException("Password can not be null", nameof(passwrdHash));
      FullName = fullname.Trim();
        Email = email.Trim().ToLowerInvariant();
        PasswordHash = passwrdHash;
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("Password hash is required.", nameof(newPasswordHash));

        PasswordHash = newPasswordHash;
        MarkUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;
        MarkUpdated();
    }
}