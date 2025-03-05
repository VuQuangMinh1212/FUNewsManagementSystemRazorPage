using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Service.Interfaces;

namespace FUNewsManagementSystem.Pages.NewsArticles
{
    public class IndexModel : PageModel
    {
        private readonly INewArticleService _newArticleService;
        private readonly ICategoryService _categoryService;

        public IndexModel(INewArticleService newArticleService, ICategoryService categoryService)
        {
            _newArticleService = newArticleService;
            _categoryService = categoryService;
        }

        public IList<NewsArticle> NewsArticle { get; set; } = default!;
        public List<Category> Categories { get; set; } = default!;

        public async Task OnGetAsync()
        {
            IEnumerable<NewsArticle> newsArticles = await _newArticleService.GetAllNewsArticlesAsync();
            IEnumerable<Category> categories = await _categoryService.GetAllCategoriesAsync();
            Categories = categories.ToList();
            NewsArticle = newsArticles.ToList();
        }
    }
}
