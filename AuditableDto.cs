namespace Art.GenAI.DTO;

/// <summary>
/// Common audit / soft-delete fields shared by most DTOs.
/// </summary>
public abstract class AuditableDto
{
    public string? CreatedBy        { get; set; }
    public DateTime CreatedAt       { get; set; }
    public string? ModifiedBy       { get; set; }
    public DateTime? ModifiedAt     { get; set; }
    public bool IsDeleted           { get; set; }
}

// ────────────────────────────────────────────────────────────────────────────────
// 1. Model registry
// ────────────────────────────────────────────────────────────────────────────────
public sealed class GenAIModelDto : AuditableDto
{
    public int Id                  { get; set; }
    public string Name             { get; set; } = default!;
    public string? Provider        { get; set; }
    public string? Version         { get; set; }
    public string? License         { get; set; }
    public bool IsActive           { get; set; }
}

// ────────────────────────────────────────────────────────────────────────────────
// 2. Conversation types
// ────────────────────────────────────────────────────────────────────────────────
public sealed class GenAIConversationTypeDto : AuditableDto
{
    public int Id                       { get; set; }
    public string Name                  { get; set; } = default!;
    public string DefaultSystemPrompt   { get; set; } = default!;
    public int Version                  { get; set; }
    public bool IsActive                { get; set; }
    public string? RiskLevel            { get; set; }   // Low | Medium | High
    public string? UseCaseCategory      { get; set; }
}

// ────────────────────────────────────────────────────────────────────────────────
// 3. Conversation sessions
// ────────────────────────────────────────────────────────────────────────────────
public sealed class GenAIConversationDto : AuditableDto
{
    public Guid Id                   { get; set; }
    public string UserId             { get; set; } = default!;
    public int ConversationTypeId    { get; set; }
    public DateTime StartedAt        { get; set; }

    // Optional expansion
    public GenAIConversationTypeDto? ConversationType { get; set; }
}

// ────────────────────────────────────────────────────────────────────────────────
// 4. System prompt overrides
// ────────────────────────────────────────────────────────────────────────────────
public sealed class GenAISystemPromptOverrideDto : AuditableDto
{
    public int Id                    { get; set; }
    public Guid ConversationId       { get; set; }
    public string OverriddenPrompt   { get; set; } = default!;
    public string? PromptType        { get; set; }     // e.g. “Temperature”
    public string? ReasonForOverride { get; set; }
    public int Version               { get; set; }
    public DateTime SetAt            { get; set; }
}

// ────────────────────────────────────────────────────────────────────────────────
// 5. Message log
// ────────────────────────────────────────────────────────────────────────────────
public sealed class GenAIMessageDto : AuditableDto
{
    public Guid Id                      { get; set; }
    public Guid ConversationId          { get; set; }
    public string Sender                { get; set; } = default!; // "user" | "assistant"
    public int MessageSequence          { get; set; }
    public string Content               { get; set; } = default!;
    public decimal? RelevancePercentage { get; set; }
    public int? ModelId                 { get; set; }
    public bool WasDecisionMade         { get; set; }
    public bool RequiresHumanReview     { get; set; }
    public bool IsFinalOutput           { get; set; }

    public GenAIModelDto? Model         { get; set; }
}

// ────────────────────────────────────────────────────────────────────────────────
// 6. Citations
// ────────────────────────────────────────────────────────────────────────────────
public sealed class GenAICitationDto : AuditableDto
{
    public int Id                { get; set; }
    public Guid MessageId        { get; set; }
    public string SourceUrl      { get; set; } = default!;
    public string? Description   { get; set; }
}

// ────────────────────────────────────────────────────────────────────────────────
// 7. User feedback
// ────────────────────────────────────────────────────────────────────────────────
public sealed class GenAIUserFeedbackDto : AuditableDto
{
    public int Id                { get; set; }
    public Guid MessageId        { get; set; }
    public int Rating            { get; set; }            // 1-5
    public string? FeedbackType  { get; set; }            // thumbs_up, flagged…
    public string? Comments      { get; set; }
    public string? FeedbackSource{ get; set; }            // UI, API, Slack
    public string? SubmittedBy   { get; set; }
    public DateTime SubmittedAt  { get; set; }
}


// This file includes manual mapping extensions for all GenAI DTO <-> Entity types
// Namespace can be changed to match your project structure

using System;
using System.Collections.Generic;
using Art.GenAI.DTO;
using Art.GenAI.Entities;

