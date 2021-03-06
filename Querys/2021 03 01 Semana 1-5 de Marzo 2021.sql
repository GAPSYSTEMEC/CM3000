USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_ImpresionPedido]    Script Date: 01/03/2021 10:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[sp_ImpresionPedido]( @Pedido as int)      
as      
begin      
 select       
 PACIENTES.PAC_APELLIDO_PATERNO + ' ' + PACIENTES.PAC_APELLIDO_MATERNO + ' ' +PACIENTES.PAC_NOMBRE1 + ' ' +PACIENTES.PAC_NOMBRE2 AS PACIENTE,       
 PACIENTES.PAC_IDENTIFICACION AS IDENTIFICACION,      
 PEDIDOS.PED_FECHA,      
 PEDIDOS.PED_CODIGO AS [NUMERO PEDIDO],      
 pedidos.ID_USUARIO AS [CODIGO USUARIO],       
 USUARIOS.USR AS USUARIO ,      
 MEDICOS.MED_APELLIDO_PATERNO + ' ' + MEDICOS.MED_APELLIDO_MATERNO + ' ' + MEDICOS.MED_NOMBRE1 + ' ' + MEDICOS.MED_NOMBRE2 as MEDICOS,      
 PRO_CODIGO,      
 dbo.datosproducto(PRO_CODIGO) as Producto,      
 PDD_CANTIDAD,      
 PDD_VALOR,      
 PDD_IVA,      
 PDD_TOTAL,  
 PAC_HISTORIA_CLINICA as HISTORIA,  
 ATENCIONES.ATE_NUMERO_ATENCION AS ATENCION,  
 (SELECT HABITACIONES.hab_Numero FROM HABITACIONES WHERE HABITACIONES.hab_Codigo=ATENCIONES.HAB_CODIGO)  AS HABITACION,  
 (SELECT PEDIDOS_ESTACIONES.PEE_NOMBRE FROM PEDIDOS_ESTACIONES WHERE PEDIDOS_ESTACIONES.PEE_CODIGO=PEDIDOS.PEE_CODIGO) AS ESTACION  ,
 (SELECT TOP (1) dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE
			FROM  dbo.ATENCIONES aa INNER JOIN dbo.ATENCION_DETALLE_CATEGORIAS ON aa.ATE_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO INNER JOIN dbo.CATEGORIAS_CONVENIOS ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO
			WHERE aa.ATE_CODIGO = ATENCIONES.ATE_CODIGO) as ASEGURADORA, PEDIDOS.PED_DESCRIPCION AS OBSERVACION
 from pedidos,PEDIDOS_DETALLE,USUARIOS,MEDICOS,ATENCIONES,PACIENTES      
 where pedidos.ID_USUARIO=USUARIOS.ID_USUARIO      
 and MEDICOS.MED_CODIGO=PEDIDOS.MED_CODIGO      
 AND PEDIDOS.PED_CODIGO=PEDIDOS_DETALLE.PED_CODIGO      
 and PEDIDOS.PED_CODIGO=@Pedido      
 and pacientes.PAC_CODIGO=ATENCIONES.PAC_CODIGO      
 and ATENCIONES.ATE_CODIGO=pedidos.ATE_CODIGO
 AND PEDIDOS_DETALLE.PDD_CANTIDAD > 0
 order by PRO_CODIGO      
end




