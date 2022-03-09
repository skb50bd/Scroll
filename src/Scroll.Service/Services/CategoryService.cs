using AutoMapper;
using Scroll.Data;
using Scroll.Library.Models;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.EditModels;
using Scroll.Library.Models.Entities;
using Scroll.Service.Data;

namespace Scroll.Service.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _repo;
    private readonly IMapper _mapper;

    public CategoryService(
        IRepository<Category> repo,
        IMapper mapper)
    {
        _repo   = repo;
        _mapper = mapper;
    }

    public async Task<CategoryDto?> Get(int id) =>
        _mapper.Map<CategoryDto?>(
            await _repo.Get(id));

    public async Task<PagedList<CategoryDto>> GetPaged(
        int pageIndex = 0,
        int pageSize = 40) =>
            _mapper.Map<PagedList<CategoryDto>>(
                await _repo.GetAll()
                    .ToPagedList(
                        pageIndex,
                        pageSize));

    public async Task<CategoryDto> Insert(
        CategoryEditModel editModel)
    {
        var category =
            _mapper.Map<Category>(editModel);

        category =
            await _repo.Upsert(category);

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> Update(
        CategoryEditModel editModel)
    {
        var originalCategory =
            await _repo.Get(editModel.Id);

        if (originalCategory is null)
        {
            throw new ArgumentException(
                "Invalid Update Operation. Entity doesn't exist");
        }

        var updatedCategory =
            _mapper.Map(editModel, originalCategory);

        if (updatedCategory is null)
        {
            throw new NullReferenceException(
                $"{nameof(updatedCategory)} is null");
        }

        await _repo.Upsert(updatedCategory);

        return _mapper.Map<CategoryDto>(updatedCategory);
    }

    public Task<bool> Delete(int id) =>
        _repo.Delete(id);

    public Task<bool> Exists(int id) =>
        _repo.Exists(id);
}