namespace Art.GenAI.Mappers
{
    public static class GenAIModelMapper
    {
        public static GenAIModelDto ToDto(this GenAIModel e) => new()
        {
            Id = e.Id,
            Name = e.Name,
            Provider = e.Provider,
            Version = e.Version,
            License = e.License,
            IsActive = e.IsActive,
            CreatedBy = e.CreatedBy,
            CreatedAt = e.CreatedAt,
            ModifiedBy = e.ModifiedBy,
            ModifiedAt = e.ModifiedAt,
            IsDeleted = e.IsDeleted
        };

        public static GenAIModel ToEntity(this GenAIModelDto d) => new()
        {
            Id = d.Id,
            Name = d.Name,
            Provider = d.Provider,
            Version = d.Version,
            License = d.License,
            IsActive = d.IsActive,
            CreatedBy = d.CreatedBy,
            CreatedAt = d.CreatedAt,
            ModifiedBy = d.ModifiedBy,
            ModifiedAt = d.ModifiedAt,
            IsDeleted = d.IsDeleted
        };
    }

    public static class GenAIConversationTypeMapper
    {
        public static GenAIConversationTypeDto ToDto(this GenAIConversationType e) => new()
        {
            Id = e.Id,
            Name = e.Name,
            DefaultSystemPrompt = e.DefaultSystemPrompt,
            Version = e.Version,
            RiskLevel = e.RiskLevel,
            UseCaseCategory = e.UseCaseCategory,
            IsActive = e.IsActive,
            CreatedBy = e.CreatedBy,
            CreatedAt = e.CreatedAt,
            ModifiedBy = e.ModifiedBy,
            ModifiedAt = e.ModifiedAt,
            IsDeleted = e.IsDeleted
        };

        public static GenAIConversationType ToEntity(this GenAIConversationTypeDto d) => new()
        {
            Id = d.Id,
            Name = d.Name,
            DefaultSystemPrompt = d.DefaultSystemPrompt,
            Version = d.Version,
            RiskLevel = d.RiskLevel,
            UseCaseCategory = d.UseCaseCategory,
            IsActive = d.IsActive,
            CreatedBy = d.CreatedBy,
            CreatedAt = d.CreatedAt,
            ModifiedBy = d.ModifiedBy,
            ModifiedAt = d.ModifiedAt,
            IsDeleted = d.IsDeleted
        };
    }

    public static class GenAIConversationMapper
    {
        public static GenAIConversationDto ToDto(this GenAIConversation e) => new()
        {
            Id = e.Id,
            UserId = e.UserId,
            ConversationTypeId = e.ConversationTypeId,
            StartedAt = e.StartedAt,
            CreatedBy = e.CreatedBy,
            CreatedAt = e.CreatedAt,
            ModifiedBy = e.ModifiedBy,
            ModifiedAt = e.ModifiedAt,
            IsDeleted = e.IsDeleted,
            ConversationType = e.ConversationType?.ToDto()
        };

        public static GenAIConversation ToEntity(this GenAIConversationDto d) => new()
        {
            Id = d.Id,
            UserId = d.UserId,
            ConversationTypeId = d.ConversationTypeId,
            StartedAt = d.StartedAt,
            CreatedBy = d.CreatedBy,
            CreatedAt = d.CreatedAt,
            ModifiedBy = d.ModifiedBy,
            ModifiedAt = d.ModifiedAt,
            IsDeleted = d.IsDeleted
        };
    }

    public static class GenAISystemPromptOverrideMapper
    {
        public static GenAISystemPromptOverrideDto ToDto(this GenAISystemPromptOverride e) => new()
        {
            Id = e.Id,
            ConversationId = e.ConversationId,
            OverriddenPrompt = e.OverriddenPrompt,
            PromptType = e.PromptType,
            ReasonForOverride = e.ReasonForOverride,
            Version = e.Version,
            SetAt = e.SetAt,
            CreatedBy = e.CreatedBy,
            CreatedAt = e.CreatedAt,
            ModifiedBy = e.ModifiedBy,
            ModifiedAt = e.ModifiedAt,
            IsDeleted = e.IsDeleted
        };

        public static GenAISystemPromptOverride ToEntity(this GenAISystemPromptOverrideDto d) => new()
        {
            Id = d.Id,
            ConversationId = d.ConversationId,
            OverriddenPrompt = d.OverriddenPrompt,
            PromptType = d.PromptType,
            ReasonForOverride = d.ReasonForOverride,
            Version = d.Version,
            SetAt = d.SetAt,
            CreatedBy = d.CreatedBy,
            CreatedAt = d.CreatedAt,
            ModifiedBy = d.ModifiedBy,
            ModifiedAt = d.ModifiedAt,
            IsDeleted = d.IsDeleted
        };
    }