USE [His3000]
GO
/***** Object:  StoredProcedure [dbo].[sp_AltaProgramada]    Script Date: 01/03/2021 10:54:38 *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_AltaProgramada]
(
@ATE_CODIGO AS INT
)      
AS BEGIN  
    
UPDATE HABITACIONES SET HES_CODIGO=2, hab_fec_cambio_est=GETDATE()   
WHERE HAB_CODIGO=(SELECT HAB_CODIGO FROM ATENCIONES WHERE ATE_CODIGO=@ATE_CODIGO)

UPDATE HABITACIONES_DETALLE SET HAD_FECHA_DISPONIBILIDAD=GETDATE() 
WHERE HAB_CODIGO=(SELECT HAB_CODIGO FROM ATENCIONES WHERE ATE_CODIGO=@ATE_CODIGO) 

UPDATE ATENCIONES SET ESC_CODIGO=1, ATE_FECHA_ALTA=GETDATE(), ATE_ESTADO=1 WHERE ATE_CODIGO=@ATE_CODIGO

END






USE [His3000]
GO
/***** Object:  StoredProcedure [dbo].[sp_ValoresAutomaticosCuentas]    Script Date: 01/03/2021 10:28:36 *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER procedure [dbo].[sp_ValoresAutomaticosCuentas]  
/*GENERA LOS VALORES AUTOMATICOS DE LAS CUENTAS*/
(  
@p_CodigoAtencion as BigInt,  
@p_CodigoUsuario as int,
@p_Dias as int, 
@p_Habitacion as varchar(30),
@p_Convenio as int,
@p_Empresa as int
)  
  
as  
begin  
  
 declare @ValorServicioClinica as decimal(18,4)  
 declare @ValorAdministracionMed as decimal(18,4)  
 declare @ValorDerechoRecuperacion as decimal(18,4)  
 declare @ValorDerechoAnestecia as decimal(18,4)   
 declare @ValorHospitalizacionPaciente as decimal(18,4)  
 
 declare @PreciosHabitacion as decimal(18,4)  
 declare @DescripcionHabitacion as varchar(200)
 
 declare @ServicioClinica as int
 declare @AdministracionMed as int
 declare @DerechoRecuperacion as int
 declare @DerechoAnestecia as int
 declare @HospitalizacionPaciente as int
   
 declare @SecuencialCuentas as bigint  
 
 /*verifico si los valores automaticos ya han sido generados*/
 
 --select @ServicioClinica=COUNT(*) 
 --from CUENTAS_PACIENTES
 --where RUB_CODIGO=5 --HOSPITALIZACION PACIENTE
 --and ATE_CODIGO=@p_CodigoAtencion 
 
 --if @ServicioClinica>0
 --begin
 
	-- delete from CUENTAS_PACIENTES
	-- where RUB_CODIGO=25 --servicios de clinica
	-- and ATE_CODIGO=@p_CodigoAtencion 
 
 --end

 select @HospitalizacionPaciente=COUNT(*) 
 from CUENTAS_PACIENTES
 where RUB_CODIGO=5 -- hospitalizacion paciente
 and ATE_CODIGO=@p_CodigoAtencion 
 
 if @HospitalizacionPaciente>0
 begin
 
	 delete from CUENTAS_PACIENTES
	 where RUB_CODIGO=5 -- hospitalizacion paciente
	 and ATE_CODIGO=@p_CodigoAtencion 
 
 end
 
 select @AdministracionMed=COUNT(*) 
 from CUENTAS_PACIENTES
 where RUB_CODIGO=1 --AdministracionMed
 and ATE_CODIGO=@p_CodigoAtencion 
 
 if @AdministracionMed>0
 begin
 
	 delete from CUENTAS_PACIENTES
	 where RUB_CODIGO=1 --AdministracionMed
	 and ATE_CODIGO=@p_CodigoAtencion 
 
 end
 
 --select @DerechoRecuperacion=COUNT(*) 
 --from CUENTAS_PACIENTES
 --where RUB_CODIGO=24 --DerechoRecuperacion
 --and ATE_CODIGO=@p_CodigoAtencion 
 
 --if @DerechoRecuperacion>0
 --begin
 
	-- delete from CUENTAS_PACIENTES
	-- where RUB_CODIGO=24 --DerechoRecuperacion
	-- and ATE_CODIGO=@p_CodigoAtencion 
 
 --end
 
 --select @DerechoAnestecia=COUNT(*) 
 --from CUENTAS_PACIENTES
 --where RUB_CODIGO=32 --DerechoAnestecia
 --and ATE_CODIGO=@p_CodigoAtencion 
 
 --if @DerechoAnestecia>0
 --begin
 
	-- delete from CUENTAS_PACIENTES
	-- where RUB_CODIGO=32 --DerechoAnestecia
	-- and ATE_CODIGO=@p_CodigoAtencion 
 
 --end
 
 

 
 /***********************************************************/  
 /************VALORES DE HOSPITALIZACION********************/
 select @PreciosHabitacion=pc.PRE_VALOR, @DescripcionHabitacion=cc.CAC_NOMBRE from CATALOGO_COSTOS cc, PRECIOS_POR_CONVENIOS pc, CATEGORIAS_CONVENIOS cca
