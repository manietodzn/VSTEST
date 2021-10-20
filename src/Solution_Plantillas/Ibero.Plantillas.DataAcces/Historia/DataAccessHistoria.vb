Imports System.Data.SqlClient
Imports FrameWork
Imports Ibero.Plantillas.Entities
Imports LibreriaUIA.Funciones

Public Class DataAccessHistoria
#Region "Propiedades Privadas"

    Private strConexion As String = ObtenerCadenaConexion()
    Public TB As String = Chr(9)
#End Region

    Public Function CargaPlazas(sFolioEntidad As String) As List(Of String)

        Dim Texto As String
        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim list As New List(Of String)

        Try

            strSQL = "SELECT num_plaza, b.nombre_puesto FROM plantillas a, puestosx b WHERE a.puesto = b.puesto AND a.folio_entidad = " & sFolioEntidad
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                Do While datReader.Read
                    Texto = Space(5 - Len(datReader.Item("num_plaza") & "-"))
                    Texto = Texto & datReader.Item("num_plaza") & " - "
                    Texto = Texto & datReader.Item("nombre_puesto")

                    list.Add(Texto)

                Loop
            End If
            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return list

    End Function

    Public Function ObtienePlantillasEntidades(sNumPlaza As String) As DataTable

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable

        Try

            strSQL = "SELECT b.entidad, b.nombre_entidad, fecha_inicio as fechai, fecha_final as fechaf FROM hst_plantillas a, entidades b WHERE a.folio_entidad = b.folio_entidad AND a.num_plaza = " & sNumPlaza & " AND a.tipo_historia = 1 ORDER BY fecha_inicio DESC"
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                dt.Load(datReader)
            End If

            datReader.Close()
            objDat.Dispose()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return dt

    End Function

    Public Function ObtienePlantillasPuestos(sNumPlaza As String) As DataTable

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable


        Try

            strSQL = "SELECT a.puesto, b.nombre_puesto, fecha_inicio as fechai, fecha_final as fechaf FROM hst_plantillas a, puestosx b WHERE a.puesto = b.puesto AND a.num_plaza = " & sNumPlaza & " AND a.tipo_historia = 2 ORDER BY fecha_inicio DESC"
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                dt.Load(datReader)
            End If

            datReader.Close()
            objDat.Dispose()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return dt

    End Function

    Public Function ObtienePlantillas(sNumPlaza As String) As DataTable

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable


        Try
            strSQL = "SELECT sueldo, sobre_sueldo, otra_remuneracion, ayuda_movilidad, gtos_operacion, fecha_inicio as fechai, fecha_final as fechaf FROM hst_plantillas WHERE num_plaza = " & sNumPlaza & " AND tipo_historia = 3 ORDER BY fecha_inicio DESC"
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                dt.Load(datReader)
            End If

            datReader.Close()
            objDat.Dispose()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return dt

    End Function

    Public Function ObtieneNominaOcupante(sNumPlaza As String) As DataTable

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable


        Try

            strSQL = "SELECT a.nomina_ocupante, b.apellido_paterno, b.apellido_materno, b.nombre, fecha_inicio as fechai, fecha_final as fechaf FROM hst_plantillas a, empleados b WHERE num_plaza = " & sNumPlaza & " AND tipo_historia = 4 AND a.nomina_ocupante = b.nomina ORDER BY fecha_inicio DESC"
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                dt.Load(datReader)
            End If

            datReader.Close()
            objDat.Dispose()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return dt

    End Function

    Public Function ObtieneNominaSuplente(sNumPlaza As String) As DataTable

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable


        Try

            strSQL = "SELECT a.nomina_suplente, b.apellido_paterno, b.apellido_materno, b.nombre, c.descripcion, fecha_inicio as fechai, fecha_final as fechaf FROM hst_plantillas a, empleados b, motivos_plantillas c WHERE a.nomina_suplente = b.nomina AND a.motivo = c.motivo AND tipo_historia = 5 AND num_plaza = " & sNumPlaza & " ORDER BY fecha_inicio DESC"
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)


            If datReader.HasRows Then
                dt.Load(datReader)
            End If

            datReader.Close()
            objDat.Dispose()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return dt

    End Function

    Public Function CargaSueldo(sPuesto As String) As DataTable
        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable

        Try

            strSQL = "SELECT c.sueldo, c.sobre_sueldo, c.otra_remuneracion, c.ayuda_movilidad, c.gto_operacion FROM puestosx a, tabuladores_puestos b, cat_tabuladores c WHERE rtrim(ltrim(a.puesto)) = rtrim(ltrim(b.puesto)) AND"
            strSQL = strSQL & "  b.tabulador = c.tabulador AND b.nivel = c.nivel AND b.tipo = c.tipo AND b.tipo = 0 AND b.tipo_tabulador = 0 AND rtrim(ltrim(a.puesto)) = '" & sPuesto & "'"

            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                dt.Load(datReader)
            End If

            datReader.Close()
            objDat.Dispose()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Function

    Public Function CargaSuplente(sNumeroPlaza As String) As List(Of String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim TextoT As String = String.Empty
        Dim Texto As String = String.Empty
        Dim listResult As New List(Of String)

        Try

            strSQL = "SELECT secuencia, tipo_historia, a.nomina_suplente, " & " b.apellido_paterno, b.apellido_materno, b.nombre, a.motivo, " & " c.descripcion, fecha_inicio as fechaI, " & " fecha_final as fechaF"
            strSQL = strSQL & " FROM hst_plantillas a, empleados b, motivos_plantillas c" & " WHERE a.num_plaza=" & Str(sNumeroPlaza) & " AND a.nomina_suplente = b.nomina" & " AND a.motivo = c.motivo" & " AND tipo_historia=5"

            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                Do While datReader.Read

                    Texto = datReader.Item("secuencia") & TB
                    Texto = Texto & datReader.Item("tipo_historia") & TB
                    Texto = Texto & datReader.Item("nomina_suplente") & TB

                    TextoT = Trim(datReader.Item("apellido_paterno")) & " "
                    TextoT = TextoT & Trim(datReader.Item("apellido_materno")) & " "
                    TextoT = TextoT & Trim(datReader.Item("nombre"))

                    Texto = Texto & TextoT & TB
                    Texto = Texto & datReader.Item("motivo") & TB
                    Texto = Texto & datReader.Item("descripcion") & TB
                    Texto = Texto & Format(datReader.Item("fechaI"), "dd-MM-yyyy") & TB

                    If Not IsDBNull(datReader.Item("fechaF")) Then
                        Texto = Texto & Format(datReader.Item("fechaF"), "dd-MM-yyyy")
                    End If

                    listResult.Add(Texto)

                Loop
            End If
            datReader.Close()
        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return listResult

    End Function

    Public Function CargaTitular(sNumeroPlaza As String) As List(Of String)
        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim TextoT As String
        Dim Texto As String
        Dim listResult As New List(Of String)

        Try

            strSQL = "SELECT secuencia, tipo_historia, a.nomina_ocupante, " & " b.apellido_paterno, b.apellido_materno, b.nombre, " & "fecha_inicio as fechaI, " & " fecha_final as fechaF"
            strSQL = strSQL & " FROM hst_plantillas a, empleados b" & " WHERE a.num_plaza=" & Str(sNumeroPlaza) & " AND a.nomina_ocupante = b.nomina" & " AND tipo_historia=4"

            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                Do While datReader.Read
                    Texto = datReader.Item("secuencia") & TB
                    Texto = Texto & datReader.Item("tipo_historia") & TB
                    Texto = Texto & datReader.Item("nomina_ocupante") & TB
                    TextoT = Trim(datReader.Item("apellido_paterno")) & " "
                    TextoT = TextoT & Trim(datReader.Item("apellido_materno")) & " "
                    TextoT = TextoT & Trim(datReader.Item("nombre"))
                    Texto = Texto & TextoT & TB
                    Texto = Texto & Format(datReader.Item("fechaI"), "dd-MM-yyyy") & TB
                    If Not IsDBNull(datReader.Item("fechaF")) Then
                        Texto = Texto & Format(datReader.Item("fechaF"), "dd-MM-yyyy")
                    End If

                    listResult.Add(Texto)
                Loop
            End If
            datReader.Close()
        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return listResult

    End Function

    Public Function CargaPuesto(sNumeroPlaza As String) As List(Of String)
        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim listResult As New List(Of String)
        Dim Texto As String

        Try

            strSQL = "SELECT secuencia, tipo_historia, a.puesto, b.nombre_puesto, fecha_inicio as fechaI, fecha_final as fechaF FROM hst_plantillas a, puestosx b WHERE a.num_plaza = "
            strSQL = strSQL & Str(sNumeroPlaza) & " AND rtrim(ltrim(a.puesto)) = rtrim(ltrim(b.puesto)) AND tipo_historia = 2"

            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                Do While datReader.Read
                    Texto = datReader.Item("secuencia") & TB
                    Texto = Texto & datReader.Item("tipo_historia") & TB
                    Texto = Texto & datReader.Item("puesto") & TB
                    Texto = Texto & datReader.Item("nombre_puesto") & TB
                    Texto = Texto & Format(datReader.Item("fechaI"), "dd-MM-yyyy") & TB
                    If Not IsDBNull(datReader.Item("fechaF")) Then
                        Texto = Texto & Format(datReader.Item("fechaF"), "dd-MM-yyyy")
                    End If
                    listResult.Add(Texto)
                Loop
            End If
            datReader.Close()
        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return listResult

    End Function

    Public Function CargaDepto(sNumeroPlaza As String) As List(Of String)
        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim listResult As New List(Of String)
        Dim Texto As String

        Try

            strSQL = "SELECT secuencia, tipo_historia, a.folio_entidad, b.entidad, b.nombre_entidad, " & " fecha_inicio as fechaI, " & " fecha_final as fechaF" & " FROM hst_plantillas a, entidades b"
            strSQL = strSQL & " WHERE a.num_plaza=" & Str(sNumeroPlaza) & " AND a.folio_entidad = b.folio_entidad" & " AND tipo_historia=1"

            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                Do While datReader.Read
                    Texto = datReader.Item("secuencia") & TB
                    Texto = Texto & datReader.Item("tipo_historia") & TB
                    Texto = Texto & datReader.Item("folio_entidad") & TB
                    Texto = Texto & datReader.Item("entidad") & TB
                    Texto = Texto & datReader.Item("nombre_entidad") & TB
                    Texto = Texto & Format(datReader.Item("fechaI"), "dd-MM-yyyy") & TB
                    If Not IsDBNull(datReader.Item("fechaF")) Then
                        Texto = Texto & Format(datReader.Item("fechaF"), "dd-MM-yyyy")
                    End If
                    listResult.Add(Texto)
                Loop
            End If
            datReader.Close()
        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return listResult

    End Function

    Public Function CargaMotivosPlantillas() As List(Of String)
        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim listResult As New List(Of String)
        Dim Texto As String

        Try

            strSQL = "SELECT motivo, descripcion FROM motivos_plantillas"
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                Do While datReader.Read
                    Texto = Format(datReader.Item("motivo"), "00#")
                    Texto = Texto & "-" & datReader.Item("descripcion")
                    listResult.Add(Texto)
                Loop
            End If
            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return listResult

    End Function

    Public Sub EliminarHstPlantillas(sNumeroPlaza As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try
            strSQL = "DELETE FROM hst_plantillas WHERE num_plaza = " & Str(sNumeroPlaza) & " AND fecha_final is null"
            objDat.EjecutarComandoDelete(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Sub EliminarHstPlantillas(sTipoHistoria As String, sNumeroPlaza As String, sSecuencia As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "DELETE FROM hst_plantillas WHERE tipo_historia = " & Str(sTipoHistoria) & " AND num_plaza = " & Str(sNumeroPlaza) & " AND secuencia = " & Str(sSecuencia)
            objDat.EjecutarComandoDelete(strSQL, Parametros)


        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Sub ActualizaPlantillaNominaSuplente(sNominaSuplente As String, sMotivo As String, sFechaInicio As String, sFechaFinal As String, sUsuario As String, sTipoHistoria As String, sSecuencia As String, sNumeroPlaza As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "UPDATE hst_plantillas SET nomina_suplente = " & sNominaSuplente & ", motivo = " & sMotivo & ", fecha_inicio = " & sFechaInicio & ", fecha_final = " & sFechaFinal
            strSQL = strSQL & ", usr_audit = '" & sUsuario & "', fch_audit = getdate() WHERE tipo_historia = " & sTipoHistoria & " AND secuencia = " & sSecuencia & " AND num_plaza = " & sNumeroPlaza


            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub

    Public Sub ActualizaPlantillaNominaOcupante(sNominaOcupante As String, sFechaInicio As String, sFechaFinal As String, sUsuario As String, sTipoHistoria As String, sSecuencia As String, sNumeroPlaza As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "UPDATE hst_plantillas SET nomina_ocupante = " & sNominaOcupante & ", fecha_inicio = " & sFechaInicio & ", fecha_final = " & sFechaFinal & ", usr_audit = '"
            strSQL = strSQL & sUsuario & "', fch_audit = getdate() WHERE tipo_historia = " & sTipoHistoria & " AND secuencia = " & sSecuencia & " AND num_plaza = " & sNumeroPlaza

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub

    Public Sub ActualizaPlantillaPuesto(sPuesto As String, sFechaInicio As String, sFechaFinal As String, sUsuario As String, sTipoHistoria As String, sSecuencia As String, sNumeroPlaza As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try
            strSQL = "UPDATE hst_plantillas SET puesto = '" & sPuesto & "', fecha_inicio = " & sFechaInicio & ", fecha_final = " & sFechaFinal & ", usr_audit = '"
            strSQL = strSQL & sUsuario & "', fch_audit = getdate() WHERE tipo_historia = " & sTipoHistoria & " AND num_plaza = " & sNumeroPlaza & " AND secuencia = " & sSecuencia

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub

    Public Sub ActualizaPlantillaSueldo(sSueldo As String, sSobreSueldo As String, sOtrasRemuneraciones As String, sAyudaMovilidad As String, sGastosOperacion As String, sFechaInicio As String, sFechaFinal As String, sUsuario As String _
                                        , sSecuencia As String, sNumeroPlaza As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "UPDATE hst_plantillas SET sueldo = " & sSueldo & ", sobre_sueldo = " & sSobreSueldo & ", otra_remuneracion = " & sOtrasRemuneraciones & ", " & "ayuda_movilidad = "
            strSQL = strSQL & sAyudaMovilidad & ", gtos_operacion = " & sGastosOperacion & ", fecha_inicio = " & sFechaInicio & ", fecha_final = " & sFechaFinal & ", usr_audit = '"
            strSQL = strSQL & sUsuario & "', fch_audit = getdate() WHERE tipo_historia = 3 AND num_plaza = " & sSecuencia & " AND secuencia = " & sNumeroPlaza

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub

    Public Sub ActualizaPlantillaFolioEntidad(sFolioEntidad As String, sFechaInicio As String, sFechaFinal As String, sUsuario As String, sTipoHistoria As String, sSecuencia As String, sNumeroPlaza As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "UPDATE hst_plantillas SET folio_entidad = " & sFolioEntidad & ", fecha_inicio = " & sFechaInicio & ", fecha_final = " & sFechaFinal & ", usr_audit = '"
            strSQL = strSQL & sUsuario & "', fch_audit = getdate() WHERE tipo_historia = " & sTipoHistoria & " AND secuencia = " & sSecuencia & " AND num_plaza = " & sNumeroPlaza

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub
    Public Sub ActualizaPlantilla(sFechaInicio As String, sFechaFinal As String, sUsuario As String, sTipoHistoria As String, sSecuencia As String, sNumeroPlaza As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "UPDATE hst_plantillas SET fecha_inicio = " & sFechaInicio & ", fecha_final = " & sFechaFinal & ", usr_audit = '" & sUsuario & "', fch_audit = getdate() WHERE tipo_historia = " & sTipoHistoria
            strSQL = strSQL & " AND num_plaza = " & sSecuencia & " AND secuencia = " & sNumeroPlaza

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub


    Public Sub InsertaPlantilla(sFechaInicio As String, sFechaFinal As String, sUsuario As String, sTipoHistoria As String, sSecuencia As String, sNumeroPlaza As String, iOpcion As Integer, sNominaSuplente As String, sMotivo As String _
                                , sTitular As String, sPuesto As String, sFolioEntidad As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "INSERT INTO hst_plantillas VALUES (" & sNumeroPlaza & ", " & sSecuencia & ", " & sFechaInicio & ", " & sFechaFinal & ", " & sTipoHistoria


            Select Case iOpcion
                Case 1
                    strSQL = strSQL & ", null, null, null, " & sNominaSuplente & ", " & sMotivo & ", " & "null, null, null, null, null, " & sUsuario & ", getdate())"
                Case 2
                    strSQL = strSQL & ", null, null, " & sTitular & ", " & "null, null, null, null, null, null, null, " & sUsuario & ", getdate())"
                Case 3
                    strSQL = strSQL & ", null, '" & sPuesto & "', " & "null, null, null, null, null, null, null, null, " & sUsuario & ", getdate())"
                Case Else
                    strSQL = strSQL & ", " & sFolioEntidad & ", " & "null, null, null, null, null, null, null, null, null, " & sUsuario & ", getdate())"
            End Select


        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub

    Public Function ObtieneSecuenciaPlantilla(sNumeroPlaza As String) As Integer

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim secuencia As Integer

        Try
            strSQL = "SELECT max(secuencia) FROM hst_plantillas where num_plaza = " & Str(sNumeroPlaza)

            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                datReader.Read()
                secuencia = datReader.Item(0) + 1
            Else
                secuencia = 1
            End If
            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return secuencia

    End Function

End Class
