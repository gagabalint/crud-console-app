using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using RTKQ6M_HSZF_2024251.Application;
using RTKQ6M_HSZF_2024251.Model;
using RTKQ6M_HSZF_2024251.Persistence.MsSql;
using System.Collections.Generic;

namespace RTKQ6M_HSZF_2024251.Test
{

    [TestFixture]
    public class ApplicationTests
    {
        [Test]
        public void AddRailway_ShouldAddRailway()
        {

            var railways = new List<RailwayLine>();

            var railwayRepo = new Mock<IRailwayDataProvider>();
            railwayRepo.Setup(r => r.Add(It.IsAny<RailwayLine>())).Callback<RailwayLine>(s =>
            {
                railways.Add(s);
            });

            var railwayService = new RailwayService(railwayRepo.Object);


            var newRailway = new RailwayLine { LineName = "From->To", LineNumber = "101" };
            railwayService.Add(newRailway);
            ;

            Assert.That(railways, Contains.Item(newRailway));
            Assert.AreEqual(1, railways.Count);
        }
        [Test]
        public void AddRailway_ShouldThrowInvalidOperation()
        {
            var railways = new List<RailwayLine>();
            var railwayRepo = new Mock<IRailwayDataProvider>();
            railwayRepo.Setup(r => r.Get(It.IsAny<string>())).Returns<string>(LineNumber =>
            {
                return railways.FirstOrDefault(s => s.LineNumber == LineNumber);
            });

            railwayRepo.Setup(r => r.Add(It.IsAny<RailwayLine>())).Callback<RailwayLine>(s =>
            {
                railways.Add(s);
            }
            );
            var railwayService = new RailwayService(railwayRepo.Object);
            var data = new RailwayLine { LineName = "From->To", LineNumber = "101" };
            railwayService.Add(data);
            Assert.That(railways, Contains.Item(data));
            var newRailway = new RailwayLine { LineName = "From->To", LineNumber = "101" };

            Assert.Throws<InvalidOperationException>(() => railwayService.Add(newRailway));
        }
        [Test]
        public void DeleteRailway_ShouldThrowKeyNotFound()
        {
            var railwayRepo = new Mock<IRailwayDataProvider>();
            var railwayService = new RailwayService(railwayRepo.Object);
            railwayRepo.Setup(r => r.Get(It.IsAny<string>())).Returns<RailwayLine>(null);
            Assert.Throws<KeyNotFoundException>(() => railwayService.Delete("1"));
        }
        [Test]
        public void DeleteRailway_ShouldRemoveRailway()
        {
            var railways = new List<RailwayLine>();
            var railwayRepo = new Mock<IRailwayDataProvider>();
            var railwayService = new RailwayService(railwayRepo.Object);
            railwayRepo.Setup(r => r.Get(It.IsAny<string>())).Returns<string>(railwayline => { return railways.FirstOrDefault(r => r.LineNumber == railwayline); });
            railwayRepo.Setup(r => r.Add(It.IsAny<RailwayLine>())).Callback<RailwayLine>(s =>
            {
                railways.Add(s);
            });
            railwayRepo.Setup(r => r.Delete(It.IsAny<string>())).Callback<string>(lineNumber =>
            {
                var railwayToRemove = railways.FirstOrDefault(s => s.LineNumber == lineNumber);

                railways.Remove(railwayToRemove);

            });

            var data = new RailwayLine { LineName = "From->To", LineNumber = "101" };
            railwayService.Add(data);
            Assert.That(railways, Contains.Item(data));
            railwayService.Delete(data.LineNumber);
            Assert.That(railways, Does.Not.Contain(data));
        }
        [Test]
        public void UpdateRailway_ShouldUpdateRailway()
        {
            var railways = new List<RailwayLine>
    {
        new RailwayLine { LineName = "From->To", LineNumber = "101" },
        new RailwayLine { LineName = "From->To", LineNumber = "102" },
        new RailwayLine { LineName = "From->To", LineNumber = "103" }
    };

            var railwayRepo = new Mock<IRailwayDataProvider>();
            var railwayService = new RailwayService(railwayRepo.Object);

            railwayRepo.Setup(r => r.Get(It.IsAny<string>())).Returns<string>(lineNumber =>
            {
                return railways.FirstOrDefault(r => r.LineNumber == lineNumber);
            });

            railwayRepo.Setup(r => r.Update(It.IsAny<string>(), It.IsAny<RailwayLine>())).Callback<string, RailwayLine>((lineNumber, updatedLine) =>
            {
                var existing = railways.FirstOrDefault(r => r.LineNumber == lineNumber);
               
                    existing.LineNumber = updatedLine.LineNumber;
                    existing.LineName = updatedLine.LineName;
                
            });

            var data = new RailwayLine { LineName = "MODIFIED", LineNumber = "-1" };

            railwayService.Update("101", data);

            Assert.That(railways, Contains.Item(data));
            Assert.That(railways, Does.Not.Contain(new RailwayLine { LineNumber = "101", LineName = "From->To" }));
        }


