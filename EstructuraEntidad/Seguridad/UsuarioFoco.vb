Namespace Seguridad
    Public Class UsuarioFoco
        Private _idUsuario As Integer

        Private _nombreUsu As String
        Private _nombreCompledo As String
        Private _password As String
        Private _usrBasico As Boolean
        Private _esAdmin As Boolean
        Private _restringueObra As Boolean
        Private _email As String
        Private _estado As Boolean
        Private _idDepto As Integer
        Private _usuarioInterno As Boolean
        Private _departamento As String


        Private _firma As Byte()
        Private _obraPredeterminada As Integer

        Private _usuarioSoloWeb As Boolean
        Private _usuarioSoloFoco As String
        Private _usuarioAndroid As Boolean

        Public Property UsuarioFoco() As String
            Get
                Return _usuarioSoloFoco
            End Get
            Set(ByVal value As String)
                _usuarioSoloFoco = value
            End Set
        End Property

        Public Property UsuarioWeb() As Boolean
            Get
                Return _usuarioSoloWeb
            End Get
            Set(ByVal value As Boolean)
                _usuarioSoloWeb = value
            End Set
        End Property

        Public Property UsuarioAndroid() As Boolean
            Get
                Return _usuarioAndroid
            End Get
            Set(ByVal value As Boolean)
                _usuarioAndroid = value
            End Set
        End Property

        Public Property IdDepto() As String
            Get
                Return _idDepto
            End Get
            Set(ByVal value As String)
                _idDepto = value
            End Set
        End Property

        Public Property RestringueObra() As Boolean
            Get
                Return _restringueObra
            End Get
            Set(ByVal value As Boolean)
                _restringueObra = value
            End Set
        End Property

        Public Property ObraPredeterminada() As Integer
            Get
                Return _obraPredeterminada
            End Get
            Set(ByVal value As Integer)
                _obraPredeterminada = value
            End Set
        End Property

        Public Property Firma() As Byte()
            Get
                Return _firma
            End Get
            Set(ByVal value As Byte())
                _firma = value
            End Set
        End Property

        Public Property Email() As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property

        Public Property UsrBasico() As Boolean
            Get
                Return _usrBasico
            End Get
            Set(ByVal value As Boolean)
                _usrBasico = value
            End Set
        End Property

        Public Property Departamento() As String
            Get
                Return _departamento
            End Get
            Set(ByVal value As String)
                _departamento = value
            End Set
        End Property

        Public Property NombreCompleto() As String
            Get
                Return _nombreCompledo
            End Get
            Set(ByVal value As String)
                _nombreCompledo = value
            End Set
        End Property


        Public Property UsuarioInterno() As Boolean
            Get
                Return _usuarioInterno
            End Get
            Set(ByVal value As Boolean)
                _usuarioInterno = value
            End Set
        End Property

        Public Property EsAdmin() As Boolean
            Get
                Return _esAdmin
            End Get
            Set(ByVal value As Boolean)
                _esAdmin = value
            End Set
        End Property

        Public Property Estado() As Boolean
            Get
                Return _estado
            End Get
            Set(ByVal value As Boolean)
                _estado = value
            End Set
        End Property

        Public Property PassWord() As String
            Get
                Return _password
            End Get
            Set(ByVal value As String)
                _password = value
            End Set
        End Property

        Public Property NombreUsu() As String
            Get
                Return _nombreUsu
            End Get
            Set(ByVal value As String)
                _nombreUsu = value
            End Set
        End Property

        Public Property IdUsuario() As Integer
            Get
                Return _idUsuario
            End Get
            Set(ByVal value As Integer)
                _idUsuario = value
            End Set
        End Property




        Public Sub New()

        End Sub
    End Class


End Namespace
