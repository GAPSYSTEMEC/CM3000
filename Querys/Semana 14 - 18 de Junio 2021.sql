--PARA TODOS





-- USE [His3000]
-- GO
-- /****** Object:  StoredProcedure [dbo].[sp_RecuperaKardex]    Script Date: 14/06/2021 12:10:35 ******/
-- SET ANSI_NULLS ON
-- GO
-- SET QUOTED_IDENTIFIER ON
-- GO
-- ALTER PROCEDURE [dbo].[sp_RecuperaKardex]
 -- @ateCodigo varchar(10)

-- AS
-- BEGIN 
	-- set @ateCodigo = (select ATE_CODIGO from ATENCIONES where ATE_NUMERO_ATENCION = @ateCodigo)

	-- SELECT * FROM KARDEXMEDICAMENTOS
	-- where AteCodigo = @ateCodigo
	-- order by Administrado, NoAdministrado ASC, FechaAdministración, HORA ASC 

-- END


-- alter PROCEDURE sp_DetallePorItem  
  
 -- @ateCodigo AS BIGINT  
  
-- AS  
-- BEGIN  
  
 -- select COUNT(CUE_DETALLE) AS CANTIDAD, CUE_DETALLE AS DETALLE, CUE_IVA as IVA, 
 -- SUM(CUE_VALOR_UNITARIO*CUE_CANTIDAD) AS TOTAL, r.RUB_NOMBRE
 -- from CUENTAS_PACIENTES, RUBROS r
 -- where ATE_CODIGO=@ateCodigo  and r.RUB_CODIGO = CUENTAS_PACIENTES.RUB_CODIGO
 -- group by CUE_DETALLE, CUE_IVA, r.RUB_CODIGO, r.RUB_NOMBRE
 -- order by r.RUB_CODIGO, 2 asc  
  
-- END

create PROCEDURE sp_ListadoPedidosQuirofano
@ate_codigo bigint
AS 
SELECT CONVERT(VARCHAR, CUE_FECHA, 111) AS FECHA, Codigo_Pedido AS 'COD. PEDIDO', 
CUE_OBSERVACION AS OBSERVACION, U.APELLIDOS + ' ' + U.NOMBRES AS USUARIO
FROM CUENTAS_PACIENTES CP
INNER JOIN USUARIOS U ON CP.ID_USUARIO = U.ID_USUARIO
WHERE ATE_CODIGO = @ate_codigo AND CUE_OBSERVACION = 'PEDIDO GENERADO POR QUIROFANO'
GROUP BY CONVERT(VARCHAR, CUE_FECHA, 111), Codigo_Pedido, CUE_OBSERVACION,
U.APELLIDOS + ' ' + U.NOMBRES
GO

