/****** ComplexNetwork SQL Setup Script ******/

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[GraphModels]    Script Date: 02/21/2013 11:33:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GraphModels](
	[GraphModelID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_GraphModels] PRIMARY KEY CLUSTERED 
(
	[GraphModelID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[GenerationParams]    Script Date: 02/21/2013 11:33:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GenerationParams](
	[GenerationParamID] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Type] [nvarchar](100) NULL,
 CONSTRAINT [PK_GenerationParams] PRIMARY KEY CLUSTERED 
(
	[GenerationParamID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[AnalyzeOptions]    Script Date: 02/21/2013 11:31:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnalyzeOptions](
	[AnalyzeOptionID] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_AnalyzeOptions] PRIMARY KEY CLUSTERED 
(
	[AnalyzeOptionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[AnalyzeOptionParams]    Script Date: 03/28/2013 11:22:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnalyzeOptionParams](
	[AnalyzeOptionParamID] [int] NOT NULL,
	[AnalyzeOptionID] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_AnalyzeOptionParams] PRIMARY KEY CLUSTERED 
(
	[AnalyzeOptionParamID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AnalyzeOptionParams]  WITH CHECK ADD  CONSTRAINT [FK_AnalyzeOptionParams_AnalyzeOptions] FOREIGN KEY([AnalyzeOptionID])
REFERENCES [dbo].[AnalyzeOptions] ([AnalyzeOptionID])
GO
ALTER TABLE [dbo].[AnalyzeOptionParams] CHECK CONSTRAINT [FK_AnalyzeOptionParams_AnalyzeOptions]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[Assemblies]    Script Date: 02/21/2013 11:31:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assemblies](
	[AssemblyID] [uniqueidentifier] NOT NULL,
	[ModelID] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Date] [datetime] NOT NULL,
	[NetworkSize] [int] NOT NULL,
	[FileName] [nvarchar](250) NOT NULL CONSTRAINT [DF_Assemblies_FileName]  DEFAULT (N'none'),
 CONSTRAINT [PK_Assemblies] PRIMARY KEY CLUSTERED 
(
	[AssemblyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Assemblies]  WITH CHECK ADD  CONSTRAINT [FK_Assemblies_GraphModel] FOREIGN KEY([ModelID])
REFERENCES [dbo].[GraphModels] ([GraphModelID])
GO
ALTER TABLE [dbo].[Assemblies] CHECK CONSTRAINT [FK_Assemblies_GraphModel]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[GenerationParamValues]    Script Date: 02/21/2013 11:33:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GenerationParamValues](
	[AssemblyID] [uniqueidentifier] NOT NULL,
	[GenerationParamID] [int] NOT NULL,
	[Value] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_GenerationParamValues] PRIMARY KEY CLUSTERED 
(
	[AssemblyID] ASC,
	[GenerationParamID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GenerationParamValues]  WITH CHECK ADD  CONSTRAINT [FK_GenerationParamValues_Assemblies] FOREIGN KEY([AssemblyID])
REFERENCES [dbo].[Assemblies] ([AssemblyID])
GO
ALTER TABLE [dbo].[GenerationParamValues] CHECK CONSTRAINT [FK_GenerationParamValues_Assemblies]
GO
ALTER TABLE [dbo].[GenerationParamValues]  WITH CHECK ADD  CONSTRAINT [FK_GenerationParamValues_GenerationParams] FOREIGN KEY([GenerationParamID])
REFERENCES [dbo].[GenerationParams] ([GenerationParamID])
GO
ALTER TABLE [dbo].[GenerationParamValues] CHECK CONSTRAINT [FK_GenerationParamValues_GenerationParams]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[AnalyzeOptionParamValues]    Script Date: 02/21/2013 11:34:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnalyzeOptionParamValues](
	[AssemblyID] [uniqueidentifier] NOT NULL,
	[AnalyzeOptionParamID] [int] NOT NULL,
	[Value] [nvarchar](50) NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AnalyzeOptionParamValues]  WITH CHECK ADD  CONSTRAINT [FK_AnalyzeOptionParamValues_AnalyzeOptionParams] FOREIGN KEY([AnalyzeOptionParamID])
REFERENCES [dbo].[AnalyzeOptionParams] ([AnalyzeOptionParamID])
GO
ALTER TABLE [dbo].[AnalyzeOptionParamValues] CHECK CONSTRAINT [FK_AnalyzeOptionParamValues_AnalyzeOptionParams]
GO
ALTER TABLE [dbo].[AnalyzeOptionParamValues]  WITH CHECK ADD  CONSTRAINT [FK_AnalyzeOptionParamValues_Assemblies] FOREIGN KEY([AssemblyID])
REFERENCES [dbo].[Assemblies] ([AssemblyID])
GO
ALTER TABLE [dbo].[AnalyzeOptionParamValues] CHECK CONSTRAINT [FK_AnalyzeOptionParamValues_Assemblies]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[AssemblyResults]    Script Date: 02/21/2013 11:31:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssemblyResults](
	[ResultsID] [int] IDENTITY(1,1) NOT NULL,
	[AssemblyID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AssemblyResults_1] PRIMARY KEY CLUSTERED 
(
	[ResultsID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AssemblyResults]  WITH CHECK ADD  CONSTRAINT [FK_AssemblyResults_Assemblies] FOREIGN KEY([AssemblyID])
REFERENCES [dbo].[Assemblies] ([AssemblyID])
GO
ALTER TABLE [dbo].[AssemblyResults] CHECK CONSTRAINT [FK_AssemblyResults_Assemblies]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[AnalyzeResults]    Script Date: 02/21/2013 11:31:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnalyzeResults](
	[ResultsID] [int] NOT NULL,
	[AnalyzeOptionID] [int] NOT NULL,
	[Result] [float] NOT NULL,
 CONSTRAINT [PK_AnalyzeResults] PRIMARY KEY CLUSTERED 
(
	[ResultsID] ASC,
	[AnalyzeOptionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AnalyzeResults]  WITH CHECK ADD  CONSTRAINT [FK_AnalyzeResults_AnalyzeOptions] FOREIGN KEY([AnalyzeOptionID])
REFERENCES [dbo].[AnalyzeOptions] ([AnalyzeOptionID])
GO
ALTER TABLE [dbo].[AnalyzeResults] CHECK CONSTRAINT [FK_AnalyzeResults_AnalyzeOptions]
GO
ALTER TABLE [dbo].[AnalyzeResults]  WITH CHECK ADD  CONSTRAINT [FK_AnalyzeResults_AssemblyResults] FOREIGN KEY([ResultsID])
REFERENCES [dbo].[AssemblyResults] ([ResultsID])
GO
ALTER TABLE [dbo].[AnalyzeResults] CHECK CONSTRAINT [FK_AnalyzeResults_AssemblyResults]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[Coefficients]    Script Date: 02/21/2013 11:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coefficients](
	[ResultsID] [int] NOT NULL,
	[Coefficient] [float] NOT NULL,
	[Count] [int] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Coefficients]  WITH CHECK ADD  CONSTRAINT [FK_Coefficients_AssemblyResults] FOREIGN KEY([ResultsID])
REFERENCES [dbo].[AssemblyResults] ([ResultsID])
GO
ALTER TABLE [dbo].[Coefficients] CHECK CONSTRAINT [FK_Coefficients_AssemblyResults]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[ConSubgraphs]    Script Date: 02/21/2013 11:32:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConSubgraphs](
	[ResultsID] [int] NOT NULL,
	[VX] [int] NOT NULL,
	[Count] [int] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ConSubgraphs]  WITH CHECK ADD  CONSTRAINT [FK_ConSubgraphs_AssemblyResults] FOREIGN KEY([ResultsID])
REFERENCES [dbo].[AssemblyResults] ([ResultsID])
GO
ALTER TABLE [dbo].[ConSubgraphs] CHECK CONSTRAINT [FK_ConSubgraphs_AssemblyResults]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[VertexDegree]    Script Date: 02/21/2013 11:34:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VertexDegree](
	[ResultsID] [int] NOT NULL,
	[Degree] [int] NOT NULL,
	[Count] [int] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[VertexDegree]  WITH CHECK ADD  CONSTRAINT [FK_VertexDegree_AssemblyResults] FOREIGN KEY([ResultsID])
REFERENCES [dbo].[AssemblyResults] ([ResultsID])
GO
ALTER TABLE [dbo].[VertexDegree] CHECK CONSTRAINT [FK_VertexDegree_AssemblyResults]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[FullSubgraphs]    Script Date: 02/21/2013 11:32:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FullSubgraphs](
	[ResultsID] [int] NOT NULL,
	[VX] [int] NOT NULL,
	[Count] [int] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[FullSubgraphs]  WITH CHECK ADD  CONSTRAINT [FK_FullSubgraphs_AssemblyResults] FOREIGN KEY([ResultsID])
REFERENCES [dbo].[AssemblyResults] ([ResultsID])
GO
ALTER TABLE [dbo].[FullSubgraphs] CHECK CONSTRAINT [FK_FullSubgraphs_AssemblyResults]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[EigenValues]    Script Date: 02/21/2013 11:32:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EigenValues](
	[ResultsID] [int] NOT NULL,
	[EigenValue] [float] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[EigenValues]  WITH CHECK ADD  CONSTRAINT [FK_EigenValues_AssemblyResults] FOREIGN KEY([ResultsID])
REFERENCES [dbo].[AssemblyResults] ([ResultsID])
GO
ALTER TABLE [dbo].[EigenValues] CHECK CONSTRAINT [FK_EigenValues_AssemblyResults]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[EigenValuesDistance]    Script Date: 02/21/2013 11:32:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EigenValuesDistance](
	[ResultsID] [int] NOT NULL,
	[Distance] [float] NOT NULL,
	[Count] [int] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[EigenValuesDistance]  WITH CHECK ADD  CONSTRAINT [FK_EigenValuesDistance_AssemblyResults] FOREIGN KEY([ResultsID])
REFERENCES [dbo].[AssemblyResults] ([ResultsID])
GO
ALTER TABLE [dbo].[EigenValuesDistance] CHECK CONSTRAINT [FK_EigenValuesDistance_AssemblyResults]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[Cycles]    Script Date: 02/21/2013 11:32:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cycles](
	[ResultsID] [int] NOT NULL,
	[Order] [int] NOT NULL,
	[Count] [bigint] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Cycles]  WITH CHECK ADD  CONSTRAINT [FK_Cycles_AssemblyResults] FOREIGN KEY([ResultsID])
REFERENCES [dbo].[AssemblyResults] ([ResultsID])
GO
ALTER TABLE [dbo].[Cycles] CHECK CONSTRAINT [FK_Cycles_AssemblyResults]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[Motifs]    Script Date: 02/21/2013 11:33:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motifs](
	[ResultsID] [int] NOT NULL,
	[ID] [int] NOT NULL,
	[Count] [float] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Motifs]  WITH CHECK ADD  CONSTRAINT [FK_Motifs_AssemblyResults] FOREIGN KEY([ResultsID])
REFERENCES [dbo].[AssemblyResults] ([ResultsID])
GO
ALTER TABLE [dbo].[Motifs] CHECK CONSTRAINT [FK_Motifs_AssemblyResults]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[Triangles]    Script Date: 02/21/2013 11:33:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Triangles](
	[ResultsID] [int] NOT NULL,
	[TriangleCount] [int] NOT NULL,
	[Count] [int] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Triangles]  WITH CHECK ADD  CONSTRAINT [FK_Triangles_AssemblyResults] FOREIGN KEY([ResultsID])
REFERENCES [dbo].[AssemblyResults] ([ResultsID])
GO
ALTER TABLE [dbo].[Triangles] CHECK CONSTRAINT [FK_Triangles_AssemblyResults]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[TriangleTrajectory]    Script Date: 02/21/2013 11:33:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TriangleTrajectory](
	[ResultsID] [int] NOT NULL,
	[Time] [int] NOT NULL,
	[TriangleCount] [float] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TriangleTrajectory]  WITH CHECK ADD  CONSTRAINT [FK_TriangleTrajectory_AssemblyResults] FOREIGN KEY([ResultsID])
