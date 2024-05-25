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

        [Fact]
        public void GetByCategory()
        {
            long id = 1;
            long espected = 3;

            var result = _repository.GetByCategory(id);

            Assert.Equal(espected, result.Count());
        }

        [Fact]
        public void GetByIdTest()
        {
            long id = 1;
            string espected = "MTI";

            var result = _repository.GetById(id).Title;

            Assert.Equal(espected, result);
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