alter  procedure [dbo].[sp_GuardaPedidoDevolucionDetalle]        
(              
 @DevCodigo as bigint,              
 @PRO_CODIGO as bigint,              
 @PRO_DESCRIPCION as varchar(50),              
 @DevDetCantidad as int ,              
 @DevDetValor as decimal(10,2) ,              
 @DevDetIva as decimal(10,2),              
 @DevDetIvaTotal as decimal(10,2),              
 @PDD_CODIGO as bigint,              
 @PED_CODIGO as bigint,      
 @ATE_CODIGO as bigint,  
 @OBSERVACION AS VARCHAR(5000) --CAMBIOS EDGAR CONTIENE LA RAZON DE DEVOLUCION 20201120  
)              
as              
begin  
 declare @Division as int              
 declare @LocalSic as int              
 declare @Usuario as int              
 declare @Grupo as int                
 declare @Seccion as int                
 declare @Departamento as int                
 declare @SubGrupo as int                
 declare @FechaT as date                
 declare @cantidad as decimal(18,4)                  
 declare @producto as varchar(16)                  
 declare @CodigoPedido as int                
 declare @AreaPedidoHis as int                
 declare @CostoProducto as Decimal(10,4)   
 declare @CostoProductoUlt as Decimal(10,4)                
 declare @Proveedor as int               
 declare @PrecioVenta as Decimal(10,2)    
 declare @CodigoAtencion as Bigint           
 declare @HistoriaClinica as Bigint  
 declare @Fecha as BigInt                
 set @FechaT=CAST(CONVERT(varchar(11),getdate(),103) as date) -- Transformo la fecha al formato 'dd/mm/yyyy'                
 select @Fecha = dbo.TransformaFercha(@FechaT)-- Transformo la fecha a numero                
 --select dbo.Transformafercha('18/10/2012')-- Transformo la fecha a numero  
 insert into PEDIDO_DEVOLUCION_DETALLE values           
 (              
  @DevCodigo ,              
  @PRO_CODIGO ,  
  @PRO_DESCRIPCION ,              
  @DevDetCantidad ,              
  @DevDetValor ,              
  @DevDetIva ,              
  @DevDetIvaTotal ,              
  @PDD_CODIGO               
 )              
 select @Division=pea_codigo from PEDIDOS              
 where PEDIDOS.PED_CODIGO=@PED_CODIGO      
   
 select @CodigoAtencion=PEDIDOS.ATE_CODIGO --Capturo el codigo de la atencion            
 from PEDIDOS             
 WHERE PEDIDOS.PED_CODIGO=@PED_CODIGO     
       
 select @HistoriaClinica=PACIENTES.PAC_HISTORIA_CLINICA       
 from   ATENCIONES,PACIENTES      
 where  ATENCIONES.PAC_CODIGO=PACIENTES.PAC_CODIGO      
 and    ATE_CODIGO=@CodigoAtencion      
   
                         
 --select @LocalSic=codlocal from Sic3000..Locales              
 --where Local_His=@Division            
 set @LocalSic= (select codlocal from Sic3000..Locales where Local_His = 1 )
 select @Usuario=id_usuario from PEDIDO_DEVOLUCION              
 where DevCodigo=@DevCodigo              
   
 update Sic3000..Bodega set existe=existe+@DevDetCantidad               
 where codpro=@PRO_CODIGO              
 and codbod=@LocalSic              
   
 ----cambio hr 2019 ultimo costo por costo promedio  
  ---Select @CostoProducto=precos,      
  Select @CostoProducto=cospro,      
  @CostoProductoUlt=precos,             
  @PrecioVenta=preven,  
  @Grupo =codgru,                
  @Seccion =codsec,               
  @Departamento =coddep,                
  @SubGrupo =codsub,                
  @Division =coddiv                
  from sic3000..PRODUCTO                 
  where PRODUCTO.codpro=@PRO_CODIGO  
    
  insert into sic3000..kardex                
  values                
  (                
  @PRO_CODIGO,                
  GETDATE(),                
  @DevCodigo,                
  'DEVP',                
  @LocalSic,                
  1,                
  @DevDetCantidad,              
  0,                
  0,                  
  'DEVOLUCION PEDIDO HIS',                
  @Usuario,               
  @CostoProductoUlt,                
  --(@CostoProductoUlt*@cantidad),        
  (@CostoProductoUlt*@DevDetCantidad),               
  @CostoProducto,                
  @Fecha,                
  @Proveedor,                
  @PrecioVenta,               
  @Grupo ,                
  @Seccion ,                
  @Departamento ,                
  @SubGrupo ,                
  @Division ,                
  @Fecha,                
  --(@CostoProducto*@cantidad) ,   
  (@CostoProducto*@DevDetCantidad) ,           
  null,               
  @HistoriaClinica, --/ Historia Clinica / ,            
  @CodigoAtencion, --/ AtencionCodigo / ,            
  null, --/ Factura /    
  0,  
  null  
  )               
 /*actualizo la cuenta del paciente*/      
 declare @CantidadCuenta as decimal(18,4) -- ALMACENA LA CANTIDAD ANTERIOR      
 declare @ValorUnitarioCuenta as decimal(18,4)  -- ALMACENA EL VALOR UNITARIO DE LA CUENTA      
 declare @IVACuenta as decimal(18,4)-- -- ALMACENA EL IVA ANTERIOR DE LA CUENTA      
 declare @CantidadCuentaNueva as decimal(18,4) -- PARA EL CALCULO DE LA NUEVA CANTIDAD DE LA CUENTA      
 declare @TotalCuentaNueva as decimal(18,4) -- PARA EL CALCULO DE EL NUEVO TOTAL DE LA CUENTA      
 declare @IVACuentaNueva as decimal(18,4) -- PARA EL CALCULO DE EL NUEVO IVA DE LA CUENTA      
 /*CAPTURO LOS VALORES DE LA CUENTA PARA ESE PRODUCTO*/      
 select       
 @CantidadCuenta=CUE_CANTIDAD,      
 @ValorUnitarioCuenta=CUE_VALOR_UNITARIO,      
 @IVACuenta=CUE_IVA      
 from CUENTAS_PACIENTES      
 where ATE_CODIGO=@ATE_CODIGO      
 and Codigo_Pedido=@PED_CODIGO      
 and PRO_CODIGO=@PRO_CODIGO      
 /****************************************************/     
 /*CALCULO LA NUEVA CANTIDAD DE LA CUENTA ********************/      
 set @CantidadCuentaNueva= @CantidadCuenta - @DevDetCantidad      
 /************************************************************/     
 if @CantidadCuentaNueva=0 -- SI LA NUEVA CANTIDAD ES CERO ACTUALIZO LOS VALORES EN 0      
 begin  
 update CUENTAS_PACIENTES      
 set CUE_CANTIDAD=0,      
 CUE_IVA=0,      
 CUE_VALOR=0,      
 CUE_OBSERVACION='DEVOLUCION N.' + CAST(@DevCodigo AS VARCHAR(64)) + ' ' + @OBSERVACION  
 where ATE_CODIGO=@ATE_CODIGO      
 and Codigo_Pedido=@PED_CODIGO      
 and PRO_CODIGO=@PRO_CODIGO  
 end  
 else -- CASO CONTRARIO CALCULO LOS NUEVOS VALORES Y ACTUALIZO      
 begin      
  
       
  
 if @IVACuenta!=0      
  
 begin      
  
  SET @IVACuentaNueva= ((@CantidadCuentaNueva*@ValorUnitarioCuenta)*12)/100      
  
 end      
  
 else      
  
 begin      
  
  SET @IVACuentaNueva=0      
  
 end      
  
       
  
 SET @TotalCuentaNueva=(@CantidadCuentaNueva*@ValorUnitarioCuenta)  
  
       
  
 update CUENTAS_PACIENTES      
  
 set CUE_CANTIDAD=@CantidadCuentaNueva,      
  
 CUE_IVA=@IVACuentaNueva,      
  
 CUE_VALOR=@TotalCuentaNueva,      
  
 CUE_OBSERVACION='DEVOLUCION N.' + CAST(@DevCodigo AS VARCHAR(64)) + ' ' + @OBSERVACION  ,  
  
 COSTO=ROUND(ISNULL(@CostoProducto,0)*ISNULL(@CantidadCuentaNueva,0),2)  
  
 where ATE_CODIGO=@ATE_CODIGO      
  
 and Codigo_Pedido=@PED_CODIGO      
  
 and PRO_CODIGO=@PRO_CODIGO      
  
      
  
 end  
 print @Fecha  
