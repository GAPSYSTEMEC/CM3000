-- CREATE TABLE FACTURAS_ANULADAS(
-- FA_CODIGO BIGINT IDENTITY(1,1) NOT NULL,
-- ATE_CODIGO BIGINT,
-- FA_ESTADO NVARCHAR(50),
-- FA_ANTERIOR NVARCHAR(20),
-- FA_NUEVA NVARCHAR(20),
-- FA_NOTACREDITO NVARCHAR(20),
-- ID_USUARIO INT,
-- FA_OBSERVACION NVARCHAR(1000),
-- FA_FECHA DATETIME DEFAULT GETDATE())

-- CREATE PROCEDURE sp_HM_FacturaNueva
-- @ate_codigo bigint,
-- @estado nvarchar(50),
-- @anterior nvarchar(20),
-- @nueva nvarchar(20),
-- @credito nvarchar(20),
-- @usuario int,
-- @observacion nvarchar(1000)
-- AS
-- INSERT INTO 
-- FACTURAS_ANULADAS(ATE_CODIGO, FA_ESTADO, FA_ANTERIOR, FA_NUEVA, FA_NOTACREDITO, ID_USUARIO, FA_OBSERVACION)
-- VALUES (@ate_codigo, @estado, @anterior, @nueva, @credito, @usuario, @observacion)
-- GO


-- CREATE PROCEDURE sp_FacturaAnuladas
-- AS
-- SELECT FA.FA_FECHA AS 'FECHA',
-- P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE,
-- P.PAC_HISTORIA_CLINICA AS HC, A.ATE_NUMERO_ATENCION AS 'N. ATENCION', FA.FA_ESTADO AS ESTADO,
-- SA.numdoc AS 'F. ANTERIOR', FA.FA_NUEVA AS 'F. NUEVA', FA.FA_NOTACREDITO AS 'N. CREDITO',
-- U.APELLIDOS + ' ' + U.NOMBRES AS 'CAMBIADO POR', FA.FA_OBSERVACION AS 'OBSERVACION'
-- FROM FACTURAS_ANULADAS FA
-- INNER JOIN ATENCIONES A ON FA.ATE_CODIGO = A.ATE_CODIGO
-- INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO
-- INNER JOIN USUARIOS U ON FA.ID_USUARIO = U.ID_USUARIO
-- GO


-- ALTER PROCEDURE [dbo].[sp_HM_EstadoFactura]
-- @numfac nvarchar(20)
-- AS
-- SELECT TOP 1 FA_ESTADO, FA_ANTERIOR, FA_NOTACREDITO FROM FACTURAS_ANULADAS WHERE FA_NUEVA = @numfac ORDER BY FA_CODIGO DESC
-- GO




-- CREATE PROCEDURE sp_HM_ValidarFacturaActiva
-- @numfac nvarchar(20)
-- AS
-- SELECT ATE_CODIGO FROM Sic3000..Nota N
-- INNER JOIN Sic3000..FacturaDatosAdicionales FDA ON N.numfac = numfac 
-- WHERE N.numfac = @numfac
-- GO



 -- ALTER PROCEDURE sp_PacuentesFac_Anuladas
 -- AS
 -- SELECT SA.fecha AS 'FECHA',
  -- P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE,
  -- P.PAC_HISTORIA_CLINICA AS HC, A.ATE_NUMERO_ATENCION AS 'N. ATENCION', --FA.FA_ESTADO AS ESTADO,
  -- SA.numdoc AS 'F. ANTERIOR', A.ATE_CODIGO as ATENCION, --FA.FA_NUEVA AS 'F. NUEVA', FA.FA_NOTACREDITO AS 'N. CREDITO',
 -- -- U.APELLIDOS + ' ' + U.NOMBRES AS 'CAMBIADO POR', 
 -- SA.obs AS 'OBSERVACION'
 -- FROM Sic3000..Auditora SA
 -- INNER JOIN Sic3000..FacturaDatosAdicionales FDA ON SA.numdoc = FDA.Numdoc
 -- INNER JOIN ATENCIONES A ON FDA.ATE_CODIGO = A.ATE_CODIGO
 -- INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO
 -- INNER JOIN USUARIOS U ON SA.codusu = U.ID_USUARIO
 -- GO



 -- create PROCEDURE sp_HM_Pacientes
 -- @nombre nvarchar(50)
 -- AS
 -- SELECT SA.fecha AS 'FECHA',
  -- P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE,
  -- P.PAC_HISTORIA_CLINICA AS HC, A.ATE_NUMERO_ATENCION AS 'N. ATENCION', --FA.FA_ESTADO AS ESTADO,
  -- SA.numdoc AS 'F. ANTERIOR', A.ATE_CODIGO as ATENCION, --FA.FA_NUEVA AS 'F. NUEVA', FA.FA_NOTACREDITO AS 'N. CREDITO',
 -- -- U.APELLIDOS + ' ' + U.NOMBRES AS 'CAMBIADO POR', 
 -- SA.obs AS 'OBSERVACION'
 -- FROM Sic3000..Auditora SA
 -- INNER JOIN Sic3000..FacturaDatosAdicionales FDA ON SA.numdoc = FDA.Numdoc
 -- INNER JOIN ATENCIONES A ON FDA.ATE_CODIGO = A.ATE_CODIGO
 -- INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO
 -- WHERE  P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 LIKE '%'+ @nombre +'%'
 -- GO


-- USE [Sic3000]
-- GO

-- /****** Object:  View [dbo].[HonorariosMedico]    Script Date: 28/09/2021 10:07:06 ******/
-- SET ANSI_NULLS ON
-- GO

-- SET QUOTED_IDENTIFIER ON
-- GO

-- ALTER VIEW [dbo].[HonorariosMedico]
-- AS
-- SELECT HM.HOM_CODIGO AS CODIGO, A.ATE_CODIGO, P.PAC_HISTORIA_CLINICA, 
-- P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE,
-- M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO
-- FROM His3000..HONORARIOS_MEDICOS HM
-- INNER JOIN His3000..ATENCIONES A ON HM.ATE_CODIGO = A.ATE_CODIGO
-- INNER JOIN His3000..PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO
-- INNER JOIN His3000..MEDICOS M ON HM.MED_CODIGO = M.MED_CODIGO 
-- GO


-- CREATE PROCEDURE sp_PacienteFacturasAnuladas  
-- @numfac nvarchar(20)  
-- AS  
-- SELECT * FROM Sic3000..Auditora WHERE numdoc = @numfac  
-----------------------------------------------------------------------------------------
-- CREATE PROCEDURE sp_HM_FacturaAnuladaRepetida
-- @numfac nvarchar(20)
-- AS
-- SELECT * FROM FACTURAS_ANULADAS WHERE FA_ANTERIOR = @numfac
-- GO