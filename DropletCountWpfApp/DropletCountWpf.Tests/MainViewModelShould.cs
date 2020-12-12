using DropletCountWpf.Model;
using DropletCountWpf.Model.Model;
using DropletCountWpf.UI.Services;
using DropletCountWpf.UI.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace DropletCountWpf.Tests
{
    public class MainViewModelShould
    {
        #region Data Fields
        private MainViewModel _sut;
        private Mock<IJsonDataService> _mockJsonDataService;
        private Mock<IFileIOService> _mockFileIOService;
        private const string TestFilePath = @"C:\DropletData\PlateDropletInfo.json";
        private DropletBin _dropletBin;

        #endregion



        #region Ctor - Will be called for each test executed

        public MainViewModelShould()
        {
            // Arrange
            // Create a droplet bin model and populate with 3 wells
            // with 2 having counts below 300           
            _dropletBin = new DropletBin()
            {
                PlateDropletInfo = new PlateDropletInfo()
                {
                    Version = 01,
                    DropletInfo = new DropletInfo()
                    {
                        Wells = new List<Well>()
                        {
                            new Well()
                            {
                                WellName = "A01",
                                WellIndex = 0,
                                DropletCount = 500
                            },
                            new Well()
                            {
                                WellName = "A02",
                                WellIndex = 1,
                                DropletCount = 100
                            },
                            new Well()
                            {
                                WellName = "A03",
                                WellIndex = 2,
                                DropletCount = 250
                            },
                        }
                    }
                }
            };


            _mockJsonDataService = new Mock<IJsonDataService>();
            _mockJsonDataService.Setup<DataTable>(dt => dt.GetDataTableFromJson())
                                .Returns(new DataTable());

            _mockJsonDataService.Setup<DropletBin>(d => d.GetDropletBinFromJson())
                             .Returns(_dropletBin);


            _mockFileIOService = new Mock<IFileIOService>();
            _mockFileIOService.Setup(x => x.OpenDatafile())
                .Returns(TestFilePath);      

            _sut = new MainViewModel(_mockJsonDataService.Object, _mockFileIOService.Object);

        }

        #endregion
       

        [Fact]
        public void ReturnValidFilePathFromSelectedFileProperty()
        {
            // Act
            _sut.ProcessSelectedFileCommand.Execute(null);

            // Assert
            Assert.Equal(TestFilePath, _sut.SelectedFile);
        }

        [Fact]
        public void ReturnDataTableFromWellsTableProperty()
        {            
            // Act
            _sut.ProcessSelectedFileCommand.Execute(null);

            // Assert
            Assert.IsType<DataTable>(_sut.WellsTable);
        }



        // The InlineData represents the user typing in these threshold values
        // in the UI Droplet Threshold textbox and clicking the Update Button 
        [Theory]
        [InlineData(0)]
        [InlineData(500)]
        [InlineData(-1)]
        [InlineData(501)]
        public void SetDropletThresholdInRange(int threshold)
        {           
            // Act
            _sut.UpdateThresholdCommand.Execute(threshold);
            var dropletThreshold = _sut.DropletThreshold;

            //Assert
            Assert.InRange<int>(dropletThreshold, 0 , 500);
        }

        [Theory]
        [InlineData(300)]        
        public void ReturnTotalLowDropletCountBasedOnThreshold(int threshold)
        {
            // Act
            _sut.ProcessSelectedFileCommand.Execute(null);
            _sut.UpdateThresholdCommand.Execute(threshold);

            // Assert
            Assert.Equal(2, _sut.TotalLowDropletCount);
        }



#if NOT_BUILDING_Grey_Out

        Some usefull xUnit test examples. See PluralSight xUnit path module TestDrivenDevelopmentinC#     
        Module 3
        ****************************************************
           
        1) sut method tested by following Unit tests below:
        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
          if (request == null)
          {
            throw new ArgumentNullException(nameof(request));
          }

          return new DeskBookingResult
          {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Date = request.Date,
          };
        }      
        
        
        // TestClass fields and  CTOR for Global Arrange. CTOR is called once per 
           test.
            private readonly DeskBookingRequest _request;
            private readonly List<Desk> _availableDesks;
            private readonly Mock<IDeskBookingRepository> _deskBookingRepositoryMock;
            private readonly Mock<IDeskRepository> _deskRepositoryMock;
            private readonly DeskBookingRequestProcessor _sut;

            // CTOR called 1x per test executed
            public DeskBookingRequestProcessorTests()
            {
            // ARRANGE CTOR
              _request = new DeskBookingRequest
              {
                FirstName = "Thomas",
                LastName = "Huber",
                Email = "thomas@thomasclaudiushuber.com",
                Date = new DateTime(2020, 1, 28)
              };

              _availableDesks = new List<Desk> { new Desk { Id = 7 } };

              _deskBookingRepositoryMock = new Mock<IDeskBookingRepository>();
              _deskRepositoryMock = new Mock<IDeskRepository>();
              _deskRepositoryMock.Setup(x => x.GetAvailableDesks(_request.Date))
                .Returns(_availableDesks);

              _sut = new DeskBookingRequestProcessor(
                _deskBookingRepositoryMock.Object, _deskRepositoryMock.Object);
            }  
     
        // Testing an a model class for valid content
        [Fact]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {
            // Arrange
            var request = new DeskBookingRequest
            {
                FirstName = "Thomas",
                LastName = "Huber",
                Email = "thomas@thomasclaudiushuber.com",
                Date = new DateTime(2020, 1, 28)
            };

            // Act
            DeskBookingResult result = _processor.BookDesk(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.FirstName, result.FirstName);
            Assert.Equal(request.LastName, result.LastName);
            Assert.Equal(request.Email, result.Email);
            Assert.Equal(request.Date, result.Date);
        }

        // Test that an exception is thrown on a specific condition, in this case passing in null
        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
          var exception = Assert.Throws<ArgumentNullException>(() => _sut.BookDesk(null));

          Assert.Equal("request", exception.ParamName);
        }

        *****************************************************************8
        2) sut method tested by following test below:
         public DeskBookingResult BookDesk(DeskBookingRequest request)
        {       
          _deskBookingRepository.Save(request);      
           return  DeskBookingResultCode.Success;
        }
        
        // Testing the contents of a model entity are saved
        [Fact]
        public void ShouldSaveDeskBooking()
        {
           // Arrange 
          DeskBooking savedDeskBooking = null;
          _deskBookingRepositoryMock.Setup(x => x.Save(It.IsAny<DeskBooking>()))
            .Callback<DeskBooking>(deskBooking =>
            {
              savedDeskBooking = deskBooking;
            });
           // Act
          _sut.BookDesk(_request);

          // Assert
          _deskBookingRepositoryMock.Verify(x => x.Save(It.IsAny<DeskBooking>()), Times.Once);

          Assert.NotNull(savedDeskBooking);
          Assert.Equal(_request.FirstName, savedDeskBooking.FirstName);
          Assert.Equal(_request.LastName, savedDeskBooking.LastName);
          Assert.Equal(_request.Email, savedDeskBooking.Email);
          Assert.Equal(_request.Date, savedDeskBooking.Date);
          Assert.Equal(_availableDesks.First().Id, savedDeskBooking.DeskId);
        }
#endif


    }
}