where cc.CAC_CODIGO=pc.CAC_CODIGO and cca.CAT_CODIGO=pc.CAT_CODIGO and cca.ASE_CODIGO=@p_Convenio and CAC_NOMBRE like '%'+@p_Habitacion+'%'

 if (@PreciosHabitacion>0)  
 begin  
    
  /*SECUENCIAL CUENTAS_PACIENTES*/  
  
  SELECT @SecuencialCuentas = (ISNULL(MAX(CUE_CODIGO),0) + 1 )  
  FROM CUENTAS_PACIENTES      
  select @p_Habitacion=codpro from SIC3000..Producto where despro=@DescripcionHabitacion
 insert into HIS3000..CUENTAS_PACIENTES   
  values  
  (  
   @SecuencialCuentas /*CUE_CODIGO*/,  
   @p_CodigoAtencion /*ATE_CODIGO*/,  
   GETDATE()/*CUE_FECHA*/,  
   @p_Habitacion /*PRO_CODIGO*/,  
   @DescripcionHabitacion  /*CUE_DETALLE*/,  
   @PreciosHabitacion /*CUE_VALOR_UNITARIO*/,  
   @p_Dias /*CUE_CANTIDAD*/,  
   @p_Dias * @PreciosHabitacion /*CUE_VALOR*/,  
   0  /*CUE_IVA*/,  
   1  /*CUE_ESTADO*/,  
   '' /*CUE_NUM_FAC*/,  
   5 /*RUB_CODIGO*/,  
   5 /*PED_CODIGO*/,  
   @p_CodigoUsuario /*ID_USUARIO*/,  
   0 /*CAT_CODIGO*/,  
   @p_Habitacion /*PRO_CODIGO_BARRAS*/,  
   '' /*CUE_NUM_CONTROL*/,  
   'VALORES AUTOMATICOS HIS3000' /*CUE_OBSERVACION*/,  
   0 /*MED_CODIGO*/,  
   0 /*CUE_ORDER_IMPRESION*/,  
   0 /*Codigo_Pedido*/  ,
   '', --idtipo medico  23102019
   0, ---costo
   '', --numvale
   'N' ---divide factura
   ,0
   ,0
   ,0
   ,''
  )  
   
 end  
 
 /**********************************************************/  
declare @auxPerporsentaje as decimal(18,4)
-- select @auxPerporsentaje=pc.PRE_PORCENTAJE from CATALOGO_COSTOS cc, PRECIOS_POR_CONVENIOS pc, CATEGORIAS_CONVENIOS cca
--where cc.CAC_CODIGO=pc.CAC_CODIGO and cca.CAT_CODIGO=pc.CAT_CODIGO and cca.ASE_CODIGO=@p_Convenio 
--and cc.CAC_NOMBRE='SERVICIO DE CLINICA (PERM. PACIENTE, ACOMPANANTE, TERAPIA, PEDIATRIA).'
  
-- select @ValorServicioClinica = (SUM(CUE_VALOR_UNITARIO * CUE_CANTIDAD)) from CUENTAS_PACIENTES   
-- where RUB_CODIGO=2 and ATE_CODIGO=@p_CodigoAtencion  

 
-- if (@ValorServicioClinica>0)  
-- begin  
--  set @ValorServicioClinica=(@ValorServicioClinica* @auxPerporsentaje)/100
--  /*SECUENCIAL CUENTAS_PACIENTES*/  
    
--  SELECT @SecuencialCuentas = (ISNULL(MAX(CUE_CODIGO),0) + 1 )  
--  FROM CUENTAS_PACIENTES      
   