end



CREATE TABLE TIPO_CONTINGENCIA
( TC_CODIGO INT IDENTITY(1,1) NOT NULL,
TC_DESCRIPCION NVARCHAR(500),
TC_ESTADO BIT)




INSERT INTO TIPO_CONTINGENCIA VALUES('ACCIDENTE GRAVE DEBIDAMENTE CERTIFICADO', 1)
INSERT INTO TIPO_CONTINGENCIA VALUES('ENFERMEDAD CATASTROFICA', 1)
INSERT INTO TIPO_CONTINGENCIA VALUES('ENFERMEDAD GENERAL', 1)
INSERT INTO TIPO_CONTINGENCIA VALUES('MATERNIDAD', 1)
INSERT INTO TIPO_CONTINGENCIA VALUES('OTROS', 1)
INSERT INTO TIPO_CONTINGENCIA VALUES('PRESUNCIÓN DE ACCIDENTE DE TRABAJO', 1)
INSERT INTO TIPO_CONTINGENCIA VALUES('PRESUNCIÓN DE ENFERMEDAD PROFESIONAL U OCUPACIONAL', 1)


USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_TarifarioHonorarioReporte]    Script Date: 17/06/2021 16:13:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_TarifarioHonorarioReporte]
@hon_codigo int
AS
SELECT HT.HON_HISTORIA_CLINICA, HT.HON_PACIENTE, TD.TAD_REFERENCIA, 
HTD.HOD_DESCRIPCION, HTD.HOD_CANTIDAD, HTD.HOD_UVR, HTD.HOD_SUBTOTAL, HT.HON_TOTAL,
M.MED_APELLIDO_PATERNO + ' ' + m.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + m.MED_NOMBRE2 AS MEDICO, 
HT.HON_DIAGNOSTICO, HTD.HOD_ANESTESIA
FROM HONORARIOS_TARIFARIO HT
INNER JOIN HONORARIOS_TARIFARIO_DETALLE HTD ON HT.HON_CODIGO = HTD.HON_CODIGO
INNER JOIN TARIFARIOS_DETALLE TD ON HTD.TAD_CODIGO = TD.TAD_CODIGO
INNER JOIN MEDICOS M ON HT.MED_CODIGO = M.MED_CODIGO
WHERE HT.HON_CODIGO = @hon_codigo









