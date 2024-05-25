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

        [Fact]
        public void GetByNameTest()
        {
            var name = "minecraft";
            var Title = "Minecraft";

            var result = _repository.GetByName(name).Title;

            Assert.Equal(Title, result);
        }

        [Fact]
        public void GetByIdTest()
        {
            var name = "minecraft";
            var Title = "Minecraft";

            var result = _repository.GetByName(name).Title;

            Assert.Equal(Title, result);
        }
    }
}