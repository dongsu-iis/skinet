using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase {

        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController (IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo, IMapper mapper) {
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productsRepo = productsRepo;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts () {
            var spec = new ProductsWithTypesAndBrandsSpecification ();
            var products = await _productsRepo.ListAsync (spec);
            return Ok (_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>> (products));
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Product>> GetProduct (int id) {
            var spec = new ProductsWithTypesAndBrandsSpecification (id);
            var product = await _productsRepo.GetEntityWithSpec (spec);
            return Ok (_mapper.Map<Product, ProductToReturnDto> (product));
        }

        [HttpGet ("brands")] public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands () {
            var brands = await _productBrandRepo.ListAllAsync ();
            return Ok (brands);
        }

        [HttpGet ("types")] public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes () {
            var types = await _productTypeRepo.ListAllAsync ();
            return Ok (types);
        }

    }
}