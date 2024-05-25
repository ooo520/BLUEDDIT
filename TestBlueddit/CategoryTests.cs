using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using bluedit.DataAccess.EfModels;
using bluedit.DataAccess;
using bluedit.DataAccess.Interfaces;

namespace TestBlueddit
{
    public class CategoryTests
    {
        private readonly CategoryRepository _repository;
        private readonly BlueditContext _context;
        private readonly IMapper _mapper;

        public CategoryTests()
        {
            var options = new DbContextOptionsBuilder<BlueditContext>()
                .UseSqlServer("Server=localhost,1432;Database=bluedit;User Id=SA;Password=Imnotweakok?;Trusted_Connection=False;Trust Server Certificate=True")
                .Options;

            _context = new BlueditContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, bluedit.Dbo.Category>();
            });
            _mapper = config.CreateMapper();

            var logger = new LoggerFactory().CreateLogger<CategoryRepository>();

            _repository = new CategoryRepository(_context, logger, _mapper);
        }

        [Theory]
        [InlineData("epita", "Epita")]
        [InlineData("france", "France")]
        [InlineData("tetris", "Tetris")]
        [InlineData("animanga", "Anime/Manga")]
        [InlineData("minecraft", "Minecraft")]
        public void GetByNameTests(string name, string expected)
        {
            var result = _repository.GetByName(name).Title;

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(6, ".NET")]
        [InlineData(7, "bluedit")]
        [InlineData(8, "Démineur")]
        [InlineData(9, "Nourriture")]
        [InlineData(10, "Pokémon")]
        public void GetByIdTests(long Id, string expected)
        {
            var result = _repository.GetById(Id).Title; ;

            Assert.Equal(expected, result);
        }
    }
}