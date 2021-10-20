Imports System.Configuration
Imports LibreriaUIA.Funciones

Module Vars
    'Public strConexionPrincipal As String = ObtenerPasswordConexion(ConfigurationManager.ConnectionStrings("ConexionSIP2000").ConnectionString, eTipoConexion.SQLServer)
    Public strConexionPrincipal As String = String.Empty
    Public strConexion As String = ""
    Public strEmpresa As String = ObtieneEmpresa()

    'ABBR 10/02/2017. Ruta PathsHome.
    Public strConexionUsuario As String = ""

	Public NmbEquipo As String = My.Computer.Name
	Public strReportsPath As String = ConfigurationManager.AppSettings("ReportsPath")
	Public Drive As String = ConfigurationManager.AppSettings("Drive")
    'Public QueriesDir As String = ConfigurationManager.AppSettings("QueriesDir")
    'Public Formas As String = ConfigurationManager.AppSettings("Formas")


    Private _QueriesDir As String
    Public Property QueriesDir() As String
        Get
            Return "C:\" & ObtieneEmpresa() & ConfigurationManager.AppSettings("QueriesDir")
        End Get
        Set(ByVal value As String)
            _QueriesDir = value
        End Set
    End Property

    Private _Formas As String
    Public Property Formas() As String
        Get
            Return "C:\" & ObtieneEmpresa() & ConfigurationManager.AppSettings("Formas")
        End Get
        Set(ByVal value As String)
            _Formas = value
        End Set
    End Property

    Public TB As String = Chr(9)
    Public strUSER As String = ""
    Public strPASW As String = ""
    Public Fecpro As String
    Public Horapro As String
    Public FechaProceso As String
    Public AnioProceso As Integer
    Public TextoMes(12) As String
    Public Diasmes(12) As Short
    Public Separadores(4) As String
    Public Unidades(9) As String
    Public Decenas(9) As String
    Public Centenas(9) As String
    Public DiezUnit(6) As String
    Public FechaUltimaNomina As Integer
    Public nTVP(1001) As Integer
    Public TVP(50) As Integer
    Public BandHistoria As Boolean

    Public QueTipoPeriodo As Integer
    Public QueEmpresa As Integer = -1
    Public QueTipoNomina As Integer = -1
    Public TodosPermisos As Boolean = True
    Public nPrmsEmpresa As Integer = 0
    Public PrmsEmpresa As String = ""
    Public nPrmsRegional As Integer = 0
    Public PrmsRegional As String = ""
    Public nPrmsNomina As Integer = 0
    Public nPrmsDepto As Integer = 0
    Public PrmsDepto As String = ""
    Public PrmsNomina As String = ""
    Public PrmsClvIndent(20) As String
    Public nPrmsTipNomina As Integer = 0
    Public PrmsTipNomina As String = ""
    Public Tipo_Modulo As Integer = 0

    Public Const TXT_TODOS As String = "Todos"
    Public Const NULL_INTEGER As Short = 0
    Public Const FIRST_STR_POS As Short = 1

    Public Const RETURN_KEY As Short = 13

    Public objConexion As Conexion

    Public objEnsamblado As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly
    Public objProceso As System.Diagnostics.Process = System.Diagnostics.Process.GetCurrentProcess
    Public strRutaApp As String = Environment.CurrentDirectory & "\"
    Public strArchivoLogErr As String = ""
    Public strUserApp As String = ""
    Public strEquipo As String = My.Computer.Name

    'ABBR 04/04/2017. Impresion Solicitud
    Public NumPlazaP As Integer
    Public IEPlazaP As Integer
    Public NivelT As Integer
    Public PlazaT As Integer
    Public FolioEntidad As Integer
    Public PlazaReporta As Integer
    Public NivelPlaza As Integer
    Public DeptoP As String
    Public PuestoP As String
    Public KstP As Integer
    Public EstatusPlazaP As Integer
    Public ActivaplazaP As Integer
    Public TipoPlazaP As Integer
    Public BlDeptoP As Boolean
    Public BlPuestoP As Boolean
    Public CondicionPlazaP As Integer
    Public SueldoTP As Double
    Public PlazaRepP As String
    Public SSueldoTP As Double
    Public OtraRemTP As Double
    Public AyudaMovTP As Double
    Public GtoOpeTP As Double
    Public SeqDeptoP, SeqPuestoP, SeqSueldoP As Integer
    Public FolioEntidadP As Integer




    Public strNumEntidad As String = ""
    Public strNombreEntidad As String = ""
    Public intPlazas As Integer = 0
    Public strPlazas As String = ""
    Public strPuesto As String
    Public bolContinua As Boolean = False
    Public bolGuardar As Boolean = False
    Public blnTran As Boolean = False
    Public plazaAutorizada As Boolean = False
    '---------------------------
    Public strSueldoMensual As String = ""
    Public strFondoAhorro As String = ""
    Public strVales As String = ""
    Public strTotalMensual As String = ""
    Public strNivel As String = ""
    Public strBN As String = ""
    Public strORR As String = ""
    Public strTT As String = ""
    Public strSS As String = ""
    Public strAM As String = ""
    Public strPrevision As String = ""
    Public intVHorario As Integer = 0
    Public strVHorario As String = ""
    Public strVHorario1 As String = ""
    Public strObservaciones As String = ""
    '---------------------------
    Public strArea As String = ""
    Public strNombrePuesto As String = ""
    Public strVSector As String = ""
    Public strVTPuesto As String = ""
    '---------------------------
    Public intEID As Integer = 0
    Public strDEntidad As String = ""
    'Public strDNombreEntidad As String = ""
    Public strDSector As String = ""
    Public strDEstatus As String = ""
    Public intDHorario As Integer = 0
    Public intDEscolaridad As Integer = 0
    Public strDJustificacion1 As String = ""
    Public strDJustificacion2 As String = ""
    Public strDJustificacion3 As String = ""
    Public strDExperiencia As String = ""
    Public strDConocimientosTecnicos As String = ""
    Public strDConocimientosEspecificos As String = ""
    'Public strDPlazas As String = ""
    'Public intDPlazasSolicitadas As Integer = 0
    'Public strDPuesto As String = ""
    Public strDNoPlaza As String = ""

    Public strSector0 As String = ""
    Public strSector1 As String = ""
    Public strSector2 As String = ""

    Public strEstatus0 As String = ""
    Public strEstatus1 As String = ""

    Public strEscolaridad As String = ""
    Public strNivelAS As String = ""
    Public strNivelA As String = ""

    Public fchCreacionFormato As String = ""
    Public fchCreacion As String = ""
    Public fchCancela As String = ""
    Public strCentroCosto As String = ""
    Public strFechaCreacion As String = ""

    'ABBR 25/08/2016... Empate del modulo
    Public Enum eEmpresa
        Universidad = 1
        Otras = 3
    End Enum

    'ABBR 25/08/2016... Empate del modulo
    Public enuEmpresa As eEmpresa

    Public NombreEmpresaTitulo As String = ""

    'ABBR 25/08/2016... Empate del modulo
    Public Enum eTipoEmpresa
        Universidad = 1
        PrepaIbero = 2
        RadioIbero = 3
        CasaMeneses = 4
        IberoTijuana = 5
        IberoChalco = 6
    End Enum

    'ABBR 25/08/2016... Empate del modulo
    Public enuTipoEmpresa As eTipoEmpresa

    'ABBR 25/08/2016... Empate del modulo
    Public Enum eTipoEmpresaGit
        Universidad = 1
        PrepaIbero = 2
        RadioIbero = 3
        CasaMeneses = 4
        IberoTijuana = 5
        IberoChalco = 6
    End Enum

    'ABBR 25/08/2016... Empate del modulo
    Public enuTipoEmpresaGit As eTipoEmpresaGit


    'ABBR 25/08/2016... Empate del modulo
    Public Enum eTipoEmpresaGit
        Universidad = 1
        PrepaIbero = 2
        RadioIbero = 3
        CasaMeneses = 4
        IberoTijuana = 5
        IberoChalco = 6
    End Enum

    'ABBR 25/08/2016..... Empate del modulo
    Public enuTipoEmpresaGit As eTipoEmpresaGit

End Module
