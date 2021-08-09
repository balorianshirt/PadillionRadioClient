using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PadillionRadio.Business.Enums;
using PadillionRadio.Business.Models;
using PadillionRadio.Data.Entities;
using PadillionRadio.Data.Interfaces;

namespace PadillionRadio.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserIOS> repository;
        private readonly ILogger<UserService> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserService(IRepository<UserIOS> repository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<List<UserModel>> GetList()
        {
            try
            {
                var models = mapper.Map<List<UserModel>>(await repository.GetItems());

                return models;
            }
            catch (Exception e)
            {
                logger.LogError(e, "error occured while listing users");
                return new List<UserModel>();
            }
        }

        public async Task<UserModel> Get(long id)
        {
            try
            {
                var item = await repository.GetItem(id);
                return mapper.Map<UserModel>(item);
            }
            catch (Exception e)
            {
                logger.LogError(e, "error occured while getting user");
                return null;
            }
        }

        public async Task<UserModel> Create(UserModel model)
        {
            try
            {
                await repository.Create(mapper.Map<UserIOS>(model));
                await unitOfWork.Save();

                return model;
            }
            catch (Exception e)
            {
                logger.LogError(e, "error occured while creating user");
                return null;
            }
        }

        public async Task<bool> Update(UserModel model)
        {
            try
            {
                var local = unitOfWork.Context.Set<UserIOS>().Local.FirstOrDefault(entry => entry.Id.Equals(model.Id));
                if (local != null) repository.Detatch(local);
                repository.Update(mapper.Map<UserIOS>(model));
                await unitOfWork.Save();

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e, "error occured while updating user");

                return false;
            }
        }

        public async Task<UserModel> Delete(long id)
        {
            try
            {
                var model = await Get(id);
                var local = unitOfWork.Context.Set<UserIOS>().Local.FirstOrDefault(entry => entry.Id.Equals(model.Id));
                if (local != null)
                {
                    repository.Detatch(local);
                }

                repository.Delete(mapper.Map<UserIOS>(model));
                await unitOfWork.Save();

                return model;
            }
            catch (Exception e)
            {
                logger.LogError(e, "error occured while deleting users");
                return null;
            }
        }

        public UserModel[] Search(string searchString, IQueryable<UserModel> models, SearchEnum searchEnum)
        {
            try
            {
                foreach (var views in models)
                {
                    var local = unitOfWork.Context.Set<UserIOS>().Local
                        .FirstOrDefault(entry => entry.Id.Equals(views.Id));

                    if (local != null)
                        repository.Detatch(local);
                }

                var query = searchEnum switch
                {
                    SearchEnum.Email => models.Where(x => x.Email == searchString),
                    SearchEnum.Code => models.Where(x => x.Code == searchString),
                    SearchEnum.DeviceIdentifier => models.Where(x => x.DeviceIdentifier == searchString),
                    _ => models.Where(x =>
                        x.DeviceIdentifier == searchString || x.Code == searchString || x.Email == searchString)
                };

                if (query.Any() == false)
                {
                    return Array.Empty<UserModel>();
                }
                
                var searcher = mapper.Map<UserModel[]>(query);
                
                return searcher;
            }
            catch (Exception e)
            {
                logger.LogError(e, "error occured while searching through users");
                return null;
            }
        }
    }
}