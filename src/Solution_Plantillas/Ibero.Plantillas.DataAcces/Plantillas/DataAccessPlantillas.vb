Imports System.Data.SqlClient
Imports Ibero.Plantillas.Entities
Imports LibreriaUIA.Funciones
Imports FrameWork
Imports System.Text

Public Class DataAccessPlantillas
#Region "Propiedades Privadas"

    Private strConexion As String = UsuarioConexion.strConexion
    Public TB As String = Chr(9)
    Private Const SP_CONSULTA_SUPLENCIAS As String = "sp_Consulta_Suplencias"

#End Region
#Region "Observaciones"

    Public Function CargaObervaciones(iNumeroPlaza As Integer) As List(Of String)

        Dim strTexto As String
        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim listObservaciones As New List(Of String)
        Const REGISTRO_HISTORICO As Short = 1

        Try
            strSQL = "SELECT * FROM plantillas_observaciones WHERE num_plaza = " & iNumeroPlaza & " ORDER BY fch_audit"
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                Do While datReader.Read
                    strTexto = datReader.Item("observacion") & TB
                    strTexto &= datReader.Item("usr_audit") & TB
                    strTexto &= datReader.Item("fch_audit") & TB
                    strTexto &= REGISTRO_HISTORICO

                    listObservaciones.Add(strTexto)
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

        Return listObservaciones

    End Function

    Public Sub ActualizaObservaciones(sObservaciones As String, sUsuario As String, iNumeroPlaza As Integer, sFecha As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try
            strSQL = "UPDATE plantillas_observaciones SET observacion = '" & sObservaciones & "', " & "usr_audit = '" & sUsuario & "' " & "WHERE num_plaza = " & iNumeroPlaza & " AND " & "fch_audit = '" & sFecha & "'"
            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Sub EliminaObservaciones(iNumeroPlaza As Integer, sFecha As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try
            strSQL = "DELETE FROM plantillas_observaciones WHERE num_plaza = " & iNumeroPlaza & " AND " & "fch_audit = '" & sFecha & "'"
            objDat.EjecutarComandoDelete(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Sub AgregaObservaciones(sObservaciones As String, sUsuario As String, iNumeroPlaza As Integer, sFecha As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try
            strSQL = "INSERT INTO plantillas_observaciones (num_plaza,observacion, usr_audit, fch_audit) VALUES (" & iNumeroPlaza & ",'" & sObservaciones & "','" & sUsuario & "','" & sFecha & "')"
            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

#End Region

#Region "Consultas Sabaticos"

    Public Function ObtieneHistoricoSabaticos() As List(Of String)

        Dim strTexto As String
        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim listResult As New List(Of String)
        Dim datReader As SqlDataReader

        Try

            strSQL = "SELECT DISTINCT e.nomina, e.apellido_paterno + ' ' + e.apellido_materno + ' ' + e.nombre as empleado, num_plaza, p.puesto, nombre_puesto, entidad, nombre_entidad "
            strSQL = strSQL & " FROM hst_sabaticos s INNER JOIN empleados e ON e.nomina = s.nomina INNER JOIN plantillas p ON p.nomina_ocupante = e.nomina INNER JOIN puestos u ON u.puesto = p.puesto "
            strSQL = strSQL & " INNER JOIN entidades n ON n.folio_entidad = p.folio_entidad WHERE motivo_baja = 0 AND fecha_final <= getdate() "
            strSQL = strSQL & " AND condicion = 6 AND e.nomina NOT IN (SELECT nomina FROM hst_sabaticos WHERE fecha_final > getdate()) ORDER BY e.nomina"

            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                Do While datReader.Read
                    strTexto = datReader.Item("nomina") & TB
                    strTexto &= datReader.Item("empleado") & TB
                    strTexto &= ObtenerFechaFinal(datReader.Item("nomina").ToString.Trim) & TB
                    strTexto &= datReader.Item("num_plaza") & TB
                    strTexto &= datReader.Item("Puesto") & TB
                    strTexto &= datReader.Item("nombre_puesto") & TB
                    strTexto &= datReader.Item("entidad") & TB
                    strTexto &= datReader.Item("nombre_entidad") & TB

                    listResult.Add(strTexto)
                Loop

            End If

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return listResult
    End Function

    Public Function ObtenerFechaFinal(ByVal sNomina As String) As String

        Dim strValor As String
        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try
            strSQL = "SELECT TOP 1 fecha_final FROM hst_sabaticos WHERE nomina = " & sNomina & " ORDER BY fecha_final desc"
            strValor = objDat.EjecutarComandoEscalar(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return strValor
    End Function

    Public Function ActualizaCondicion(sUsuario As String, sNumeroPlaza As String) As Boolean

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        ActualizaCondicion = False

        Try
            strSQL = "UPDATE plantillas SET condicion = 2, usr_audit = '" & sUsuario & "', fch_audit = getdate() WHERE num_plaza = " & sNumeroPlaza
            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)
            ActualizaCondicion = True

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return ActualizaCondicion

    End Function

#End Region

#Region "Consulta Plantillas"
    Public Function CargaPlantilla(sNumeroPlaza As String) As DataTable

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable

        Try
            strSQL = "SELECT a.num_plaza,a.nomina_ocupante, b.apellido_paterno, b.apellido_materno, b.nombre, a.puesto, c.nombre_puesto, d.nombre_entidad , C.Status,  b.categoria,e.descripcion, a.nivel, a.plaza_reporta "
            strSQL &= "FROM plantillas a, empleados b, puestosx c, entidades d, tipos_periodos e "
            strSQL &= "Where a.nomina_ocupante = b.nomina And a.Puesto = C.Puesto And a.folio_entidad = d.folio_entidad AND c.sector = e.tipo_periodo AND a.num_plaza = " & sNumeroPlaza

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

    Public Function CargaPlantilla(sFolioEntidad As String, sNivel As String) As DataTable

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable

        Try
            strSQL = "SELECT a.num_plaza, a.nomina_ocupante, b.apellido_paterno, b.apellido_materno, b.nombre, a.puesto, c.nombre_puesto, d.nombre_entidad, c.status, b.categoria, e.descripcion, a.nivel, a.plaza_reporta "
            strSQL &= "FROM plantillas a, empleados b, puestosx c, entidades d, tipos_periodos e "
            strSQL &= "WHERE a.nomina_ocupante = b.nomina AND a.puesto = c.puesto AND a.folio_entidad = d.folio_entidad AND c.sector = e.tipo_periodo AND a.folio_entidad = " & sFolioEntidad & " AND a.nivel = " & sNivel & " ORDER BY a.nivel"

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

    Public Function ObtieneNiveles(sFolioEntidad As String) As List(Of String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim listNiveles As New List(Of String)

        Try

            strSQL = "SELECT distinct nivel FROM plantillas WHERE folio_entidad = " & sFolioEntidad & " ORDER BY nivel ASC"
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                Do While datReader.Read
                    listNiveles.Add(datReader.Item(0))
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

        Return listNiveles
    End Function

    Public Function CargaSueldo(sNomina As String) As Double

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        CargaSueldo = 0

        Try
            strSQL = "SELECT sueldo * .01 FROM empleados WHERE nomina = " & Str(sNomina)
            CargaSueldo = objDat.EjecutarComandoEscalar(strSQL, Parametros)
        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return CargaSueldo

    End Function

#End Region

#Region "Carga Reportes"
    Public Function CargaCombos(sCombo As String) As List(Of String)

        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim strSQL As String = String.Empty
        Dim resultList As New List(Of String)

        Try
            If sCombo = "Entidades" Then


                strSQL = "SELECT ISNULL(depto,'') depto, nombre_entidad "
                strSQL &= "FROM entidades "
                strSQL &= "ORDER BY nombre_entidad "
                datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

                If datReader.HasRows Then
                    Do While datReader.Read
                        resultList.Add(datReader.Item(1).ToString & Space(100) & "|" & datReader.Item(0).ToString)
                    Loop
                End If
                datReader.Close()
            Else

                strSQL = "SELECT tipo_nomina, descripcion "
                strSQL &= "FROM tipos_nominas "
                strSQL &= "ORDER BY descripcion "
                datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
                If datReader.HasRows Then
                    Do While datReader.Read
                        resultList.Add(datReader.Item(1).ToString & Space(100) & "|" & datReader.Item(0).ToString)
                    Loop
                End If
                datReader.Close()

            End If

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return resultList

    End Function
#End Region

#Region "Captura Plazas"
    Public Function ObtieneCondicionesPlantillas() As List(Of String)

        Dim strTexto As String = String.Empty
        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim condicionesList As New List(Of String)

        Try
            strSQL = "SELECT condicion, descripcion FROM condicion_plantillas WHERE condicion not in (5)"
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                Do While datReader.Read
                    strTexto = "00" & datReader.Item("condicion") & " -"
                    strTexto = strTexto & Space(6 - Len(strTexto))
                    strTexto = strTexto & datReader.Item("descripcion").ToString.Trim

                    condicionesList.Add(strTexto)
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

        Return condicionesList

    End Function

    Public Sub EliminaPlantillas(sNumeroPlaza As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try
            strSQL = "DELETE FROM hst_plantillas WHERE num_plaza = " & sNumeroPlaza
            objDat.EjecutarComandoDelete(strSQL, Parametros)

            strSQL = "DELETE FROM plantillas WHERE num_plaza = " & sNumeroPlaza
            objDat.EjecutarComandoDelete(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Sub ActualizaPlantillas(sNumeroPlaza As String, sUsuario As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "UPDATE plantillas SET estatus = 1, usr_audit = '" & sUsuario & "', fch_audit = getdate() WHERE num_plaza = " & sNumeroPlaza
            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Sub ActualizaPlantillas(sFolioEntidad As String, sPuesto As String, sFechaCreacion As String, sFechaCancelacion As String, sTipoPlaza As String, sPlazaReporta As String, sInteriorExterior As String, sCondicion As String _
                                   , sNivelPlaza As String, sUsuario As String, sNumeroPlaza As String, bCondicional As Boolean)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "UPDATE plantillas SET " & "folio_entidad = " & sFolioEntidad & ", " & "puesto = '" & sPuesto & "', " & "fecha_creacion = " & sFechaCreacion & ", "
            strSQL &= " fecha_cancelacion = " & sFechaCancelacion & ", " & "tipo_plaza = " & sTipoPlaza & ", " & "plaza_reporta = " & sPlazaReporta & ", " & "int_ext = " & sInteriorExterior & ", "
            strSQL &= " condicion = " & sCondicion & ", " & "nivel = " & sNivelPlaza & ", " & "usr_audit = '" & sUsuario & "', " & "fch_audit = getdate()"

            If bCondicional Then
                strSQL = strSQL & ", estatus = 0"
            End If
            strSQL = strSQL & " WHERE num_plaza = " & sNumeroPlaza

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Sub ActualizaPlantillas(sFolioEntidad As String, sPlazaReporta As String, iNominaOcupante As Integer, iNominaSuplente As Integer, bDisponible As Boolean, sEstatusPlaza As String, sCondicionPlaza As String, sUusuario As String, sNumeroPlaza As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "UPDATE plantillas SET "
            strSQL = strSQL & "folio_entidad = " & sFolioEntidad & ", "
            strSQL = strSQL & "plaza_reporta = " & sPlazaReporta & ", "

            If iNominaOcupante = 0 Then
                strSQL = strSQL & "nomina_ocupante = null, "
            Else
                strSQL = strSQL & "nomina_ocupante = " & Str(iNominaOcupante) & ", "
            End If

            If iNominaSuplente = 0 Then
                strSQL = strSQL & "nomina_suplente = null, "
            Else
                strSQL = strSQL & "nomina_suplente = " & Str(iNominaSuplente) & ", "
            End If

            If bDisponible Then
                strSQL = strSQL & "ConMovto = 1, "
            Else
                strSQL = strSQL & "ConMovto = 0, "
            End If

            strSQL = strSQL & "estatus = " & Str(sEstatusPlaza) & ", "
            strSQL = strSQL & "condicion = " & Str(sCondicionPlaza) & ", "
            strSQL = strSQL & "usr_audit = '" & sUusuario & "', "
            strSQL = strSQL & "fch_audit = getdate() "
            strSQL = strSQL & "WHERE num_plaza = " & sNumeroPlaza
            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Sub InsertaPlantillas(sFolioEntidad As String, sPuesto As String, sFechaCreacion As String, sFechaCancelacion As String, sTipoPlaza As String, sPlazaReporta As String, sInteriorExterior As String, sCondicion As String _
                                   , sNivelPlaza As String, sUsuario As String, sNumeroPlaza As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "INSERT INTO plantillas VALUES (" & sNumeroPlaza & ", " & sFolioEntidad & ", '" & sPuesto & "', " & sCondicion & ", " & sFechaCreacion & ", " & sFechaCancelacion & ", " & sTipoPlaza
            strSQL &= ", null, null, " & sPlazaReporta & ", " & sInteriorExterior & ", " & sNivelPlaza & ", 0, '" & sUsuario & "', getdate(), 0)"

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Function CargaNombrePuesto(ByRef Repo As Integer) As String

        Dim TextoTemp As String = String.Empty
        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader

        Try
            strSQL = "SELECT b.nombre_puesto FROM plantillas a, puestosx b WHERE a.puesto = b.puesto AND a.num_plaza = " & Str(Repo)
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                datReader.Read()
                TextoTemp = datReader.Item("nombre_puesto").ToString.Trim

            End If

            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return TextoTemp
    End Function

    Public Function ObtieneMaxNumPlaza() As Integer

        Dim TextoTemp As String = String.Empty
        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim NumPlaza As Integer = 0

        Try
            strSQL = "SELECT max(num_plaza) FROM plantillas "
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                datReader.Read()
                NumPlaza = datReader.Item(0)
                NumPlaza = NumPlaza + 1
            Else
                NumPlaza = 1
            End If

            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return NumPlaza

    End Function

    ''' <summary>
    ''' Opcion 1,4,7
    ''' </summary>
    ''' <param name="sNumeroPlaza"></param>
    ''' <param name="sSecuencia"></param>
    ''' <param name="sFolioEntidad"></param>
    ''' <param name="sUsuario"></param>
    ''' <param name="sOpcion"></param>
    Public Sub InsertHistoricoPlantilla(sNumeroPlaza As String, sSecuencia As String, sFolioEntidad As String, sUsuario As String, sOpcion As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try
            strSQL = "INSERT INTO hst_plantillas VALUES(" & sNumeroPlaza & ", " & sSecuencia & ", " & "getdate(), null, 1, " & sFolioEntidad & ", null, null, null, null, null, null, null, null, " & "null, '" & sUsuario & "', getdate())"

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub

    ''' <summary>
    ''' Opcion 2,5,8
    ''' </summary>
    ''' <param name="sNumeroPlaza"></param>
    ''' <param name="sSecuencia"></param>
    ''' <param name="sPuesto"></param>
    ''' <param name="sUsuario"></param>
    Public Sub InsertHistoricoPlantilla(sNumeroPlaza As String, sSecuencia As String, sPuesto As String, sUsuario As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try
            strSQL = "INSERT INTO hst_plantillas VALUES(" & sNumeroPlaza & ", " & sSecuencia & ", " & "getdate(), null, 2, null, '" & sPuesto & "', null, null, null, null, null, null, null, " & "null, '" & sUsuario & "', getdate())"

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub

    ''' <summary>
    ''' Opcion 3,6,9
    ''' </summary>
    ''' <param name="sNumeroPlaza"></param>
    ''' <param name="sSecuencia"></param>
    ''' <param name="sSueldoT"></param>
    ''' <param name="sSobreSueltoT"></param>
    ''' <param name="sOtrasRetenciones"></param>
    ''' <param name="sAyudaMovilidad"></param>
    ''' <param name="sGastoOperaciones"></param>
    ''' <param name="sUsuario"></param>
    Public Sub InsertHistoricoPlantilla(sNumeroPlaza As String, sSecuencia As String, sSueldoT As String, sSobreSueltoT As String, sOtrasRetenciones As String, sAyudaMovilidad As String, sGastoOperaciones As String, sUsuario As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "INSERT INTO hst_plantillas VALUES(" & sNumeroPlaza & ", " & sSecuencia & ", " & "getdate(), null, 3, null, null, null, " & "null, null, " & sSueldoT & ", " & sSobreSueltoT & ", " & sOtrasRetenciones & ", " & sAyudaMovilidad & ", " & sGastoOperaciones & ", '" & sUsuario & "', getdate())"
            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub

    Public Function FoliosCount(sNumeroPlaza As String) As Integer

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim intCount As Integer = 0

        Try

            strSQL = "SELECT count(folio_entidad) FROM hst_plantillas WHERE num_plaza = " & Str(sNumeroPlaza)
            intCount = objDat.EjecutarComandoEscalar(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return intCount

    End Function

    Public Function PuestosCount(sNumeroPlaza As String) As Integer

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim intCount As Integer = 0

        Try

            strSQL = "SELECT count(puesto) FROM hst_plantillas WHERE num_plaza = " & sNumeroPlaza
            intCount = objDat.EjecutarComandoEscalar(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return intCount

    End Function

    Public Sub ActualizaHistoricoPlantilla(sSecuenciaDepto As String, sNumeroPlaza As String, sTipoHistoria As Integer)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try
            strSQL = "UPDATE hst_plantillas SET fecha_final = DATEADD(day,-1,getdate()) WHERE secuencia = " & sSecuenciaDepto & " AND num_plaza = " & sNumeroPlaza & " AND tipo_historia = " & sTipoHistoria
            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Function ObtieneSueldos(sPuesto As String, sTabulador As String, sNivel As String, sTipo As String) As DataTable

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable

        Try
            strSQL = "SELECT distinct c.sueldo, c.sobre_sueldo, c.otra_remuneracion, c.ayuda_movilidad, c.gto_operacion "
            strSQL &= "FROM puestosx a, tabuladores_puestos b, cat_tabuladores c "
            strSQL &= "WHERE a.puesto = b.puesto AND b.tabulador = c.tabulador  AND c.nombre = '" & sTabulador & "' and c.nivel=" & sNivel & " and c.tipo=" & sTipo

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

    Public Function ObtienePuestos(sFoliosEntidad As String) As DataTable

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable

        Try

            strSQL = "SELECT a.num_plaza, a.puesto, b.nombre_puesto, a.condicion, c.descripcion, a.fecha_creacion, ISNULL(a.fecha_cancelacion, '') fecha_cancelacion, a.tipo_plaza, ISNULL(a.nomina_ocupante, 0) nomina_ocupante, "
            strSQL &= "  a.nomina_suplente, ISNULL(a.plaza_reporta,0) plaza_reporta, a.int_ext, a.estatus  FROM plantillas a, puestosx b, condicion_plantillas c "
            strSQL &= "  WHERE a.folio_entidad = " & sFoliosEntidad & " AND a.puesto = b.puesto AND a.condicion = c.condicion ORDER BY b.nombre_puesto "

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

    Public Function ObtieneNivel(sPlazaReporta) As List(Of String)

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim list As New List(Of String)

        Try

            strSQL = "SELECT num_plaza, nivel, folio_entidad FROM plantillas " & " WHERE num_plaza=" & sPlazaReporta

            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                datReader.Read()
                list.Add(datReader.Item("nivel"))
                list.Add(datReader.Item("folio_entidad"))
            End If

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return list

    End Function

    Public Function CargaSecuencia(sNumeroPlaza As String) As Short

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader

        Try
            strSQL = "SELECT isnull(max(secuencia),1) FROM hst_plantillas "
            strSQL &= "Where num_plaza = " & sNumeroPlaza

            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                datReader.Read()
                If Not IsDBNull(datReader.Item(0)) Then
                    CargaSecuencia = datReader.Item(0)
                Else
                    CargaSecuencia = 0
                End If
            Else
                CargaSecuencia = 0
            End If

            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Function

    Public Function CargaSecuenciaPlantilla(sNumeroPlaza As String) As DataTable

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable

        Try
            strSQL = "SELECT secuencia, tipo_historia FROM hst_plantillas "
            strSQL &= "Where num_plaza = " & sNumeroPlaza & " AND tipo_historia in (1,2,3) AND fecha_final is null"
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

    Public Function ExtraerDatosBD(ByVal intEID As Integer) As DataTable

        Dim strSQL As String = ""
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable

        Try

            strSQL = "SELECT a.IDSolicitud, a.Plazas_solicitadas, a.Numero_plazas, a.Sector, a.Estatus, a.Entidad, b.nombre_entidad, b.centro_costo, a.IDHorario, a.Justificación1, a.Justificación2, a.Justificación3,  a.IDEscolaridad, a.Experiencia, a.Conocimientos_tecnicos, "
            strSQL &= " a.Conocimientos_espeficicos, CONVERT(VARCHAR(10), a.Fecha_Creacion,103) AS Fecha_Creacion, ISNULL(Autorizada,0) Autorizada, observaciones"
            strSQL &= " FROM Solicitudes_Plazas a WITH(NOLOCK) INNER JOIN entidades b WITH(NOLOCK) ON a.Entidad = b.entidad "
            strSQL &= " WHERE a.IDSolicitud =" & intEID

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

    Public Function LlenarGridSolicitudesPlazas(ByVal Conexion As String, ByVal strEntidad As String, ByRef sentencia As String) As Collection

        Dim objConstancias As New Collection
        Dim objDat As New Dat(Conexion)
        Dim Parametros As New Collection
        Dim sSQL As New StringBuilder


        sSQL.Append("SELECT a.IDSolicitud, a.Plazas_solicitadas, a.Numero_plazas, a.Sector, a.Estatus, a.Entidad, b.nombre_entidad, b.centro_costo, a.IDHorario, a.Justificación1, a.Justificación2, a.Justificación3,  a.IDEscolaridad, a.Experiencia, a.Conocimientos_tecnicos, a.Conocimientos_espeficicos, CONVERT(VARCHAR(10), a.Fecha_Creacion,103) AS Fecha_Creacion ")
        sSQL.Append("FROM Solicitudes_Plazas a WITH(NOLOCK) INNER JOIN entidades b WITH(NOLOCK) ON a.Entidad = b.entidad ")
        sSQL.Append("WHERE a.Entidad LIKE '%" & strEntidad & "%'")

        sentencia = sSQL.ToString

        Try
            Using Lector As SqlDataReader = objDat.EjecutarComandoReader(sSQL.ToString, Parametros)
                While Lector.Read
                    objConstancias.Add(New SolicitudesPlazas With {.IDSolicitud = Lector("IDSolicitud"), .Plazas_solicitadas = Lector("Plazas_solicitadas"),
                         .Numero_plazas = Lector("Numero_plazas").ToString, .Sector = Lector("Sector").ToString,
                         .Estatus = Lector("Estatus").ToString, .Entidad = Lector("Entidad").ToString, .nombre_entidad = Lector("nombre_entidad").ToString,
                         .CentroCosto = Lector("centro_costo").ToString, .IDHorario = Lector("IDHorario"), .Justificación1 = Lector("Justificación1").ToString, .Justificación2 = Lector("Justificación2").ToString,
                         .Justificación3 = Lector("Justificación3").ToString, .IDEscolaridad = Lector("IDEscolaridad"), .Experiencia = Lector("Experiencia").ToString,
                         .Conocimientos_tecnicos = Lector("Conocimientos_tecnicos").ToString, .Conocimientos_espeficicos = Lector("Conocimientos_espeficicos").ToString, .FechaCreacion = Lector("Fecha_Creacion").ToString}, Lector("IDSolicitud"))
                End While
            End Using

            Return objConstancias

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & sSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return objConstancias
    End Function

    Public Function MaximoSolicitudesPlazas() As Integer

        Dim objDat1 As New Dat(strConexion)
        Dim objReader As SqlClient.SqlDataReader
        Dim Parametros As New Collection
        Dim strSQL As String = ""
        Dim intID As Integer

        Try
            strSQL = " SELECT ISNULL(MAX(IDSolicitud),0) + 1 AS IDSolicitud FROM Solicitudes_Plazas WITH(NOLOCK) "

            objReader = objDat1.EjecutarComandoReader(strSQL, Parametros)
            If objReader.HasRows Then
                While objReader.Read
                    intID = CInt(objReader.Item(0))
                End While
            End If
            objReader.Close()
            objDat1.Dispose()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat1.Dispose()
        End Try

        Return intID

    End Function

    Public Function Registro(ByVal intID As Integer, ByVal strNumEntidad As String, ByVal intPlazas As Integer, ByVal strPlazas As String, ByVal strSector As String, ByVal strEstatus As String, ByVal intHorario As Integer _
                              , ByVal strJustificacion1 As String, ByVal strJustificacion2 As String, ByVal strJustificacion3 As String, ByVal intEscolaridad As Integer, ByVal strExperiencia As String, ByVal strConocimientosTec As String _
                              , ByVal strConocimientosEsp As String, ByVal strUserApp As String, ByVal strFechaCreacion As String, ByVal sObservaciones As String) As Boolean

        Dim Parametros As New Collection
        Dim sSQL As New StringBuilder
        Dim objDat As New Dat(strConexion)

        Try
            Registro = False

            sSQL.AppendLine("")
            sSQL.AppendLine("Exec spI_SolicitudesPlazas ")
            sSQL.AppendLine(intID & ", ")
            sSQL.AppendLine("'" & strNumEntidad & "', ")
            sSQL.AppendLine(intPlazas & ", ")
            sSQL.AppendLine("'" & strPlazas & "', ")
            sSQL.AppendLine("'" & strSector & "', ")
            sSQL.AppendLine("'" & strEstatus & "', ")
            sSQL.AppendLine(intHorario & ", ")
            sSQL.AppendLine("'" & strJustificacion1 & "', ")
            sSQL.AppendLine("'" & strJustificacion2 & "', ")
            sSQL.AppendLine("'" & strJustificacion3 & "', ")
            sSQL.AppendLine(intEscolaridad & ", ")
            sSQL.AppendLine("'" & strExperiencia & "', ")
            sSQL.AppendLine("'" & strConocimientosTec & "', ")
            sSQL.AppendLine("'" & strConocimientosEsp & "', ")
            sSQL.AppendLine("'" & strUserApp & "', ")
            sSQL.AppendLine("'" & strFechaCreacion & "',")
            sSQL.AppendLine("'" & sObservaciones & "'")

            objDat.EjecutarComandoInsertUpdate(sSQL.ToString, Parametros)

            Registro = True

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & sSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Function


#End Region

#Region "Captura Plantillas"

    Public Function ObtenUltimaObservacion(ByVal iNumeroPlaza As Integer) As String

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim ultimaObservacion As String = String.Empty

        Try
            strSQL = "SELECT TOP 1 observacion FROM plantillas_observaciones WHERE num_plaza = " & iNumeroPlaza & " ORDER BY fch_audit desc"
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                datReader.Read()
                ultimaObservacion = datReader.Item("observacion").ToString.Trim
            End If
            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return ultimaObservacion

    End Function

    Public Function ObtieneNombreEmpleado(sNomina) As String

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim TextoTemp As String = String.Empty

        Try
            strSQL = "SELECT apellido_paterno, apellido_materno, nombre FROM empleados WHERE nomina = " & sNomina
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                datReader.Read()
                TextoTemp = datReader.Item(0).ToString.Trim & " "
                TextoTemp = TextoTemp & datReader.Item(1).ToString.Trim & " "
                TextoTemp = TextoTemp & datReader.Item(2).ToString.Trim & " "
            End If

            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return TextoTemp

    End Function

    Public Function CargaMotivoSuplencia(ByVal lNumeroPlaza As Long) As String

        Dim TextoT As String = String.Empty
        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader

        Try
            CargaMotivoSuplencia = String.Empty

            strSQL = "SELECT b.motivo ,descripcion FROM motivos_plantillas a, hst_plantillas b, "
            strSQL &= " plantillas c WHERE c.num_plaza = " & Str(lNumeroPlaza)
            strSQL &= " AND b.num_plaza = c.num_plaza "
            strSQL &= " AND b.motivo = a.motivo "
            strSQL &= " AND b.tipo_historia = 5 "
            strSQL &= " AND b.fecha_final is null "

            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                datReader.Read()
                TextoT = datReader.Item("motivo") & TB
                TextoT = TextoT & datReader.Item("descripcion")
            End If

            datReader.Close()
            CargaMotivoSuplencia = TextoT

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Function

    Public Function _CargaPlantilla(sFolioEntidad As String, iOpcion As Integer) As DataTable

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable

        Try
            strSQL = "SELECT a.num_plaza, a.puesto, b.nombre_puesto, a.condicion, "
            strSQL &= " c.descripcion, a.fecha_creacion, "
            strSQL &= " a.nomina_ocupante, a.nomina_suplente, a.plaza_reporta, a.estatus, a.ConMovto "
            strSQL &= " FROM plantillas a, puestosx b, condicion_plantillas c "
            strSQL &= " WHERE a.folio_entidad = " & sFolioEntidad
            strSQL &= " AND a.puesto = b.puesto AND a.condicion = c.condicion "


            If iOpcion = 0 Then
                strSQL &= " AND a.int_ext = 0 And a.condicion <> 5 "
            ElseIf iOpcion = 1 Then
                strSQL &= " AND a.int_ext = 1 And a.condicion <> 5 "
            ElseIf iOpcion = 2 Then
                strSQL &= " And a.condicion = 5 "
            End If
            strSQL = strSQL & " ORDER BY b.nombre_puesto "

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

    Public Sub ActualizaInsertaPlantilla(sSecuencia As String, sNumeroPlaza As String, sSeqOcupante As String, sTitular As String, sTipoHistoria As String, uUsuario As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "UPDATE hst_plantillas SET fecha_final = getdate() "
            strSQL &= " WHERE secuencia = " & sSeqOcupante
            strSQL &= " AND num_plaza = " & sNumeroPlaza
            strSQL &= " AND tipo_historia = " & sTipoHistoria
            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

            strSQL = "INSERT INTO hst_plantillas VALUES ( "
            strSQL &= sNumeroPlaza & ", "
            strSQL &= sSecuencia & ", "
            strSQL &= "getdate(), null," & sTipoHistoria & ", null, null, "
            strSQL &= sTitular & ", null, null, null, null, null, "
            strSQL &= "null, null, '"
            strSQL &= uUsuario & "', getdate())"
            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Function CargaSueldoEmpleado(sNomina As String) As List(Of String)

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim result As New List(Of String)

        Try

            strSQL = "SELECT sueldo, compl_sueldo_me FROM empleados WHERE nomina = " & sNomina
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                datReader.Read()
                result.Add(datReader.Item("sueldo"))
                result.Add(datReader.Item("compl_sueldo_me"))
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

    Public Function ObtieneSueldosEmpleados(sNomina As String) As DataTable

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable

        Try
            strSQL = " SELECT renglon, saldo FROM movimientos WHERE nomina = " & sNomina & " AND renglon in (19,60,79)"
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

    Public Function ObtieneMotivos() As List(Of String)

        Dim strstrTexto As String = String.Empty
        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim motivosList As New List(Of String)

        Try
            strSQL = "SELECT motivo, descripcion FROM motivos_plantillas ORDER BY motivo"
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                Do While datReader.Read
                    strstrTexto = Format(Val(datReader.Item("motivo")), "00#") & " -"
                    strstrTexto = strstrTexto & Space(6 - Len(strstrTexto))
                    strstrTexto = strstrTexto & datReader.Item("descripcion")

                    motivosList.Add(strstrTexto)
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

        Return motivosList

    End Function

    Public Function ObtieneSector(sNumeroPlaza As String) As String

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim sector As String = String.Empty

        Try

            strSQL = "SELECT sector FROM puestosx a, plantillas b "
            strSQL &= " WHERE a.puesto = b.puesto and b.num_plaza = " & sNumeroPlaza
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                datReader.Read()
                sector = datReader.Item("sector")
            End If
            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return sector

    End Function

    Public Function ObtieneNumeroPlaza(sNomina As String, sPuesto As String, sFolioEntidad As String) As String

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim plaza As String = String.Empty

        Try

            strSQL = "SELECT num_plaza FROM plantillas WHERE (nomina_ocupante=" & sNomina
            strSQL &= " OR nomina_suplente = " & sNomina & ") "
            strSQL &= " AND puesto = '" & sPuesto & "' AND folio_entidad = " & sFolioEntidad
            strSQL &= " AND estatus = 0"
            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                datReader.Read()
                plaza = datReader.Item(0)
            End If
            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return plaza

    End Function

    Public Function ObtienePlazaEmpleado(sNomina As String) As String

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim TxtLinea As String = String.Empty
        Dim TxtMensaje As String = String.Empty

        Try
            strSQL = "SELECT a.num_plaza, b.nombre_puesto, c.nombre_entidad "
            strSQL &= " FROM plantillas a, puestosx b, entidades c "
            strSQL &= " WHERE (nomina_ocupante = " & sNomina
            strSQL &= " OR nomina_suplente = " & sNomina & ") AND a.puesto = b.puesto AND a.folio_entidad = c.folio_entidad AND a.estatus = 0"

            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                Do While datReader.Read

                    TxtLinea = Space(6 - Len(datReader.Item("num_plaza"))) & datReader.Item("num_plaza") & " "

                    If Len(datReader.Item("nombre_puesto")) < 40 Then
                        TxtLinea = TxtLinea & datReader.Item("nombre_puesto") & " "
                    Else
                        TxtLinea = TxtLinea & Mid(datReader.Item("nombre_puesto"), 1, 40) & " "
                    End If

                    TxtLinea = TxtLinea & Space(48 - Len(TxtLinea))
                    TxtLinea = TxtLinea & datReader.Item("nombre_entidad") & vbCrLf
                    TxtMensaje = TxtMensaje & TxtLinea

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

        Return TxtMensaje

    End Function

    Public Function ObtieneNumeroPlantillas(sNumeroPlaza As String) As Integer

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim CValida As Integer = 0

        Try
            strSQL = "SELECT count(*) FROM plantillas WHERE plaza_reporta=" & sNumeroPlaza
            CValida = objDat.EjecutarComandoEscalar(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return CValida

    End Function

    Public Function NominaSuplenteCount(sNumeroPlaza As String) As Integer

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim intCount As Integer = 0

        Try

            strSQL = "SELECT count(nomina_suplente) FROM hst_plantillas WHERE num_plaza = " & sNumeroPlaza
            intCount = objDat.EjecutarComandoEscalar(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return intCount

    End Function

    Public Function NominaOcupanteCount(sNumeroPlaza As String) As Integer

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim intCount As Integer = 0

        Try

            strSQL = "SELECT count(nomina_ocupante) FROM hst_plantillas WHERE num_plaza = " & sNumeroPlaza
            objDat.EjecutarComandoEscalar(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return intCount

    End Function

    Public Sub InsertaPlantillasNominaSuplente(sNumeroPlaza As String, sSecuencia As String, sNominaSuplente As String, sMSuplente As String, sUsuario As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try
            strSQL = "INSERT INTO hst_plantillas VALUES ( "
            strSQL &= Str(sNumeroPlaza) & ", "
            strSQL &= Str(sSecuencia) & ", "
            strSQL &= "getdate(), null, 5, null, null, null, "
            strSQL &= sNominaSuplente & ", " & sMSuplente & ", null, null, null, null, "
            strSQL &= "null, '" & sUsuario & "', getdate())"

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Sub InsertaPlantillasNominaOcupante(sNumeroPlaza As String, sSecuencia As String, sNominaOcupante As String, sUsuario As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try
            strSQL = "INSERT INTO hst_plantillas VALUES ( "
            strSQL &= sNumeroPlaza & ", "
            strSQL &= sSecuencia & ", "
            strSQL &= "getdate(), null, 4, null, null, "
            strSQL &= sNominaOcupante & ", null, null, null, null, null, "
            strSQL &= "null, null, '"
            strSQL &= sUsuario & "', getdate())"

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Sub ActualizaHistoricoPlantilla(sSecuenciaOcupante As String, sSecuenciaSueldo As String, sNumeroPlaza As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try
            strSQL = "UPDATE hst_plantillas SET fecha_final = DATEADD(day,-1,getdate()) "
            strSQL &= " WHERE secuencia in (" & sSecuenciaOcupante & ", " & sSecuenciaSueldo & ")"
            strSQL &= " AND num_plaza = " & sNumeroPlaza
            strSQL &= " AND tipo_historia in (4,3)"

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub
#End Region

#Region "Suplencias"
    Public Function ConsultaSuplencia(ByVal iNomina As Integer) As DataTable

        Dim strSQL As String = ""
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable

        Try

            strSQL = SP_CONSULTA_SUPLENCIAS & " " & iNomina

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


#End Region


    Public Function LlenarHorarios(iDHorario As Integer) As DataTable

        Dim strSQL As String = ""
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable

        Try

            strSQL = "SELECT horario, hora_ini1, hora_fin1, hora_ini2, hora_fin2 FROM horarios WITH(NOLOCK) "

            If iDHorario >= 0 Then
                strSQL = strSQL & "  WHERE horario = " & iDHorario
            End If

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

    Public Function LlenarHorarios() As DataTable

        Dim strSQL As String = ""
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable

        Try
            'Carga el catálogo de horarios
            strSQL = "SELECT horario, hora_ini1, hora_fin1, hora_ini2, hora_fin2 FROM horarios WITH(NOLOCK)"
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

    Public Function LlenarEscolaridad() As List(Of String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim result As New List(Of String)

        Try

            strSQL = "SELECT CONCAT(nivel_estudios,'-',descripcion) AS Estudio FROM niveles_estudios WITH(NOLOCK) ORDER BY nivel_estudios DESC"

            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                Do While datReader.Read
                    result.Add(datReader.Item(0))
                Loop
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

        Return result
    End Function

    Public Function NivelEstudios(idNivel As Integer) As String

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim strEscolaridad As String = String.Empty

        Try

            strSQL = "SELECT descripcion FROM niveles_estudios WITH(NOLOCK) WHERE nivel_estudios =  " & idNivel

            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                datReader.Read()
                strEscolaridad = datReader.Item("descripcion").ToString
            End If

            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return strEscolaridad

    End Function

    Public Function CargaSueldoTabulador(sPuesto As String, sPlazasSolicitud As String) As DataTable

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim objDat2 As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable

        Try

            'Nota: Depende de la empresa cambia este Query.
            'Ya que los datos de la tabla del tabulador cambia.
            strSQL = "SELECT b.ayuda_educa + b.subsidio_bienes AS Prevision, b.tabulador, b.nivel, b.tipo, b.nombre, "
            strSQL &= "b.sueldo, b.fondo_ahorro_empl, b.vales, b.total, b.gto_operacion AS BN, b.otra_remuneracion AS ORR, b.total AS TT, "
            strSQL &= "b.sobre_sueldo AS SS, ayuda_movilidad AS AM, b.fondo_ahorro_uia "
            strSQL &= "FROM cat_tabuladores b WITH(NOLOCK) "
            strSQL &= "JOIN plantillas  a  "
            strSQL &= "ON a.tabulador = b.nombre AND b.tipo in (0) "
            strSQL &= "WHERE a.num_plaza in(" & sPlazasSolicitud & ")"
            strSQL &= "ORDER BY b.nivel, b.tipo"

            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader.HasRows Then
                dt.Load(datReader)
            Else
                strSQL = "SELECT b.ayuda_educa + b.subsidio_bienes AS Prevision, a.tabulador, a.nivel, b.tipo, b.nombre, a.tipo_tabulador, "
                strSQL &= "b.sueldo, b.fondo_ahorro_empl, b.vales, b.total, b.gto_operacion AS BN, b.otra_remuneracion AS ORR, b.total AS TT, "
                strSQL &= "b.sobre_sueldo AS SS, ayuda_movilidad AS AM, b.fondo_ahorro_uia "
                strSQL &= "FROM tabuladores_puestos a WITH(NOLOCK), cat_tabuladores b WITH(NOLOCK) "
                strSQL &= "WHERE a.tabulador = b.tabulador AND a.nivel = b.nivel AND b.tipo in (0) "
                strSQL &= "AND a.puesto = '" & sPuesto & "' AND a.tipo_tabulador = 0  "
                strSQL &= "ORDER BY a.nivel, a.tipo, a.tipo_tabulador"

                datReader = objDat2.EjecutarComandoReader(strSQL, Parametros)

                If datReader.HasRows Then
                    dt.Load(datReader)
                End If

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

    Public Function ExtraerPuesto(sNumeroPlaza As String) As DataTable

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable

        Try

            strSQL = "SELECT a.puesto, (SELECT nombre_puesto FROM puestosx WITH(NOLOCK) WHERE puesto = a.puesto)  AS nombre_puesto "
            strSQL = strSQL & "FROM plantillas a WITH(NOLOCK) WHERE a.num_plaza = " & sNumeroPlaza

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

    Public Function ActualizarSolicitud(ByVal intID As Integer, ByVal strSector As String, ByVal strEstatus As String, ByVal intHorario As Integer, ByVal strJustificacion1 As String, ByVal strJustificacion2 As String _
                                        , ByVal strJustificacion3 As String, ByVal intEscolaridad As Integer, ByVal strExperiencia As String, ByVal strConocimientosTec As String, ByVal strConocimientosEsp As String, ByVal strUserApp As String, sObservaciones As String) As Boolean

        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim sSQL As New StringBuilder

        Try
            ActualizarSolicitud = False

            sSQL.AppendLine("")
            sSQL.AppendLine("Exec spU_SolicitudesPlazas ")
            sSQL.AppendLine(intID & ", ")
            sSQL.AppendLine("'" & strSector & "', ")
            sSQL.AppendLine("'" & strEstatus & "', ")
            sSQL.AppendLine(intHorario & ", ")
            sSQL.AppendLine("'" & strJustificacion1 & "', ")
            sSQL.AppendLine("'" & strJustificacion2 & "', ")
            sSQL.AppendLine("'" & strJustificacion3 & "', ")
            sSQL.AppendLine(intEscolaridad & ", ")
            sSQL.AppendLine("'" & strExperiencia & "', ")
            sSQL.AppendLine("'" & strConocimientosTec & "', ")
            sSQL.AppendLine("'" & strConocimientosEsp & "', ")
            sSQL.AppendLine("'" & strUserApp & "',")
            sSQL.AppendLine("'" & sObservaciones & "'")

            objDat.EjecutarComandoInsertUpdate(sSQL.ToString, Parametros)
            objDat.Dispose()


            ActualizarSolicitud = True

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & sSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Function

    Public Function CentroCosto(sEntidad As String) As String

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim strcentroCosto As String = String.Empty

        Try

            strSQL = "SELECT centro_costo FROM entidades WITH(NOLOCK) WHERE entidad = '" & sEntidad & "' "

            datReader = objDat.EjecutarComandoReader(strSQL, Parametros)
            If datReader.HasRows Then
                datReader.Read()
                strcentroCosto = datReader.Item("centro_costo").ToString()

            End If
            datReader.Close()


        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return strcentroCosto

    End Function

    Public Function _CargaSueldo(sPuesto As String, sTabulador As String, sNivel As String) As DataTable

        Dim strSQL As String = ""
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable


        Try
            strSQL = "SELECT distinct c.sueldo, c.sobre_sueldo, c.otra_remuneracion, c.ayuda_movilidad, c.gto_operacion "
            strSQL &= "FROM puestosx a, tabuladores_puestos b, cat_tabuladores c "
            strSQL &= "WHERE a.puesto = b.puesto AND b.tabulador = c.tabulador  AND c.nombre = '" & sTabulador & "' and c.nivel=" & sNivel & " and c.tipo=0"

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

    Public Function AsignaNiveles(sNumeroPlaza As String) As DataTable

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader1 As SqlDataReader
        Dim dt As New DataTable

        Try
            strSQL = "SELECT num_plaza, nivel, folio_entidad FROM plantillas " & " WHERE num_plaza=" & sNumeroPlaza

            datReader1 = objDat.EjecutarComandoReader(strSQL, Parametros)

            If datReader1.HasRows Then
                dt.Load(datReader1)
            End If

            datReader1.Close()
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

#Region "Envio de Correo"

    Public Function CorreoEmpleado(sModulo As String, sSeccion As String, sUsuario As String) As DataTable

        Dim strSQL As New System.Text.StringBuilder
        Dim objDat As New Dat(strConexion)
        Dim datReader As SqlDataReader
        Dim Parametros As New Collection
        Dim dt As New DataTable
        Try

            strSQL.Remove(0, strSQL.Length)
            strSQL.AppendLine("")
            strSQL.AppendLine("SELECT servidor, ")
            strSQL.AppendLine("       puerto, ")
            strSQL.AppendLine("       tls, ")
            strSQL.AppendLine("       default_credencial, ")
            strSQL.AppendLine("       de_nombre, ")
            strSQL.AppendLine("       de_correo, ")
            strSQL.AppendLine("       de_password, ")
            strSQL.AppendLine("       ISNULL(para_correo,'') para_correo, ")
            strSQL.AppendLine("       ISNULL(copia_correo,'') copia_correo, ")
            strSQL.AppendLine("       ISNULL(copia_oculta_correo,'') copia_oculta_correo, ")
            strSQL.AppendLine("       ISNULL(formato, 2) formato, ")
            strSQL.AppendLine("       ISNULL(importancia, 1) importancia ")
            strSQL.AppendLine("  FROM rh_config_correo ")
            strSQL.AppendLine(" WHERE modulo = '" & sModulo & "' ")
            strSQL.AppendLine("   AND seccion = '" & sSeccion & "' ")
            strSQL.AppendLine("   AND usuario = '" & sUsuario & "' ")

            datReader = objDat.EjecutarComandoReader(strSQL.ToString, Parametros)
            If datReader.HasRows Then
                dt.Load(datReader)
            End If

            datReader.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try


        Return dt
    End Function
#End Region

End Class
