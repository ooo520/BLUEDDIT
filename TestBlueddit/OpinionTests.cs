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
    public class OpinionTests
    {
        private readonly OpinionRepository _repository;
        private readonly BlueditContext _context;
        private readonly IMapper _mapper;

        public OpinionTests()
        {
            var options = new DbContextOptionsBuilder<BlueditContext>()
                .UseSqlServer("Server=localhost,1432;Database=bluedit;User Id=SA;Password=Imnotweakok?;Trusted_Connection=False;Trust Server Certificate=True")
                .Options;

            _context = new BlueditContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Opinion, bluedit.Dbo.Opinion>();
            });
            _mapper = config.CreateMapper();

            var logger = new LoggerFactory().CreateLogger<OpinionRepository>();

            _repository = new OpinionRepository(_context, logger, _mapper);
        }

        /*
        [Fact]
        public void GetByNameTest()
        {
            var name = "minecraft";
            var Title = "Minecraft";

            var result = _repository.GetByName(name).Title;

            Assert.Equal(Title, result);
        }
        */
    }
}