        [Test]
        public void AddService_ShouldAddService()
        {

            var services = new List<Service>();

            var serviceRepo = new Mock<IServiceDataProvider>();

            serviceRepo.Setup(r => r.Batch(It.IsAny<Func<Service, bool>>())).Returns(() => services);

            serviceRepo.Setup(r => r.Add(It.IsAny<Service>())).Callback<Service>(s =>
            {
                services.Add(s);
            });

            var serviceService = new ServiceService(serviceRepo.Object);


            var newService = new Service { TrainNumber = 2, DelayAmount = 10, LineNumber = "102" };
            serviceService.Add(newService);
            ;

            Assert.That(services, Contains.Item(newService));
            Assert.AreEqual(1, services.Count);
        }
        [Test]
        public void AddService_ShouldThrowInvalidOperation()
        {

            var services = new List<Service>();

            var serviceRepo = new Mock<IServiceDataProvider>();

            serviceRepo.Setup(r => r.Batch(It.IsAny<Func<Service,bool>>())).Returns(() => services);
            serviceRepo.Setup(r => r.Get(It.IsAny<int>())).Returns<int>(trainNumber =>
            {
                return services.FirstOrDefault(s => s.TrainNumber == trainNumber);
            });

            serviceRepo.Setup(r => r.Add(It.IsAny<Service>())).Callback<Service>(s =>
            {
                services.Add(s);
            });

            var serviceService = new ServiceService(serviceRepo.Object);

            var data = new Service { TrainNumber = 2, DelayAmount = 20, LineNumber = "103" };
            var newService = new Service { TrainNumber = 2, DelayAmount = 10, LineNumber = "102" };
            serviceService.Add(data);



            Assert.Throws<InvalidOperationException>(() => serviceService.Add(newService));
        }


        [Test]
        public void UpdateService_ShouldReplaceExistingServiceAndTriggerEvent()
        {

            string triggeredMessage = null;
            Events.LeastDelayEvent += (message) => triggeredMessage = message;

            var list = new List<Service>
    {
        new Service { TrainNumber = 1, DelayAmount = 15, LineNumber = "101" },
        new Service { TrainNumber = 2, DelayAmount = 10, LineNumber = "102" }
    };

            var serviceRepo = new Mock<IServiceDataProvider>();


            serviceRepo.Setup(r => r.Batch(It.IsAny<Func<Service, bool>>())).Returns(() => list);
            serviceRepo.Setup(r => r.Get(It.IsAny<int>())).Returns<int>(trainNumber =>
            {
                return list.FirstOrDefault(s => s.TrainNumber == trainNumber);
            });
            serviceRepo.Setup(r => r.Update(It.IsAny<int>(), It.IsAny<Service>())).Callback<int, Service>((trainNumber, updatedService) =>
            {
                var existingService = list.FirstOrDefault(s => s.TrainNumber == trainNumber);


                existingService.TrainNumber = updatedService.TrainNumber;
                existingService.DelayAmount = updatedService.DelayAmount;
                existingService.LineNumber = updatedService.LineNumber;
            });


            var serviceService = new ServiceService(serviceRepo.Object);


            serviceService.Update(2, new Service { TrainNumber = 2, DelayAmount = 5, LineNumber = "102" });

            Assert.IsNotNull(triggeredMessage);
            Assert.That(triggeredMessage, Does.Contain("No 2"));

            var updatedService = list.FirstOrDefault(s => s.TrainNumber == 2);
            Assert.IsNotNull(updatedService);
            Assert.AreEqual(2, updatedService.TrainNumber);
        }