--  insert into CUENTAS_PACIENTES   
--  values  
--  (  
--   @SecuencialCuentas /*CUE_CODIGO*/,  
--   @p_CodigoAtencion /*ATE_CODIGO*/,  
--   GETDATE()/*CUE_FECHA*/,  
--   '65111' /*PRO_CODIGO*/,  
--   'SERVICIO DE CLINICA'  /*CUE_DETALLE*/,  
--   @ValorServicioClinica /*CUE_VALOR_UNITARIO*/,  
--   1 /*CUE_CANTIDAD*/,  
--   @ValorServicioClinica /*CUE_VALOR*/,  
--   0  /*CUE_IVA*/,  
--   1  /*CUE_ESTADO*/,  
--   '' /*CUE_NUM_FAC*/,  
--   25 /*RUB_CODIGO*/,  
--   13 /*PED_CODIGO*/,  
--   @p_CodigoUsuario /*ID_USUARIO*/,  
--   0 /*CAT_CODIGO*/,  
--   '65111' /*PRO_CODIGO_BARRAS*/,  
--   '' /*CUE_NUM_CONTROL*/,  
--   'VALORES AUTOMATICOS HIS3000' /*CUE_OBSERVACION*/,  
--   0 /*MED_CODIGO*/,  
--   0 /*CUE_ORDER_IMPRESION*/,  
--   0 /*Codigo_Pedido*/,
--   '', --idtipo medico  23102019
--   0, ---costo
--   '', --numvale
--   'N' ---divide factura
--   ,0,0
--  )  
   
-- end  
 
 ----------------------------------------------------------------------
 ----------------------------------------------------------------------
select @auxPerporsentaje=pc.PRE_PORCENTAJE from CATALOGO_COSTOS cc, PRECIOS_POR_CONVENIOS pc, CATEGORIAS_CONVENIOS cca
where cc.CAC_CODIGO=pc.CAC_CODIGO and cca.CAT_CODIGO=pc.CAT_CODIGO and cca.ASE_CODIGO=@p_Convenio and CAC_NOMBRE='ADMINISTRACION MEDICAMENTOS'
and cc.CAC_NOMBRE='ADMINISTRACION MEDICAMENTOS'
select @ValorAdministracionMed=SUM(CUE_VALOR_UNITARIO * CUE_CANTIDAD) from CUENTAS_PACIENTES   
where RUB_CODIGO in (1,27) and ATE_CODIGO=@p_CodigoAtencion 
if (@ValorAdministracionMed>0)  
begin  
	set @ValorAdministracionMed=(@ValorAdministracionMed * @auxPerporsentaje)/100
  /*SECUENCIAL CUENTAS_PACIENTES*/  
  SELECT @SecuencialCuentas = (ISNULL(MAX(CUE_CODIGO),0) + 1 )  
  FROM CUENTAS_PACIENTES      
   
  insert into CUENTAS_PACIENTES   
  values  
  (  
   @SecuencialCuentas /*CUE_CODIGO*/,  
   @p_CodigoAtencion /*ATE_CODIGO*/,  
   GETDATE()/*CUE_FECHA*/,  
   '106234' /*PRO_CODIGO*/,  
   'ADMINISTRACION MEDICAMENTOS Y SUMINISTROS/INSUMOS'  /*CUE_DETALLE*/,  
   @ValorAdministracionMed /*CUE_VALOR_UNITARIO*/,  
   1 /*CUE_CANTIDAD*/,  
   @ValorAdministracionMed /*CUE_VALOR*/,  
   0  /*CUE_IVA*/,  
   1  /*CUE_ESTADO*/,  
   '' /*CUE_NUM_FAC*/,  
   1 /*RUB_CODIGO*/,  
   1 /*PED_CODIGO*/,  
   @p_CodigoUsuario /*ID_USUARIO*/,  
   0 /*CAT_CODIGO*/,  
   '106234' /*PRO_CODIGO_BARRAS*/,  
   '' /*CUE_NUM_CONTROL*/,  
   'VALORES AUTOMATICOS HIS3000' /*CUE_OBSERVACION*/,  
   0 /*MED_CODIGO*/,  
   0 /*CUE_ORDER_IMPRESION*/,  
   0 /*Codigo_Pedido*/,
   '', --idtipo medico  23102019
   0, ---costo
   '', --numvale
   'N' ---divide factura  
   ,0
   ,0
   ,0
   ,''
  )  
   
 end  

