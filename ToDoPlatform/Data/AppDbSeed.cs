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
               Id = "65b7ff98-b501-47f1-a5b9-f33e0af5b016",
               Name = "Administrador",
               NormalizedName = "ADMINISTRADOR"
            },
            new()
            {
               Id = "40446663-5c11-4fa6-a96a-33095f5558b0",
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
                Title = "Terminar os PTDs",
                Description = "Finalizar até 11/03",
                UserId = users[0].Id
            },
            new ToDo()
            {
                Id = 2,
                Title = "Preparar material de revisão de MVC",
                Description = "Preparar apostila para alunos de MVC",
                UserId = users[0].Id
            },
            new ToDo()
            {
                Id = 3,
                Title = "Ligar no SAAE",
                Description = "Solicitar conserto do hidrometro",
                UserId = users[1].Id
            },
        };
        builder.Entity<ToDo>().HasData(toDos);
        #endregion
    }
}
