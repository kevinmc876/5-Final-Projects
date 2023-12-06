using InventoryInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Drawing;
using System.Text;

namespace InventoryInterface.Controllers
{
    public class ProductController : Controller
    {
        private string BASEURL = "https://localhost:7265/api/Inventory/";

        private readonly IHttpClientFactory _httpClientFactory;
        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        #region list products

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            try
            {
                List<Products> prodList = await GetProductListWithDetailsAsync();

                return View(prodList);
            }
            catch (Exception)
            {
                // Handle exceptions, log errors, or display an error message.
                // For example: _logger.LogError($"An error occurred: {ex.Message}");
                return View(new List<Products>());
            }
        }

        private async Task<List<Products>> GetProductListWithDetailsAsync()
        {
            List<Products> prodList = new List<Products>();
            var httpClient = _httpClientFactory.CreateClient("InventorySystem");
            HttpResponseMessage response = await httpClient.GetAsync(BASEURL);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                prodList = JsonConvert.DeserializeObject<List<Products>>(data)!;

                //foreach (var product in prodList)
                //{
                //    await AddCategoryAndSizeDetailsAsync(httpClient, product);
                //}
            }

            return prodList;
        }

        //private async Task AddCategoryAndSizeDetailsAsync(HttpClient httpClient, Products product)
        //{
        //    HttpResponseMessage categoryResponse = await httpClient.GetAsync($"{BASEURL}/Category/{product.CategoryId}");
        //    if (categoryResponse.IsSuccessStatusCode)
        //    {
        //        var categoryData = await categoryResponse.Content.ReadAsStringAsync();
        //        var category = JsonConvert.DeserializeObject<Categories>(categoryData);
        //        product.Category = category;
        //    }

        //    HttpResponseMessage sizeResponse = await httpClient.GetAsync($"{BASEURL}/Size/{product.SizeId}");
        //    if (sizeResponse.IsSuccessStatusCode)
        //    {
        //        var sizeData = await sizeResponse.Content.ReadAsStringAsync();
        //        var size = JsonConvert.DeserializeObject<Size>(sizeData);
        //        product.Size = size;
        //    }
        //}


        #endregion



        #region Deatils Page
        public IActionResult Details(int id)
        {
            var Apt = new Models.Products();

            // Fetch the product details from the API or your data source
            var httpClient = _httpClientFactory.CreateClient("InventorySystem");
            HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                Apt = JsonConvert.DeserializeObject<Models.Products>(data);

                // Make sure the Apt object is not null
                if (Apt != null)
                {
                    return View(Apt);
                }
            }

            // If no product details are found or an error occurs, return a view with an empty model
            return View(new Models.Products());
        }

        #endregion

        #region Edit View
        //Create Page
        [HttpGet]
        public IActionResult Edit(int id)
        {
            //Declear List to store Categoy & Size list content to
            List<Categories> CategoriesList = new List<Categories>();
            List<Sizes> sizesList = new List<Sizes>();
            //Instansiating a Global Variable of the Products
            Products products = new Products();

            var httpClient = _httpClientFactory.CreateClient("InventorySystem");

            //Product Response List to convert Json Strings
            HttpResponseMessage reponse = httpClient.GetAsync($"{BASEURL}{id}").Result;
            if (reponse.IsSuccessStatusCode)
            {
                var data = reponse.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<Products>(data)!;
            }

            //Category Response List to convert Json Strings
            HttpResponseMessage catResponse = httpClient.GetAsync($"{BASEURL}Category/").Result;
            if (catResponse.IsSuccessStatusCode)
            {
                var catData = catResponse.Content.ReadAsStringAsync().Result;
                CategoriesList = JsonConvert.DeserializeObject<List<Categories>>(catData)!;
            }
            //Size Response List to convert Json Strings
            HttpResponseMessage sizeResponse = httpClient.GetAsync($"{BASEURL}Size/").Result;
            if (sizeResponse.IsSuccessStatusCode)
            {
                var sizeData = sizeResponse.Content.ReadAsStringAsync().Result;
                sizesList = JsonConvert.DeserializeObject<List<Sizes>>(sizeData)!;
            }

            //Instancate New ViewModel to update selectlist in ProductsModel
            var viewModel = new ProductVM
            {
                Id = products.Id,
                Name = products.Name,
                Image = products.Image,
                CategoryId = products.CategoryId,
                sizeId = products.SizeId,
                Quantity = products.Quantity,
                SelectedCategory = products.CategoryId,
                SelectedSize = products.SizeId,

                //Declartion of Select List content to the CategoryList in the View Model
                CategoryList = CategoriesList.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),

                //Declartion of Select List content to the SizeList in the View Model
                SizeList = sizesList.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductVM productvm)
        {
            //if(!ModelState.IsValid) return View(productvm);

            var httpClient = _httpClientFactory.CreateClient("InventoryAPI");

            //Instansiate New Product Model with ViewModel Data
            var prod = new Products
            {
                Id = id,
                Name = productvm.Name,
                Image= productvm.Image,
                CategoryId = productvm.SelectedCategory,
                SizeId = productvm.SelectedSize,
                Quantity = productvm.Quantity,
            };

            //Serialize Content stored to the new Product Model to JSON String
            var json = JsonConvert.SerializeObject(prod);
            //Converts serialized to UTF-8 string convension
            var content = new StringContent(json, Encoding.UTF8, "application/Json");

            //Product Response List to convert Json Strings
            HttpResponseMessage reponse = httpClient.PutAsync($"{BASEURL}{id}", content).Result;
            if (reponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to Update Product");
                return View(productvm);
            }
        }
        #endregion


        #region Create View
        [HttpGet]
        public IActionResult Create()
        {
            //Declear List to store Categoy & Size list content to
            List<Categories> CategoriesList = new List<Categories>();
            List<Sizes> sizesList = new List<Sizes>();

            var httpClient = _httpClientFactory.CreateClient("InventorySystem");

            //Category Response List to convert Json Strings
            HttpResponseMessage catResponse = httpClient.GetAsync($"{BASEURL}Category/").Result;
            if (catResponse.IsSuccessStatusCode)
            {
                var catData = catResponse.Content.ReadAsStringAsync().Result;
                CategoriesList = JsonConvert.DeserializeObject<List<Categories>>(catData)!;
            }
            //Size Response List to convert Json Strings
            HttpResponseMessage sizeResponse = httpClient.GetAsync($"{BASEURL}Size/").Result;
            if (sizeResponse.IsSuccessStatusCode)
            {
                var sizeData = sizeResponse.Content.ReadAsStringAsync().Result;
                sizesList = JsonConvert.DeserializeObject<List<Sizes>>(sizeData)!;
            }

            //Instancate New ViewModel to update selectlist in ProductsModel
            var viewModel = new ProductVM
            {
                //Declartion of Select List content to the CategoryList in the View Model
                CategoryList = CategoriesList.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),

                //Declartion of Select List content to the SizeList in the View Model
                SizeList = sizesList.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Create(ProductVM productvm)
        {
            //if(!ModelState.IsValid) return View(productvm);

            var httpClient = _httpClientFactory.CreateClient("InventoryAPI");

            //Instansiate New Product Model with ViewModel Data
            var prod = new Products
            {
                Name = productvm.Name,
                Image = productvm.Image,
                CategoryId = productvm.SelectedCategory,
                SizeId = productvm.SelectedSize,
                Quantity = productvm.Quantity,
            };

            //Serialize Content stored to the new Product Model to JSON String
            var json = JsonConvert.SerializeObject(prod);
            //Converts serialized to UTF-8 string convension
            var content = new StringContent(json, Encoding.UTF8, "application/Json");

            //Product Response List to convert Json Strings
            HttpResponseMessage reponse = httpClient.PostAsync($"{BASEURL}", content).Result;
            if (reponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to Create Product");
                return View(productvm);
            }
        }
        #endregion
    }
}