REFERENCES [dbo].[AssemblyResults] ([ResultsID])
GO
ALTER TABLE [dbo].[TriangleTrajectory] CHECK CONSTRAINT [FK_TriangleTrajectory_AssemblyResults]

USE [ComplexNetwork]
GO
/****** Object:  Table [dbo].[VertexDistance]    Script Date: 02/21/2013 11:34:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VertexDistance](
	[ResultsID] [int] NOT NULL,
	[Distance] [int] NOT NULL,
	[Count] [int] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[VertexDistance]  WITH CHECK ADD  CONSTRAINT [FK_VertexDistance_AssemblyResults] FOREIGN KEY([ResultsID])
REFERENCES [dbo].[AssemblyResults] ([ResultsID])
GO
ALTER TABLE [dbo].[VertexDistance] CHECK CONSTRAINT [FK_VertexDistance_AssemblyResults]

/***  Load initial Data ***/

INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (0 ,'None','Used for indicating empty selection')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (1 ,'Degree Distribution','Degree distribution')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (2 ,'Average Path Length','Avarage path length')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (4 ,'Clustering Coefficient','Clustering coefficient')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (8 ,'Eigen Values','Eigen values')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (16 ,'Cycles of Order 3','Cycles with 3 length')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (32 ,'Diameter','Diameter of graph')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (64 ,'Connected Subgraphs by Order','Propability of k-degree connected sub graph existance')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (128 ,'Cycles 3 by Eigen Values','Cycles 3 by eigen values')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (256 ,'Cycles of Order 4','Cycles with 4 length')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (512 ,'Cycles 4 by Eigen Values','Cycles 4 by eigen values')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (1024 ,'Motifs','The count of motifs')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (2048 ,'Distance between Vertices','Minpath distribution')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (4096 ,'Distance between Eigen Values','Eigen distribution')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (8192 ,'Full Subgraphs by Order','Propability of k-degree connected sub graph existance')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (16384 ,'Order of Maximal Full Subgraph','Order of maximal full subgraph')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (32768 ,'Largest Connected Component','Maximal size of connected subgraph')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (65536 ,'Minimal Eigen Value','Minimal eigen value')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (131072 ,'Maximal Eigen Value','Maximal eigen value')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (262144 ,'Cycles','Cycles of any degree')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (524288 ,'Triangle Count by Vertex','Triangle Count')
INSERT INTO AnalyzeOptions(AnalyzeOptionID,[Name],[Description]) VALUES (1048576 ,'Triangle Trajectory','Triangle trajectory')