---------------------------------------------------------------
---------------------------------------------------------------


-- select @auxPerporsentaje=pc.PRE_PORCENTAJE from CATALOGO_COSTOS cc, PRECIOS_POR_CONVENIOS pc, CATEGORIAS_CONVENIOS cca
--where cc.CAC_CODIGO=pc.CAC_CODIGO and cca.CAT_CODIGO=pc.CAT_CODIGO and cca.ASE_CODIGO=@p_Convenio
--and cc.CAC_NOMBRE='DERECHO DE RECUPERACION(DEPENDE TIEMPO CIRUJIA)'

--select @ValorDerechoRecuperacion=SUM(CUE_VALOR_UNITARIO * CUE_CANTIDAD)from CUENTAS_PACIENTES   
-- where RUB_CODIGO in (11) and ATE_CODIGO = @p_CodigoAtencion  
   
-- if (@ValorDerechoRecuperacion>0)  
-- begin  
--    set @ValorDerechoRecuperacion=(@ValorDerechoRecuperacion*@auxPerporsentaje)/100
--  /*SECUENCIAL CUENTAS_PACIENTES*/  
    
--  SELECT @SecuencialCuentas = (ISNULL(MAX(CUE_CODIGO),0) + 1 )  
--  FROM CUENTAS_PACIENTES      
   
--  insert into CUENTAS_PACIENTES   
--  values  
--  (  
--   @SecuencialCuentas /*CUE_CODIGO*/,  
--   @p_CodigoAtencion /*ATE_CODIGO*/,  
--   GETDATE()/*CUE_FECHA*/,  
--   '64111' /*PRO_CODIGO*/,  
--   'DERECHO DE RECUPERACION'  /*CUE_DETALLE*/,  
--   @ValorDerechoRecuperacion /*CUE_VALOR_UNITARIO*/,  
--   1 /*CUE_CANTIDAD*/,  
--   @ValorDerechoRecuperacion /*CUE_VALOR*/,  
--   0  /*CUE_IVA*/,  
--   1  /*CUE_ESTADO*/,  
--   '' /*CUE_NUM_FAC*/,  
--   24 /*RUB_CODIGO*/,  
--   13 /*PED_CODIGO*/,  
--   @p_CodigoUsuario /*ID_USUARIO*/,  
--   0 /*CAT_CODIGO*/,  
--   '64111' /*PRO_CODIGO_BARRAS*/,  
--   '' /*CUE_NUM_CONTROL*/,  
--   'VALORES AUTOMATICOS HIS3000' /*CUE_OBSERVACION*/,  
--   0 /*MED_CODIGO*/,  
--   0 /*CUE_ORDER_IMPRESION*/,  
--   0 /*Codigo_Pedido*/,
--   '', --idtipo medico  23102019
--   0, ---costo
--   '', --numvale
--   'N' ---divide factura
--   ,0,0
--  )  
   
-- end  
   
-- ---------------------------------------------------------------------
-- ---------------------------------------------------------------------
  
-- select @auxPerporsentaje=pc.PRE_PORCENTAJE from CATALOGO_COSTOS cc, PRECIOS_POR_CONVENIOS pc, CATEGORIAS_CONVENIOS cca
--where cc.CAC_CODIGO=pc.CAC_CODIGO and cca.CAT_CODIGO=pc.CAT_CODIGO and cca.ASE_CODIGO=@p_Convenio
--and cc.CAC_NOMBRE='DERECHO DE ANESTESIA(DEPENDE DEL TIEMPO DE CIRUJIA)'

--select @ValorDerechoAnestecia=SUM(CUE_VALOR_UNITARIO * CUE_CANTIDAD) from CUENTAS_PACIENTES   
-- where RUB_CODIGO in (11) and ATE_CODIGO=@p_CodigoAtencion  
   
