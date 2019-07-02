using Communication.BL.Models;
using Communication.BL.Repositories;
using Communication.DAL;
using Communication.BL.Services;
using Communication.BL.Exceptions;
using Communication.DAL.Entities;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Communication.BL.Mappers;

namespace TestConsoleApp
{

        class Program
        {
        static void testDAL()
        {
            //CommunicationDbContext dbContext = new CommunicationDbContext();
            //UserEntity userEntity = new UserEntity
            //{
            //    Name = "Pavel",
            //    Surname = "Dvorak",
            //    Email = "kocicak@vut.pl",
            //    Password = "heslo123_bude_hash",
            //    TelephoneNumber = "+420420420420"

            //};

            //dbContext.Users.Add(userEntity);
            //dbContext.SaveChanges();


            //var response = dbContext.Users.Find(userEntity.Id);
            //Console.Write("ID: " + response.Id);
            //Console.Write("\nNAME: " + response.Name);
            //Console.Write("\nEmail: " + response.Email);
            //Console.ReadKey();
        }

        static void testRepo()
        {
            CommunicationDbContext ctx = new CommunicationDbContext();
            ReactionRepository reactionRepository = new ReactionRepository(ctx);
            UserRepository userRepository = new UserRepository(ctx);
            CommentRepository commentRepository = new CommentRepository(ctx);
            ContributionRepository contributionRepository = new ContributionRepository(ctx);

            UserModel userModel = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Pavel",
                Surname = "Dvorak",
                Email = "kocicak@vut.pl",
                Password = "heslo123_bude_hash",
                TelephoneNumber = "+420420420420"
            };

            UserModel userModel2 = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Petr",
                Surname = "Novak",
                Email = "kocicaka@vut.pl",
                Password = "password",
                TelephoneNumber = "+420420420420"
            };

            ContributionModel contribution1 = new ContributionModel
            {
                Id = Guid.NewGuid(),
                Message = "AhojQy,dneska v druzine byl na obed krtkův dort <3, to byl náhul",
                User = userModel,
                Time = DateTime.Now
            };

            CommentModel commentModel1 = new CommentModel
            {
                Id = Guid.NewGuid(),
                Message = "No a co",
                Time = DateTime.Now,
                User = userModel,
                Contribution = contribution1
            };

            CommentModel commentModel2 = new CommentModel
            {
                Id = Guid.NewGuid(),
                Message = "Mnam dopi..",
                Time = DateTime.Now,
                User = userModel,
                Contribution = contribution1
            };

            userRepository.Insert(userModel);
            //userRepository.Insert(userModel2);
            contributionRepository.Insert(contribution1);
            commentRepository.Insert(commentModel1);
            commentRepository.Insert(commentModel2);

            var userResponse = userRepository.GetById(userModel.Id);
            var contributionReponse = contributionRepository.GetById(contribution1.Id);
            var commentResponse = commentRepository.GetCommentByUserId(userModel.Id);

            Console.Write("ID: " + userResponse.Id);
            Console.Write("\nNAME: " + userResponse.Name);
            Console.Write("\nEmail: " + userResponse.Email);
            Console.Write("\n\tStatus: " + contributionReponse.Message);
            Console.Write("\n\t\tComments: ");
            foreach(var comment in commentResponse)
            {
                Console.Write("\n\t\tMessage: " + comment.Message);
            }

            LoginService loginService = new LoginService();
            loginService.register("Jmeno", "Prijmeni", "mail@seznam.cz", "heslo123", "123456789", null);

            var userByEmail = loginService.LoadUserByEmail("mail@seznam.cz", "heslo123");
            Console.Write("\nLoadUserByUsername: " + userByEmail.Name);


            loginService.deactivateUser("mail@seznam.cz");
            try
            {
                userByEmail = loginService.LoadUserByEmail("mail@seznam.cz", "heslo123");
                Console.Write("\nChyba ");
            }
            catch(LoginException ex)
            {
                Console.Write("\nLoadUserByUsername: " + ex.Message);
            }

            userRepository.Delete(userByEmail);
            userRepository.Delete(userModel);
            Console.ReadKey();
        }
            static void Main(string[] args)
            {
                testRepo();
            }
        }

}
