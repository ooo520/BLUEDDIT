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
    public class AnswerTests
    {
        private readonly AnswerRepository _repository;
        private readonly BlueditContext _context;
        private readonly IMapper _mapper;

        public AnswerTests()
        {
            var options = new DbContextOptionsBuilder<BlueditContext>()
                .UseSqlServer("Server=localhost,1432;Database=bluedit;User Id=SA;Password=Imnotweakok?;Trusted_Connection=False;Trust Server Certificate=True")
                .Options;

            _context = new BlueditContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Answer, bluedit.Dbo.Answer>();
            });
            _mapper = config.CreateMapper();

            var logger = new LoggerFactory().CreateLogger<AnswerRepository>();

            _repository = new AnswerRepository(_context, logger, _mapper);
        }

        [Fact]
        public void GetByThreadTest()
        {
            long threadId = 1;

            var result = _repository.GetByThread(threadId);

            Assert.Equal(4, result.Count());
        }

        [Fact]
        public void GetRootAnswerOfThreadTest()
        {
            long threadId = 1;

            var result = _repository.GetRootAnswerOfThread(threadId);

            Assert.Equal(threadId, result.ThreadId);
        }
    }
}