-- if (@ValorDerechoAnestecia>0)  
-- begin  
--    set @ValorDerechoAnestecia=(@ValorDerechoAnestecia*@auxPerporsentaje)/100
--  /*SECUENCIAL CUENTAS_PACIENTES*/  
    
--  SELECT @SecuencialCuentas = (ISNULL(MAX(CUE_CODIGO),0) + 1 )  
--  FROM CUENTAS_PACIENTES      
   
--  insert into CUENTAS_PACIENTES   
--  values  
--  (  
--   @SecuencialCuentas /*CUE_CODIGO*/,  
--   @p_CodigoAtencion /*ATE_CODIGO*/,  
--   GETDATE()/*CUE_FECHA*/,  
--   '3255' /*PRO_CODIGO*/,  
--   'DERECHO DE ANESTESIA'  /*CUE_DETALLE*/,  
--   @ValorDerechoAnestecia /*CUE_VALOR_UNITARIO*/,  
--   1 /*CUE_CANTIDAD*/,  
--   @ValorDerechoAnestecia /*CUE_VALOR*/,  
--   0  /*CUE_IVA*/,  
--   1  /*CUE_ESTADO*/,  
--   '' /*CUE_NUM_FAC*/,  
--   32 /*RUB_CODIGO*/,  
--   13 /*PED_CODIGO*/,  
--   @p_CodigoUsuario /*ID_USUARIO*/,  
--   0 /*CAT_CODIGO*/,  
--   '3255' /*PRO_CODIGO_BARRAS*/,  
--   '' /*CUE_NUM_CONTROL*/,  
--   'VALORES AUTOMATICOS HIS3000' /*CUE_OBSERVACION*/,  
--   0 /*MED_CODIGO*/,  
--   0 /*CUE_ORDER_IMPRESION*/,  
--   0 /*Codigo_Pedido*/,
--   '', --idtipo medico  23102019
--   0, ---costo
--   '', --numvale
--   'N' ---divide factura
--   ,0,0
--  )  
   
--end  
  
end





alter table HC_PROTOCOLO_OPERATORIO
alter column PROT_PREOPERATORIO varchar(5000)

alter table HC_PROTOCOLO_OPERATORIO
alter column PROT_PROYECTADA varchar(5000)

alter table HC_PROTOCOLO_OPERATORIO
alter column PROT_POSTOPERATORIO varchar(5000)



create table PRESCRIPCIONES_CONSULTA_EXTERNA(
PCE_CODIGO bigint IDENTITY(1,1) NOT NULL,
ID_USUARIO BIGINT NULL,
USUARIO VARCHAR(100) NULL, 
INDICACION VARCHAR(500) NULL,
FARMACOS VARCHAR(500) NULL,
FECHA_ADMIN DATETIME,
ADMINISTRADO BIT,
ID_FORM002 BIGINT NULL)


alter table PACIENTES_DATOS_ADICIONALES2
add ID_USUARIO INT 

alter table PACIENTES_DATOS_ADICIONALES2
add ID_USUARIO_REVIERTE INT 


USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_ListaPedidoPaciente]    Script Date: 02/03/2021 16:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_ListaPedidoPaciente]
(

	@codigoArea AS INT,
	@codigoAtencion AS INT

)
AS
BEGIN 

	SELECT c.Codigo_Pedido PED_CODIGO, c.RUB_CODIGO PEA_CODIGO, c.PRO_CODIGO PEE_CODIGO, c.CUE_DETALLE PED_DESCRIPCION, c.CUE_FECHA PED_FECHA,
	u.ID_USUARIO, u.APELLIDOS + ' ' + u.NOMBRES USUARIO, p.PAC_HISTORIA_CLINICA HISTORIA_CLINICA, a.ATE_CODIGO,
	p.PAC_APELLIDO_PATERNO + ' ' + p.PAC_APELLIDO_MATERNO + ' ' + p.PAC_NOMBRE1 + ' ' + p.PAC_NOMBRE2 PACIENTE, c.CUE_CODIGO PDD_CODIGO,
	c.PRO_CODIGO, c.CUE_DETALLE PRO_DESCRIPCION, c.CUE_CANTIDAD, CONCAT(m.MED_APELLIDO_PATERNO, +' '+ m.MED_NOMBRE1) MEDICO
	FROM ATENCIONES a, PACIENTES p, CUENTAS_PACIENTES c, USUARIOS u, MEDICOS m
	WHERE a.PAC_CODIGO = p.PAC_CODIGO AND a.ATE_CODIGO = c.ATE_CODIGO AND c.ID_USUARIO = u.ID_USUARIO AND
	c.MED_CODIGO = m.MED_CODIGO AND c.RUB_CODIGO = @codigoArea AND c.ATE_CODIGO = @codigoAtencion
	order by c.PED_CODIGO, c.CUE_FECHA desc

