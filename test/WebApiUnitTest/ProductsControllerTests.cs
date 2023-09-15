using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebApiUnitTest
{
    public class ProductsControllerTests
    {
        private  Mock<IProductService> _mockRepo;
        private  ProductsController _controller;

        public ProductsControllerTests()
        {
        }

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task GetAll_Executes_ReturnOkObjectResult()
        {
            //Arange
            _mockRepo = new Mock<IProductService>();
            _controller = new ProductsController(_mockRepo.Object, null);

            //Act
            var result =  await _controller.GetAll(null);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetAll_Executes_ReturnExactNumberOfProduct()
        {
            //Arange
            _mockRepo = new Mock<IProductService>();
            _controller = new ProductsController(_mockRepo.Object, null);

            //Act
            _mockRepo.Setup(repo =>  repo.GetAllAsync(null, null).Result).Returns(new List<ProductDto>() { new ProductDto(), new ProductDto() });
            var result = (await _controller.GetAll(null)) as OkObjectResult;
            var products = result.Value as List<ProductDto>;
            
            //Assert
            Assert.AreEqual(2, products.Count);
        }

        [Test]
        public async Task GetAll_Executes_ReturnOkObjectResult_WhenProductIsNull()
        {
            //Arange
            _mockRepo = new Mock<IProductService>();
            _controller = new ProductsController(_mockRepo.Object, null);

            //Act
            var result = await _controller.GetAll(null);
            var products = (result as OkObjectResult).Value as List<ProductDto>;

            //Assert
            Assert.IsNull(products);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}