        [Test]
        public void DeleteService_ShouldThrowIfNotFound()
        {
            var serviceRepo = new Mock<IServiceDataProvider>();
            serviceRepo.Setup(r => r.Get(It.IsAny<int>())).Returns<Service>(null);
            var serviceService = new ServiceService(serviceRepo.Object);

            Assert.Throws<KeyNotFoundException>(() => serviceService.Delete(1));
        }
        [Test]
        public void DeleteService_ShouldDeleteService()
        {
            var services = new List<Service>();

            var serviceRepo = new Mock<IServiceDataProvider>();

            serviceRepo.Setup(r => r.Batch(It.IsAny<Func<Service, bool>>()  )).Returns(() => services);

            serviceRepo.Setup(r => r.Get(It.IsAny<int>())).Returns<int>(trainNumber =>
            {
                return services.FirstOrDefault(s => s.TrainNumber == trainNumber);
            });

            serviceRepo.Setup(r => r.Delete(It.IsAny<int>())).Callback<int>(trainNumber =>
            {
                var serviceToRemove = services.FirstOrDefault(s => s.TrainNumber == trainNumber);

                services.Remove(serviceToRemove);

            });

            serviceRepo.Setup(r => r.Add(It.IsAny<Service>())).Callback<Service>(s =>
            {
                services.Add(s);
            });

            var serviceService = new ServiceService(serviceRepo.Object);


            var data = new Service { TrainNumber = 2, DelayAmount = 20, LineNumber = "103" };
            serviceService.Add(data);

            Assert.That(services, Contains.Item(data));

            serviceService.Delete(data.TrainNumber);


            Assert.That(services, Does.Not.Contain(data));
        }



        [Test]
        public void SearchRailwayLines_ShouldFilterByLineNumber()
        {

            var railwayService = new Mock<IRailwayService>();
            var serviceService = new Mock<IServiceService>();

            railwayService.Setup(r => r.GetAll(It.IsAny<Func<RailwayLine, bool>>())).Returns(new List<RailwayLine>
    {
        new RailwayLine { LineNumber = "101", LineName = "Line1" },
        new RailwayLine { LineNumber = "102", LineName = "Line2" }
    });

            serviceService.Setup(s => s.GetAll(It.IsAny<Func<Service, bool>>())).Returns(new List<Service>
    {
        new Service { LineNumber = "101", TrainType = "Express", From = "CityA", To = "CityB" },
        new Service { LineNumber = "102", TrainType = "Local", From = "CityC", To = "CityD" }
    });

            var searchAndList = new SearchAndList(railwayService.Object, serviceService.Object);


            var result = searchAndList.SearchRailwayLines("101", "", "", "", "");


            Assert.That(result, Does.Contain("101"));
            Assert.That(result, Does.Contain("Express"));
            Assert.That(result, Does.Not.Contain("102"));
        }
        [Test]
        public void SearchRailwayLines_ShouldReturnAllIfNoCriteria()
        {

            var railwayService = new Mock<IRailwayService>();
            var serviceService = new Mock<IServiceService>();

            railwayService.Setup(r => r.GetAll(It.IsAny<Func<RailwayLine, bool>>())).Returns(new List<RailwayLine>
    {
        new RailwayLine { LineNumber = "101", LineName = "Test Line 1" },
        new RailwayLine { LineNumber = "102", LineName = "Test Line 2" }
    });

            serviceService.Setup(s => s.GetAll(It.IsAny<Func<Service, bool>>())).Returns(new List<Service>
    {
        new Service { LineNumber = "101", TrainType = "Express", From = "CityA", To = "CityB" },
        new Service { LineNumber = "102", TrainType = "Local", From = "CityC", To = "CityD" }
    });

            var searchAndList = new SearchAndList(railwayService.Object, serviceService.Object);

            var result = searchAndList.SearchRailwayLines("", "", "", "", "");

            Assert.That(result, Does.Contain("101"));
            Assert.That(result, Does.Contain("102"));
            Assert.That(result, Does.Contain("Express"));
            Assert.That(result, Does.Contain("Local"));
        }