INSERT INTO GraphModels([Name],[Description]) VALUES ('Block-Hierarchic','Block-hierarchic Model')
INSERT INTO GraphModels([Name],[Description]) VALUES ('BAModel','Barabasi-Albert Model')
INSERT INTO GraphModels([Name],[Description]) VALUES ('Block-Hierarchic Parisi','Block-hierarchic Parisi Model')
INSERT INTO GraphModels([Name],[Description]) VALUES ('WSModel','Watts-Strogatz Model')
INSERT INTO GraphModels([Name],[Description]) VALUES ('ERModel','Erdos-Renyi Model')
INSERT INTO GraphModels([Name],[Description]) VALUES ('Block-Hierarchic Non Regular','Block-Hierarchic Non Regular Model')

INSERT INTO GenerationParams(GenerationParamID,[Name],[Type]) VALUES(1,'Vertices','Int32')
INSERT INTO GenerationParams(GenerationParamID,[Name],[Type]) VALUES(2,'Edges','Int32')
INSERT INTO GenerationParams(GenerationParamID,[Name],[Type]) VALUES(3,'BranchIndex','Int16')
INSERT INTO GenerationParams(GenerationParamID,[Name],[Type]) VALUES(4,'Level','Int16')
INSERT INTO GenerationParams(GenerationParamID,[Name],[Type]) VALUES(5,'MaxEdges ','Int16')
INSERT INTO GenerationParams(GenerationParamID,[Name],[Type]) VALUES(6,'Mu','Double')
INSERT INTO GenerationParams(GenerationParamID,[Name],[Type]) VALUES(7,'P','Double')
INSERT INTO GenerationParams(GenerationParamID,[Name],[Type]) VALUES(8,'StepCount','Int32')
INSERT INTO GenerationParams(GenerationParamID,[Name],[Type]) VALUES(9,'InitialStep','String')
INSERT INTO GenerationParams(GenerationParamID,[Name],[Type]) VALUES(10,'InitialProbability','Double')
INSERT INTO GenerationParams(GenerationParamID,[Name],[Type]) VALUES(11,'Permanent','Boolean')

INSERT INTO AnalyzeOptionParams(AnalyzeOptionParamID,AnalyzeOptionID,[Name],[Type]) VALUES(1,262144,'CyclesLow','Int16')
INSERT INTO AnalyzeOptionParams(AnalyzeOptionParamID,AnalyzeOptionID,[Name],[Type]) VALUES(2,262144,'CyclesHigh','Int16')
INSERT INTO AnalyzeOptionParams(AnalyzeOptionParamID,AnalyzeOptionID,[Name],[Type]) VALUES(3,1024,'MotifsLow','Int16')
INSERT INTO AnalyzeOptionParams(AnalyzeOptionParamID,AnalyzeOptionID,[Name],[Type]) VALUES(4,1024,'MotifsHigh','Int16')
INSERT INTO AnalyzeOptionParams(AnalyzeOptionParamID,AnalyzeOptionID,[Name],[Type]) VALUES(5,1048576,'TrajectoryMu','Double')
INSERT INTO AnalyzeOptionParams(AnalyzeOptionParamID,AnalyzeOptionID,[Name],[Type]) VALUES(6,1048576,'TrajectoryStepCount','BigInteger')

