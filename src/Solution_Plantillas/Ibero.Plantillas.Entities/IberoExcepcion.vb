
Public Class IberoExcepcion
    Inherits Exception

    Private _IsDataAccess As Boolean
    Public Property IsDataAccess() As Boolean
        Get
            Return _IsDataAccess
        End Get
        Set(ByVal value As Boolean)
            _IsDataAccess = value
        End Set
    End Property


public... desde develop
hotfix-comentario


    Private _IsBusinessLayer As Boolean
    Public Property IsBusinessLayer() As Boolean
        Get
            Return _IsBusinessLayer
        End Get
        Set(ByVal value As Boolean)
            _IsBusinessLayer = value
        End Set
    End Property

    Private _Procedure As String
    Public Property Procedure() As String
        Get
            Return _Procedure
        End Get
        Set(ByVal value As String)
            _Procedure = value
        End Set
    End Property

    Private _Query As String
    Public Property Query() As String
        Get
            Return _Query
        End Get
        Set(ByVal value As String)
            _Query = value
        End Set
    End Property


    Private _Layer As String
    Public Property Layer() As String
        Get
            Return _Layer
        End Get
        Set(ByVal value As String)
            _Layer = value
        End Set
    End Property

    Private _Server As String
    Public Property Server() As String
        Get
            Return _Server
        End Get
        Set(ByVal value As String)
            _Server = value
        End Set
    End Property

        Private _HolaMundo As String
    Public Property HolaMundo() As String
        Get
            Return _HolaMundo
        End Get
        Set(ByVal value As String)
            _HolaMundo = value
        End Set
    End Property


    Public Sub New()
    End Sub
    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(ByVal message As String, ByVal inner As Exception, ByVal sLayer As String)
        MyBase.New(message, inner)
        Me._Layer = sLayer
    End Sub

    Public Sub New(ByVal message As String, ByVal inner As Exception, ByVal sLayer As String, ByVal sQuery As String, ByVal sProcedure As String, ByVal sServer As String)
        MyBase.New(message, inner)
        Me._Query = sQuery
        Me._Layer = sLayer
        Me._Procedure = sProcedure
        Me._Server = sServer
    End Sub

End Class