    public static class GenAIMessageMapper
    {
        public static GenAIMessageDto ToDto(this GenAIMessage e) => new()
        {
            Id = e.Id,
            ConversationId = e.ConversationId,
            Sender = e.Sender,
            MessageSequence = e.MessageSequence,
            Content = e.Content,
            RelevancePercentage = e.RelevancePercentage,
            ModelId = e.ModelId,
            WasDecisionMade = e.WasDecisionMade,
            RequiresHumanReview = e.RequiresHumanReview,
            IsFinalOutput = e.IsFinalOutput,
            CreatedBy = e.CreatedBy,
            CreatedAt = e.CreatedAt,
            ModifiedBy = e.ModifiedBy,
            ModifiedAt = e.ModifiedAt,
            IsDeleted = e.IsDeleted,
            Model = e.Model?.ToDto()
        };

        public static GenAIMessage ToEntity(this GenAIMessageDto d) => new()
        {
            Id = d.Id,
            ConversationId = d.ConversationId,
            Sender = d.Sender,
            MessageSequence = d.MessageSequence,
            Content = d.Content,
            RelevancePercentage = d.RelevancePercentage,
            ModelId = d.ModelId,
            WasDecisionMade = d.WasDecisionMade,
            RequiresHumanReview = d.RequiresHumanReview,
            IsFinalOutput = d.IsFinalOutput,
            CreatedBy = d.CreatedBy,
            CreatedAt = d.CreatedAt,
            ModifiedBy = d.ModifiedBy,
            ModifiedAt = d.ModifiedAt,
            IsDeleted = d.IsDeleted
        };
    }

    public static class GenAICitationMapper
    {
        public static GenAICitationDto ToDto(this GenAICitation e) => new()
        {
            Id = e.Id,
            MessageId = e.MessageId,
            SourceUrl = e.SourceUrl,
            Description = e.Description,
            CreatedBy = e.CreatedBy,
            CreatedAt = e.CreatedAt,
            ModifiedBy = e.ModifiedBy,
            ModifiedAt = e.ModifiedAt,
            IsDeleted = e.IsDeleted
        };

        public static GenAICitation ToEntity(this GenAICitationDto d) => new()
        {
            Id = d.Id,
            MessageId = d.MessageId,
            SourceUrl = d.SourceUrl,
            Description = d.Description,
            CreatedBy = d.CreatedBy,
            CreatedAt = d.CreatedAt,
            ModifiedBy = d.ModifiedBy,
            ModifiedAt = d.ModifiedAt,
            IsDeleted = d.IsDeleted
        };
    }

    public static class GenAIUserFeedbackMapper
    {
        public static GenAIUserFeedbackDto ToDto(this GenAIUserFeedback e) => new()
        {
            Id = e.Id,
            MessageId = e.MessageId,
            Rating = e.Rating,
            FeedbackType = e.FeedbackType,
            Comments = e.Comments,
            FeedbackSource = e.FeedbackSource,
            SubmittedBy = e.SubmittedBy,
            SubmittedAt = e.SubmittedAt,
            IsDeleted = e.IsDeleted
        };

        public static GenAIUserFeedback ToEntity(this GenAIUserFeedbackDto d) => new()
        {
            Id = d.Id,
            MessageId = d.MessageId,
            Rating = d.Rating,
            FeedbackType = d.FeedbackType,
            Comments = d.Comments,
            FeedbackSource = d.FeedbackSource,
            SubmittedBy = d.SubmittedBy,
            SubmittedAt = d.SubmittedAt,
            IsDeleted = d.IsDeleted
        };
    }
}






// These are basic EF Core services for loading and saving GenAI entities
// You can register these in DI as scoped services in your application

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Art.GenAI.DTO;
using Art.GenAI.Entities;
using Art.GenAI.Mappers;

namespace Art.GenAI.Services
{
    public class GenAIConversationService
    {
        private readonly GenAIDbContext _context;

        public GenAIConversationService(GenAIDbContext context)
        {
            _context = context;
        }

        public async Task<List<GenAIConversationDto>> GetAllConversationsAsync(string userId)
        {
            var entities = await _context.GenAIConversations
                .Include(c => c.ConversationType)
                .Where(c => c.UserId == userId && !c.IsDeleted)
                .ToListAsync();

            return entities.Select(c => c.ToDto()).ToList();
        }

        public async Task<GenAIConversationDto?> GetConversationByIdAsync(Guid id)
        {
            var entity = await _context.GenAIConversations
                .Include(c => c.ConversationType)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            return entity?.ToDto();
        }

