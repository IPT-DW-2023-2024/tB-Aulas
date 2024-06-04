using Aulas.Models;
using Microsoft.AspNetCore.Identity;


namespace Aulas.Data {

    internal class DbInitializer {

        internal static async void Initialize(ApplicationDbContext dbContext) {

            /*
             * https://stackoverflow.com/questions/70581816/how-to-seed-data-in-net-core-6-with-entity-framework
             * 
             * https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-6.0#initialize-db-with-test-data
             * https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/data/ef-mvc/intro/samples/5cu/Program.cs
             * https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/ide0300
             */


            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();

            // var auxiliar
            bool haAdicao = false;



            // Se não houver Cursos, cria-os
            var cursos = Array.Empty<Cursos>();
            if (!dbContext.Cursos.Any()) {
                cursos = [
                   new Cursos{ Nome="Curso A", Logotipo="noImage.jpg" },
               new Cursos{ Nome="Curso B", Logotipo="noImage.jpg" }
                //adicionar outros cursos
                ];
                await dbContext.Cursos.AddRangeAsync(cursos);
                haAdicao = true;
            }


            // Se não houver Alunos, cria-os
            var alunos = Array.Empty<Alunos>();
            if (!dbContext.Alunos.Any()) {
                alunos = [
                   new Alunos{ Nome="Mário", DataNascimento=DateOnly.Parse("2000-12-15"),Telemovel="" ,
                           Curso= cursos[0], DataMatricula=DateTime.Parse("2024-02-15"), NumAluno=1 },
               new Alunos{ Nome="Joana", DataNascimento=DateOnly.Parse("2000-12-16"),Telemovel="913456789" ,
                           Curso= cursos[0], DataMatricula=DateTime.Parse("2024-12-15"), NumAluno=2 },
               new Alunos{ Nome="João", DataNascimento=DateOnly.Parse("1999-12-31"),Telemovel="92345687" ,
                           Curso= cursos[0], DataMatricula=DateTime.Parse("2024-12-15"), NumAluno=3 },

               new Alunos{ Nome="Maria", DataNascimento=DateOnly.Parse("2000-12-15"),Telemovel="9612347" ,
                           Curso= cursos[1], DataMatricula=DateTime.Parse("2024-12-15"), NumAluno=4 },
               new Alunos{ Nome="Ana", DataNascimento=DateOnly.Parse("2000-12-15"),Telemovel="" ,
                           Curso= cursos[1], DataMatricula=DateTime.Parse("2024-12-15"), NumAluno=5 },
               //add other users
            ];
                await dbContext.Alunos.AddRangeAsync(alunos);
                haAdicao = true;
            }


            // Se não houver Utilizadores Identity, cria-os
            var users = Array.Empty<IdentityUser>();
            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();

            if (!dbContext.Users.Any()) {
                users = [
                   new IdentityUser{UserName="email.seis@mail.pt", NormalizedUserName="EMAIL.SEIS@MAIL.PT",
               Email="email.seis@mail.pt",NormalizedEmail="EMAIL.SEIS@MAIL.PT", EmailConfirmed=true,
               SecurityStamp="5ZPZEF6SBW7IU4M344XNLT4NN5RO4GRU", ConcurrencyStamp="c86d8254-dd50-44be-8561-d2d44d4bbb2f",
               PasswordHash=hasher.HashPassword(null,"Aa0_aa") },
            new IdentityUser{UserName="email.sete@mail.pt", NormalizedUserName="EMAIL.SETE@MAIL.PT",
               Email="email.sete@mail.pt",NormalizedEmail="EMAIL.SETE@MAIL.PT", EmailConfirmed=true,
               SecurityStamp="TW49PF6SBW7IU4M344XNLT4NN5RO4GRU", ConcurrencyStamp="d8254c86-dd50-44be-8561-d2d44d4bbb2f",
               PasswordHash=hasher.HashPassword(null,"Aa0_aa") }
                   ];
                await dbContext.Users.AddRangeAsync(users);
                haAdicao = true;
            }


            // Se não houver Professores, cria-os
            var profs = Array.Empty<Professores>();
            if (!dbContext.Professores.Any()) {
                profs = [
                   new Professores { Nome="João Mendes", DataNascimento=DateOnly.Parse("1970-04-10"), Telemovel="919876543" , UserId=users[0].Id },
               new Professores { Nome="Maria Sousa", DataNascimento=DateOnly.Parse("1988-09-12"), Telemovel="918076543" , UserId=users[1].Id }
                  ];
                await dbContext.Professores.AddRangeAsync(profs);
                haAdicao = true;
            }



            // Se não houver UCs, cria-as
            var ucs = Array.Empty<UnidadesCurriculares>();
            if (!dbContext.UCs.Any()) {
                ucs = [
                   new UnidadesCurriculares{Nome="Unidade Curricular A", AnoCurricular=1, Semestre=1,Curso=cursos[0],ListaProfessores=[ profs[0] ]},
               new UnidadesCurriculares{Nome="Unidade Curricular B", AnoCurricular=1, Semestre=1,Curso=cursos[0],ListaProfessores=[ profs[0],profs[1] ]},
               new UnidadesCurriculares{Nome="Unidade Curricular C", AnoCurricular=1, Semestre=1,Curso=cursos[1],ListaProfessores=[ profs[1] ]},
               new UnidadesCurriculares{Nome="Unidade Curricular D", AnoCurricular=1, Semestre=2,Curso=cursos[1],ListaProfessores=[ profs[1] ]}
                ];
                await dbContext.UCs.AddRangeAsync(ucs);
                haAdicao = true;
            }


            try {
                if (haAdicao) {
                    // tornar persistentes os dados
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex) {

                throw;
            }



        }
    }
}