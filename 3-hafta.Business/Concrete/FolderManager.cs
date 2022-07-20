﻿using _3_hafta.Business.Abstract;
using _3_hafta.Business.Validation.FluentValidation;
using _3_hafta.DataAccess.Abstract;
using _3_hafta.Dto.Concrete;
using _3_hafta.Entity.Concrete;
using AutoMapper;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result;

namespace _3_hafta.Business.Concrete
{
    public class FolderManager : BaseManager<Folder, FolderDto>, IFolderService
    {
        public FolderManager(IFolderDal entityRepository, IMapper mapper) : base(entityRepository, mapper)
        {
        }

        [ValidationAspect(typeof(FolderValidator))]
        public override Task<IResult> AddAsync(FolderDto entity)
        {
            return base.AddAsync(entity);
        }
        [ValidationAspect(typeof(FolderValidator))]
        public override Task<IResult> UpdateAsync(int id, FolderDto entity)
        {
            return base.UpdateAsync(id, entity);
        }
    }
}