--FIN TODOS







--SOLO PASTEUR


-- ALTER TABLE HC_INTERCONSULTA
-- ADD HIN_INTERCONSU_ID Varchar(15) NULL

-- ALTER TABLE HC_INTERCONSULTA
-- ADD HIN_FECHACREACION datetime NOT NULL


-- --AGREGAR LOS MEDICOS DEPENDIENDO DE LOS PERFILES YA ASIGNADOS

-- SELECT * FROM USUARIOS WHERE APELLIDOS LIKE '%GALARZA%'

-- SELECT * FROM USUARIOS_PERFILES WHERE ID_USUARIO = 196 ORDER BY 1 ASC
-- SELECT * FROM USUARIOS_PERFILES WHERE ID_USUARIO = 195 ORDER BY  1 ASC

-- SELECT * FROM USUARIOS U 
-- INNER JOIN DEPARTAMENTOS D ON U.DEP_CODIGO = D.DEP_CODIGO WHERE D.DEP_CODIGO = 34

-- --ME AYUDA HACER EL INSERT CON LOS REGISTRO DEL PACIENTE QUE YA ESTA DEFINIDO SUS MODULOS.
-- INSERT INTO USUARIOS_PERFILES 
-- SELECT ID_PERFIL, 196, ID_USUARIOS_PERFILES, INSERTAR, ACTUALIZAR, ELIMINAR
-- FROM USUARIOS_PERFILES WHERE ID_USUARIO = 195 AND ID_PERFIL <> 18

--FIN PASTEUR




--SOLO ALIANZA
ALTER TABLE HC_INTERCONSULTA
ADD HIN_INTERCONSU_ID Varchar(15) NULL


--PRIMERO SE ELIMINA LA COLUMNA 
ALTER TABLE HC_INTERCONSULTA
DROP COLUMN HIN_FECHACREACION

--LUEGO SE VUELVE A CREAR PERO CON UN VALOR YA DEFINIDO
ALTER TABLE HC_INTERCONSULTA
ADD HIN_FECHACREACION DATETIME DEFAULT GETDATE() NOT NULL



ALTER TABLE AUDITA_MEDICOS
ALTER COLUMN MED_NOMBRE1 NVARCHAR(100)

ALTER TABLE AUDITA_MEDICOS
ALTER COLUMN MED_NOMBRE2 NVARCHAR(100)


ALTER TABLE AUDITA_MEDICOS
ALTER COLUMN MED_APELLIDO_PATERNO NVARCHAR(100)


ALTER TABLE AUDITA_MEDICOS
ALTER COLUMN MED_APELLIDO_MATERNO NVARCHAR(100)


ALTER TABLE AUDITA_MEDICOS
ALTER COLUMN MED_DIRECCION NVARCHAR(500)

ALTER TABLE AUDITA_MEDICOS
ALTER COLUMN MED_TELEFONO_CONSULTORIO NVARCHAR(25)

--FIN ALIANZA
