using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using bluedit.DataAccess.EfModels;
using bluedit.DataAccess;

namespace TestBlueddit
{
    public class UserTests
    {
        private readonly UserRepository _repository;
        private readonly BlueditContext _context;
        private readonly IMapper _mapper;

        public UserTests()
        {
            var options = new DbContextOptionsBuilder<BlueditContext>()
                .UseSqlServer("Server=localhost,1432;Database=bluedit;User Id=SA;Password=Imnotweakok?;Trusted_Connection=False;Trust Server Certificate=True")
                .Options;

            _context = new BlueditContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, bluedit.Dbo.User>();
            });
            _mapper = config.CreateMapper();

            var logger = new LoggerFactory().CreateLogger<UserRepository>();

            _repository = new UserRepository(_context, logger, _mapper);
        }

        [Theory]
        [InlineData("Dylan", "dylan.toledano@epita.fr")]
        [InlineData("Leo", "leo@epitech.fr")]
        [InlineData("Lea", "lea@uwu.fr")]
        [InlineData("null", null)]
        public void GetByNameTests(string name, string mail)
        {
            var result = _repository.GetByName(name)?.Mail;

            Assert.Equal(mail, result);
        }

        [Theory]
        [InlineData(1, "dylan.toledano@epita.fr")]
        [InlineData(2, "leo@epitech.fr")]
        [InlineData(3, "lea@uwu.fr")]
        [InlineData(225, null)]
        public void GetByIdTests(long id, string mail)
        {
            var result = _repository.GetById(id)?.Mail;

            Assert.Equal(mail, result);
        }

        [Fact]
        public void HashTest()
        {
            string mot = "mot";
            string hash = "AUWfGgHwjm6GR0129CQNIInP5PG9ML6euPiCR9e1QBU=";

            var result = _repository.Hash(mot);

            Assert.Equal(hash, result);
        }

        /*
        [Fact]
        public async Task SignUpTest()
        {
            string pseudo = "p";
            string password = "p";
            string mail = "mail";

            var result = await _repository.SignUp(pseudo, password, mail);

            Assert.NotNull(result);
            Assert.Equal(pseudo, result?.Name);
        }*/

        [Theory]
        [InlineData("Dylan", "password")]
        [InlineData("Leo", "password")]
        [InlineData("Lea", "password")]
        [InlineData("Quentin", "password")]
        public void SignInTests(string pseudo, string password)
        {
            var result = _repository.SignIn(pseudo, password);

            Assert.NotNull(result);
            Assert.Equal(pseudo, result?.Name);
        }

        [Theory]
        [InlineData("Dylan", "pass")]
        [InlineData("Leololo", "password")]
        public void SignInnullTests(string pseudo, string password)
        {
            var result = _repository.SignIn(pseudo, password);

            Assert.Null(result);
        }
    }
}