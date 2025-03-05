using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Service.Interfaces;

namespace FUNewsManagementSystem.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["ParentCategoryId"] = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "CategoryDesciption");
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _categoryService.AddCategoryAsync(Category);

            return RedirectToPage("./Index");
        }
    }
}
