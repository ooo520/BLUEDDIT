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
    public class ThreadTests
    {
        private readonly ThreadRepository _repository;
        private readonly BlueditContext _context;
        private readonly IMapper _mapper;

        public ThreadTests()
        {
            var options = new DbContextOptionsBuilder<BlueditContext>()
                .UseSqlServer("Server=localhost,1432;Database=bluedit;User Id=SA;Password=Imnotweakok?;Trusted_Connection=False;Trust Server Certificate=True")
                .Options;

            _context = new BlueditContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<bluedit.DataAccess.EfModels.Thread, bluedit.Dbo.Thread>();
            });
            _mapper = config.CreateMapper();

            var logger = new LoggerFactory().CreateLogger<ThreadRepository>();

            _repository = new ThreadRepository(_context, logger, _mapper);
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(2, 3)]
        [InlineData(3, 3)]
        [InlineData(4, 3)]
        [InlineData(5, 3)]
        public void GetByCategoryTests(long id, long expected)
        {
            var result = _repository.GetByCategory(id);

            Assert.Equal(expected, result.Count());
        }

        [Theory]
        [InlineData(1, "MTI")]
        [InlineData(5, "Paris")]
        [InlineData(8, "WR")]
        [InlineData(12, "UwU")]
        [InlineData(27, "Vegan")]
        public void GetByIdTests(long id, string expected)
        {
            var result = _repository.GetById(id).Title;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetAnswerCountForThreadTest()
        {
            long id = 1;
            long espected = 3;

            var result = _repository.GetAnswerCountForThread(id);

            Assert.Equal(espected, result);
        }
    }
}