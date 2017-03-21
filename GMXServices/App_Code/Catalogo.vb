Public Class Catalogo
    Public _ParserLogFileName As String
    Public _InterpreterLogFileName As String

    Public Property ParserLogFileName() As String
        Get
            Return _ParserLogFileName
        End Get
        Private Set(ByVal value As String)
            _ParserLogFileName = value
        End Set
    End Property
    Public Property InterpreterLogFileName() As String
        Get
            Return _InterpreterLogFileName
        End Get
        Private Set(ByVal value As String)
            _InterpreterLogFileName = value
        End Set
    End Property
End Class