        public async Task<Guid> CreateConversationAsync(GenAIConversationDto dto)
        {
            var entity = dto.ToEntity();
            _context.GenAIConversations.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateConversationAsync(GenAIConversationDto dto)
        {
            var entity = await _context.GenAIConversations.FindAsync(dto.Id);
            if (entity == null || entity.IsDeleted) return;

            entity.UserId = dto.UserId;
            entity.ConversationTypeId = dto.ConversationTypeId;
            entity.ModifiedBy = dto.ModifiedBy;
            entity.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteConversationAsync(Guid id)
        {
            var entity = await _context.GenAIConversations.FindAsync(id);
            if (entity == null || entity.IsDeleted) return;

            entity.IsDeleted = true;
            entity.ModifiedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public class GenAIMessageService
    {
        private readonly GenAIDbContext _context;

        public GenAIMessageService(GenAIDbContext context)
        {
            _context = context;
        }

        public async Task<List<GenAIMessageDto>> GetMessagesByConversationIdAsync(Guid conversationId)
        {
            var messages = await _context.GenAIMessages
                .Include(m => m.Model)
                .Where(m => m.ConversationId == conversationId && !m.IsDeleted)
                .OrderBy(m => m.MessageSequence)
                .ToListAsync();

            return messages.Select(m => m.ToDto()).ToList();
        }

        public async Task<Guid> AddMessageAsync(GenAIMessageDto dto)
        {
            var entity = dto.ToEntity();
            _context.GenAIMessages.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteMessageAsync(Guid id)
        {
            var entity = await _context.GenAIMessages.FindAsync(id);
            if (entity == null || entity.IsDeleted) return;

            entity.IsDeleted = true;
            entity.ModifiedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public class GenAIModelService
    {
        private readonly GenAIDbContext _context;

        public GenAIModelService(GenAIDbContext context)
        {
            _context = context;
        }

        public async Task<List<GenAIModelDto>> GetModelsAsync()
        {
            return await _context.GenAIModels
                .Where(m => !m.IsDeleted)
                .Select(m => m.ToDto())
                .ToListAsync();
        }

        public async Task<int> AddModelAsync(GenAIModelDto dto)
        {
            var entity = dto.ToEntity();
            _context.GenAIModels.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
    }

    public class GenAIUserFeedbackService
    {
        private readonly GenAIDbContext _context;

        public GenAIUserFeedbackService(GenAIDbContext context)
        {
            _context = context;
        }

        public async Task SubmitFeedbackAsync(GenAIUserFeedbackDto dto)
        {
            var entity = dto.ToEntity();
            _context.GenAIUserFeedbacks.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GenAIUserFeedbackDto>> GetFeedbackForMessageAsync(Guid messageId)
        {
            return await _context.GenAIUserFeedbacks
                .Where(f => f.MessageId == messageId && !f.IsDeleted)
                .Select(f => f.ToDto())
                .ToListAsync();
        }
    }

    public class GenAICitationService
    {
        private readonly GenAIDbContext _context;

        public GenAICitationService(GenAIDbContext context)
        {
            _context = context;
        }

        public async Task<List<GenAICitationDto>> GetCitationsByMessageIdAsync(Guid messageId)
        {
            return await _context.GenAICitations
                .Where(c => c.MessageId == messageId && !c.IsDeleted)
                .Select(c => c.ToDto())
                .ToListAsync();
        }

        public async Task<int> AddCitationAsync(GenAICitationDto dto)
        {
            var entity = dto.ToEntity();
            _context.GenAICitations.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
    }

    public class GenAISystemPromptOverrideService
    {
        private readonly GenAIDbContext _context;

        public GenAISystemPromptOverrideService(GenAIDbContext context)
        {
            _context = context;
        }

        public async Task<List<GenAISystemPromptOverrideDto>> GetOverridesForConversationAsync(Guid conversationId)
        {
            return await _context.GenAISystemPromptOverrides
                .Where(p => p.ConversationId == conversationId && !p.IsDeleted)
                .Select(p => p.ToDto())
                .ToListAsync();
        }

        public async Task<int> AddOverrideAsync(GenAISystemPromptOverrideDto dto)
        {
            var entity = dto.ToEntity();
            _context.GenAISystemPromptOverrides.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
    }

    public class GenAIConversationTypeService
    {
        private readonly GenAIDbContext _context;

        public GenAIConversationTypeService(GenAIDbContext context)
        {
            _context = context;
        }

        public async Task<List<GenAIConversationTypeDto>> GetAllAsync()
        {
            return await _context.GenAIConversationTypes
                .Where(ct => !ct.IsDeleted)
                .Select(ct => ct.ToDto())
                .ToListAsync();
        }

        public async Task<int> AddAsync(GenAIConversationTypeDto dto)
        {
            var entity = dto.ToEntity();
            _context.GenAIConversationTypes.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
    }
}



