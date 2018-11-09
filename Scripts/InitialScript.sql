IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180717183858_InitialCreate')
BEGIN
    CREATE TABLE [ICD10] (
        [ID] int NOT NULL IDENTITY,
        [Code] nvarchar(10) NOT NULL,
        [Summary] nvarchar(100) NOT NULL,
        [Description] nvarchar(400) NOT NULL,
        [Eye] nchar(1) NOT NULL,
        [CreateDate] datetime2 NOT NULL,
        [ModifyDate] datetime2 NULL,
        CONSTRAINT [PK_ICD10] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180717183858_InitialCreate')
BEGIN
    CREATE TABLE [Users] (
        [ID] int NOT NULL IDENTITY,
        [FirstName] nvarchar(50) NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        [KYLicenseNumber] nvarchar(20) NOT NULL,
        [LDAPID] nvarchar(20) NOT NULL,
        [CreateDate] datetime2 NOT NULL,
        [ModifyDate] datetime2 NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180717183858_InitialCreate')
BEGIN
    CREATE TABLE [DiagnosticReports] (
        [ID] int NOT NULL IDENTITY,
        [PatientDemographicRaw] nvarchar(max) NULL,
        [PatientName] nvarchar(150) NOT NULL,
        [Location] nvarchar(75) NULL,
        [DRS] nvarchar(50) NULL,
        [Gender] nchar(1) NOT NULL,
        [DOB] datetime2 NOT NULL,
        [ImageCaptureDateTime] datetime2 NOT NULL,
        [ManagementPlan] nvarchar(10) NOT NULL,
        [Comments] nvarchar(max) NULL,
        [UserID] int NOT NULL,
        [CreateDate] datetime2 NOT NULL,
        CONSTRAINT [PK_DiagnosticReports] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_DiagnosticReports_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users] ([ID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180717183858_InitialCreate')
BEGIN
    CREATE TABLE [ReportCodes] (
        [DiagnosticReportID] int NOT NULL,
        [ICD10ID] int NOT NULL,
        CONSTRAINT [PK_ReportCodes] PRIMARY KEY ([DiagnosticReportID], [ICD10ID]),
        CONSTRAINT [FK_ReportCodes_DiagnosticReports_DiagnosticReportID] FOREIGN KEY ([DiagnosticReportID]) REFERENCES [DiagnosticReports] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_ReportCodes_ICD10_ICD10ID] FOREIGN KEY ([ICD10ID]) REFERENCES [ICD10] ([ID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180717183858_InitialCreate')
BEGIN
    CREATE INDEX [IX_DiagnosticReports_UserID] ON [DiagnosticReports] ([UserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180717183858_InitialCreate')
BEGIN
    CREATE INDEX [IX_ReportCodes_ICD10ID] ON [ReportCodes] ([ICD10ID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180717183858_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180717183858_InitialCreate', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180726192804_RemovingRequired')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DiagnosticReports]') AND [c].[name] = N'PatientName');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [DiagnosticReports] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [DiagnosticReports] ALTER COLUMN [PatientName] nvarchar(150) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180726192804_RemovingRequired')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DiagnosticReports]') AND [c].[name] = N'ImageCaptureDateTime');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [DiagnosticReports] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [DiagnosticReports] ALTER COLUMN [ImageCaptureDateTime] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180726192804_RemovingRequired')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DiagnosticReports]') AND [c].[name] = N'Gender');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [DiagnosticReports] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [DiagnosticReports] ALTER COLUMN [Gender] nchar(1) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180726192804_RemovingRequired')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DiagnosticReports]') AND [c].[name] = N'DOB');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [DiagnosticReports] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [DiagnosticReports] ALTER COLUMN [DOB] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180726192804_RemovingRequired')
BEGIN
    ALTER TABLE [DiagnosticReports] ADD [PatientCode] nvarchar(50) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180726192804_RemovingRequired')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180726192804_RemovingRequired', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017195847_NewRecommendationPlanTable')
BEGIN
    update DiagnosticReports set PatientName='', Location = '', DRS = '', Gender = ''
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017195847_NewRecommendationPlanTable')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DiagnosticReports]') AND [c].[name] = N'PatientName');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [DiagnosticReports] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [DiagnosticReports] ALTER COLUMN [PatientName] nvarchar(150) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017195847_NewRecommendationPlanTable')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DiagnosticReports]') AND [c].[name] = N'ManagementPlan');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [DiagnosticReports] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [DiagnosticReports] ALTER COLUMN [ManagementPlan] nvarchar(10) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017195847_NewRecommendationPlanTable')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DiagnosticReports]') AND [c].[name] = N'Location');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [DiagnosticReports] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [DiagnosticReports] ALTER COLUMN [Location] nvarchar(75) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017195847_NewRecommendationPlanTable')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DiagnosticReports]') AND [c].[name] = N'Gender');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [DiagnosticReports] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [DiagnosticReports] ALTER COLUMN [Gender] nchar(1) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017195847_NewRecommendationPlanTable')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DiagnosticReports]') AND [c].[name] = N'DRS');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [DiagnosticReports] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [DiagnosticReports] ALTER COLUMN [DRS] nvarchar(50) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017195847_NewRecommendationPlanTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181017195847_NewRecommendationPlanTable', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017201417_NewRecommendationPlanTableV2')
BEGIN
    CREATE TABLE [RecommendationPlans] (
        [ID] int NOT NULL IDENTITY,
        [CodeType] nvarchar(100) NOT NULL,
        [Description] nvarchar(400) NOT NULL,
        CONSTRAINT [PK_RecommendationPlans] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017201417_NewRecommendationPlanTableV2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181017201417_NewRecommendationPlanTableV2', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017203011_AddColsAroundReferralInfo')
