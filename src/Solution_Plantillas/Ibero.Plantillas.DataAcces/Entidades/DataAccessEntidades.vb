Imports System.Data.SqlClient
Imports FrameWork
Imports Ibero.Plantillas.Entities
Imports LibreriaUIA.Funciones

Public Class DataAccessEntidades
#Region "Propiedades Privadas"
    Private strConexion As String = UsuarioConexion.strConexion
    Public TB As String = Chr(9)
#End Region

    Public Function ValidaEmpleado(sNomina As String) As List(Of String)

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim resultList As New List(Of String)
        Try

            strSQL = "SELECT templ, nomina_procesada FROM empleados WHERE nomina = " & sNomina
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                datReader.Read()

                resultList.Add(datReader.Item("templ"))
                resultList.Add(datReader.Item("nomina_procesada"))
            End If
            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return resultList

    End Function

    Public Function ObtieneNivelEstructura(sLiReporta As String) As String

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim resultList As New List(Of String)
        Dim estructura As String = String.Empty
        Try

            strSQL = "SELECT nivel_estructura FROM entidades WHERE folio_entidad =" & sLiReporta
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                datReader.Read()
                estructura = datReader.Item("nivel_estructura")
            End If
            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return estructura

    End Function

    Public Sub ActualizaEntidades(sEntidad As String, sNombre As String, sEstructura As String, sNominaResponsable As String, sContable As String, sDepartamento As String, sCentroCosto As String, sDepartamentoSCE As String, sCoordinacionSCE As String _
                                  , sReporta As String, sInteraccion As String, sEstatus As String, sEdificio As String, sPiso As String, sSiglas As String, sFechaCreacion As String, sFechaCancelacion As String, sClaveEntidad As String, sUsuario As String _
                                  , sNominaResponsableOrg As String, sFolioEntidad As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "UPDATE entidades SET "
            strSQL = strSQL & "entidad=" & PonNull(sEntidad) & ", "
            strSQL = strSQL & "nombre_entidad=" & PonNull(sNombre) & ", "
            strSQL = strSQL & "nivel_estructura=" & PonNull(sEstructura) & ", "
            If Val(sNominaResponsable) = 0 Then
                strSQL = strSQL & "nomina_responsable=null, "
            Else
                strSQL = strSQL & "nomina_responsable=" & sNominaResponsable & ", "
            End If
            strSQL = strSQL & "contable=" & sContable & ", "
            If sDepartamento = String.Empty Then
                strSQL = strSQL & "depto=null, "
            Else
                strSQL = strSQL & "depto='" & sDepartamento & "', "
            End If
            strSQL = strSQL & "centro_costo=" & PonNull(sCentroCosto) & ", "
            If Val(CStr(sDepartamentoSCE)) = 0 Then
                strSQL = strSQL & "depto_sce=null, "
            Else
                strSQL = strSQL & "depto_sce=" & Str(sDepartamentoSCE) & ", "
            End If
            If Val(CStr(sCoordinacionSCE)) = 0 Then
                strSQL = strSQL & "coordinacion_sce=null, "
            Else
                strSQL = strSQL & "coordinacion_sce=" & Str(sCoordinacionSCE) & ", "
            End If
            If sReporta = 0 Then
                strSQL = strSQL & "reporta=null, "
            Else
                strSQL = strSQL & "reporta=" & Str(sReporta) & ", "
            End If
            If Val(CStr(sInteraccion)) = 0 Then
                strSQL = strSQL & "interaccion=null, "
            Else
                strSQL = strSQL & "interaccion=" & Str(sInteraccion) & ", "
            End If
            strSQL = strSQL & "status=" & PonNull(sEstatus) & ", "
            strSQL = strSQL & "edificio=" & Str(sEdificio) & ", "
            strSQL = strSQL & "piso=" & PonNull(sPiso) & ", "
            strSQL = strSQL & "siglas=" & PonNull(sSiglas) & ", "
            strSQL = strSQL & "fecha_creacion=" & PonFecha(sFechaCreacion) & ", "
            strSQL = strSQL & "fecha_cancelacion=" & PonFecha(sFechaCancelacion) & ", "
            strSQL = strSQL & "clave_entidad='" & sClaveEntidad & "', "
            strSQL = strSQL & "usr_audit='" & sUsuario & "', "
            strSQL = strSQL & "fch_audit=getdate(),"
            If Val(CStr(sNominaResponsableOrg)) = 0 Then
                strSQL = strSQL & "responsable_organigrama=null "
            Else
                strSQL = strSQL & "responsable_organigrama=" & Str(sNominaResponsableOrg)
            End If
            strSQL = strSQL & " WHERE folio_entidad=" & sFolioEntidad

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Sub InsertaEntidades(sEntidad As String, sNombre As String, sEstructura As String, sNominaResponsable As String, sContable As String, sDepartamento As String, sCentroCosto As String, sDepartamentoSCE As String, sCoordinacionSCE As String _
                                  , sReporta As String, sInteraccion As String, sEstatus As String, sEdificio As String, sPiso As String, sSiglas As String, sFechaCreacion As String, sFechaCancelacion As String, sClaveEntidad As String, sUsuario As String _
                                  , sNominaResponsableOrg As String, sFolioEntidad As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "INSERT INTO entidades VALUES ("
            strSQL = strSQL & sFolioEntidad & ", '"
            strSQL = strSQL & sEntidad & "', '"
            strSQL = strSQL & sNombre & "', "
            strSQL = strSQL & PonNull(sEstructura) & ", "
            If Val(CStr(sNominaResponsable)) = 0 Then
                strSQL = strSQL & "null, "
            Else
                strSQL = strSQL & Str(sNominaResponsable) & ", "
            End If
            strSQL = strSQL & Str(sContable) & ", "
            If sDepartamento = "" Then
                strSQL = strSQL & "null, "
            Else
                strSQL = strSQL & "'" & sDepartamento & "', "
            End If
            strSQL = strSQL & PonNull(sCentroCosto) & ", "
            If Val(CStr(sDepartamentoSCE)) = 0 Then
                strSQL = strSQL & "null, "
            Else
                strSQL = strSQL & Str(sDepartamentoSCE) & ", "
            End If
            If Val(CStr(sCoordinacionSCE)) = 0 Then
                strSQL = strSQL & "null, "
            Else
                strSQL = strSQL & Str(sCoordinacionSCE) & ", "
            End If
            If sReporta = 0 Then
                strSQL = strSQL & "null, "
            Else
                strSQL = strSQL & Str(sReporta) & ", "
            End If
            If Val(CStr(sInteraccion)) = 0 Then
                strSQL = strSQL & "null, "
            Else
                strSQL = strSQL & Str(sInteraccion) & ", "
            End If
            strSQL = strSQL & PonNull(sEstatus) & ", "
            strSQL = strSQL & Str(sEdificio) & ", "
            strSQL = strSQL & PonNull(sPiso) & ", "
            strSQL = strSQL & PonNull(sSiglas) & ", "
            strSQL = strSQL & PonFecha(sFechaCreacion) & ", "
            strSQL = strSQL & PonFecha(sFechaCancelacion) & ", '"
            strSQL = strSQL & sClaveEntidad & "', null, '"
            strSQL = strSQL & sUsuario & "', "
            strSQL = strSQL & "getdate(),"
            If Val(CStr(sNominaResponsableOrg)) = 0 Then
                strSQL = strSQL & "null)"
            Else
                strSQL = strSQL & Str(sNominaResponsableOrg) & ")"
            End If

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try


    End Sub

    Public Function ObtieneMaximoFolioEntidad() As Integer

        Dim folioEntidad As Integer = 0
        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader

        Try

            strSQL = "SELECT max(folio_entidad) FROM entidades "
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                datReader.Read()
                If Not IsDBNull(datReader.Item(0)) Then
                    folioEntidad = datReader.Item(0) + 1
                Else
                    folioEntidad = 1
                End If
            End If
            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return folioEntidad

    End Function

    Public Function ObtieneNumeroFolioEntidad(sFolioEntidad As String) As Integer

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim cuenta As Integer

        Try

            strSQL = "SELECT count(*) FROM entidades WHERE entidad = '" & Trim(sFolioEntidad) & "'"
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                datReader.Read()
                cuenta = datReader.Item(0)
            End If
            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return cuenta

    End Function

    Public Function ObtieneEntidad(sFolioEntidad As String) As DataTable

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable


        Try

            strSQL = "SELECT entidad, nombre_entidad, nivel_estructura, " & "nomina_responsable, responsable_organigrama, contable, depto, " & "centro_costo, depto_sce, coordinacion_sce, " & "reporta, interaccion, status, edificio, piso, siglas, "
            strSQL = strSQL & "convert(char(8),fecha_creacion,112) as FCreacion, " & "convert(char(8),fecha_cancelacion,112) as FCancelacion " & " FROM entidades WHERE folio_entidad=" & Str(sFolioEntidad)

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

    Public Function ObtieneEmpleado(sNomina As String) As DataTable

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable


        Try

            strSQL = "SELECT apellido_paterno, apellido_materno, nombre " & " FROM empleados WHERE nomina=" & Str(sNomina)
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

    Public Function ObtieneNumeroEntidades(sFolioEntidad As String, sEntidad As String) As Integer

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim cuenta As Integer

        Try

            strSQL = "SELECT count(*) FROM entidades WHERE entidad = '" & sEntidad & "' AND folio_entidad <> " & Str(sFolioEntidad)
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                datReader.Read()
                cuenta = datReader.Item(0)
            End If
            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return cuenta

    End Function

    Public Function ObtieneNumeroEstructura(sDepartamento As String) As Integer

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim cuenta As Integer

        Try

            strSQL = "SELECT count(*) FROM estructura WHERE depto = '" & sDepartamento & "'"
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                datReader.Read()
                cuenta = datReader.Item(0)
            End If
            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return cuenta

    End Function

    Public Function ObtieneNumeroPlantillas(sFolioEntidad As String) As Integer

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim cuenta As Integer

        Try

            strSQL = "SELECT count(*) FROM plantillas WHERE folio_entidad=" & sFolioEntidad
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                datReader.Read()
                cuenta = datReader.Item(0)
            End If
            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return cuenta

    End Function

    Public Function ObtieneNumeroHistoricoPlantillas(sFolioEntidad As String) As Integer

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim cuenta As Integer

        Try

            strSQL = "SELECT count(*) FROM hst_plantillas WHERE folio_entidad=" & sFolioEntidad
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                datReader.Read()
                cuenta = datReader.Item(0)
            End If
            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return cuenta

    End Function

    Public Sub EliminaEntidad(sFolioEntidad As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "DELETE FROM entidades WHERE folio_entidad = " & sFolioEntidad
            objDat.EjecutarComandoDelete(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try


    End Sub

    Public Function ObtieneNivelesEstructura() As List(Of String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim Texto As String = String.Empty
        Dim result As New List(Of String)

        Try

            strSQL = "SELECT nivel_estructura, descripcion FROM niveles_estructura ORDER BY nivel_estructura "
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                Do While datReader.Read
                    Texto = datReader.Item("nivel_estructura") & " - "
                    Texto = Texto & datReader.Item("descripcion")

                    result.Add(Texto)
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

        Return result

    End Function

    Public Function ObtieneEdificios() As List(Of String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim Texto As String
        Dim result As New List(Of String)

        Try

            strSQL = "SELECT edificio, nombre FROM edificios ORDER by edificio "
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                Do While datReader.Read

                    Texto = datReader.Item("edificio") & " - "
                    Texto = Texto & datReader.Item("nombre")

                    result.Add(Texto)
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

        Return result

    End Function



End Class
