Imports System.Data.SqlClient
Imports Ibero.Plantillas.Entities
Imports LibreriaUIA.Funciones
Imports FrameWork

Public Class DataAccessCatalogos
#Region "Propiedades Privadas"

    Private strConexion As String = UsuarioConexion.strConexion
    Public TB As String = Chr(9)

#End Region

    Public Sub ActualizaMotivos(sDescripcion As String, sUsuario As String, sMotivo As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try
            strSQL = "UPDATE motivos_plantillas SET " & "descripcion = '" & sDescripcion & "', " & "usr_audit = '" & sUsuario & "', " & "fch_audit=getdate() " & "WHERE motivo = " & sMotivo

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub

    Public Sub ActualizaCondiciones(sDescripcion As String, sUsuario As String, sCondicion As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "UPDATE condicion_plantillas SET " & "descripcion = '" & sDescripcion & "', " & "usr_audit = '" & sUsuario & "', " & "fch_audit=getdate() " & "WHERE condicion = " & sCondicion

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub

    Public Sub ActualizaNivelesEstructura(sDescripcion As String, sUsuario As String, sConsecutivo As String, sNivel As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "UPDATE niveles_estructura SET "
            strSQL = strSQL & "descripcion = '" & sDescripcion & "', "

            If sNivel = 0 Or sNivel = "" Then
                strSQL = strSQL & "nivel_mando = null, "
            Else
                strSQL = strSQL & "nivel_mando = " & sNivel & ", "
            End If

            strSQL = strSQL & "usr_audit = '" & sUsuario & "', "
            strSQL = strSQL & "fch_audit = getdate() "
            strSQL = strSQL & "WHERE nivel_estructura = '" & sConsecutivo & "'"

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub



    Public Sub InsertaMotivos(sDescripcion As String, sUsuario As String, sMotivo As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "INSERT INTO motivos_plantillas VALUES(" & sMotivo & ", '" & sDescripcion & "', '" & sUsuario & "', " & "getdate())"

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub

    Public Sub InsertaCondiciones(sDescripcion As String, sUsuario As String, sCondicion As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "INSERT INTO condicion_plantillas VALUES(" & sCondicion & ", '" & sDescripcion & "', '" & sUsuario & "', getdate())"

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub

    Public Sub InsertaNivelesEstructura(sDescripcion As String, sUsuario As String, sConsecutivo As String, sNivel As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "INSERT INTO niveles_estructura VALUES('"
            strSQL = strSQL & sConsecutivo & "', '"
            strSQL = strSQL & sDescripcion & "', "
            If Val(sNivel) = 0 Or Trim(sNivel) = "" Then
                strSQL = strSQL & "null, '"
            Else
                strSQL = strSQL & Str(Val(sNivel)) & ", '"
            End If
            strSQL = strSQL & sUsuario & "', getdate())"

            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

    End Sub



    Public Sub EliminaMotivos(sIdentificador As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try
            strSQL = "DELETE FROM motivos_plantillas WHERE motivo = " & sIdentificador
            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Sub EliminaCondiciones(sIdentificador As String)

        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection

        Try

            strSQL = "DELETE FROM condicion_plantillas WHERE condicion = " & sIdentificador
            objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try
    End Sub

    Public Function EliminaNiveles(sIdentificador As String) As Integer

        Dim strSQL As String = String.Empty
        Dim strSQLCount As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim Cuenta As Integer = 0

        Try
            strSQLCount = "SELECT count(*) FROM entidades WHERE nivel_estructura = '" & sIdentificador & "'"
            Cuenta = objDat.EjecutarComandoEscalar(strSQLCount, Parametros)

            If Cuenta = 0 Then
                strSQLCount = "SELECT count(*) FROM estructura WHERE nivel_estructura = '" & sIdentificador & "'"
                Cuenta = objDat.EjecutarComandoEscalar(strSQLCount, Parametros)
            End If

            If Cuenta = 0 Then
                strSQL = "DELETE FROM niveles_estructura WHERE nivel_estructura = '" & sIdentificador & "'"
                objDat.EjecutarComandoInsertUpdate(strSQL, Parametros)
            End If

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return Cuenta

    End Function



    Public Function ObtieneMotivos() As List(Of String)
        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim resultList As New List(Of String)
        Dim dtrTemp As SqlDataReader
        Dim Texto As String

        Try
            strSQL = "SELECT motivo, descripcion FROM motivos_plantillas ORDER BY motivo"
            dtrTemp = objDat.EjecutarComandoReader(strSQL, Parametros)

            If dtrTemp.HasRows Then
                Do While dtrTemp.Read

                    Texto = dtrTemp.Item(0).ToString.Trim & TB
                    Texto = Texto & dtrTemp.Item(1).ToString.Trim

                    resultList.Add(Texto)

                Loop
            End If
            dtrTemp.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return resultList
    End Function

    Public Function ObtieneCondiciones() As List(Of String)
        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim resultList As New List(Of String)
        Dim dtrTemp As SqlDataReader
        Dim Texto As String

        Try
            strSQL = "SELECT condicion, descripcion FROM condicion_plantillas ORDER BY condicion"
            dtrTemp = objDat.EjecutarComandoReader(strSQL, Parametros)

            If dtrTemp.HasRows Then
                Do While dtrTemp.Read

                    Texto = dtrTemp.Item(0).ToString.Trim & TB
                    Texto = Texto & dtrTemp.Item(1).ToString.Trim

                    resultList.Add(Texto)

                Loop
            End If
            dtrTemp.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return resultList
    End Function

    Public Function ObtieneNiveles() As List(Of String)
        Dim strSQL As String = String.Empty
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim resultList As New List(Of String)
        Dim dtrTemp As SqlDataReader
        Dim Texto As String

        Try
            strSQL = "SELECT nivel_estructura, descripcion, nivel_mando FROM niveles_estructura"
            dtrTemp = objDat.EjecutarComandoReader(strSQL, Parametros)

            If dtrTemp.HasRows Then
                Do While dtrTemp.Read

                    Texto = dtrTemp.Item(0).ToString.Trim & TB
                    Texto = Texto & dtrTemp.Item(1).ToString.Trim
                    Texto = Texto & TB & dtrTemp.Item(2).ToString.Trim


                    resultList.Add(Texto)

                Loop
            End If
            dtrTemp.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return resultList

    End Function


    Public Function CargarDatosTabulador(sTabulador As String) As DataTable
        Dim strSQL As String = ""
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim dtrTemp As SqlDataReader
        Dim strTexto As String
        Dim I As Integer
        Dim J As Integer
        Dim strValor As String
        Dim dt As DataTable = New DataTable
        Try

            strSQL = "SELECT a.tabulador, b.descripcion, b.status, nivel, tipo, nombre, sueldo, sobre_sueldo, "
            strSQL &= "otra_remuneracion, ayuda_educa, subsidio_bienes, vales, ayuda_movilidad, gto_operacion, "
            strSQL &= "fondo_ahorro_uia, fondo_ahorro_empl, bono_anual, prima_vacaciones, aguinaldo, total, base, "
            strSQL &= "impuesto, neto, neto_liquido "
            strSQL &= "FROM cat_tabuladores a, cuadros_tabuladores b "
            strSQL &= "WHERE a.tabulador = b.tabulador AND tipo = 0 "


            'J = cboTabuladores.Text.IndexOf("-")
            'strValor = cboTabuladores.SelectedValue.ToString.Trim

            If Val(sTabulador) < 980 Then
                strSQL &= " AND a.tabulador = " & sTabulador & " "
            End If
            'If Mid(strValor, 1, 2) = "98" Then
            '    strSQL &= " AND b.descripcion = '" & cboTabuladores.Text.Substring(J + 1).Trim & "' "
            'End If
            'If Mid(strValor, 1, 2) = "99" Then
            '    strSQL &= " AND b.status = '" & cboTabuladores.Text.Substring(J + 1).Trim & "' "
            'End If

            strSQL &= "ORDER BY a.tabulador, nivel, status, tipo"


            dtrTemp = objDat.EjecutarComandoReader(strSQL, Parametros)
            If dtrTemp.HasRows Then
                Do While dtrTemp.Read
                    strTexto = ""
                    For I = 0 To dtrTemp.FieldCount - 1
                        If I = 0 Then strTexto = dtrTemp.Item(I) & TB

                        If (I = 1) Or (I = 2) Or (I = 3) Then strTexto &= dtrTemp.Item(I) & TB

                        If I = 4 Then
                            If dtrTemp.Item(I) = 0 Then
                                strTexto &= "Mensual"
                            Else
                                strTexto &= "Anual"
                            End If
                        End If

                        If I = 5 Then
                            strTexto &= TB & dtrTemp.Item(I)
                        End If

                        If Not IsDBNull(dtrTemp.Item(I)) Then
                            If I > 5 Then strTexto &= TB & Format(dtrTemp.Item(I), "$###,###,##0.00")
                        Else
                            strTexto &= TB & ""
                        End If
                    Next I
                    dt.Rows.Add(strTexto.Split(TB))
                Loop
            Else
                Beep()

            End If

            dtrTemp.Close()

        Catch ex As SqlException
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace, "Query: " & strSQL.ToString, "Procedure: ", "Server: " & ex.Server)
        Catch ex As Exception
            Throw New IberoExcepcion(ex.Message, ex, Me.GetType.FullName & Chr(13) & " Método:" & ex.StackTrace)
        Finally
            objDat.Dispose()
        End Try

        Return dt

    End Function


    Public Function CargaTiposNomina() As DataTable

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable


        Try

            strSQL = "SELECT -1 AS tipo_nomina, 'Seleccione' as descripcion UNION  SELECT tipo_nomina, descripcion "
            strSQL &= "FROM tipos_nominas "

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

    Public Function CargaCondicionPlantillas() As DataTable

        Dim strSQL As String
        Dim objDat As New Dat(strConexion)
        Dim Parametros As New Collection
        Dim datReader As SqlDataReader
        Dim dt As New DataTable


        Try

            strSQL = "SELECT 0 AS condicion, 'Seleccione' as descripcion UNION SELECT condicion, descripcion FROM condicion_plantillas WHERE condicion not in (5)"

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

End Class
