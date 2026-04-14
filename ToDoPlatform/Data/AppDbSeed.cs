using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoPlatform.Models;

namespace ToDoPlatform.Data;

public class AppDbSeed
{
    public AppDbSeed(ModelBuilder builder)
    {
        #region Popular Perfis de usuários
        List<IdentityRole> roles = new()
        {
             new()
            {
                Id = "45a2cb91-04c5-4160-8447-7a47f7534c0a",
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            },
            new()
            {
                Id = "eb15bf5d-57f9-4bd1-b74f-4f5ef63555b6",
                Name = "Usuário",
                NormalizedName = "USUÁRIO"
            },
        };
        builder.Entity<IdentityRole>().HasData(roles);
        #endregion

        #region Popular dados de Usuário
        List<AppUser> users = new()
        {
            new AppUser()
            {
                 Id = "1b3a49bf-fe11-40f6-b8c0-af72ae166492",
                Email = "viniciusgodoy.martins@gmail.com",
                NormalizedEmail = "VINICIUSGODOY.MARTINS@GMAIL.COM",
                UserName = "viniciusgodoy.martins@gmail.com",
                NormalizedUserName = "VINICIUSGODOY.MARTINS@GMAIL.COM",
                LockoutEnabled = false,
                EmailConfirmed = true,
                Name = "Vinicius Godoy Martins",
                ProfilePicture = "/img/users/07c08a8c-48a4-4cde-b17f-7fedf5a95c79.png"
            },
            new AppUser()
            {
                Id = "70e7ccf8-33c0-4f79-9022-d64f56187821",
                Email = "anthonyserrano894@gmail.com",
                NormalizedEmail = "ANTHONYSERRANO894@GMAIL.COM",
                UserName = "anthonyserrano894@gmail.com",
                NormalizedUserName = "ANTHONYSERRANO894@GMAIL.COM",
                LockoutEnabled = true,
                EmailConfirmed = true,
                Name = "Anthony Rodrigues da Silva",
                ProfilePicture = "/img/users/41e8ef46-4c13-43cb-9b7e-7222642df441.png"
            }
        };

        foreach (var user in users)
        {
            PasswordHasher<IdentityUser> pass = new();
            user.PasswordHash = pass.HashPassword(user, "123456");
        }
        builder.Entity<AppUser>().HasData(users);
        #endregion

        #region Popular Dados de Usuário Perfil
        List<IdentityUserRole<string>> userRoles = new()
        {
            new IdentityUserRole<string>()
            {
                UserId = users[0].Id,
                RoleId = roles[0].Id
            },
            new IdentityUserRole<string>()
            {
                UserId = users[1].Id,
                RoleId = roles[1].Id
            },
        };
        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        #endregion

        #region Popular as Tarefas do usuário
        List<ToDo> toDos = new()
        {
            new ToDo()
            {
                Id = 1,
                Title = "Fazer redação da Meire",
                Description = "Produzir uma redação modelo ENEM.",
                UserId = users[0].Id
            },

            new ToDo()
            {
                Id = 2,
                Title = "Ligar na barbearia",
                Description = "Agendar corte de cabelo.",
                UserId = users[1].Id
            },

            new ToDo()
            {
                Id = 3,
                Title = "Estudar prova",
                Description = "Estudar para prova de Matemática.",
                UserId = users[1].Id
            },

        };
        builder.Entity<ToDo>().HasData(toDos);
        #endregion
    }
}