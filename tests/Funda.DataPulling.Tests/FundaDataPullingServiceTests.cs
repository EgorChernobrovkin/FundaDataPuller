using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Funda.Api.RealEstate.DTOs;
using Funda.Api.RealEstate.Interfaces;
using Funda.Api.Settings;
using Funda.DataPulling.Impl;
using Funda.DataPulling.Mapping;
using Funda.Domain;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using Xunit;
using RealEstate = Funda.Api.RealEstate.DTOs.RealEstate;

namespace Funda.DataPulling.Tests
{
    public class FundaDataPullingServiceTests
    {
        [Fact]
        public async Task PullObjects_FirstPageOfOne_ShouldNotDoAdditionalRequests()
        {
            const int pageSize = 20;
            var requesterMock = new Mock<IFundaRealEstateRequester>();
            requesterMock
                .Setup(r => r.Request($"?type=koop&zo=/amsterdam/&pagesize={pageSize}"))
                .ReturnsAsync(() => new RealEstateResult {Objects = new []
                {
                    new RealEstate(),
                    new RealEstate()
                }, TotalNumberOfObjects = pageSize});

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RealEstate, Domain.RealEstate>());
            var mapper = config.CreateMapper();

            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string>()
            {
                {"FundaApiInfo:PageSize", pageSize.ToString()}
            });
            var conf = builder.Build();

            var pullingService = new FundaDataPullingService(requesterMock.Object, conf, mapper);


            var res = await pullingService.PullObjectInAmsterdam();


            Assert.Equal(2, res.Count);
        }
        
        [Fact]
        public async Task PullObjects_FirstPageOfTen_ShouldDoNineAdditionalRequests()
        {
            const int pageSize = 20;
            var requesterMock = new Mock<IFundaRealEstateRequester>();
            requesterMock
                .Setup(r => r.Request(It.IsAny<string>()))
                .ReturnsAsync(() => new RealEstateResult {Objects = new []
                {
                    new RealEstate() {Id = Guid.NewGuid().ToString()},
                }, TotalNumberOfObjects = 23, Paging = new Paging() {NumberOfPages = 10}});

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RealEstate, Domain.RealEstate>());
            var mapper = config.CreateMapper();

            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string>()
            {
                {"FundaApiInfo:PageSize", pageSize.ToString()}
            });
            var conf = builder.Build();

            var pullingService = new FundaDataPullingService(requesterMock.Object, conf, mapper);


            var res = await pullingService.PullObjectInAmsterdam();


            Assert.Equal(10, res.Count);
        }

        [Fact]
        public async Task PullObjects_ThereIsDuplicates_ShouldReturnWithoutDuplicates()
        {
            const int pageSize = 20;
            var requesterMock = new Mock<IFundaRealEstateRequester>();
            requesterMock
                .Setup(r => r.Request(It.IsAny<string>()))
                .ReturnsAsync(() => new RealEstateResult {Objects = new []
                {
                    new RealEstate() {Id = Guid.NewGuid().ToString()},
                    new RealEstate() {Id = "duplicateId"}
                }, TotalNumberOfObjects = 23, Paging = new Paging() {NumberOfPages = 10}});

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RealEstate, Domain.RealEstate>());
            var mapper = config.CreateMapper();

            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string>()
            {
                {"FundaApiInfo:PageSize", pageSize.ToString()}
            });
            var conf = builder.Build();

            var pullingService = new FundaDataPullingService(requesterMock.Object, conf, mapper);


            var res = await pullingService.PullObjectInAmsterdam();


            Assert.Equal(11, res.Count);
        }

        [Fact]
        public async Task PullObjectInAmsterdam_ShouldUseSpecificUrl()
        {
            const int pageSize = 20;
            var requesterMock = new Mock<IFundaRealEstateRequester>();
            requesterMock
                .Setup(r => r.Request($"?type=koop&zo=/amsterdam/&pagesize={pageSize}"))
                .ReturnsAsync(() => new RealEstateResult {Objects = new []
                {
                    new RealEstate(),
                    new RealEstate()
                }, TotalNumberOfObjects = pageSize});

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RealEstate, Domain.RealEstate>());
            var mapper = config.CreateMapper();

            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string>()
            {
                {"FundaApiInfo:PageSize", pageSize.ToString()}
            });
            var conf = builder.Build();

            var pullingService = new FundaDataPullingService(requesterMock.Object, conf, mapper);


            var res = await pullingService.PullObjectInAmsterdam();


            Assert.Equal(2, res.Count);
        }

        [Fact]
        public async Task PullObjectWithGardenInAmsterdam_ShouldUseSpecificUrl()
        {
            const int pageSize = 20;
            var requesterMock = new Mock<IFundaRealEstateRequester>();
            requesterMock
                .Setup(r => r.Request($"?type=koop&zo=/amsterdam/tuin/&pagesize={pageSize}"))
                .ReturnsAsync(() => new RealEstateResult {Objects = new []
                {
                    new RealEstate(),
                    new RealEstate()
                }, TotalNumberOfObjects = pageSize});

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RealEstate, Domain.RealEstate>());
            var mapper = config.CreateMapper();

            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string>()
            {
                {"FundaApiInfo:PageSize", pageSize.ToString()}
            });
            var conf = builder.Build();

            var pullingService = new FundaDataPullingService(requesterMock.Object, conf, mapper);


            var res = await pullingService.PullObjectWithGardenInAmsterdam();


            Assert.Equal(2, res.Count);
        }
    }
}
