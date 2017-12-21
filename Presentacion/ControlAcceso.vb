Public Class ControlAcceso

    Private _control As Control
    Private _permiso As List(Of String)
    Public Property Permiso() As List(Of String)
        Get
            Return _permiso
        End Get
        Set(ByVal value As List(Of String))
            _permiso = value
        End Set
    End Property

    Public Property Control() As Control
        Get
            Return _control
        End Get
        Set(ByVal value As Control)
            _control = value
        End Set
    End Property

    Public Sub New(control As Control, ParamArray permiso() As String)
        Me.Control = control

        Me.Permiso = New List(Of String)
        For Each p As String In permiso
            Me.Permiso.Add(p)
        Next
    End Sub

    Public Sub New()

    End Sub
End Class