END




---solo pasteur 
USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_ImpresionDevolucion]    Script Date: 03/03/2021 18:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_ImpresionDevolucion]( @Pedido as int)            
as            
begin            
 select             
 PACIENTES.PAC_APELLIDO_PATERNO + ' ' + PACIENTES.PAC_APELLIDO_MATERNO + ' ' +PACIENTES.PAC_NOMBRE1 + ' ' +PACIENTES.PAC_NOMBRE2 AS PACIENTE,             
 PACIENTES.PAC_IDENTIFICACION AS IDENTIFICACION,            
 PEDIDO_DEVOLUCION.DevFecha as FECHA,            
 PEDIDO_DEVOLUCION.DevCodigo AS [NUMERO PEDIDO],            
 PEDIDO_DEVOLUCION.ID_USUARIO AS [CODIGO USUARIO],             
 USUARIOS.USR AS USUARIO ,            
 MEDICOS.MED_APELLIDO_PATERNO + ' ' + MEDICOS.MED_APELLIDO_MATERNO + ' ' + MEDICOS.MED_NOMBRE1 + ' ' + MEDICOS.MED_NOMBRE2 as MEDICOS,            
 PEDIDO_DEVOLUCION_DETALLE.PRO_CODIGO, dbo.datosproducto(PEDIDO_DEVOLUCION_DETALLE.PRO_CODIGO) as Producto,            
 DevDetCantidad as PDD_CANTIDAD, DevDetValor as PDD_VALOR, DevDetValor as PDD_IVA, DevDetIvaTotal PDD_TOTAL,        
 PAC_HISTORIA_CLINICA as HISTORIA,        
 ATENCIONES.ATE_NUMERO_ATENCION AS ATENCION,        
 (SELECT HABITACIONES.hab_Numero FROM HABITACIONES WHERE HABITACIONES.hab_Codigo=ATENCIONES.HAB_CODIGO)  AS HABITACION,        
 (SELECT PEDIDOS_ESTACIONES.PEE_NOMBRE FROM PEDIDOS_ESTACIONES WHERE PEDIDOS_ESTACIONES.PEE_CODIGO=PEDIDOS.PEE_CODIGO) AS ESTACION, 
 PEDIDOS.PED_DESCRIPCION AS OBSERVACION
 from PEDIDO_DEVOLUCION,PEDIDO_DEVOLUCION_DETALLE,USUARIOS,MEDICOS,ATENCIONES,PACIENTES,PEDIDOS       
 where PEDIDO_DEVOLUCION.ID_USUARIO=USUARIOS.ID_USUARIO            
 AND PEDIDO_DEVOLUCION.Ped_Codigo=PEDIDOS.PED_CODIGO           
 AND PEDIDO_DEVOLUCION.DevCodigo=PEDIDO_DEVOLUCION_DETALLE.DevCodigo        
 and MEDICOS.MED_CODIGO=PEDIDOS.MED_CODIGO            
 and PEDIDO_DEVOLUCION.DevCodigo=@Pedido            
 and pacientes.PAC_CODIGO=ATENCIONES.PAC_CODIGO            
 and ATENCIONES.ATE_CODIGO=pedidos.ATE_CODIGO      
            
 order by PRO_CODIGO            
end  
  
  