        [Test]
        public void AvgDelayByLines_ShouldReturnCorrectAverage()
        {
            var railwayService = new Mock<IRailwayService>();
            railwayService.Setup(r => r.GetAll(It.IsAny<Func<RailwayLine, bool>>())).Returns(new List<RailwayLine>
            {
                new RailwayLine { LineNumber = "101", Services = new List<Service> {
                    new Service { DelayAmount = 10 },
                    new Service { DelayAmount = 20 }
                }}
            });

            var serviceService = new Mock<IServiceService>();
            var avgDelayService = new AvgDelayByRailways(railwayService.Object, serviceService.Object);

            var result = avgDelayService.AvgDelayByLines();

            Assert.AreEqual(15, result["101"]);
        }
        [Test]
        public void LessThan5MinutesDelay_ShouldReturnCorrectAmount()
        {
            var railwayService = new Mock<IRailwayService>();
            railwayService.Setup(r => r.GetAll(It.IsAny<Func<RailwayLine, bool>>())).Returns(new List<RailwayLine>
            {
                new RailwayLine { LineNumber = "101", Services = new List<Service> {
                    new Service { DelayAmount = 10 },
                    new Service { DelayAmount = 20 },
                    new Service { DelayAmount = 2 },
                    new Service { DelayAmount = 4 }
                }}
            });

            var serviceService = new Mock<IServiceService>();
            var delayStatistics = new DelayStatistics(railwayService.Object);

            var result = delayStatistics.GenerateDelayStatistics();

            Assert.AreEqual(2, result["101"]);
        }

        [Test]
        public void MostDelayedDestinations_ShouldReturnCorrectDestination()
        {

            var railwayService = new Mock<IRailwayService>();
            railwayService.Setup(r => r.GetAll(It.IsAny<Func<RailwayLine, bool>>())).Returns(new List<RailwayLine>
            {
                new RailwayLine
                {
                    LineNumber = "101",
                    Services = new List<Service>
                    {
                        new Service { To = "CityA", DelayAmount = 20 },
                        new Service { To = "CityB", DelayAmount = 30 },
                        new Service { To = "CityA", DelayAmount = 40 }
                    }
                }
            });

            var mostDelayed = new MostDelayedDestinations(railwayService.Object);

            var result = mostDelayed.GetMostDelayedDestinations();

            Assert.AreEqual("CityA", result["101"]);
        }

        [Test]
        public void LoadDataFromFile_ShouldLoadDataCorrectly()
        {
            var railwayRepo = new Mock<IRailwayDataProvider>();
            var serviceRepo = new Mock<IServiceDataProvider>();
            var loader = new DataLoader(railwayRepo.Object, serviceRepo.Object);

            var filePath = "testdata.json";
            File.WriteAllText(filePath, @"{ ""RailwayLines"": [ { ""LineNumber"": ""101"", ""Services"": [] } ] }");

            loader.LoadDataFromFile(filePath);

            railwayRepo.Verify(r => r.Add(It.Is<RailwayLine>(l => l.LineNumber == "101")), Times.Once);
            File.Delete(filePath);
        }


        [Test]
        public void AddMultipleServices_ShouldTriggerEventOnlyForSpecificTrain()
        {
            
            string triggeredMessage = null;
            Events.LeastDelayEvent += (message) => triggeredMessage = message;

            List<Service> list = new List<Service>(); 
            var railwayRepo = new Mock<IRailwayDataProvider>();
            var railwayService = new RailwayService(railwayRepo.Object);

            var serviceRepo = new Mock<IServiceDataProvider>();

            
            serviceRepo.Setup(r => r.Batch(It.IsAny<Func<Service, bool>>()))
                .Returns<Func<Service, bool>>(filter =>
                {
                    return list.Where(filter).ToList(); 
                });

            
            serviceRepo.Setup(r => r.Add(It.IsAny<Service>())).Callback<Service>(s =>
            {
                list.Add(s); 
            });

            var serviceService = new ServiceService(serviceRepo.Object);

            
            railwayService.Add(new RailwayLine { LineName = "Test1", LineNumber = "101" });
            railwayService.Add(new RailwayLine { LineName = "Test2", LineNumber = "102" });

            serviceService.Add(new Service { TrainNumber = 1, DelayAmount = 15, LineNumber = "101" });
            serviceService.Add(new Service { TrainNumber = 2, DelayAmount = 5, LineNumber = "102" });

            triggeredMessage = null; 
            serviceService.Add(new Service { TrainNumber = 3, DelayAmount = 8, LineNumber = "102" });
            Assert.IsNull(triggeredMessage); 

            serviceService.Add(new Service { TrainNumber = 4, DelayAmount = 10, LineNumber = "101" });
            Assert.IsNotNull(triggeredMessage); 
            Assert.That(triggeredMessage, Does.Contain("No 4")); 
        }

    }
}




