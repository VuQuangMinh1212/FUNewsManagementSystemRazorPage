using BusinessObjects;
using Microsoft.AspNetCore.SignalR;
using Repository.UOW;
using Service.Hubs; 
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services
{
    public class NewsArticleService : INewArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<NewsHub> _newsHubContext;

        public NewsArticleService(IUnitOfWork unitOfWork, IHubContext<NewsHub> newsHubContext)
        {
            _unitOfWork = unitOfWork;
            _newsHubContext = newsHubContext;
        }

        public async Task AddNewsArticleAsync(NewsArticle article, List<int> tags)
        {
            await _unitOfWork.NewsArticleRepository.CreateAsync(article, tags);
            await _newsHubContext.Clients.All.SendAsync("ReceiveNewsUpdate", $"New article published: {article.NewsTitle}");
        }

        public async Task DeleteNewsArticleAsync(string id)
        {
            await _unitOfWork.NewsArticleRepository.DeleteAsync(id);
            await _newsHubContext.Clients.All.SendAsync("ReceiveNewsUpdate", "An article was deleted!");
        }

        public async Task<IEnumerable<NewsArticle>> GetAllNewsArticlesAsync()
        {
            return await _unitOfWork.NewsArticleRepository.GetAllAsync();
        }

        public async Task<IEnumerable<NewsArticle>> GetFilteredNewsArticlesAsync(string searchTitle, int? categoryFilter)
        {
            return await _unitOfWork.NewsArticleRepository.GetFilteredNewsArticlesAsync(searchTitle, categoryFilter);
        }

        public async Task<IEnumerable<NewsArticle>> GetLatestNews(int count)
        {
            return await _unitOfWork.NewsArticleRepository.GetLatestNews(count);
        }

        public async Task<NewsArticle?> GetNewsArticleByIdAsync(string id)
        {
            return await _unitOfWork.NewsArticleRepository.GetByIdAsync(id);
        }

        public async Task<List<NewsArticle>> GetNewsArticlesByStaffIdAsync(short staffId)
        {
            return await _unitOfWork.NewsArticleRepository.GetNewsArticlesByStaffIdAsync(staffId);
        }

        public async Task<IEnumerable<NewsArticle>> GetNewsReportAsync(DateTime? startDate, DateTime? endDate)
        {
            return await _unitOfWork.NewsArticleRepository.GetNewsReportAsync(startDate, endDate);
        }

        public async Task<bool> HasNewsInCategoryAsync(short categoryId)
        {
            return await _unitOfWork.NewsArticleRepository.HasNewsInCategoryAsync(categoryId);
        }

        public async Task UpdateNewsArticleAsync(NewsArticle article, List<int> tags)
        {
            await _unitOfWork.NewsArticleRepository.UpdateAsync(article, tags);
            await _newsHubContext.Clients.All.SendAsync("ReceiveNewsEdit", $"Article updated: {article.NewsTitle}");
        }
    }
    }

