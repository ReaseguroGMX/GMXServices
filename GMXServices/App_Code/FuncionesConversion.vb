Imports Itenso.Rtf.Converter.Html
Imports Itenso.Rtf.Converter.Image
Imports Itenso.Rtf.Interpreter
Imports Itenso.Rtf.Parser
Imports Itenso.Rtf.Support
Imports System.Drawing.Imaging
Imports System.IO
Imports Itenso.Rtf

Public Class FuncionesConversion

#Region "Conversion RTFtoHTML"
    Public Function RempCarEsp(Cadena As String) As String

        Cadena = Replace(Cadena, "`c1", "Á")
        Cadena = Replace(Cadena, "`e1", "á")
        Cadena = Replace(Cadena, "`c9", "É")
        Cadena = Replace(Cadena, "`e9", "é")
        Cadena = Replace(Cadena, "`cd", "Í")
        Cadena = Replace(Cadena, "`ed", "í")
        Cadena = Replace(Cadena, "`d3", "Ó")
        Cadena = Replace(Cadena, "`f3", "ó")
        Cadena = Replace(Cadena, "`da", "Ú")
        Cadena = Replace(Cadena, "`fa", "ú")
        Cadena = Replace(Cadena, "`f1", "ñ")
        Cadena = Replace(Cadena, "`b0", "°")
        Cadena = Replace(Cadena, "`b4", ",")
        ' Cadena = Replace(Cadena, "`", ",")
        Cadena = Replace(Cadena, ",b7", "-")
        Cadena = Replace(Cadena, "`b7", "-")
        Cadena = Replace(Cadena, "<p>&nbsp;</p>", "")
        Cadena = Replace(Cadena, "<li></li>", "")
        Cadena = Replace(Cadena, "<li>", "<ul><li>")
        Cadena = Replace(Cadena, "</li>", "</li></ul>")

        Return Cadena
    End Function

    Public Function ConvertRtf2Html(rftString As String) As String

        'parser
        Dim rtfStructure As IRtfGroup = ParseRtf_String(rftString)
        If rtfStructure Is Nothing Then
            Return String.Empty
        End If

        'interpreter logger
        Dim interpreterLogger As RtfInterpreterListenerFileLogger
        interpreterLogger = Nothing
        Dim varCatalogo As New Catalogo
        If Not String.IsNullOrEmpty(varCatalogo._InterpreterLogFileName) Then
            interpreterLogger = New RtfInterpreterListenerFileLogger(varCatalogo._InterpreterLogFileName)
        End If


        'image converter
        Dim imageAdapter As RtfVisualImageAdapter = New RtfVisualImageAdapter(ImageFormat.Jpeg)
        Dim imageConvertSettings As RtfImageConvertSettings = New RtfImageConvertSettings(imageAdapter)
        imageConvertSettings.ScaleImage = True ' scale images
        Dim imageConverter As RtfImageConverter = New RtfImageConverter(imageConvertSettings)

        'rtf interpreter
        Dim interpreterSettings As RtfInterpreterSettings = New RtfInterpreterSettings
        interpreterSettings.IgnoreDuplicatedFonts = True
        Dim rtfDocument As IRtfDocument = RtfInterpreterTool.BuildDoc(rtfStructure, interpreterSettings, interpreterLogger, imageConverter)

        'html converter
        Dim htmlConvertSettings As RtfHtmlConvertSettings = New RtfHtmlConvertSettings()
        htmlConvertSettings.ConvertScope = RtfHtmlConvertScope.Content
        Dim htmlConverter As RtfHtmlConverter = New RtfHtmlConverter(rtfDocument)
        Return htmlConverter.Convert()

    End Function
    Public Function ParseRtf_String(CadenaRTF As String) As IRtfGroup
        Dim rtfStructure As IRtfGroup
        Dim varCatalogo As New Catalogo
        Using stream As Stream = GenerateStreamFromString(CadenaRTF)
            Dim structureBuilder As New RtfParserListenerStructureBuilder()
            Dim parser As New RtfParser(structureBuilder)
            parser.IgnoreContentAfterRootGroup = True ' support WordPad documents
            If Not String.IsNullOrEmpty(varCatalogo._ParserLogFileName) Then
                parser.AddParserListener(New RtfParserListenerFileLogger(varCatalogo._ParserLogFileName))

            End If
            parser.Parse(New RtfSource(stream))
            rtfStructure = structureBuilder.StructureRoot

        End Using
        Return rtfStructure
    End Function

    Public Function GenerateStreamFromString(s As String) As Stream

        Dim Stream As MemoryStream = New MemoryStream()
        Dim writer As StreamWriter = New StreamWriter(Stream, System.Text.Encoding.Default)
        writer.Write(s)
        writer.Flush()
        Stream.Position = 0
        Return Stream
    End Function

#End Region

End Class
