using System;
using Actions.Core.Domain.StatusHistories.Enums;
using Actions.Core.Domain.Users.Entities;
using FluentValidation;
using Shared.Core.Domain.Impl.Entity;
using Shared.CrossCutting.Tools;

namespace Actions.Core.Domain.StatusHistories.Entities
{
    public class StatusHistory : Entity<StatusHistory, string>
    {
        protected StatusHistory(string id, DateTime date, string userId, StatusHistoryEnum status, string metadataId)
        {
            Id = id;
            Date = date;
            UserId = userId;
            Status = status;
            MetadataId = metadataId;
        }
        public StatusHistory(DateTime date, string userId, StatusHistoryEnum status, string metadataId)
        : this (GuidExtensions.GenerateGuid(), date, userId, status, metadataId) { }

        public DateTime Date { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public StatusHistoryEnum Status { get; set; }
        public string MetadataId { get; set; }
    }

    public class StatusHistoryValidator : AbstractValidator<StatusHistory>
    {
        public StatusHistoryValidator()
        {
            RuleFor(x => x.Date).NotNull().WithMessage("Date is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.Status).IsInEnum().WithMessage("Status is required");
            RuleFor(x => x.MetadataId).NotEmpty().WithMessage("MetadataId is required");
        }
    }
}