BEGIN
    ALTER TABLE [DiagnosticReports] ADD [ManagementPlanID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017203011_AddColsAroundReferralInfo')
BEGIN
    ALTER TABLE [DiagnosticReports] ADD [ReferralEntityID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017203011_AddColsAroundReferralInfo')
BEGIN
    ALTER TABLE [DiagnosticReports] ADD [ReferralTimeframeID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017203011_AddColsAroundReferralInfo')
BEGIN
    CREATE INDEX [IX_DiagnosticReports_ManagementPlanID] ON [DiagnosticReports] ([ManagementPlanID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017203011_AddColsAroundReferralInfo')
BEGIN
    CREATE INDEX [IX_DiagnosticReports_ReferralEntityID] ON [DiagnosticReports] ([ReferralEntityID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017203011_AddColsAroundReferralInfo')
BEGIN
    CREATE INDEX [IX_DiagnosticReports_ReferralTimeframeID] ON [DiagnosticReports] ([ReferralTimeframeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017203011_AddColsAroundReferralInfo')
BEGIN
    ALTER TABLE [DiagnosticReports] ADD CONSTRAINT [FK_DiagnosticReports_RecommendationPlans_ManagementPlanID] FOREIGN KEY ([ManagementPlanID]) REFERENCES [RecommendationPlans] ([ID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017203011_AddColsAroundReferralInfo')
BEGIN
    ALTER TABLE [DiagnosticReports] ADD CONSTRAINT [FK_DiagnosticReports_RecommendationPlans_ReferralEntityID] FOREIGN KEY ([ReferralEntityID]) REFERENCES [RecommendationPlans] ([ID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017203011_AddColsAroundReferralInfo')
BEGIN
    ALTER TABLE [DiagnosticReports] ADD CONSTRAINT [FK_DiagnosticReports_RecommendationPlans_ReferralTimeframeID] FOREIGN KEY ([ReferralTimeframeID]) REFERENCES [RecommendationPlans] ([ID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017203011_AddColsAroundReferralInfo')
BEGIN
    update DiagnosticReports set ManagementPlanID = 1, ReferralEntityID = 11, ReferralTimeframeID = 7
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017203011_AddColsAroundReferralInfo')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DiagnosticReports]') AND [c].[name] = N'ManagementPlan');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [DiagnosticReports] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [DiagnosticReports] DROP COLUMN [ManagementPlan];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181017203011_AddColsAroundReferralInfo')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181017203011_AddColsAroundReferralInfo', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181018183537_AddEyeOtherColumnsToReport')
BEGIN
    ALTER TABLE [DiagnosticReports] ADD [LeftEyeOther] nvarchar(200) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181018183537_AddEyeOtherColumnsToReport')
BEGIN
    ALTER TABLE [DiagnosticReports] ADD [RightEyeOther] nvarchar(200) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181018183537_AddEyeOtherColumnsToReport')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181018183537_AddEyeOtherColumnsToReport', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181105162309_AddClinicSitesTable')
BEGIN
    DROP INDEX [IX_DiagnosticReports_ReferralTimeframeID] ON [DiagnosticReports];
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DiagnosticReports]') AND [c].[name] = N'ReferralTimeframeID');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [DiagnosticReports] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [DiagnosticReports] ALTER COLUMN [ReferralTimeframeID] int NOT NULL;
    CREATE INDEX [IX_DiagnosticReports_ReferralTimeframeID] ON [DiagnosticReports] ([ReferralTimeframeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181105162309_AddClinicSitesTable')
BEGIN
    DROP INDEX [IX_DiagnosticReports_ReferralEntityID] ON [DiagnosticReports];
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DiagnosticReports]') AND [c].[name] = N'ReferralEntityID');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [DiagnosticReports] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [DiagnosticReports] ALTER COLUMN [ReferralEntityID] int NOT NULL;
    CREATE INDEX [IX_DiagnosticReports_ReferralEntityID] ON [DiagnosticReports] ([ReferralEntityID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181105162309_AddClinicSitesTable')
BEGIN
    DROP INDEX [IX_DiagnosticReports_ManagementPlanID] ON [DiagnosticReports];
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DiagnosticReports]') AND [c].[name] = N'ManagementPlanID');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [DiagnosticReports] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [DiagnosticReports] ALTER COLUMN [ManagementPlanID] int NOT NULL;
    CREATE INDEX [IX_DiagnosticReports_ManagementPlanID] ON [DiagnosticReports] ([ManagementPlanID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181105162309_AddClinicSitesTable')
BEGIN
    CREATE TABLE [ClinicSites] (
        [ID] int NOT NULL IDENTITY,
        [CameraID] int NOT NULL,
        [CameraCode] nvarchar(50) NOT NULL,
        [LocationName] nvarchar(50) NOT NULL,
        [Email] nvarchar(50) NOT NULL,
        [Fax] int NOT NULL,
        CONSTRAINT [PK_ClinicSites] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181105162309_AddClinicSitesTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181105162309_AddClinicSitesTable', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181107152449_AddReportFaxTable')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ClinicSites]') AND [c].[name] = N'Fax');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [ClinicSites] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [ClinicSites] ALTER COLUMN [Fax] nvarchar(10) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181107152449_AddReportFaxTable')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ClinicSites]') AND [c].[name] = N'CameraCode');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [ClinicSites] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [ClinicSites] ALTER COLUMN [CameraCode] nvarchar(50) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181107152449_AddReportFaxTable')
BEGIN
    CREATE TABLE [FaxAttempts] (
        [ID] int NOT NULL IDENTITY,
        [DiagnosticReportID] int NOT NULL,
        [InterfaxID] bigint NOT NULL,
        [PagesSent] int NOT NULL,
        [AttemptsMade] int NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_FaxAttempts] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181107152449_AddReportFaxTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181107152449_AddReportFaxTable', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181107185502_AddCreateDateToFaxTable')
BEGIN
    ALTER TABLE [FaxAttempts] ADD [CreateDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181107185502_AddCreateDateToFaxTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181107185502_AddCreateDateToFaxTable', N'2.1.4-rtm-31024');
END;

GO

