create table Risk (
    Id varchar(36) not null PRIMARY KEY,
    Code varchar(100) not null,
    Status int not null,
    OwnerId varchar(36) not null FOREIGN KEY REFERENCES [User](Id),
    Name varchar(100) not null,
    Description varchar(MAX) not null,
    Cause varchar(MAX),
    Impact varchar(MAX) not null,
    Category int not null,
    Level int not null,
    Dimension int not null,
    DimensionDescription varchar(MAX),
    ProjectStep int not null,
    CreatedDate datetime not null,
    CreatedById varchar(36) not null FOREIGN KEY REFERENCES [User](Id),
    ClosedCancelledDate datetime,
    ClosedCancelledById varchar(36) FOREIGN KEY REFERENCES [User](Id),
    Justification int,
    RealImpact varchar(MAX)
);

create table Deviation (
    Id varchar(36) not null PRIMARY KEY,
    Code varchar(100) not null,
    Status int not null,
    Name varchar(100) not null,
    Description varchar(MAX) not null,
    Cause varchar(MAX),
    Category int not null,    
    AssociatedRiskId varchar(36) FOREIGN KEY REFERENCES [Risk](Id),
    Priority int not null,
    CreatedDate datetime not null,
    CreatedById varchar(36) not null FOREIGN KEY REFERENCES [User](Id),
    ClosedCancelledDate datetime,
    ClosedCancelledById varchar(36) FOREIGN KEY REFERENCES [User](Id)
);

create table Action (
    Id varchar(36) not null PRIMARY KEY,
    RelatedId varchar(36),
    Description varchar(100) not null,
    ResponsibleId varchar(36) not null FOREIGN KEY REFERENCES [User](Id),
    DueDate datetime not null,
    OriginalDueDate datetime,
    Status int not null,
    ActualStartDate datetime,
    ActualEndDate datetime,
    Cost decimal(18,2),
    Comments varchar(MAX),
    ClosedDate datetime,
    ClosedById varchar(36) FOREIGN KEY REFERENCES [User](Id),
    CreatedDate datetime not null,
);

create table ResponsePlan (
    Id varchar(36) not null PRIMARY KEY,
    ActionDescription varchar(200) not null,
    ResponsibleId varchar(36) not null FOREIGN KEY REFERENCES [User](Id),
    DueDate datetime not null,
    OriginalDueDate datetime,
    Status int not null,
    ActualStartDate datetime,
    ActualEndDate datetime,
    Cost decimal(18,2),
    Comments varchar(MAX),
    CreatedDate datetime not null,
    ClosedDate datetime,
    ClosedById varchar(36) FOREIGN KEY REFERENCES [User](Id),
    MetadataId varchar(36)
);

create table StatusHistory (
    Id varchar(36) not null PRIMARY KEY,
    Date datetime not null,
    UserId varchar(36) not null FOREIGN KEY REFERENCES [User](Id),
    Status int not null
);

create table RisksTasks (
    Id varchar(36) not null PRIMARY KEY,
    RiskId varchar(36) not null FOREIGN KEY REFERENCES [Risk](Id),
    TaskId varchar(36) not null FOREIGN KEY REFERENCES [ProjectTask](Id),
);