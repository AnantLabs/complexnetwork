GO
/****** Object:  Trigger [dbo].[FillInformation]    Script Date: 04/12/2013 10:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Ani Kocharyan>
-- Create date: <06.04.13>
-- Description:	<Filling clear information for Statistic Analyzer>
-- =============================================
CREATE TRIGGER [dbo].[FillInformation] 
   ON  [dbo].[Assemblies]
   FOR UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	DECLARE @paramID uniqueidentifier;
	DECLARE @paramSize int;

	SELECT @paramID = AssemblyID, @paramSize = NetworkSize FROM DELETED;

	DECLARE @check int;

	-- Fill CoefficientsGlobal and CoefficientsLocal tables
	SELECT @check = COUNT(*) FROM Coefficients 
	WHERE ResultsID in (SELECT ResultsID FROM AssemblyResults WHERE AssemblyID = @paramID)
	IF @check > 0
	BEGIN
		EXEC CountAllGlobalCoefficients @paramID, @paramSize;
		EXEC CountAllLocalCoefficients @paramID, @paramSize;
	END

	-- Fill VertexDegreeGlobal and VertexDegreeLocal tables
	SELECT @check = COUNT(*) FROM VertexDegree 
	WHERE ResultsID in (SELECT ResultsID FROM AssemblyResults WHERE AssemblyID = @paramID)
	IF @check > 0
	BEGIN
		EXEC CountAllGlobalVertexDegrees @paramID, @paramSize;
		EXEC CountAllLocalVertexDegrees @paramID, @paramSize;
	END

	-- Fill ConSubgraphsLocal table
	SELECT @check = COUNT(*) FROM ConSubgraphs 
	WHERE ResultsID in (SELECT ResultsID FROM AssemblyResults WHERE AssemblyID = @paramID)
	IF @check > 0
	BEGIN
		EXEC CountAllLocalConSubgraphs @paramID, @paramSize;
	END

	-- Fill VertexDistanceLocal table
	SELECT @check = COUNT(*) FROM VertexDistance 
	WHERE ResultsID in (SELECT ResultsID FROM AssemblyResults WHERE AssemblyID = @paramID)
	IF @check > 0
	BEGIN
		EXEC CountAllLocalVertexDistances @paramID, @paramSize;
	END

	-- Fill EigenValuesDistanceLocal table
	SELECT @check = COUNT(*) FROM EigenValuesDistance 
	WHERE ResultsID in (SELECT ResultsID FROM AssemblyResults WHERE AssemblyID = @paramID)
	IF @check > 0
	BEGIN
		EXEC CountAllLocalEigenValuesDistances @paramID;
	END

	-- Fill TriangleTrajectoryLocal table
	SELECT @check = COUNT(*) FROM TriangleTrajectory
	WHERE ResultsID in (SELECT ResultsID FROM AssemblyResults WHERE AssemblyID = @paramID)
	IF @check > 0
	BEGIN
		EXEC CountAllLocalTriangleTrajectories @paramID;
	END
END
