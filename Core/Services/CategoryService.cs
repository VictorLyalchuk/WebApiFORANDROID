﻿using AutoMapper;
using Core.DTO.Category;
using Core.Entities;
using Core.Interfaces;

namespace Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<CategoryEntity> _categoryRepository;
        private readonly IFilesService _filesService;

        public CategoryService(IRepository<CategoryEntity> categoryRepository, IMapper mapper, IFilesService filesService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _filesService = filesService;
        }

        public async Task CreateAsync(CreateCategoryDTO createCategoryDTO)
        {
            var category = _mapper.Map<CategoryEntity>(createCategoryDTO);
            if (createCategoryDTO.ImageFile != null)
            {
                category.ImagePath = await _filesService.SaveImage(createCategoryDTO.ImageFile!);
            }
            await _categoryRepository.InsertAsync(category);
            await _categoryRepository.SaveAsync();
        }

        public async Task DeleteCategoryByIDAsync(int id)
        {
            var categoryToDelete = await _categoryRepository.GetByIDAsync(id);
            if (categoryToDelete != null)
            {
                if (categoryToDelete.ImagePath != null)
                {
                    await _filesService.DeleteImage(categoryToDelete.ImagePath!);
                }
                await _categoryRepository.DeleteAsync(categoryToDelete);
                await _categoryRepository.SaveAsync();
            }
        }

        public async Task EditAsync(EditCategoryDTO editCategoryDTO)
        {
            var category = _mapper.Map<CategoryEntity>(editCategoryDTO);
            if (category.ImagePath != null)
            {
                var entity = await _categoryRepository.GetByIDAsync(editCategoryDTO.Id);
                if (entity.ImagePath != null)
                {
                    await _filesService.DeleteImage(category.ImagePath!);
                    category.ImagePath = await _filesService.SaveImage(editCategoryDTO.ImageFile!);
                }
            }
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveAsync();
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAsync();
            return _mapper.Map<List<CategoryDTO>>(categories);

        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIDAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
