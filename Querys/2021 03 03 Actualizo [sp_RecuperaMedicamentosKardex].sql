USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_RecuperaMedicamentosKardex]    Script Date: 03/03/2021 12:32:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_RecuperaMedicamentosKardex]
(
	
	@ate_codigo VARCHAR(10),
	@rubro int,
	@check int
)
AS
BEGIN
	
	IF(@check=0)
	BEGIN
		SET @ate_codigo=(SELECT ATE_CODIGO FROM ATENCIONES WHERE ATE_NUMERO_ATENCION=@ate_codigo)
		select CUE_CODIGO AS CODIGO, CUE_DETALLE AS PRODUCTO, cast(CUE_CANTIDAD as int) AS CANTIDAD
		from CUENTAS_PACIENTES 
		where ATE_CODIGO=@ate_codigo AND RUB_CODIGO=@rubro AND 
				CUE_CODIGO NOT IN (SELECT CueCodigo FROM KARDEXMEDICAMENTOS WHERE AteCodigo=@ate_codigo) AND 
				CUE_CODIGO NOT IN (SELECT CueCodigo FROM KARDEXINSUMOS WHERE AteCodigo=@ate_codigo)
	END
	ELSE
	BEGIN
					SELECT P.codpro AS CODIGO, P.despro AS PRODUCTO,1 AS CANTIDAD FROM Sic3000.dbo.Producto P 
				INNER JOIN Sic3000.dbo.ProductoSubdivision ON P.codsub = Sic3000.dbo.ProductoSubdivision.codsub AND P.coddiv = Sic3000.dbo.ProductoSubdivision.coddiv 
				INNER JOIN dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO WHERE dbo.RUBROS.RUB_CODIGO=@rubro


	END
END

USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_IngresaDesactivacionAtencion]    Script Date: 04/03/2021 15:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_IngresaDesactivacionAtencion]
	(
		@atencion bigint,
		@motivo varchar(1000)
	)
AS BEGIN
	declare @ATE_CODIGO bigint
	set @ATE_CODIGO = (select ATE_CODIGO from ATENCIONES where ATE_NUMERO_ATENCION=@atencion)
	INSERT INTO STATUS_ATENCION VALUES
	(
		1,
		@motivo,
		@ATE_CODIGO
	)

	UPDATE ATENCIONES SET ESC_CODIGO=13 WHERE ATE_CODIGO=@ATE_CODIGO

END