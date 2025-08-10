# Noble.Salah Cursor Rules

This directory contains specialized rule files for the Noble.Salah project development. These rules help maintain consistency, quality, and best practices across the codebase.

## Rule Files Overview

### 1. `maui-rule.mdc` - Core MAUI Development Rules
**Purpose**: Main development guidelines for the Noble.Salah MAUI Blazor application.

**Key Areas**:
- Project architecture and structure
- Service patterns and dependency injection
- MudBlazor component usage
- Cross-platform development
- Performance and accessibility guidelines

**When to Apply**: Always apply when working on MAUI-specific features, UI components, or service implementations.

### 2. `prayer-calculations.mdc` - Prayer Calculation Rules
**Purpose**: Specialized guidelines for prayer time calculation implementation.

**Key Areas**:
- Adhan library integration
- Location services and permissions
- Timezone handling
- Data validation and error handling
- Performance optimization for calculations

**When to Apply**: Apply when working on prayer calculation services, location services, or related business logic.

### 3. `ui-components.mdc` - UI Component Development Rules
**Purpose**: Guidelines for creating and maintaining UI components.

**Key Areas**:
- MudBlazor component patterns
- Responsive design implementation
- Theme system integration
- Accessibility requirements
- Component testing and documentation

**When to Apply**: Apply when creating or modifying UI components, implementing themes, or working on responsive design.

### 4. `architecture.mdc` - Architecture and Service Design Rules
**Purpose**: Guidelines for maintaining clean architecture and service design.

**Key Areas**:
- Clean architecture principles
- Service interface design
- Dependency injection patterns
- Error handling strategies
- Configuration management

**When to Apply**: Apply when designing new services, refactoring existing code, or making architectural decisions.

### 5. `testing.mdc` - Testing Rules and Guidelines
**Purpose**: Comprehensive testing guidelines for all aspects of the application.

**Key Areas**:
- Unit testing patterns
- Integration testing strategies
- Performance testing
- Accessibility testing
- Test automation and maintenance

**When to Apply**: Apply when writing tests, setting up test infrastructure, or reviewing test coverage.

### 6. `ramadan-features.mdc` - Ramadan Features and Islamic Functionality Rules
**Purpose**: Guidelines for implementing Islamic-specific features and Ramadan functionality.

**Key Areas**:
- Ramadan mode implementation
- Islamic content guidelines
- Cultural sensitivity
- Community features
- Religious accuracy requirements

**When to Apply**: Apply when implementing Islamic features, Ramadan functionality, or community features.

## How to Use These Rules

### Automatic Application
- Rules are automatically applied based on file patterns and context
- Cursor will suggest rule-compliant code based on the active rule file
- Multiple rules can be active simultaneously for comprehensive guidance

### Manual Application
- Reference specific rule files when working on related features
- Use rule content as checklists during code reviews
- Apply rules when making architectural decisions

### Rule Updates
- Update rules when project requirements change
- Add new rules for emerging patterns or requirements
- Maintain rule consistency across the project

## Rule File Structure

Each rule file follows this structure:
```markdown
---
alwaysApply: false
---

# Rule Title

## Section 1
### Subsection
- Guideline 1
- Guideline 2
- Guideline 3

## Section 2
### Subsection
- Guideline 1
- Guideline 2
- Guideline 3
```

## Best Practices

### Rule Maintenance
- Keep rules up-to-date with project evolution
- Remove obsolete guidelines
- Add new patterns as they emerge
- Ensure rule consistency

### Rule Application
- Apply rules consistently across the team
- Use rules as guidance, not strict requirements
- Adapt rules to specific project needs
- Document rule exceptions when necessary

### Rule Documentation
- Document rule rationale where helpful
- Include examples in rule descriptions
- Reference related documentation
- Maintain rule version history

## Integration with Project Documentation

These rules complement the existing project documentation:
- `Plan.md` - Project planning and goals
- `FEATURES.md` - Feature specifications
- `IMPLEMENTATION_STATUS.md` - Current implementation status
- `.github/copilot/` - AI development guidelines

## Contributing to Rules

When adding or modifying rules:
1. Follow the established structure
2. Include clear, actionable guidelines
3. Provide examples where helpful
4. Update this README when adding new rule files
5. Ensure consistency with existing rules

---

**Last Updated**: December 2024
**Version**: 1.0.0
**Status**: Active and maintained
