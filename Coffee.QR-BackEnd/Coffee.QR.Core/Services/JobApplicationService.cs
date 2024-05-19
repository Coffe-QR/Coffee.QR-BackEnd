﻿using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class JobApplicationService : CrudService<JobApplicationDto, JobApplication>, IJobApplicationService
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;


        public JobApplicationService(ICrudRepository<JobApplication> crudRepository, IMapper mapper, IJobApplicationRepository jobApplicationRepository)
            : base(crudRepository, mapper)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public Result<JobApplicationDto> CreateJobApplication(JobApplicationDto jobApplicationDto)
        {
            try
            {
                var jobApplicationt = _jobApplicationRepository.Create(new JobApplication(jobApplicationDto.FirstName, jobApplicationDto.LastName, jobApplicationDto.Email, jobApplicationDto.Phone, jobApplicationDto.DateOfBirth, jobApplicationDto.Address, jobApplicationDto.ApplicationDate, jobApplicationDto.LocalId, jobApplicationDto.ApplicantDescription, (JobPosition)Enum.Parse(typeof(JobPosition), jobApplicationDto.Position.ToString())));

                JobApplicationDto resultDto = new JobApplicationDto
                {
                    Id = jobApplicationt.Id,
                    FirstName = jobApplicationt.FirstName,
                    LastName = jobApplicationt.LastName,
                    Email = jobApplicationt.Email,
                    Phone = jobApplicationt.Phone,
                    DateOfBirth = jobApplicationt.DateOfBirth,
                    Address = jobApplicationt.Address,
                    ApplicationDate = jobApplicationt.ApplicationDate,
                    LocalId = jobApplicationt.LocalId,
                    ApplicantDescription = jobApplicationt.ApplicantDescription,
                    Position = (JobPositionDto)Enum.Parse(typeof(JobPositionDto), jobApplicationDto.Position.ToString(),true),

                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<JobApplicationDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<JobApplicationDto>> GetAllJobApplications()
        {
            try
            {
                var jobApplications = _jobApplicationRepository.GetAll();
                var jobApplicationDtos = jobApplications.Select(j => new JobApplicationDto
                {
                    Id=j.Id,
                    FirstName = j.FirstName,
                    LastName = j.LastName,
                    Email = j.Email,
                    Phone = j.Phone,
                    DateOfBirth = j.DateOfBirth,
                    Address = j.Address,
                    ApplicationDate = j.ApplicationDate,
                    LocalId = j.LocalId,
                    ApplicantDescription=j.ApplicantDescription,
                    Position = (JobPositionDto)Enum.Parse(typeof(JobPositionDto), j.Position.ToString(), true),

                }).ToList();

                return Result.Ok(jobApplicationDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<JobApplicationDto>>("Failed to retrieve jobApplications").WithError(e.Message);
            }
        }


        public bool DeleteJobApplication(long jobApplicationId)
        {
            var jobApplciationToDelete = _jobApplicationRepository.Delete(jobApplicationId);
            return jobApplciationToDelete != null;
        }


        public Task<Result<JobApplicationDto>> GetJobApplicationByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<JobApplicationDto>> UpdateJobApplicationAsync(JobApplicationDto jobApplicationDto)
        {
            throw new NotImplementedException();
        }

        public Result<List<JobApplicationDto>> GetAllByLocalId(long localId)
        {
            try
            {
                var jobs = _jobApplicationRepository.GetAllByLocalId(localId);
                var jobsDtos = jobs.Select(j => new JobApplicationDto
                {
                    Id = j.Id,
                    FirstName = j.FirstName,
                    LastName = j.LastName,
                    Email = j.Email,
                    Phone = j.Phone,
                    DateOfBirth = j.DateOfBirth,
                    Address = j.Address,
                    ApplicationDate = j.ApplicationDate,
                    LocalId = j.LocalId,
                    ApplicantDescription = j.ApplicantDescription,
                    Position = (JobPositionDto)Enum.Parse(typeof(JobPositionDto), j.Position.ToString(), true),

                }).ToList();

                return Result.Ok(jobsDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<JobApplicationDto>>("Failed to retrieve job applications for local").WithError(e.Message);
            }
        }
    }
}
