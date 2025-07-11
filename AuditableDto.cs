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



using AutoMapper;
using Art.GenAI.Entities;
using Art.GenAI.DTO;

public class GenAIProfile : Profile
{
    public GenAIProfile()
    {
        // Model Registry
        CreateMap<GenAIModel, GenAIModelDto>().ReverseMap();

        // Conversation Type
        CreateMap<GenAIConversationType, GenAIConversationTypeDto>().ReverseMap();

        // Conversation
        CreateMap<GenAIConversation, GenAIConversationDto>()
            .ForMember(dest => dest.ConversationType, opt => opt.MapFrom(src => src.ConversationType))
            .ReverseMap();

        // System Prompt Override
        CreateMap<GenAISystemPromptOverride, GenAISystemPromptOverrideDto>().ReverseMap();

        // Message
        CreateMap<GenAIMessage, GenAIMessageDto>()
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ReverseMap();

        // Citation
        CreateMap<GenAICitation, GenAICitationDto>().ReverseMap();

        // Feedback
        CreateMap<GenAIUserFeedback, GenAIUserFeedbackDto>().ReverseMap();
    }
}


builder.Services.AddAutoMapper(typeof(GenAIProfile).Assembly);


