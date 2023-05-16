using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProductManagementWebClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public ProductController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7027/api/product";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + "/GetProducts");
            List<Product>? products = new List<Product>();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                products = response.Content.ReadFromJsonAsync<List<Product>>().Result;
            }

            return View(products);
        }

        // GET: ProductController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + "/GetProductById/id?id=" + id);

            Product product = new Product();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                product = response.Content.ReadFromJsonAsync<Product>().Result;
            }

            HttpResponseMessage response2 = await client.GetAsync(ProductApiUrl + "/GetCategories");
            List<Category>? categories = new List<Category>();
            if (response2.StatusCode == System.Net.HttpStatusCode.OK)
            {
                categories = response2.Content.ReadFromJsonAsync<List<Category>>().Result;
            }
            ViewData["category"] = categories;
            return View(product);
        }

        //GET: ProductsController/Create
        public async Task<IActionResult> Create()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + "/GetCategories");
            List<Category>? categories = new List<Category>();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                categories = response.Content.ReadFromJsonAsync<List<Category>>().Result;
            }
            ViewData["category"] = categories;
            return View();
        }

        // GET: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product p)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(ProductApiUrl + "/PostProduct", p);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return Redirect("Create");
        }

        // GET: ProductController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + "/GetProductById/id?id=" + id);
            Product product = new Product();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                product = response.Content.ReadFromJsonAsync<Product>().Result;
            }

            HttpResponseMessage response2 = await client.GetAsync(ProductApiUrl + "/GetCategories");
            List<Category>? categories = new List<Category>();
            if (response2.StatusCode == System.Net.HttpStatusCode.OK)
            {
                categories = response2.Content.ReadFromJsonAsync<List<Category>>().Result;
            }
            ViewData["category"] = categories;
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product p)
        {
            if (ModelState.IsValid)
            {
                await client.PutAsJsonAsync(ProductApiUrl + "/UpdateProduct/id?id=" + id, p);
                return RedirectToAction("Index");
            }
            return View(p);
        }

        // GET: ProductController/Delete.5
        public async Task<IActionResult> Delete(int id)
        {
            await client.DeleteAsync(ProductApiUrl + "/DeleteProduct/id?id=" + id);
            return RedirectToAction("Index");
        }

    }
}
