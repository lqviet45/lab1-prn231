using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductManagementWebClient.Controllers;

public class ProductController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly string _productApiUrl = "http://lab01_aspnetcorewebapi-lab01_asp.netcorewebapi-1/api/Products";
    private readonly string _categoryApiUrl = "http://lab01_aspnetcorewebapi-lab01_asp.netcorewebapi-1/api/Category";
    // private readonly string _productApiUrl = "http://localhost:5000/api/Products";
    // private readonly string _categoryApiUrl = "http://localhost:5000/api/Category";
    public ProductController()
    {
        _httpClient = new HttpClient();
        var contentTypes = new MediaTypeWithQualityHeaderValue("application/json");
        _httpClient.DefaultRequestHeaders.Accept.Add(contentTypes);
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync(_productApiUrl);
        var strData = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var products = JsonSerializer.Deserialize<IEnumerable<Product>>(strData, options);

        return View(products);
    }

    public async Task<IActionResult> Details(int id)
    {
        var response = await _httpClient.GetAsync($"{_productApiUrl}/{id}");
        var strData = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var product = JsonSerializer.Deserialize<Product>(strData, options);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    public async Task<IActionResult> Create()
    {
        var categories = await GetCategories();

        if (categories == null)
        {
            return NotFound();
        }

        ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(IFormCollection collection)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var categories = await GetCategories();
        
        if (categories == null)
        {
            return NotFound();
        }
        
        ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
        
        var product = new Product
        {
            ProductName = collection["ProductName"],
            UnitPrice = Convert.ToDecimal(collection["UnitPrice"]),
            CategoryId = Convert.ToInt32(collection["CategoryId"])
        };

        var json = JsonSerializer.Serialize(product);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_productApiUrl, content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(product);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var product = await _httpClient.GetAsync($"{_productApiUrl}/{id}");
        var strData = await product.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var productToDelete = JsonSerializer.Deserialize<Product>(strData, options);
        if (productToDelete == null)
        {
            return NotFound();
        }

        return View(productToDelete);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, IFormCollection collection)
    {
        var response = await _httpClient.DeleteAsync($"{_productApiUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        return RedirectToAction("Delete", new { id = id });
    }


    public async Task<IActionResult> Edit(int id)
    {
        var response = await _httpClient.GetAsync($"{_productApiUrl}/{id}");
        var strData = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        var categories = await GetCategories();

        ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

        var product = JsonSerializer.Deserialize<Product>(strData, options);

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, IFormCollection collection)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var categories = await GetCategories();

        ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

        var product = new Product
        {
            ProductId = id,
            ProductName = collection["ProductName"],
            UnitPrice = Convert.ToDecimal(collection["UnitPrice"]),
            CategoryId = Convert.ToInt32(collection["CategoryId"])
        };

        var json = JsonSerializer.Serialize(product);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{_productApiUrl}/{id}", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(product);
        }
    }

    private async Task<IEnumerable<Category>?> GetCategories()
    {
        var responseCategory = await _httpClient.GetAsync(_categoryApiUrl);
        var strDataCategory = await responseCategory.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var categories = JsonSerializer.Deserialize<IEnumerable<Category>>(strDataCategory, options);
        return categories;
    }
}