---queda actualizado en la pasteur y la alianza
USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_ValidaEstatusAtencion]    Script Date: 24/03/2021 08:44:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_ValidaEstatusAtencion]
	@atencion bigint

AS BEGIN 
	declare @ATE_CODIGO bigint
	set @ATE_CODIGO = (select ATE_CODIGO from ATENCIONES where ATE_NUMERO_ATENCION=@atencion)
	
	--SELECT ate_codigo, Status FROM STATUS_ATENCION where Ate_Codigo=@ATE_CODIGO
	--union
	SELECT  ATE_CODIGO, sum(CUE_CANTIDAD) FROM CUENTAS_PACIENTES where Ate_Codigo=@ATE_CODIGO
	group by CUE_CANTIDAD, ATE_CODIGO
END
--fin


--solo es para la pasteur
USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_RecuperaMedicosEvolucion]    Script Date: 25/03/2021 10:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_RecuperaMedicosEvolucion]
(@EVO_CODIGO AS INT)
AS
BEGIN

	select ed.nom_usuario MÉDICO, ed.evd_fecha 'FECHA EVOLUCIÓN' from HC_EVOLUCION_DETALLE ed
where ed.evo_codigo=@EVO_CODIGO

END

---fin


---Solo falta en la alianza
create procedure sp_CertificadosMedicos
@fechainicio datetime,
@fechafin datetime
AS
select CM.CER_CODIGO AS 'NRO CERTIFICADO', CM.CER_FECHA AS 'FECHA CERTIFICADO',  A.ATE_NUMERO_ATENCION AS 'NRO ATENCION', 
P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE,
M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,
CER_DIAS_REPOSO AS 'DIAS REPOSO', CER_OBSERVACION AS OBSERVACION
from CERTIFICADO_MEDICO CM
--INNER JOIN CERTIFICADO_MEDICO_DETALLE CMD ON CM.CER_CODIGO = CMD.CER_CODIGO
INNER JOIN MEDICOS M ON CM.MED_CODIGO = M.MED_CODIGO
INNER JOIN ATENCIONES A ON CM.ATE_CODIGO = A.ATE_CODIGO
INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO
where CM.CER_FECHA BETWEEN @fechainicio AND @fechafin
GO
---fin




